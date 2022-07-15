using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EurekaSim.Net;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Drawing;

namespace MyCSAddin
{
    [ClassInterface(ClassInterfaceType.AutoDispatch)]

    //Change this GUID for your own plugin...

    [Guid("CA20D230-8AB6-4df7-B77C-06C3C4F8FC17")]

    public class MyCSAddinImp : EurekaSim.Net.Addin
    {
        #region Component Category Registration

        [ComRegisterFunction]

        [ComVisible(false)]

        public static void RegisterFunction(Type t)

        {

            string sKey = @"\CLSID\{" + t.GUID.ToString() + @"}\Implemented Categories";

            Microsoft.Win32.RegistryKey regKey
                = Microsoft.Win32.Registry.ClassesRoot
                .OpenSubKey(sKey, true);

            if (regKey != null)

            {

                regKey.CreateSubKey("{66EBC8D5-A4D0-48C4-978B-55D1B34E157B}");

            }

        }



        [ComUnregisterFunction]

        [ComVisible(false)]

        public static void UnregisterFunction(Type t)

        {

            string sKey = @"\CLSID\{" + t.GUID.ToString() + @"}\Implemented Categories";

            Microsoft.Win32.RegistryKey regKey
                = Microsoft.Win32.Registry.ClassesRoot
                .OpenSubKey(sKey, true);

            if (regKey != null)

            {

                regKey.DeleteSubKey("{66EBC8D5-A4D0-48C4-978B-55D1B34E157B}");
                

            }

        }

        public void About()
        {
            MessageBox.Show("The C# Addin About Box");
        }

        public void Initialize(int lSessionID, MainApplication pApp, object bFirstTime)
        {
            // TODO:  Add CAddinDotNet.SalesMatePlusLib.ISmpAddin.Initialize implementation
            //MessageBox.Show("C#.Initialize");
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            //Read the embedded XML menu resource
            Stream rgbxml = thisAssembly.GetManifestResourceStream("MyCSAddin.AddinRibbon.xml");

            Stream rgbBmp = thisAssembly.GetManifestResourceStream("MyCSAddin.toolbar1.bmp");

            Bitmap bmp = new Bitmap(rgbBmp);

            XmlDocument doc = new XmlDocument();
            doc.Load(rgbxml);
            string strMenuXML = doc.InnerXml;
            //MessageBox.Show(strMenuXML);
            IntPtr bitMapHandle = bmp.GetHbitmap();
           
            pApp.SetAddInInfo(lSessionID, 0, strMenuXML, (long)bitMapHandle, "");
        }

        public void Uninitialize(int lSessionID)
        {
            GC.Collect();
        }

        public void InvokePreferenceSettings()
        {
            MessageBox.Show("C#.InvokePreferenceSettings");
        }

        public void InvokeDefaultSettings()
        {
            MessageBox.Show("C#.InvokeDefaultSettings");
        }

        #endregion
    }
}
