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
using System.Runtime;
using Google.Apis;
using Google.Apis.Auth;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v2;
using DriveService = Google.Apis.Drive.v3.DriveService;
using FilesResource = Google.Apis.Drive.v3.FilesResource;

namespace goolgedrivetest
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]

    //Change this GUID for your own plugin...

    [Guid("600dd509-4436-46aa-9714-1284ba469b68")]

    public class goolgedrivetestImp : EurekaSim.Net.Addin,
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
        public static string m_strFileName;
        public static byte[] m_strFileContent;
        public static string m_strFilePath;
        public static int m_saveCheck;

        #endregion

        #region Constructor
        public goolgedrivetestImp()
        {
            m_pAddinSimulationManager=new AddinSimulationManager(this); //Addin Simulation Manager
            m_lSessionID = -1;
            m_ExperimentName="";
            m_strFileName = "";
            m_strFileContent = null;
            m_saveCheck = 0;
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
            // TODO:  Add CAddinDotNet.SalesMatePlusLib.ISmpAddin.Initialize implementation
            //MessageBox.Show("C#.Initialize");
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            //Read the embedded XML menu resource
            Stream rgbxml = thisAssembly.GetManifestResourceStream("goolgedrivetest.AddinRibbon.xml");

            Stream rgbBmp = thisAssembly.GetManifestResourceStream("goolgedrivetest.toolbar1.bmp");

            Bitmap bmp = new Bitmap(rgbBmp);

            XmlDocument doc = new XmlDocument();
            doc.Load(rgbxml);
            LoadAddinName(doc);
            string strMenuXML = doc.InnerXml;
            //MessageBox.Show(strMenuXML);
            IntPtr bitMapHandle = bmp.GetHbitmap();
            m_lSessionID = lSessionID;
            pApp.SetAddInInfo(lSessionID, 0, strMenuXML, (long)bitMapHandle, "");
            Experiment experiment = new Experiment();
            experiment.AddExperiment(lSessionID,Constants.CS_SAMPLE_MAIN_EXPERIMENT_NAME);
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
            MessageBox.Show("The C# Addin About Box");
        }
        #endregion

        #region From XML UI Commands
        public void InvokePreferenceSettings()
        {
            MessageBox.Show("C#.InvokePreferenceSettings");
        }
       
        public void SetRibbonControlText(string ID, string Text)
        {
            (new RibbonToolbar()).SetControlText(ID, Text);
        }

        public void InvokeDefaultSettings()
        {
            MessageBox.Show("C#.InvokeDefaultSettings");
        }

        public void InvokeUploadDlg() {
            var login = Googledrivelogin.loginstatus;
           var gdaccesstoken= Properties.Settings.Default.accessToken;
            if (string.IsNullOrEmpty(gdaccesstoken))
            {
                DialogResult result = MessageBox.Show("You are not logged in to googledrive. Continue to login?", "EurekaSim GoogleDrive Backup", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    GDSettingDlg settingsDlg = new GDSettingDlg();
                    settingsDlg.ShowDialog();
                }
                else
                {
                    return;
                }
            }
            else
            {
                GDUploadDlg uploadDialog = new GDUploadDlg();
                uploadDialog.ShowDialog();

            }
        }

        public void InvokeDownloadDlg()
        {
            var login = Googledrivelogin.loginstatus;
            var gdaccesstoken = Properties.Settings.Default.accessToken;

            if (string.IsNullOrEmpty(gdaccesstoken))
            {
                DialogResult result = MessageBox.Show("You are not logged in to Google drive. Continue to login?", "EurekaSim Googledrive Backup", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    GDSettingDlg settingsDlg = new GDSettingDlg();
                    settingsDlg.ShowDialog();
                }
                else
                {

                    return;
                }
            }
            else
            {

                GDDownloadDlg downloadDlg = new GDDownloadDlg();
                downloadDlg.ShowDialog();

            }

        }

        public void InvokeSettingsDlg()
        {
            GDSettingDlg settingsDlg = new GDSettingDlg();
            settingsDlg.ShowDialog();
        }

        public void InvokeAboutDlg()
        {
            AboutGD aboutDlg = new AboutGD();
            aboutDlg.ShowDialog();
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
            m_saveCheck = 1;
            m_strFilePath = DocumentPath;
           
            m_pAddinSimulationManager.OnBeforeSaveDocument(DocumentPath);
        }
		public void OnAfterSaveDocument(string DocumentPath)
        {
            m_saveCheck = 2;

            GetCurrentFile(DocumentPath);
             bool autoSave = Properties.Settings.Default.autoUpload;
             string token = Properties.Settings.Default.accessToken;
             string dfltUploadLoc = Properties.Settings.Default.dfltUploadLoc;
            string fileNme = m_strFilePath;
            byte[] content = m_strFileContent;
           
            if (autoSave && token != "")
            {

                DriveService _driveService;
                Googledrivelogin s = new Googledrivelogin();
                _driveService = s.GetAuth();
           


                String folderId = " ";


                FilesResource.ListRequest listRequest = _driveService.Files.List();
                listRequest.PageSize = 100;
                listRequest.Fields = "nextPageToken, files(id, name)";

                
                var filess = listRequest.Execute().Files;

                if (filess != null && filess.Count > 0)
                {
                    foreach (var f in filess)
                    {
                        if (f.Name == dfltUploadLoc)
                            folderId = f.Id;

                    }
                }
                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {

                    
                    Name = Path.GetFileName(dfltUploadLoc),
                   
                    Parents = new List<string>
                {
                   folderId
                }
                };
                fileMetadata.Name = Path.GetFileName(fileNme);
                fileMetadata.MimeType = Path.GetExtension(fileNme);
                

                FilesResource.CreateMediaUpload request;

                using (var stream = new FileStream(fileNme,
                           FileMode.Open))
                {
                    
                    request = _driveService.Files.Create(
                        fileMetadata, stream, "image/jpeg");
                    request.Fields = "id";
                    request.Upload();
                }
              

                var file = request.ResponseBody;

                MessageBox.Show("Backup successful", "Google Drive Backup");
            }
            else
            {
                MessageBox.Show("Kindly login");
            }
        }
        private void GetCurrentFile(string DocumentPath)
        {
            try
            {

                if (File.Exists(DocumentPath))
                {
                    var data = File.ReadAllBytes(DocumentPath);

                    var file = Path.GetFileName(DocumentPath);

                    m_strFileName = file;
                    m_strFileContent = data;
                    m_strFilePath = DocumentPath;
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Googledrive Addin");
            }
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
