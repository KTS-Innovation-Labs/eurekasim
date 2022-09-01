using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dropbox.Api;
using Dropbox.Api.Users;
using Dropbox.Api.Files;
using EurekaSim.Net;

namespace Dropbox
{
    public partial class UploadDialog : Form
    {
        private string m_accessToken = "";
        DropboxClient m_dbClient;
        FullAccount m_details;
        string m_strFileNme;
        byte[] m_strFileContnt;
        bool fromBrowse;
        public UploadDialog()
        {
            InitializeComponent();
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(768 | 3072);

            this.Text = "Upload to Dropbox";
            m_accessToken = Properties.Settings.Default.accessToken;
            m_details = null;
            m_dbClient = null;
            fromBrowse = false;
            this.txtDBUploadFile.Text = DropboxImp.m_strFilePath;
            m_strFileNme = DropboxImp.m_strFileName;
            m_strFileContnt = DropboxImp.m_strFileContent;

            if (string.IsNullOrEmpty(m_accessToken))
            {
                statusBarStatus.Text = "Logged Out.Please Login..";
            }
            else
            {
                this.statusBarStatus.Text = "Updating...";

                GetDBClient();

                this.txtDBLocation.Text = Properties.Settings.Default.dfltUploadLoc;
            }
        }

        private async void GetDBClient()
        {
            try
            {
                m_dbClient = await DBLogin.CreateDBClient();
                if (m_dbClient != null)
                {
                    bool folderExixits = false;
                    DisplayAccountDetails();
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
                    this.txtDBLocation.Text = Properties.Settings.Default.dfltUploadLoc;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
            }
        }

        //Upload form cancel.
        private void BtnDBUploadDlgCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Upload button.
        private async  void BtnDBUpload_Click(object sender, EventArgs e)
        {
            if (DropboxImp.m_saveCheck != 2 && fromBrowse == false)
            {
                MessageBox.Show("Eurekasim file is not saved. Please save it first", "EurekaSim Dropbox Addin");
                return;
            }
            if (m_dbClient != null)
            {
                try
                {
                    this.statusBarStatus.Text = "Uploading...";
                    FileMetadata resp =  await Upload(m_dbClient, txtDBLocation.Text, m_strFileNme, m_strFileContnt);
                    if (resp != null)
                    {
                        this.statusBarStatus.Text = "Succssfully uploaded.";
                        MessageBox.Show("File uploaded succssfully.", "EurekaSim Dropbox Addin");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
                }
            }
            else
            {
                MessageBox.Show("You are not logged in. Please login to Dropbox first", "EurekaSim Dropbox Addin");
            }

        }

        //Dropbox location browse button event.
        private void BtnDBLocBrowse_Click(object sender, EventArgs e)
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

        //Get account details
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
                        this.statusBarAccCountry.Text = m_details.Country;
                        this.statusBarStatus.Text = "Logged In";
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
                statusBarAccCountry.Text = "";
            }
        }

        //Set Selected location text.
        public void SetSelectedLoc(string folder)
        {
            this.txtDBLocation.Text = folder;
        }

        private async Task<FileMetadata> Upload(DropboxClient client, string folder, string fileName, byte[] fileContent)
        {
            FileMetadata response = null;
            try
            {
                using (var stream = new MemoryStream(fileContent))
                {
                    response = await client.Files.UploadAsync("/" + folder + "/" + fileName, WriteMode.Overwrite.Instance, body: stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
            }
            return response;
        }

        //Create default backup folder.
        private async Task CreateFolder(DropboxClient client, string path)
        {
            var folderArg = new CreateFolderArg(path);
            var folder = await client.Files.CreateFolderAsync(folderArg);
        }

        private void TxtUploadFile_Changed(object sender, EventArgs e)
        {
            txtDBUploadFile.Update();
            txtDBUploadFile.Focus();
            txtDBUploadFile.SelectionStart = txtDBUploadFile.Text.Length;
            txtDBUploadFile.ScrollToCaret();
        }

        private void TxtDropbocLoc_Changed(object sender, EventArgs e)
        {
            txtDBLocation.Update();
            txtDBLocation.Focus();
            txtDBLocation.SelectionStart = txtDBLocation.Text.Length;
            txtDBLocation.ScrollToCaret();
        }

        private void BtnUploadFileDialog_Click(object sender, EventArgs e)
        {
            string path = "";
            OpenFileDialog uploadFileDlg = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Eurekasim Files",
                DefaultExt = "psl",
                Filter = "Eurekasim Files (*.psl)|*.psl|All files (*.*)|*.*",
            };
            if (uploadFileDlg.ShowDialog() == DialogResult.OK)
            {
                fromBrowse = true;
                path = uploadFileDlg.FileName;
                GetCurrentFile(path);
                this.txtDBUploadFile.Text = path;
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
                    m_strFileNme = file;
                    m_strFileContnt = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
            }
        }
    }
}
