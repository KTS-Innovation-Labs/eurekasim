#
# To unregister completely:
#   MyPythonAddinImp.py --unregister
#
# To debug, execute:
#   MyPythonAddinImp.py --debug
#
# Then open Pythonwin, and select "Tools->Trace Collector Debugging Tool"
# Restart EurekaSim, and you should see some output generated.
# call EurekaSim Automation COM Object like this => View=win32com.client.Dispatch('EurekaSim.ApplicationView')


import winerror
import pythoncom
import win32com
import sys
import os.path
import win32api
import win32gui
import win32con
import win32ui
import EurekaSimLib

from win32com.client import constants
from win32com import universal
from win32com.server.exception import COMException
from win32com.client import gencache, Dispatch, DispatchWithEvents
from EurekaSimLib import IAddin , IApplicationView

gencache.EnsureModule('{38379857-2986-4CD5-B26F-C2FED468D342}', 0, 1, 0, bForDemand=True) 

universal.RegisterInterfaces('{38379857-2986-4CD5-B26F-C2FED468D342}', 0, 1, 0, ["IAddin"])

class MyPythonAddinImp:
    _com_interfaces_ = ['IAddin']
    _public_methods_ = ['InvokePreferenceSettings','InvokeDefaultSettings']
    _reg_clsctx_ = pythoncom.CLSCTX_INPROC_SERVER
    _reg_clsid_ = "{0F47D9F3-598B-4d24-B7F4-92AC15ED27E2}"
    _reg_progid_ = "MyPythonAddinImp"
    _reg_policy_spec_ = "win32com.server.policy.EventHandlerPolicy"
    def Initialize(self, lSessionID, pApp, bFirstTime):
        try:
            my_path = os.path.abspath(os.path.dirname(__file__))
            text_file = open(os.path.join(my_path,"AddinRibbon.xml"), "r")
            #read whole file to a string
            XmlRibbonToolbarSettings = text_file.read()
            text_file.close()
            #Now Load Bitmap and its handle
            self.appHostApp = pApp
            self.bmp_name =os.path.join(my_path,"toolbar1.bmp")
            flags = win32con.LR_DEFAULTSIZE | win32con.LR_LOADFROMFILE
            self.bmp_handle = win32gui.LoadImage(0, self.bmp_name,
                                                 win32con.IMAGE_BITMAP,
                                                 0, 0, flags)
            self.appHostApp.SetAddInInfo(lSessionID, 0, XmlRibbonToolbarSettings, self.bmp_handle, "");
        except:
            win32ui.MessageBox("Exception In Initialize")
    def Uninitialize(self, lSessionID):
        #win32ui.MessageBox("Uninitialize from Python")
        Message="hai"
        
    def About(self):
        win32ui.MessageBox("About Box from Python")
    def InvokePreferenceSettings(self):
        win32ui.MessageBox("InvokePreferenceSettings From Python") 
    def InvokeDefaultSettings(self):
        try:
            # Call a Sample EurekaSim COM Boject
            #View=win32com.client.Dispatch('EurekaSim.ApplicationView')
            win32ui.MessageBox("InvokeDefaultSettings from Python")
        except Exception as ex:
            win32ui.MessageBox(str(ex))
def RegisterAddin(klass):
    import winreg
    key = winreg.CreateKey(winreg.HKEY_CLASSES_ROOT, "CLSID\\"+ klass._reg_clsid_)
    subkey = winreg.CreateKey(key, "Implemented Categories")
    winreg.CreateKey(subkey, "{66EBC8D5-A4D0-48C4-978B-55D1B34E157B}")
    
def UnregisterAddin(klass):
    import winreg
    try:
        winreg.DeleteKey(winreg.HKEY_CLASSES_ROOT, "CLSID\\"+ klass._reg_clsid_)
    except WindowsError:
        pass

if __name__ == '__main__':
    import win32com.server.register
    win32com.server.register.UseCommandLine(MyPythonAddinImp)
    if "--unregister" in sys.argv:
        UnregisterAddin(MyPythonAddinImp)
    else:
        RegisterAddin(MyPythonAddinImp)
