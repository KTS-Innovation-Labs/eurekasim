using Microsoft.Graph;
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

namespace MyCSAddin
{
    public partial class UploadForm : Form
    {
        OneDriveSdkMan m_objSdkMan;
        public Stream stream, stream1,stream2 = null;
        public string originalFilename = "";
        public DriveItem targetFolderName;

        public UploadForm(OneDriveSdkMan obj = null)
        {
            InitializeComponent();
            m_objSdkMan = obj;

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void UploadForm_Load(object sender, EventArgs e)
        {
            SetStatusText("");
            textBoxUploadFilepath.Text = Properties.Settings.Default.UploadFolder;
             if(Properties.Settings.Default.AutoUpload)
             {
                    
                textBox_Uploadfile.Text = m_objSdkMan.m_strFilePath;
                stream1=new System.IO.FileStream(m_objSdkMan.m_strFilePath, System.IO.FileMode.Open);
                
             }
            else
            {
                textBox_Uploadfile.Text = null;
            }
           
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            textBoxUploadFilepath.Text=m_objSdkMan.OpenBrowserWindow();
        }

        private void button_UploadFileBrowse_Click(object sender, EventArgs e)
        {
            SetStatusText("");
            targetFolderName = m_objSdkMan.CurrentFolder;
            stream2 = GetFileStreamForUpload(targetFolderName.Name, out originalFilename);

        }

        private System.IO.Stream GetFileStreamForUpload(string targetFolderName, out string originalFilename)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Upload to " + targetFolderName;
            dialog.Filter = "All Files (*.*)|*.*";
            dialog.CheckFileExists = true;
            var response = dialog.ShowDialog();

            if (response != DialogResult.OK)
            {
                originalFilename = null;
                return null;
            }
            textBox_Uploadfile.Text = dialog.FileName;
            try
            {
                originalFilename = System.IO.Path.GetFileName(dialog.FileName);
                string strNewPath = m_objSdkMan.CopytoTempFolder(dialog.FileName);
                return new System.IO.FileStream(strNewPath, System.IO.FileMode.Open);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error uploading file: " + ex.Message);
                originalFilename = null;
                return null;
            }

        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            if (textBox_Uploadfile.Text == "")
                MessageBox.Show("Please Select A File To Upload", "EurekaSim OneDrive Addin");
            else
            {
                this.toolStripStatusLabel_upload.ForeColor = Color.Blue;
                toolStripStatusLabel_upload.Text = "Uploading.......";

                if (stream2 == null)
                {
                    stream = stream1;
                    originalFilename = System.IO.Path.GetFileName(textBox_Uploadfile.Text);
                }
                else
                {
                    stream = stream2;
                }

                string uploadFolder = "/" + textBoxUploadFilepath.Text.Trim();
                m_objSdkMan.FileUpload(stream, uploadFolder, originalFilename);
                
            }
            
        }
        public void SetStatusText(string msg)
        {
            toolStripStatusLabel_upload.Text = msg;
        }
    }
}
