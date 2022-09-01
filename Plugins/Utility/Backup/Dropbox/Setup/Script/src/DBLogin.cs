using Dropbox.Api;
using Dropbox.Api.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dropbox
{
    public static class DBLogin
    {
        private const string APP_KEY = "dj7mv3rjvvc28sl";
        private static string m_accessToken = string.Empty;
        private static readonly string m_loopbackHost = "http://127.0.0.1:55333/";
        private static readonly Uri m_redirectURI = new Uri(m_loopbackHost + "authorize");
        private static readonly Uri JSRedirectUri = new Uri(m_loopbackHost + "token");
        private static string m_strRefreshToken = string.Empty;
        private static DateTime m_dtTokenExpiresAt;
      
        public static async Task<DropboxClient> CreateDBClient()
        {
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(768 | 3072);
            m_accessToken = string.Empty;
            m_strRefreshToken = string.Empty;
            m_dtTokenExpiresAt = new DateTime();

            if (Properties.Settings.Default.accessToken != string.Empty)
            {
                m_accessToken = Properties.Settings.Default.accessToken;
            }
            if (Properties.Settings.Default.refreshToken != string.Empty)
            {
                m_strRefreshToken = Properties.Settings.Default.refreshToken;
            }
            m_accessToken = Properties.Settings.Default.accessToken;
            if (!string.IsNullOrEmpty(m_accessToken))
            {
                return new DropboxClient(m_strRefreshToken, APP_KEY, GetConfig());
            }

            string authState = "";
            PKCEOAuthFlow OAuthFlow = null;
            DropboxClient client = null;
            string authURL = GenerateAuthorizationURL(ref authState, ref OAuthFlow);
            if (!string.IsNullOrEmpty(authURL))
            {
                try
                {
                    DropboxCertHelper.InitializeCertPinning();

                    //Http server to receive redirect URL.
                    HttpListener httpListener = new HttpListener();
                    httpListener.Prefixes.Add(m_loopbackHost);
                    httpListener.Start();

                    //Open default browser and load the authorization URL.
                    Process process = Process.Start(authURL.ToString());
                    // Handle OAuth redirect and send URL fragment to local server using JS.
                    await HandleOAuth2Redirect(httpListener);

                    // Handle redirect from JS and process OAuth response.
                    var result = await HandleJSRedirect(httpListener);

                    var tokenResult = await OAuthFlow.ProcessCodeFlowAsync(result, APP_KEY, m_redirectURI.ToString(), state: authState);


                    httpListener.Stop();

                    m_accessToken = tokenResult.AccessToken.ToString();

                    m_strRefreshToken = tokenResult.RefreshToken.ToString();

                    m_dtTokenExpiresAt = (DateTime)tokenResult.ExpiresAt;

                    Properties.Settings.Default.accessToken = m_accessToken;
                    Properties.Settings.Default.refreshToken = m_strRefreshToken;
                    Properties.Settings.Default.tokenExpiresAt = m_dtTokenExpiresAt;
                    Properties.Settings.Default.Save();

                    client = new DropboxClient(m_strRefreshToken, APP_KEY, GetConfig());

                    //foreach (Process proc in Process.GetProcesses())
                    //{
                    //    if (proc.Id == process.Id)
                    //    {
                    //        proc.Kill();
                    //    }
                    //}
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "EurekaSim Dropbox Addin");
                }
            }
            return client;
        }

        //Set client configuration.
        private static DropboxClientConfig GetConfig()
        {
            var httpClient = new HttpClient(new WebRequestHandler { ReadWriteTimeout = 10 * 1000 })
            {
                Timeout = TimeSpan.FromMinutes(20)
            };
            var config = new DropboxClientConfig("EurekaSim_Addin/1.0.0")
            {
                HttpClient = httpClient
            };
            return config;
        }

        private static string GenerateAuthorizationURL(ref string oAuth2State, ref PKCEOAuthFlow OAuthFlow)
        {
            string authURL = "";
            oAuth2State = "";
            OAuthFlow = null;
            try
            {
                oAuth2State = Guid.NewGuid().ToString("N");
                OAuthFlow = new PKCEOAuthFlow();

                Uri authorizationURI = OAuthFlow.GetAuthorizeUri(OAuthResponseType.Code, APP_KEY, m_redirectURI.ToString(), state: oAuth2State, false, false, null, false, TokenAccessType.Offline, null, includeGrantedScopes: IncludeGrantedScopes.None);
                authURL = authorizationURI.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to generate Authorization URL", "EurekaSim Dropbox Addin");
            }
            return authURL;
        }

        private static async Task HandleOAuth2Redirect(HttpListener http)
        {
            var context = await http.GetContextAsync();

            // We only care about request to RedirectUri endpoint.
            while (context.Request.Url.AbsolutePath != m_redirectURI.AbsolutePath)
            {
                context = await http.GetContextAsync();
            }

            context.Response.ContentType = "text/html";

            // Respond with a page which runs JS and sends URL fragment as query string
            // to TokenRedirectUri.
            string htmlFile = "<html><script type = \"text/javascript\">function redirect() {document.location.href = \"/token?url_with_fragment=\" + encodeURIComponent(document.location.href);}</script><body onload = \"redirect()\" /></html>";
            byte[] byteArray = Encoding.ASCII.GetBytes(htmlFile);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                stream.CopyTo(context.Response.OutputStream);
            }
            context.Response.OutputStream.Close();
        }

        private static async Task<Uri> HandleJSRedirect(HttpListener http)
        {
            var context = await http.GetContextAsync();

            // We only care about request to TokenRedirectUri endpoint.
            while (context.Request.Url.AbsolutePath != JSRedirectUri.AbsolutePath)
            {
                context = await http.GetContextAsync();
            }

            var redirectUri = new Uri(context.Request.QueryString["url_with_fragment"]);
            return redirectUri;
        }
    }
}
