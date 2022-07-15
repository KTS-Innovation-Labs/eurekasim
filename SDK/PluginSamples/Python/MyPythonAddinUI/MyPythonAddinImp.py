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


from numpy import var
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
import gc

from win32com.client import constants
from win32com import universal
from win32com.server.exception import COMException
from win32com.client import gencache, Dispatch, DispatchWithEvents
from EurekaSimLib import *
from Constants import *
from AddinSimulationManager import AddinSimulationManager
import comtypes
from comtypes import POINTER 

gencache.EnsureModule('{38379857-2986-4CD5-B26F-C2FED468D342}', 0, 1, 0, bForDemand=True) 

universal.RegisterInterfaces('{38379857-2986-4CD5-B26F-C2FED468D342}', 0, 1, 0, ["IAddin","IExperimentTreeViewEvents",
                                            "IExperimentEvents","IApplicationDocumentEvents","IApplicationViewEvents",
                                            "IMainApplicationEvents","IMainWindowEvents","IPropertyWindowEvents",
                                            "IRibbonToolbarEvents","IFileSettingsTreeViewEvents"])
                        
                                                                                 
class MyPythonAddinImp():
    _com_interfaces_ = ['IAddin','IExperimentTreeViewEvents','IExperimentEvents',
                        'IApplicationDocumentEvents','IApplicationViewEvents',
                        'IMainApplicationEvents','IMainWindowEvents',
                        'IPropertyWindowEvents','IRibbonToolbarEvents',
                        'IFileSettingsTreeViewEvents']

    _public_methods_ = ['InvokePreferenceSettings','InvokeDefaultSettings',"InvokeOnExperimentChanged"]
    _reg_clsctx_ = pythoncom.CLSCTX_INPROC_SERVER
    _reg_clsid_ = "{0F47D9F3-598B-4d24-B7F4-92AC15ED27E2}"
    _reg_progid_ = "MyPythonAddinImp"
    _reg_policy_spec_ = "win32com.server.policy.EventHandlerPolicy"

    defaultNamedOptArg=pythoncom.Empty
    defaultNamedNotOptArg=pythoncom.Empty
    defaultUnnamedArg=pythoncom.Empty
    

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
            self.appHostApp.SetAddInInfo(lSessionID, 0, XmlRibbonToolbarSettings, self.bmp_handle, "")
            experiment=Experiment()
            self.m_lSessionID=lSessionID
            experiment.AddExperiment(lSessionID,PY_SAMPLE_MAIN_EXPERIMENT_NAME)
            self.m_objAddinSimulationManager=AddinSimulationManager(self)
        except Exception as ex:
            win32ui.MessageBox(str(ex))

    def Uninitialize(self, lSessionID):
        #win32ui.MessageBox("Uninitialize from Python")
        #use the below code below to exit the EurekaSim.exe form task manager. Else it will not quit
        del  self.appHostApp
        gc.collect()
        
    def About(self):
        win32ui.MessageBox("About Box from Python")

# Methods For XML Ribbon Toolbar
    def InvokePreferenceSettings(self):
        win32ui.MessageBox("InvokePreferenceSettings From Python") 
    def InvokeDefaultSettings(self):
        try:
            # Call a Sample EurekaSim COM Boject
            #View=win32com.client.Dispatch('EurekaSim.ApplicationView')
            win32ui.MessageBox("InvokeDefaultSettings from Python")
        except Exception as ex:
            win32ui.MessageBox(str(ex))

    def InvokeOnExperimentChanged(self):
        ribbonToolbar = RibbonToolbar()
        strExpName=None
        try:
            res=ribbonToolbar.GetControlText("Py.Sample.Experimental.Setup.Select.Experiment",strExpName)
            self.m_objAddinSimulationManager.LoadExperiments(res)
        except Exception as e:
            win32ui.MessageBox(str(e))

    def SetRibbonControlText(self,ID,Text):
        ribToolbar=RibbonToolbar()
        ribToolbar.SetControlText(ID, Text)

# Methods from IExperimentTreeViewEvents
    def OnReloadExperiment(self, SessionID=defaultNamedNotOptArg, RootText=defaultNamedNotOptArg, ExperimentGroup=defaultNamedNotOptArg, ExperimentName=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnReloadExperiment(SessionID,RootText,ExperimentGroup,ExperimentName)

    def OnTreeNodeDblClick(self, SessionID=defaultNamedNotOptArg, RootText=defaultNamedNotOptArg, ExperimentGroup=defaultNamedNotOptArg, ExperimentName=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnTreeNodeDblClick(SessionID,RootText,ExperimentGroup,ExperimentName)

    def OnTreeNodeSelect(self, SessionID=defaultNamedNotOptArg, RootText=defaultNamedNotOptArg, ExperimentGroup=defaultNamedNotOptArg, ExperimentName=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnTreeNodeSelect(SessionID,RootText,ExperimentGroup,ExperimentName)

# Methods from IExperimentEvents
    def OnError(self, ErrorCode=defaultNamedNotOptArg, ErrorDesc=defaultNamedNotOptArg, AdditionalParam=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnError(ErrorCode, ErrorDesc, AdditionalParam)

    def OnInitializeLogFileInfo(self, ExperimentName=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnInitializeLogFileInfo(ExperimentName)

    def OnInitializeSimulationGraph(self, ExperimentName=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnInitializeSimulationGraph(ExperimentName)

    def OnInitializeSimulationVideoRecording(self, ExperimentName=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnInitializeSimulationVideoRecording(ExperimentName)

    def OnStartSimulation(self, ExperimentName=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnStartSimulation(ExperimentName)

    def OnStatusChange(self, StatusCode=defaultNamedNotOptArg, StatusDesc=defaultNamedNotOptArg, AdditionalParam=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnStatusChange(StatusCode,StatusDesc,AdditionalParam)

    def OnStopSimulation(self, ExperimentName=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnStopSimulation(ExperimentName)

# Methods from IApplicationDocumentEvents
    def OnBeforeSaveDocument(self, DocumentPath=defaultNamedNotOptArg):
       self.m_objAddinSimulationManager.OnBeforeSaveDocument(DocumentPath)
    def OnAfterSaveDocument(self, DocumentPath=defaultNamedNotOptArg):
       self.m_objAddinSimulationManager.OnBeforeSaveDocument(DocumentPath)
       
    def OnCloseDocument(self, DocumentPath=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnCloseDocument(DocumentPath)

    def OnDocumentOpened(self, DocumentPath=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnDocumentOpened(DocumentPath)

    def OnNewDocument(self, DocumentPath=defaultNamedNotOptArg):
         self.m_objAddinSimulationManager.OnNewDocument(DocumentPath)

# Methods from IApplicationViewEvents
    def OnActivateView(self, bActivate=defaultNamedNotOptArg, CurrentViewFilePath=defaultNamedNotOptArg, PreviousViewFilePath=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnActivateView(bActivate, CurrentViewFilePath, PreviousViewFilePath)

    def OnDrawPredefinedScene(self, Experiment=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnDrawPredefinedScene(Experiment)

    def OnDrawSimulation(self):
        self.m_objAddinSimulationManager.OnDrawSimulation()

    def OnInitializeSimulation(self, b3DMode=defaultNamedNotOptArg, VisualizationMode=defaultNamedNotOptArg, Experiment=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnInitializeSimulation(b3DMode, VisualizationMode, Experiment)

    def OnOwnerDrawCreate(self):
        self.m_objAddinSimulationManager.OnOwnerDrawCreate()

    def OnOwnerDrawSimulation(self):
        self.m_objAddinSimulationManager.OnOwnerDrawSimulation()

    def ViewWndProc(self, MsgID=defaultNamedNotOptArg, wParam=defaultNamedNotOptArg, lParam=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.ViewWndProc(MsgID, wParam, lParam)

# Methods from IMainApplicationEvents
    def OnApplicationClose(self):
        self.m_objAddinSimulationManager.OnApplicationClose()

    def OnApplicationLaunched(self):
        self.m_objAddinSimulationManager.OnApplicationLaunched()

# Methods from IMainWindowEvents
    def MianWndProc(self, MsgID=defaultNamedNotOptArg, wParam=defaultNamedNotOptArg, lParam=defaultNamedNotOptArg):
         self.m_objAddinSimulationManager.MianWndProc(MsgID, wParam, lParam)

# Methods from IPropertyWindowEvents
    def OnPropertyChanged(self, GroupName=defaultNamedNotOptArg, PropertyName=defaultNamedNotOptArg, PropertyValue=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnPropertyChanged(GroupName, PropertyName, PropertyValue)

# Methods from IRibbonToolbarEvents
    def GetControlStatus(self, CtrlID=defaultNamedNotOptArg, pStatus=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.GetControlStatus(CtrlID, pStatus)
    def OnAfterAddinControlsLoad(self):
        self.m_objAddinSimulationManager.OnAfterAddinControlsLoad()

    def OnBeforeAddinControlsLoad(self):
        self.m_objAddinSimulationManager.OnBeforeAddinControlsLoad()

    def RibbonWndProc(self, MsgID=defaultNamedNotOptArg, wParam=defaultNamedNotOptArg, lParam=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.RibbonWndProc(MsgID, wParam, lParam)

# Methods from IFileSettingsTreeViewEvents
    def OnActivateSnapshot(self, FilePath=defaultNamedNotOptArg, GroupName=defaultNamedNotOptArg, SnapshotName=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnActivateSnapshot(FilePath, GroupName, SnapshotName)

    def OnAddSnapshot(self, FilePath=defaultNamedNotOptArg, GroupName=defaultNamedNotOptArg, SnapshotName=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnAddSnapshot(FilePath, GroupName, SnapshotName)

    def OnDeleteAllSnapshot(self, FilePath=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnDeleteAllSnapshot(FilePath)

    def OnDeleteSnapshot(self, FilePath=defaultNamedNotOptArg, GroupName=defaultNamedNotOptArg, SnapshotName=defaultNamedNotOptArg):
        self.m_objAddinSimulationManager.OnDeleteSnapshot(FilePath, GroupName, SnapshotName)
        
# Registering and unregister Methods
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
