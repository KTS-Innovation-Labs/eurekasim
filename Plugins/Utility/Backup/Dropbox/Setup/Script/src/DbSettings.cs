using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dropbox
{
    public partial class DbSettings : Form
    {
        private static string m_accessToken = "";
        DropboxClient m_dbClient;
        FullAccount m_details;
        private bool m_bAutoUpload;
        private string m_strDfltUpLoc;
        private string m_strDfltDownLoc;

        public DbSettings()
        {
            InitializeComponent();
            this.Text = "Dropbox Settings";
            m_accessToken = Properties.Settings.Default.accessToken;
            m_dbClient = null;
            m_details = null;
            m_bAutoUpload = false;
            this.btnDfltUpLocBrowse.Enabled = false;

            if (Properties.Settings.Default.dfltDownloadLoc == "")
            {
                m_strDfltDownLoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\EurekaSim_DB_Backup\\";
                System.IO.Directory.CreateDirectory(m_strDfltDownLoc);
                Properties.Settings.Default.dfltDownloadLoc = m_strDfltDownLoc;
                Properties.Settings.Default.Save();
            }
            else
            {
                m_strDfltDownLoc = Properties.Settings.Default.dfltDownloadLoc;
            }

            txtDfltFileDownLoc.Text = m_strDfltDownLoc;

            chkAutoBackup.Checked = Properties.Settings.Default.autoUpload;
            if (string.IsNullOrEmpty(m_accessToken))
            {
                statusBarStatus.Text = "Logged Out.Please Login..";
                btnLogInOut.Text = "Login";
                this.txtDfltFileUpLoc.Text = "";
            }
            else
            {
                statusBarStatus.Text = "Updating...";
                btnLogInOut.Text = "Logout";
                GetDBClient();
                m_strDfltUpLoc = Properties.Settings.Default.dfltUploadLoc;
                this.txtDfltFileUpLoc.Text = m_strDfltUpLoc;
            }

        }

        //Login/Logout.
        private async void BtnLogInOut_Click(object sender, EventArgs e)
        {
            if (btnLogInOut.Text == "Login")
            {
                statusBarStatus.Text = "Please wait...";

                await GetDBClient();
            }
            else if (btnLogInOut.Text == "Logout")
            {
                m_accessToken = string.Empty;
                m_dbClient = null;
                m_details = null;
                Properties.Settings.Default.accessToken = string.Empty;
                Properties.Settings.Default.refreshToken = string.Empty;
                Properties.Settings.Default.tokenExpiresAt = new DateTime();
                Properties.Settings.Default.Save();
                statusBarAccName.Text = string.Empty;
                statusBarStatus.Text = "Logged Out";
                btnLogInOut.Text = "Login";
                this.txtDfltFileUpLoc.Text = string.Empty;
            }
        }

        //Get client.
        private async Task GetDBClient()
        {
            try
            {
                m_dbClient = await DBLogin.CreateDBClient();
                if (m_dbClient != null)
                {
                    bool folderExixits = false;
                    await DisplayAccountDetails();
                    var list = await m_dbClient.Files.ListFolderAsync(string.Empty);
                    
                    foreach (var item in list.Entries.Where(i => i.IsFolder))
                    {
                        if (item.Name == "EurekaSim_Backup")
                        {
                            folderExixits = true;
                        }
                    }
                    if (folderExixits != true)
                    {
                        await CreateFolder(m_dbClient, "/EurekaSim_Backup");
                    }
                    m_strDfltUpLoc = Properties.Settings.Default.dfltUploadLoc;
                    this.txtDfltFileUpLoc.Text = m_strDfltUpLoc;
                    this.btnDfltUpLocBrowse.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
            }
        }

        public async Task DisplayAccountDetails()
        {
            if (m_dbClient != null)
            {
                try
                {
                    m_details = await m_dbClient.Users.GetCurrentAccountAsync();
                    MethodInvoker inv = delegate
                    {
                        this.statusBarAccName.Text = m_details.Name.DisplayName;
                        this.statusBarStatus.Text = "Logged In";
                        this.btnLogInOut.Text = "Logout";
                    };
                    this.Invoke(inv);

                }
                catch (Exception)
                {
                    MessageBox.Show("Error in getting dropbox account details", "EurekaSim Dropbox Addin");
                }
            }
            else
            {
                statusBarAccName.Text = "";
            }
        }

        //Dialog close.
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Save.
        private void BtnSettingsSave_Click(object sender, EventArgs e)
        {
            if (m_dbClient != null)
            {
                Properties.Settings.Default.autoUpload = m_bAutoUpload;
                Properties.Settings.Default.dfltUploadLoc = m_strDfltUpLoc;
                Properties.Settings.Default.dfltDownloadLoc = m_strDfltDownLoc;
                Properties.Settings.Default.Save();

                this.txtDfltFileUpLoc.Text = m_strDfltUpLoc;
                this.txtDfltFileDownLoc.Text = m_strDfltDownLoc;
                this.statusBarStatus.Text = "Settings succesfully saved.";
                MessageBox.Show("Settings succesfully saved.", "EurekaSim Dropbox Addin");
            }
            else
            {
                this.statusBarStatus.Text = "Login to save settings.";
            }
        }

        //Browse default upload location.
        private void BtnBrowseUploadLoc_Click(object sender, EventArgs e)
        {
            if (m_dbClient != null)
            {
                DBLocationDialog locDialog = new DBLocationDialog(m_dbClient, m_details.Name.DisplayName, this);
                locDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("You are not logged in. Please login to Dropbox first", "EurekaSim Dropbox Addin");
            }

        }

        //Browse default download location.
        private void BtnBrowseDownloadLoc_Click(object sender, EventArgs e)
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

        //Set default download location.
        public void SetDefaultUploadLoc(string loc)
        {
            this.txtDfltFileUpLoc.Text = loc;
            m_strDfltUpLoc = loc;
        }

        //Checkbox check changed.
        private void ChkAutoSave_Changed(object sender, EventArgs e)
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
            Properties.Settings.Default.Save();
        }

        //Create default backup folder.
        private async Task CreateFolder(DropboxClient client, string path)
        {
            try
            {
                var folderArg = new CreateFolderArg(path);
                var folder = await client.Files.CreateFolderAsync(folderArg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
            }
           
        }

        private void TxtDfltUpLoc_Changed(object sender, EventArgs e)
        {
            txtDfltFileUpLoc.SelectionStart = txtDfltFileUpLoc.Text.Length;
            txtDfltFileUpLoc.ScrollToCaret();
            txtDfltFileUpLoc.Refresh();
        }

        private void TxtDfltDownLoc_Changed(object sender, EventArgs e)
        {
            txtDfltFileDownLoc.Update();
            txtDfltFileDownLoc.SelectionStart = txtDfltFileDownLoc.Text.Length;
            txtDfltFileDownLoc.ScrollToCaret();
            txtDfltFileDownLoc.Refresh();
        }
    }
}
