

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Microsoft.Graph;
using System.IO;

namespace MyCSAddin
{
    public partial class SettingsForm : Form
    {
        
        OneDriveSdkMan m_objSdkMan;
        public SettingsForm(OneDriveSdkMan obj = null)
        {
            InitializeComponent();
            m_objSdkMan = obj;

        }
       
        public void SetButtonText(string msg)
        {
            buttonLogin.Text = msg;
        }
        public void SetStatusText(string msg)
        {
            toolStripStatusLabel.Text = msg;
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            this.toolStripStatusLabel.ForeColor = Color.Blue;
            if (buttonLogin.Text=="Login")
            {
                toolStripStatusLabel.Text = "Logging In.......Please Wait........";
                await m_objSdkMan.SignIn();
            }
           else if(buttonLogin.Text=="Logout")
            {
                toolStripStatusLabel.Text = "Logging Out.......Please Wait........";
                m_objSdkMan.SignOut();
            }
           
        }

       
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonUploadBrowse_Click(object sender, EventArgs e)
        {

            textBoxUploadFolder.Text=m_objSdkMan.OpenBrowserWindow();
        }
       
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void buttonDownloadBrowse_Click(object sender, EventArgs e)
        {
         
                string path;

                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "Upload to " + textBoxUploadFolder.Text;
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
            textBoxDownloadFolder.Text = path;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DownloadFolder = textBoxDownloadFolder.Text;
            Properties.Settings.Default.UploadFolder = textBoxUploadFolder.Text;
            if (checkBox1.Checked == true)
            {
                Properties.Settings.Default.AutoUpload = true;
               
    }
            else
            {
                Properties.Settings.Default.AutoUpload = false;
             
            }
            Properties.Settings.Default.Save();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            textBoxDownloadFolder.Text = Properties.Settings.Default.DownloadFolder;
            textBoxUploadFolder.Text = Properties.Settings.Default.UploadFolder;
            checkBox1.Checked =  (bool)Properties.Settings.Default.AutoUpload;
        }
    }
}
