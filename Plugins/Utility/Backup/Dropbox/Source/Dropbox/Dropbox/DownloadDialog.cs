using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EurekaSim.Net;

namespace Dropbox
{
    public partial class DownloadDialog : Form
    {
        private const string APP_KEY = "mt88f0a04tgq7ld";
        private string m_accessToken = "";
        DropboxClient m_dbClient;
        FullAccount m_details;
        string m_strfileName = "";
        string m_strLocalPath = "";
        public DownloadDialog()
        {
            InitializeComponent();
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(768 | 3072);
            
            this.Text = "Download from Dropbox";
            m_accessToken = Properties.Settings.Default.accessToken;
            m_details = null;
            m_dbClient = null;
            this.btnDBLocBrowse.Enabled = false;

            if (string.IsNullOrEmpty(Properties.Settings.Default.dfltDownloadLoc))
            {
                var localDir = System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\EurekaSim_DB_Backup\\");
                m_strLocalPath = localDir.FullName;
            }
            else
            {
                m_strLocalPath = Properties.Settings.Default.dfltDownloadLoc;
            }

            if (string.IsNullOrEmpty(m_accessToken))
            {
                statusBarStatus.Text = "Logged Out.Please Login..";
            }
            else
            {
                statusBarStatus.Text = "Updating...";

                GetDBClient();
            }
        }

        //Location Browse.
        private void BtnDBLocBrowse_Click(object sender, EventArgs e)
        {
            if (m_dbClient != null)
            {
                try
                {
                    DBLocationDialog locDialog = new DBLocationDialog(m_dbClient, m_details.Name.DisplayName, this);
                    locDialog.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
                }
            }
            else
            {
                MessageBox.Show("You are not logged in. Please login to Dropbox first", "Information");
            }
        }

        public async void DisplayAccountDetails()
        {
            if (m_dbClient != null)
            {
                try
                {
                    m_details = await m_dbClient.Users.GetCurrentAccountAsync();
                    MethodInvoker inv = delegate
                    {
                        this.statusBarAccName.Text = m_details.Name.DisplayName;
                        this.StatusBarAccCountry.Text = m_details.Country;
                        statusBarStatus.Text = "Logged In";
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
                StatusBarAccCountry.Text = "";
            }
        }

        private async void GetDBClient()
        {
            try
            {
                m_dbClient = await DBLogin.CreateDBClient();
                if (m_dbClient != null)
                {
                    DisplayAccountDetails();
                    this.btnDBLocBrowse.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
            }
        }

        //Download.
        private async void BtnDBDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtDBDowloadFile.Text != string.Empty)
                {
                    var upDlg = new FolderBrowserDialog();
                    upDlg.SelectedPath = m_strLocalPath;
                    upDlg.Description = "Browse Download Location";
                    DialogResult result = upDlg.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        m_strLocalPath = upDlg.SelectedPath + "\\";
                        this.statusBarStatus.Text = "Downloading...";
                        await Download(m_dbClient, this.txtDBDowloadFile.Text, m_strLocalPath, m_strfileName);
                        this.statusBarStatus.Text = "File successfully downloaded.";
                        MessageBox.Show("File successfully downloaded to " + m_strLocalPath, "EurekaSim Dropbox Addin");
                        if (chkOpenAftDownload.Checked == true)
                        {
                            ApplicationDocument doc = new ApplicationDocument();
                            doc.OpenDocument(m_strLocalPath + m_strfileName);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select a file Dropbox to download", "EurekaSim Dropbox Addin");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
            }
        }

        private async Task Download(DropboxClient client, string filePath, string localPath, string fileName)
        {
            try
            {
                using (var response = await client.Files.DownloadAsync(filePath))
                {
                    using (var fileStream = File.Create(localPath + fileName))
                    {
                        (await response.GetContentAsStreamAsync()).CopyTo(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
            }

        }

        //Set download file name.
        public void SetDownloadFile(string file, string fileName)
        {
            m_strfileName = fileName;
            file = "/" + file;
            this.txtDBDowloadFile.Text = file;
        }

        //Dialog close.
        private void BtnDownloadDlgClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtDowloadFile_Changed(object sender, EventArgs e)
        {
            txtDBDowloadFile.Update();
            txtDBDowloadFile.SelectionStart = txtDBDowloadFile.Text.Length;
            txtDBDowloadFile.ScrollToCaret();
            txtDBDowloadFile.Refresh();
        }
    }
}
