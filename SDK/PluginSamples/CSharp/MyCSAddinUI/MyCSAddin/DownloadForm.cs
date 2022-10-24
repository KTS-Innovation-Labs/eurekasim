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
    public partial class DownloadForm : Form
    {
        public string  path;
        public bool fileOpen;
        OneDriveSdkMan m_objSdkMan;
        public DownloadForm(OneDriveSdkMan obj = null)
        {
            InitializeComponent();
            m_objSdkMan = obj;
           
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
           
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Download to Selected Folder " ;
            var response = dialog.ShowDialog();
            if (response != DialogResult.OK)
            {
                path = null;

            }

            try
            {
                path = dialog.SelectedPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                path = null;
            }

           textBoxFilepath.Text = path;
            
        }

        private void DownloadForm_Load(object sender, EventArgs e)
        {
            SetStatusText("");
            path = Properties.Settings.Default.DownloadFolder;
            textBoxFilepath.Text = path;
            textBox_downloadfile.Text = "";
            checkBox_openfile.Checked = false;

        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            if (textBox_downloadfile.Text == "")
                MessageBox.Show("Please Select A File To Download", "EurekaSim OneDrive Addin");

            else
            {
                this.toolStripStatusLabel_downLoad.ForeColor = Color.Blue;
                toolStripStatusLabel_downLoad.Text = "Downloading.......";
                m_objSdkMan.FileDownload(textBox_downloadfile.Text);
            }
                
        }
        public void SetStatusText(string msg)
        {
            toolStripStatusLabel_downLoad.Text = msg;
        }
        private void button_downloadfilebrowse_Click(object sender, EventArgs e)
        {
            SetStatusText("");
            textBox_downloadfile.Text = m_objSdkMan.OpenBrowserWindow();
          
        }
        public bool GetCheckStatus()
        {
            return checkBox_openfile.Checked;
        }
        private void checkBox_openfile_CheckedChanged(object sender, EventArgs e)
        {
            fileOpen = checkBox_openfile.Checked;
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
