// Standard header includes for MFC, OpenGL, and math libraries
#include "stdafx.h"
#include "ObjectDemoExperiment.h"
#include "GameDemoSimulation.h"
#include "AddinSimulationManager.h"
#include "PropSliderCtrl.h"
#include <math.h>
#include <GL/gl.h>
#include <GL/glu.h>
#include <cmath>
#pragma comment(lib, "opengl32.lib")
#pragma comment(lib, "glu32.lib")
using namespace ATL;

// -------------------------------------------------------------------
// CObjectPattern class - Handles object properties and serialization
// -------------------------------------------------------------------
CObjectPattern::CObjectPattern()
{
	m_strObjectType = _T("FootballPenalty");
	m_Color = RGB(0, 0, 255);
	m_strSimulationPattern = _T("Penalty Kick View");
	m_lSimulationInterval = 100;
}

// Serialize object pattern data to/from archive
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

// Handle property changes from UI
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

// -------------------------------------------------------------------
// CGraphPoints class - Stores 3D point data for graphing
// -------------------------------------------------------------------
CGraphPoints::CGraphPoints()
{
	m_Angle = 0.0f;
	m_x = 0.0f;
	m_y = 0.0f;
	m_z = 0.0f;
}

// -------------------------------------------------------------------
// CFootballPenaltyGameState class - Manages football penalty game state
// -------------------------------------------------------------------
CFootballPenaltyGameState::CFootballPenaltyGameState()
{
	Reset();
}

// Reset all football game state variables to initial values
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

// -------------------------------------------------------------------
// CElephantGameState class - Manages elephant animation state
// -------------------------------------------------------------------
CElephantGameState::CElephantGameState()
{
	Reset();
}

// Reset all elephant animation state variables
void CElephantGameState::Reset()
{
	positionX = 0.0f;
	positionY = 0.0f;
	positionZ = 0.0f;
	rotationY = 0.0f;
	animationTime = 0.0f;
	currentState = 0;
	walkSpeed = 0.015f;
	runSpeed = 0.03f;
	scale = 1.2f;
	isTrumpeting = false;
	trumpetProgress = 0.0f;
	headBob = 0.0f;
	trunkSwing = 0.0f;
	earFlap = 0.0f;
}

// -------------------------------------------------------------------
// CObjectDemoExperiment class - Main experiment controller
// -------------------------------------------------------------------
// Constructor - Initialize experiment with simulation manager
CObjectDemoExperiment::CObjectDemoExperiment(CAddinSimulationManager* pManager)
{
	m_pManager = pManager;
	ResetFootballPenalty();
	m_ElephantGameState.Reset();
	m_ElephantGameState.scale = 0.8f;
	m_bPenaltyKickView = false;
	m_bShowAimingReticle = true;
	m_bShowPowerMeter = true;
	m_bKeyboardActive = false;
}

// Destructor - Clean up allocated memory
CObjectDemoExperiment::~CObjectDemoExperiment()
{
	for (int i = 0; i < m_PlotInfoArray.GetCount(); i++)
	{
		CGraphPoints* pPoint = (CGraphPoints*)m_PlotInfoArray.GetAt(i);
		delete pPoint;
	}
	m_PlotInfoArray.RemoveAll();
}

// Main simulation loop for object animations
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
		else if (m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_PENALTY_KICK)
		{
			UpdateFootballPenalty();
			x = 0; y = 0; z = 0;
		}
		else
		{
			x = 0; y = 0; z = 0;
		}
		if (m_ObjectPattern.m_strObjectType != OBJECT_TYPE_ELEPHANT)
		{
			if (!m_pManager->m_b3DMode && m_ObjectPattern.m_strSimulationPattern != OBJECT_PATTERN_TYPE_PENALTY_KICK)
			{
				x = 0;
				y = 0;
			}
			if (m_ObjectPattern.m_strSimulationPattern != OBJECT_PATTERN_TYPE_PENALTY_KICK)
			{
				ApplicationView->RotateObject(Angle, x, y, z);
			}
		}
		else
		{
			m_ElephantGameState.animationTime += 0.016f;
			m_ElephantGameState.trunkSwing = sinf(m_ElephantGameState.animationTime * 2.0f) * 0.5f;
			m_ElephantGameState.earFlap = sinf(m_ElephantGameState.animationTime * 1.5f) * 0.3f;
			m_ElephantGameState.headBob = sinf(m_ElephantGameState.animationTime * 0.5f) * 0.005f;
			DrawElephant();
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

// Load all available experiments into the tree view
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

// Handle tree node selection events
void CObjectDemoExperiment::OnTreeNodeSelect(BSTR ExperimentGroup, BSTR ExperimentName)
{
	OnReloadExperiment(ExperimentGroup, ExperimentName);
}

// Handle tree node double-click events
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

// Reload experiment when selected from tree view
void CObjectDemoExperiment::OnReloadExperiment(BSTR ExperimentGroup, BSTR ExperimentName)
{
	if (CString(ExperimentGroup) == OBJECT_3D_TREE_ROOT_TITLE)
	{
		DrawObject(ExperimentName);
	}
}

// Show object properties in property window
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

// Serialize experiment data to/from archive
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

// Handle property change events from UI
void CObjectDemoExperiment::OnPropertyChanged(BSTR GroupName, BSTR PropertyName, BSTR PropertyValue)
{
	if (CString(GroupName) == OBJECT_PROPERTIES_TITLE)
	{
		m_ObjectPattern.OnPropertyChanged(GroupName, PropertyName, PropertyValue);
	}
	DrawScene();
}

// Redraw the entire scene
void CObjectDemoExperiment::DrawScene()
{
	OnReloadExperiment(m_pManager->m_strExperimentGroup.AllocSysString(), m_pManager->m_strExperimentName.AllocSysString());
}

// Draw selected object based on type and simulation pattern
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
		if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_FOOTBALLPENALTY)
		{
			DrawFootballPenalty();
		}
		else if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_ELEPHANT)
		{
			DrawElephant();
		}
	}
}

// Reset football penalty game to initial state
void CObjectDemoExperiment::ResetFootballPenalty()
{
	m_FootballGameState.Reset();
}

// Draw football penalty scene with all components
void CObjectDemoExperiment::DrawFootballPenalty()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR)) return;
	ApplicationView->InitializeEnvironment(TRUE);
	ApplicationView->BeginGraphicsCommands();
	ApplicationView->SetBkgColor(0.2f, 0.6f, 0.8f, 1.0f);
	HR = ApplicationView->StartNewDisplayList();
	if (HR == E_FAIL)
	{
		ApplicationView->EndGraphicsCommands();
		return;
	}
	UpdateFootballPenalty();
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

// Update football penalty game state
void CObjectDemoExperiment::UpdateFootballPenalty()
{
	m_FootballGameState.gameTime += 0.016f;
	HandleKeyboardInput();
	if (m_FootballGameState.isBallMoving)
	{
		m_FootballGameState.ballX += m_FootballGameState.ballSpeedX;
		m_FootballGameState.ballY += m_FootballGameState.ballSpeedY;
		m_FootballGameState.ballZ += m_FootballGameState.ballSpeedZ;
		m_FootballGameState.ballSpeedY -= 0.098f * 0.5f;
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
		CheckGoal();
	}
	UpdateGoalkeeper();
	if (m_FootballGameState.isKicking &&
		(m_FootballGameState.gameTime - m_FootballGameState.kickAnimationTime) > 0.5f)
	{
		m_FootballGameState.isKicking = false;
	}
}

// Handle keyboard input for football game
void CObjectDemoExperiment::HandleKeyboardInput()
{
	if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_FOOTBALLPENALTY ||
		m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_PENALTY_KICK)
	{
		if (!m_FootballGameState.isBallMoving)
		{
			if ((GetAsyncKeyState('D') & 0x8000) != 0)
			{
				m_FootballGameState.kickerAngle = -45.0f;
				m_FootballGameState.kickPower = 1.0f;
				ProcessKick();
				Sleep(2);
			}
			else if ((GetAsyncKeyState('S') & 0x8000) != 0)
			{
				m_FootballGameState.kickerAngle = 0.0f;
				m_FootballGameState.kickPower = 1.0f;
				ProcessKick();
				Sleep(2);
			}
			else if ((GetAsyncKeyState('A') & 0x8000) != 0)
			{
				m_FootballGameState.kickerAngle = 45.0f;
				m_FootballGameState.kickPower = 1.0f;
				ProcessKick();
				Sleep(2);
			}
		}
	}
}

// Process kick action with physics calculations
void CObjectDemoExperiment::ProcessKick()
{
	float angleRad = m_FootballGameState.kickerAngle * 3.14159f / 180.0f;
	float powerMultiplier = 9.0f;
	if (fabs(m_FootballGameState.kickerAngle) > 30.0f) {
		float cornerPower = powerMultiplier * 0.9f;
		float cornerHeight = 1.2f;
		float maxCornerX = 3.5f;
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

// Reset shot result flags
void CObjectDemoExperiment::ResetShotResult()
{
	m_FootballGameState.isGoalScored = false;
	m_FootballGameState.isShotSaved = false;
	m_FootballGameState.isShotMissed = false;
	m_FootballGameState.isKicking = false;
}

// Update goalkeeper position and diving logic
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
				else if (predictedBallX < -1.0f)
				{
					m_FootballGameState.goalkeeperDiveLeft = true;
				}
				else if (predictedBallX > 1.0f)
				{
					m_FootballGameState.goalkeeperDiveRight = true;
				}
				else
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
			m_FootballGameState.goalkeeperX -= 0.08f;
			if (m_FootballGameState.goalkeeperX < -maxGoalkeeperX)
				m_FootballGameState.goalkeeperX = -maxGoalkeeperX;
		}
		else if (m_FootballGameState.goalkeeperDiveRight)
		{
			m_FootballGameState.goalkeeperX += 0.08f;
			if (m_FootballGameState.goalkeeperX > maxGoalkeeperX)
				m_FootballGameState.goalkeeperX = maxGoalkeeperX;
		}
	}
	else if (!m_FootballGameState.isBallMoving)
	{
		m_FootballGameState.goalkeeperX = 0.0f;
		m_FootballGameState.goalkeeperDiveLeft = false;
		m_FootballGameState.goalkeeperDiveRight = false;
		m_FootballGameState.goalkeeperTimer = 0.5f;
	}
}

// Reset ball position and velocity
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

// Check if ball entered goal and update score
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

// Draw football field with markings
void CObjectDemoExperiment::DrawFootballField()
{
	CComPtr<IOpenGLView> OpenGLView;
	HRESULT HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR)) return;
	OpenGLView->glBegin(GL_QUADS);
	OpenGLView->glColor3f(0.3f, 0.7f, 0.3f);
	OpenGLView->glVertex3f(-8.0f, 0.0f, -4.0f);
	OpenGLView->glVertex3f(8.0f, 0.0f, -4.0f);
	OpenGLView->glVertex3f(8.0f, 0.0f, 8.0f);
	OpenGLView->glVertex3f(-8.0f, 0.0f, 8.0f);
	OpenGLView->glEnd();
	OpenGLView->glBegin(GL_LINES);
	OpenGLView->glColor3f(1.0f, 1.0f, 1.0f);
	OpenGLView->glVertex3f(-6.0f, 0.01f, -3.0f);
	OpenGLView->glVertex3f(6.0f, 0.01f, -3.0f);
	OpenGLView->glVertex3f(6.0f, 0.01f, -3.0f);
	OpenGLView->glVertex3f(6.0f, 0.01f, 6.0f);
	OpenGLView->glVertex3f(6.0f, 0.01f, 6.0f);
	OpenGLView->glVertex3f(-6.0f, 0.01f, 6.0f);
	OpenGLView->glVertex3f(-6.0f, 0.01f, 6.0f);
	OpenGLView->glVertex3f(-6.0f, 0.01f, -3.0f);
	OpenGLView->glVertex3f(-6.0f, 0.01f, 1.5f);
	OpenGLView->glVertex3f(6.0f, 0.01f, 1.5f);
	OpenGLView->glVertex3f(-0.1f, 0.01f, -2.8f);
	OpenGLView->glVertex3f(0.1f, 0.01f, -2.8f);
	OpenGLView->glVertex3f(0.0f, 0.01f, -2.9f);
	OpenGLView->glVertex3f(0.0f, 0.01f, -2.7f);
	OpenGLView->glEnd();
}

// Draw goal post structure
void CObjectDemoExperiment::DrawGoalPost()
{
	CComPtr<IOpenGLView> OpenGLView;
	HRESULT HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR)) return;
	OpenGLView->glColor3f(1.0f, 1.0f, 1.0f);
	float goalWidth = 6.0f;
	float goalHeight = 1.8f;
	OpenGLView->glPushMatrix();
	OpenGLView->glTranslatef(-goalWidth / 2, goalHeight / 2, 4.0f);
	OpenGLView->glScalef(0.1f, goalHeight, 0.1f);
	DrawCubePrimitive(1.0f);
	OpenGLView->glPopMatrix();
	OpenGLView->glPushMatrix();
	OpenGLView->glTranslatef(goalWidth / 2, goalHeight / 2, 4.0f);
	OpenGLView->glScalef(0.1f, goalHeight, 0.1f);
	DrawCubePrimitive(1.0f);
	OpenGLView->glPopMatrix();
	OpenGLView->glPushMatrix();
	OpenGLView->glTranslatef(0.0f, goalHeight, 4.0f);
	OpenGLView->glScalef(goalWidth, 0.1f, 0.1f);
	DrawCubePrimitive(1.0f);
	OpenGLView->glPopMatrix();
}

// Draw pentagon shape
void CObjectDemoExperiment::DrawPentagon(float radius)
{
	CComPtr<IOpenGLView> gl;
	HRESULT hr = gl.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(hr)) return;
	gl->glBegin(GL_POLYGON);
	for (int i = 0; i < 5; i++)
	{
		float angle = i * 72.0f * 3.14159f / 180.0f;
		float x = cosf(angle) * radius;
		float y = sinf(angle) * radius;
		gl->glVertex2f(x, y);
	}
	gl->glEnd();
}

// Draw football with spinning animation
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
	float spin = m_FootballGameState.gameTime * 10.0f;
	gl->glRotatef(spin * 300.0f, 1.0f, 1.0f, 0.5f);
	float radius = 0.13f;
	view->SetColorf(0.12f, 0.12f, 0.12f);
	view->DrawSphere(radius, 40, 40);
	gl->glPopMatrix();
}

// Draw kicker character with animation
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
	gl->glColor3f(0.95f, 0.8f, 0.65f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.90f, 0.0f);
	gl->glScalef(0.20f, 0.18f, 0.20f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glColor3f(0.0f, 0.3f, 0.9f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.65f, 0.0f);
	gl->glScalef(0.44f, 0.35f, 0.26f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glColor3f(0.0f, 0.25f, 0.8f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.28f, 0.65f, 0.0f);
	gl->glRotatef(20.0f, 0.0f, 0.0f, 1.0f);
	gl->glScalef(0.13f, 0.30f, 0.13f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glPushMatrix();
	gl->glTranslatef(0.28f, 0.65f, 0.0f);
	gl->glRotatef(-20.0f, 0.0f, 0.0f, 1.0f);
	gl->glScalef(0.13f, 0.30f, 0.13f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glColor3f(1.0f, 1.0f, 1.0f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.40f, 0.0f);
	gl->glScalef(0.48f, 0.18f, 0.30f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glColor3f(0.95f, 0.8f, 0.65f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.11f, 0.25f, 0.0f);
	gl->glScalef(0.13f, 0.28f, 0.13f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glColor3f(1.0f, 1.0f, 1.0f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.11f, 0.05f, 0.0f);
	gl->glScalef(0.15f, 0.20f, 0.15f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glPushMatrix();
	gl->glTranslatef(0.11f, 0.40f, 0.0f);
	gl->glRotatef(kickAngle, 1.0f, 0.0f, 0.0f);
	gl->glColor3f(0.95f, 0.8f, 0.65f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, -0.14f, 0.0f);
	gl->glScalef(0.13f, 0.28f, 0.13f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glTranslatef(0.0f, -0.28f, 0.0f);
	float kneeBend = kickT > 0.4f ? 40.0f * (kickT - 0.4f) / 0.6f : 0.0f;
	gl->glRotatef(-kneeBend, 1.0f, 0.0f, 0.0f);
	gl->glColor3f(0.95f, 0.8f, 0.65f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, -0.12f, 0.0f);
	gl->glScalef(0.11f, 0.24f, 0.11f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glPopMatrix();
	gl->glPopMatrix();
}

// Draw goalkeeper with diving animation
void CObjectDemoExperiment::DrawGoalkeeper()
{
	CComPtr<IOpenGLView> gl;
	HRESULT hr = gl.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(hr)) return;
	gl->glPushMatrix();
	gl->glTranslatef(m_FootballGameState.goalkeeperX, 0.0f, 4.0f);
	gl->glRotatef(m_FootballGameState.kickerAngle, 0.0f, 1.0f, 0.0f);
	float diveAngle = 0.0f;
	float diveOffsetY = 0.0f;
	float diveOffsetX = 0.0f;
	if (m_FootballGameState.goalkeeperDiveLeft)
	{
		diveAngle = -48.0f;
		diveOffsetY = 0.6f;
		diveOffsetX = -0.45f;
	}
	else if (m_FootballGameState.goalkeeperDiveRight)
	{
		diveAngle = 48.0f;
		diveOffsetY = 0.25f;
		diveOffsetX = 0.45f;
	}
	gl->glTranslatef(diveOffsetX, diveOffsetY, 0.0f);
	gl->glRotatef(diveAngle, 0.0f, 0.0f, 1.0f);
	gl->glColor3f(0.95f, 0.8f, 0.65f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 1.12f, 0.0f);
	gl->glScalef(0.22f, 0.24f, 0.22f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glColor3f(0.0f, 0.9f, 0.3f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.80f, 0.0f);
	gl->glScalef(0.48f, 0.48f, 0.28f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glColor3f(0.0f, 0.8f, 0.25f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.35f, 0.90f, 0.0f);
	gl->glRotatef(-70.0f + diveAngle * 0.8f, 0.0f, 0.0f, 1.0f);
	gl->glScalef(0.14f, 0.45f, 0.14f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glPushMatrix();
	gl->glTranslatef(0.35f, 0.90f, 0.0f);
	gl->glRotatef(70.0f + diveAngle * 0.8f, 0.0f, 0.0f, 1.0f);
	gl->glScalef(0.14f, 0.45f, 0.14f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glColor3f(0.1f, 0.1f, 0.1f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.58f, 0.90f, 0.0f);
	gl->glScalef(0.18f, 0.14f, 0.18f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glPushMatrix();
	gl->glTranslatef(0.58f, 0.90f, 0.0f);
	gl->glScalef(0.18f, 0.14f, 0.18f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glColor3f(0.1f, 0.1f, 0.1f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.55f, 0.0f);
	gl->glScalef(0.50f, 0.24f, 0.32f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glColor3f(0.95f, 0.8f, 0.65f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.11f, 0.35f, 0.0f);
	gl->glRotatef(20.0f + diveAngle * 0.6f, 1.0f, 0.0f, 0.0f);
	gl->glScalef(0.14f, 0.38f, 0.14f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glPushMatrix();
	gl->glTranslatef(0.11f, 0.35f, 0.0f);
	gl->glRotatef(-30.0f + diveAngle * 0.7f, 1.0f, 0.0f, 0.0f);
	gl->glScalef(0.14f, 0.38f, 0.14f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glColor3f(0.0f, 0.7f, 0.2f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.11f, 0.08f, 0.0f);
	gl->glScalef(0.16f, 0.25f, 0.16f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
	gl->glPushMatrix();
	gl->glTranslatef(0.11f, 0.08f, 0.0f);
	gl->glScalef(0.16f, 0.25f, 0.16f);
	DrawCubePrimitive(1.0f);
	gl->glPopMatrix();
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
	gl->glPopMatrix();
}

// Draw environment (sky, grass, stadium elements)
void CObjectDemoExperiment::DrawEnvironment()
{
	CComPtr<IOpenGLView> gl;
	HRESULT hr = gl.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(hr)) return;
	gl->glBegin(GL_QUADS);
	gl->glColor3f(0.45f, 0.75f, 0.98f);
	gl->glVertex3f(-50.0f, 25.0f, -50.0f);
	gl->glVertex3f(50.0f, 25.0f, -50.0f);
	gl->glVertex3f(50.0f, 25.0f, 60.0f);
	gl->glVertex3f(-50.0f, 25.0f, 60.0f);
	gl->glColor3f(0.70f, 0.88f, 0.99f);
	gl->glVertex3f(-50.0f, 0.0f, -50.0f);
	gl->glVertex3f(50.0f, 0.0f, -50.0f);
	gl->glColor3f(0.65f, 0.85f, 0.99f);
	gl->glVertex3f(50.0f, 0.0f, 60.0f);
	gl->glVertex3f(-50.0f, 0.0f, 60.0f);
	gl->glEnd();
	gl->glBegin(GL_QUADS);
	gl->glColor3f(0.18f, 0.55f, 0.10f);
	gl->glVertex3f(-12.0f, 0.01f, -8.0f);
	gl->glVertex3f(12.0f, 0.01f, -8.0f);
	gl->glVertex3f(12.0f, 0.01f, 12.0f);
	gl->glVertex3f(-12.0f, 0.01f, 12.0f);
	gl->glEnd();
	gl->glColor3f(1.0f, 1.0f, 1.0f);
	gl->glLineWidth(3.0f);
	gl->glBegin(GL_LINES);
	gl->glVertex3f(-12.0f, 0.02f, 2.0f);
	gl->glVertex3f(12.0f, 0.02f, 2.0f);
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
	gl->glVertex3f(-4.0f, 0.02f, 9.0f);
	gl->glVertex3f(4.0f, 0.02f, 9.0f);
	gl->glVertex3f(-4.0f, 0.02f, 9.0f);
	gl->glVertex3f(-4.0f, 0.02f, 12.0f);
	gl->glVertex3f(4.0f, 0.02f, 9.0f);
	gl->glVertex3f(4.0f, 0.02f, 12.0f);
	gl->glVertex3f(-5.0f, 0.02f, 12.0f);
	gl->glVertex3f(5.0f, 0.02f, 12.0f);
	gl->glEnd();
	gl->glBegin(GL_QUADS);
	gl->glColor3f(0.15f, 0.15f, 0.20f);
	gl->glVertex3f(-20.0f, 0.0f, -15.0f);
	gl->glVertex3f(-12.0f, 0.0f, -10.0f);
	gl->glVertex3f(-12.0f, 8.0f, -10.0f);
	gl->glVertex3f(-20.0f, 6.0f, -15.0f);
	gl->glColor3f(0.9f, 0.3f, 0.1f);
	gl->glVertex3f(-19.5f, 1.0f, -14.5f);
	gl->glVertex3f(-12.5f, 1.0f, -10.5f);
	gl->glVertex3f(-12.5f, 7.0f, -10.5f);
	gl->glVertex3f(-19.5f, 5.0f, -14.5f);
	gl->glColor3f(0.15f, 0.15f, 0.20f);
	gl->glVertex3f(20.0f, 0.0f, -15.0f);
	gl->glVertex3f(12.0f, 0.0f, -10.0f);
	gl->glVertex3f(12.0f, 8.0f, -10.0f);
	gl->glVertex3f(20.0f, 6.0f, -15.0f);
	gl->glColor3f(0.1f, 0.3f, 0.9f);
	gl->glVertex3f(19.5f, 1.0f, -14.5f);
	gl->glVertex3f(12.5f, 1.0f, -10.5f);
	gl->glVertex3f(12.5f, 7.0f, -10.5f);
	gl->glVertex3f(19.5f, 5.0f, -14.5f);
	gl->glEnd();
	gl->glBegin(GL_QUADS);
	gl->glColor3f(0.4f, 0.7f, 0.2f);
	gl->glVertex3f(-40.0f, 0.0f, 50.0f);
	gl->glVertex3f(40.0f, 0.0f, 50.0f);
	gl->glVertex3f(40.0f, 20.0f, 50.0f);
	gl->glVertex3f(-40.0f, 20.0f, 50.0f);
	gl->glEnd();
}

// Draw cube primitive
void CObjectDemoExperiment::DrawCubePrimitive(float size)
{
	CComPtr<IOpenGLView> OpenGLView;
	HRESULT HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR)) return;
	float s = size / 2.0f;
	OpenGLView->glBegin(GL_QUADS);
	OpenGLView->glVertex3f(-s, -s, s);
	OpenGLView->glVertex3f(s, -s, s);
	OpenGLView->glVertex3f(s, s, s);
	OpenGLView->glVertex3f(-s, s, s);
	OpenGLView->glVertex3f(-s, -s, -s);
	OpenGLView->glVertex3f(-s, s, -s);
	OpenGLView->glVertex3f(s, s, -s);
	OpenGLView->glVertex3f(s, -s, -s);
	OpenGLView->glVertex3f(-s, s, -s);
	OpenGLView->glVertex3f(-s, s, s);
	OpenGLView->glVertex3f(s, s, s);
	OpenGLView->glVertex3f(s, s, -s);
	OpenGLView->glVertex3f(-s, -s, -s);
	OpenGLView->glVertex3f(s, -s, -s);
	OpenGLView->glVertex3f(s, -s, s);
	OpenGLView->glVertex3f(-s, -s, s);
	OpenGLView->glVertex3f(s, -s, -s);
	OpenGLView->glVertex3f(s, s, -s);
	OpenGLView->glVertex3f(s, s, s);
	OpenGLView->glVertex3f(s, -s, s);
	OpenGLView->glVertex3f(-s, -s, -s);
	OpenGLView->glVertex3f(-s, -s, s);
	OpenGLView->glVertex3f(-s, s, s);
	OpenGLView->glVertex3f(-s, s, -s);
	OpenGLView->glEnd();
}

// Draw penalty kick view with HUD
void CObjectDemoExperiment::DrawPenaltyKickView()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR)) return;
	ApplicationView->InitializeEnvironment(TRUE);
	ApplicationView->BeginGraphicsCommands();
	ApplicationView->SetBkgColor(0.2f, 0.6f, 0.8f, 1.0f);
	HR = ApplicationView->StartNewDisplayList();
	if (HR == E_FAIL)
	{
		ApplicationView->EndGraphicsCommands();
		return;
	}
	UpdateFootballPenalty();
	DrawStadium();
	DrawEnvironment();
	DrawFootballField();
	DrawGoalPost();
	DrawNet();
	DrawFootball();
	DrawKicker();
	DrawGoalkeeper();
	DrawPenaltyKickHUD();
	DrawResultMessage();
	ApplicationView->EndNewDisplayList();
	ApplicationView->EndGraphicsCommands();
	ApplicationView->Refresh();
}

// Draw penalty kick HUD elements
void CObjectDemoExperiment::DrawPenaltyKickHUD()
{
	DrawAimingReticle();
}

// Draw aiming reticle for penalty kick
void CObjectDemoExperiment::DrawAimingReticle()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR)) return;
	if (!m_bShowAimingReticle) return;
	ApplicationView->SetColorf(1.0f, 1.0f, 1.0f);
	ApplicationView->SetLineWidth(2.0f);
	ApplicationView->BeginDraw(GL_LINES);
	ApplicationView->Set2DVertexf(-0.05f, 0.0f);
	ApplicationView->Set2DVertexf(0.05f, 0.0f);
	ApplicationView->Set2DVertexf(0.0f, -0.05f);
	ApplicationView->Set2DVertexf(0.0f, 0.05f);
	ApplicationView->EndDraw();
	float angleIndicatorX = sinf(m_FootballGameState.kickerAngle * 3.14159f / 180.0f) * 0.1f;
	ApplicationView->SetColorf(1.0f, 0.0f, 0.0f);
	ApplicationView->BeginDraw(GL_LINES);
	ApplicationView->Set2DVertexf(0.0f, -0.05f);
	ApplicationView->Set2DVertexf(angleIndicatorX, -0.08f);
	ApplicationView->EndDraw();
}

// Draw result message (GOAL!, SAVED!, MISSED!)
void CObjectDemoExperiment::DrawResultMessage()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR)) return;
	if (m_FootballGameState.isGoalScored || m_FootballGameState.isShotSaved || m_FootballGameState.isShotMissed)
	{
		CString strMessage;
		if (m_FootballGameState.isGoalScored)
		{
			strMessage = _T("GOAL!");
			ApplicationView->SetColorf(0.0f, 1.0f, 0.0f);
		}
		else if (m_FootballGameState.isShotSaved)
		{
			strMessage = _T("SAVED!");
			ApplicationView->SetColorf(1.0f, 1.0f, 0.0f);
		}
		else if (m_FootballGameState.isShotMissed)
		{
			strMessage = _T("MISSED!");
			ApplicationView->SetColorf(1.0f, 0.0f, 0.0f);
		}
		ApplicationView->BeginDraw(GL_QUADS);
		ApplicationView->Set2DVertexf(-0.2f, 0.1f);
		ApplicationView->Set2DVertexf(0.2f, 0.1f);
		ApplicationView->Set2DVertexf(0.2f, 0.2f);
		ApplicationView->Set2DVertexf(-0.2f, 0.2f);
		ApplicationView->EndDraw();
		ApplicationView->SetColorf(1.0f, 1.0f, 1.0f);
		ApplicationView->BeginDraw(GL_LINE_LOOP);
		ApplicationView->Set2DVertexf(-0.2f, 0.1f);
		ApplicationView->Set2DVertexf(0.2f, 0.1f);
		ApplicationView->Set2DVertexf(0.2f, 0.2f);
		ApplicationView->Set2DVertexf(-0.2f, 0.2f);
		ApplicationView->EndDraw();
	}
}

// Draw stadium structure
void CObjectDemoExperiment::DrawStadium()
{
	CComPtr<IOpenGLView> OpenGLView;
	HRESULT HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR)) return;
	OpenGLView->glColor3f(0.4f, 0.4f, 0.4f);
	OpenGLView->glBegin(GL_QUADS);
	OpenGLView->glVertex3f(-8.0f, 0.0f, -4.0f);
	OpenGLView->glVertex3f(-8.0f, 3.0f, -4.0f);
	OpenGLView->glVertex3f(-8.0f, 3.0f, 8.0f);
	OpenGLView->glVertex3f(-8.0f, 0.0f, 8.0f);
	OpenGLView->glVertex3f(8.0f, 0.0f, -4.0f);
	OpenGLView->glVertex3f(8.0f, 3.0f, -4.0f);
	OpenGLView->glVertex3f(8.0f, 3.0f, 8.0f);
	OpenGLView->glVertex3f(8.0f, 0.0f, 8.0f);
	OpenGLView->glVertex3f(-8.0f, 0.0f, 8.0f);
	OpenGLView->glVertex3f(-8.0f, 3.0f, 8.0f);
	OpenGLView->glVertex3f(8.0f, 3.0f, 8.0f);
	OpenGLView->glVertex3f(8.0f, 0.0f, 8.0f);
	OpenGLView->glEnd();
}

// Draw goal net
void CObjectDemoExperiment::DrawNet()
{
	CComPtr<IOpenGLView> OpenGLView;
	HRESULT HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR)) return;
	float goalWidth = 6.0f;
	float goalHeight = 1.8f;
	OpenGLView->glColor4f(1.0f, 1.0f, 1.0f, 0.3f);
	for (int i = 0; i <= 8; i++)
	{
		float x = -goalWidth / 2 + (goalWidth / 8) * i;
		OpenGLView->glBegin(GL_LINES);
		OpenGLView->glVertex3f(x, 0.0f, 4.0f);
		OpenGLView->glVertex3f(x, goalHeight, 4.0f);
		OpenGLView->glEnd();
	}
	for (int i = 0; i <= 4; i++)
	{
		float y = (goalHeight / 4) * i;
		OpenGLView->glBegin(GL_LINES);
		OpenGLView->glVertex3f(-goalWidth / 2, y, 4.0f);
		OpenGLView->glVertex3f(goalWidth / 2, y, 4.0f);
		OpenGLView->glEnd();
	}
}

// Start simulation for selected experiment
void CObjectDemoExperiment::StartSimulation(BSTR ExperimentGroup, BSTR ExperimentName)
{
	if (CString(ExperimentGroup) == OBJECT_3D_TREE_ROOT_TITLE)
	{
		if (CString(ExperimentName) == OBJECT_3D_TREE_LEAF_PATTERN_TITLE)
		{
			if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_ELEPHANT)
			{
				m_ElephantGameState.positionX = 0.0f;
				m_ElephantGameState.positionY = 0.0f;
				m_ElephantGameState.positionZ = 0.0f;
				m_ElephantGameState.currentState = 0;
				m_ElephantGameState.animationTime = 0.0f;
			}
			StartObjectSimulation();
		}
	}
}

// Initialize simulation graph
void CObjectDemoExperiment::InitializeSimulationGraph(CString ExperimentName)
{
	if (ExperimentName == OBJECT_3D_TREE_LEAF_PATTERN_TITLE)
	{
		for (int i = 0; i < m_PlotInfoArray.GetCount(); i++)
		{
			CGraphPoints* pPoint = (CGraphPoints*)m_PlotInfoArray.GetAt(i);
			delete pPoint;
		}
		m_PlotInfoArray.RemoveAll();
		DisplayObjectDemoGraph();
	}
}

// Handle next simulation point
void CObjectDemoExperiment::OnNextSimulationPoint(float Angle, float x, float y, float z)
{
	PlotSimulationPoint(Angle, x, y, z);
}

// Plot simulation point to graph
void CObjectDemoExperiment::PlotSimulationPoint(float Angle, float x, float y, float z)
{
	CGraphPoints* pPoint = new CGraphPoints();
	pPoint->m_Angle = Angle;
	pPoint->m_x = x;
	pPoint->m_y = y;
	pPoint->m_z = z;
	m_PlotInfoArray.Add(pPoint);
	if (m_PlotInfoArray.GetCount() > 1000)
	{
		CGraphPoints* pOldPoint = (CGraphPoints*)m_PlotInfoArray.GetAt(0);
		delete pOldPoint;
		m_PlotInfoArray.RemoveAt(0);
	}
}

// Display object demo graph
void CObjectDemoExperiment::DisplayObjectDemoGraph()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (SUCCEEDED(HR))
	{
		ApplicationView->SetBkgColor(1.0f, 1.0f, 1.0f, 1.0f);
	}
}

// -------------------------------------------------------------------
// Elephant rendering functions
// -------------------------------------------------------------------
// Draw complete elephant with all components
void CObjectDemoExperiment::DrawElephant()
{
	CComPtr<IApplicationView> view;
	if (FAILED(view.CoCreateInstance(CLSID_ApplicationView))) return;
	view->InitializeEnvironment(TRUE);
	view->BeginGraphicsCommands();
	view->SetBkgColor(0.7f, 0.9f, 0.4f, 1.0f);

	if (view->StartNewDisplayList() == E_FAIL)
	{
		view->EndGraphicsCommands();
		return;
	}

	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView)))
	{
		view->EndGraphicsCommands();
		return;
	}

	UpdateElephantAnimation();
	DrawSavannahEnvironmentWithPond();

	gl->glPushMatrix();

	float baseLift = 0.02f;

	gl->glTranslatef(m_ElephantGameState.positionX,
		baseLift,
		m_ElephantGameState.positionZ);
	gl->glRotatef(m_ElephantGameState.rotationY, 0.0f, 1.0f, 0.0f);

	float overallScale = 0.8f;
	gl->glScalef(overallScale, overallScale, overallScale);

	DrawElephantLegs();
	DrawElephantBody();
	DrawElephantTail();
	DrawElephantHead();
	DrawElephantEars();
	DrawElephantTusks();
	DrawElephantTrunk();

	gl->glPopMatrix();

	view->EndNewDisplayList();
	view->EndGraphicsCommands();
	view->Refresh();
}

// Draw elephant body with multiple ellipsoid segments
void CObjectDemoExperiment::DrawElephantBody()
{
	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView))) return;

	float bodyR = 0.58f;
	float bodyG = 0.58f;
	float bodyB = 0.60f;

	float bob = (m_ElephantGameState.currentState != 0) ?
		fabsf(sinf(m_ElephantGameState.animationTime * 4.0f)) * 0.02f : 0.0f;

	gl->glPushMatrix();

	float bodyBaseY = 0.85f;
	gl->glTranslatef(0.0f, bodyBaseY + bob, 0.0f);

	gl->glColor3f(bodyR, bodyG, bodyB);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.25f, 0.0f);
	gl->glScalef(0.9f, 0.5f, 1.4f);
	DrawEllipsoid(1.0f, 40, 30);
	gl->glPopMatrix();

	gl->glColor3f(bodyR * 1.05f, bodyG * 1.05f, bodyB * 1.05f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.5f, -0.4f);
	gl->glScalef(0.7f, 0.45f, 0.7f);
	DrawEllipsoid(1.0f, 35, 25);
	gl->glPopMatrix();

	gl->glColor3f(bodyR * 0.95f, bodyG * 0.95f, bodyB * 0.95f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.1f, 0.15f);
	gl->glScalef(0.8f, 0.3f, 1.1f);
	DrawEllipsoid(1.0f, 40, 30);
	gl->glPopMatrix();

	gl->glColor3f(bodyR * 1.02f, bodyG * 1.02f, bodyB * 1.02f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.4f, -0.75f);
	gl->glScalef(0.65f, 0.4f, 0.5f);
	DrawEllipsoid(1.0f, 35, 25);
	gl->glPopMatrix();

	gl->glColor3f(bodyR * 0.98f, bodyG * 0.98f, bodyB * 0.98f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.35f, 0.9f);
	gl->glScalef(0.45f, 0.35f, 0.4f);
	DrawEllipsoid(1.0f, 30, 20);
	gl->glPopMatrix();

	gl->glColor3f(bodyR * 0.97f, bodyG * 0.97f, bodyB * 0.97f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.2f, -1.0f);
	gl->glScalef(0.6f, 0.45f, 0.55f);
	DrawEllipsoid(1.0f, 30, 20);
	gl->glPopMatrix();

	gl->glPopMatrix();
}

// Draw elephant head with eyes, mouth, and facial features
void CObjectDemoExperiment::DrawElephantHead()
{
	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView))) return;
	float headR = 0.56f;
	float headG = 0.56f;
	float headB = 0.58f;
	gl->glPushMatrix();
	float bodyBaseY = 0.8f;
	float neckBaseY = bodyBaseY + 0.35f;
	float headY = neckBaseY + 0.1f + m_ElephantGameState.headBob;
	float headZ = 1.15f;
	gl->glTranslatef(0.0f, headY, headZ);
	gl->glColor3f(headR, headG, headB);
	gl->glPushMatrix();
	gl->glScalef(0.55f, 0.45f, 0.5f);
	DrawEllipsoid(1.0f, 35, 25);
	gl->glPopMatrix();
	gl->glColor3f(headR * 1.02f, headG * 1.02f, headB * 1.02f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 0.15f, 0.1f);
	gl->glScalef(0.45f, 0.25f, 0.35f);
	DrawEllipsoid(1.0f, 30, 20);
	gl->glPopMatrix();
	gl->glColor3f(0.55f, 0.35f, 0.15f);
	gl->glPushMatrix();
	gl->glTranslatef(0.46f, 0.20f, 0.16f);
	gl->glScalef(0.035f, 0.035f, 0.035f);
	DrawEllipsoid(1.0f, 12, 8);
	gl->glPopMatrix();
	gl->glColor3f(0.05f, 0.05f, 0.05f);
	gl->glPushMatrix();
	gl->glTranslatef(0.46f, 0.20f, 0.162f);
	gl->glScalef(0.018f, 0.018f, 0.018f);
	DrawEllipsoid(1.0f, 12, 8);
	gl->glPopMatrix();
	gl->glColor3f(0.55f, 0.35f, 0.15f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.46f, 0.20f, 0.16f);
	gl->glScalef(0.035f, 0.035f, 0.035f);
	DrawEllipsoid(1.0f, 12, 8);
	gl->glPopMatrix();
	gl->glColor3f(0.05f, 0.05f, 0.05f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.46f, 0.20f, 0.162f);
	gl->glScalef(0.018f, 0.018f, 0.018f);
	DrawEllipsoid(1.0f, 12, 8);
	gl->glPopMatrix();
	gl->glColor3f(0.05f, 0.05f, 0.05f);
	gl->glPushMatrix();
	gl->glTranslatef(0.48f, 0.22f, 0.155f);
	gl->glScalef(0.025f, 0.02f, 0.03f);
	DrawEllipsoid(1.0f, 8, 6);
	gl->glPopMatrix();
	gl->glPushMatrix();
	gl->glTranslatef(-0.48f, 0.22f, 0.155f);
	gl->glScalef(0.025f, 0.02f, 0.03f);
	DrawEllipsoid(1.0f, 8, 6);
	gl->glPopMatrix();
	gl->glColor3f(headR * 0.98f, headG * 0.98f, headB * 0.98f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.3f, 0.0f, 0.05f);
	gl->glScalef(0.2f, 0.175f, 0.15f);
	DrawEllipsoid(1.0f, 25, 15);
	gl->glPopMatrix();
	gl->glPushMatrix();
	gl->glTranslatef(0.3f, 0.0f, 0.05f);
	gl->glScalef(0.2f, 0.175f, 0.15f);
	DrawEllipsoid(1.0f, 25, 15);
	gl->glPopMatrix();
	gl->glColor3f(headR * 0.95f, headG * 0.95f, headB * 0.95f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, -0.15f, 0.15f);
	gl->glScalef(0.2f, 0.1f, 0.15f);
	DrawEllipsoid(1.0f, 20, 15);
	gl->glPopMatrix();
	gl->glColor3f(headR * 0.9f, headG * 0.9f, headB * 0.9f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, -0.2f, 0.2f);
	gl->glScalef(0.1f, 0.04f, 0.075f);
	DrawEllipsoid(1.0f, 20, 12);
	gl->glPopMatrix();
	gl->glPopMatrix();
}

// Draw elephant ears with flapping animation
void CObjectDemoExperiment::DrawElephantEars()
{
	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView))) return;

	gl->glEnable(GL_BLEND);
	gl->glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

	float earR = 0.60f;
	float earG = 0.60f;
	float earB = 0.62f;
	float alpha = 0.85f;

	float flap = m_ElephantGameState.earFlap;

	float bodyBaseY = 0.8f;
	float neckBaseY = bodyBaseY + 0.35f;
	float headY = neckBaseY + 0.1f;

	gl->glPushMatrix();
	gl->glTranslatef(0.0f, headY, 1.15f);

	gl->glPushMatrix();
	gl->glTranslatef(-0.45f, 0.0f, -0.25f);
	gl->glRotatef(-25.0f + flap * 15.0f, 0.0f, 0.0f, 1.0f);
	gl->glRotatef(-10.0f, 1.0f, 0.0f, 0.0f);

	gl->glColor4f(earR, earG, earB, alpha);
	gl->glPushMatrix();
	gl->glScalef(0.9f, 0.5f, 0.025f);
	DrawEllipsoid(1.0f, 35, 20);
	gl->glPopMatrix();

	gl->glColor4f(earR * 0.92f, earG * 0.92f, earB * 0.92f, alpha * 0.9f);
	for (int wrinkle = 0; wrinkle < 3; wrinkle++) {
		float offset = (wrinkle - 1.0f) * 0.2f;
		gl->glPushMatrix();
		gl->glTranslatef(offset, 0.0f, 0.0f);
		gl->glScalef(0.7f - wrinkle * 0.1f, 0.4f, 0.02f);
		DrawEllipsoid(1.0f, 20, 15);
		gl->glPopMatrix();
	}

	gl->glPopMatrix();

	gl->glPushMatrix();
	gl->glTranslatef(0.45f, 0.0f, -0.25f);
	gl->glRotatef(25.0f - flap * 15.0f, 0.0f, 0.0f, 1.0f);
	gl->glRotatef(-10.0f, 1.0f, 0.0f, 0.0f);

	gl->glColor4f(earR, earG, earB, alpha);
	gl->glPushMatrix();
	gl->glScalef(0.9f, 0.5f, 0.025f);
	DrawEllipsoid(1.0f, 35, 20);
	gl->glPopMatrix();

	gl->glColor4f(earR * 0.92f, earG * 0.92f, earB * 0.92f, alpha * 0.9f);
	for (int wrinkle = 0; wrinkle < 3; wrinkle++) {
		float offset = (wrinkle - 1.0f) * 0.2f;
		gl->glPushMatrix();
		gl->glTranslatef(offset, 0.0f, 0.0f);
		gl->glScalef(0.7f - wrinkle * 0.1f, 0.4f, 0.02f);
		DrawEllipsoid(1.0f, 20, 15);
		gl->glPopMatrix();
	}

	gl->glPopMatrix();
	gl->glPopMatrix();

	gl->glDisable(GL_BLEND);
}

// Draw elephant trunk with swinging animation
void CObjectDemoExperiment::DrawElephantTrunk()
{
	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView))) return;
	float trunkR = 0.57f;
	float trunkG = 0.57f;
	float trunkB = 0.59f;
	float swing = m_ElephantGameState.trunkSwing * 0.3f;
	float trunkLift = m_ElephantGameState.isTrumpeting ? 0.4f : 0.0f;
	float trunkScale = 0.6f;
	float bodyBaseY = 0.8f;
	float neckBaseY = bodyBaseY + 0.35f;
	float headY = neckBaseY + 0.1f;
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, headY, 1.15f);
	gl->glTranslatef(0.0f, -0.2f, 0.5f);
	gl->glColor3f(trunkR, trunkG, trunkB);
	gl->glPushMatrix();
	gl->glScalef(0.2f, 0.25f * trunkScale, 0.1f);
	DrawEllipsoid(1.0f, 30, 20);
	gl->glPopMatrix();
	gl->glTranslatef(swing * 0.1f, trunkLift * 0.15f * trunkScale - 0.2f * trunkScale, 0.0f);
	gl->glRotatef(swing * 10.0f, 0.0f, 1.0f, 0.0f);
	int numSegments = 6;
	float segmentLengths[] = { 0.3f, 0.275f, 0.25f, 0.225f, 0.2f, 0.175f };
	float segmentRadii[] = { 0.175f, 0.16f, 0.145f, 0.13f, 0.115f, 0.1f };
	for (int i = 0; i < numSegments; i++)
	{
		float segLen = segmentLengths[i] * trunkScale;
		gl->glPushMatrix();
		float curveAngle = 0.0f;
		gl->glRotatef(curveAngle, 1.0f, 0.0f, 0.0f);
		gl->glScalef(segmentRadii[i], segLen, segmentRadii[i]);
		DrawEllipsoid(1.0f, 25, 18);
		gl->glPopMatrix();
		gl->glTranslatef(0.0f, -segLen, 0.0f);
	}
	gl->glColor3f(trunkR * 0.9f, trunkG * 0.9f, trunkB * 0.9f);
	gl->glPushMatrix();
	gl->glScalef(0.06f, 0.1f * trunkScale, 0.06f);
	DrawEllipsoid(1.0f, 20, 15);
	gl->glPopMatrix();
	gl->glColor3f(0.15f, 0.15f, 0.15f);
	gl->glPushMatrix();
	gl->glTranslatef(-0.025f, -0.075f * trunkScale, 0.0f);
	gl->glScalef(0.02f, 0.02f, 0.02f);
	DrawSphere(1.0f, 8, 6);
	gl->glPopMatrix();
	gl->glPushMatrix();
	gl->glTranslatef(0.025f, -0.075f * trunkScale, 0.0f);
	gl->glScalef(0.02f, 0.02f, 0.02f);
	DrawSphere(1.0f, 8, 6);
	gl->glPopMatrix();
	gl->glPopMatrix();
}

// Draw elephant legs with walking animation
void CObjectDemoExperiment::DrawElephantLegs()
{
	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView))) return;
	float legR = 0.55f;
	float legG = 0.55f;
	float legB = 0.57f;
	float walkCycle = m_ElephantGameState.animationTime * 4.0f;
	float frontLift = (m_ElephantGameState.currentState != 0) ?
		sinf(walkCycle) * 0.1f : 0.0f;
	float backLift = sinf(walkCycle + 3.14159f) * 0.1f;
	struct LegInfo {
		float x;
		float z;
		float lift;
		bool isFront;
	};
	LegInfo legs[4] = {
		{ -0.6f, 0.5f, frontLift, true },
		{ 0.6f, 0.5f, frontLift, true },
		{ -0.45f, -0.75f, backLift, false },
		{ 0.45f, -0.75f, backLift, false }
	};
	float legLength = 0.85f;
	float frontLegRadius = 0.18f;
	float backLegRadius = 0.20f;
	float baseLift = 0.02f;
	for (int i = 0; i < 4; i++)
	{
		gl->glPushMatrix();
		gl->glTranslatef(legs[i].x, legs[i].lift + baseLift, legs[i].z);
		float currentRadius = legs[i].isFront ? frontLegRadius : backLegRadius;
		gl->glColor3f(legR, legG, legB);
		DrawCylinder(currentRadius, legLength);
		gl->glColor3f(0.45f, 0.45f, 0.48f);
		gl->glPushMatrix();
		gl->glTranslatef(0.0f, -0.005f, 0.0f);
		float footScale = legs[i].isFront ? currentRadius * 1.45f : currentRadius * 1.5f;
		gl->glScalef(footScale, 0.025f, footScale);
		DrawEllipsoid(1.0f, 40, 40);
		gl->glPopMatrix();
		gl->glColor3f(0.65f, 0.58f, 0.50f);
		float toeSpacing = legs[i].isFront ? 0.12f : 0.10f;
		float toePositions[5][2] = {
			{ -toeSpacing * 2.0f, -0.02f },
			{ -toeSpacing, -0.06f },
			{ 0.0f, -0.08f },
			{ toeSpacing, -0.06f },
			{ toeSpacing * 2.0f, -0.02f }
		};
		for (int toe = 0; toe < 5; toe++)
		{
			gl->glPushMatrix();
			gl->glTranslatef(toePositions[toe][0], 0.02f, toePositions[toe][1]);
			gl->glRotatef(15.0f, 1.0f, 0.0f, 0.0f);
			float nailScaleX = (toe == 2) ? 0.055f : 0.040f;
			float nailScaleY = 0.015f;
			float nailScaleZ = (toe == 2) ? 0.045f : 0.035f;
			gl->glScalef(nailScaleX, nailScaleY, nailScaleZ);
			DrawEllipsoid(1.0f, 16, 12);
			gl->glPopMatrix();
		}
		gl->glPopMatrix();
	}
}

// Draw elephant tail with hair and swaying animation
void CObjectDemoExperiment::DrawElephantTail()
{
	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView))) return;
	float tailR = 0.50f, tailG = 0.50f, tailB = 0.52f;
	float hairR = 0.18f, hairG = 0.18f, hairB = 0.20f;
	float sway = sinf(m_ElephantGameState.animationTime * 1.4f) * 8.0f;
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 1.15f, -1.3f);
	gl->glRotatef(sway, 0.0f, 1.0f, 0.0f);
	float tailHang = 40.0f + sinf(m_ElephantGameState.animationTime * 0.7f) * 4.0f;
	gl->glRotatef(-tailHang, 1.0f, 0.0f, 0.0f);
	gl->glColor3f(tailR, tailG, tailB);
	gl->glPushMatrix();
	gl->glRotatef(-50.0f, 1.0f, 0.0f, 0.0f);
	gl->glBegin(GL_TRIANGLE_FAN);
	gl->glVertex3f(0.0f, 0.0f, 0.0f);
	float baseRadius = 0.07f;
	int discSegments = 16;
	for (int j = 0; j <= discSegments; ++j)
	{
		float angle = (float)j / (float)discSegments * 2.0f * 3.14159f;
		float x = baseRadius * cosf(angle);
		float y = baseRadius * sinf(angle);
		gl->glVertex3f(x, y, 0.0f);
	}
	gl->glEnd();
	gl->glPopMatrix();
	float tipRadius = 0.018f;
	float tailLength = 0.85f;
	int segments = 14;
	int circleSegments = 10;
	gl->glBegin(GL_QUAD_STRIP);
	for (int i = 0; i <= segments; ++i)
	{
		float t = (float)i / (float)segments;
		float radius = baseRadius * (1.0f - t) + tipRadius * t;
		float z = -0.02f - tailLength * t;
		float curve = t * t * 0.6f;
		float x = sinf(m_ElephantGameState.animationTime * 0.3f + t * 2.5f) * 0.015f;
		float y = -curve;
		float t2 = (i < segments) ? ((float)(i + 1) / (float)segments) : t;
		float z2 = -0.02f - tailLength * t2;
		float curve2 = t2 * t2 * 0.6f;
		float x2 = sinf(m_ElephantGameState.animationTime * 0.3f + t2 * 2.5f) * 0.015f;
		float y2 = -curve2;
		float dx = x2 - x;
		float dy = y2 - y;
		float dz = z2 - z;
		float upX = 0.0f, upY = 1.0f, upZ = 0.0f;
		float rightX = dy * upZ - dz * upY;
		float rightY = dz * upX - dx * upZ;
		float rightZ = dx * upY - dy * upX;
		float rightLen = sqrtf(rightX*rightX + rightY*rightY + rightZ*rightZ);
		if (rightLen > 0.001f)
		{
			rightX /= rightLen;
			rightY /= rightLen;
			rightZ /= rightLen;
		}
		float dirLen = sqrtf(dx*dx + dy*dy + dz*dz);
		if (dirLen > 0.001f)
		{
			dx /= dirLen;
			dy /= dirLen;
			dz /= dirLen;
		}
		float trueUpX = rightY * dz - rightZ * dy;
		float trueUpY = rightZ * dx - rightX * dz;
		float trueUpZ = rightX * dy - rightY * dx;
		for (int j = 0; j <= circleSegments; ++j)
		{
			float angle = (float)j / (float)circleSegments * 2.0f * 3.14159f;
			float cosA = cosf(angle);
			float sinA = sinf(angle);
			float circleX = x + radius * (cosA * rightX + sinA * trueUpX);
			float circleY = y + radius * (cosA * rightY + sinA * trueUpY);
			float circleZ = z + radius * (cosA * rightZ + sinA * trueUpZ);
			gl->glVertex3f(circleX, circleY, circleZ);
			if (i < segments)
			{
				float nextCircleX = x2 + radius * (cosA * rightX + sinA * trueUpX);
				float nextCircleY = y2 + radius * (cosA * rightY + sinA * trueUpY);
				float nextCircleZ = z2 + radius * (cosA * rightZ + sinA * trueUpZ);
				gl->glVertex3f(nextCircleX, nextCircleY, nextCircleZ);
			}
			else
			{
				gl->glVertex3f(circleX, circleY, circleZ);
			}
		}
	}
	gl->glEnd();
	gl->glColor3f(hairR, hairG, hairB);
	float tipZ = -0.02f - tailLength;
	float tipY = -0.6f;
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, tipY, tipZ);
	gl->glRotatef(-tailHang * 0.8f, 1.0f, 0.0f, 0.0f);
	gl->glRotatef(sway * 2.0f, 0.0f, 1.0f, 0.0f);
	int numHairs = 20;
	float hairLength = 0.45f;
	float fanAngle = 70.0f;
	for (int i = 0; i < numHairs; ++i)
	{
		float angle = (i - (numHairs - 1) / 2.0f) * (fanAngle / (numHairs - 1));
		gl->glPushMatrix();
		gl->glRotatef(angle, 0.0f, 0.0f, 1.0f);
		float wave = sinf(m_ElephantGameState.animationTime * 2.8f + i * 1.3f) * 10.0f;
		gl->glRotatef(-80.0f + wave, 1.0f, 0.0f, 0.0f);
		gl->glBegin(GL_LINES);
		gl->glVertex3f(0.0f, 0.0f, 0.0f);
		gl->glVertex3f(0.0f, 0.0f, -hairLength);
		gl->glEnd();
		gl->glPopMatrix();
	}
	gl->glPopMatrix();
	gl->glPopMatrix();
}

// Structure for 3D point
struct Point3D
{
	float x, y, z;
};

// Draw elephant tusks
void CObjectDemoExperiment::DrawElephantTusks()
{
	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView))) return;
	gl->glColor3f(0.95f, 0.95f, 0.88f);
	float tuskLength = 0.8f;
	float baseRadius = 0.05f;
	float tipRadius = 0.015f;
	float bodyBaseY = 0.8f;
	float neckBaseY = bodyBaseY + 0.35f;
	float headY = neckBaseY + 0.1f + m_ElephantGameState.headBob;
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, headY, 1.15f);
	gl->glTranslatef(-0.2f, -0.15f, 0.4f);
	gl->glRotatef(-20.0f, 0.0f, 1.0f, 0.0f);
	gl->glRotatef(-5.0f, 1.0f, 0.0f, 0.0f);
	int segments = 12;
	for (int i = 0; i < segments; i++)
	{
		float t = (float)i / (float)segments;
		float radius = baseRadius * (1.0f - t) + tipRadius * t;
		float y = -t * t * 0.15f;
		float z = t * tuskLength;
		gl->glPushMatrix();
		gl->glTranslatef(0.0f, y, z);
		gl->glScalef(radius, radius, tuskLength / (float)segments);
		DrawEllipsoid(1.0f, 10, 8);
		gl->glPopMatrix();
	}
	gl->glPopMatrix();
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, headY, 1.15f);
	gl->glTranslatef(0.2f, -0.15f, 0.4f);
	gl->glRotatef(20.0f, 0.0f, 1.0f, 0.0f);
	gl->glRotatef(-5.0f, 1.0f, 0.0f, 0.0f);
	for (int i = 0; i < segments; i++)
	{
		float t = (float)i / (float)segments;
		float radius = baseRadius * (1.0f - t) + tipRadius * t;
		float y = -t * t * 0.15f;
		float z = t * tuskLength;
		gl->glPushMatrix();
		gl->glTranslatef(0.0f, y, z);
		gl->glScalef(radius, radius, tuskLength / (float)segments);
		DrawEllipsoid(1.0f, 10, 8);
		gl->glPopMatrix();
	}
	gl->glPopMatrix();
}

// Update elephant animation state
void CObjectDemoExperiment::UpdateElephantAnimation()
{
	m_ElephantGameState.animationTime += 0.016f;

	float breathe = sinf(m_ElephantGameState.animationTime * 1.5f) * 0.008f;

	m_ElephantGameState.headBob = breathe + sinf(m_ElephantGameState.animationTime * 0.8f) * 0.005f;

	m_ElephantGameState.trunkSwing = sinf(m_ElephantGameState.animationTime * 1.8f) * 0.2f;

	m_ElephantGameState.earFlap = sinf(m_ElephantGameState.animationTime * 1.2f) * 0.15f;

	if (m_ElephantGameState.currentState != 0)
	{
		float moveSpeed = m_ElephantGameState.walkSpeed;
		float radY = m_ElephantGameState.rotationY * 3.14159f / 180.0f;

		m_ElephantGameState.positionZ -= cosf(radY) * moveSpeed;
		m_ElephantGameState.positionX -= sinf(radY) * moveSpeed;
	}

	if (m_ElephantGameState.isTrumpeting)
	{
		m_ElephantGameState.trumpetProgress += 0.03f;
		if (m_ElephantGameState.trumpetProgress >= 1.0f)
		{
			m_ElephantGameState.isTrumpeting = false;
			m_ElephantGameState.trumpetProgress = 0.0f;
		}
	}
}

// -------------------------------------------------------------------
// Geometric primitive drawing functions
// -------------------------------------------------------------------
// Draw sphere primitive
void CObjectDemoExperiment::DrawSphere(float radius, int slices, int stacks)
{
	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView))) return;
	const float PI = 3.14159265358979323846f;
	for (int i = 0; i < stacks; ++i)
	{
		float phi1 = (i * PI) / stacks;
		float phi2 = ((i + 1) * PI) / stacks;
		gl->glBegin(GL_QUAD_STRIP);
		for (int j = 0; j <= slices; ++j)
		{
			float theta = (2.0f * j * PI) / slices;
			float x1 = radius * sinf(phi1) * cosf(theta);
			float y1 = radius * cosf(phi1);
			float z1 = radius * sinf(phi1) * sinf(theta);
			float x2 = radius * sinf(phi2) * cosf(theta);
			float y2 = radius * cosf(phi2);
			float z2 = radius * sinf(phi2) * sinf(theta);
			gl->glVertex3f(x1, y1, z1);
			gl->glVertex3f(x2, y2, z2);
		}
		gl->glEnd();
	}
}

// Draw cylinder primitive
void CObjectDemoExperiment::DrawCylinder(float radius, float height)
{
	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView))) return;

	const int slices = 16;
	const float PI = 3.14159265358979323846f;

	gl->glBegin(GL_QUAD_STRIP);
	for (int i = 0; i <= slices; i++)
	{
		float angle = 2.0f * PI * i / slices;
		float x = cosf(angle) * radius;
		float z = sinf(angle) * radius;

		gl->glVertex3f(x, height, z);
		gl->glVertex3f(x, 0.0f, z);
	}
	gl->glEnd();

	gl->glBegin(GL_TRIANGLE_FAN);
	gl->glVertex3f(0.0f, height, 0.0f);
	for (int i = 0; i <= slices; i++)
	{
		float angle = 2.0f * PI * i / slices;
		float x = cosf(angle) * radius;
		float z = sinf(angle) * radius;
		gl->glVertex3f(x, height, z);
	}
	gl->glEnd();

	gl->glBegin(GL_TRIANGLE_FAN);
	gl->glVertex3f(0.0f, 0.0f, 0.0f);
	for (int i = slices; i >= 0; i--)
	{
		float angle = 2.0f * PI * i / slices;
		float x = cosf(angle) * radius;
		float z = sinf(angle) * radius;
		gl->glVertex3f(x, 0.0f, z);
	}
	gl->glEnd();
}

// Draw ellipsoid primitive
void CObjectDemoExperiment::DrawEllipsoid(float radius, int slices, int stacks)
{
	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView))) return;
	const float PI = 3.14159265358979323846f;
	for (int i = 0; i < stacks; ++i)
	{
		float phi1 = (i * PI) / stacks;
		float phi2 = ((i + 1) * PI) / stacks;
		gl->glBegin(GL_QUAD_STRIP);
		for (int j = 0; j <= slices; ++j)
		{
			float theta = (2.0f * j * PI) / slices;
			float x1 = radius * sinf(phi1) * cosf(theta);
			float y1 = radius * cosf(phi1);
			float z1 = radius * sinf(phi1) * sinf(theta);
			float x2 = radius * sinf(phi2) * cosf(theta);
			float y2 = radius * cosf(phi2);
			float z2 = radius * sinf(phi2) * sinf(theta);
			gl->glNormal3f(x1 / radius, y1 / radius, z1 / radius);
			gl->glVertex3f(x1, y1, z1);
			gl->glNormal3f(x2 / radius, y2 / radius, z2 / radius);
			gl->glVertex3f(x2, y2, z2);
		}
		gl->glEnd();
	}
}

// Draw rounded cube (sphere approximation)
void CObjectDemoExperiment::DrawRoundedCube(float size)
{
	DrawSphere(size, 10, 10);
}

// -------------------------------------------------------------------
// Environment rendering functions
// -------------------------------------------------------------------
// Draw savannah environment with pond and vegetation
void CObjectDemoExperiment::DrawSavannahEnvironmentWithPond()
{
	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView))) return;
	gl->glEnable(GL_BLEND);
	gl->glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
	gl->glBegin(GL_QUADS);
	gl->glColor3f(0.55f, 0.68f, 0.35f);
	gl->glVertex3f(-20.0f, 0.0f, -20.0f);
	gl->glVertex3f(20.0f, 0.0f, -20.0f);
	gl->glVertex3f(20.0f, 0.0f, 20.0f);
	gl->glVertex3f(-20.0f, 0.0f, 20.0f);
	for (int patch = 0; patch < 6; patch++)
	{
		float x = (rand() % 15) - 7.5f;
		float z = (rand() % 15) - 7.5f;
		float size = 3.0f + (rand() % 5);
		gl->glColor3f(0.65f, 0.75f, 0.45f);
		gl->glVertex3f(x - size, 0.01f, z - size);
		gl->glVertex3f(x + size, 0.01f, z - size);
		gl->glVertex3f(x + size, 0.01f, z + size);
		gl->glVertex3f(x - size, 0.01f, z + size);
	}
	gl->glEnd();
	float pondCenterZ = 8.0f;
	float pondRadius = 4.5f;
	gl->glBegin(GL_QUADS);
	gl->glColor3f(0.75f, 0.65f, 0.45f);
	for (int i = 0; i < 32; i++)
	{
		float angle1 = i * (360.0f / 32) * 3.14159f / 180.0f;
		float angle2 = (i + 1) * (360.0f / 32) * 3.14159f / 180.0f;
		float bankWidth = 1.2f;
		float x1_outer = cosf(angle1) * (pondRadius + bankWidth);
		float z1_outer = sinf(angle1) * (pondRadius + bankWidth) + pondCenterZ;
		float x2_outer = cosf(angle2) * (pondRadius + bankWidth);
		float z2_outer = sinf(angle2) * (pondRadius + bankWidth) + pondCenterZ;
		float x1_inner = cosf(angle1) * pondRadius * 1.05f;
		float z1_inner = sinf(angle1) * pondRadius * 1.05f + pondCenterZ;
		float x2_inner = cosf(angle2) * pondRadius * 1.05f;
		float z2_inner = sinf(angle2) * pondRadius * 1.05f + pondCenterZ;
		gl->glVertex3f(x1_outer, 0.02f, z1_outer);
		gl->glVertex3f(x2_outer, 0.02f, z2_outer);
		gl->glVertex3f(x2_inner, 0.02f, z2_inner);
		gl->glVertex3f(x1_inner, 0.02f, z1_inner);
	}
	gl->glEnd();
	gl->glBegin(GL_QUADS);
	for (int layer = 0; layer < 4; layer++)
	{
		float innerRadius = pondRadius * (1.0f - layer * 0.15f);
		float alpha = 0.5f - layer * 0.08f;
		float y = 0.01f + layer * 0.002f;
		gl->glColor4f(0.25f, 0.45f, 0.75f, alpha);
		for (int i = 0; i < 32; i++)
		{
			float angle1 = i * (360.0f / 32) * 3.14159f / 180.0f;
			float angle2 = (i + 1) * (360.0f / 32) * 3.14159f / 180.0f;
			float x1 = cosf(angle1) * innerRadius;
			float z1 = sinf(angle1) * innerRadius + pondCenterZ;
			float x2 = cosf(angle2) * innerRadius;
			float z2 = sinf(angle2) * innerRadius + pondCenterZ;
			gl->glVertex3f(0.0f, y, pondCenterZ);
			gl->glVertex3f(x1, y, z1);
			gl->glVertex3f(x2, y, z2);
			gl->glVertex3f(0.0f, y, pondCenterZ);
		}
	}
	gl->glEnd();
	float rippleTime = m_ElephantGameState.animationTime;
	gl->glBegin(GL_LINES);
	gl->glColor4f(0.9f, 0.9f, 1.0f, 0.4f);
	for (int ring = 0; ring < 3; ring++)
	{
		float rippleRadius = 1.5f + ring * 0.8f + sinf(rippleTime * 2.0f + ring) * 0.3f;
		for (int i = 0; i < 24; i++)
		{
			float angle1 = i * (360.0f / 24) * 3.14159f / 180.0f;
			float angle2 = (i + 1) * (360.0f / 24) * 3.14159f / 180.0f;
			float x1 = cosf(angle1) * rippleRadius;
			float z1 = sinf(angle1) * rippleRadius + pondCenterZ;
			float x2 = cosf(angle2) * rippleRadius;
			float z2 = sinf(angle2) * rippleRadius + pondCenterZ;
			gl->glVertex3f(x1, 0.015f, z1);
			gl->glVertex3f(x2, 0.015f, z2);
		}
	}
	gl->glEnd();
	gl->glColor3f(0.3f, 0.6f, 0.3f);
	for (int lily = 0; lily < 8; lily++)
	{
		float angle = lily * 45.0f * 3.14159f / 180.0f;
		float radius = 2.0f + (lily % 3) * 0.4f;
		float x = cosf(angle) * radius;
		float z = sinf(angle) * radius + pondCenterZ;
		gl->glPushMatrix();
		gl->glTranslatef(x, 0.02f, z);
		gl->glBegin(GL_TRIANGLE_FAN);
		gl->glVertex3f(0.0f, 0.0f, 0.0f);
		for (int i = 0; i <= 12; i++)
		{
			float lilyAngle = i * (360.0f / 12) * 3.14159f / 180.0f;
			float lilyX = cosf(lilyAngle) * 0.4f;
			float lilyZ = sinf(lilyAngle) * 0.4f;
			gl->glVertex3f(lilyX, 0.0f, lilyZ);
		}
		gl->glEnd();
		gl->glPopMatrix();
	}
	gl->glBegin(GL_QUADS);
	gl->glColor3f(0.4f, 0.6f, 0.9f);
	gl->glVertex3f(-30.0f, 15.0f, -30.0f);
	gl->glVertex3f(30.0f, 15.0f, -30.0f);
	gl->glColor3f(0.7f, 0.85f, 1.0f);
	gl->glVertex3f(30.0f, 0.0f, -30.0f);
	gl->glVertex3f(-30.0f, 0.0f, -30.0f);
	gl->glColor3f(0.4f, 0.6f, 0.9f);
	gl->glVertex3f(-30.0f, 15.0f, 30.0f);
	gl->glVertex3f(30.0f, 15.0f, 30.0f);
	gl->glColor3f(0.7f, 0.85f, 1.0f);
	gl->glVertex3f(30.0f, 0.0f, 30.0f);
	gl->glVertex3f(-30.0f, 0.0f, 30.0f);
	gl->glColor3f(0.4f, 0.6f, 0.9f);
	gl->glVertex3f(-30.0f, 15.0f, -30.0f);
	gl->glVertex3f(-30.0f, 15.0f, 30.0f);
	gl->glColor3f(0.7f, 0.85f, 1.0f);
	gl->glVertex3f(-30.0f, 0.0f, 30.0f);
	gl->glVertex3f(-30.0f, 0.0f, -30.0f);
	gl->glColor3f(0.4f, 0.6f, 0.9f);
	gl->glVertex3f(30.0f, 15.0f, -30.0f);
	gl->glVertex3f(30.0f, 15.0f, 30.0f);
	gl->glColor3f(0.7f, 0.85f, 1.0f);
	gl->glVertex3f(30.0f, 0.0f, 30.0f);
	gl->glVertex3f(30.0f, 0.0f, -30.0f);
	gl->glEnd();
	gl->glPushMatrix();
	gl->glTranslatef(12.0f, 8.0f, -15.0f);
	gl->glColor3f(1.0f, 0.95f, 0.7f);
	gl->glBegin(GL_TRIANGLE_FAN);
	gl->glVertex3f(0.0f, 0.0f, 0.0f);
	for (int i = 0; i <= 24; i++)
	{
		float sunAngle = i * (360.0f / 24) * 3.14159f / 180.0f;
		float sunX = cosf(sunAngle) * 1.5f;
		float sunY = sinf(sunAngle) * 1.5f;
		gl->glVertex3f(sunX, sunY, 0.0f);
	}
	gl->glEnd();
	gl->glColor4f(1.0f, 0.9f, 0.6f, 0.3f);
	for (int ray = 0; ray < 12; ray++)
	{
		float rayAngle = ray * 30.0f * 3.14159f / 180.0f;
		float rayLength = 2.5f;
		gl->glBegin(GL_TRIANGLES);
		gl->glVertex3f(0.0f, 0.0f, 0.0f);
		float rayX1 = cosf(rayAngle - 0.1f) * rayLength;
		float rayY1 = sinf(rayAngle - 0.1f) * rayLength;
		float rayX2 = cosf(rayAngle + 0.1f) * rayLength;
		float rayY2 = sinf(rayAngle + 0.1f) * rayLength;
		gl->glVertex3f(rayX1, rayY1, 0.0f);
		gl->glVertex3f(rayX2, rayY2, 0.0f);
		gl->glEnd();
	}
	gl->glPopMatrix();

	for (int tree = 0; tree < 10; tree++)
	{
		float angle = tree * 36.0f * 3.14159f / 180.0f;
		float baseDistance = 5.0f;
		float distance = baseDistance + (tree % 3) * 1.0f;
		float x = cosf(angle) * distance;
		float z = sinf(angle) * distance;
		if (sqrtf((x * x) + ((z - pondCenterZ) * (z - pondCenterZ))) < pondRadius + 3.0f)
			continue;
		float distanceFromOrigin = sqrtf(x*x + z*z);
		if (distanceFromOrigin < 3.0f)
			continue;
		DrawAcaciaTree(x, z);
	}

	gl->glPushMatrix();

	DrawAcaciaTree(4.0f, 2.0f);
	DrawAcaciaTree(5.0f, -1.0f);
	DrawAcaciaTree(-4.0f, 2.0f);
	DrawAcaciaTree(-5.0f, -1.0f);
	DrawAcaciaTree(0.0f, -6.0f);
	DrawAcaciaTree(3.0f, 6.0f);
	DrawAcaciaTree(-3.0f, 6.0f);

	gl->glPopMatrix();

	gl->glColor3f(0.45f, 0.58f, 0.3f);
	for (int clump = 0; clump < 50; clump++)
	{
		float x = (rand() % 38) - 19.0f;
		float z = (rand() % 38) - 19.0f;
		if (sqrtf((x * x) + ((z - pondCenterZ) * (z - pondCenterZ))) < pondRadius + 1.5f)
			continue;
		DrawGrassClump(x, z);
	}

	float cloudTime = m_ElephantGameState.animationTime * 0.1f;
	DrawCloud(-8.0f + sinf(cloudTime) * 2.0f, 6.0f, -10.0f, 1.2f);
	DrawCloud(5.0f + sinf(cloudTime * 0.8f + 1.0f) * 1.5f, 5.5f, 5.0f, 0.9f);
	DrawCloud(-15.0f + sinf(cloudTime * 1.2f + 2.0f) * 1.8f, 7.0f, 8.0f, 1.5f);

	gl->glColor3f(0.4f, 0.5f, 0.3f);
	for (int hill = 0; hill < 3; hill++)
	{
		float hillZ = -18.0f - hill * 3.0f;
		float hillHeight = 3.0f + hill * 1.5f;
		gl->glBegin(GL_TRIANGLE_STRIP);
		for (int x = -20; x <= 20; x += 2)
		{
			float heightVariation = sinf(x * 0.3f + hill * 2.0f) * 0.5f;
			gl->glVertex3f(x, 0.0f, hillZ);
			gl->glVertex3f(x, hillHeight + heightVariation, hillZ - 2.0f);
		}
		gl->glEnd();
	}
	gl->glDisable(GL_BLEND);
}

// Draw acacia tree with branches and leaves
void CObjectDemoExperiment::DrawAcaciaTree(float x, float z)
{
	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView))) return;
	gl->glPushMatrix();
	gl->glTranslatef(x, 0.0f, z);

	gl->glColor3f(0.45f, 0.35f, 0.25f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 1.2f, 0.0f);
	gl->glRotatef(sinf(x * 0.5f) * 15.0f, 0, 1, 0);
	gl->glScalef(0.22f, 2.5f, 0.22f);
	DrawEllipsoid(1.0f, 10, 8);
	gl->glPopMatrix();

	gl->glColor3f(0.4f, 0.3f, 0.2f);
	for (int branch = 0; branch < 3; branch++)
	{
		float angle = branch * 120.0f * 3.14159f / 180.0f;
		float branchHeight = 2.5f + (branch % 2) * 0.3f;
		float branchLength = 1.6f + (rand() % 6) * 0.1f;
		gl->glPushMatrix();
		gl->glTranslatef(0.0f, branchHeight, 0.0f);
		gl->glRotatef(angle, 0, 1, 0);
		gl->glRotatef(20.0f, 1, 0, 0);
		gl->glPushMatrix();
		gl->glScalef(0.10f, 0.10f, branchLength);
		DrawEllipsoid(1.0f, 8, 6);
		gl->glPopMatrix();

		for (int sub = 0; sub < 2; sub++)
		{
			float subAngle = sub * 180.0f * 3.14159f / 180.0f;
			float subLength = 0.6f + (rand() % 3) * 0.1f;
			gl->glPushMatrix();
			gl->glTranslatef(0.0f, 0.0f, branchLength * 0.5f);
			gl->glRotatef(subAngle, 0, 0, 1);
			gl->glRotatef(30.0f, 0, 1, 0);
			gl->glPushMatrix();
			gl->glScalef(0.05f, 0.05f, subLength);
			DrawEllipsoid(1.0f, 6, 4);
			gl->glPopMatrix();
			gl->glPopMatrix();
		}
		gl->glPopMatrix();
	}

	gl->glColor3f(0.25f, 0.35f, 0.2f);
	gl->glPushMatrix();
	gl->glTranslatef(0.0f, 3.2f, 0.0f);
	gl->glScalef(1.8f, 0.6f, 1.8f);
	DrawEllipsoid(1.0f, 12, 10);
	gl->glPopMatrix();

	gl->glColor3f(0.2f, 0.3f, 0.15f);
	for (int leaf = 0; leaf < 5; leaf++)
	{
		float leafAngle = leaf * 72.0f * 3.14159f / 180.0f;
		float leafRadius = 0.8f + (rand() % 3) * 0.1f;
		float leafX = cosf(leafAngle) * leafRadius;
		float leafZ = sinf(leafAngle) * leafRadius;
		float leafY = 3.0f + (rand() % 2) * 0.1f;
		gl->glPushMatrix();
		gl->glTranslatef(leafX, leafY, leafZ);
		gl->glScalef(0.5f, 0.25f, 0.5f);
		DrawEllipsoid(1.0f, 6, 4);
		gl->glPopMatrix();
	}

	gl->glPopMatrix();
}

// Draw grass clump with wind animation
void CObjectDemoExperiment::DrawGrassClump(float x, float z)
{
	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView))) return;
	gl->glPushMatrix();
	gl->glTranslatef(x, 0.0f, z);
	for (int blade = 0; blade < 7; blade++)
	{
		float bladeAngle = blade * (360.0f / 7) * 3.14159f / 180.0f;
		float bladeX = cosf(bladeAngle) * 0.2f;
		float bladeZ = sinf(bladeAngle) * 0.2f;
		float bladeHeight = 0.3f + (rand() % 10) * 0.03f;
		float bladeWidth = 0.02f;
		float windSway = sinf(m_ElephantGameState.animationTime * 2.0f + x * 0.1f) * 0.1f;
		gl->glPushMatrix();
		gl->glTranslatef(bladeX, 0.0f, bladeZ);
		gl->glRotatef(windSway * 30.0f, 0, 1, 0);
		gl->glBegin(GL_TRIANGLES);
		gl->glVertex3f(-bladeWidth, 0.0f, 0.0f);
		gl->glVertex3f(bladeWidth, 0.0f, 0.0f);
		gl->glVertex3f(0.0f, bladeHeight, 0.0f);
		gl->glEnd();
		gl->glPopMatrix();
	}
	gl->glPopMatrix();
}

// Draw cloud with animated movement
void CObjectDemoExperiment::DrawCloud(float x, float y, float z, float scale)
{
	CComPtr<IOpenGLView> gl;
	if (FAILED(gl.CoCreateInstance(CLSID_OpenGLView))) return;
	gl->glPushMatrix();
	gl->glTranslatef(x, y, z);
	gl->glScalef(scale, scale * 0.5f, scale);
	gl->glColor4f(1.0f, 1.0f, 1.0f, 0.8f);
	float cloudParts[][3] = {
		{ 0.0f, 0.0f, 0.0f },
		{ 0.5f, 0.2f, 0.2f },
		{ -0.4f, 0.1f, 0.3f },
		{ 0.3f, -0.1f, -0.4f },
		{ -0.3f, -0.2f, -0.2f }
	};
	for (int i = 0; i < 5; i++)
	{
		gl->glPushMatrix();
		gl->glTranslatef(cloudParts[i][0], cloudParts[i][1], cloudParts[i][2]);
		gl->glScalef(0.7f, 0.5f, 0.7f);
		DrawEllipsoid(1.0f, 12, 10);
		gl->glPopMatrix();
	}
	gl->glPopMatrix();
}