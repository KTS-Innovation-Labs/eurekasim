using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using Box.V2;
using System.Net;
using System.Net.Http;
using System.IO;
using Box.V2.Config;
using System.Diagnostics;
using Box.V2.Models;

namespace ESimBoxCloud
{
    public partial class UploadForm : Form
    {
        bool fromBrowse;
        BoxClient m_Boxclient;
        string m_strFileNme;
        byte[] m_strFileContnt;
        string PATH;
        
        public UploadForm(BoxClient boxclient)
        {
            m_Boxclient = boxclient;
            InitializeComponent();
            fromBrowse = false;
            m_strFileNme = ESimBoxCloudImp.m_strFileName;
            txtFileUpload.Text = ESimBoxCloudImp.m_strFilePath;
            this.txtBoxLocation.Text = Properties.Settings.Default.dfltUploadLoc;
            this.txtBoxLocation.Tag = Properties.Settings.Default.UpldId;
            PATH = ESimBoxCloudImp.m_strFilePath;
            m_strFileContnt = ESimBoxCloudImp.m_strFileContent;
            if (m_Boxclient==null)
            {
                StatusBarStatus.Text = "Logged Out.Please Login..";
               
            }
            else
            {
                this.StatusBarStatus.Text = "Updating...";


               
                this.txtBoxLocation.Text = Properties.Settings.Default.dfltUploadLoc;
            }
        }

        private void SetCurrentFileName(string fileName)
        {
            txtFileUpload.Text = fileName;
        }
 

        private void  btnBrowse_Click(object sender, EventArgs e)
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
                PATH = path;
                GetCurrentFile(path);
                this.txtFileUpload.Text =path;
            }

        }
        private void btnBoxUploadLoc_Click(object sender, EventArgs e)
        {
           
            if (m_Boxclient != null)
            {
                try
                {
                    BoxLocation locDialog = new BoxLocation(m_Boxclient);
                    locDialog.ShowDialog();
                    txtBoxLocation.Text = BoxLocation.setvaluefortxtBoxLocSelect;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "EurekaSim Box Addin");
                }
            }
            else
            {
                MessageBox.Show("You are not logged in. Please login to Box first", "Information");
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
                MessageBox.Show(ex.Message, "EurekaSim Box Addin");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnUpload_Click(object sender, EventArgs e)
        {
            if (m_Boxclient == null)
            {
                MessageBox.Show("Logged Out.Please Login..");
            }
                if (ESimBoxCloudImp.m_saveCheck != 2 && fromBrowse == false)
            {
                MessageBox.Show("Eurekasim file is not saved. Please save it first", "EurekaSim box Addin");
                return;
            }
            try
            {
                using (FileStream fs = File.Open(PATH,FileMode.Open,FileAccess.Read))
                {
                    StatusBarStatus.Text = "Uploading file...";

                    // Create request object with name and parent folder the file should be uploaded to
                    BoxFileRequest request = new BoxFileRequest();

                    request.Name = m_strFileNme;
                   
                    request.Parent = new BoxRequestEntity() { Id = Properties.Settings.Default.UpldId };
                    BoxFile f = await m_Boxclient.FilesManager.UploadAsync(request,fs);
                    StatusBarStatus.Text = "Uploaded Successfully...";
                    MessageBox.Show("Uploaded Successfully...");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           
        }
    }
}
