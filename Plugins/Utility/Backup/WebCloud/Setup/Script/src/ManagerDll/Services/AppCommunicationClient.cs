using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerDll.Services
{
    public class AppCommunicationClient : HttpClient
    {
        public string UserID { get; set; }//useremail in mobile
        private string m_AuthenticationToken;
        public string SubmitURL { get; set; }

        public void SetUserCredentials(string CustomerEmail, string AuthenticationToken)
        {
            
            UserID = CustomerEmail;
            m_AuthenticationToken = AuthenticationToken;
            
        }
        public new Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            if (!string.IsNullOrEmpty(m_AuthenticationToken))
            {
                DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_AuthenticationToken);
            }
            else
            {
                DefaultRequestHeaders.Clear();
            }
            SubmitURL = requestUri;
            return base.GetAsync(requestUri);
            
        }
        public new Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            if (!string.IsNullOrEmpty(m_AuthenticationToken))
            {
                DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_AuthenticationToken);
            }
            SubmitURL = requestUri;
            return base.PostAsync(requestUri, content);
        }
}
}
