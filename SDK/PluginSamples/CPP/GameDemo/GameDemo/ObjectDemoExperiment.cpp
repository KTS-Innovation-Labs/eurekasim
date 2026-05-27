#include "stdafx.h"
#include "ObjectDemoExperiment.h"
#include "GameDemoSimulation.h"
#include "AddinSimulationManager.h"
#include "PropSliderCtrl.h"
#include <math.h>

using namespace ATL;

// CObjectPattern Implementation
CObjectPattern::CObjectPattern()
{
	m_strObjectType = _T("Cube");
	m_Color = RGB(0, 0, 255);
	m_strSimulationPattern = _T("Rotate");
	m_lSimulationInterval = 100;
}

void CObjectPattern::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		ar << m_strObjectType;
		ar << m_Color;
		ar << m_strSimulationPattern;
		ar << m_lSimulationInterval;
	}
	else
	{
		ar >> m_strObjectType;
		ar >> m_Color;
		ar >> m_strSimulationPattern;
		ar >> m_lSimulationInterval;
	}
}

void CObjectPattern::OnPropertyChanged(BSTR GroupName, BSTR PropertyName, BSTR PropertyValue)
{
	if (CString(GroupName) != OBJECT_PROPERTIES_TITLE)
	{
		return;
	}

	if (CString(PropertyName) == OBJECT_TYPE_TITLE)
	{
		m_strObjectType = CString(PropertyValue);
	}
	else if (CString(PropertyName) == OBJECT_COLOR_TITLE)
	{
		m_Color = (COLORREF)_ttol(CString(PropertyValue));
	}
	else if (CString(PropertyName) == OBJECT_SIMULATION_PATTERN_TITLE)
	{
		m_strSimulationPattern = CString(PropertyValue);
	}
	else if (CString(PropertyName) == OBJECT_SIMULATION_INTERVAL_TITLE)
	{
		m_lSimulationInterval = _ttol(CString(PropertyValue));
	}
}

// CGraphPoints Implementation
CGraphPoints::CGraphPoints()
{
	m_Angle = 0.0;
	m_x = 0.0;
	m_y = 0.0;
	m_z = 0.0;
}

// CFootballPenaltyGameState Implementation
CFootballPenaltyGameState::CFootballPenaltyGameState()
{
	Reset();
}

void CFootballPenaltyGameState::Reset()
{
	ballX = 0.0f;
	ballY = 0.2f;
	ballZ = -1.6f;
	ballSpeedX = 0.0f;
	ballSpeedY = 0.0f;
	ballSpeedZ = 0.0f;
	kickerAngle = 0.0f;
	kickPower = 0.0f;
	goalkeeperX = 0.0f;
	goalkeeperZ = 4.0f;
	goalkeeperSpeed = 0.0f;
	goalkeeperTimer = 0.0f;
	isBallMoving = false;
	isKicking = false;
	score = 0;
	attempts = 0;
	gameTime = 0.0f;
	goalkeeperDiveLeft = false;
	goalkeeperDiveRight = false;
	isGoalScored = false;
	isShotSaved = false;
	isShotMissed = false;
	isGameActive = true;
	kickAnimationTime = 0.0f;
}

// CObjectDemoExperiment Implementation
CObjectDemoExperiment::CObjectDemoExperiment(CAddinSimulationManager* pManager)
{
	m_pManager = pManager;
	ResetFootballPenalty();

	// Initialize enhanced camera variables
	m_bPenaltyKickView = false;
	m_bShowAimingReticle = true;
	m_bShowPowerMeter = true;
	m_bKeyboardActive = false;
}

CObjectDemoExperiment::~CObjectDemoExperiment()
{
	for (int i = 0; i < m_PlotInfoArray.GetCount(); i++)
	{
		CGraphPoints* pPoint = (CGraphPoints*)m_PlotInfoArray.GetAt(i);
		delete pPoint;
	}
	m_PlotInfoArray.RemoveAll();
}

void CObjectDemoExperiment::LoadAllExperiments()
{
	CComPtr<IExperimentTreeView> ExperimentTreeView;
	HRESULT HR = ExperimentTreeView.CoCreateInstance(CLSID_ExperimentTreeView);
	if (FAILED(HR))
	{
		return;
	}
	long SessionID = m_pManager->m_pAddin->m_lSessionID;
	ExperimentTreeView->DeleteAllExperiments(SessionID);
	ExperimentTreeView->SetRootNodeName(CString(CPP_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES).AllocSysString(), TRUE);

	ExperimentTreeView->AddExperiment(SessionID, CString(OBJECT_3D_TREE_ROOT_TITLE).AllocSysString(), CString(OBJECT_3D_TREE_LEAF_PATTERN_TITLE).AllocSysString());

	ExperimentTreeView->Refresh();
}

void CObjectDemoExperiment::OnTreeNodeSelect(BSTR ExperimentGroup, BSTR ExperimentName)
{
	OnReloadExperiment(ExperimentGroup, ExperimentName);
}

void CObjectDemoExperiment::OnTreeNodeDblClick(BSTR ExperimentGroup, BSTR ExperimentName)
{
	if (CString(ExperimentGroup) == OBJECT_3D_TREE_ROOT_TITLE && CString(ExperimentName) == OBJECT_3D_TREE_LEAF_PATTERN_TITLE)
	{
		ShowObjectProperties();
	}
	else
	{
		m_pManager->ResetPropertyGrid();
	}
}

void CObjectDemoExperiment::OnReloadExperiment(BSTR ExperimentGroup, BSTR ExperimentName)
{
	if (CString(ExperimentGroup) == OBJECT_3D_TREE_ROOT_TITLE)
	{
		DrawObject(ExperimentName);
	}
}

void CObjectDemoExperiment::ShowObjectProperties()
{
	CComPtr<IPropertyWindow> PropertyWindow;
	HRESULT HR = PropertyWindow.CoCreateInstance(CLSID_PropertyWindow);
	CString strGroupName = _T("");
	if (SUCCEEDED(HR))
	{
		PropertyWindow->RemoveAll();
		strGroupName = OBJECT_PROPERTIES_TITLE;
		PropertyWindow->AddPropertyGroup(strGroupName.AllocSysString());
		PropertyWindow->AddPropertyItemsAsString(strGroupName.AllocSysString(), OBJECT_TYPE_TITLE, OBJECT_TYPES, m_ObjectPattern.m_strObjectType.AllocSysString(), _T("Select the Object from the List"), FALSE);
		PropertyWindow->AddColorPropertyItem(strGroupName.AllocSysString(), OBJECT_COLOR_TITLE, m_ObjectPattern.m_Color, _T("Select the Color"));
		PropertyWindow->AddPropertyItemsAsString(strGroupName.AllocSysString(), OBJECT_SIMULATION_PATTERN_TITLE, OBJECT_PATTERN_TYPES, m_ObjectPattern.m_strSimulationPattern.AllocSysString(), _T("Select the Simulation Pattern"), FALSE);
		CString strInterval;
		strInterval.Format(_T("%d"), m_ObjectPattern.m_lSimulationInterval);
		PropertyWindow->AddPropertyItemAsString(strGroupName.AllocSysString(), OBJECT_SIMULATION_INTERVAL_TITLE, strInterval.AllocSysString(), _T("Simulation Interval In Milli Seconds"));

		PropertyWindow->EnableHeaderCtrl(FALSE);
		PropertyWindow->EnableDescriptionArea(TRUE);
		PropertyWindow->SetVSDotNetLook(TRUE);
		PropertyWindow->MarkModifiedProperties(TRUE, TRUE);
	}
}

void CObjectDemoExperiment::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		m_ObjectPattern.Serialize(ar);
	}
	else
	{
		m_ObjectPattern.Serialize(ar);
	}
}

void CObjectDemoExperiment::OnPropertyChanged(BSTR GroupName, BSTR PropertyName, BSTR PropertyValue)
{
	if (CString(GroupName) == OBJECT_PROPERTIES_TITLE)
	{
		m_ObjectPattern.OnPropertyChanged(GroupName, PropertyName, PropertyValue);
	}
	DrawScene();
}

void CObjectDemoExperiment::DrawScene()
{
	OnReloadExperiment(m_pManager->m_strExperimentGroup.AllocSysString(), m_pManager->m_strExperimentName.AllocSysString());
}

void CObjectDemoExperiment::DrawObject(CString ExperimentName)
{
	if (m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_PENALTY_KICK)
	{
		m_bPenaltyKickView = true;
		DrawPenaltyKickView();
	}
	else
	{
		m_bPenaltyKickView = false;
		if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_CUBE)
		{
			DrawCube();
		}
		else if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_BALL)
		{
			DrawBall();
		}
		else if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_PYRAMID)
		{
			DrawPyramid();
		}
		else if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_AEROPLANE)
		{
			DrawAeroplane();
		}
		else if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_CLOCK)
		{
			DrawClock();
		}
		else if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_FOOTBALLPENALTY)
		{
			DrawFootballPenalty();
		}
	}
}

// Cube Drawing
void CObjectDemoExperiment::DrawCube()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR))
	{
		return;
	}

	ApplicationView->InitializeEnvironment(TRUE);
	ApplicationView->BeginGraphicsCommands();

	ApplicationView->SetBkgColor(GetRValue(m_ObjectPattern.m_Color) / (float)255.0, GetGValue(m_ObjectPattern.m_Color) / (float)255.0,
		GetBValue(m_ObjectPattern.m_Color) / (float)255.0, 1.0);

	HR = ApplicationView->StartNewDisplayList();
	if (HR == E_FAIL)
	{
		return;
	}

	CComPtr<IOpenGLView> OpenGLView;
	HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR))
	{
		return;
	}

	// ORIGINAL CUBE CODE
	OpenGLView->glBegin(GL_QUAD_STRIP);
	OpenGLView->glColor3f(1.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(-0.3f, 0.3f, 0.3f);
	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(-0.3f, -0.3f, 0.3f);
	OpenGLView->glColor3f(1.0f, 1.0f, 1.0f);
	OpenGLView->glVertex3f(0.3f, 0.3f, 0.3f);
	OpenGLView->glColor3f(1.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(0.3f, -0.3f, 0.3f);
	OpenGLView->glColor3f(0.0f, 1.0f, 1.0f);
	OpenGLView->glVertex3f(0.3f, 0.3f, -0.3f);
	OpenGLView->glColor3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(0.3f, -0.3f, -0.3f);
	OpenGLView->glColor3f(0.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(-0.3f, 0.3f, -0.3f);
	OpenGLView->glColor3f(0.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(-0.3f, -0.3f, -0.3f);
	OpenGLView->glColor3f(1.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(-0.3f, 0.3f, 0.3f);
	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(-0.3f, -0.3f, 0.3f);
	OpenGLView->glEnd();

	OpenGLView->glBegin(GL_QUADS);
	OpenGLView->glColor3f(1.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(-0.3f, 0.3f, 0.3f);
	OpenGLView->glColor3f(1.0f, 1.0f, 1.0f);
	OpenGLView->glVertex3f(0.3f, 0.3f, 0.3f);
	OpenGLView->glColor3f(0.0f, 1.0f, 1.0f);
	OpenGLView->glVertex3f(0.3f, 0.3f, -0.3f);
	OpenGLView->glColor3f(0.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(-0.3f, 0.3f, -0.3f);
	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(-0.3f, -0.3f, 0.3f);
	OpenGLView->glColor3f(0.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(-0.3f, -0.3f, -0.3f);
	OpenGLView->glColor3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(0.3f, -0.3f, -0.3f);
	OpenGLView->glColor3f(1.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(0.3f, -0.3f, 0.3f);
	OpenGLView->glEnd();

	ApplicationView->EndNewDisplayList();
	ApplicationView->EndGraphicsCommands();
	ApplicationView->Refresh();
}

// Ball Drawing
void CObjectDemoExperiment::DrawBall()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR))
	{
		return;
	}

	ApplicationView->InitializeEnvironment(TRUE);
	ApplicationView->BeginGraphicsCommands();

	ApplicationView->SetBkgColor(GetRValue(m_ObjectPattern.m_Color) / (float)255, GetGValue(m_ObjectPattern.m_Color) / (float)255,
		GetBValue(m_ObjectPattern.m_Color) / (float)255, 1);

	int SECTIONS = 25;
	double RADIUS = 1.0;

	HR = ApplicationView->StartNewDisplayList();
	if (HR == E_FAIL)
	{
		return;
	}

	// ORIGINAL BALL CODE
	ApplicationView->SetColorf(0.0f, 0.0f, 1.0f);
	ApplicationView->DrawSphere(RADIUS, SECTIONS, SECTIONS);

	ApplicationView->SetColorf(1.0f, 1.0f, 1.0f);
	ApplicationView->DrawSphere(RADIUS / 1.5, SECTIONS, SECTIONS);

	ApplicationView->EndNewDisplayList();
	ApplicationView->EndGraphicsCommands();
	ApplicationView->Refresh();
}

// Pyramid Drawing
void CObjectDemoExperiment::DrawPyramid()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR))
	{
		return;
	}

	ApplicationView->InitializeEnvironment(TRUE);
	ApplicationView->BeginGraphicsCommands();

	ApplicationView->SetBkgColor(GetRValue(m_ObjectPattern.m_Color) / (float)255, GetGValue(m_ObjectPattern.m_Color) / (float)255,
		GetBValue(m_ObjectPattern.m_Color) / (float)255, 1);

	HR = ApplicationView->StartNewDisplayList();
	if (HR == E_FAIL)
	{
		return;
	}

	CComPtr<IOpenGLView> OpenGLView;
	HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR))
	{
		return;
	}

	// ORIGINAL PYRAMID CODE
	OpenGLView->glTranslatef(0.01f, 0.f, 0.01f);
	OpenGLView->glColor3f(0.0f, 0.4f, 0.8f);

	OpenGLView->glBegin(GL_TRIANGLES);
	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glColor3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(-1.0f, -1.0f, 1.0f);
	OpenGLView->glColor3f(0.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(1.0f, -1.0f, 1.0f);

	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glColor3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(1.0f, -1.0f, 1.0f);
	OpenGLView->glColor3f(0.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(1.0f, -1.0f, -1.0f);

	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glColor3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(1.0f, -1.0f, -1.0f);
	OpenGLView->glColor3f(0.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(-1.0f, -1.0f, -1.0f);

	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glColor3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(-1.0f, -1.0f, -1.0f);
	OpenGLView->glColor3f(0.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(-1.0f, -1.0f, 1.0f);

	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(-1.0f, -1.0f, 1.0f);
	OpenGLView->glColor3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(1.0f, -1.0f, 1.0f);
	OpenGLView->glColor3f(0.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(-1.0f, -1.0f, -1.0f);

	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(-1.0f, -1.0f, -1.0f);
	OpenGLView->glColor3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(1.0f, -1.0f, -1.0f);
	OpenGLView->glColor3f(0.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(1.0f, -1.0f, 1.0f);
	OpenGLView->glEnd();

	ApplicationView->EndNewDisplayList();
	ApplicationView->EndGraphicsCommands();
	ApplicationView->Refresh();
}

// Aeroplane Drawing
void CObjectDemoExperiment::DrawAeroplane()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR))
	{
		return;
	}

	ApplicationView->InitializeEnvironment(TRUE);
	ApplicationView->BeginGraphicsCommands();

	ApplicationView->SetBkgColor(GetRValue(m_ObjectPattern.m_Color) / (float)255, GetGValue(m_ObjectPattern.m_Color) / (float)255,
		GetBValue(m_ObjectPattern.m_Color) / (float)255, 1);

	HR = ApplicationView->StartNewDisplayList();
	if (HR == E_FAIL)
	{
		return;
	}

	CComPtr<IOpenGLView> OpenGLView;
	HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR))
	{
		return;
	}

	// ORIGINAL AEROPLANE CODE
	OpenGLView->glTranslatef(0.01f, 0.f, 0.01f);
	OpenGLView->glColor3f(0.0f, 0.4f, 0.8f);
	OpenGLView->glBegin(GL_TRIANGLES);
	OpenGLView->glVertex3f(0.f, 0.f, 0.001f);
	OpenGLView->glVertex3f(0.f, -0.5f, 1.f);
	OpenGLView->glVertex3f(0.f, 1.f, 0.001f);
	OpenGLView->glEnd();
	OpenGLView->glColor3f(0.0f, 0.3f, 0.7f);
	OpenGLView->glBegin(GL_TRIANGLE_STRIP);
	OpenGLView->glVertex3f(1.f, -0.5f, 0.f);
	OpenGLView->glVertex3f(0.f, 0.f, 0.2f);
	OpenGLView->glVertex3f(0.f, 2.f, 0.f);
	OpenGLView->glVertex3f(-1.f, -0.5f, 0.f);
	OpenGLView->glEnd();

	ApplicationView->EndNewDisplayList();
	ApplicationView->EndGraphicsCommands();
	ApplicationView->Refresh();
}

// Clock Drawing
void CObjectDemoExperiment::DrawClock()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR))
	{
		return;
	}

	ApplicationView->InitializeEnvironment(TRUE);
	ApplicationView->BeginGraphicsCommands();

	//Set the Background Color
	ApplicationView->SetBkgColor(GetRValue(m_ObjectPattern.m_Color) / (float)255, GetGValue(m_ObjectPattern.m_Color) / (float)255,
		GetBValue(m_ObjectPattern.m_Color) / (float)255, 1);

	HR = ApplicationView->StartNewDisplayList();
	if (HR == E_FAIL)
	{
		return;
	}

	float x1 = 0.0, y1 = 0.0;

	float segments = 100;
	float radius = 1.0;

	//Drawing Clock main Circle

	ApplicationView->SetLineWidth(4);
	ApplicationView->SetColorf(1, 0, 0);
	DrawCircle(segments, radius, x1, y1);

	//Drawing Minute Line
	ApplicationView->SetColorf(1, 1, 0);
	ApplicationView->SetLineWidth(2);
	ApplicationView->BeginDraw(GL_LINES);
	ApplicationView->Set2DVertexf(x1, y1);
	ApplicationView->Set2DVertexf(x1, (float)((radius / 3.0)*2.0));
	ApplicationView->EndDraw();

	//Drawing Hour Line
	ApplicationView->SetColorf(1, 0, 0);
	ApplicationView->SetLineWidth(2);
	ApplicationView->BeginDraw(GL_LINES);
	ApplicationView->Set2DVertexf(x1, y1);
	ApplicationView->Set2DVertexf((float)(radius / 3.0), (float)(radius / 3.0));
	ApplicationView->EndDraw();

	ApplicationView->EndNewDisplayList();
	ApplicationView->EndGraphicsCommands();
	ApplicationView->Refresh();
}

void CObjectDemoExperiment::DrawCircle(float segments, float radius, float sx, float sy)
{
	CComPtr<IOpenGLView> OpenGLView;
	HRESULT HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR))
	{
		return;
	}

	OpenGLView->glBegin(GL_LINE_LOOP);
	for (int i = 0; i<segments; i++)
	{
		float theta = (float)(2.0*3.142*float(i) / float(segments)); //get the current angle
		float x = (float)(radius*cos(theta));
		float y = (float)(radius*sin(theta));
		OpenGLView->glVertex2f(x + sx, y + sy);
	}
	OpenGLView->glEnd();
}

// ============================================================================
// PENALTY KICK CODE - DECREASED HEIGHTS
// ============================================================================

void CObjectDemoExperiment::ResetFootballPenalty()
{
	m_FootballGameState.Reset();
}

void CObjectDemoExperiment::DrawFootballPenalty()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR)) return;

	ApplicationView->InitializeEnvironment(TRUE);
	ApplicationView->BeginGraphicsCommands();

	// Set stadium background color
	ApplicationView->SetBkgColor(0.2f, 0.6f, 0.8f, 1.0f);

	HR = ApplicationView->StartNewDisplayList();
	if (HR == E_FAIL)
	{
		ApplicationView->EndGraphicsCommands();
		return;
	}

	// Update game state
	UpdateFootballPenalty();

	// Draw the complete scene
	DrawStadium();
	DrawEnvironment();
	DrawFootballField();
	DrawGoalPost();
	DrawNet();
	DrawFootball();
	DrawKicker();
	DrawGoalkeeper();
	DrawResultMessage();

	ApplicationView->EndNewDisplayList();
	ApplicationView->EndGraphicsCommands();
	ApplicationView->Refresh();
}

void CObjectDemoExperiment::UpdateFootballPenalty()
{
	m_FootballGameState.gameTime += 0.016f;

	HandleKeyboardInput();

	if (m_FootballGameState.isBallMoving)
	{
		m_FootballGameState.ballX += m_FootballGameState.ballSpeedX;
		m_FootballGameState.ballY += m_FootballGameState.ballSpeedY;
		m_FootballGameState.ballZ += m_FootballGameState.ballSpeedZ;
		m_FootballGameState.ballSpeedY -= BALL_GRAVITY * 0.5f;

		if (m_FootballGameState.ballY <= 0.2f && m_FootballGameState.ballZ < 4.0f)
		{
			m_FootballGameState.ballY = 0.2f;
			m_FootballGameState.ballSpeedY *= -0.6f;
			m_FootballGameState.ballSpeedX *= 0.8f;
			m_FootballGameState.ballSpeedZ *= 0.8f;

			if (fabs(m_FootballGameState.ballSpeedY) < 0.1f &&
				fabs(m_FootballGameState.ballSpeedX) < 0.1f &&
				fabs(m_FootballGameState.ballSpeedZ) < 0.1f &&
				m_FootballGameState.ballZ < 4.0f)
			{
				m_FootballGameState.isBallMoving = false;
				m_FootballGameState.isShotMissed = true;
				ResetBall();  
			}
		}

		// Check if ball is out of bounds or scored
		CheckGoal();
	}

	// Update goalkeeper
	UpdateGoalkeeper();

	// Continue kicker animation for longer
	if (m_FootballGameState.isKicking &&
		(m_FootballGameState.gameTime - m_FootballGameState.kickAnimationTime) > 0.5f)
	{
		m_FootballGameState.isKicking = false;
	}
}

// FIXED: Proper keyboard handling for corner kicks
void CObjectDemoExperiment::HandleKeyboardInput()
{
	// Always check keys when in football mode
	if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_FOOTBALLPENALTY ||
		m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_PENALTY_KICK)
	{
		if (!m_FootballGameState.isBallMoving)
		{
			if ((GetAsyncKeyState('D') & 0x8000) != 0)
			{
				m_FootballGameState.kickerAngle = -45.0f; // Left corner
				m_FootballGameState.kickPower = 1.0f;
				ProcessKick();
				Sleep(2);
			}
			else if ((GetAsyncKeyState('S') & 0x8000) != 0)
			{
				m_FootballGameState.kickerAngle = 0.0f;   // Straight shot
				m_FootballGameState.kickPower = 1.0f;
				ProcessKick();
				Sleep(2);
			}
			else if ((GetAsyncKeyState('A') & 0x8000) != 0)
			{
				m_FootballGameState.kickerAngle = 45.0f;  // Right corner
				m_FootballGameState.kickPower = 1.0f;
				ProcessKick();
				Sleep(2); // Prevent multiple kicks
			}
		}
	}
}

void CObjectDemoExperiment::ProcessKick()
{
	float angleRad = m_FootballGameState.kickerAngle * 3.14159f / 180.0f;
	float powerMultiplier = 9.0f; 
	if (fabs(m_FootballGameState.kickerAngle) > 30.0f) {
		float cornerPower = powerMultiplier * 0.9f; 
		float cornerHeight = 1.2f;
		float maxCornerX = 3.5f; // Limit X movement to keep within goal
		float targetX = sinf(angleRad) * maxCornerX;

		m_FootballGameState.ballSpeedX = targetX * 0.8f;
		m_FootballGameState.ballSpeedZ = cosf(angleRad) * cornerPower;
		m_FootballGameState.ballSpeedY = cornerHeight;
	}
	else {

		m_FootballGameState.ballSpeedX = sinf(angleRad) * powerMultiplier * 0.4f;
		m_FootballGameState.ballSpeedZ = cosf(angleRad) * powerMultiplier;
		m_FootballGameState.ballSpeedY = 1.5f; 
	}

	float maxBallSpeedX = 3.0f;
	if (fabs(m_FootballGameState.ballSpeedX) > maxBallSpeedX) {
		m_FootballGameState.ballSpeedX = (m_FootballGameState.ballSpeedX > 0) ? maxBallSpeedX : -maxBallSpeedX;
	}

	m_FootballGameState.isBallMoving = true;
	m_FootballGameState.attempts++;
	m_FootballGameState.isKicking = true;
	m_FootballGameState.kickAnimationTime = m_FootballGameState.gameTime;

	ResetShotResult();
	DrawScene();
}

void CObjectDemoExperiment::ResetShotResult()
{
	m_FootballGameState.isGoalScored = false;
	m_FootballGameState.isShotSaved = false;
	m_FootballGameState.isShotMissed = false;
	m_FootballGameState.isKicking = false;
}

void CObjectDemoExperiment::UpdateGoalkeeper()
{
	m_FootballGameState.goalkeeperTimer -= 0.016f;

	if (m_FootballGameState.goalkeeperTimer <= 0.0f && m_FootballGameState.isBallMoving)
	{
		if (!m_FootballGameState.goalkeeperDiveLeft && !m_FootballGameState.goalkeeperDiveRight)
		{
			float timeToGoal = (4.0f - m_FootballGameState.ballZ) / m_FootballGameState.ballSpeedZ;
			if (timeToGoal > 0) {
				float predictedBallX = m_FootballGameState.ballX + (m_FootballGameState.ballSpeedX * timeToGoal);

				float goalHalfWidth = 3.0f;

				if (predictedBallX < -goalHalfWidth * 0.7f)
				{
					m_FootballGameState.goalkeeperDiveLeft = true;
				}
				else if (predictedBallX > goalHalfWidth * 0.7f) 
				{
					m_FootballGameState.goalkeeperDiveRight = true;
				}
				else if (predictedBallX < -1.0f) // Left side
				{
					m_FootballGameState.goalkeeperDiveLeft = true;
				}
				else if (predictedBallX > 1.0f) // Right side
				{
					m_FootballGameState.goalkeeperDiveRight = true;
				}
				else // Center - random dive
				{
					if (rand() % 2 == 0) {
						m_FootballGameState.goalkeeperDiveLeft = true;
					}
					else {
						m_FootballGameState.goalkeeperDiveRight = true;
					}
				}
			}
		}

		float maxGoalkeeperX = 2.8f;

		if (m_FootballGameState.goalkeeperDiveLeft)
		{
			m_FootballGameState.goalkeeperX -= GOALKEEPER_SPEED;
			if (m_FootballGameState.goalkeeperX < -maxGoalkeeperX)
				m_FootballGameState.goalkeeperX = -maxGoalkeeperX;
		}
		else if (m_FootballGameState.goalkeeperDiveRight)
		{
			m_FootballGameState.goalkeeperX += GOALKEEPER_SPEED;
			if (m_FootballGameState.goalkeeperX > maxGoalkeeperX)
				m_FootballGameState.goalkeeperX = maxGoalkeeperX;
		}
	}
	else if (!m_FootballGameState.isBallMoving)
	{
		m_FootballGameState.goalkeeperX = 0.0f;
		m_FootballGameState.goalkeeperDiveLeft = false;
		m_FootballGameState.goalkeeperDiveRight = false;
		m_FootballGameState.goalkeeperTimer = GOALKEEPER_REACTION_TIME;
	}
}

void CObjectDemoExperiment::ResetBall()
{
	m_FootballGameState.ballX = 0.0f;
	m_FootballGameState.ballY = 0.2f;
	m_FootballGameState.ballZ = -2.9f;
	m_FootballGameState.ballSpeedX = 0.0f;
	m_FootballGameState.ballSpeedY = 0.0f;
	m_FootballGameState.ballSpeedZ = 0.0f;
	m_FootballGameState.kickPower = 0.0f;
	m_FootballGameState.isKicking = false;
}

void CObjectDemoExperiment::CheckGoal()
{
	float goalWidth = 6.0f;  
	float goalDepth = 1.0f;  

	if (m_FootballGameState.ballZ >= 3.8f &&
		m_FootballGameState.ballZ <= 4.0f + goalDepth && 
		fabs(m_FootballGameState.ballX) <= goalWidth / 2.0f + 0.2f &&
		m_FootballGameState.ballY >= 0.1f &&
		m_FootballGameState.ballY <= 1.8f)          
	{
		bool isSaved = false;

		if (m_FootballGameState.goalkeeperDiveLeft && m_FootballGameState.ballX < -1.0f) {
			if (fabs(m_FootballGameState.goalkeeperX - m_FootballGameState.ballX) < 1.8f) {
				isSaved = (rand() % 100) < 60; 
			}
		}
		else if (m_FootballGameState.goalkeeperDiveRight && m_FootballGameState.ballX > 1.0f) {
			if (fabs(m_FootballGameState.goalkeeperX - m_FootballGameState.ballX) < 1.8f) {
				isSaved = (rand() % 100) < 60; 
			}
		}
		else if (fabs(m_FootballGameState.ballX) < 1.5f && fabs(m_FootballGameState.goalkeeperX) < 1.5f) {
			// Central shots
			isSaved = (rand() % 100) < 50;
		}

		if (!isSaved)
		{
			m_FootballGameState.score++;
			m_FootballGameState.isGoalScored = true;
			m_FootballGameState.ballSpeedZ *= 0.5f;
			m_FootballGameState.ballSpeedX *= 0.5f;
			m_FootballGameState.ballSpeedY *= 0.3f;

			if (m_FootballGameState.ballZ > 7.0f) {
				m_FootballGameState.isBallMoving = false;
				ResetBall();
			}
		}
		else
		{
			m_FootballGameState.isShotSaved = true;
			m_FootballGameState.ballSpeedX *= -0.7f;
			m_FootballGameState.ballSpeedZ *= -0.4f;
			m_FootballGameState.ballSpeedY *= 0.5f;
		}
		return;
	}

	if (m_FootballGameState.ballZ > 12.0f || 
		fabs(m_FootballGameState.ballX) > 10.0f ||  
		m_FootballGameState.ballY < -2.0f)     
	{
		m_FootballGameState.isBallMoving = false;
		m_FootballGameState.isShotMissed = true;
		ResetBall();
	}
}

// ============================================================================
// DRAWING METHODS WITH DECREASED HEIGHTS
// ============================================================================

void CObjectDemoExperiment::DrawFootballField()
{
	CComPtr<IOpenGLView> OpenGLView;
	HRESULT HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR)) return;

	// Draw grass
	OpenGLView->glBegin(GL_QUADS);
	OpenGLView->glColor3f(0.3f, 0.7f, 0.3f);
	OpenGLView->glVertex3f(-8.0f, 0.0f, -4.0f);
	OpenGLView->glVertex3f(8.0f, 0.0f, -4.0f);
	OpenGLView->glVertex3f(8.0f, 0.0f, 8.0f);
	OpenGLView->glVertex3f(-8.0f, 0.0f, 8.0f);
	OpenGLView->glEnd();

	// Draw field markings
	OpenGLView->glBegin(GL_LINES);
	OpenGLView->glColor3f(1.0f, 1.0f, 1.0f);

	// Outer boundary
	OpenGLView->glVertex3f(-6.0f, 0.01f, -3.0f);
	OpenGLView->glVertex3f(6.0f, 0.01f, -3.0f);
	OpenGLView->glVertex3f(6.0f, 0.01f, -3.0f);
	OpenGLView->glVertex3f(6.0f, 0.01f, 6.0f);
	OpenGLView->glVertex3f(6.0f, 0.01f, 6.0f);
	OpenGLView->glVertex3f(-6.0f, 0.01f, 6.0f);
	OpenGLView->glVertex3f(-6.0f, 0.01f, 6.0f);
	OpenGLView->glVertex3f(-6.0f, 0.01f, -3.0f);

	// Center line
	OpenGLView->glVertex3f(-6.0f, 0.01f, 1.5f);
	OpenGLView->glVertex3f(6.0f, 0.01f, 1.5f);

	// Penalty spot
	OpenGLView->glVertex3f(-0.1f, 0.01f, -2.8f);
	OpenGLView->glVertex3f(0.1f, 0.01f, -2.8f);
	OpenGLView->glVertex3f(0.0f, 0.01f, -2.9f);
	OpenGLView->glVertex3f(0.0f, 0.01f, -2.7f);

	OpenGLView->glEnd();
}

// FIXED: Lower goalpost for decreased height
void CObjectDemoExperiment::DrawGoalPost()
{
	CComPtr<IOpenGLView> OpenGLView;
	HRESULT HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR)) return;

	OpenGLView->glColor3f(1.0f, 1.0f, 1.0f);

	float goalWidth = 6.0f;  // Wider goal for corner shots
	float goalHeight = 1.8f; // DECREASED HEIGHT from 2.5f to 1.8f

							 // Left post (moved further left for corners)
	OpenGLView->glPushMatrix();
	OpenGLView->glTranslatef(-goalWidth / 2, goalHeight / 2, 4.0f);
	OpenGLView->glScalef(0.1f, goalHeight, 0.1f);
	DrawCubePrimitive(1.0f);
	OpenGLView->glPopMatrix();

	// Right post (moved further right for corners)
	OpenGLView->glPushMatrix();
	OpenGLView->glTranslatef(goalWidth / 2, goalHeight / 2, 4.0f);
	OpenGLView->glScalef(0.1f, goalHeight, 0.1f);
	DrawCubePrimitive(1.0f);
	OpenGLView->glPopMatrix();

	// Crossbar
	OpenGLView->glPushMatrix();
	OpenGLView->glTranslatef(0.0f, goalHeight, 4.0f);
	OpenGLView->glScalef(goalWidth, 0.1f, 0.1f);
	DrawCubePrimitive(1.0f);
	OpenGLView->glPopMatrix();
}

void CObjectDemoExperiment::DrawPentagon(float radius)
{
	CComPtr<IOpenGLView> gl;
	HRESULT hr = gl.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(hr)) return;

	gl->glBegin(GL_POLYGON);
	for (int i = 0; i < 5; i++)
	{
		float angle = i * 72.0f * 3.14159f / 180.0f;  // 72° per side
		float x = cosf(angle) * radius;
		float y = sinf(angle) * radius;
		gl->glVertex2f(x, y);
	}
	gl->glEnd();
}

void CObjectDemoExperiment::DrawFootball()
{
    CComPtr<IApplicationView> view;
    HRESULT hr = view.CoCreateInstance(CLSID_ApplicationView);
    if (FAILED(hr)) return;
    
    CComPtr<IOpenGLView> gl;
    hr = gl.CoCreateInstance(CLSID_OpenGLView);
    if (FAILED(hr)) return;

    gl->glPushMatrix();
    gl->glTranslatef(m_FootballGameState.ballX, m_FootballGameState.ballY, m_FootballGameState.ballZ);
    
    // Make it spin
    float spin = m_FootballGameState.gameTime * 10.0f;
    gl->glRotatef(spin * 300.0f, 1.0f, 1.0f, 0.5f);
    
    float radius = 0.13f;
    
    // Draw a plain black sphere with no panels or patterns
    view->SetColorf(0.12f, 0.12f, 0.12f);
    view->DrawSphere(radius, 40, 40);
    
    gl->glPopMatrix();
}
// FIXED: Decreased height for kicker
void CObjectDemoExperiment::DrawKicker()
{
	CComPtr<IOpenGLView> gl;
	HRESULT hr = gl.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(hr)) return;

	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.0f, -2.0f);
	gl->glRotatef(m_FootballGameState.kickerAngle, 0.0f, 1.0f, 0.0f);

	float kickT = 0.0f;
	if (m_FootballGameState.isKicking)
	{
		kickT = sinf(m_FootballGameState.gameTime * 12.0f);
		if (kickT < 0.0f) kickT = 0.0f;
	}
	float kickAngle = -15.0f + 100.0f * kickT;

	// HEAD (Reduced height)
	gl->glColor3f(0.95f, 0.8f, 0.65f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.90f, 0.0f);  // Reduced from 1.10f
	gl->glScalef(0.20f, 0.18f, 0.20f);   // Reduced height from 0.22f to 0.18f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// JERSEY (Reduced height)
	gl->glColor3f(0.0f, 0.3f, 0.9f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.65f, 0.0f);  // Reduced from 0.80f
	gl->glScalef(0.44f, 0.35f, 0.26f);   // Reduced height from 0.45f to 0.35f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// Arms (Reduced height)
	gl->glColor3f(0.0f, 0.25f, 0.8f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.28f, 0.65f, 0.0f);  // Reduced from 0.80f
	gl->glRotatef(20.0f, 0.0f, 0.0f, 1.0f);
	gl->glScalef(0.13f, 0.30f, 0.13f);     // Reduced height from 0.38f to 0.30f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	gl->glPushMatrix();
	gl->glTranslatef(0.28f, 0.65f, 0.0f);   // Reduced from 0.80f
	gl->glRotatef(-20.0f, 0.0f, 0.0f, 1.0f);
	gl->glScalef(0.13f, 0.30f, 0.13f);     // Reduced height from 0.38f to 0.30f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// SHORTS (Reduced height)
	gl->glColor3f(1.0f, 1.0f, 1.0f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.40f, 0.0f);  // Reduced from 0.55f
	gl->glScalef(0.48f, 0.18f, 0.30f);   // Reduced height from 0.22f to 0.18f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// STANDING LEG (Reduced height)
	gl->glColor3f(0.95f, 0.8f, 0.65f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.11f, 0.25f, 0.0f);  // Reduced from 0.35f
	gl->glScalef(0.13f, 0.28f, 0.13f);     // Reduced height from 0.35f to 0.28f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// Standing leg foot
	gl->glColor3f(1.0f, 1.0f, 1.0f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.11f, 0.05f, 0.0f);  // Reduced from 0.10f
	gl->glScalef(0.15f, 0.20f, 0.15f);     // Reduced height from 0.25f to 0.20f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// KICKING LEG (Reduced height)
	gl->glPushMatrix();
	gl->glTranslatef(0.11f, 0.40f, 0.0f);  // Reduced from 0.55f
	gl->glRotatef(kickAngle, 1.0f, 0.0f, 0.0f);

	// Kicking leg thigh
	gl->glColor3f(0.95f, 0.8f, 0.65f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, -0.14f, 0.0f);  // Reduced from -0.175f
	gl->glScalef(0.13f, 0.28f, 0.13f);    // Reduced height from 0.35f to 0.28f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// Continue with the rest of the kicking leg with similar reductions...
	gl->glTranslatef(0.0f, -0.28f, 0.0f);  // Reduced from -0.35f
	float kneeBend = kickT > 0.4f ? 40.0f * (kickT - 0.4f) / 0.6f : 0.0f;
	gl->glRotatef(-kneeBend, 1.0f, 0.0f, 0.0f);

	// Shin (reduced)
	gl->glColor3f(0.95f, 0.8f, 0.65f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, -0.12f, 0.0f);  // Reduced from -0.15f
	gl->glScalef(0.11f, 0.24f, 0.11f);    // Reduced height from 0.30f to 0.24f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	gl->glPopMatrix(); // end kicking leg
	gl->glPopMatrix(); // end kicker
}

// FIXED: Decreased height for goalkeeper
void CObjectDemoExperiment::DrawGoalkeeper()
{
	CComPtr<IOpenGLView> gl;
	HRESULT hr = gl.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(hr)) return;

	gl->glPushMatrix();
	gl->glTranslatef(m_FootballGameState.goalkeeperX, 0.0f, 4.0f);
	gl->glRotatef(m_FootballGameState.kickerAngle, 0.0f, 1.0f, 0.0f);

	// DIVE ANIMATION
	float diveAngle = 0.0f;
	float diveOffsetY = 0.0f;
	float diveOffsetX = 0.0f;

	if (m_FootballGameState.goalkeeperDiveLeft)
	{
		diveAngle = -48.0f;        // big dramatic dive left
		diveOffsetY = 0.6f;        // DECREASED from 0.8f to 0.6f
		diveOffsetX = -0.45f;
	}
	else if (m_FootballGameState.goalkeeperDiveRight)
	{
		diveAngle = 48.0f;
		diveOffsetY = 0.25f;       // DECREASED from 0.35f to 0.25f
		diveOffsetX = 0.45f;
	}

	// ROOT TRANSFORM (dive)
	gl->glTranslatef(diveOffsetX, diveOffsetY, 0.0f);
	gl->glRotatef(diveAngle, 0.0f, 0.0f, 1.0f);

	// HEAD (DECREASED HEIGHT)
	gl->glColor3f(0.95f, 0.8f, 0.65f);   // skin
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 1.12f, 0.0f); // DECREASED from 1.32f to 1.12f
	gl->glScalef(0.22f, 0.24f, 0.22f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// GOALKEEPER JERSEY (DECREASED HEIGHT)
	gl->glColor3f(0.0f, 0.9f, 0.3f);     // neon green
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.80f, 0.0f); // DECREASED from 0.95f to 0.80f
	gl->glScalef(0.48f, 0.48f, 0.28f);   // DECREASED height from 0.58f to 0.48f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// ARMS (extended during dive - DECREASED HEIGHT)
	gl->glColor3f(0.0f, 0.8f, 0.25f);    // slightly darker green sleeves

										 // Left arm - stretched out
	gl->glPushMatrix();
	gl->glTranslatef(-0.35f, 0.90f, 0.0f); // DECREASED from 1.05f to 0.90f
	gl->glRotatef(-70.0f + diveAngle * 0.8f, 0.0f, 0.0f, 1.0f);
	gl->glScalef(0.14f, 0.45f, 0.14f);   // DECREASED height from 0.50f to 0.45f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// Right arm - stretched out
	gl->glPushMatrix();
	gl->glTranslatef(0.35f, 0.90f, 0.0f); // DECREASED from 1.05f to 0.90f
	gl->glRotatef(70.0f + diveAngle * 0.8f, 0.0f, 0.0f, 1.0f);
	gl->glScalef(0.14f, 0.45f, 0.14f);   // DECREASED height from 0.50f to 0.45f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// Gloves (black)
	gl->glColor3f(0.1f, 0.1f, 0.1f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.58f, 0.90f, 0.0f); // DECREASED from 1.05f to 0.90f
	gl->glScalef(0.18f, 0.14f, 0.18f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glPushMatrix();
	gl->glTranslatef(0.58f, 0.90f, 0.0f); // DECREASED from 1.05f to 0.90f
	gl->glScalef(0.18f, 0.14f, 0.18f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// SHORTS (black - DECREASED HEIGHT)
	gl->glColor3f(0.1f, 0.1f, 0.1f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.55f, 0.0f); // DECREASED from 0.68f to 0.55f
	gl->glScalef(0.50f, 0.24f, 0.32f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// LEGS (tucked during dive - DECREASED HEIGHT)
	// Left leg
	gl->glColor3f(0.95f, 0.8f, 0.65f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.11f, 0.35f, 0.0f); // DECREASED from 0.45f to 0.35f
	gl->glRotatef(20.0f + diveAngle * 0.6f, 1.0f, 0.0f, 0.0f);
	gl->glScalef(0.14f, 0.38f, 0.14f);    // DECREASED height from 0.48f to 0.38f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// Right leg
	gl->glPushMatrix();
	gl->glTranslatef(0.11f, 0.35f, 0.0f); // DECREASED from 0.45f to 0.35f
	gl->glRotatef(-30.0f + diveAngle * 0.7f, 1.0f, 0.0f, 0.0f);
	gl->glScalef(0.14f, 0.38f, 0.14f);    // DECREASED height from 0.48f to 0.38f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// Socks (green - DECREASED HEIGHT)
	gl->glColor3f(0.0f, 0.7f, 0.2f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.11f, 0.08f, 0.0f); // DECREASED from 0.12f to 0.08f
	gl->glScalef(0.16f, 0.25f, 0.16f);     // DECREASED height from 0.30f to 0.25f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glPushMatrix();
	gl->glTranslatef(0.11f, 0.08f, 0.0f); // DECREASED from 0.12f to 0.08f
	gl->glScalef(0.16f, 0.25f, 0.16f);     // DECREASED height from 0.30f to 0.25f
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	// Boots
	gl->glColor3f(0.1f, 0.1f, 0.1f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.11f, 0.0f, 0.06f);
	gl->glScalef(0.16f, 0.10f, 0.26f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glPushMatrix();
	gl->glTranslatef(0.11f, 0.0f, 0.06f);
	gl->glScalef(0.16f, 0.10f, 0.26f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();

	gl->glPopMatrix(); // end goalkeeper
}

void CObjectDemoExperiment::DrawEnvironment()
{
	CComPtr<IOpenGLView> gl;
	HRESULT hr = gl.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(hr)) return;

	// SKY – nice daytime gradient (light blue → pale near horizon)
	gl->glBegin(GL_QUADS);
	gl->glColor3f(0.45f, 0.75f, 0.98f);   // top
	gl->glVertex3f(-50.0f, 25.0f, -50.0f);
	gl->glVertex3f(50.0f, 25.0f, -50.0f);
	gl->glVertex3f(50.0f, 25.0f, 60.0f);
	gl->glVertex3f(-50.0f, 25.0f, 60.0f);

	gl->glColor3f(0.70f, 0.88f, 0.99f);   // bottom (paler near horizon)
	gl->glVertex3f(-50.0f, 0.0f, -50.0f);
	gl->glVertex3f(50.0f, 0.0f, -50.0f);
	gl->glColor3f(0.65f, 0.85f, 0.99f);
	gl->glVertex3f(50.0f, 0.0f, 60.0f);
	gl->glVertex3f(-50.0f, 0.0f, 60.0f);
	gl->glEnd();

	// GREEN PITCH
	gl->glBegin(GL_QUADS);
	gl->glColor3f(0.18f, 0.55f, 0.10f);   // rich green
	gl->glVertex3f(-12.0f, 0.01f, -8.0f);
	gl->glVertex3f(12.0f, 0.01f, -8.0f);
	gl->glVertex3f(12.0f, 0.01f, 12.0f);
	gl->glVertex3f(-12.0f, 0.01f, 12.0f);
	gl->glEnd();

	// WHITE PITCH LINES (real football field markings)
	gl->glColor3f(1.0f, 1.0f, 1.0f);
	gl->glLineWidth(3.0f);

	gl->glBegin(GL_LINES);
	// Center line
	gl->glVertex3f(-12.0f, 0.02f, 2.0f);
	gl->glVertex3f(12.0f, 0.02f, 2.0f);
	// Center circle
	for (int i = 0; i < 32; i++)
	{
		float a = i * 11.25f * 3.14159f / 180.0f;
		float x1 = cosf(a) * 2.0f;
		float z1 = sinf(a) * 2.0f + 2.0f;
		float a2 = (i + 1) * 11.25f * 3.14159f / 180.0f;
		float x2 = cosf(a2) * 2.0f;
		float z2 = sinf(a2) * 2.0f + 2.0f;
		gl->glVertex3f(x1, 0.02f, z1);
		gl->glVertex3f(x2, 0.02f, z2);
	}
	// Penalty box (near goal at z=4)
	gl->glVertex3f(-4.0f, 0.02f, 9.0f);
	gl->glVertex3f(4.0f, 0.02f, 9.0f);
	gl->glVertex3f(-4.0f, 0.02f, 9.0f);
	gl->glVertex3f(-4.0f, 0.02f, 12.0f);
	gl->glVertex3f(4.0f, 0.02f, 9.0f);
	gl->glVertex3f(4.0f, 0.02f, 12.0f);
	// Goal line
	gl->glVertex3f(-5.0f, 0.02f, 12.0f);
	gl->glVertex3f(5.0f, 0.02f, 12.0f);
	gl->glEnd();

	// STADIUM STANDS (simple but effective)
	// Left stand
	gl->glBegin(GL_QUADS);
	gl->glColor3f(0.15f, 0.15f, 0.20f);   // dark gray structure
	gl->glVertex3f(-20.0f, 0.0f, -15.0f);
	gl->glVertex3f(-12.0f, 0.0f, -10.0f);
	gl->glVertex3f(-12.0f, 8.0f, -10.0f);
	gl->glVertex3f(-20.0f, 6.0f, -15.0f);

	gl->glColor3f(0.9f, 0.3f, 0.1f);   // red seats
	gl->glVertex3f(-19.5f, 1.0f, -14.5f);
	gl->glVertex3f(-12.5f, 1.0f, -10.5f);
	gl->glVertex3f(-12.5f, 7.0f, -10.5f);
	gl->glVertex3f(-19.5f, 5.0f, -14.5f);
	// Right stand
	gl->glColor3f(0.15f, 0.15f, 0.20f);
	gl->glVertex3f(20.0f, 0.0f, -15.0f);
	gl->glVertex3f(12.0f, 0.0f, -10.0f);
	gl->glVertex3f(12.0f, 8.0f, -10.0f);
	gl->glVertex3f(20.0f, 6.0f, -15.0f);

	gl->glColor3f(0.1f, 0.3f, 0.9f);   // blue seats
	gl->glVertex3f(19.5f, 1.0f, -14.5f);
	gl->glVertex3f(12.5f, 1.0f, -10.5f);
	gl->glVertex3f(12.5f, 7.0f, -10.5f);
	gl->glVertex3f(19.5f, 5.0f, -14.5f);
	gl->glEnd();

	// FAR BACKGROUND STANDS (for depth)
	gl->glBegin(GL_QUADS);
	gl->glColor3f(0.4f, 0.7f, 0.2f);   // distant crowd green
	gl->glVertex3f(-40.0f, 0.0f, 50.0f);
	gl->glVertex3f(40.0f, 0.0f, 50.0f);
	gl->glVertex3f(40.0f, 20.0f, 50.0f);
	gl->glVertex3f(-40.0f, 20.0f, 50.0f);
	gl->glEnd();
}

void CObjectDemoExperiment::DrawCubePrimitive(float size)
{
	CComPtr<IOpenGLView> OpenGLView;
	HRESULT HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR)) return;

	float s = size / 2.0f;

	OpenGLView->glBegin(GL_QUADS);

	// Front face
	OpenGLView->glVertex3f(-s, -s, s);
	OpenGLView->glVertex3f(s, -s, s);
	OpenGLView->glVertex3f(s, s, s);
	OpenGLView->glVertex3f(-s, s, s);

	// Back face
	OpenGLView->glVertex3f(-s, -s, -s);
	OpenGLView->glVertex3f(-s, s, -s);
	OpenGLView->glVertex3f(s, s, -s);
	OpenGLView->glVertex3f(s, -s, -s);

	// Top face
	OpenGLView->glVertex3f(-s, s, -s);
	OpenGLView->glVertex3f(-s, s, s);
	OpenGLView->glVertex3f(s, s, s);
	OpenGLView->glVertex3f(s, s, -s);

	// Bottom face
	OpenGLView->glVertex3f(-s, -s, -s);
	OpenGLView->glVertex3f(s, -s, -s);
	OpenGLView->glVertex3f(s, -s, s);
	OpenGLView->glVertex3f(-s, -s, s);

	// Right face
	OpenGLView->glVertex3f(s, -s, -s);
	OpenGLView->glVertex3f(s, s, -s);
	OpenGLView->glVertex3f(s, s, s);
	OpenGLView->glVertex3f(s, -s, s);

	// Left face
	OpenGLView->glVertex3f(-s, -s, -s);
	OpenGLView->glVertex3f(-s, -s, s);
	OpenGLView->glVertex3f(-s, s, s);
	OpenGLView->glVertex3f(-s, s, -s);

	OpenGLView->glEnd();
}

// Enhanced Penalty Kick View Methods
void CObjectDemoExperiment::DrawPenaltyKickView()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR)) return;

	ApplicationView->InitializeEnvironment(TRUE);
	ApplicationView->BeginGraphicsCommands();

	// Set stadium background color
	ApplicationView->SetBkgColor(0.2f, 0.6f, 0.8f, 1.0f);

	HR = ApplicationView->StartNewDisplayList();
	if (HR == E_FAIL)
	{
		ApplicationView->EndGraphicsCommands();
		return;
	}

	// Update game state
	UpdateFootballPenalty();

	// Draw the scene from penalty kick perspective
	DrawStadium();
	DrawEnvironment();
	DrawFootballField();
	DrawGoalPost();
	DrawNet();
	DrawFootball();
	DrawKicker();
	DrawGoalkeeper();

	// Draw HUD elements
	DrawPenaltyKickHUD();

	// Draw result message if needed
	DrawResultMessage();

	ApplicationView->EndNewDisplayList();
	ApplicationView->EndGraphicsCommands();
	ApplicationView->Refresh();
}

void CObjectDemoExperiment::DrawPenaltyKickHUD()
{
	DrawAimingReticle();
}

void CObjectDemoExperiment::DrawAimingReticle()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR)) return;

	if (!m_bShowAimingReticle) return;

	// Draw aiming reticle (crosshair)
	ApplicationView->SetColorf(1.0f, 1.0f, 1.0f);
	ApplicationView->SetLineWidth(2.0f);

	ApplicationView->BeginDraw(GL_LINES);
	// Horizontal line
	ApplicationView->Set2DVertexf(-0.05f, 0.0f);
	ApplicationView->Set2DVertexf(0.05f, 0.0f);
	// Vertical line
	ApplicationView->Set2DVertexf(0.0f, -0.05f);
	ApplicationView->Set2DVertexf(0.0f, 0.05f);
	ApplicationView->EndDraw();

	// Draw aiming direction indicator based on kicker angle
	float angleIndicatorX = sinf(m_FootballGameState.kickerAngle * 3.14159f / 180.0f) * 0.1f;
	ApplicationView->SetColorf(1.0f, 0.0f, 0.0f);
	ApplicationView->BeginDraw(GL_LINES);
	ApplicationView->Set2DVertexf(0.0f, -0.05f);
	ApplicationView->Set2DVertexf(angleIndicatorX, -0.08f);
	ApplicationView->EndDraw();
}

void CObjectDemoExperiment::DrawResultMessage()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR)) return;

	// Show result message for a short time after shot
	if (m_FootballGameState.isGoalScored || m_FootballGameState.isShotSaved || m_FootballGameState.isShotMissed)
	{
		CString strMessage;
		if (m_FootballGameState.isGoalScored)
		{
			strMessage = _T("GOAL!");
			ApplicationView->SetColorf(0.0f, 1.0f, 0.0f); // Green for goal
		}
		else if (m_FootballGameState.isShotSaved)
		{
			strMessage = _T("SAVED!");
			ApplicationView->SetColorf(1.0f, 1.0f, 0.0f); // Yellow for saved
		}
		else if (m_FootballGameState.isShotMissed)
		{
			strMessage = _T("MISSED!");
			ApplicationView->SetColorf(1.0f, 0.0f, 0.0f); // Red for missed
		}

		// Draw message background
		ApplicationView->BeginDraw(GL_QUADS);
		ApplicationView->Set2DVertexf(-0.2f, 0.1f);
		ApplicationView->Set2DVertexf(0.2f, 0.1f);
		ApplicationView->Set2DVertexf(0.2f, 0.2f);
		ApplicationView->Set2DVertexf(-0.2f, 0.2f);
		ApplicationView->EndDraw();

		// Draw message border
		ApplicationView->SetColorf(1.0f, 1.0f, 1.0f);
		ApplicationView->BeginDraw(GL_LINE_LOOP);
		ApplicationView->Set2DVertexf(-0.2f, 0.1f);
		ApplicationView->Set2DVertexf(0.2f, 0.1f);
		ApplicationView->Set2DVertexf(0.2f, 0.2f);
		ApplicationView->Set2DVertexf(-0.2f, 0.2f);
		ApplicationView->EndDraw();
	}
}

void CObjectDemoExperiment::DrawStadium()
{
	CComPtr<IOpenGLView> OpenGLView;
	HRESULT HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR)) return;

	// Draw simple stadium structure
	OpenGLView->glColor3f(0.4f, 0.4f, 0.4f);

	// Stadium walls
	OpenGLView->glBegin(GL_QUADS);
	// Left wall
	OpenGLView->glVertex3f(-8.0f, 0.0f, -4.0f);
	OpenGLView->glVertex3f(-8.0f, 3.0f, -4.0f);
	OpenGLView->glVertex3f(-8.0f, 3.0f, 8.0f);
	OpenGLView->glVertex3f(-8.0f, 0.0f, 8.0f);

	// Right wall
	OpenGLView->glVertex3f(8.0f, 0.0f, -4.0f);
	OpenGLView->glVertex3f(8.0f, 3.0f, -4.0f);
	OpenGLView->glVertex3f(8.0f, 3.0f, 8.0f);
	OpenGLView->glVertex3f(8.0f, 0.0f, 8.0f);

	// Back wall
	OpenGLView->glVertex3f(-8.0f, 0.0f, 8.0f);
	OpenGLView->glVertex3f(-8.0f, 3.0f, 8.0f);
	OpenGLView->glVertex3f(8.0f, 3.0f, 8.0f);
	OpenGLView->glVertex3f(8.0f, 0.0f, 8.0f);
	OpenGLView->glEnd();
}

void CObjectDemoExperiment::DrawNet()
{
	CComPtr<IOpenGLView> OpenGLView;
	HRESULT HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR)) return;

	float goalWidth = 6.0f;
	float goalHeight = 1.8f; // DECREASED HEIGHT from 2.5f to 1.8f

	OpenGLView->glColor4f(1.0f, 1.0f, 1.0f, 0.3f);

	// Draw net mesh (simplified)
	for (int i = 0; i <= 8; i++)
	{
		float x = -goalWidth / 2 + (goalWidth / 8) * i;

		// Vertical net lines
		OpenGLView->glBegin(GL_LINES);
		OpenGLView->glVertex3f(x, 0.0f, 4.0f);
		OpenGLView->glVertex3f(x, goalHeight, 4.0f);
		OpenGLView->glEnd();
	}

	for (int i = 0; i <= 4; i++)
	{
		float y = (goalHeight / 4) * i;

		// Horizontal net lines
		OpenGLView->glBegin(GL_LINES);
		OpenGLView->glVertex3f(-goalWidth / 2, y, 4.0f);
		OpenGLView->glVertex3f(goalWidth / 2, y, 4.0f);
		OpenGLView->glEnd();
	}
}

// Simulation Methods
void CObjectDemoExperiment::StartSimulation(BSTR ExperimentGroup, BSTR ExperimentName)
{
	if (CString(ExperimentGroup) == OBJECT_3D_TREE_ROOT_TITLE && CString(ExperimentName) == OBJECT_3D_TREE_LEAF_PATTERN_TITLE)
	{
		StartObjectSimulation();
	}
}

void CObjectDemoExperiment::StartObjectSimulation()
{
	m_pManager->SetSimulationStatus(TRUE);
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR)) return;

	float Angle = 0.0f, x = 0.0f, y = 0.0f, z = 0.0f;
	int i = 0;

	while (m_pManager->m_bSimulationActive)
	{
		ApplicationView->BeginGraphicsCommands();

		if (m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_ROTATE)
		{
			x = 0.1f, y = 1.0f, z = 0.1f;
		}
		else if (m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_RANDOM)
		{
			switch (i)
			{
			case 0:
				x = 1.0f, y = 0.1f, z = 0.1f;
				break;
			case 1:
				x = 0.1f, y = 1.0f, z = 0.1f;
				break;
			case 2:
				x = 0.1f, y = 0.1f, z = 1.0f;
				break;
			}
			i = rand() % 3;
		}
		else if (m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_PENALTY_KICK)
		{
			// For penalty kick view, update game state instead of rotating
			UpdateFootballPenalty();
			x = 0; y = 0; z = 0;
		}

		if (!m_pManager->m_b3DMode && m_ObjectPattern.m_strSimulationPattern != OBJECT_PATTERN_TYPE_PENALTY_KICK)
		{
			x = 0;
			y = 0;
		}

		if (m_ObjectPattern.m_strSimulationPattern != OBJECT_PATTERN_TYPE_PENALTY_KICK)
		{
			ApplicationView->RotateObject(Angle, x, y, z);
		}

		ApplicationView->EndGraphicsCommands();
		ApplicationView->Refresh();

		OnNextSimulationPoint(Angle, x, y, z);

		Angle = Angle + 5;
		if (Angle > 360)
		{
			Angle = 0;
		}
		Sleep(m_ObjectPattern.m_lSimulationInterval);
	}
}

void CObjectDemoExperiment::OnNextSimulationPoint(float Angle, float x, float y, float z)
{
	CString strStatus;
	strStatus.Format(_T("Simulation Points (Angle:%.3f,X:%.3f,Y:%.3f,Z:%.3f)\n"), Angle, x, y, z);

	if (m_pManager->m_bShowExperimentalParamaters)
	{
		m_pManager->AddOperationStatus(strStatus);
	}

	if (m_pManager->m_bLogSimulationResultsToCSVFile)
	{
		CString strLog;
		strLog.Format(_T("%.3f,%.3f,%.3f,%.3f\n"), Angle, x, y, z);
		m_pManager->LogSimulationPoint(strLog);
	}

	if (m_pManager->m_bDisplayRealTimeGraph)
	{
		PlotSimulationPoint(Angle, x, y, z);
	}
}

void CObjectDemoExperiment::PlotSimulationPoint(float Angle, float x, float y, float z)
{
	CGraphPoints* pPoint = new CGraphPoints();
	pPoint->m_Angle = Angle;
	pPoint->m_x = x;
	pPoint->m_y = y;
	pPoint->m_z = z;

	m_PlotInfoArray.Add(pPoint);

	CString strStatus;
	strStatus.Format(_T("Plot Data Points Count =%d"), m_PlotInfoArray.GetCount());
	m_pManager->SetStatusBarMessage(strStatus);

	DisplayObjectDemoGraph();
}

void CObjectDemoExperiment::InitializeSimulationGraph(CString ExperimentName)
{
	for (int i = 0; i < m_PlotInfoArray.GetCount(); i++)
	{
		CGraphPoints* pPoint = (CGraphPoints*)m_PlotInfoArray.GetAt(i);
		delete pPoint;
	}
	m_PlotInfoArray.RemoveAll();

	CComPtr<IApplicationChart> ApplicationChart;
	HRESULT HR = ApplicationChart.CoCreateInstance(CLSID_ApplicationChart);
	if (SUCCEEDED(HR))
	{
		ApplicationChart->DeleteAllCharts();
		ApplicationChart->Initialize2dChart(3);

		ApplicationChart->Set2dGraphInfo(0, _T("Angle Vs X"), _T("Angle(Degree)"), _T("X"), TRUE);
		ApplicationChart->Set2dAxisRange(0, EAxisPos::BottomAxis, 0, 365);
		ApplicationChart->Set2dAxisRange(0, EAxisPos::LeftAxis, 0, 2);

		ApplicationChart->Set2dGraphInfo(1, _T("Angle Vs Y"), _T("Angle(Degree)"), _T("Y"), TRUE);
		ApplicationChart->Set2dAxisRange(1, EAxisPos::BottomAxis, 0, 365);
		ApplicationChart->Set2dAxisRange(1, EAxisPos::LeftAxis, 0, 2);

		ApplicationChart->Set2dGraphInfo(2, _T("Angle Vs Z"), _T("Angle(Degree)"), _T("Z"), TRUE);
		ApplicationChart->Set2dAxisRange(2, EAxisPos::BottomAxis, 0, 365);
		ApplicationChart->Set2dAxisRange(2, EAxisPos::LeftAxis, 0, 2);

		ApplicationChart->ResizeChartWindow();
	}
}

void CObjectDemoExperiment::DisplayObjectDemoGraph()
{
	int iArraySize = (int)m_PlotInfoArray.GetCount();
	if (iArraySize < 2)
	{
		return;
	}

	COleSafeArray saX, saY, saZ;
	SAFEARRAYBOUND sabX[2], sabY[2], sabZ[2];

	sabX[0].cElements = iArraySize;
	sabX[1].cElements = 2;
	sabX[0].lLbound = sabX[1].lLbound = 1;
	saX.Create(VT_R8, 2, sabX);

	sabY[0].cElements = iArraySize;
	sabY[1].cElements = 2;
	sabY[0].lLbound = sabY[1].lLbound = 1;
	saY.Create(VT_R8, 2, sabY);

	sabZ[0].cElements = iArraySize;
	sabZ[1].cElements = 2;
	sabZ[0].lLbound = sabZ[1].lLbound = 1;
	saZ.Create(VT_R8, 2, sabZ);

	long index[2] = { 0,0 };

	for (int i = 0; i < iArraySize; i++)
	{
		CGraphPoints* pInfo = (CGraphPoints*)m_PlotInfoArray.GetAt(i);
		index[0] = i + 1;
		index[1] = 1;
		double pValue = pInfo->m_Angle;
		saX.PutElement(index, &pValue);
		saY.PutElement(index, &pValue);
		saZ.PutElement(index, &pValue);

		index[1] = 2;
		pValue = pInfo->m_x;
		saX.PutElement(index, &pValue);
		pValue = pInfo->m_y;
		saY.PutElement(index, &pValue);
		pValue = pInfo->m_z;
		saZ.PutElement(index, &pValue);
	}

	if (iArraySize % 5 == 0)
	{
		CComPtr<IApplicationChart> ApplicationChart;
		HRESULT HR = ApplicationChart.CoCreateInstance(CLSID_ApplicationChart);
		if (SUCCEEDED(HR))
		{
			ApplicationChart->Set2dChartData(0, saX);
			ApplicationChart->Set2dChartData(1, saY);
			ApplicationChart->Set2dChartData(2, saZ);
		}
	}
}