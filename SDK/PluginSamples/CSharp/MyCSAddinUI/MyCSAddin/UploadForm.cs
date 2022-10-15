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
        //SettingsForm m_SettingsForm;
        // ObjectBrowserForm m_ObjectBrowserForm;

        public UploadForm(OneDriveSdkMan obj = null)
        {
            InitializeComponent();
            m_objSdkMan = obj;
            //textBoxUploadFilepath.Text= Properties.Settings.Default.UploadFolder;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        //private void buttonBrowse_Click(object sender, EventArgs e)
        //{
        //    //m_objSdkMan.OpenBrowserWindow();
        //}

        private void UploadForm_Load(object sender, EventArgs e)
        {

            textBoxUploadFilepath.Text = Properties.Settings.Default.UploadFolder;
            if(m_objSdkMan.m_strFilePath==string.Empty)
            {
                MessageBox.Show("Please Save the Current EurekaSim File Before Uploading....","EurekaSim OneDrive Addin");
               // this.BeginInvoke(new MethodInvoker(this.Close));
            }
            else
            {
                textBox_Uploadfile.Text = m_objSdkMan.m_strFilePath;
                //stream1 = System.IO.File.OpenRead(m_objSdkMan.m_strFilePath);
                stream1=new System.IO.FileStream(m_objSdkMan.m_strFilePath, System.IO.FileMode.Open);
                
            }
           
            //MessageBox.Show(m_objSdkMan.m_strFilePath);
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            textBoxUploadFilepath.Text=m_objSdkMan.OpenBrowserWindow();
            //m_objSdkMan.UploadFolderPathUpdation(textBoxUploadFilepath.Text);

        }

        private void button_UploadFileBrowse_Click(object sender, EventArgs e)
        {
            
            targetFolderName = m_objSdkMan.CurrentFolder;
            //string originalFilename = "";
            stream2 = GetFileStreamForUpload(targetFolderName.Name, out originalFilename);
            //textBox_Uploadfile.Text = originalFilename;
            //m_objSdkMan.FileUpload(stream, targetFolderName, originalFilename);

        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
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
                return new System.IO.FileStream(dialog.FileName, System.IO.FileMode.Open);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error uploading file: " + ex.Message);
                originalFilename = null;
                return null;
            }

        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {

            //m_objSdkMan.FileUpload(stream, targetFolderName, originalFilename);
           // stream=(stream2 == null) ? stream1 : stream2;
           if(stream2==null)
            {
                stream = stream1;
                originalFilename = System.IO.Path.GetFileName(textBox_Uploadfile.Text);
               // MessageBox.Show("stream2 is empty");
            }
            else
            {
                stream = stream2;
            }

            string uploadFolder = "/"+ textBoxUploadFilepath.Text.Trim();
            m_objSdkMan.FileUpload(stream, uploadFolder, originalFilename);
        }
    }
}
