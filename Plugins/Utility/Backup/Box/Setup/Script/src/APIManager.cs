using Box.V2;
using Box.V2.Config;
using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESimBoxCloud
{
    public class APIManager
    {
        const string ClientId = "yh72mzzerwaa2c8ncjln7u5uu6ca7f5h";
        private const string ClientSecret = "ikMMM01T0rEQcp08BSCi4CsSo5UEarNO";
        const string RedirectUrl = "http://127.0.0.1:55333/";
        static BoxClient m_boxclient;
        SettingsForm m_ObjSettingform;
        public static async void  SigInProcessAsync()
        {
            var config = new BoxConfigBuilder(ClientId, ClientSecret, new System.Uri(RedirectUrl)).Build();
            var sdk = new BoxClient(config);
            var authorizationUrl = config.AuthCodeUri;
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(RedirectUrl);
            listener.Start();
            System.Diagnostics.Process.Start(authorizationUrl.ToString());
            var context = listener.GetContext();
            string code = context.Request.QueryString["code"];
            listener.Stop();
            var session = await sdk.Auth.AuthenticateAsync(code);
            m_boxclient = new BoxClient(config, session);
            
        }
        internal void ShowDownloadForm()
        {
            DownloadForm downloadForm = new DownloadForm(m_boxclient);
            downloadForm.Show();
           
        }

        internal void ShowUploadForm()
        {
            UploadForm uploadForm = new UploadForm(m_boxclient);
            uploadForm.Show();
            
        }

        internal void ShowSettingsForm()
        {
            SettingsForm settingsForm = new SettingsForm(m_boxclient);
            settingsForm.Show();
          
        }
        internal void BoxLocationForm()
        {
            BoxLocation BoxForm = new BoxLocation(m_boxclient);
            BoxForm.Show();

        }
        public static void Logout()
        {
            m_boxclient = null;
            
        }

        internal async Task UploadEx()
        {
            //throw new NotImplementedException();
            try
            {
               var Path = ESimBoxCloudImp.m_strFilePath;
                using (FileStream fs = File.Open(Path, FileMode.Open, FileAccess.Read))
                {


                    // Create request object with name and parent folder the file should be uploaded to
                    BoxFileRequest request = new BoxFileRequest();

                    request.Name = ESimBoxCloudImp.m_strFileName;
                   
                    request.Parent = new BoxRequestEntity() { Id = Properties.Settings.Default.UpldId };

                    BoxFile f =await m_boxclient.FilesManager.UploadAsync(request, fs);

                    MessageBox.Show("Uploaded Successfully...");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Box Addin");
            }
        }
    }
}
