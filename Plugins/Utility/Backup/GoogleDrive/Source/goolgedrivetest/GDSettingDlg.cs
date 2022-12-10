using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Util.Store;
using System.Threading;
using System.IO;
using System.Net;

using DriveService = Google.Apis.Drive.v3.DriveService;
using Google.Apis.Drive.v2.Data;
//using Google.Apis.Auth.OAuth2.Data;
namespace goolgedrivetest
{

    public partial class GDSettingDlg : Form
    {
        private bool m_bAutoUpload;
        public string m_strDfltUpLoc;
        public string m_strDfltDownLoc;
        private Googledrivelogin m_objGoogledrivelogin;
        static DriveService _driveservice;
        public static string gdaccesstoken = Properties.Settings.Default.accessToken;
        public static string useremail=Properties.Settings.Default.emailid;

        public GDSettingDlg()
        {
            InitializeComponent();
            m_objGoogledrivelogin=new Googledrivelogin();

            var gdaccesstoken = Properties.Settings.Default.accessToken;
           
            if (string.IsNullOrEmpty(gdaccesstoken))
            {
               
                btnLogInOut.Text = "Login";
                statusBarStatus.Text = "kindly login ";
            }
            else
            {
               
                btnLogInOut.Text = "Logged";
                this.txtDfltFileDownLoc.Text = Properties.Settings.Default.dfltDownloadLoc;
                this.txtDfltFileUpLoc.Text = Properties.Settings.Default.dfltUploadLoc;
                statusBarStatus.Text = "Updating ";
                _driveservice = m_objGoogledrivelogin.GetAuth();
                statusBarStatus.Text = "Logged as ";
                statusBarAccName.Text = useremail; 


            }

            if (Properties.Settings.Default.dfltDownloadLoc == "")
            {
                m_strDfltDownLoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\EurekaSim_GD_Backup\\";
                System.IO.Directory.CreateDirectory(m_strDfltDownLoc);
                Properties.Settings.Default.dfltDownloadLoc = m_strDfltDownLoc;
                Properties.Settings.Default.Save();
            }
            else
            {
                m_strDfltDownLoc = Properties.Settings.Default.dfltDownloadLoc;
            }


        }




        private void btnLogInOut_Click(object sender, EventArgs e)
        {

            if (btnLogInOut.Text == "Login")
            { 
                statusBarStatus.Text = "Please wait...";
                Googledrivelogin.loginstatus = true;
                 _driveservice= m_objGoogledrivelogin.GetAuth();
                btnLogInOut.Text = "Logged";
                var request = _driveservice.About.Get();
                request.Fields = "user";
                var about = request.Execute();
                statusBarStatus.Text = "Logined as  ";
                Properties.Settings.Default.emailid = about.User.EmailAddress;
                useremail= Properties.Settings.Default.emailid;
        statusBarAccName.Text = useremail;
            }
            else if(btnLogInOut.Text == "Logged")
            {
                bool status = m_objGoogledrivelogin.logoutfun();
                gdaccesstoken = string.Empty;
                
                Properties.Settings.Default.accessToken = string.Empty;
                Properties.Settings.Default.refreshToken = string.Empty;
                Properties.Settings.Default.tokenExpiresAt = new DateTime();
                Properties.Settings.Default.dfltDownloadLoc = string.Empty;
                Properties.Settings.Default.dfltUploadLoc = string.Empty;
                Properties.Settings.Default.emailid = string.Empty;
                useremail =string.Empty; 
                Properties.Settings.Default.Save();
                statusBarAccName.Text = string.Empty;
                statusBarStatus.Text = "Logged Out";
                btnLogInOut.Text = "Login";
                this.txtDfltFileUpLoc.Text = string.Empty;
                this.txtDfltFileDownLoc.Text = string.Empty;

                
            }
        }
       
          

        private void btnDfltUpLocBrowse_Click_1(object sender, EventArgs e)
        {
            var gaccesstoken = Properties.Settings.Default.accessToken;


            if (string.IsNullOrEmpty(gaccesstoken))
            {

                MessageBox.Show("Kindly Login First");
            }
            else
            {

                m_objGoogledrivelogin.GetAuth();
                GDLocationcDlg lpcdlg = new GDLocationcDlg(_driveservice, this.Text, this);
                lpcdlg.ShowDialog();

                m_strDfltUpLoc = lpcdlg.UploadLocation;
                txtDfltFileUpLoc.Text = m_strDfltUpLoc;

                Properties.Settings.Default.dfltUploadLoc = m_strDfltUpLoc;
            }

        }

        private void btnDfltDownLocBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog upDlg = new FolderBrowserDialog();
            upDlg.Description = "Browse Default Download Location";
            var rep = upDlg.ShowDialog();
            if (rep == DialogResult.OK)
            {
                m_strDfltDownLoc = upDlg.SelectedPath;
                txtDfltFileDownLoc.Text = m_strDfltDownLoc;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSettingsSave_Click(object sender, EventArgs e)
        {
           
            if (_driveservice != null)
            {

                try
                {
                    Properties.Settings.Default.autoUpload = m_bAutoUpload;
                    Properties.Settings.Default.dfltUploadLoc = m_strDfltUpLoc;
                    Properties.Settings.Default.dfltDownloadLoc = m_strDfltDownLoc;
                    Properties.Settings.Default.Save();

                  
                    this.txtDfltFileUpLoc.Text = m_strDfltUpLoc;

                    this.txtDfltFileDownLoc.Text = m_strDfltDownLoc;
                 

                    this.statusBarStatus.Text = "Settings succesfully saved.";
                    MessageBox.Show("Settings succesfully saved.", "EurekaSim Googledrive Addin");
                }
                catch(Exception  ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                this.statusBarStatus.Text = "Login to save settings.";
            }
        }

        private void chkAutoBackup_CheckedChanged(object sender, EventArgs e)
        {

            if (chkAutoBackup.Checked == true)
            {
                m_bAutoUpload = true;
                Googledrivelogin.m_uploadlocation = m_strDfltUpLoc;
                Googledrivelogin.m_downloadlocation = m_strDfltDownLoc;
                 this.txtDfltFileDownLoc.Text = m_strDfltDownLoc;
                    this.txtDfltFileUpLoc.Text = m_strDfltUpLoc;
            }
            else if (chkAutoBackup.Checked == false)
            {
                m_bAutoUpload = false;
                
            }
        }
    }
}
