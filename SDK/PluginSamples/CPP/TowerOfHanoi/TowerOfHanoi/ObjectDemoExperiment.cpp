// PlusTwoPhysicsExperiment.cpp : implementation file
//

#include "stdafx.h"
#include "ObjectDemoExperiment.h"
#include "TowerOfHanoiSimulation.h"
#include "AddinSimulationManager.h"
#include "PropSliderCtrl.h"
#include <cmath>
#include <vector>
#include <stack>
#include <algorithm>

// CPlusTwoPhysicsExperiment
using namespace ATL;




CObjectDemoExperiment::CObjectDemoExperiment(CAddinSimulationManager* pManager)
{
	m_pManager = pManager;
	m_ObjectPattern.m_dAnimationSpeed = 1.0; // Base speed 1x
}

CObjectDemoExperiment::~CObjectDemoExperiment()
{
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
	ExperimentTreeView->AddExperiment(SessionID, CString(OBJECT_3D_TREE_ROOT_TITLE).AllocSysString(), CString(OBJECT_TYPE_TOWEROFHANOI).AllocSysString());

	ExperimentTreeView->Refresh();
}

void CObjectDemoExperiment::OnTreeNodeSelect(BSTR ExperimentGroup, BSTR ExperimentName)
{
	OnReloadExperiment(ExperimentGroup, ExperimentName);
}
void CObjectDemoExperiment::OnTreeNodeDblClick(BSTR ExperimentGroup, BSTR ExperimentName)
{
	if (CString(ExperimentGroup) == OBJECT_3D_TREE_ROOT_TITLE)
	{
		m_ObjectPattern.m_strObjectType = CString(ExperimentName);
		ShowObjectProperties();
	}
	else
	{
		m_pManager->ResetPropertyGrid();
	}
}
void CObjectDemoExperiment::OnReloadExperiment(BSTR ExperimentGroup, BSTR ExperimentName)
{
	if (m_pManager->m_bSimulationActive)
	{
		m_pManager->SetSimulationStatus(FALSE);
		Sleep(100); // Give the thread a moment to exit
	}

	if (CString(ExperimentGroup) == OBJECT_3D_TREE_ROOT_TITLE)
	{
		m_ObjectPattern.m_strObjectType = CString(ExperimentName);
		if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_TOWEROFHANOI)
		{
			ShowObjectProperties();
		}
		DrawObject(ExperimentName);
	}
	else
	{
		//
	}
}

// CPlusTwoPhysicsExperiment member functions
void CObjectDemoExperiment::ShowObjectProperties()
{
	CComPtr<IPropertyWindow> PropertyWindow;
	HRESULT HR = PropertyWindow.CoCreateInstance(CLSID_PropertyWindow);
	CString strGroupName = _T("");
	if (SUCCEEDED(HR))
	{
		PropertyWindow->RemoveAll();

		if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_TOWEROFHANOI)
		{
			strGroupName = TOWEROFHANOI_PROPERTIES_TITLE;
			PropertyWindow->AddPropertyGroup(strGroupName.AllocSysString());

			CString strRings;
			strRings.Format(_T("%d"), m_ObjectPattern.m_nNumberOfRings);
			PropertyWindow->AddPropertyItemAsString(strGroupName.AllocSysString(), TOWEROFHANOI_RINGS_TITLE, strRings.AllocSysString(), _T("Number of rings on the starting peg (3-8)"));

			CString strSpeed;
			strSpeed.Format(_T("%.2f"), m_ObjectPattern.m_dAnimationSpeed);
			PropertyWindow->AddPropertyItemsAsString(strGroupName.AllocSysString(), TOWEROFHANOI_SPEED_TITLE, TOWEROFHANOI_SPEED_OPTIONS, strSpeed.AllocSysString(), _T("Select the animation speed multiplier."), FALSE);

		}
		else
		{
			strGroupName = OBJECT_PROPERTIES_TITLE;
			PropertyWindow->AddPropertyGroup(strGroupName.AllocSysString());
			PropertyWindow->AddPropertyItemsAsString(strGroupName.AllocSysString(), OBJECT_TYPE_TITLE, OBJECT_TYPES, m_ObjectPattern.m_strObjectType.AllocSysString(), _T("Select the Object from the List"), FALSE);
			PropertyWindow->AddPropertyItemsAsString(strGroupName.AllocSysString(), OBJECT_SIMULATION_PATTERN_TITLE, OBJECT_PATTERN_TYPES, m_ObjectPattern.m_strSimulationPattern.AllocSysString(), _T("Select the Simulation Pattern"), FALSE);
			CString strInterval;
			strInterval.Format(_T("%d"), m_ObjectPattern.m_lSimulationInterval);
			PropertyWindow->AddPropertyItemAsString(strGroupName.AllocSysString(), OBJECT_SIMULATION_INTERVAL_TITLE, strInterval.AllocSysString(), _T("Simulation Interval In Milli Seconds"));
		}

		PropertyWindow->AddColorPropertyItem(strGroupName.AllocSysString(), OBJECT_COLOR_TITLE, m_ObjectPattern.m_Color, _T("Select the Color"));

		PropertyWindow->EnableHeaderCtrl(FALSE);
		PropertyWindow->EnableDescriptionArea(TRUE);
		PropertyWindow->SetVSDotNetLook(TRUE);
		PropertyWindow->MarkModifiedProperties(TRUE, TRUE);

		if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_TOWEROFHANOI)
		{
			SyncRibbonWithProperties();
		}
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

void CObjectDemoExperiment::SyncRibbonWithProperties()
{
	if (m_pManager && m_pManager->m_pAddin)
	{
		// Sync number of rings
		CString ringsText;
		ringsText.Format(_T("%d"), m_ObjectPattern.m_nNumberOfRings);
		m_pManager->m_pAddin->SetRibbonControlText(_T("NumRingsCombo"), ringsText);

		// Sync animation speed
		CString speedText;
		speedText.Format(_T("%.2fx"), m_ObjectPattern.m_dAnimationSpeed);
		m_pManager->m_pAddin->SetRibbonControlText(_T("SpeedCombo"), speedText);
	}
}


void CObjectDemoExperiment::OnPropertyChanged(BSTR GroupName, BSTR PropertyName, BSTR PropertyValue)
{
	if (CString(GroupName) == OBJECT_PROPERTIES_TITLE || CString(GroupName) == TOWEROFHANOI_PROPERTIES_TITLE)
	{
		m_ObjectPattern.OnPropertyChanged(GroupName, PropertyName, PropertyValue);
		SyncRibbonWithProperties();
	}

	if (m_pManager->m_bSimulationActive)
	{
		m_pManager->SetSimulationStatus(FALSE);
		Sleep(100); // give thread time to exit
	}
	DrawScene();
}

void CObjectDemoExperiment::DrawScene()
{
	OnReloadExperiment(m_pManager->m_strExperimentGroup.AllocSysString(), m_pManager->m_strExperimentName.AllocSysString());
}


void CObjectDemoExperiment::DrawObject(CString ExperimentName)
{
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
	else if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_TOWEROFHANOI)
	{
		InitializePegs();
		DrawTowerOfHanoi();
	}

}


void CObjectDemoExperiment::DrawCube()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR))
	{
		return;
	}

	//We can use all the normal OpenGL API defined in the standard Opengl header file
	const float radius = 0.34f;

	ApplicationView->InitializeEnvironment(TRUE);
	ApplicationView->BeginGraphicsCommands();

	//Set the Background Color
	ApplicationView->SetBkgColor(GetRValue(m_ObjectPattern.m_Color) / (float)255.0, GetGValue(m_ObjectPattern.m_Color) / (float)255.0,
		GetBValue(m_ObjectPattern.m_Color) / (float)255.0, 1.0);

	HR = ApplicationView->StartNewDisplayList();
	if (HR == E_FAIL)
	{
		return;
	}

	//Draw using Native IOpenGLView Interface
	CComPtr<IOpenGLView> OpenGLView;
	HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR))
	{
		return;
	}


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

	OpenGLView->glColor3f(1.f, 1.f, 1.f);
	OpenGLView->glRasterPos3f(-radius, radius, radius);
	OpenGLView->glRasterPos3f(-radius, -radius, radius);
	OpenGLView->glRasterPos3f(radius, radius, radius);
	OpenGLView->glRasterPos3f(radius, -radius, radius);
	OpenGLView->glRasterPos3f(radius, radius, -radius);
	OpenGLView->glRasterPos3f(radius, -radius, -radius);
	OpenGLView->glRasterPos3f(-radius, radius, -radius);
	OpenGLView->glRasterPos3f(-radius, -radius, -radius);

	ApplicationView->EndNewDisplayList();
	ApplicationView->EndGraphicsCommands();
	ApplicationView->Refresh();
}


void CObjectDemoExperiment::DrawBall()
{
	//Draw using ApplicationView Interface
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

	int SECTIONS = 25;
	double RADIUS = 1.0;

	HR = ApplicationView->StartNewDisplayList();
	if (HR == E_FAIL)
	{
		return;
	}

	ApplicationView->SetColorf(0.0f, 0.0f, 1.0f);

	ApplicationView->DrawSphere(RADIUS, SECTIONS, SECTIONS);
	//Draw One more spehere inside it

	ApplicationView->SetColorf(1.0f, 1.0f, 1.0f);

	ApplicationView->DrawSphere(RADIUS / 1.5, SECTIONS, SECTIONS);

	ApplicationView->EndNewDisplayList();
	ApplicationView->EndGraphicsCommands();
	ApplicationView->Refresh();
}


void CObjectDemoExperiment::DrawPyramid()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR))
	{
		return;
	}

	//We can use all the normal OpenGL API defined in the standard Opengl header file
	const float radius = 0.34f;
	ApplicationView->ResetScene();
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

	//Draw using Native IOpenGLView Interface
	CComPtr<IOpenGLView> OpenGLView;
	HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR))
	{
		return;
	}

	OpenGLView->glTranslatef(0.01f, 0.f, 0.01f);
	OpenGLView->glColor3f(0.0f, 0.4f, 0.8f);

	// We're telling OpenGL that we want to render triangles.
	OpenGLView->glBegin(GL_TRIANGLES);

	// Each of the pyramid's faces will have 3 vertices.
	// We'll start drawing at the top, then go down to the bottom left,
	// then to the right.

	// When we start our next triangle, we're going to be going back to
	// the top-middle. Imagine drawing a pyramid without ever lifting your
	// pen up.

	// New Triangle - Front
	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(0.0f, 1.0f, 0.0f);

	OpenGLView->glColor3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(-1.0f, -1.0f, 1.0f);

	OpenGLView->glColor3f(0.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(1.0f, -1.0f, 1.0f);

	// New Triangle - Right
	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(0.0f, 1.0f, 0.0f);

	OpenGLView->glColor3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(1.0f, -1.0f, 1.0f);

	OpenGLView->glColor3f(0.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(1.0f, -1.0f, -1.0f);

	// New Triangle - Back
	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(0.0f, 1.0f, 0.0f);

	OpenGLView->glColor3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(1.0f, -1.0f, -1.0f);

	OpenGLView->glColor3f(0.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(-1.0f, -1.0f, -1.0f);

	// New Triangle - left
	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(0.0f, 1.0f, 0.0f);

	OpenGLView->glColor3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(-1.0f, -1.0f, -1.0f);

	OpenGLView->glColor3f(0.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(-1.0f, -1.0f, 1.0f);

	// New Triangle - Bottom 1
	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(-1.0f, -1.0f, 1.0f);

	OpenGLView->glColor3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(1.0f, -1.0f, 1.0f);

	OpenGLView->glColor3f(0.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(-1.0f, -1.0f, -1.0f);

	// New Triangle - Bottom 2
	OpenGLView->glColor3f(1.0f, 0.0f, 0.0f);
	OpenGLView->glVertex3f(-1.0f, -1.0f, -1.0f);      // Note: we're starting from the last point
													  // of the previous triangle.

	OpenGLView->glColor3f(0.0f, 1.0f, 0.0f);
	OpenGLView->glVertex3f(1.0f, -1.0f, -1.0f);

	OpenGLView->glColor3f(0.0f, 0.0f, 1.0f);
	OpenGLView->glVertex3f(1.0f, -1.0f, 1.0f);

	OpenGLView->glEnd();

	ApplicationView->EndNewDisplayList();
	ApplicationView->EndGraphicsCommands();
	ApplicationView->Refresh();
}


void CObjectDemoExperiment::DrawAeroplane()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR))
	{
		return;
	}

	//We can use all the normal OpenGL API defined in the standard Opengl header file
	const float radius = 0.34f;

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

	//Draw using Native IOpenGLView Interface
	CComPtr<IOpenGLView> OpenGLView;
	HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR))
	{
		return;
	}

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

void DrawCylinder(IOpenGLView* pView, float radius, float height, int slices)
{
	if (!pView) return;

	const float PI = 3.1415926535f;
	float angle_step = 2.0f * PI / slices;

	// Draw the wall
	pView->glBegin(GL_QUAD_STRIP);
	for (int i = 0; i <= slices; i++) {
		float angle = i * angle_step;
		float x = radius * cos(angle);
		float z = radius * sin(angle);
		pView->glNormal3f(x / radius, 0.0f, z / radius);
		pView->glVertex3f(x, height / 2.0f, z);
		pView->glVertex3f(x, -height / 2.0f, z);
	}
	pView->glEnd();

	// Draw the top cap
	pView->glBegin(GL_TRIANGLE_FAN);
	pView->glNormal3f(0.0f, 1.0f, 0.0f);
	pView->glVertex3f(0.0f, height / 2.0f, 0.0f);
	for (int i = 0; i <= slices; i++) {
		float angle = i * angle_step;
		pView->glVertex3f(radius * cos(angle), height / 2.0f, radius * sin(angle));
	}
	pView->glEnd();

	// Draw the bottom cap
	pView->glBegin(GL_TRIANGLE_FAN);
	pView->glNormal3f(0.0f, -1.0f, 0.0f);
	pView->glVertex3f(0.0f, -height / 2.0f, 0.0f);
	for (int i = slices; i >= 0; i--) {
		float angle = i * angle_step;
		pView->glVertex3f(radius * cos(angle), -height / 2.0f, radius * sin(angle));
	}
	pView->glEnd();
}

void DrawRing(IOpenGLView* pView, float innerRadius, float outerRadius, float height, int slices)
{
	if (!pView) return;

	const float PI = 3.1415926535f;
	float angle_step = 2.0f * PI / slices;

	// Draw the outer wall
	pView->glBegin(GL_QUAD_STRIP);
	for (int i = 0; i <= slices; i++) {
		float angle = i * angle_step;
		float x = outerRadius * cos(angle);
		float z = outerRadius * sin(angle);
		pView->glNormal3f(x / outerRadius, 0.0f, z / outerRadius); // Normal points outwards
		pView->glVertex3f(x, height / 2.0f, z);
		pView->glVertex3f(x, -height / 2.0f, z);
	}
	pView->glEnd();

	// Draw the inner wall
	pView->glBegin(GL_QUAD_STRIP);
	for (int i = 0; i <= slices; i++) {
		float angle = i * angle_step;
		float x = innerRadius * cos(angle);
		float z = innerRadius * sin(angle);
		pView->glNormal3f(-x / innerRadius, 0.0f, -z / innerRadius); // Normal points inwards
		pView->glVertex3f(x, -height / 2.0f, z);
		pView->glVertex3f(x, height / 2.0f, z);
	}
	pView->glEnd();

	// Draw the top cap
	pView->glBegin(GL_QUAD_STRIP);
	pView->glNormal3f(0.0f, 1.0f, 0.0f);
	for (int i = 0; i <= slices; i++) {
		float angle = i * angle_step;
		pView->glVertex3f(outerRadius * cos(angle), height / 2.0f, outerRadius * sin(angle));
		pView->glVertex3f(innerRadius * cos(angle), height / 2.0f, innerRadius * sin(angle));
	}
	pView->glEnd();

	// Draw the bottom cap
	pView->glBegin(GL_QUAD_STRIP);
	pView->glNormal3f(0.0f, -1.0f, 0.0f);
	for (int i = 0; i <= slices; i++) {
		float angle = i * angle_step;
		pView->glVertex3f(innerRadius * cos(angle), -height / 2.0f, innerRadius * sin(angle));
		pView->glVertex3f(outerRadius * cos(angle), -height / 2.0f, outerRadius * sin(angle));
	}
	pView->glEnd();
}


void CObjectDemoExperiment::InitializePegs()
{
	m_pegs.clear();
	m_pegs.resize(3);

	for (int i = 0; i < 3; ++i) {
		while (!m_pegs[i].empty()) {
			m_pegs[i].pop();
		}
	}

	for (int i = m_ObjectPattern.m_nNumberOfRings; i > 0; --i)
	{
		m_pegs[0].push(i);
	}
}

void CObjectDemoExperiment::TowerOfHanoiSolver(int n, int from_peg, int to_peg, int aux_peg)
{
	if (n == 0) {
		return;
	}
	TowerOfHanoiSolver(n - 1, from_peg, aux_peg, to_peg);
	m_moves.push_back({ from_peg, to_peg });
	TowerOfHanoiSolver(n - 1, aux_peg, to_peg, from_peg);
}

void CObjectDemoExperiment::DrawTowerOfHanoi()
{
	DrawTowerOfHanoi(-1, 0, 0, 0);
}

void CObjectDemoExperiment::DrawTowerOfHanoi(int movingDisk, float mx, float my, float mz)
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR)) return;

	ApplicationView->InitializeEnvironment(TRUE);
	ApplicationView->BeginGraphicsCommands();

	ApplicationView->SetBkgColor(GetRValue(m_ObjectPattern.m_Color) / 255.0f, GetGValue(m_ObjectPattern.m_Color) / 255.0f, GetBValue(m_ObjectPattern.m_Color) / 255.0f, 1.0f);

	// The application framework seems to require rebuilding its display list every frame for animation.
	HR = ApplicationView->StartNewDisplayList();
	if (HR == E_FAIL)
	{
		ApplicationView->EndGraphicsCommands();
		ApplicationView->Refresh();
		return;
	}

	CComPtr<IOpenGLView> OpenGLView;
	HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR))
	{
		ApplicationView->EndNewDisplayList();
		ApplicationView->EndGraphicsCommands();
		ApplicationView->Refresh();
		return;
	}

	// --- Model Parameters ---
	float baseWidth = 2.5f;
	float baseHeight = 0.1f;
	float pegHeight = 1.0f;
	float pegRadius = 0.02f;
	float pegSpacing = 0.8f;
	float diskThickness = 0.08f;
	float maxDiskRadius = 0.35f;
	float minDiskRadius = 0.1f;
	float pegPositions[3] = { -pegSpacing, 0.0f, pegSpacing };
	float diskHoleRadius = pegRadius + 0.02f;


	// --- Draw Base ---
	OpenGLView->glPushMatrix();
	OpenGLView->glTranslatef(0.0f, -pegHeight / 2.0f - baseHeight / 2.0f, 0.0f);
	OpenGLView->glColor3f(0.4f, 0.2f, 0.1f);
	DrawCylinder(OpenGLView, baseWidth / 2.0f, baseHeight, 32);
	OpenGLView->glPopMatrix();

	// --- Draw Pegs ---
	for (int i = 0; i < 3; i++)
	{
		OpenGLView->glPushMatrix();
		OpenGLView->glTranslatef(pegPositions[i], 0.0f, 0.0f);
		OpenGLView->glColor3f(0.8f, 0.7f, 0.6f);
		DrawCylinder(OpenGLView, pegRadius, pegHeight, 16);
		OpenGLView->glPopMatrix();
	}

	// --- Draw Disks based on m_pegs state ---
	for (int i = 0; i < 3; ++i)
	{
		if (i < m_pegs.size())
		{
			std::stack<int> tempPeg = m_pegs[i];
			std::vector<int> disksOnPeg;
			while (!tempPeg.empty())
			{
				disksOnPeg.push_back(tempPeg.top());
				tempPeg.pop();
			}
			std::reverse(disksOnPeg.begin(), disksOnPeg.end());

			for (size_t j = 0; j < disksOnPeg.size(); ++j)
			{
				int diskID = disksOnPeg[j];

				float currentDiskY = -pegHeight / 2.0f + diskThickness / 2.0f + j * diskThickness;

				if (diskID == movingDisk) continue;

				float radius = minDiskRadius + (maxDiskRadius - minDiskRadius) * ((float)(diskID - 1) / (m_ObjectPattern.m_nNumberOfRings - 1));

				OpenGLView->glPushMatrix();
				OpenGLView->glTranslatef(pegPositions[i], currentDiskY, 0.0f);

				float r = (float)((diskID * 37) % 256) / 255.0f;
				float g = (float)((diskID * 59) % 256) / 255.0f;
				float b = (float)((diskID * 73) % 256) / 255.0f;
				OpenGLView->glColor3f(r, g, b);

				DrawRing(OpenGLView, diskHoleRadius, radius, diskThickness, 32);
				OpenGLView->glPopMatrix();
			}
		}
	}

	// --- Draw the moving disk ---
	if (movingDisk != -1)
	{
		float radius = minDiskRadius + (maxDiskRadius - minDiskRadius) * ((float)(movingDisk - 1) / (m_ObjectPattern.m_nNumberOfRings - 1));

		OpenGLView->glPushMatrix();
		OpenGLView->glTranslatef(mx, my, mz);

		float r = (float)((movingDisk * 37) % 256) / 255.0f;
		float g = (float)((movingDisk * 59) % 256) / 255.0f;
		float b = (float)((movingDisk * 73) % 256) / 255.0f;
		OpenGLView->glColor3f(r, g, b);

		DrawRing(OpenGLView, diskHoleRadius, radius, diskThickness, 32);
		OpenGLView->glPopMatrix();
	}

	ApplicationView->EndNewDisplayList();
	ApplicationView->EndGraphicsCommands();
	ApplicationView->Refresh();
}

void CObjectDemoExperiment::StartSimulation(BSTR ExperimentGroup, BSTR ExperimentName)
{
	if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_TOWEROFHANOI)
	{
		StartTowerOfHanoiSimulation();
	}
	else if (CString(ExperimentGroup) == OBJECT_3D_TREE_ROOT_TITLE && CString(ExperimentName) == OBJECT_3D_TREE_LEAF_PATTERN_TITLE)
	{
		StartObjectSimulation();
	}
}

void CObjectDemoExperiment::StartTowerOfHanoiSimulation()
{
	m_pManager->SetStatusBarMessage(_T("Simulation Started..."), TRUE);
	InitializePegs();
	m_moves.clear();

	TowerOfHanoiSolver(m_ObjectPattern.m_nNumberOfRings, 0, 2, 1);

	for (const auto& move : m_moves)
	{
		// Process application messages to keep the UI responsive
		// and to detect if the user has clicked the Stop button.
		MSG msg;
		while (::PeekMessage(&msg, NULL, 0, 0, PM_NOREMOVE))
		{
			if (!AfxGetApp()->PumpMessage())
			{
				m_pManager->SetSimulationStatus(FALSE);
				return;
			}
		}

		if (!m_pManager->m_bSimulationActive)
		{
			break; // Simulation was stopped by the user
		}
		AnimateMove(move.from, move.to);
	}

	if (m_pManager->m_bSimulationActive)
	{
		m_pManager->SetSimulationStatus(FALSE);
		m_pManager->SetStatusBarMessage(_T("Simulation Finished."), TRUE);
	}

	// Visually reset the board to the initial state
	InitializePegs();
	DrawTowerOfHanoi();
}


void CObjectDemoExperiment::AnimateMove(int fromPeg, int toPeg)
{
	if (fromPeg < 0 || fromPeg >= (int)m_pegs.size() || m_pegs[fromPeg].empty()) return;

	int diskID = m_pegs[fromPeg].top();

	// Base duration of each animation phase (lift, move, lower) in milliseconds.
	const float baseDuration = 500.0f;
	DWORD animationDuration = static_cast<DWORD>(baseDuration / m_ObjectPattern.m_dAnimationSpeed);
	if (animationDuration < 1) animationDuration = 1;

	float liftHeight = 0.8f;
	float pegHeight = 1.0f;
	float pegSpacing = 0.8f;
	float diskThickness = 0.08f;
	float pegPositions[3] = { -pegSpacing, 0.0f, pegSpacing };

	float startX = pegPositions[fromPeg];
	float startY = -pegHeight / 2.0f + diskThickness / 2.0f + (m_pegs[fromPeg].size() - 1) * diskThickness;

	float endX = pegPositions[toPeg];
	float endY = -pegHeight / 2.0f + diskThickness / 2.0f + m_pegs[toPeg].size() * diskThickness;

	m_pegs[fromPeg].pop();

	DWORD startTime;
	float progress;

	// Lambda function to check for the stop signal while keeping the UI responsive.
	auto CheckStop = [&]() {
		MSG msg;
		while (::PeekMessage(&msg, NULL, 0, 0, PM_NOREMOVE)) {
			if (!AfxGetApp()->PumpMessage()) {
				m_pManager->SetSimulationStatus(FALSE);
			}
		}
		return !m_pManager->m_bSimulationActive;
	};

	// 1. Lift disk up
	startTime = GetTickCount();
	do {
		if (CheckStop()) { m_pegs[fromPeg].push(diskID); return; }
		DWORD elapsed = GetTickCount() - startTime;
		progress = (float)elapsed / animationDuration;
		if (progress > 1.0f) progress = 1.0f;

		float currentY = startY + (liftHeight - startY) * progress;
		DrawTowerOfHanoi(diskID, startX, currentY, 0.0f);

	} while (progress < 1.0f);

	// 2. Move disk across
	startTime = GetTickCount();
	do {
		if (CheckStop()) { m_pegs[fromPeg].push(diskID); return; }
		DWORD elapsed = GetTickCount() - startTime;
		progress = (float)elapsed / animationDuration;
		if (progress > 1.0f) progress = 1.0f;

		float currentX = startX + (endX - startX) * progress;
		DrawTowerOfHanoi(diskID, currentX, liftHeight, 0.0f);

	} while (progress < 1.0f);

	// 3. Lower disk down
	startTime = GetTickCount();
	do {
		if (CheckStop()) { m_pegs[fromPeg].push(diskID); return; }
		DWORD elapsed = GetTickCount() - startTime;
		progress = (float)elapsed / animationDuration;
		if (progress > 1.0f) progress = 1.0f;

		float currentY = liftHeight + (endY - liftHeight) * progress;
		DrawTowerOfHanoi(diskID, endX, currentY, 0.0f);

	} while (progress < 1.0f);


	m_pegs[toPeg].push(diskID);
	DrawTowerOfHanoi(); // Final draw to ensure it's in the correct position
}


void CObjectDemoExperiment::StartObjectSimulation()
{
	//AfxMessageBox(_T("CPlusTwoPhysicsExperiment::StartObjectSimulation()"));
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR))
	{
		return;
	}
	float Angle = (float)0.0, x = (float)0.0, y = (float)0.0, z = (float)0.0;
	int i = 0; //Indicate Random Movment after each iteration
	while (m_pManager->m_bSimulationActive)
	{
		ApplicationView->BeginGraphicsCommands();

		if (m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_ROTATE)
		{
			//Rotate the object with respect to Y Axis
			x = (float)0.1, y = (float)1.0, z = (float)0.1;

		}
		else if (m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_RANDOM)
		{
			//Simulate Random Rotation
			switch (i)
			{
			case 0:
				x = (float)1.0, y = (float)0.1, z = (float)0.1;

				break;
			case 1:
				x = (float)0.1, y = (float)1.0, z = (float)0.1;

				break;
			case 2:
				x = (float)0.1, y = (float)0.1, z = (float)1.0;

				break;

			}
			i = rand() % 3;
		}

		if (!m_pManager->m_b3DMode)
		{
			//Set the x y Rotation point to zero for two d view
			x = 0;
			y = 0;
		}
		//Rotate the Object with the specified angle
		ApplicationView->RotateObject(Angle, x, y, z);
		ApplicationView->EndGraphicsCommands();
		ApplicationView->Refresh();
		//Process the Results
		OnNextSimulationPoint(Angle, x, y, z);

		Angle = Angle + 5;
		if (Angle > 360)
		{
			Angle = 0;
		}
		Sleep(m_ObjectPattern.m_lSimulationInterval); //Sleep for 500 Milli seconds
	}
	m_pManager->SetSimulationStatus(FALSE);
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

	if (iArraySize <2)
	{
		return;
	}

	COleSafeArray			saX;
	COleSafeArray			saY;
	COleSafeArray			saZ;

	SAFEARRAYBOUND			sabX[2];
	SAFEARRAYBOUND			sabY[2];
	SAFEARRAYBOUND			sabZ[2];

	sabX[0].cElements = iArraySize;// give this exact
	sabX[1].cElements = 2; //number of columns + 1 (because the first column is where we put 
						   // the row labels - ie in 1.1, 2.1, 3.1, 4,1 etc
	sabX[0].lLbound = sabX[1].lLbound = 1;

	saX.Create(VT_R8, 2, sabX);

	//
	sabY[0].cElements = iArraySize;// give this exact
	sabY[1].cElements = 2; //number of columns + 1 (because the first column is where we put 
						   // the row labels - ie in 1.1, 2.1, 3.1, 4,1 etc
	sabY[0].lLbound = sabY[1].lLbound = 1;

	saY.Create(VT_R8, 2, sabY);

	//

	sabZ[0].cElements = iArraySize;// give this exact
	sabZ[1].cElements = 2; //number of columns + 1 (because the first column is where we put 
						   // the row labels - ie in 1.1, 2.1, 3.1, 4,1 etc
	sabZ[0].lLbound = sabZ[1].lLbound = 1;

	saZ.Create(VT_R8, 2, sabZ);

	//

	long index[2] = { 0,0 }; //a 2D graph needs a 2D array as index array

	for (int i = 0; i < iArraySize; i++)
	{
		CGraphPoints* pInfo = (CGraphPoints*)m_PlotInfoArray.GetAt(i);
		index[0] = i + 1;
		index[1] = 1;
		double pValue = pInfo->m_Angle;
		saX.PutElement(index, &pValue);
		saY.PutElement(index, &pValue);
		saZ.PutElement(index, &pValue);

		//Now plot the other Y Value for each data
		index[1] = 2;
		pValue = pInfo->m_x; //set the X 
		saX.PutElement(index, &pValue);

		pValue = pInfo->m_y; //set the Y
		saY.PutElement(index, &pValue);

		pValue = pInfo->m_z; //set the Z
		saZ.PutElement(index, &pValue);

	}
	//Refresh Graph on Only 5th Data entry
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


void CObjectDemoExperiment::DrawClock()
{
	//Draw using ApplicationView Interface
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