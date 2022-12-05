using ManagerDll;
using ManagerDll.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCloud.Api.DS;
using ManagerDll.Services;
using System.Windows.Forms;

namespace ManagerDll.TestData
{
    public static class DefinedSettings
    {
        
        #region Member functions
        //messages 
        public static string strAppName = "eyeCloud";
        public static string strConfirmDelete = "Are you sure want to delete";
        //Request types.
        public static string strReqUploading = "-->>";
        public static string strReqDownloading = "---<<";
        public static string strReqDirectoryListing = "Directory listing";
        public static string strReqFileRenaming = "File renaming";
        public static string strReqFileDeleting = "File deleting";
        public static string strReqCreateDirectory = "Create directory";


        //Tree view nodes names
        public static string strNode = "DummyNode";
        public static string strNodeDektop = "Desktop";
        public static string strNodeMyDocuments = "My Documents";
        public static string strNodeMyComputer = "My Computer";
        public static string strNodeMyFavorites = "My Favorites";

        //List view Column header
        public static string strChFilename = "Name";
        public static string strChFileSize = "Size";
        public static string strChFileType = "Type";
        public static string strChLastModified = "Date modified";
        public static string strChDateCreated = "Date created";
        public static string strChPermission = "Permission";

        //Connection status.
        public static string strCnnStateReq = "Checking internet connection...";
        public static string strCnnStateResp = "Its lool like your PC isn't connected to internet at this moment.";
        public static string strLoginState = "please login to webCloud";
        //Image index numbers
        public static int iImgIndxDesktop = 0;
        public static int iImgIndxMyComp = 1;
        public static int iImgIndxMyDoc = 2;
        public static int iImgIndxMyFav = 3;
        public static int iImgIndxCDRom = 4;
        public static int iImgIndxCDrive = 5;
        public static int iImgIndxPenDrive = 6;
        public static int iImgIndxFolderClosed = 7;
        public static int iImgIndxFolderOpen = 8;
        public static int iImgIndxKeyEnter = 9;
        public static int iImgIndxKeyFile = 10;
        #endregion

        #region Constructor
        /*
        Constructor must be in private
        private DefinedSettings()
        { }
         */
        #endregion
    }
    public partial class ApiTestData
    {
        public ApiTestData objTestDataUtil;
        public string username;
        public ApiMenuTestDataUtil objMenuTestData;
        public class ApiMenuTestDataUtil
        {
        }
        public void InvokeListFilesAndDirectoryAPI()
        {
            ListAllDirFilesForm ListAllFilesForm = new ListAllDirFilesForm(this, false, false);
            ListAllFilesForm.ShowDialog();
        }
        public async Task<DirListingResp> InvokeListDirectoryAPI(string UserName, string DestinationPath)
        {
            return await MainForm.ApiService.ListDirectory(UserName, DestinationPath);
        }
        public async Task<FileChunkResp> UploadFileChunkAPI(FileChunkReq fileChunkRequest)
        {
            return await MainForm.ApiService.UploadFileChunk(fileChunkRequest);
        }
        public async Task<FileChunkDownloadResp> DownloadFileChunkAPI(FileChunkDownloadReq fileChunkDownloadReq)
        {
            return await MainForm.ApiService.DownloadFileChunk(fileChunkDownloadReq);
        }
        public void InvokeCRUDFileDirectoryAPI()
        {
            ListAllDirFilesForm ListAllFilesForm = new ListAllDirFilesForm(this, false, false, "", true);
            ListAllFilesForm.ShowDialog();
        }

        internal Task<DirListingResp> InvokeListDirectoryAPI(object userID, string destPath)
        {
            throw new NotImplementedException();
        }
        public async Task InvokeGetUserListForDeletionAPI(ApiTestData obj)
        {

            try
            {
                objTestDataUtil = obj;

                List<UserAccountInfo> result = await ObjMainForm.ApiService.GetUserList();
                foreach (UserAccountInfo item in result)
                {
                    if(item.Email== ObjMainForm.GetUserID())
                    {
                        username = item.FirstName;
                        ObjMainForm.SetCurrentUser(username);
                    }
                    else
                    {
                        username = "";
                    }
                }
              
            }
            catch (Exception Ex)
            {

                //MessageBox.Show(Ex.Message);
            }
        }
       
    }

   
}
