using Newtonsoft.Json;
using System;
using System.Windows.Forms;
using WebCloud.Api.DS;
using ManagerDll.Menu;
using ManagerDll.Services;
using ManagerDll.TestData;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using System.Net.Http;
using static ManagerDll.TestData.ApiTestData;

namespace ManagerDll
{
    public partial class Settings : Form
    {
       
        public AppApiService ApiService = null;
        AuthInfo AuthInfo;
        public ApiTestData objTestDataUtil;
        private bool m_bAutoUpload;
        private List<UserAccountInfo> result;
        public ApiMenuTestDataUtil objMenuTestData;
        public bool Autoup;
        public string destpath;
        public bool AutoUpload;
       
        public object ObjMainForm { get; private set; }

        public Settings()
        {
            InitializeComponent();
            AuthInfo = new AuthInfo();
            objTestDataUtil = new ApiTestData(this);
            m_bAutoUpload = false;
            
        }

        public void SetStatusText(string status)
        {
            toolStripStatusLabelRequestURL.Text = status;
        }
        public string GetLoginButtonText()
        {
            return buttonLogin.Text;
        }
        public string GetMainURL()
        {
            return textBoxURL.Text;
        }
        public string GetUserID()
        {
            return textBoxUserID.Text;
            
        }
        public string GetPassword()
        {
            return textBoxPassword.Text;
        }
        public void SetRequestURL(string strRequestURL)
        {
            toolStripStatusLabelRequestURL.Text = "Logged In";
        }

        public void SetLoginButtonText(string strName)
        {
            buttonLogin.Text = strName;
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            await objTestDataUtil.InvikeLoginValidationAPI();
            await objTestDataUtil.InvokeGetUserListForDeletionAPI(objTestDataUtil);
           
        }
       
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public string SetAutheticationToken(string Token)
        {
            return Token;
        }

        public void SaveDefaultValues()
        {
            try
            {
                Microsoft.Win32.RegistryKey ApiRegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("IDrive");
                ApiRegistryKey.SetValue("URL", textBoxURL.Text);
                ApiRegistryKey.SetValue("UserID", textBoxUserID.Text);
                ApiRegistryKey.SetValue("Password", textBoxPassword.Text);
                ApiRegistryKey.SetValue("LocalPath", textBoxLocalFilePath.Text);
                ApiRegistryKey.SetValue("DestinationPath", textBoxDestPath.Text);
                ApiRegistryKey.Close();
            }
            catch (Exception Ex)
            {
               MessageBox.Show(Ex.Message);
            }

        }
        public void LoadDefaultValues()
        {
            try
            {
                Microsoft.Win32.RegistryKey ApiRegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("IDrive");
                if (ApiRegistryKey != null)
                {
                    textBoxURL.Text = ApiRegistryKey.GetValue("URL").ToString();
                    textBoxUserID.Text = ApiRegistryKey.GetValue("UserID").ToString();
                    textBoxPassword.Text = ApiRegistryKey.GetValue("Password").ToString();
                    textBoxLocalFilePath.Text = ApiRegistryKey.GetValue("LocalPath").ToString();
                    textBoxDestPath.Text = ApiRegistryKey.GetValue("DestinationPath").ToString();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
           
            Properties.Settings.Default.dfltUploadLoc = textBoxDestPath.Text;
            destpath = textBoxDestPath.Text;
            Properties.Settings.Default.dfltDownLoc = textBoxLocalFilePath.Text;
            if (chkAutoBackup.Checked == true)
            {
                Properties.Settings.Default.autoUpload = true;
            }
            else
            {
                Properties.Settings.Default.autoUpload = false;
            }
            Properties.Settings.Default.Save();
            MessageBox.Show("Settings Saved Successfully","Eurekasim Addin");
           
        }
        private void buttonLocalPathBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                ListAllDirFilesForm LocalPath = new ListAllDirFilesForm(objTestDataUtil, true, false, textBoxLocalFilePath.Text);

                LocalPath.ShowDialog();
                textBoxLocalFilePath.Text = LocalPath.DestPath;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void buttonDestinationPathBrowse_Click(object sender, EventArgs e)
        {
           
                try
                {
                    ListAllDirFilesForm RemotePath = new ListAllDirFilesForm(objTestDataUtil, true, true, textBoxDestPath.Text);
                    RemotePath.ShowDialog();
                    textBoxDestPath.Text = RemotePath.DestPath;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                
            }

        private void chkAutoBackup_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoBackup.Checked == true)
            {
                m_bAutoUpload = true;
            }
            else if (chkAutoBackup.Checked == false)
            {
                m_bAutoUpload = false;
            }
            Properties.Settings.Default.autoUpload = m_bAutoUpload;

            Autoup = m_bAutoUpload;
            Properties.Settings.Default.Save();
           
        }

        public void SetCurrentUser(string name)
        {
            if (buttonLogin.Text == "Login")
            {
                toolStripStatusLabelUser.Text = "";
            }
            else
            {
                toolStripStatusLabelUser.Text = name;
            }
                
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            textBoxDestPath.Text = Properties.Settings.Default.dfltUploadLoc;
            textBoxLocalFilePath.Text = Properties.Settings.Default.dfltDownLoc;
            chkAutoBackup.Checked = (bool)Properties.Settings.Default.autoUpload;
        }
    }
  }

