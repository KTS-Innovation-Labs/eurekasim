using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Exceptions;
using Box.V2.JWTAuth;
using Box.V2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ESimBoxCloud
{
    public partial class SettingsForm : Form
    {
       
      
      
        BoxClient m_Boxclient;
        private bool m_bAutoUpload;
        private string m_strDfltUpLoc;

        public string m_strDfitUplosdID { get;  set; }

        private string m_strDfltDownLoc;
        public SettingsForm(BoxClient boxclient)
        {
            m_Boxclient = boxclient;
            InitializeComponent();


            if (Properties.Settings.Default.dfltDownloadLoc == "")
            {
                m_strDfltDownLoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\EurekaSim_Box_Backup\\";
                System.IO.Directory.CreateDirectory(m_strDfltDownLoc);
                Properties.Settings.Default.dfltDownloadLoc = m_strDfltDownLoc;

                Properties.Settings.Default.Save();
            }
            else
            {
                m_strDfltDownLoc = Properties.Settings.Default.dfltDownloadLoc;


            }

            txtboxDefaultDownloadLocation.Text = m_strDfltDownLoc;

            ChkAutoBackUp.Checked = Properties.Settings.Default.autoUpload;
            if (m_Boxclient == null)
            {
                LoginStatusStrip.Text = "Logged Out.Please Login..";
                btnLogin.Text = "Login";
                this.txtboxDefaultUploadLoc.Text = "";
            }
            else
            {
                LoginStatusStrip.Text = "Updating...";
                btnLogin.Text = "Logout";
                GetCurrentUserDetails();
                CreateFolder();
                m_strDfltUpLoc = Properties.Settings.Default.dfltUploadLoc;
                m_strDfitUplosdID= Properties.Settings.Default.UpldId;
                txtboxDefaultUploadLoc.Text = m_strDfltUpLoc;
               
            }

        }

       
        private async Task CreateFolder()
        {
            if (m_Boxclient != null)
            {
                // Create a new folder in the user's root folder
               
                var folderParams = new BoxFolderRequest()
                {
                    Name = "EurekaSim_Backup",
                    Parent = new BoxRequestEntity()
                    {
                        Id = "0"
                    }

                };
                BoxFolder folder = await m_Boxclient.FoldersManager.CreateAsync(folderParams);
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (btnLogin.Text == "Login")
            {
               LoginStatusStrip.Text = "Please wait...";
                APIManager.SigInProcessAsync();
                
                
            }
            else if (btnLogin.Text == "Logout")
            {
                APIManager.Logout();
                this.NameStatusStrip.Text = "";
                LoginStatusStrip.Text = "Logged Out";
                btnLogin.Text = "Login";
               
            }
            
        }

        public async Task GetCurrentUserDetails()
        {

            if (m_Boxclient != null)
            {
                try
                {
                    BoxUser currentUser = await m_Boxclient.UsersManager.GetCurrentUserInformationAsync();
                    MethodInvoker inv = delegate
                    {
                        this.NameStatusStrip.Text = currentUser.Name;
                        LoginStatusStrip.Text = "Logged In";

                        this.btnLogin.Text = "Logout";
                    };
                    this.Invoke(inv);

                }
                catch (Exception)
                {
                    MessageBox.Show("Error in getting box account details", "EurekaSim Box Addin");
                }
            }
            else
            {
                this.NameStatusStrip.Text = "";
            }
            
        }
        private void btnUploadLocBrowse_Click(object sender, EventArgs e)
        {
           
            if (m_Boxclient != null)
            {
                BoxLocation locDialog = new BoxLocation(m_Boxclient);
                
                locDialog.ShowDialog();
                txtboxDefaultUploadLoc.Text= BoxLocation.setvaluefortxtBoxLocSelect;
                m_strDfltUpLoc = txtboxDefaultUploadLoc.Text;
                // m_strDfitUplosdID =(string) txtboxDefaultUploadLoc.Tag;
                m_strDfitUplosdID = Properties.Settings.Default.UpldId;
            }
            else
            {
                MessageBox.Show("You are not logged in. Please login to Box first", "EurekaSim Box Addin");
            }
        }

        private void btnDownloadLocBrowse_Click(object sender, EventArgs e)
        {
            if (m_Boxclient != null)
            {
                FolderBrowserDialog upDlg = new FolderBrowserDialog();
                upDlg.Description = "Browse Default Download Location";
                var rep = upDlg.ShowDialog();
                if (rep == DialogResult.OK)
                {
                    m_strDfltDownLoc = upDlg.SelectedPath;
                    txtboxDefaultDownloadLocation.Text = m_strDfltDownLoc;
                }
            }
            else
            {
                MessageBox.Show("You are not logged in. Please login to Box first", "EurekaSim Box Addin");
            }
        }

        private void txtboxDefaultDownloadLocation_TextChanged(object sender, EventArgs e)
        {
            txtboxDefaultDownloadLocation.Update();
            txtboxDefaultDownloadLocation.SelectionStart = txtboxDefaultDownloadLocation.Text.Length;
            txtboxDefaultDownloadLocation.ScrollToCaret();
            txtboxDefaultDownloadLocation.Refresh();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (m_Boxclient != null)
            {
                
                Properties.Settings.Default.autoUpload = m_bAutoUpload;
                Properties.Settings.Default.dfltUploadLoc = m_strDfltUpLoc;
                Properties.Settings.Default.dfltDownloadLoc = m_strDfltDownLoc;
                Properties.Settings.Default.UpldId = m_strDfitUplosdID;
                Properties.Settings.Default.Save();

                this.txtboxDefaultUploadLoc.Text = m_strDfltUpLoc;
                this.txtboxDefaultDownloadLocation.Text = m_strDfltDownLoc;
                this.LoginStatusStrip.Text = "Settings succesfully saved.";
                MessageBox.Show("Settings succesfully saved.", "EurekaSim Box Addin");
            }
            else
            {
                this.LoginStatusStrip.Text = "Login to save settings.";
            }
        }

        private void ChkAutoBackUp_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAutoBackUp.Checked == true)
            {
                m_bAutoUpload = true;
            }
            else if (ChkAutoBackUp.Checked == false)
            {
                m_bAutoUpload = false;
            }
            Properties.Settings.Default.autoUpload = m_bAutoUpload;
            Properties.Settings.Default.Save();
        }

        private void txtboxDefaultUploadLoc_TextChanged(object sender, EventArgs e)
        {
            txtboxDefaultUploadLoc.SelectionStart = txtboxDefaultUploadLoc.Text.Length;
            txtboxDefaultUploadLoc.ScrollToCaret();
            txtboxDefaultUploadLoc.Refresh();
        }
    }


}
