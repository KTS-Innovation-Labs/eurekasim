using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using System.Reflection;

namespace goolgedrivetest
{
   
    public class Googledrivelogin
        {

        public static string m_strFilePath;
        public static string m_uploadlocation;
        public static string m_downloadlocation;
        public static string savestaus;
        private static string CredentialPath = " ";

        private string[] Scopes = {
                                     DriveService.Scope.Drive,
                                      DriveService.Scope.DriveFile,
                                      DriveService.Scope.DriveMetadata,
           
                                  };
        private const string ApplicationName = "EurekasimPlugin";
        public static bool loginstatus;
        private UserCredential _userCredential;
        public  static DriveService _driveService;
        private static string Path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        private static int r;
        public Googledrivelogin()
        {
        }

        public DriveService GetAuth()
        {


            string pathres = Environment.ExpandEnvironmentVariables(@"%AppData%\Local\EurekaSimGDrive");
            Directory.CreateDirectory(pathres);


            using (var stream = new FileStream(Path + "\\google_secret.json", System.IO.FileMode.Open, System.IO.FileAccess.Read))

            {
                var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(pathres, true));
                _userCredential = credentials.Result;
                if (credentials.IsCanceled || credentials.IsFaulted)
                    throw new Exception("cannot connect");

                _driveService = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = _userCredential,
                    ApplicationName = ApplicationName,
                });

                Properties.Settings.Default.accessToken = _userCredential.GetAccessTokenForRequestAsync().Result;

                return _driveService;
            }
            
        }

       
        public bool logoutfun()
        {

            _userCredential.RevokeTokenAsync(CancellationToken.None);
                return true;

            
        }
    }


}
