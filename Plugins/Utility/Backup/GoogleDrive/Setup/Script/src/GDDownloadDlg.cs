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
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace goolgedrivetest
{
    public partial class GDDownloadDlg : Form
    {
        string m_strfileName = "";
        string m_strLocalPath = Googledrivelogin.m_downloadlocation;
        public Google.Apis.Drive.v3.DriveService _driveService;
        public static string fileid = "";
        public GDDownloadDlg()
        {
            InitializeComponent();
            Googledrivelogin s = new Googledrivelogin();
           
                _driveService = s.GetAuth();

            if (string.IsNullOrEmpty(Properties.Settings.Default.dfltDownloadLoc))
            {
                var localDir = System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\EurekaSim_GD_Backup\\");
                m_strLocalPath = localDir.FullName;
            }
            else
            {
                m_strLocalPath = Properties.Settings.Default.dfltDownloadLoc;

            }

        }



        private void btnDBLocBrowse_Click_1(object sender, EventArgs e)
        {
            GDLocationcDlg locDialog = new GDLocationcDlg(_driveService, this.Text, this);

            locDialog.ShowDialog();

            txtGDDowloadFile.Text = locDialog.DownloadLocation;

        }

        private void btnDBDownload_Click_1(object sender, EventArgs e)
        {
            if (this.txtGDDowloadFile.Text != string.Empty)
            {
                var upDlg = new FolderBrowserDialog();
                upDlg.SelectedPath = m_strLocalPath;
                upDlg.Description = "Browse Download Location";
                DialogResult result = upDlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    //  m_strLocalPath = upDlg.SelectedPath + "\\";

                    m_strLocalPath = upDlg.SelectedPath + "\\";
                    FilesResource.ListRequest listRequest = _driveService.Files.List();
                    listRequest.PageSize = 100;
                    listRequest.Fields = "nextPageToken, files(id, name)";

                    // List files.
                    IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
                    files = listRequest.Execute().Files;
                    string filenametxt = txtGDDowloadFile.Text;
                    string selectedfiledow = filenametxt.Substring(filenametxt.IndexOf("\\") + 1);
                   
                    if (files != null && files.Count > 0)
                    {
                        
                        foreach (var file in files)
                        {
                            if (file.Name == selectedfiledow)
                            {
                                
                                fileid = file.Id;
                            }


                        }

                    }
                    
                    FilesResource.GetRequest request = _driveService.Files.Get(fileid);
                    string filename = request.Execute().Name;
                    string filepath = Path.Combine(m_strLocalPath, filename);
                    MemoryStream stream = new MemoryStream();
                    request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                {
                                    MessageBox.Show("trying to download");
                                    break;
                                }
                            case DownloadStatus.Completed:
                                {

                                    SaveStream(stream, filepath);
                                    MessageBox.Show("Download complete.");
                                    break;
                                }
                            case DownloadStatus.Failed:
                                {
                                    MessageBox.Show("Download failed.");
                                    break;
                                }
                        }
                    };
                    request.Download(stream);

                    string pathopen = m_strLocalPath.Remove(m_strLocalPath.Length - 1, 1);
                    if (chkOpenAftDownload.Checked == true)
                    {
                        ApplicationDocument doc = new ApplicationDocument();
                        doc.OpenDocument(m_strLocalPath + filename);

                    }
                }
            }

            else
            {
                MessageBox.Show("Select a file  to download");
            }




        }
        public static void SaveStream(MemoryStream stream, string filepath)
        {
            using (System.IO.FileStream file = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                stream.WriteTo(file);
        }

        private void btnUploadDlgCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
