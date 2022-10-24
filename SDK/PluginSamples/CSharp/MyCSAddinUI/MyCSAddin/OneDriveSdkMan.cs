using EurekaSim.Net;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCSAddin
{

    public class OneDriveSdkMan
    {
        SettingsForm m_settingsForm;
        DownloadForm m_downloadForm;
        UploadForm m_uploadForm;
        public ObjectBrowserForm m_formObjectBrowserForm;
        public const string MsaClientId = "37274955-0b0a-4b94-91a6-b6762ebc7f4a";
        public const string MsaReturnUrl = "urn:ietf:wg:oauth:2.0:oob";
        public string m_strFilePath = string.Empty;
        private enum ClientType
        {
            Consumer,
            Business
        }

        private GraphServiceClient graphClient { get; set; }
        private ClientType clientType { get; set; }
        public DriveItem CurrentFolder { get; set; }
        public string strSelectedItem { get; set; }
        public OneDriveSdkMan()
        {
            m_formObjectBrowserForm = new ObjectBrowserForm(this);
            m_settingsForm = new SettingsForm(this);
            m_downloadForm = new DownloadForm(this);
            m_uploadForm = new UploadForm(this);
            
        }
        public void OpenSettingsWindow()
        {
            m_settingsForm.ShowDialog();
        }
        public void OpenUploadWindow()
        {
            if (Authentication.TokenForUser == null)
            {
                DialogResult result;
                result = MessageBox.Show("Please login to OneDrive before uploading..", "Eurekasim OneDrive Addin");
                if (result == DialogResult.OK)
                {
                    m_settingsForm.ShowDialog();
                }
            }
            else
            {
                if (Properties.Settings.Default.AutoUpload) 
                {
                    if (m_strFilePath == string.Empty)
                    {
                        MessageBox.Show("Please Save the Current EurekaSim File Before Uploading....", "EurekaSim OneDrive Addin");
                    }
                    else
                    {
                        m_uploadForm.ShowDialog();
                    }

                }
                else
                    m_uploadForm.ShowDialog();
            }
        }

        public void OpenDownloadWindow()
        {
            if (Authentication.TokenForUser == null)
            {
                DialogResult result;
                result = MessageBox.Show("Please login to OneDrive before downloading..", "Eurekasim OneDrive Addin");
                if (result == DialogResult.OK)
                {
                    m_settingsForm.ShowDialog();
                }
            }
            else
            {
                m_downloadForm.ShowDialog();
            }
        }
        public void OpenAboutWindow()
        {
            AboutForm form = new AboutForm();
            form.Show();
        }
        

        private async Task LoadFolderFromPath(string path = null)
        {
            if (null == this.graphClient) return;

            try
            {
                DriveItem folder;

                var expandValue = this.clientType == ClientType.Consumer
                    ? "thumbnails,children($expand=thumbnails)"
                    : "thumbnails,children";

                if (path == null)
                {
                    folder = await this.graphClient.Drive.Root.Request().Expand(expandValue).GetAsync();
                }
                else
                {
                    folder =
                        await
                            this.graphClient.Drive.Root.ItemWithPath("/" + path)
                                .Request()
                                .Expand(expandValue)
                                .GetAsync();
                }

                AddRootNode(folder);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void AddRootNode(DriveItem folder)
        {
            if (folder != null)
            {
                CurrentFolder = folder;

                if (folder.Folder != null && folder.Children != null && folder.Children.CurrentPage != null)
                {
                    LoadChildren(folder.Children.CurrentPage);
                }
            }
        }
        private void LoadChildren(IList<DriveItem> items)
        {
            foreach (DriveItem obj in items)
            {
                m_formObjectBrowserForm.UpdateTreeView(obj);
            }
        }
        public async Task LoadFolderFromId(string id)
        {
            if (null == this.graphClient) return;

            try
            {
                var expandString = this.clientType == ClientType.Consumer
                    ? "thumbnails,children($expand=thumbnails)"
                    : "thumbnails,children";

                var folder =
                    await this.graphClient.Drive.Items[id].Request().Expand(expandString).GetAsync();

                AddSubNodes(folder);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void AddSubNodes(DriveItem folder)
        {
            if (folder != null)
            {
                this.CurrentFolder = folder;

                if (folder.Folder != null && folder.Children != null && folder.Children.CurrentPage != null)
                {
                    LoadSubNodes(folder.Children.CurrentPage);
                }
            }
        }
        private void LoadSubNodes(IList<DriveItem> items)
        {
            foreach (DriveItem obj in items)
            {
                m_formObjectBrowserForm.UpdateNode(obj);
            }

        }
        public void SignOut()
        {

            try
            {
                Authentication.SignOut();
                
            }
            catch (ServiceException exception)
            {

                PresentServiceException(exception);

            }

        }

        public async Task SignIn()
        {

            try
            {
                this.graphClient = Authentication.GetAuthenticatedClient();

            }
            catch (ServiceException exception)
            {

                PresentServiceException(exception);
               
            }
            try
            {
                await LoadFolderFromPath();
                if (Authentication.TokenForUser == null)
                    m_settingsForm.SetStatusText("Unable To Login ");
                else
                {
                    m_settingsForm.SetStatusText("Logged In");
                    m_settingsForm.SetButtonText("Logout");
                }
                    

            }
            catch (ServiceException exception)
            {
                PresentServiceException(exception);
                this.graphClient = null;
            }
        }
        private static void PresentServiceException(Exception exception)
        {
            string message = null;
            var oneDriveException = exception as ServiceException;
            if (oneDriveException == null)
            {
                message = exception.Message;
            }
            else
            {
                message = string.Format("{0}{1}", Environment.NewLine, oneDriveException.ToString());
            }

            MessageBox.Show(string.Format("OneDrive reported the following error: {0}", message));
        }
        public string OpenBrowserWindow()
        {
            string strFolderPath = "";
            if (Authentication.TokenForUser == null)
            {
                MessageBox.Show("You are not logged in. Please login to OneDrive Account", "EurekaSim Dropbox Addin");
            }
            else
            {
                m_formObjectBrowserForm.ShowDialog();
                strFolderPath = m_formObjectBrowserForm.UploadFolderPath;
            }
            return strFolderPath;
        }
        

        public void setFilePath(string filePath)
        {
            m_strFilePath = filePath;
            if (Authentication.TokenForUser == null)
            {
                MessageBox.Show("Please Login To OneDrive  For AutoBackUp", "EurekaSim OneDrive Addin");
            }
            else
            {
                if (Properties.Settings.Default.AutoUpload)
                {
                    m_uploadForm.Show();
                    //string strNewPath = CopytoTempFolder(filePath);
                    //string originalFilename = System.IO.Path.GetFileName(strNewPath);
                    //Stream stream = new System.IO.FileStream(strNewPath, System.IO.FileMode.Open);
                    //string uploadFolder = Properties.Settings.Default.UploadFolder;
                    //FileUpload(stream, uploadFolder, originalFilename);

                }
            }


        }

        public string CopytoTempFolder(string filePath)
        {
            string strDestinationFile="";
            if (!System.IO.File.Exists(filePath))
            {
                return strDestinationFile;
            }
            strDestinationFile=Path.Combine(System.IO.Path.GetTempPath(), System.IO.Path.GetFileName(filePath));
            System.IO.File.Copy(filePath, strDestinationFile,true);
            return strDestinationFile;
        }

        public async void FileUpload(Stream file_stream, string folderPath, string filename)
        {
            using (var stream = file_stream)
            {
                if (stream != null)
                {
                    var uploadPath = folderPath + "/" + Uri.EscapeUriString(System.IO.Path.GetFileName(filename));

                    try
                    {
                        var uploadedItem =
                            await this.graphClient.Drive.Root.ItemWithPath(uploadPath).Content.Request().PutAsync<DriveItem>(stream);
                        MessageBox.Show("Uploaded to OneDrive Folder Successfully", "EurekaSim OneDrive PlugIn");
                        m_uploadForm.SetStatusText(" Successfully Uploaded ");
                        AddItemToFolderContents(uploadedItem);
                       
                    }
                    catch (Exception exception)
                    {
                        PresentServiceException(exception);
                        m_uploadForm.SetStatusText("Unable To Upload");

                    }
                }
            }
        }

        private void AddItemToFolderContents(DriveItem uploadedItem)
        {
            try
            {
                m_formObjectBrowserForm.UpdateNode(uploadedItem);
            }
            catch (Exception)
            {

               // throw;
            }
            
        }
        public async void FileDownload(string filename)
        {
            
            string Filename = Path.GetFileName(filename);
            string path = m_downloadForm.path + "\\" +Filename;
            try
            {
               
                using (var stream = await this.graphClient.Drive.Items[strSelectedItem].Content.Request().GetAsync())
                    
                using (var outputStream = new System.IO.FileStream(path, System.IO.FileMode.Create))
                    {
                    
                    await stream.CopyToAsync(outputStream);
                        
                    }
                MessageBox.Show("File Downloaded to "+ path + " Successfully","EurekaSim OneDrive PlugIn");
                m_downloadForm.SetStatusText("Successfully Downloaded ...");
                OpenDowloadedFile(path);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                m_downloadForm.SetStatusText("Unable To Downloaded");
            }
           
        }
        private void OpenDowloadedFile(string strFilePath)
        {
            if (!m_downloadForm.GetCheckStatus())
                return;
            try
            {
                Thread.Sleep(1000);
                new ApplicationDocument().OpenDocument(strFilePath);
               
            }
            catch (Exception)
            {
                
            }
        }
    }
}
