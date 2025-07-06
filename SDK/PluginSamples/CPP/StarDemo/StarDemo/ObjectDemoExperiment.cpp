// PlusTwoPhysicsExperiment.cpp : implementation file
//

#include "stdafx.h"
#include "ObjectDemoExperiment.h"
#include "StarDemoSimulation.h"
#include "AddinSimulationManager.h"
#include "PropSliderCtrl.h"

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

		if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_CUBE) {
			m_ObjectPattern.m_strAvailablePatterns = _T("Rotate,Random Movement");
		}
		else if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_STAR) {
			m_ObjectPattern.m_strAvailablePatterns = _T("Rotate,Random Movement,Glow");
		}

		PropertyWindow->AddPropertyItemsAsString(strGroupName.AllocSysString(), OBJECT_SIMULATION_PATTERN_TITLE, m_ObjectPattern.m_strAvailablePatterns.AllocSysString(), m_ObjectPattern.m_strSimulationPattern.AllocSysString(), _T("Select the Simulation Pattern"), FALSE);
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

		if (CString(PropertyName) == OBJECT_COLOR_TITLE)
		{
			m_ObjectPattern.m_UserSelectedColor = m_ObjectPattern.m_Color; // save latest user color
		}

		if (CString(PropertyName) == OBJECT_TYPE_TITLE)
		{
			m_ObjectPattern.m_strSimulationPattern = _T("Rotate");// set to default
			m_ObjectPattern.m_Color = RGB(0, 0, 255);
			m_ObjectPattern.m_lSimulationInterval = 100;
			m_ObjectPattern.m_UserSelectedColor = m_ObjectPattern.m_Color; // save latest user color

			ShowObjectProperties();
			return;
		}
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
	else if (m_ObjectPattern.m_strObjectType == OBJECT_TYPE_STAR)
	{
		DrawStar();
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

	//Set the Inner Sphere Color


	ApplicationView->EndNewDisplayList();
	ApplicationView->EndGraphicsCommands();
	ApplicationView->Refresh();

}
void CObjectDemoExperiment::DrawStar() {
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

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);			//gold colour for the faces

		OpenGLView->glBegin(GL_POLYGON);					//back pentagon face of star
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.0f);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.0f);

		OpenGLView->glColor3f(1.0f, 0.910f, 0.5f);			//shading

		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.0f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_POLYGON);					//front pentagon face of star
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.4703f);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.4703f);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.4703f);

		OpenGLView->glColor3f(1.0f, 0.910f, 0.5f);

		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.4703f);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.4703f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);					//top right vertex of the star
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.4703f);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.4703f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);		//shading

		OpenGLView->glVertex3f(0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.4703f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.0f);
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.4703f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.0f);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.0f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);						//top left vertex of star
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.4703f);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.4703f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(-0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.4703f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(-0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.0f);
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.4703f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(-0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.0f);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.0f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(-0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);						//bottom right vertex of star
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.4703f);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.4703f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.4703f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.0f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.4703f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);							//bottom left vertex of star
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.4703f);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.4703f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(-0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.4703f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(-0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.0f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(-0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.4703f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(-0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);							//bottom most vertex of star
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.4703f);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.4703f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(0.0f, -0.7309f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.4703f);


		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);
		OpenGLView->glVertex3f(0.0f, -0.7309f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.4703f);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.0f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(0.0f, -0.7309f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glColor3f(1.0f, 0.843f, 0.0f);

		OpenGLView->glBegin(GL_TRIANGLES);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.0f);

		OpenGLView->glColor3f(0.396f, 0.263f, 0.129f);

		OpenGLView->glVertex3f(0.0f, -0.7309f, 0.2351f);
		OpenGLView->glEnd();


		OpenGLView->glColor3f(0.0f, 0.0f, 0.0f);			//black colour for drawing borders
		OpenGLView->glLineWidth(2.0f);

		OpenGLView->glBegin(GL_LINE_LOOP);					//border of back pentagon face
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.0f);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.0f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);					//border of front pentagon face
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.4703f);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.4703f);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.4703f);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.4703f);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.4703f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);						//border of top right vertex
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.4703f);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.4703f);
		OpenGLView->glVertex3f(0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.4703f);
		OpenGLView->glVertex3f(0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.0f);
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.4703f);
		OpenGLView->glVertex3f(0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.0f);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();


		OpenGLView->glBegin(GL_LINE_LOOP);						//border of top left vertex
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.4703f);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.4703f);
		OpenGLView->glVertex3f(-0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.4703f);
		OpenGLView->glVertex3f(-0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.0f);
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.4703f);
		OpenGLView->glVertex3f(-0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(0.0f, 0.4f, 0.0f);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(-0.3654f, 0.5626f, 0.2351f);
		OpenGLView->glEnd();


		OpenGLView->glBegin(GL_LINE_LOOP);							//border of bottom right vertex
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.4703f);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.4703f);
		OpenGLView->glVertex3f(0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.4703f);
		OpenGLView->glVertex3f(0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.4703f);
		OpenGLView->glVertex3f(0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();


		OpenGLView->glBegin(GL_LINE_LOOP);							//border of bottom left vertex
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.4703f);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.4703f);
		OpenGLView->glVertex3f(-0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.4703f);
		OpenGLView->glVertex3f(-0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(-0.3804f, 0.1236f, 0.0f);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(-0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.4703f);
		OpenGLView->glVertex3f(-0.6951f, -0.2259f, 0.2351f);
		OpenGLView->glEnd();


		OpenGLView->glBegin(GL_LINE_LOOP);							//border of bottom most vertex
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.4703f);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.4703f);
		OpenGLView->glVertex3f(0.0f, -0.7309f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.4703f);
		OpenGLView->glVertex3f(0.0f, -0.7309f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.4703f);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(0.0f, -0.7309f, 0.2351f);
		OpenGLView->glEnd();

		OpenGLView->glBegin(GL_LINE_LOOP);
		OpenGLView->glVertex3f(0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(-0.2351f, -0.3236f, 0.0f);
		OpenGLView->glVertex3f(0.0f, -0.7309f, 0.2351f);
		OpenGLView->glEnd();


		ApplicationView->EndNewDisplayList();
		ApplicationView->EndGraphicsCommands();
		ApplicationView->Refresh();
	}
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
	//AfxMessageBox(_T("CPlusTwoPhysicsExperiment::StartObjectSimulation()"));
	m_pManager->SetSimulationStatus(TRUE);
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
		else if (m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_GLOW)
		{
			static bool bGlowToggle = false;

			if (bGlowToggle)
				m_ObjectPattern.m_Color = RGB(255, 215, 0); // Glow gold
			else
				m_ObjectPattern.m_Color = m_ObjectPattern.m_UserSelectedColor; // Always latest user bg color

			bGlowToggle = !bGlowToggle;

			ApplicationView->BeginGraphicsCommands();
			ApplicationView->SetBkgColor(
				GetRValue(m_ObjectPattern.m_Color) / 255.0f,
				GetGValue(m_ObjectPattern.m_Color) / 255.0f,
				GetBValue(m_ObjectPattern.m_Color) / 255.0f,
				1.0f
			);
			ApplicationView->EndGraphicsCommands();
			ApplicationView->Refresh();

			Sleep(m_ObjectPattern.m_lSimulationInterval);
			continue;
		}
		if (!m_pManager->m_b3DMode)
		{
			//Set the x y Rotation point to zero for two d view
			x = 0;
			y = 0;
		}
		//Rotate the Object with the specified angle
		if (m_ObjectPattern.m_strSimulationPattern != OBJECT_PATTERN_TYPE_GLOW)
		{
			ApplicationView->RotateObject(Angle, x, y, z);
		}

		ApplicationView->EndGraphicsCommands();
		ApplicationView->Refresh();

		if (m_ObjectPattern.m_strSimulationPattern != OBJECT_PATTERN_TYPE_GLOW)
		{
			OnNextSimulationPoint(Angle, x, y, z); //Process the Results
		}

		Angle = Angle + 5;
		if (Angle > 360)
		{
			Angle = 0;
		}
		Sleep(m_ObjectPattern.m_lSimulationInterval); //Sleep for 500 Milli seconds
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