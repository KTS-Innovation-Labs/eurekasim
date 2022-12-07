using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebCloud.Api.DS;
using ManagerDll.TestData;
using System.IO;
using ManagerDll.Services;


namespace ManagerDll.Menu
{
    public partial class ListAllDirFilesForm : Form
    {
       
        public ApiTestData objTestDataUtil;
        private TreeNode TreeRemote = null;
        bool m_bFolderType = false;
        bool m_bLocalPath = true;
        bool m_bEditDirFiles = false;
        bool m_bFileType;
        public string DestPath { get; set; }
        private FolderBrowserDialog LocalFolderPathDlg = new FolderBrowserDialog();
        private OpenFileDialog FileBrowser = new OpenFileDialog();
        public ListAllDirFilesForm(ApiTestData obj, bool bFolderType = false, bool bLocalPath = true, string DefaultPath = "", bool bEditDirFiles = false)
        {
            InitializeComponent();
            m_bFileType = false;
            objTestDataUtil = obj;
            m_bFolderType = bFolderType;
            m_bLocalPath = bLocalPath;
            DestPath = DefaultPath;
            m_bEditDirFiles = bEditDirFiles;
        }

        private void ListAllDirFilesForm_Load(object sender, EventArgs e)
        {
            try
            {
                menuStripDirOperations.Visible = m_bEditDirFiles;
                textBoxPath.Text = DestPath;
                if (m_bLocalPath)
                {
                    if (m_bFolderType)
                    {
                        this.Text = "Select Local Folder";
                        LocalFolderPathDlg.ShowDialog();
                        textBoxPath.Text = LocalFolderPathDlg.SelectedPath + "\\";
                       
                    }
                    else
                    {
                        this.Text = "Select Local File";
                       FileBrowser.ShowDialog();
                       textBoxPath.Text = FileBrowser.FileName;
                    }
                    DestPath = textBoxPath.Text;
                    this.Close();
                }
                else
                {

                    if (objTestDataUtil.MainForm.ApiService == null)
                    {
                        throw new Exception("Please Login to the App First");
                        
                    }
                    if (m_bFolderType)
                    {
                        this.Text = "Select Remote Folder";
                    }
                    else
                    {
                        this.Text = "Select Remote File";
                    }

                    TreeNode treeNode = new TreeNode(objTestDataUtil.MainForm.ApiService.UserID);
                    treeViewRemote.Nodes.Add(treeNode);

                }
                if (m_bEditDirFiles)
                {
                    this.Text = "Create | Delete | Rename Remote Files | Folders ..";
                }
            }
            catch (Exception Ex)
            {
                this.Close();
                MessageBox.Show(Ex.Message, "IDrive");
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DestPath = textBoxPath.Text;
            this.Close();
        }
        public void SetStatusMessage(string Message)
        {
            labelStatus.Text = Message;
        }
        private void AddToTreeViewRemote(DirListingResp directoryListingResp, bool bFolder = true)
        {
            if (!directoryListingResp.IsSuccesfulResponse)
            {
                SetStatusMessage(directoryListingResp.Message);
                return;
            }
            try
            {
                if (null != TreeRemote.FirstNode && string.Compare(TreeRemote.FirstNode.Text, DefinedSettings.strNode) == 0)
                {
                    treeViewRemote.Nodes.Remove(TreeRemote.FirstNode);
                }

                //statusBarRemote.Text = string.Format("{0} Directories", Convert.ToString(directoryListingResp.DirecoryInfo.Directories.Length));
                foreach (Dir directory in directoryListingResp.DirecoryInfo.Directories)
                {
                    Dir directoryTrim = new Dir();
                    string strDirfullPath = directory.StrDirectoryName;
                    string strDirNameTrimmed = System.IO.Path.GetFileName(strDirfullPath);
                    directoryTrim.HasContents = directory.HasContents;
                    directoryTrim.StrDirectoryName = strDirNameTrimmed;

                    if (directoryTrim.HasContents)
                    {
                        TreeNode nodes = new TreeNode(directoryTrim.StrDirectoryName);
                        treeViewRemote.SelectedNode.Nodes.Add(nodes);
                        nodes.Tag = strDirfullPath;
                        nodes.ImageIndex = DefinedSettings.iImgIndxFolderClosed; ;
                        nodes.SelectedImageIndex = DefinedSettings.iImgIndxFolderOpen;
                        nodes.Nodes.Add(DefinedSettings.strNode);
                    }
                    else
                    {
                        TreeNode nodes = new TreeNode(directoryTrim.StrDirectoryName);
                        treeViewRemote.SelectedNode.Nodes.Add(nodes);
                        nodes.Tag = strDirfullPath;
                        nodes.ImageIndex = DefinedSettings.iImgIndxFolderClosed;
                        nodes.SelectedImageIndex = DefinedSettings.iImgIndxFolderOpen;
                        nodes.Text = directoryTrim.StrDirectoryName;
                    }
                }
                if (!bFolder)
                {
                    foreach (FileDetails FileInfo in directoryListingResp.DirecoryInfo.Filedetails)
                    {
                        TreeNode nodes = new TreeNode(FileInfo.FileName);
                        treeViewRemote.SelectedNode.Nodes.Add(nodes);
                        nodes.Tag = FileInfo.FileName;
                        nodes.ImageIndex = DefinedSettings.iImgIndxKeyFile;
                        nodes.SelectedImageIndex = DefinedSettings.iImgIndxKeyFile;
                        nodes.Text = System.IO.Path.GetFileName(FileInfo.FileName);
                    }
                }


            }
            catch (Exception ex)
            { throw ex; }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void treeViewRemote_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                treeViewRemote.SelectedNode.Nodes.Clear();
                await BrowseDirectory();
            }
            catch (Exception) { }
        }
        public async Task BrowseDirectory()
        {
            try
            {
                SetStatusMessage("Please Wait...");
                TreeNode CurrentNode = treeViewRemote.SelectedNode;
                if (CurrentNode != TreeRemote)
                {
                    TreeRemote = CurrentNode;
                }
                else
                {
                    SetStatusMessage("Clicked on the Same Node ..");
                    return;
                }

                List<string> ParentArray = new List<string>();
                TreeNode Parent = TreeRemote.Parent;
                while (Parent != null)
                {
                    ParentArray.Add(Parent.Text);
                    Parent = Parent.Parent;
                }
                DestPath = "";
                for (int i = ParentArray.Count - 1; i >= 0; i--)
                {
                    DestPath = DestPath + ParentArray[i] + "\\";
                }
                DestPath = DestPath + TreeRemote.Text + "\\";
                if (m_bFolderType)
                {
                    textBoxPath.Text = DestPath;
                }
                else
                {
                    if (TreeRemote.Tag != null)
                    {
                        textBoxPath.Text = TreeRemote.Tag.ToString();
                    }

                }

                DirListingResp Resp = await objTestDataUtil.InvokeListDirectoryAPI(objTestDataUtil.MainForm.ApiService.UserID, DestPath);
                m_bFileType = (Resp.DirecoryInfo == null);
                AddToTreeViewRemote(Resp, m_bFolderType);
                SetStatusMessage("Done...");

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }
}
