using Google.Apis.Drive.v3.Data;
using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Google.Apis.Requests;
using Google.Apis.Drive.v2;


using FilesResource = Google.Apis.Drive.v3.FilesResource;
using DriveService = Google.Apis.Drive.v3.DriveService;
using Google.Apis.Auth.OAuth2;

namespace goolgedrivetest
{

    public partial class GDLocationcDlg : Form
    {
        public Google.Apis.Drive.v3.DriveService _driveService;
        IList<Google.Apis.Drive.v3.Data.File> files;
        static bool folderExixits = false;
        public String UploadLocation = null;
        public String DownloadLocation = null;
        public GDLocationcDlg()
        {
            InitializeComponent();
            Googledrivelogin s = new Googledrivelogin();

            _driveService = s.GetAuth();

            PopulateGDLocationTree();




        }
        public GDLocationcDlg(DriveService driveService, string displayname, GDSettingDlg gDSettingDlg)
        {
            InitializeComponent();

            _driveService = driveService;
            this.Text = "Select Google Drive Setting Location";
            PopulateGDLocationTree();

        }
        public GDLocationcDlg(DriveService driveService, string displayname, GDUploadDlg gDUploadDlg)
        {
            InitializeComponent();

            _driveService = driveService;
            this.Text = "Select Google Drive Upload Location";
            PopulateGDLocationTree();

        }
        public GDLocationcDlg(DriveService driveService, string displayname, GDDownloadDlg gDDownloadDlg)
        {
            InitializeComponent();
            this.Text = "Select Google Drive Download  Location";
            _driveService = driveService;

            PopulateGDLocationTree();

        }
        private void TreeGDLoc_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                var paths = e.Node.FullPath;

                if (this.Text == "Select Google Drive Upload Location")
                {
                    var pathh = e.Node.FullPath;
                    var newPathh = pathh.Replace('\\', '/');
                    selectedGDLocationtxtBox.Text = newPathh;
                   
                }
                else if (this.Text == "Select Google Drive Download  Location")
                {
                    var pathh = e.Node.FullPath;
                    var newPathh = pathh.Replace('\\', '/');
                    selectedGDLocationtxtBox.Text = newPathh;

                }
                else if (this.Text == "Select Google Drive Download Location")
                {
                    var filePath = e.Node.FullPath;
                    var newFilePath = filePath.Replace('\\', '/');
                }
                var filePathselected = treeView1.SelectedNode.FullPath;
               selectedGDLocationtxtBox.Text = filePathselected;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Google Drive Addin");
            }
        }

        private void TreeGDLoc_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var path = "";
            path = "/" + e.Node.FullPath + "/";
            var newPath = path.Replace('\\', '/');
           


        }
        public IList<Google.Apis.Drive.v3.Data.File> folderlist()
        {
            FilesResource.ListRequest listRequest = _driveService.Files.List();
            listRequest.PageSize = 100;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> filess = listRequest.Execute().Files;
           
            return filess;
        }
        private void PopulateGDLocationTree(FileList fileschild)
        {

        }
        private void PopulateGDLocationTree()
        {

            if (_driveService != null)
            {
                IList<Google.Apis.Drive.v3.Data.File> files = folderlist();

                var fileListRequest = _driveService.Files.List();
                fileListRequest.Q = "mimeType = 'application/vnd.google-apps.folder'";
                var fileListResponse = fileListRequest.Execute();
                var filess = fileListResponse.Files;

                if (filess != null && filess.Count > 0)
                {
                    int j = 0;

                    foreach (var file in filess)
                    {


                        treeView1.Nodes.Add(file.Name);

                        treeView1.Nodes[j].ImageIndex = 0;
                        String folederid = file.Id;
                        FileList childfile = GetChildren(folederid);
                        if (childfile != null && files.Count > 0)
                        {
                            int i = 0;

                            foreach (var f in childfile.Files)
                            {
                                var childnode = treeView1.Nodes[j].Nodes.Add(f.Name);

                                childnode.ImageIndex = 1;
                                i++;
                            }
                        }
                        j++;



                        if (file.Name == "EurekaSimBackup")
                            folderExixits = true;


                    }
                    if (folderExixits == false)
                    {
                        createfolder();
                    }
                }
               
            }
            else
            {
                treeView1.Nodes.Add("no data");
            }


           

        }

        public void createfolder()
        {



            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = "EurekaSimBackup",
                MimeType = "application/vnd.google-apps.folder"
            };


            // Create a new folder on drive.
            var request = _driveService.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            folderExixits = true;
           


        }
        public FileList GetChildren(string dir_name)
        {
            List<Google.Apis.Drive.v3.Data.File> fileslist = new List<Google.Apis.Drive.v3.Data.File>();



            var request = _driveService.Files.List();
            request.Q = "Parents in'" + dir_name + "'";

            List<Google.Apis.Drive.v3.Data.File> result = new List<Google.Apis.Drive.v3.Data.File>();

            FilesResource.ListRequest req = _driveService.Files.List();
            req.Q = "trashed=false";
            req.Q += string.Format(" and '{0}' in parents", dir_name);

            FileList files = req.Execute();


            return files;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var filePathselected = treeView1.SelectedNode.FullPath;
           
            if (this.Text == "Select Google Drive Download  Location")
            {
                DownloadLocation = filePathselected;
            }
            else
            {
                UploadLocation = filePathselected;

            }
            this.Close();
            // return filePathselected;
        }

    }
}
