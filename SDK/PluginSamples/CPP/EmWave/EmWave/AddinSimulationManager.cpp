// AddinSimulationManager.cpp : implementation file
//

#include "stdafx.h"
#include "EmWaveSimulation.h"
#include "AddinSimulationManager.h"


// CAddinSimulationManager

CAddinSimulationManager::CAddinSimulationManager(CEmWaveSimulation* pAddin)
{
	m_pAddin = pAddin;
	m_pObjectDemoExperiment = new CObjectDemoExperiment(this);
	m_bSimulationActive = FALSE;
		
}

CAddinSimulationManager::~CAddinSimulationManager()
{
	delete m_pObjectDemoExperiment;
	
}

void CAddinSimulationManager::LoadExperiments()
{
	if (!IsAddinExperimentSelected())
	{
		return;
	}

	m_pObjectDemoExperiment->LoadAllExperiments();

	
}

void CAddinSimulationManager::OnTreeNodeSelect(long SessionID, BSTR RootText, BSTR ExperimentGroup, BSTR ExperimentName)
{
	if (m_pAddin->m_lSessionID != SessionID)
	{
		return;
	}
	else
	{
		m_strRootText = RootText;
		m_strExperimentGroup = ExperimentGroup;
		m_strExperimentName = ExperimentName;
		SetStatusBarMessage(CString(RootText)+ CString(" | ") + CString(ExperimentGroup) + CString(" | ") + CString(ExperimentName), FALSE);
	}
	if (CString(RootText) == CPP_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES)
	{
		m_pObjectDemoExperiment->OnTreeNodeSelect(ExperimentGroup, ExperimentName);
	}
}
void CAddinSimulationManager::OnTreeNodeDblClick(long SessionID, BSTR RootText, BSTR ExperimentGroup, BSTR ExperimentName)
{
	if (m_pAddin->m_lSessionID != SessionID)
	{
		return;
	}
	else
	{
		m_strRootText = RootText;
		m_strExperimentGroup = ExperimentGroup;
		m_strExperimentName = ExperimentName;
		SetStatusBarMessage(CString(RootText) + CString(" | ") + CString(ExperimentGroup) + CString(" | ") + CString(ExperimentName), FALSE);
	}

	if (CString(RootText) == CPP_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES)
	{
		m_pObjectDemoExperiment->OnTreeNodeDblClick(ExperimentGroup, ExperimentName);
	}

}
void CAddinSimulationManager::OnReloadExperiment(long SessionID, BSTR RootText, BSTR ExperimentGroup, BSTR ExperimentName)
{
	if (m_pAddin->m_lSessionID != SessionID)
	{
		return;
	}
	if (CString(RootText) == CPP_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES)
	{
		m_pObjectDemoExperiment->OnReloadExperiment(ExperimentGroup, ExperimentName);
	}
	
}

void CAddinSimulationManager::OnStatusChange(long StatusCode, BSTR StatusDesc, BSTR AdditionalParam)
{
	if (StatusCode == 10000) //No Experiment Selected
	{
		
	}
	else if (StatusCode == 10001 && m_pAddin->m_lSessionID==_ttoi(CString(AdditionalParam))) //Experiment Changed 
	{
		m_pObjectDemoExperiment->LoadAllExperiments();
		m_pObjectDemoExperiment->OnReloadExperiment(m_strExperimentGroup.AllocSysString(), m_strExperimentName.AllocSysString());
				
		m_strSelectedExperiment = StatusDesc;
		LoadDefaultSelection();
	}
}
// CAddinSimulationManager member functions


void CAddinSimulationManager::ResetExpermentTree()
{
	CComPtr<IExperimentTreeView> ExperimentTreeView;
	HRESULT HR = ExperimentTreeView.CoCreateInstance(CLSID_ExperimentTreeView);
	if (FAILED(HR))
	{
		return;
	}
	long SessionID = m_pAddin->m_lSessionID;
	ExperimentTreeView->DeleteAllExperiments(SessionID);
	ExperimentTreeView->SetRootNodeName(CString(_T("Experiment List | Properties")).AllocSysString(), TRUE);
	ExperimentTreeView->Refresh();
}

void CAddinSimulationManager::OnError(long ErrorCode, BSTR ErrorDesc, BSTR AdditionalParam)
{

}
HRESULT CAddinSimulationManager::OnStartSimulation(BSTR ExperimentName)
{
	if (!IsAddinExperimentSelected())
	{
		CString strMessage;
		strMessage.Format(_T("Failed to Start %s. Please select Experimental Parameters before starting the Simulation Experiment.."), CString(ExperimentName));
		AfxMessageBox(strMessage);
		return E_FAIL;
	}

	LoadOtherSimulationOptions();
	ResetAllStatusWindows();
	m_pObjectDemoExperiment->StartSimulation(m_strExperimentGroup.AllocSysString(), m_strExperimentName.AllocSysString());

	return S_OK;
}

void CAddinSimulationManager::AddOperationStatus(CString strStatus, LONG bPostMessage)
{
	//((CMainFrame*)AfxGetMainWnd())->AddOperationStatus(strStatus, Mode);
	_tcscpy(m_strCurrentOutputStatusMessage, strStatus);

	//Send Operation Status to Window
	CComPtr<IMainWindow> MainWindow;
	HRESULT hr = MainWindow.CoCreateInstance(CLSID_MainWindow);
	if (SUCCEEDED(hr))
	{
		MainWindow->AddOperationStatus(strStatus.AllocSysString(), bPostMessage);
	}

}

HRESULT CAddinSimulationManager::OnStopSimulation(BSTR ExperimentName)
{
	SetSimulationStatus(FALSE);
	return S_OK;
}
HRESULT CAddinSimulationManager::OnInitializeLogFileInfo(BSTR ExperimentName)
{
	CComPtr<IExperiment> Experiment;
	HRESULT HR = Experiment.CoCreateInstance(CLSID_Experiment);
	if (SUCCEEDED(HR))
	{
		//Set Polygon Crystal Header File Info
		Experiment->WriteCSVLogFileHeaderInfo(CString("Angle,X,Y,Z\n").AllocSysString());
		return S_OK;
	}
	else
	{
		return E_FAIL;
	}
}
void CAddinSimulationManager::OnInitializeSimulationGraph(BSTR ExperimentName)
{
	m_pObjectDemoExperiment->InitializeSimulationGraph(ExperimentName);
}
void CAddinSimulationManager::OnInitializeSimulationVideoRecording(BSTR ExperimentName)
{

}
void CAddinSimulationManager::OnNewDocument(BSTR DocumentName)
{

}
BOOL CAddinSimulationManager::OnDocumentOpened(BSTR DocumentPath)
{
	if (LoadDataFromDocument())
	{
		//Load the Default Selection
		LoadDefaultSelection();
		return S_OK;
	}
	else
	{
		return E_NOTIMPL;
	}
}

void CAddinSimulationManager::LoadDefaultSelection()
{
	CComPtr<IExperiment> Experiment;
	HRESULT hr = Experiment.CoCreateInstance(CLSID_Experiment);
	if (SUCCEEDED(hr))
	{
		BSTR SelectedExperiment;
		long SessionID;
		Experiment->GetSelectedExperiment(&SelectedExperiment, &SessionID);
		if (m_pAddin->m_lSessionID == SessionID)
		{
			LoadExperiments();
			if (m_strExperimentName != _T(""))
			{
				OnTreeNodeDblClick(SessionID, m_strRootText.AllocSysString(), m_strExperimentGroup.AllocSysString(), m_strExperimentName.AllocSysString());
				//Now selcet the tree now and Exand it
				CComPtr<IExperimentTreeView> ExperimentTreeView;
				HRESULT HR = ExperimentTreeView.CoCreateInstance(CLSID_ExperimentTreeView);
				if (FAILED(HR))
				{
					return;
				}
				ExperimentTreeView->SetTreeGroupState(m_strExperimentGroup.AllocSysString(), TVE_EXPAND);
				ExperimentTreeView->SelectActiveExperiment(SessionID, m_strExperimentGroup.AllocSysString(), m_strExperimentName.AllocSysString());
				OnReloadExperiment(SessionID, m_strRootText.AllocSysString(), m_strExperimentGroup.AllocSysString(), m_strExperimentName.AllocSysString());
			}
		}
	}
}
void CAddinSimulationManager::OnCloseDocument(BSTR DocumentPath)
{

}
void CAddinSimulationManager::OnBeforeSaveDocument(BSTR DocumentPath)
{
	SetDataToDocument();
}
void CAddinSimulationManager::OnDrawSimulation()
{
	
}
void CAddinSimulationManager::OnInitializeSimulation(long b3DMode, long VisualizationMode, BSTR Experiment)
{
	m_b3DMode = b3DMode;
	m_lVisualizationMode = VisualizationMode;
	LoadOtherSimulationOptions();
	//Reload the selected experiment to reflect the Visualization Mode
	OnReloadExperiment(m_pAddin->m_lSessionID, m_strRootText.AllocSysString(), m_strExperimentGroup.AllocSysString(), m_strExperimentName.AllocSysString());
	
}
void CAddinSimulationManager::OnDrawPredefinedScene(BSTR Experiment)
{

}
void CAddinSimulationManager::OnOwnerDrawSimulation()
{

}
void CAddinSimulationManager::OnOwnerDrawCreate()
{

}
void CAddinSimulationManager::ViewWndProc(long MsgID, VARIANT wParam, VARIANT lParam)
{

}
void CAddinSimulationManager::OnActivateView(long bActivate, BSTR CurrentViewFilePath, BSTR PreviousViewFilePath)
{
	if (bActivate)
	{
		LoadDefaultSelection();
	}
		
}
void CAddinSimulationManager::OnApplicationLaunched()
{

}
void CAddinSimulationManager::OnApplicationClose()
{

}

void CAddinSimulationManager::MianWndProc(long MsgID, VARIANT wParam, VARIANT lParam)
{

}
void CAddinSimulationManager::OnPropertyChanged(BSTR GroupName, BSTR PropertyName, BSTR PropertyValue)
{
	if (CString(GroupName) == OBJECT_PROPERTIES_TITLE)
	{
		m_pObjectDemoExperiment->OnPropertyChanged(GroupName, PropertyName, PropertyValue);
	}
	
}
void CAddinSimulationManager::OnBeforeAddinControlsLoad()
{

}
void CAddinSimulationManager::OnAfterAddinControlsLoad() 
{

}
void CAddinSimulationManager::GetControlStatus(BSTR CtrlID, long * pStatus)
{
	*pStatus = TRUE;
}
void CAddinSimulationManager::RibbonWndProc(long MsgID, VARIANT wParam, VARIANT lParam)
{

}

void CAddinSimulationManager::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		ar << m_strRootText;
		ar << m_strExperimentGroup;
		ar << m_strExperimentName;
		m_pObjectDemoExperiment->Serialize(ar);
	}
	else
	{
		ar >> m_strRootText;
		ar >> m_strExperimentGroup;
		ar >> m_strExperimentName;
		m_pObjectDemoExperiment->Serialize(ar);
	}
}


BOOL CAddinSimulationManager::LoadDataFromDocument()
{
	//We will be loading all the data from the file as a Plugin Data File.
	//with a Key Value . The value will be set to base 64 format. 

	CComPtr<IApplicationDocument> ApplicationDocument;
	HRESULT HR = ApplicationDocument.CoCreateInstance(CLSID_ApplicationDocument);
	BSTR EncodedData = _T("");
	if (SUCCEEDED(HR))
	{
		ApplicationDocument->GetAddinSettingsAsString(m_strPluginName.AllocSysString(), CComBSTR(CPP_SAMPLE_DOC_SETTINGS_KEY), &EncodedData);
		CStringA strEncodedData(EncodedData);
		if (strEncodedData == _T(""))
		{
			return FALSE;
		}
		//AfxMessageBox(CString(strEncodedData));
		CBase64DecodeInfo Info;
		BOOL bResult = DecodeBase64(strEncodedData, Info);
		if (!bResult)
		{
			return FALSE;
		}
		//Load the data to data strructure
		CMemFile theFile(Info.m_pData, Info.m_Length);
		CArchive archive(&theFile, CArchive::load);
		Serialize(archive);
		archive.Close();

		return TRUE;
	}
	else
	{
		return FALSE;
	}
}


void CAddinSimulationManager::SetDataToDocument()
{
	CMemFile theFile;
	CArchive archive(&theFile, CArchive::store);
	Serialize(archive);
	archive.Close();

	int nSize = (int)theFile.GetLength();
	BYTE* mData = theFile.Detach();

	CStringA EncodedData = ToBase64(mData, nSize);
	//Save it Document Object

	CComPtr<IApplicationDocument> ApplicationDocument;
	HRESULT HR = ApplicationDocument.CoCreateInstance(CLSID_ApplicationDocument);
	if (SUCCEEDED(HR))
	{
		ApplicationDocument->SetAddinSettingsAsString(m_strPluginName.AllocSysString(), CComBSTR(CPP_SAMPLE_DOC_SETTINGS_KEY), EncodedData.AllocSysString());

	}
}

CStringA CAddinSimulationManager::ToBase64(const void* bytes, int byteLength)
{
	ASSERT(0 != bytes);

	CStringA base64;
	int base64Length = Base64EncodeGetRequiredLength(byteLength);

	VERIFY(Base64Encode(static_cast<const BYTE*>(bytes),
		byteLength,
		base64.GetBufferSetLength(base64Length),
		&base64Length));

	base64.ReleaseBufferSetLength(base64Length);
	return base64;
}

BOOL CAddinSimulationManager::DecodeBase64(CStringA Base64Data, CBase64DecodeInfo & Info)
{
	int iSrcLen = Base64Data.GetLength();
	int iRstLen = Base64DecodeGetRequiredLength(iSrcLen);
	iRstLen++;
	Info.m_Length = iRstLen;
	Info.m_pData = new BYTE[iRstLen];
	memset(Info.m_pData, 0, iRstLen);
	BOOL bRet = Base64Decode(Base64Data, iSrcLen, Info.m_pData, &Info.m_Length);
	return bRet;
}

BOOL CAddinSimulationManager::IsAddinExperimentSelected()
{
	//This routine should not be called form another thread 
	//It will fail
	
	CComPtr<IExperiment> Experiment;
	HRESULT hr = Experiment.CoCreateInstance(CLSID_Experiment);
	if (SUCCEEDED(hr))
	{
		m_strSelectedExperiment = _T("");
		static BSTR SelectedExperiment;
		long SessionID;
		Experiment->GetSelectedExperiment(&SelectedExperiment, &SessionID);
		if (m_pAddin->m_lSessionID != SessionID)
		{
			CString strMessage;
			strMessage.Format(_T("Error in Loading Experiment. Selected Experiment Name%s | Addin SessionID %d | Current SessionID %d "), CString(SelectedExperiment), m_pAddin->m_lSessionID, SessionID);
			AfxMessageBox(strMessage);
			return FALSE;
		}
		m_strSelectedExperiment = SelectedExperiment;
		return TRUE;
	}
	else
	{
		DWORD LastError = GetLastError();
		CString strMessage;
		strMessage.Format(_T("Failed to Start Experiment Interface | HR Returns %d | GetLastError Returns %d"), hr, LastError);
		AfxMessageBox(strMessage);
		return FALSE;
	}
}

void CAddinSimulationManager::SetStatusBarMessage(CString strStatus, BOOL bPostMessage)
{
	_tcscpy(m_strCurrentStatusBarMessage, strStatus);

	CComPtr<IMainWindow> MainWindow;
	HRESULT hr = MainWindow.CoCreateInstance(CLSID_MainWindow);
	if (SUCCEEDED(hr))
	{
		MainWindow->SetStatusbarMessage(strStatus.AllocSysString(), bPostMessage);
	}

}

void CAddinSimulationManager::ResetPropertyGrid()
{
	CComPtr<IPropertyWindow> PropertyWindow;
	HRESULT HR = PropertyWindow.CoCreateInstance(CLSID_PropertyWindow);
	CString strGroupName = _T("");
	if (SUCCEEDED(HR))
	{
		PropertyWindow->RemoveAll();

		PropertyWindow->EnableHeaderCtrl(FALSE);
		PropertyWindow->EnableDescriptionArea(TRUE);
		PropertyWindow->SetVSDotNetLook(TRUE);
		PropertyWindow->MarkModifiedProperties(TRUE, TRUE);

	}
}

void CAddinSimulationManager::ResetAllStatusWindows()
{
	CComPtr<IMainWindow> MainWindow;
	HRESULT hr = MainWindow.CoCreateInstance(CLSID_MainWindow);
	if (SUCCEEDED(hr))
	{
		MainWindow->ResetAllStatusWindows();
	}
}

void CAddinSimulationManager::SetSimulationStatus(BOOL bActive)
{
	m_bSimulationActive = bActive;
}

void CAddinSimulationManager::LoadOtherSimulationOptions()
{
	CComPtr<IApplicationDocument> ApplicationDocument;
	HRESULT HR = ApplicationDocument.CoCreateInstance(CLSID_ApplicationDocument);
	if (SUCCEEDED(HR))
	{
		LONG Status;
		ApplicationDocument->get_LogToCSVFileStatus(&Status);
		m_bLogSimulationResultsToCSVFile = Status;

		ApplicationDocument->get_DisplayRealTimeGraphStatus(&Status);
		m_bDisplayRealTimeGraph = Status;

		ApplicationDocument->get_RecordSimulationAsVideoStatus(&Status);
		m_bRecordSimulationAsVideo = Status;

		ApplicationDocument->get_DisplayExpParamStatus(&Status);
		m_bShowExperimentalParamaters = Status;
		

	}
}

void CAddinSimulationManager::LogSimulationPoint(CString strLogData)
{
	//Log to file using Interfaces
	CComPtr<IExperiment> Experiment;
	HRESULT hr = Experiment.CoCreateInstance(CLSID_Experiment);
	if (SUCCEEDED(hr))
	{
		Experiment->WriteToCSVLogFile(strLogData.AllocSysString());
	}
}

void CAddinSimulationManager::OnActivateSnapshot(BSTR FilePath, BSTR GroupName, BSTR SnapshotName)
{
	LoadDataFromDocument();
}

void CAddinSimulationManager::OnAddSnapshot(BSTR FilePath, BSTR GroupName, BSTR SnapshotName)
{
}

void CAddinSimulationManager::OnDeleteSnapshot(BSTR FilePath, BSTR GroupName, BSTR SnapshotName)
{
}

void CAddinSimulationManager::OnDeleteAllSnapshot(BSTR FilePath)
{
}

