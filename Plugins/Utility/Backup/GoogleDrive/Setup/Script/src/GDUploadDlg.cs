using Google.Apis.Drive.v2;
using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FilesResource = Google.Apis.Drive.v3.FilesResource;

namespace goolgedrivetest
{
    public partial class GDUploadDlg : Form
    {
        string m_strFileNme;
        byte[] m_strFileContnt;
        bool fromBrowse;
        public Google.Apis.Drive.v3.DriveService _driveService;
        public GDUploadDlg()
        {
            InitializeComponent();
            Googledrivelogin s = new Googledrivelogin();
                _driveService = s.GetAuth();
            
            this.txtGDUploadFile.Text = goolgedrivetestImp.m_strFilePath;

            this.txtGDLocation.Text = Properties.Settings.Default.dfltUploadLoc;
      
        }

        private void btnUploadDlgCancel_Click(object sender, EventArgs e)
        {
            this.Close();



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnUploadFileBrowse_Click(object sender, EventArgs e)
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
                this.txtGDUploadFile.Text = path;

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
                MessageBox.Show(ex.Message, "EurekaSim  Addin");
            }
        }
        private void btnDBLocBrowse_Click(object sender, EventArgs e)
        {
            try
            {

                GDLocationcDlg locDialog = new GDLocationcDlg(_driveService, this.Text, this);
                var pathsel = locDialog.ShowDialog();
               
                this.txtGDLocation.Text = locDialog.UploadLocation;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim google drive Addin");
            }

        }

        private void btnGDUpload_Click(object sender, EventArgs e)
        {
           
            String fileneedupload = this.txtGDUploadFile.Text;
            string drivelocation = this.txtGDLocation.Text;
            String folderId = "1FVCKpLDbbEK78vl6WNstO3ScVl7g-tO4";


            FilesResource.ListRequest listRequest = _driveService.Files.List();
            listRequest.PageSize = 100;
            listRequest.Fields = "nextPageToken, files(id, name)";

            
            var filess = listRequest.Execute().Files;

            if (filess != null && filess.Count > 0)
            {
                foreach (var f in filess)
                {
                    if (f.Name == drivelocation)
                        folderId = f.Id;
                    
                }
            }
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {

                
                Name = Path.GetFileName(drivelocation),
                
                Parents = new List<string>
                {
                   folderId
                }
            };
            fileMetadata.Name = Path.GetFileName(fileneedupload);
            fileMetadata.MimeType = Path.GetExtension(fileneedupload);
            
            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(fileneedupload,
                       FileMode.Open))
            {
                request = _driveService.Files.Create(
                    fileMetadata, stream, "image/jpeg");
                request.Fields = "id";
                request.Upload();
            }
            
            var file = request.ResponseBody;
            MessageBox.Show(" Uploaded Successfully File");
        }
    }
}
