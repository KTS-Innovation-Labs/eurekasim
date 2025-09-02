using EurekaSim.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TowerOfHanoiCS
{
	enum EAxisPos
	{
		LeftAxis = 0,
		BottomAxis,
		RightAxis,
		TopAxis
	};
	public class AddinSimulationManager
	{
		#region Member Variable Declerations
		string m_strCurrentStatusBarMessage;
		string m_strSelectedExperiment;
		string m_strPluginName;
		string m_strRootText;

		public string m_strExperimentGroup;
		public string m_strExperimentName;
		public bool m_bSimulationActive;
		public TowerOfHanoiCSImp m_pAddin;
		public ObjectDemoExperiment m_pObjectDemoExperiment;

		public bool m_b3DMode;
		public long m_lVisualizationMode;
		public bool m_bLogSimulationResultsToCSVFile;
		public bool m_bDisplayRealTimeGraph;
		public bool m_bRecordSimulationAsVideo;
		public bool m_bShowExperimentalParamaters;
		public string m_strCurrentOutputStatusMessage;
		#endregion

		#region Constructor
		public AddinSimulationManager(TowerOfHanoiCSImp pAddin)
		{
			m_pAddin = pAddin;
			m_pObjectDemoExperiment = new ObjectDemoExperiment(this);
			m_bSimulationActive = false;

		}
		#endregion
		#region Destructor
		~AddinSimulationManager()
		{
			m_pAddin = null;
			m_pObjectDemoExperiment = null;
			m_bSimulationActive = false;
			GC.Collect();
		}
		#endregion
		#region ITreeNodeEvents
		public void OnTreeNodeSelect(long SessionID, string RootText, string ExperimentGroup, string ExperimentName)
		{
			if (m_pAddin.m_lSessionID != SessionID)
			{
				return;
			}
			else
			{
				m_strRootText = RootText;
				m_strExperimentGroup = ExperimentGroup;
				m_strExperimentName = ExperimentName;
				SetStatusBarMessage(RootText + " | " + ExperimentGroup + " | " + ExperimentName, false);
			}
			if (RootText == Constants.CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES)
			{
				m_pObjectDemoExperiment.OnTreeNodeSelect(ExperimentGroup, ExperimentName);
			}
		}
		public void OnTreeNodeDblClick(int SessionID, string RootText, string ExperimentGroup, string ExperimentName)
		{
			if (m_pAddin.m_lSessionID != SessionID)
			{
				return;
			}
			else
			{
				m_strRootText = RootText;
				m_strExperimentGroup = ExperimentGroup;
				m_strExperimentName = ExperimentName;
				SetStatusBarMessage(RootText + " | " + ExperimentGroup + " | " + ExperimentName, false);
			}

			if (RootText == Constants.CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES)
			{
				m_pObjectDemoExperiment.OnTreeNodeDblClick(ExperimentGroup, ExperimentName);
			}
		}
		public void OnReloadExperiment(int SessionID, string RootText, string ExperimentGroup, string ExperimentName)
		{
			if (m_pAddin.m_lSessionID != SessionID)
			{
				return;
			}
			if (RootText == Constants.CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES)
			{
				m_pObjectDemoExperiment.OnReloadExperiment(ExperimentGroup, ExperimentName);
			}
		}
		#endregion

		#region IExperimentEvents
		public void OnStartSimulation(string ExperimentName)
		{
			if (!IsAddinExperimentSelected())
			{
				string strMessage = string.Format("Failed to Start {0}. Please select Experimental Parameters before starting the Simulation Experiment..", ExperimentName);
				MessageBox.Show(strMessage);
				return;
			}

			LoadOtherSimulationOptions();
			ResetAllStatusWindows();
			m_pObjectDemoExperiment.StartSimulation(m_strExperimentGroup, m_strExperimentName);
		}

		public void OnStopSimulation(string experimentName)
		{
			SetSimulationStatus(Constants.FALSE);
			SetStatusBarMessage("Simulation Stopped.", true);
		}

		public void OnStatusChange(int statusCode, string statusDesc, string additionalParam)
		{
			if (statusCode == 10000) //No Experiment Selected
			{

			}
			else if (statusCode == 10001 && m_pAddin.m_lSessionID == Convert.ToInt32(additionalParam)) //Experiment Changed 
			{
				m_pObjectDemoExperiment.LoadAllExperiments();
				m_pObjectDemoExperiment.OnReloadExperiment(m_strExperimentGroup, m_strExperimentName);

				m_strSelectedExperiment = statusDesc;
				LoadDefaultSelection();
			}
		}
		public void OnError(int errorCode, string errorDesc, string additionalParam)
		{
			// To be Implimented
		}

		public void OnInitializeLogFileInfo(string experimentName)
		{
			Experiment experiment = new Experiment();
			try
			{
				experiment.WriteCSVLogFileHeaderInfo("Angle,X,Y,Z\n");
			}
			catch (Exception) { }
		}

		public void OnInitializeSimulationGraph(string ExperimentName)
		{
			m_pObjectDemoExperiment.InitializeSimulationGraph(ExperimentName);
		}

		public void OnInitializeSimulationVideoRecording(string experimentName)
		{
		}

		#endregion

		#region IApplicationDocumentEvents

		public void OnNewDocument(string DocumentName)
		{
		}

		public void OnDocumentOpened(string DocumentPath)
		{
			if (LoadDataFromDocument())
			{
				LoadDefaultSelection();
			}
		}
		public void OnCloseDocument(string documentPath)
		{
		}

		public void OnBeforeSaveDocument(string documentPath)
		{
			SetDataToDocument();
		}
		#endregion

		#region IApplicationViewEvents

		public void OnDrawSimulation()
		{
		}

		public void OnInitializeSimulation(int b3DMode, int VisualizationMode, string Experiment)
		{
			m_b3DMode = Convert.ToBoolean(b3DMode);
			m_lVisualizationMode = VisualizationMode;
			LoadOtherSimulationOptions();
			OnReloadExperiment(m_pAddin.m_lSessionID, m_strRootText, m_strExperimentGroup, m_strExperimentName);
		}

		public void OnDrawPredefinedScene(string Experiment)
		{
		}

		public void OnOwnerDrawSimulation()
		{
		}

		public void OnOwnerDrawCreate()
		{
		}
		public void ViewWndProc(int msgID, object wParam, object lParam)
		{
		}
		public void OnActivateView(int bActivate, string currentViewFilePath, string previousViewFilePath)
		{
			if (Convert.ToBoolean(bActivate))
			{
				LoadDefaultSelection();
			}
		}
		#endregion

		#region IMainWindowEvents
		public void MianWndProc(int msgID, object wParam, object lParam)
		{
		}


		#endregion

		#region IPropertyWindowEvents
		public void OnPropertyChanged(string GroupName, string PropertyName, string PropertyValue)
		{
			if (GroupName == Constants.OBJECT_PROPERTIES_TITLE || GroupName == Constants.TOWEROFHANOI_PROPERTIES_TITLE)
			{
				m_pObjectDemoExperiment.OnPropertyChanged(GroupName, PropertyName, PropertyValue);
			}
		}

		#endregion

		#region IMainApplicationEvents
		public void OnApplicationClose()
		{
		}

		public void OnApplicationLaunched()
		{
		}
		#endregion

		#region IRibbonToolbarEvents
		public void OnBeforeAddinControlsLoad()
		{
		}

		public void OnAfterAddinControlsLoad()
		{
		}

		public void GetControlStatus(string ctrlID, ref int pStatus)
		{
			if (ctrlID == "Start")
			{
				pStatus = Constants.BOOL(!m_bSimulationActive);
			}
			else if (ctrlID == "Stop")
			{
				pStatus = Constants.BOOL(m_bSimulationActive);
			}
			else
			{
				pStatus = Constants.TRUE;
			}
		}

		public void RibbonWndProc(int msgID, object wParam, object lParam)
		{
		}
		#endregion

		#region IFileSettingsTreeViewEvents
		public void OnActivateSnapshot(string filePath, string groupName, string snapshotName)
		{
			LoadDataFromDocument();
		}
		public void OnAddSnapshot(string filePath, string groupName, string snapshotName)
		{
		}
		public void OnDeleteSnapshot(string filePath, string groupName, string snapshotName)
		{
		}
		public void OnDeleteAllSnapshot(string filePath)
		{
		}
		#endregion

		#region Other Dependent Methods

		private void LoadDefaultSelection()
		{
			string SelectedExperiment = string.Empty;
			int SessionID = -1;
			Experiment experiment = new Experiment();
			experiment.GetSelectedExperiment(ref SelectedExperiment, ref SessionID);
			if (m_pAddin.m_lSessionID == SessionID)
			{
				LoadExperiments();
				if (!string.IsNullOrEmpty(m_strExperimentName))
				{
					OnTreeNodeDblClick(SessionID, m_strRootText, m_strExperimentGroup, m_strExperimentName);
					ExperimentTreeView experimentTreeView = new ExperimentTreeView();
					experimentTreeView.SetTreeGroupState(m_strExperimentGroup, Constants.TVE_EXPAND);
					experimentTreeView.SelectActiveExperiment(SessionID, m_strExperimentGroup, m_strExperimentName);
					OnReloadExperiment(SessionID, m_strRootText, m_strExperimentGroup, m_strExperimentName);
					experimentTreeView = null;
				}
			}
			experiment = null;
		}
		private void LoadExperiments()
		{
			if (!IsAddinExperimentSelected())
			{
				return;
			}
			m_pObjectDemoExperiment.LoadAllExperiments();
		}

		private bool IsAddinExperimentSelected()
		{
			try
			{
				m_strSelectedExperiment = string.Empty;
				string SelectedExperiment = string.Empty;
				int SessionID = -1;
				Experiment experiment = new Experiment();
				experiment.GetSelectedExperiment(ref SelectedExperiment, ref SessionID);
				if (m_pAddin.m_lSessionID != SessionID)
				{
					string strMessage;
					strMessage = string.Format("Error in Loading Experiment. Selected Experiment Name {0} | Addin SessionID {1} | Current SessionID {2} ", SelectedExperiment, m_pAddin.m_lSessionID, SessionID);
					MessageBox.Show(strMessage);
					return false;
				}
				m_strSelectedExperiment = SelectedExperiment;
				experiment = null;
				return true;
			}
			catch (Exception Ex)
			{
				string LastError = Ex.ToString();
				string strMessage = string.Format("Failed to Start Experiment Interface | GetLastError Returns {0}", LastError);
				MessageBox.Show(strMessage);
				return false;
			}
		}
		public void SetPluginName(string strPluginName)
		{
			m_strPluginName = strPluginName;
		}
		private void ResetAllStatusWindows()
		{
			MainWindow mainWindow = new MainWindow();
			try
			{
				mainWindow.ResetAllStatusWindows();
			}
			catch (Exception) { }
			finally { mainWindow = null; }
		}

		private void LoadOtherSimulationOptions()
		{
			ApplicationDocument applicationDocument = new ApplicationDocument();
			try
			{
				m_bLogSimulationResultsToCSVFile = Convert.ToBoolean(applicationDocument.LogToCSVFileStatus);
				m_bDisplayRealTimeGraph = Convert.ToBoolean(applicationDocument.DisplayRealTimeGraphStatus);
				m_bRecordSimulationAsVideo = Convert.ToBoolean(applicationDocument.RecordSimulationAsVideoStatus);
				m_bShowExperimentalParamaters = Convert.ToBoolean(applicationDocument.DisplayExpParamStatus);
			}
			catch (Exception) { }
			finally { applicationDocument = null; }
		}

		private bool LoadDataFromDocument()
		{
			ApplicationDocument applicationDocument = new ApplicationDocument();
			try
			{
				string strEncodedData = string.Empty;
				applicationDocument.GetAddinSettingsAsString(m_strPluginName, Constants.CS_SAMPLE_DOC_SETTINGS_KEY, ref strEncodedData);
				if (string.IsNullOrEmpty(strEncodedData))
				{
					return false;
				}
				if (!DeSerialize(strEncodedData))
				{
					return false;
				}
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			finally { applicationDocument = null; }
		}
		public void LogSimulationPoint(string strLogData)
		{
			try
			{
				(new Experiment()).WriteToCSVLogFile(strLogData);
			}
			catch (Exception) { }
		}
		public void ResetPropertyGrid()
		{
			PropertyWindow objPropertyWindow = new PropertyWindow();
			string strGroupName = string.Empty;
			objPropertyWindow.RemoveAll();
			objPropertyWindow.EnableHeaderCtrl(Constants.FALSE);
			objPropertyWindow.EnableDescriptionArea(Constants.TRUE);
			objPropertyWindow.SetVSDotNetLook(Constants.TRUE);
			objPropertyWindow.MarkModifiedProperties(Constants.TRUE, Constants.TRUE);
			objPropertyWindow = null;
		}

		public void AddOperationStatus(string strStatus, int bPostMessage = Constants.TRUE)
		{
			m_strCurrentOutputStatusMessage = strStatus;
			try
			{
				(new MainWindow()).AddOperationStatus(strStatus, bPostMessage);
			}
			catch (Exception) { }
		}

		public string Serialize()
		{
			ExperimentInfo info = m_pObjectDemoExperiment.Serialize();
			try
			{
				info.RootText = m_strRootText;
				info.ExperimentName = m_strExperimentName;
				info.ExperimentGroup = m_strExperimentGroup;
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExperimentInfo));
				System.IO.StringWriter textWriter = new System.IO.StringWriter();
				xmlSerializer.Serialize(textWriter, info);
				return textWriter.ToString();
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}
		public bool SetDataToDocument()
		{
			ApplicationDocument applicationDocument = new ApplicationDocument();
			try
			{
				string strEncodedData = Serialize();
				if (string.IsNullOrEmpty(strEncodedData))
				{
					return false;
				}
				applicationDocument.SetAddinSettingsAsString(m_strPluginName, Constants.CS_SAMPLE_DOC_SETTINGS_KEY, strEncodedData);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			finally { applicationDocument = null; }
		}
		public bool DeSerialize(string strXML)
		{
			ExperimentInfo info = new ExperimentInfo();
			try
			{
				info.RootText = m_strRootText;
				info.ExperimentName = m_strExperimentName;
				info.ExperimentGroup = m_strExperimentGroup;
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExperimentInfo));
				System.IO.StringReader textReader = new System.IO.StringReader(strXML);
				info = (ExperimentInfo)xmlSerializer.Deserialize(textReader);
				m_strRootText = info.RootText;
				m_strExperimentName = info.ExperimentName;
				m_strExperimentGroup = info.ExperimentGroup;
				m_pObjectDemoExperiment.DeSerialize(info);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public void SetSimulationStatus(int bActive)
		{
			m_bSimulationActive = Convert.ToBoolean(bActive);
		}

		public void SetStatusBarMessage(string strStatus, bool bPostMessage = true)
		{
			m_strCurrentStatusBarMessage = strStatus;
			MainWindow mWindow = new MainWindow();
			mWindow.SetStatusbarMessage(strStatus, Constants.BOOL(bPostMessage));
			mWindow = null;
		}

		#endregion
	}
}