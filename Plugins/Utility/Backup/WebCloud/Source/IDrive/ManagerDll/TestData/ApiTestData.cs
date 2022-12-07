using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ManagerDll.Services;
using WebCloud.Api.DS;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManagerDll;
using Newtonsoft.Json;

namespace ManagerDll.TestData
{
    public partial class ApiTestData
    {
        public Settings ObjMainForm;
        public Settings MainForm { get { return ObjMainForm; } }

        public ApiTestData(Settings MainForm)
        {
            ObjMainForm = MainForm;
        }

        public async Task InvikeLoginValidationAPI()
        {
            try
            {
                ObjMainForm.SetStatusText("Please Wait..");
                string LoginButtonText = ObjMainForm.GetLoginButtonText();
                if (LoginButtonText == "Login")
                {
                    ObjMainForm.ApiService = new AppApiService(ObjMainForm.GetMainURL());
                    AuthInfo Info = await ObjMainForm.ApiService.ValidateUser(ObjMainForm.GetUserID(),ObjMainForm.GetPassword());
                    ObjMainForm.ApiService.SetUserCredentials(Info.UserID, Info.AuthenticationToken);
                    ObjMainForm.SetAutheticationToken(Info.AuthenticationToken);
                    ObjMainForm.SetRequestURL(ObjMainForm.ApiService.SubmitURL);
                    ObjMainForm.SetLoginButtonText("Logout");
                }
                else
                {
                    ObjMainForm.ApiService.SetUserCredentials("", "");
                    ObjMainForm.SetAutheticationToken("");
                    ObjMainForm.SetLoginButtonText("Login");
                    ObjMainForm.SetStatusText("Logout..");
                    
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }

        }
        internal void SetRequestURL(object submitURL)
        {
            throw new NotImplementedException();
        }
        //internal void LoadDefaultValues()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
