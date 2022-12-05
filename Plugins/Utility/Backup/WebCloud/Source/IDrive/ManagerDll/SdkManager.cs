using ManagerDll.Services;
using ManagerDll.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerDll
{

    public class SdkManager
    {
         public AppApiService ApiService = null;
        public Settings set = new Settings();
        public bool auto;
        public string dest;
        public string filepath;
        public Upload up;
        public string FName;
        public SdkManager()
        {
          
        }
        public void ShowUploadForm()
        {
            up = new Upload(set);
            ShowPath(filepath);
            if(auto==true)
            {
                up.setPath(filepath);
                up.UploadEx();
            }
            else if(auto==false)
            {
                up.setPath(filepath);
                up.ShowDialog();
            }
          
        }
        public void GetAutoUpload()
        {
            auto = set.Autoup;
        }
        public void GetDestination()
        {
            dest = set.destpath;
        }
             
        public void ShowSettingsForm()
        {
           
            set.ShowDialog();

        }
        public void ShowDownloadForm()
        {
           
            Download down = new Download(set);
            GetFilename(FName);
            down.LoadFileName(FName);
            down.ShowDialog();
           

        }
        public void ShowPath(string path)
        {
            filepath = path;

        } 
        public void GetFilename(string fileName)
        {
            FName = fileName;
        }
    }
}
