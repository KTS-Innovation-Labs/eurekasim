// MySimulationAddin.h : Declaration of the CMySimulationAddin

#pragma once
#include "resource.h"       // main symbols



#include "EmWave_i.h"
#include "AddinSimulationManager.h"



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;


// CMySimulationAddin
static const GUID CATID_PROJECT_FRAMEWORK_ADDINS_RIBBON =
{ 0x66ebc8d5, 0xa4d0, 0x48c4,{ 0x97, 0x8b, 0x55, 0xd1, 0xb3, 0x4e,0x15, 0x7b } };

class CAddinSimulationManager;

class ATL_NO_VTABLE CEmWaveSimulation :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CEmWaveSimulation, &CLSID_EmWaveSimulation>,
	public IDispatchImpl<IEmWaveSimulation, &IID_IEmWaveSimulation, &LIBID_EmWaveLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
	public IDispatchImpl<IAddin, &__uuidof(IAddin), &LIBID_EurekaSim, /* wMajor = */ 1>,
	public IDispatchImpl<IExperimentTreeViewEvents, &__uuidof(IExperimentTreeViewEvents), &LIBID_EurekaSim, /* wMajor = */ 1>,
	public IDispatchImpl<IExperimentEvents, &__uuidof(IExperimentEvents), &LIBID_EurekaSim, /* wMajor = */ 1>,
	public IDispatchImpl<IApplicationDocumentEvents, &__uuidof(IApplicationDocumentEvents), &LIBID_EurekaSim, /* wMajor = */ 1>,
	public IDispatchImpl<IApplicationViewEvents, &__uuidof(IApplicationViewEvents), &LIBID_EurekaSim, /* wMajor = */ 1>,
	public IDispatchImpl<IMainApplicationEvents, &__uuidof(IMainApplicationEvents), &LIBID_EurekaSim, /* wMajor = */ 1>,
	public IDispatchImpl<IMainWindowEvents, &__uuidof(IMainWindowEvents), &LIBID_EurekaSim, /* wMajor = */ 1>,
	public IDispatchImpl<IPropertyWindowEvents, &__uuidof(IPropertyWindowEvents), &LIBID_EurekaSim, /* wMajor = */ 1>,
	public IDispatchImpl<IRibbonToolbarEvents, &__uuidof(IRibbonToolbarEvents), &LIBID_EurekaSim, /* wMajor = */ 1>,
	public IDispatchImpl<IFileSettingsTreeViewEvents, &__uuidof(IFileSettingsTreeViewEvents), &LIBID_EurekaSim, /* wMajor = */ 1>
{
	IMainApplication *			m_pMainApplication;
public:
	CAddinSimulationManager* m_pAddinSimulationManager; //Addin Simulation Manager
	long m_lSessionID;
	CString m_ExperimentName;
	CEmWaveSimulation()
	{
		m_lSessionID = -1;
		m_pAddinSimulationManager = new CAddinSimulationManager(this);
		m_ExperimentName = _T("");
	}


	DECLARE_REGISTRY_RESOURCEID(IDR_MYSIMULATIONADDIN)


	BEGIN_COM_MAP(CEmWaveSimulation)
		COM_INTERFACE_ENTRY(IEmWaveSimulation)
		COM_INTERFACE_ENTRY2(IDispatch, IEmWaveSimulation)
		COM_INTERFACE_ENTRY(IAddin)
		COM_INTERFACE_ENTRY(IExperimentTreeViewEvents)
		COM_INTERFACE_ENTRY(IExperimentEvents)
		COM_INTERFACE_ENTRY(IApplicationDocumentEvents)
		COM_INTERFACE_ENTRY(IApplicationViewEvents)
		COM_INTERFACE_ENTRY(IMainApplicationEvents)
		COM_INTERFACE_ENTRY(IMainWindowEvents)
		COM_INTERFACE_ENTRY(IPropertyWindowEvents)
		COM_INTERFACE_ENTRY(IRibbonToolbarEvents)
		COM_INTERFACE_ENTRY(IFileSettingsTreeViewEvents)
	END_COM_MAP()

	BEGIN_CATEGORY_MAP(CAddinObject)
		IMPLEMENTED_CATEGORY(CATID_PROJECT_FRAMEWORK_ADDINS_RIBBON)
	END_CATEGORY_MAP()


	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}
	void SetRibbonControlText(CString ID, CString Text);
	void LoadAddinName(CString strXML);
public:

	// IAddin Methods
public:
	STDMETHOD(Initialize)(long lSessionID, IMainApplication * pApp, VARIANT bFirstTime)
	{
		AFX_MANAGE_STATE(AfxGetStaticModuleState())
			CString strXMLCommand;
		m_pMainApplication = pApp;
		m_lSessionID = lSessionID;
		//You have to berak the string table if you want to accomodate all 
		//words that is there in the XML String
		//Load the XML String
		HINSTANCE lAppHandle = AfxGetInstanceHandle();
		CString strReserved = _T("");

		if (LoadLongResource(strXMLCommand, IDR_HTML_RIBBON_ELMENTS))
		{
			strXMLCommand.Replace(_T("\n"), _T("")); //Replace all new line characters
			strXMLCommand.Replace(_T("\t"), _T("")); //Replace all tab characters
		}
		pApp->SetAddInInfo(lSessionID, (LONGLONG)lAppHandle, strXMLCommand.AllocSysString(), (LONGLONG)IDR_TOOLBAR_ADDIN, strReserved.AllocSysString());
		LoadAddinName(strXMLCommand);

		CComPtr<IRibbonToolbar> MyRibbonToolbar;
		HRESULT hr = MyRibbonToolbar.CoCreateInstance(CLSID_RibbonToolbar);
		if (SUCCEEDED(hr))
		{
			MyRibbonToolbar->SetAddinRibbonMenuInfo(lSessionID, CString("Print").AllocSysString(), CString("TestMenu").AllocSysString(),
				CString("ID.TestMenu").AllocSysString(), CString("InvokeDefaultSettings").AllocSysString(), CString("Test Menu Description").AllocSysString(), CString("").AllocSysString(), 0);

		}

		CComPtr<IExperiment> Experiment;
		hr = Experiment.CoCreateInstance(CLSID_Experiment);

		if (SUCCEEDED(hr))
		{
			CString ExperimentName = CPP_SAMPLE_MAIN_EXPERIMENT_NAME;
			m_ExperimentName = ExperimentName;
			Experiment->AddExperiment(lSessionID, ExperimentName.AllocSysString());

		}

		return S_OK;
	}
	STDMETHOD(Uninitialize)(long lSessionID)
	{
		if (m_lSessionID == lSessionID)
		{
			delete m_pAddinSimulationManager;
		}

		return S_OK;
	}
	STDMETHOD(About)()
	{
		AfxMessageBox(_T("About My Addin"));
		return S_OK;
	}

	BOOL LoadLongResource(CString &str, UINT nID);

	STDMETHOD(InvokePreferenceSettings)();
	STDMETHOD(InvokeDefaultSettings)();
	STDMETHOD(InvokeOnExperimentChanged)();

	// IExperimentTreeViewEvents Methods
public:
	STDMETHOD(OnTreeNodeSelect)(long SessionID, BSTR RootText, BSTR ExperimentGroup, BSTR ExperimentName)
	{
		m_pAddinSimulationManager->OnTreeNodeSelect(SessionID, RootText, ExperimentGroup, ExperimentName);
		return S_OK;
	}
	STDMETHOD(OnTreeNodeDblClick)(long SessionID, BSTR RootText, BSTR ExperimentGroup, BSTR ExperimentName)
	{
		m_pAddinSimulationManager->OnTreeNodeDblClick(SessionID, RootText, ExperimentGroup, ExperimentName);
		return S_OK;
	}
	STDMETHOD(OnReloadExperiment)(long SessionID, BSTR RootText, BSTR ExperimentGroup, BSTR ExperimentName)
	{
		m_pAddinSimulationManager->OnReloadExperiment(SessionID, RootText, ExperimentGroup, ExperimentName);
		return S_OK;
	}

	// IExperimentEvents Methods
public:
	STDMETHOD(OnStartSimulation)(BSTR ExperimentName)
	{
		m_pAddinSimulationManager->OnStartSimulation(ExperimentName);
		return E_NOTIMPL;
	}
	STDMETHOD(OnStopSimulation)(BSTR ExperimentName)
	{
		m_pAddinSimulationManager->OnStopSimulation(ExperimentName);
		return E_NOTIMPL;
	}
	STDMETHOD(OnStatusChange)(long StatusCode, BSTR StatusDesc, BSTR AdditionalParam)
	{
		m_pAddinSimulationManager->OnStatusChange(StatusCode, StatusDesc, AdditionalParam);

		return S_OK;
	}
	STDMETHOD(OnError)(long ErrorCode, BSTR ErrorDesc, BSTR AdditionalParam)
	{
		m_pAddinSimulationManager->OnError( ErrorCode, ErrorDesc, AdditionalParam);
		return E_NOTIMPL;
	}
	STDMETHOD(OnInitializeLogFileInfo)(BSTR ExperimentName)
	{
		return m_pAddinSimulationManager->OnInitializeLogFileInfo(ExperimentName);
	}
	STDMETHOD(OnInitializeSimulationGraph)(BSTR ExperimentName)
	{
		m_pAddinSimulationManager->OnInitializeSimulationGraph(ExperimentName);
		return S_OK;
	}
	STDMETHOD(OnInitializeSimulationVideoRecording)(BSTR ExperimentName)
	{
		m_pAddinSimulationManager->OnInitializeSimulationVideoRecording(ExperimentName);
		return S_OK;
	}

	// IApplicationDocumentEvents Methods
public:
	STDMETHOD(OnNewDocument)(BSTR DocumentName)
	{
		m_pAddinSimulationManager->OnNewDocument(DocumentName);
		return S_OK;
	}
	STDMETHOD(OnDocumentOpened)(BSTR DocumentPath)
	{
		return m_pAddinSimulationManager->OnDocumentOpened(DocumentPath);
		
	}
	STDMETHOD(OnCloseDocument)(BSTR DocumentPath)
	{
		m_pAddinSimulationManager->OnCloseDocument(DocumentPath);
		return S_OK;
	}
	STDMETHOD(OnBeforeSaveDocument)(BSTR DocumentPath)
	{
		m_pAddinSimulationManager->OnBeforeSaveDocument(DocumentPath);
		return S_OK;
	}
	STDMETHOD(OnAfterSaveDocument)(BSTR DocumentPath)
	{
		return S_OK;
	}
	// IApplicationViewEvents Methods
public:
	STDMETHOD(OnDrawSimulation)()
	{
		m_pAddinSimulationManager->OnDrawSimulation();
		return S_OK;
	}
	STDMETHOD(OnInitializeSimulation)(long b3DMode, long VisualizationMode, BSTR Experiment)
	{
		m_pAddinSimulationManager->OnInitializeSimulation(b3DMode, VisualizationMode, Experiment);
		return S_OK;
	}
	STDMETHOD(OnDrawPredefinedScene)(BSTR Experiment)
	{
		m_pAddinSimulationManager->OnDrawPredefinedScene(Experiment);
		return S_OK;
	}
	STDMETHOD(OnOwnerDrawSimulation)()
	{
		m_pAddinSimulationManager->OnOwnerDrawSimulation();
		return S_OK;
	}
	STDMETHOD(OnOwnerDrawCreate)()
	{
		m_pAddinSimulationManager->OnOwnerDrawCreate();
		return S_OK;
	}
	STDMETHOD(ViewWndProc)(long MsgID, VARIANT wParam, VARIANT lParam)
	{
		m_pAddinSimulationManager->ViewWndProc(MsgID, wParam, lParam);
		return S_OK;
	}
	STDMETHOD(OnActivateView)(long bActivate, BSTR CurrentViewFilePath, BSTR PreviousViewFilePath)
	{
		m_pAddinSimulationManager->OnActivateView(bActivate, CurrentViewFilePath, PreviousViewFilePath);
		return S_OK;
	}

	// IMainApplicationEvents Methods
public:
	STDMETHOD(OnApplicationLaunched)()
	{
		m_pAddinSimulationManager->OnApplicationLaunched();
		return S_OK;
	}
	STDMETHOD(OnApplicationClose)()
	{
		m_pAddinSimulationManager->OnApplicationClose();
		return S_OK;
	}

	// IMainWindowEvents Methods
public:
	STDMETHOD(MianWndProc)(long MsgID, VARIANT wParam, VARIANT lParam)
	{
		m_pAddinSimulationManager->MianWndProc(MsgID, wParam, lParam);
		return S_OK;
	}

	// IPropertyWindowEvents Methods
public:
	STDMETHOD(OnPropertyChanged)(BSTR GroupName, BSTR PropertyName, BSTR PropertyValue)
	{
		m_pAddinSimulationManager->OnPropertyChanged(GroupName, PropertyName, PropertyValue);
		return S_OK;
	}

	// IRibbonToolbarEvents Methods
public:
	STDMETHOD(OnBeforeAddinControlsLoad)()
	{
		m_pAddinSimulationManager->OnBeforeAddinControlsLoad();
		return S_OK;
	}
	STDMETHOD(OnAfterAddinControlsLoad)()
	{
		m_pAddinSimulationManager->OnAfterAddinControlsLoad();
		return S_OK;
	}
	STDMETHOD(GetControlStatus)(BSTR CtrlID, long * pStatus)
	{
		m_pAddinSimulationManager->GetControlStatus(CtrlID,  pStatus);
		return S_OK;
	}
	STDMETHOD(RibbonWndProc)(long MsgID, VARIANT wParam, VARIANT lParam)
	{
		m_pAddinSimulationManager->RibbonWndProc(MsgID, wParam, lParam);
		return S_OK;
	}

	// IFileSettingsTreeViewEvents Methods
public:
	STDMETHOD(OnActivateSnapshot)(BSTR FilePath, BSTR GroupName, BSTR SnapshotName)
	{
		m_pAddinSimulationManager->OnActivateSnapshot(FilePath, GroupName, SnapshotName);

		return S_OK;
	}
	STDMETHOD(OnAddSnapshot)(BSTR FilePath, BSTR GroupName, BSTR SnapshotName)
	{
		m_pAddinSimulationManager->OnAddSnapshot(FilePath, GroupName, SnapshotName);
		return S_OK;
	}
	STDMETHOD(OnDeleteSnapshot)(BSTR FilePath, BSTR GroupName, BSTR SnapshotName)
	{
		m_pAddinSimulationManager->OnDeleteSnapshot(FilePath, GroupName, SnapshotName);
		return S_OK;
	}
	STDMETHOD(OnDeleteAllSnapshot)(BSTR FilePath)
	{
		m_pAddinSimulationManager->OnDeleteAllSnapshot(FilePath);
		return S_OK;
	}

};

OBJECT_ENTRY_AUTO(__uuidof(EmWaveSimulation), CEmWaveSimulation)
