using ManagerDll.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ManagerDll.Services
{
    public partial class AppApiService
    {
        AppCommunicationClient AppServiceClient;
        public HttpStatusCode StatusCode { get; set; }
        public string AzureBackendUrl { get; set; }
        public string SubmitURL
        {
            get { return AppServiceClient.SubmitURL; }

        }
        public string UserID
        {
            get { return AppServiceClient.UserID; }

        }
        public AppApiService(string URL)
        {
            AzureBackendUrl = URL;
            InitializeClient();
        }
        public void InitializeClient()
        {
            AppServiceClient = new AppCommunicationClient();
            AppServiceClient.BaseAddress = new Uri($"{AzureBackendUrl}/");
        }
        public void SetUserCredentials(string CustomerEmail, string AuthenticationToken)
        {
            AppServiceClient.SetUserCredentials(CustomerEmail, AuthenticationToken);
        }

    }
}

