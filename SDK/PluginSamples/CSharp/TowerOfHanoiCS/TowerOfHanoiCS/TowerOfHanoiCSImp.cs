using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EurekaSim.Net;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Drawing;

namespace TowerOfHanoiCS
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [Guid("6307d90f-f663-4f71-b54a-a00386b3c2ee")]
    public class TowerOfHanoiCSImp : EurekaSim.Net.Addin,
        IExperimentTreeViewEvents,
        IExperimentEvents,
        IApplicationDocumentEvents,
        IApplicationViewEvents, IMainApplicationEvents,
        IMainWindowEvents, IPropertyWindowEvents,
        IRibbonToolbarEvents,
        IFileSettingsTreeViewEvents
    {
        #region Member Variables
        public AddinSimulationManager m_pAddinSimulationManager; //Addin Simulation Manager
        public int m_lSessionID;
        public string m_ExperimentName;

        #endregion

        #region Constructor
        public TowerOfHanoiCSImp()
        {
            m_pAddinSimulationManager = new AddinSimulationManager(this); //Addin Simulation Manager
            m_lSessionID = -1;
            m_ExperimentName = "";
        }
        #endregion

        #region Component Category Registration
        [ComRegisterFunction]
        [ComVisible(false)]
        public static void RegisterFunction(Type t)
        {
            string sKey = @"\CLSID\{" + t.GUID.ToString() + @"}\Implemented Categories";
            Microsoft.Win32.RegistryKey regKey
                = Microsoft.Win32.Registry.ClassesRoot
                .OpenSubKey(sKey, true);
            if (regKey != null)
            {
                regKey.CreateSubKey("{66EBC8D5-A4D0-48C4-978B-55D1B34E157B}");
            }
        }

        [ComUnregisterFunction]
        [ComVisible(false)]
        public static void UnregisterFunction(Type t)
        {
            string sKey = @"\CLSID\{" + t.GUID.ToString() + @"}\Implemented Categories";
            Microsoft.Win32.RegistryKey regKey
                = Microsoft.Win32.Registry.ClassesRoot
                .OpenSubKey(sKey, true);
            if (regKey != null)
            {
                regKey.DeleteSubKey("{66EBC8D5-A4D0-48C4-978B-55D1B34E157B}");
            }
        }
        #endregion

        #region IAddin
        public void Initialize(int lSessionID, MainApplication pApp, object bFirstTime)
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            Stream rgbxml = thisAssembly.GetManifestResourceStream("TowerOfHanoiCS.AddinRibbon.xml");
            Stream rgbBmp = thisAssembly.GetManifestResourceStream("TowerOfHanoiCS.toolbar1.bmp");

            Bitmap bmp = new Bitmap(rgbBmp);

            XmlDocument doc = new XmlDocument();
            doc.Load(rgbxml);
            LoadAddinName(doc);
            string strMenuXML = doc.InnerXml;
            IntPtr bitMapHandle = bmp.GetHbitmap();
            m_lSessionID = lSessionID;
            pApp.SetAddInInfo(lSessionID, 0, strMenuXML, (long)bitMapHandle, "");
            Experiment experiment = new Experiment();
            experiment.AddExperiment(lSessionID, Constants.CS_SAMPLE_MAIN_EXPERIMENT_NAME);
            experiment = null;
        }

        private void LoadAddinName(XmlDocument doc)
        {
            XmlElement root = doc.DocumentElement;
            string strPluginName = root.Attributes["label"].Value;
            m_pAddinSimulationManager.SetPluginName(strPluginName);
        }

        public void Uninitialize(int lSessionID)
        {
            m_pAddinSimulationManager = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        public void About()
        {
            MessageBox.Show("The C# Tower of Hanoi Addin");
        }
        #endregion

        #region From XML UI Commands
        public void InvokePreferenceSettings()
        {
            MessageBox.Show("C#.InvokePreferenceSettings");
        }

        public void InvokeDefaultSettings()
        {
            if (m_pAddinSimulationManager != null && m_pAddinSimulationManager.m_pObjectDemoExperiment != null)
            {
                m_pAddinSimulationManager.m_pObjectDemoExperiment.m_ObjectPattern.m_strObjectType = Constants.OBJECT_TYPE_TOWEROFHANOI;
                m_pAddinSimulationManager.m_pObjectDemoExperiment.ShowObjectProperties();
            }
        }

        public void OnNumRingsChange(string CtrlID, int Index)
        {
            if (m_pAddinSimulationManager != null && m_pAddinSimulationManager.m_pObjectDemoExperiment != null)
            {
                int numRings = Index + 3; // Index is 0-based, rings start at 3
                if (m_pAddinSimulationManager.m_pObjectDemoExperiment.m_ObjectPattern.m_nNumberOfRings != numRings)
                {
                    if (m_pAddinSimulationManager.m_bSimulationActive)
                    {
                        m_pAddinSimulationManager.SetSimulationStatus(Constants.FALSE);
                        System.Threading.Thread.Sleep(100); // Give simulation loop time to terminate
                    }

                    m_pAddinSimulationManager.m_pObjectDemoExperiment.m_ObjectPattern.m_nNumberOfRings = numRings;
                    m_pAddinSimulationManager.m_pObjectDemoExperiment.ShowObjectProperties();
                    m_pAddinSimulationManager.m_pObjectDemoExperiment.DrawScene();
                }
            }
        }

        public void OnSpeedChange(string CtrlID, int Index)
        {
            double[] speeds = { 0.25, 0.5, 0.75, 1.0, 1.25, 1.5, 1.75, 2.0 };
            if (Index >= 0 && Index < speeds.Length)
            {
                double speed = speeds[Index];
                if (m_pAddinSimulationManager != null && m_pAddinSimulationManager.m_pObjectDemoExperiment != null)
                {
                    m_pAddinSimulationManager.m_pObjectDemoExperiment.m_ObjectPattern.m_dAnimationSpeed = speed;
                    m_pAddinSimulationManager.m_pObjectDemoExperiment.ShowObjectProperties();
                }
            }
        }

        public void SetRibbonControlText(string ID, string Text)
        {
            RibbonToolbar ribbonToolbar = new RibbonToolbar();
            try
            {
                ribbonToolbar.SetControlText(ID, Text);
            }
            catch (Exception) { }
        }

        #endregion

        #region ITreeNodeEvents
        public void OnTreeNodeSelect(int SessionID, string RootText, string ExperimentGroup, string ExperimentName)
        {
            m_pAddinSimulationManager.OnTreeNodeSelect(SessionID, RootText, ExperimentGroup, ExperimentName);
        }

        public void OnTreeNodeDblClick(int SessionID, string RootText, string ExperimentGroup, string ExperimentName)
        {
            m_pAddinSimulationManager.OnTreeNodeDblClick(SessionID, RootText, ExperimentGroup, ExperimentName);
        }

        public void OnReloadExperiment(int SessionID, string RootText, string ExperimentGroup, string ExperimentName)
        {
            m_pAddinSimulationManager.OnReloadExperiment(SessionID, RootText, ExperimentGroup, ExperimentName);
        }
        #endregion

        #region IExperimentEvents
        public void OnStartSimulation(string ExperimentName)
        {
            m_pAddinSimulationManager.OnStartSimulation(ExperimentName);
        }

        public void OnStopSimulation(string ExperimentName)
        {
            m_pAddinSimulationManager.OnStopSimulation(ExperimentName);
        }

        public void OnStatusChange(int StatusCode, string StatusDesc, string AdditionalParam)
        {
            m_pAddinSimulationManager.OnStatusChange(StatusCode, StatusDesc, AdditionalParam);
        }

        public void OnError(int ErrorCode, string ErrorDesc, string AdditionalParam)
        {
            m_pAddinSimulationManager.OnError(ErrorCode, ErrorDesc, AdditionalParam);
        }

        public void OnInitializeLogFileInfo(string ExperimentName)
        {
            m_pAddinSimulationManager.OnInitializeLogFileInfo(ExperimentName);
        }

        public void OnInitializeSimulationGraph(string ExperimentName)
        {
            m_pAddinSimulationManager.OnInitializeSimulationGraph(ExperimentName);
        }

        public void OnInitializeSimulationVideoRecording(string ExperimentName)
        {
            m_pAddinSimulationManager.OnInitializeSimulationVideoRecording(ExperimentName);
        }
        #endregion

        #region IApplicationDocumentEvents
        public void OnNewDocument(string DocumentName)
        {
            m_pAddinSimulationManager.OnNewDocument(DocumentName);
        }

        public void OnDocumentOpened(string DocumentPath)
        {
            m_pAddinSimulationManager.OnDocumentOpened(DocumentPath);
        }

        public void OnCloseDocument(string DocumentPath)
        {
            m_pAddinSimulationManager.OnCloseDocument(DocumentPath);
        }

        public void OnBeforeSaveDocument(string DocumentPath)
        {
            m_pAddinSimulationManager.OnBeforeSaveDocument(DocumentPath);
        }
        public void OnAfterSaveDocument(string DocumentPath)
        {
        }
        #endregion

        #region IApplicationViewEvents
        public void OnDrawSimulation()
        {
            m_pAddinSimulationManager.OnDrawSimulation();
        }

        public void OnInitializeSimulation(int b3DMode, int VisualizationMode, string Experiment)
        {
            m_pAddinSimulationManager.OnInitializeSimulation(b3DMode, VisualizationMode, Experiment);
        }

        public void OnDrawPredefinedScene(string Experiment)
        {
            m_pAddinSimulationManager.OnDrawPredefinedScene(Experiment);
        }

        public void OnOwnerDrawSimulation()
        {
            m_pAddinSimulationManager.OnOwnerDrawSimulation();
        }

        public void OnOwnerDrawCreate()
        {
            m_pAddinSimulationManager.OnOwnerDrawCreate();
        }

        public void ViewWndProc(int MsgID, object wParam, object lParam)
        {
            m_pAddinSimulationManager.ViewWndProc(MsgID, wParam, lParam);
        }

        public void OnActivateView(int bActivate, string CurrentViewFilePath, string PreviousViewFilePath)
        {
            m_pAddinSimulationManager.OnActivateView(bActivate, CurrentViewFilePath, PreviousViewFilePath);
        }
        #endregion

        #region IMainApplicationEvents
        public void OnApplicationLaunched()
        {
            m_pAddinSimulationManager.OnApplicationLaunched();
        }

        public void OnApplicationClose()
        {
            m_pAddinSimulationManager.OnApplicationClose();
        }
        #endregion

        #region IMainWindowEvents
        public void MianWndProc(int MsgID, object wParam, object lParam)
        {
            m_pAddinSimulationManager.MianWndProc(MsgID, wParam, lParam);
        }
        #endregion

        #region IPropertyWindowEvents
        public void OnPropertyChanged(string GroupName, string PropertyName, string PropertyValue)
        {
            m_pAddinSimulationManager.OnPropertyChanged(GroupName, PropertyName, PropertyValue);
        }
        #endregion

        #region IRibbonToolbarEvents
        public void OnBeforeAddinControlsLoad()
        {
            m_pAddinSimulationManager.OnBeforeAddinControlsLoad();
        }

        public void OnAfterAddinControlsLoad()
        {
            m_pAddinSimulationManager.OnAfterAddinControlsLoad();
        }

        public void GetControlStatus(string CtrlID, ref int pStatus)
        {
            m_pAddinSimulationManager.GetControlStatus(CtrlID, ref pStatus);
        }

        public void RibbonWndProc(int MsgID, object wParam, object lParam)
        {
            m_pAddinSimulationManager.RibbonWndProc(MsgID, wParam, lParam);
        }
        #endregion

        #region IFileSettingsTreeViewEvents
        public void OnActivateSnapshot(string FilePath, string GroupName, string SnapshotName)
        {
            m_pAddinSimulationManager.OnActivateSnapshot(FilePath, GroupName, SnapshotName);
        }

        public void OnAddSnapshot(string FilePath, string GroupName, string SnapshotName)
        {
            m_pAddinSimulationManager.OnAddSnapshot(FilePath, GroupName, SnapshotName);
        }

        public void OnDeleteSnapshot(string FilePath, string GroupName, string SnapshotName)
        {
            m_pAddinSimulationManager.OnDeleteSnapshot(FilePath, GroupName, SnapshotName);
        }

        public void OnDeleteAllSnapshot(string FilePath)
        {
            m_pAddinSimulationManager.OnDeleteAllSnapshot(FilePath);
        }
        #endregion
    }
}