using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebCloud.Api.DS;
using System.Windows.Forms;

namespace ManagerDll.Services
{
    public partial class AppApiService
    {

        public async Task<AuthInfo> ValidateUser(string UserID, string Password)
        {
            AuthInfo Info = new AuthInfo();
            try
            {
                string Paramters = "api/Auth/ValidateUser?UserID=" + UserID + "&Password=" + Password;
                var requestTask = await AppServiceClient.GetAsync(Paramters);
                var response = Task.Run(() => requestTask);
                
                Task<string> ResponseData;
               
                if (response.Result.IsSuccessStatusCode)
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();
                    Info = JsonConvert.DeserializeObject<AuthInfo>(ResponseData.Result);
                }
                else
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();
                    //throw new Exception(ResponseData.Result);
                }
                return Info;
                
            }
            catch (Exception Ex)
            {
               throw Ex;
            }
        }
    }
}
