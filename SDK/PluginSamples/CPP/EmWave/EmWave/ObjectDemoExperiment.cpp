
#include "stdafx.h"
#include "ObjectDemoExperiment.h"
#include "EmWaveSimulation.h"
#include "AddinSimulationManager.h"
#include "PropSliderCtrl.h"
#define _USE_MATH_DEFINES
#include <cmath>

// CPlusTwoPhysicsExperiment

using namespace ATL;

CObjectDemoExperiment::CObjectDemoExperiment(CAddinSimulationManager* pManager)

{

	m_pManager = pManager;
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

#if FALSE //Will Implement this Later

	ExperimentTreeView->AddExperiment(SessionID, CString(MECHANICS_TREE_ROOT_TITLE).AllocSysString(), CString(MECHANICS_TREE_SIMPLE_PENDULUM_TITLE).AllocSysString());
	ExperimentTreeView->AddExperiment(SessionID, CString(MECHANICS_TREE_ROOT_TITLE).AllocSysString(), CString(MECHANICS_TREE_PROJECTILE_MOTION_TITLE).AllocSysString());
	ExperimentTreeView->AddExperiment(SessionID, CString(MECHANICS_TREE_ROOT_TITLE).AllocSysString(), CString(MECHANICS_TREE_PLANETORY_MOTION_TITLE).AllocSysString());

#endif //Will Implement this Later
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
	else
	{
	}
}


// CPlusTwoPhysicsExperiment member functions


void CObjectDemoExperiment::ShowObjectProperties()

{

#if TRUE

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

#else //This shows all the property Options // But some methods has Bugs

	CComPtr<IPropertyWindow> PropertyWindow;
	HRESULT HR = PropertyWindow.CoCreateInstance(CLSID_PropertyWindow);
	CString strGroupName = _T("");
	if (SUCCEEDED(HR))

	{

		PropertyWindow->RemoveAll();
		strGroupName = _T("Custom Group");
		PropertyWindow->AddPropertyGroup(strGroupName.AllocSysString());
		PropertyWindow->AddFilePathItem(strGroupName.AllocSysString(), _T("File Path"), _T("C:\\Test\\"), TRUE, _T("Icon Files(*.ico)|*.ico|All Files(*.*)|*.*||"), _T("ico"), _T("Select the File Path"));
		PropertyWindow->AddFilePathItem(strGroupName.AllocSysString(), _T("Folder Path"), _T("D:\\Test\\"), FALSE, _T(""), _T(""), _T("Select the Folder Path"));
		PropertyWindow->AddColorPropertyItem(strGroupName.AllocSysString(), _T("Select Color"), RGB(255, 0, 0), _T("Select the Color"));
		VARIANT DefaultValue, AddParam1, AddParam2, AddParam3, AddParam4;
		DefaultValue.vt = VT_BSTR;
		DefaultValue.bstrVal = _T("C:\\");
		AddParam1.vt = VT_BSTR;
		AddParam2.vt = VT_BSTR;
		AddParam3.vt = VT_BSTR;
		AddParam4.vt = VT_BSTR;

		PropertyWindow->AddHierarchyItem(_T("New Group"), _T("Sub Item Group1"), _T("Place"), _T("Enter Your Place"), NormalEdit, DefaultValue, AddParam1, AddParam2, AddParam3, AddParam4);
		PropertyWindow->AddHierarchyItem(_T("New Group"), _T("Sub Item Group1,Group 2"), _T("Name"), _T("Enter Your Name"), NormalEdit, DefaultValue, AddParam1, AddParam2, AddParam3, AddParam4);
		DefaultValue.bstrVal = _T("C:\\test.txt");

		AddParam1.bstrVal = _T("Icon Files(*.ico)|*.ico|All Files(*.*)|*.*||");
		AddParam2.bstrVal = _T("ico");
		PropertyWindow->AddHierarchyItem(_T("New Group"), _T("Sub Item Group1"), _T("File Path"), _T("Enter Your File Path"), FilePathEdit, DefaultValue, AddParam1, AddParam2, AddParam3, AddParam4);
		DefaultValue.bstrVal = _T("C:\\");
		PropertyWindow->AddHierarchyItem(_T("New Group"), _T("Sub Item Group1,Group 2"), _T("Folder Path"), _T("Enter Folder Path"), FolderPathEdit, DefaultValue, AddParam1, AddParam2, AddParam3, AddParam4);
		DefaultValue.vt = VT_UI4;
		DefaultValue.llVal = RGB(0, 255, 0);
		PropertyWindow->AddHierarchyItem(_T("New Group"), _T("Sub Item Group1"), _T("Default Color"), _T("Enter Your File Path"), ColorEdit, DefaultValue, AddParam1, AddParam2, AddParam3, AddParam4);
		DefaultValue.vt = VT_BSTR;

		DefaultValue.bstrVal = _T("Petrol");

		AddParam1.vt = VT_BSTR;

		AddParam1.bstrVal = _T("Diesel,Gas,Petrol,Electric");

		AddParam2.vt = VT_I4;

		AddParam2.llVal = FALSE;

		PropertyWindow->AddHierarchyItem(_T("New Group"), _T("Sub Item Group1,Group 2"), _T("Select Engine"), _T("Select Your Engines"), ComboEdit, DefaultValue, AddParam1, AddParam2, AddParam3, AddParam4);

		AddParam2.llVal = TRUE;

		PropertyWindow->AddHierarchyItem(_T("New Group"), _T("Sub Item Group1"), _T("Select Engine"), _T("Select The latest Engines"), ComboReadOnly, DefaultValue, AddParam1, AddParam2, AddParam3, AddParam4);



		CSliderProp* pProp = new CSliderProp(_T("Range Values"), 10, _T("Select the Range Values"));

		VARIANT VarControl;

		VarControl.vt = VT_BYREF;

		VarControl.byref = pProp;

		PropertyWindow->AddCustomPropertyItem(_T("New Group"), VarControl);

		PropertyWindow->EnableHeaderCtrl(FALSE);

		PropertyWindow->EnableDescriptionArea(TRUE);

		PropertyWindow->SetVSDotNetLook(TRUE);

		PropertyWindow->MarkModifiedProperties(TRUE, TRUE);

	}

	//This section demonstrates Other property Controls which is present in this Framework

#endif


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



	//if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_CUBE)

	//{

	//DrawCube();

	//}

	//else if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_BALL)

	//{

	//DrawBall();

	//}

	//else if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_PYRAMID)

	//{

	//DrawPyramid();

	//}

	//else if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_AEROPLANE)

	//{

	//DrawAeroplane();

	//}

	//else if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_CLOCK)

	//{

	//DrawClock();

	//}

	if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_PENDULUM)

	{

		DrawPendulum();

	}
	else if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_EMWAVE)

		{

		DrawEmWave();

		}

}



void CObjectDemoExperiment::DrawPendulum()

{

	CComPtr<IApplicationView> ApplicationView;

	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);

	if (FAILED(HR))

	{

		return;

	}



	ApplicationView->InitializeEnvironment(TRUE);

	ApplicationView->BeginGraphicsCommands();

	// Background color

	ApplicationView->SetBkgColor(

		GetRValue(m_ObjectPattern.m_Color) / 255.0f,

		GetGValue(m_ObjectPattern.m_Color) / 255.0f,

		GetBValue(m_ObjectPattern.m_Color) / 255.0f,

		1.0f

	);



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



	// Pendulum parameters

	float rodLength = 2.0f;

	float bobRadius = 0.2f;

	// Draw pivot (small cube)

	OpenGLView->glColor3f(0.7f, 0.7f, 0.7f);

	OpenGLView->glBegin(GL_QUADS);

	// A small cube around (0,0,0)

	float half = 0.1f;

	// Front face

	OpenGLView->glVertex3f(-half, half, half);

	OpenGLView->glVertex3f(-half, -half, half);

	OpenGLView->glVertex3f(half, -half, half);

	OpenGLView->glVertex3f(half, half, half);

	// Back face

	OpenGLView->glVertex3f(-half, half, -half);

	OpenGLView->glVertex3f(-half, -half, -half);

	OpenGLView->glVertex3f(half, -half, -half);

	OpenGLView->glVertex3f(half, half, -half);

	// Left face

	OpenGLView->glVertex3f(-half, half, half);

	OpenGLView->glVertex3f(-half, -half, half);

	OpenGLView->glVertex3f(-half, -half, -half);

	OpenGLView->glVertex3f(-half, half, -half);

	// Right face

	OpenGLView->glVertex3f(half, half, half);

	OpenGLView->glVertex3f(half, -half, half);

	OpenGLView->glVertex3f(half, -half, -half);

	OpenGLView->glVertex3f(half, half, -half);

	// Top face

	OpenGLView->glVertex3f(-half, half, half);

	OpenGLView->glVertex3f(half, half, half);

	OpenGLView->glVertex3f(half, half, -half);

	OpenGLView->glVertex3f(-half, half, -half);

	// Bottom face

	OpenGLView->glVertex3f(-half, -half, half);

	OpenGLView->glVertex3f(half, -half, half);

	OpenGLView->glVertex3f(half, -half, -half);

	OpenGLView->glVertex3f(-half, -half, -half);

	OpenGLView->glEnd();



	// Draw rod (as a thin line)

	OpenGLView->glColor3f(0.0f, 0.0f, 0.0f);

	OpenGLView->glBegin(GL_LINES);

	OpenGLView->glVertex3f(0.0f, 0.0f, 0.0f);

	OpenGLView->glVertex3f(0.0f, -rodLength, 0.0f);

	OpenGLView->glEnd();



	// Draw bob (simple polygon approximation of sphere)

	int slices = 20, stacks = 20;

	float theta, phi;

	for (int i = 0; i < stacks; i++)

	{

		float lat0 = M_PI * (-0.5 + (float)(i) / stacks);

		float z0 = sin(lat0);

		float zr0 = cos(lat0);

		float lat1 = M_PI * (-0.5 + (float)(i + 1) / stacks);

		float z1 = sin(lat1);

		float zr1 = cos(lat1);

		OpenGLView->glColor3f(0.8f, 0.1f, 0.1f);

		OpenGLView->glBegin(GL_QUAD_STRIP);

		for (int j = 0; j <= slices; j++)

		{

			theta = 2 * M_PI * (float)(j) / slices;

			float x = cos(theta);

			float y = sin(theta);



			OpenGLView->glVertex3f(bobRadius * x * zr0, -rodLength + bobRadius * z0, bobRadius * y * zr0);

			OpenGLView->glVertex3f(bobRadius * x * zr1, -rodLength + bobRadius * z1, bobRadius * y * zr1);

		}

		OpenGLView->glEnd();

	}



	ApplicationView->EndNewDisplayList();

	ApplicationView->EndGraphicsCommands();

	ApplicationView->Refresh();

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


	//Set the Inner Sphere Color



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

	OpenGLView->glVertex3f(-1.0f, -1.0f, -1.0f); // Note: we're starting from the last point

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

void CObjectDemoExperiment::StartSimulation(BSTR ExperimentGroup, BSTR ExperimentName)

{

	if (CString(ExperimentGroup) == OBJECT_3D_TREE_ROOT_TITLE && CString(ExperimentName) == OBJECT_3D_TREE_LEAF_PATTERN_TITLE)

	{

		StartObjectSimulation();

	}

	else

	{

	}

}
void CObjectDemoExperiment::StartObjectSimulation()
{
	m_pManager->SetSimulationStatus(TRUE);

	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR)) return;

	// reset
	float Angle = 0.0f;
	float x = 0.0f, y = 0.0f, z = 0.0f;
	int i = 0;

	while (m_pManager->m_bSimulationActive)
	{
		if (m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_ROTATE)
		{
			ApplicationView->BeginGraphicsCommands();

			// rotate about Y
			x = 0.1f; y = 1.0f; z = 0.1f;
			Angle += 5.0f;
			if (Angle > 360.0f) Angle = 0.0f;

			if (!m_pManager->m_b3DMode) { x = 0.0f; y = 0.0f; }
			ApplicationView->RotateObject(Angle, x, y, z);

			ApplicationView->EndGraphicsCommands();
			ApplicationView->Refresh();
		}
		else if (m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_RANDOM)
		{
			ApplicationView->BeginGraphicsCommands();

			switch (i)
			{
			case 0: x = 1.0f; y = 0.1f; z = 0.1f; break;
			case 1: x = 0.1f; y = 1.0f; z = 0.1f; break;
			default: x = 0.1f; y = 0.1f; z = 1.0f; break;
			}
			i = rand() % 3;

			Angle += 5.0f;
			if (Angle > 360.0f) Angle = 0.0f;

			if (!m_pManager->m_b3DMode) { x = 0.0f; y = 0.0f; }
			ApplicationView->RotateObject(Angle, x, y, z);

			ApplicationView->EndGraphicsCommands();
			ApplicationView->Refresh();
		}
		else if (m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_SWING)
		{
			ApplicationView->BeginGraphicsCommands();

			static float time = 0.0f;
			const float amplitude = 30.0f;
			const float frequency = 0.5f;
			const float dt = 0.05f;

			Angle = amplitude * sinf(2.0f * 3.14159f * frequency * time);
			time += dt;

			x = 0.0f; y = 0.0f; z = 1.0f;

			if (!m_pManager->m_b3DMode) { x = 0.0f; y = 0.0f; }
			ApplicationView->RotateObject(Angle, x, y, z);

			ApplicationView->EndGraphicsCommands();
			ApplicationView->Refresh();
		}
		else if (m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_PROPAGATE)
		{
			// IMPORTANT:
			// No drawing via IApplicationView here (it has no DrawLine).
			// Just render the EM wave using the OpenGL-based routine.
			// DrawEmWave() already advances time internally and refreshes.
			DrawEmWave();

			// Don't rotate the object in propagate mode
			Angle = 0.0f; x = 0.0f; y = 0.0f; z = 0.0f;
		}

		OnNextSimulationPoint(Angle, x, y, z);
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

	if (iArraySize <2)

	{

		return;

	}

	COleSafeArray saX;

	COleSafeArray saY;

	COleSafeArray saZ;

	SAFEARRAYBOUND sabX[2];

	SAFEARRAYBOUND sabY[2];

	SAFEARRAYBOUND sabZ[2];


	sabX[0].cElements = iArraySize;// give this exact

	sabX[1].cElements = 2; //number of columns + 1 (because the first column is where we put

						   // the row labels - ie in 1.1, 2.1, 3.1, 4,1 etc

	sabX[0].lLbound = sabX[1].lLbound = 1;

	saX.Create(VT_R8, 2, sabX);

	sabY[0].cElements = iArraySize;// give this exact

	sabY[1].cElements = 2; //number of columns + 1 (because the first column is where we put

						   // the row labels - ie in 1.1, 2.1, 3.1, 4,1 etc

	sabY[0].lLbound = sabY[1].lLbound = 1;
	saY.Create(VT_R8, 2, sabY);

	sabZ[0].cElements = iArraySize;// give this exact

	sabZ[1].cElements = 2; //number of columns + 1 (because the first column is where we put

						   // the row labels - ie in 1.1, 2.1, 3.1, 4,1 etc

	sabZ[0].lLbound = sabZ[1].lLbound = 1;

	saZ.Create(VT_R8, 2, sabZ);

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

	//Refresh Graph on Only 10th Data entry

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
void CObjectDemoExperiment::DrawEmWave()
{
	CComPtr<IApplicationView> ApplicationView;
	HRESULT HR = ApplicationView.CoCreateInstance(CLSID_ApplicationView);
	if (FAILED(HR))
	{
		return;
	}

	ApplicationView->InitializeEnvironment(TRUE);
	ApplicationView->BeginGraphicsCommands();

	// Set the Background Color to black
	ApplicationView->SetBkgColor(0.0f, 0.0f, 0.0f, 1.0f);

	HR = ApplicationView->StartNewDisplayList();
	if (HR == E_FAIL)
	{
		return;
	}

	// Draw using the Native IOpenGLView Interface
	CComPtr<IOpenGLView> OpenGLView;
	HR = OpenGLView.CoCreateInstance(CLSID_OpenGLView);
	if (FAILED(HR))
	{
		return;
	}

	// --- Wave Parameters ---
	const float amplitude = 1.2f;
	const float length = 8.0f;       // Total length of the wave to be drawn
	const int num_cycles = 2;        // Number of full wavelengths to show
	const int segments = 200;        // Number of segments for smoothness
	const float lambda = length / num_cycles; // Wavelength
	const float k = (2.0f * M_PI) / lambda;   // Wavenumber

											  // --- Time-dependent phase ---
	static float t = 0.0f;     // persists across frames
	t += 0.3f;                // controls animation speed
	const float omega = 2.0f * M_PI * 0.7f;  // angular frequency (adjust for speed)

											 // Center the wave on the origin, propagating along the Z-axis
	float z_start = -length / 2.0f;
	float z_step = length / segments;

	// --- 1. Electric Field (Red, oscillating on Y-axis) ---
	OpenGLView->glColor3f(1.0f, 0.2f, 0.2f);

	// Draw the vertical E-field oscillations
	OpenGLView->glBegin(GL_LINES);
	for (int i = 0; i <= segments; i++)
	{
		float z = z_start + i * z_step;
		float y_val = amplitude * sinf(k * z - omega * t);
		OpenGLView->glVertex3f(0.0f, -y_val, z);
		OpenGLView->glVertex3f(0.0f, y_val, z);
	}
	OpenGLView->glEnd();

	// Draw top and bottom outlines of the E-field wave
	OpenGLView->glLineWidth(2.0f);
	OpenGLView->glBegin(GL_LINE_STRIP);
	for (int i = 0; i <= segments; i++)
	{
		float z = z_start + i * z_step;
		float y_val = amplitude * sinf(k * z - omega * t);
		OpenGLView->glVertex3f(0.0f, y_val, z);
	}
	OpenGLView->glEnd();

	OpenGLView->glBegin(GL_LINE_STRIP);
	for (int i = 0; i <= segments; i++)
	{
		float z = z_start + i * z_step;
		float y_val = amplitude * sinf(k * z - omega * t);
		OpenGLView->glVertex3f(0.0f, -y_val, z);
	}
	OpenGLView->glEnd();
	OpenGLView->glLineWidth(1.0f);

	// --- 2. Magnetic Field (Blue, oscillating on X-axis) ---
	OpenGLView->glColor3f(0.2f, 0.5f, 1.0f);

	// Draw ribbon-like B-field
	OpenGLView->glBegin(GL_TRIANGLE_STRIP);
	for (int i = 0; i <= segments; i++)
	{
		float z = z_start + i * z_step;
		float x_val = amplitude * sinf(k * z - omega * t);
		OpenGLView->glVertex3f(-x_val, 0.0f, z);
		OpenGLView->glVertex3f(x_val, 0.0f, z);
	}
	OpenGLView->glEnd();

	// --- 3. White dot at origin (charge source) ---
	OpenGLView->glPointSize(8.0f);
	OpenGLView->glColor3f(1.0f, 1.0f, 1.0f);
	OpenGLView->glBegin(GL_POINTS);
	OpenGLView->glVertex3f(0.0f, 0.0f, 0.0f);
	OpenGLView->glEnd();

	ApplicationView->EndNewDisplayList();
	ApplicationView->EndGraphicsCommands();
	ApplicationView->Refresh();
}
