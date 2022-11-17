using Box.V2;
using Box.V2.Config;
using Box.V2.Models;
using EurekaSim.Net;
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

namespace ESimBoxCloud
{
    public partial class DownloadForm : Form
    {

        BoxClient m_Boxclient;
        string m_strLocalPath = "";
      

        public DownloadForm(BoxClient boxclient)
        {
            m_Boxclient = boxclient;
            
            InitializeComponent();
           
            if (m_Boxclient != null)
            {
                toolStripStatusLabel1.Text = "Updating";
               
            }
            else
            {
                toolStripStatusLabel1.Text = "Logged Out.Please Login..";
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.dfltDownloadLoc))
            {
                var localDir = System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\EurekaSim_Box_Backup\\");
                m_strLocalPath = localDir.FullName;

            }
            else
            {
                m_strLocalPath = Properties.Settings.Default.dfltDownloadLoc;

            }
        }

        [Obsolete]
        private async void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {

               
                if (this.txtBoxDowloadFile.Text != string.Empty)
                {
                    var upDlg = new FolderBrowserDialog();
                     upDlg.SelectedPath = m_strLocalPath;
                    upDlg.Description = "Browse Download Location";
                    DialogResult result = upDlg.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        m_strLocalPath = upDlg.SelectedPath + "\\";
                        toolStripStatusLabel1.Text = "Downloading...";

                        using (var response = await m_Boxclient.FilesManager.DownloadStreamAsync(BoxLocation.FolderID))
                        {
                            using (var fileStream = File.Create(m_strLocalPath + BoxLocation.FileName))
                            {
                                await response.CopyToAsync(fileStream);
                            }
                        }
                        if (chkOpenDownload.Checked == true)
                        {
                            ApplicationDocument doc = new ApplicationDocument();
                            doc.OpenDocument(m_strLocalPath + BoxLocation.FileName);
                        }
                        toolStripStatusLabel1.Text = "File successfully downloaded.";
                        MessageBox.Show("File successfully downloaded ");
                    }
                }

                else
                {
                    MessageBox.Show("Select a file from Box to download", "EurekaSim Box Addin");
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Box Addin");
            }
        }

        private void btnDownLocBrws_Click(object sender, EventArgs e)
        {
            if (m_Boxclient != null)
            {
                try
                {
                    BoxLocation locDialog = new BoxLocation(m_Boxclient);
                    locDialog.ShowDialog();
                    txtBoxDowloadFile.Text = BoxLocation.setvaluefortxtBoxLocSelect;
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
        private void setBoxClient(BoxClient boxclient)
        {

            m_Boxclient = boxclient;


        }
        

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
