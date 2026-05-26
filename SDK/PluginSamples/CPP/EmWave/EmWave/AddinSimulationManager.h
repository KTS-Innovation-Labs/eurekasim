#pragma once


#include "ObjectDemoExperiment.h"

#define CPP_SAMPLE_DOC_SETTINGS_KEY									_T("Cpp.Sample.Addin.Settings")
#define CPP_SAMPLE_MAIN_EXPERIMENT_NAME								_T("EmWave Experiment Simulation Demo")



#define CPP_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES				_T("Experiment Group 1 Properties")


class CBase64DecodeInfo
{
public:

	BYTE* m_pData;
	int m_Length;
	CBase64DecodeInfo()
	{
		m_pData = NULL;
		m_Length = 0;
	}
};

// CAddinSimulationManager command target
class CEmWaveSimulation;

enum EAxisPos
{
	LeftAxis = 0,
	BottomAxis,
	RightAxis,
	TopAxis
};

class CAddinSimulationManager : public CObject
{
	TCHAR									m_strCurrentStatusBarMessage[250];
	CString									m_strSelectedExperiment;
	CString									m_strPluginName;
	CString									m_strRootText;
	
public:
	CString									m_strExperimentGroup;
	CString									m_strExperimentName;
	BOOL									m_bSimulationActive;
	CEmWaveSimulation*						m_pAddin;
	CObjectDemoExperiment*					m_pObjectDemoExperiment;
	
	BOOL									m_b3DMode;
	long									m_lVisualizationMode;
	BOOL									m_bLogSimulationResultsToCSVFile;
	BOOL									m_bDisplayRealTimeGraph;
	BOOL									m_bRecordSimulationAsVideo;
	BOOL									m_bShowExperimentalParamaters;
	TCHAR									m_strCurrentOutputStatusMessage[250];

	void SetPluginName(CString strPluginName)
	{
		m_strPluginName = strPluginName;
	}

	CAddinSimulationManager(CEmWaveSimulation* pAddin);
	virtual ~CAddinSimulationManager();
	void LoadExperiments();
	void OnTreeNodeSelect(long SessionID, BSTR RootText, BSTR ExperimentGroup, BSTR ExperimentName);
	void OnTreeNodeDblClick(long SessionID, BSTR RootText, BSTR ExperimentGroup, BSTR ExperimentName);
	void OnReloadExperiment(long SessionID, BSTR RootText, BSTR ExperimentGroup, BSTR ExperimentName);
	
	void ResetExpermentTree();

	void OnStatusChange(long StatusCode, BSTR StatusDesc, BSTR AdditionalParam);
	
	void OnError(long ErrorCode, BSTR ErrorDesc, BSTR AdditionalParam);
	HRESULT OnStartSimulation(BSTR ExperimentName);
	HRESULT OnStopSimulation(BSTR ExperimentName);
	HRESULT OnInitializeLogFileInfo(BSTR ExperimentName);
	void OnInitializeSimulationGraph(BSTR ExperimentName);
	void OnInitializeSimulationVideoRecording(BSTR ExperimentName);
	void OnNewDocument(BSTR DocumentName);
	BOOL OnDocumentOpened(BSTR DocumentPath);
	void OnCloseDocument(BSTR DocumentPath);
	void OnBeforeSaveDocument(BSTR DocumentPath);
	void OnDrawSimulation();
	void OnInitializeSimulation(long b3DMode, long VisualizationMode, BSTR Experiment);
	void OnDrawPredefinedScene(BSTR Experiment);
	void OnOwnerDrawSimulation();
	void OnOwnerDrawCreate();
	void ViewWndProc(long MsgID, VARIANT wParam, VARIANT lParam);
	void OnActivateView(long bActivate, BSTR CurrentViewFilePath, BSTR PreviousViewFilePath);
	void OnApplicationLaunched();
	void OnApplicationClose();
	void MianWndProc(long MsgID, VARIANT wParam, VARIANT lParam);
	void OnPropertyChanged(BSTR GroupName, BSTR PropertyName, BSTR PropertyValue);
	void OnBeforeAddinControlsLoad();
	void OnAfterAddinControlsLoad();
	void GetControlStatus(BSTR CtrlID, long * pStatus);
	void RibbonWndProc(long MsgID, VARIANT wParam, VARIANT lParam);

	//Virtual Methods in CObject
	virtual void Serialize(CArchive& ar);

	BOOL LoadDataFromDocument();
	void SetDataToDocument();
	CStringA ToBase64(const void* bytes, int byteLength);
	BOOL DecodeBase64(CStringA Base64Data, CBase64DecodeInfo & Info);

	void LoadDefaultSelection();
	

	BOOL IsAddinExperimentSelected();
	void SetStatusBarMessage(CString strStatus, BOOL bPostMessage=TRUE);

	void ResetPropertyGrid();
	

	void ResetAllStatusWindows();
	

	void SetSimulationStatus(BOOL bActive);
	

	// Get the simulation options
	void LoadOtherSimulationOptions();
	void AddOperationStatus(CString strStatus, LONG bPostMessage = TRUE);

	void LogSimulationPoint(CString strLogData);

	void OnActivateSnapshot(BSTR FilePath, BSTR GroupName, BSTR SnapshotName);
	void OnAddSnapshot(BSTR FilePath, BSTR GroupName, BSTR SnapshotName);
	void OnDeleteSnapshot(BSTR FilePath, BSTR GroupName, BSTR SnapshotName);
	void OnDeleteAllSnapshot(BSTR FilePath);
	
};


