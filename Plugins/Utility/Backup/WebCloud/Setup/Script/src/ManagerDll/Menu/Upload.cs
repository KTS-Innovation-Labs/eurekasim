using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebCloud.Api.DS;
using System.Windows.Forms;
using ManagerDll.TestData;
using ManagerDll.Services;
using ManagerDll.Menu;

namespace ManagerDll
{
    public partial class Upload : Form
    {
        public AppApiService ApiService = null;
        private FileCollection m_Files = null;
        private int m_iNoOfTry = 1;
        private ApiTestData objTestDataUtil;
        private Thread uploaderThread = null;
        #region Deligates and events
        public delegate void OnUploadProgress(object sender, FileUploadProgressEvntArgs e);
        public delegate void OnUploadStatusChanged(object sender, UploadStatusChangeEventArgs e);
        delegate void SetTextCallback(string text);
        public event OnUploadProgress OnUploadFileProgress;
        public event OnUploadStatusChanged OnUploadFileStatus;
        public string m_strFilepath;
        public SdkManager manager = new SdkManager();
       
        #endregion
        public Upload(Settings set)
        {
            InitializeComponent();
            objTestDataUtil = set.objTestDataUtil;
            LoadValues();
            OnUploadFileProgress = WCOnUploadProgress;
            OnUploadFileStatus = WCOnUploadFileStatus;
           
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void WCOnUploadProgress(object sender, FileUploadProgressEvntArgs Progress)
        {
            string ProgressStatus = System.IO.Path.GetFileName(Progress.FileName) + " | " + Progress.ProgressPercentage + " " + "% Completed";
            SetProgressText(ProgressStatus);
        }
        public void WCOnUploadFileStatus(object sender, UploadStatusChangeEventArgs Status)
        {
            string FileStatus = System.IO.Path.GetFileName(Status.File.FilePath) + " | " + Status.Status;
            SetStatusText(FileStatus);
            if (Status.Status == UploadStatus.SingleFileUploadFinishSuccesfuly)
            {
                uploaderThread = null;
                buttonUploadFile.Text = "Upload File";
            }

        }
        private void SetStatusText(string Status)
        {
            if (labelStatus.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetStatusText);
                this.Invoke(d, new object[] { Status });
            }
            else
            {
                labelStatus.Text = Status;
            }

        }
        private void SetProgressText(string Progress)
        {
            if (labelProgress.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetProgressText);
                this.Invoke(d, new object[] { Progress });
            }
            else
            {
                labelProgress.Text = Progress;
            }

        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {

            UploadEx();
        }
        public void  UploadEx()
        {
            
            m_Files = new FileCollection();
            CloudFile file = new CloudFile();
            setPath(m_strFilepath);
            string strFilePath = m_strFilepath;

            FileInfo info = new FileInfo(strFilePath);
            
            file.FilePath = strFilePath;
            file.DestinationDirectory = textBoxDestinationDir.Text;
            m_Files.Add(file);
            if (buttonUploadFile.Text == "Upload File")
            {
                uploaderThread = new Thread(new ThreadStart(UploadFiles));
                uploaderThread.Start();

                buttonUploadFile.Text = "Abort";

            }
            else
            {
                AbortUploading();
                uploaderThread = null;
                buttonUploadFile.Text = "Upload File";
            }
        }
        private void buttonLocalPathBrowseClick_Click(object sender, EventArgs e)
        {
            try
            {
                ListAllDirFilesForm LocalPath = new ListAllDirFilesForm(objTestDataUtil, false, true, textBoxLocalFilePath.Text);
                LocalPath.ShowDialog();
                textBoxLocalFilePath.Text = LocalPath.DestPath;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void buttonDestinationPathBrowseClick_Click(object sender, EventArgs e)
        {
            try
            {
                ListAllDirFilesForm RemotePath = new ListAllDirFilesForm(objTestDataUtil, true, false, textBoxDestinationDir.Text);
                RemotePath.ShowDialog();
                textBoxDestinationDir.Text = RemotePath.DestPath;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }
        public void AbortUploading()
        {
            try
            {
                uploaderThread.Abort();
            }
            catch (Exception)
            {
            }
        }
        private void UploadFiles()
        {

            CloudFile file = null;
            try
            {
                for (int iIndex = 0; iIndex < m_Files.Count; iIndex++)
                {
                    file = m_Files[iIndex];
                    file.FileStatus = TransferFileStatus.NotTransfer;
                    try
                    {
                        UploadFile(file);
                        file.FileStatus = TransferFileStatus.TransferSuccesfuly;
                        MessageBox.Show("Uploaded Successfully","Eurekasim Addin");
                    }
                    catch (Exception ex)
                    {
                        file.FileStatus = TransferFileStatus.TransferFailed;
                        OnUploadFileStatus(this, new UploadStatusChangeEventArgs(file, UploadStatus.SingleFileUploadFailed, ex));

                    }

                }

                //On_Upload_Progress(this, new AiopFileUploadProgressEvntArgs(AiopProgressTypes.TotalFileUploadProgress));
            }
            catch (Exception Ex)
            {
                file.FileStatus = TransferFileStatus.TransferFailed;
                OnUploadFileStatus(this, new UploadStatusChangeEventArgs(file, UploadStatus.SingleFileUploadFailed, Ex));
            }
        }

        private async void UploadFile(CloudFile file)
        {
            OnUploadFileStatus(this, new UploadStatusChangeEventArgs(file, UploadStatus.SingleFileUploadStarted));

            Stream fileContents = null;
            try
            {
                fileContents = (Stream)file.FileContent;

                int nBufferSize = 1024 * 512;
                bool bIsEndOfFile = false;
                bool bHasBlocks = true;
                float lLen = fileContents.Length;

                if (lLen < nBufferSize)
                {
                    bHasBlocks = false;
                    nBufferSize = (int)lLen;
                }

                float iBytesRead = 0;
                int iFileBlockNo = -1;
                fileContents.Position = nBufferSize * file.LastTransferedChunkNo;

                while (iBytesRead < lLen)
                {
                    byte[] filebuff = new byte[nBufferSize];
                    iBytesRead += fileContents.Read(filebuff, 0, nBufferSize);
                    iFileBlockNo++;

                    float lRemain = lLen - fileContents.Position;
                    if (lRemain < nBufferSize)
                    {
                        nBufferSize = (int)lRemain;
                    }

                    FileChunkReq fileChunkReq = new FileChunkReq();
                    fileChunkReq.FileName = file.FilePath;
                    fileChunkReq.Data = filebuff;
                    fileChunkReq.DestinationDirectory = file.DestinationDirectory;
                    fileChunkReq.EndOfData = bIsEndOfFile;
                    fileChunkReq.HasBlocks = bHasBlocks;
                    fileChunkReq.ChunkNumber = iFileBlockNo;

                    // retry downloading
                    file.LastTransferedChunkNo = iFileBlockNo;
                    // webservice method
                    //webservice.SetWSTData(m_strUserName, m_strPassword, m_strUserType, REQUEST_TYPE, WstUtils.GetSerilizedXml(fileChunkReq));
                    FileChunkResp Resp = await objTestDataUtil.UploadFileChunkAPI(fileChunkReq);

                    float sendBytes = iBytesRead / lLen;
                    int iSendByteProgress = (int)Math.Ceiling(((decimal)sendBytes * 100));

                    TransferFileStatus Tstaus = TransferFileStatus.Transferring;
                    if (100 == iSendByteProgress)
                    {
                        Tstaus = TransferFileStatus.TransferSuccesfuly;

                    }

                    FileUploadProgressEvntArgs fileUploadEventArgs =
                        new FileUploadProgressEvntArgs(ProgressTypes.SingleFileUploadProgess,
                                                           Tstaus,
                                                           fileChunkReq.FileName,
                                                           iSendByteProgress,
                                                           (int)Math.Ceiling(iBytesRead));

                    OnUploadFileProgress(this, fileUploadEventArgs);

                    if (100 == iSendByteProgress)
                    {
                        OnUploadFileStatus(this, new UploadStatusChangeEventArgs(file, UploadStatus.SingleFileUploadFinishSuccesfuly));

                    }

                }
                fileContents.Close();
                fileContents.Dispose();
                file.FileStatus = TransferFileStatus.TransferSuccesfuly;

                //OnUploadFileStatus(this, new UploadStatusChangeEventArgs(file, UploadStatus.SingleFileUploadFinishSuccesfuly));
            }
            catch (Exception)
            {
                int ThreadSleep = 1000;
                while (m_iNoOfTry <= 4)
                {
                    Thread.Sleep(ThreadSleep);

                    if (null != fileContents)
                    {
                        fileContents.Close();
                        fileContents.Dispose();
                    }

                    m_iNoOfTry++;
                    ThreadSleep = ThreadSleep * m_iNoOfTry;
                    UploadFile(file);
                }

                file.FileStatus = TransferFileStatus.TransferFailed;
                //OnUploadFileStatus(this, new UploadStatusChangeEventArgs(file, UploadStatus.SingleFileUploadFailed));
                if (null != fileContents)
                {
                    fileContents.Close();
                    fileContents.Dispose();
                }

            }
        }
        public void LoadValues()
        {
            textBoxLocalFilePath.Text = m_strFilepath;
            textBoxDestinationDir.Text = Properties.Settings.Default.dfltDownLoc;
        }
       
        public void setPath(string filepath)
        {
            m_strFilepath = filepath;
        }

        private void Upload_Load(object sender, EventArgs e)
        {
            LoadValues();
        }
    } 
}





