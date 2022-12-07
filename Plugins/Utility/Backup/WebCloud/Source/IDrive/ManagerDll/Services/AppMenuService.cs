using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebCloud.Api.DS;

namespace ManagerDll.Services
{
    public partial class AppApiService
    {
        public async Task<DirListingResp> ListDirectory(string UserName, string DestinationPath)
        {

            DirListingResp Response = null;
            try
            {
                DirListingReq ListingReq = new DirListingReq();
                ListingReq.CurrentDirectotyPath = DestinationPath;
                ListingReq.UserID = UserName;
                var serializedItem = JsonConvert.SerializeObject(ListingReq);
                string Paramters = "api/CB/ListDirectory";
                var requestTask = await AppServiceClient.PostAsync(Paramters, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                var response = Task.Run(() => requestTask);
                Task<string> ResponseData;
                if (response.Result.IsSuccessStatusCode)
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();

                    Response = JsonConvert.DeserializeObject<DirListingResp>(ResponseData.Result);

                }
                else
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();
                    throw new Exception(ResponseData.Result);
                }
                return Response;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public async Task<FileChunkResp> UploadFileChunk(FileChunkReq fileChunkRequest)
        {
            FileChunkResp Response = null;
            try
            {
                var serializedItem = JsonConvert.SerializeObject(fileChunkRequest);
                string Paramters = "api/CB/UploadFileChunk";
                var requestTask = await AppServiceClient.PostAsync(Paramters, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                var response = Task.Run(() => requestTask);
                Task<string> ResponseData;
                if (response.Result.IsSuccessStatusCode)
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();

                    Response = JsonConvert.DeserializeObject<FileChunkResp>(ResponseData.Result);

                }
                else
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();
                    throw new Exception(ResponseData.Result);
                }
                return Response;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
      
        public async Task<FileChunkDownloadResp> DownloadFileChunk(FileChunkDownloadReq fileChunkDownloadReq)
        {
            FileChunkDownloadResp Response = null;
            try
            {
                var serializedItem = JsonConvert.SerializeObject(fileChunkDownloadReq);
                string Paramters = "api/CB/DownloadFileChunk";
                var requestTask = await AppServiceClient.PostAsync(Paramters, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                var response = Task.Run(() => requestTask);
                Task<string> ResponseData;
                if (response.Result.IsSuccessStatusCode)
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();

                    Response = JsonConvert.DeserializeObject<FileChunkDownloadResp>(ResponseData.Result);

                }
                else
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();
                    throw new Exception(ResponseData.Result);
                }
                return Response;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public async Task<List<UserAccountInfo>> GetUserList()
        {
            List<UserAccountInfo> Info = new List<UserAccountInfo>();
            try
            {
                string Paramters = "api/Account/GetUserList";
                var requestTask = await AppServiceClient.GetAsync(Paramters);
                var response = Task.Run(() => requestTask);
                Task<string> ResponseData;
                if (response.Result.IsSuccessStatusCode)
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();

                    Info = JsonConvert.DeserializeObject<List<UserAccountInfo>>(ResponseData.Result);

                }
                else
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();
                    throw new Exception(ResponseData.Result);

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