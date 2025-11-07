
from Constants import *
import win32ui
from EurekaSimLib import Experiment,ExperimentTreeView,MainWindow,ApplicationDocument,PropertyWindow
from ObjectDemoExperiment import *


class AddinSimulationManager():

    defaultNamedOptArg=None
    defaultNamedNotOptArg=None
    defaultUnnamedArg=None

    def __init__(self,objAddin):
            self.m_strCurrentStatusBarMessage=None
            self.m_strSelectedExperiment=None
            self.m_strPluginName=None
            self.m_strRootText=None

            self.m_strExperimentGroup=None
            self.m_strExperimentName=None
            self.m_strExperimentType=PY_SAMPLE_EXPERIMENT_TYPE_GROUP_1
            self.m_bSimulationActive=False
            self.m_objAddin=objAddin
            self.m_ObjectDemoExperiment=ObjectDemoExperiment(self)

            self.m_b3DMode=False
            self.m_lVisualizationMode=0
            self.m_bLogSimulationResultsToCSVFile=False
            self.m_bDisplayRealTimeGraph=False
            self.m_bRecordSimulationAsVideo=False
            self.m_bShowExperimentalParamaters=False
            self.m_strCurrentOutputStatusMessage=None

# Methods from IExperimentTreeViewEvents
    def OnReloadExperiment(self, SessionID=defaultNamedNotOptArg, RootText=defaultNamedNotOptArg, ExperimentGroup=defaultNamedNotOptArg, ExperimentName=defaultNamedNotOptArg):
        if  self.m_objAddin.m_lSessionID != SessionID:
            return
        if RootText == PY_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES:
            self.m_ObjectDemoExperiment.OnReloadExperiment(ExperimentGroup, ExperimentName)

    def OnTreeNodeDblClick(self, SessionID=defaultNamedNotOptArg, RootText=defaultNamedNotOptArg, ExperimentGroup=defaultNamedNotOptArg, ExperimentName=defaultNamedNotOptArg):
        if  self.m_objAddin.m_lSessionID != SessionID:
            return
        else:
            self.m_strRootText = RootText
            self.m_strExperimentGroup = ExperimentGroup
            self.m_strExperimentName = ExperimentName
            self.SetStatusBarMessage(RootText + " | " + ExperimentGroup + " | " + ExperimentName, False)

        if RootText == PY_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES:
            self.m_ObjectDemoExperiment.OnTreeNodeDblClick(ExperimentGroup, ExperimentName)

    def OnTreeNodeSelect(self, SessionID=defaultNamedNotOptArg, RootText=defaultNamedNotOptArg, ExperimentGroup=defaultNamedNotOptArg, ExperimentName=defaultNamedNotOptArg):
        if  self.m_objAddin.m_lSessionID != SessionID:
            return

        else:
            self.m_strRootText = RootText
            self.m_strExperimentGroup = ExperimentGroup
            self.m_strExperimentName = ExperimentName
            self.SetStatusBarMessage(RootText + " | " + ExperimentGroup + " | " + ExperimentName, False)

        if  RootText == PY_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES:
            self.m_ObjectDemoExperiment.OnTreeNodeSelect(ExperimentGroup, ExperimentName)

# Methods from IExperimentEvents
    def OnError(self, ErrorCode=defaultNamedNotOptArg, ErrorDesc=defaultNamedNotOptArg, AdditionalParam=defaultNamedNotOptArg):
        pass

    def OnInitializeLogFileInfo(self, ExperimentName=defaultNamedNotOptArg):
        experiment = Experiment()
        try:
            #Set Polygon Crystal Header File Info
            experiment.WriteCSVLogFileHeaderInfo("Angle,X,Y,Z\n")
        except:
            pass
			

    def OnInitializeSimulationGraph(self, ExperimentName=defaultNamedNotOptArg):
        self.m_ObjectDemoExperiment.InitializeSimulationGraph(ExperimentName)

    def OnInitializeSimulationVideoRecording(self, ExperimentName=defaultNamedNotOptArg):
        pass

    def OnStartSimulation(self, ExperimentName=defaultNamedNotOptArg):
        if self.IsAddinExperimentSelected()==False:
            strMessage= "Failed to Start "+ExperimentName+". Please select Experimental Parameters before starting the Simulation Experiment.."
            win32ui.MessageBox(strMessage)
            return 
        self.LoadOtherSimulationOptions()
        self.ResetAllStatusWindows() 
        self.m_ObjectDemoExperiment.StartSimulation(self.m_strExperimentGroup, self.m_strExperimentName)

    def OnStatusChange(self, StatusCode=defaultNamedNotOptArg, StatusDesc=defaultNamedNotOptArg, AdditionalParam=defaultNamedNotOptArg):
        if  StatusCode == 10000: #No Experiment Selected
            pass
        elif StatusCode == 10001 and self.m_objAddin.m_lSessionID == int(AdditionalParam): #Experiment Changed 
            self.m_ObjectDemoExperiment.LoadAllExperiments()
            self.m_ObjectDemoExperiment.OnReloadExperiment(self.m_strExperimentGroup, self.m_strExperimentName)
            self.m_strSelectedExperiment = StatusCode
            self.LoadDefaultSelection()

    def OnStopSimulation(self, ExperimentName=defaultNamedNotOptArg):
        self.SetSimulationStatus(False)

# Methods from IApplicationDocumentEvents
    def OnBeforeSaveDocument(self, DocumentPath=defaultNamedNotOptArg):
        try:
            self.SetDataToDocument()
        except Exception as e:
                win32ui.MessageBox(str(e))

    def OnCloseDocument(self, DocumentPath=defaultNamedNotOptArg):
        pass

    def OnDocumentOpened(self, DocumentPath=defaultNamedNotOptArg):
        if self.LoadDataFromDocument():
            self.LoadDefaultSelection()
        else:
            pass

    def OnNewDocument(self, DocumentName=defaultNamedNotOptArg):
        pass

# Methods from IApplicationViewEvents
    def OnActivateView(self, bActivate=defaultNamedNotOptArg, CurrentViewFilePath=defaultNamedNotOptArg, PreviousViewFilePath=defaultNamedNotOptArg):
        if bActivate:
            self.LoadDefaultSelection()

    def OnDrawPredefinedScene(self, Experiment=defaultNamedNotOptArg):
        pass

    def OnDrawSimulation(self):
        pass

    def OnInitializeSimulation(self, b3DMode=defaultNamedNotOptArg, VisualizationMode=defaultNamedNotOptArg, Experiment=defaultNamedNotOptArg):
        self.m_b3DMode = b3DMode
        self.m_lVisualizationMode = VisualizationMode
        self.LoadOtherSimulationOptions()
        #Reload the selected experiment to reflect the Visualization Mode
        self.OnReloadExperiment(self.m_objAddin.m_lSessionID, self.m_strRootText, self.m_strExperimentGroup, self.m_strExperimentName)

    def OnOwnerDrawCreate(self):
        pass

    def OnOwnerDrawSimulation(self):
        pass

    def ViewWndProc(self, MsgID=defaultNamedNotOptArg, wParam=defaultNamedNotOptArg, lParam=defaultNamedNotOptArg):
        pass

# Methods from IMainApplicationEvents
    def OnApplicationClose(self):
        pass

    def OnApplicationLaunched(self):
        pass

# Methods from IMainWindowEvents
    def MianWndProc(self, MsgID=defaultNamedNotOptArg, wParam=defaultNamedNotOptArg, lParam=defaultNamedNotOptArg):
        pass
# Methods from IPropertyWindowEvents
    def OnPropertyChanged(self, GroupName=defaultNamedNotOptArg, PropertyName=defaultNamedNotOptArg, PropertyValue=defaultNamedNotOptArg):
        if GroupName == OBJECT_PROPERTIES_TITLE:
             self.m_ObjectDemoExperiment.OnPropertyChanged(GroupName, PropertyName, PropertyValue)

# Methods from IRibbonToolbarEvents
    def GetControlStatus(self, CtrlID=defaultNamedNotOptArg, pStatus=defaultNamedNotOptArg):
        pStatus = True
    def OnAfterAddinControlsLoad(self):
        pass

    def OnBeforeAddinControlsLoad(self):
        pass

    def RibbonWndProc(self, MsgID=defaultNamedNotOptArg, wParam=defaultNamedNotOptArg, lParam=defaultNamedNotOptArg):
        pass
# Methods from IFileSettingsTreeViewEvents
    def OnActivateSnapshot(self, FilePath=defaultNamedNotOptArg, GroupName=defaultNamedNotOptArg, SnapshotName=defaultNamedNotOptArg):
        self.LoadDataFromDocument()

    def OnAddSnapshot(self, FilePath=defaultNamedNotOptArg, GroupName=defaultNamedNotOptArg, SnapshotName=defaultNamedNotOptArg):
        pass

    def OnDeleteAllSnapshot(self, FilePath=defaultNamedNotOptArg):
        pass

    def OnDeleteSnapshot(self, FilePath=defaultNamedNotOptArg, GroupName=defaultNamedNotOptArg, SnapshotName=defaultNamedNotOptArg):
        pass


#  Other Dependent Methods
    def LoadDefaultSelection(self):
        SelectedExperiment = None
        SessionID = -1
        experiment = Experiment()
        SelectedExperiment, SessionID=experiment.GetSelectedExperiment(SelectedExperiment,SessionID)
        if self.m_objAddin.m_lSessionID == SessionID:
           self.LoadExperiments()
           if self.m_strExperimentName !="":			
                   self.OnTreeNodeDblClick(SessionID, self.m_strRootText, self.m_strExperimentGroup, self.m_strExperimentName)
                   #Now selcet the tree now and Exand it
                   experimentTreeView = ExperimentTreeView()
                   experimentTreeView.SetTreeGroupState(self.m_strExperimentGroup,TVE_EXPAND)
                   experimentTreeView.SelectActiveExperiment(SessionID, self.m_strExperimentGroup, self.m_strExperimentName)
                   self.OnReloadExperiment(SessionID, self.m_strRootText, self.m_strExperimentGroup, self.m_strExperimentName)

    def IsAddinExperimentSelected(self):
        try:
            self.m_strSelectedExperiment = None
            SelectedExperiment = None
            SessionID = -1
            experiment = Experiment()
            SelectedExperiment, SessionID=experiment.GetSelectedExperiment(SelectedExperiment, SessionID)
            if (self.m_objAddin.m_lSessionID != SessionID):
               strMessage = "Error in Loading Experiment. Selected Experiment Name "+SelectedExperiment+" | Addin SessionID "+self.m_objAddin.m_lSessionID+" | Current SessionID "+SessionID
               win32ui.MessageBox(strMessage)
               return False
            self.m_strSelectedExperiment = SelectedExperiment
            return True
        except Exception as ex:
            strErrorMsg = "Failed to Start Experiment Interface | GetLastError Returns "+ex
            win32ui.MessageBox(strErrorMsg)
            return False

    def LoadExperiments(self):
        if self.IsAddinExperimentSelected() == False:
            return
        self.m_ObjectDemoExperiment.LoadAllExperiments()

    def SetPluginName(self,strPluginName):
        self.m_strPluginName = strPluginName

    def ResetAllStatusWindows(self):
        mainWindow = MainWindow()
        try:
            mainWindow.ResetAllStatusWindows()
        except Exception as e:
                win32ui.MessageBox(e)

    def LoadOtherSimulationOptions(self):
        applicationDocument = ApplicationDocument()
        try:
            self.m_bLogSimulationResultsToCSVFile =applicationDocument.LogToCSVFileStatus
            self.m_bDisplayRealTimeGraph = applicationDocument.DisplayRealTimeGraphStatus
            self.m_bRecordSimulationAsVideo = applicationDocument.RecordSimulationAsVideoStatus
            self.m_bShowExperimentalParamaters = applicationDocument.DisplayExpParamStatus
        except Exception as e:
                win32ui.MessageBox(e)

    def LoadDataFromDocument(self):
         applicationDocument = ApplicationDocument()
         try:
            strEncodedData = ""
            strEncodedData=applicationDocument.GetAddinSettingsAsString(self.m_strPluginName, PY_SAMPLE_DOC_SETTINGS_KEY,strEncodedData)
            if strEncodedData=="":
                return False
            if self.DeSerialize(strEncodedData)==False:
                return False
            return True
         except Exception as e:
                win32ui.MessageBox(e)
                return False

    def LogSimulationPoint(self,strLogData):
        try:
            experiment=Experiment()
            experiment.WriteToCSVLogFile(strLogData)
        except Exception as e:
                win32ui.MessageBox(e)

    def ResetPropertyGrid(self):
        try:
            objPropertyWindow = PropertyWindow()
            objPropertyWindow.RemoveAll()
            objPropertyWindow.EnableHeaderCtrl(False)
            objPropertyWindow.EnableDescriptionArea(True)
            objPropertyWindow.SetVSDotNetLook(True)
            objPropertyWindow.MarkModifiedProperties(True, True)
        except Exception as e:
                win32ui.MessageBox(e)

    def AddOperationStatus(self,strStatus, bPostMessage = True):
           self.m_strCurrentOutputStatusMessage=strStatus
           try:
               mainWindow=MainWindow()
               mainWindow.AddOperationStatus(strStatus, bPostMessage)
           except Exception as e:
                win32ui.MessageBox(e)

    def Serialize(self):
        try:
            info = self.m_ObjectDemoExperiment.Serialize()
            info.RootText = self.m_strRootText
            info.ExperimentName = self.m_strExperimentName
            info.ExperimentGroup = self.m_strExperimentGroup
            return info.Serilize()
        except Exception as e:
                win32ui.MessageBox(e)
                return ""

    def SetDataToDocument(self):
        try:
            applicationDocument = ApplicationDocument()
            strEncodedData = self.Serialize()
            if strEncodedData == "":
                return False
            applicationDocument.SetAddinSettingsAsString(self.m_strPluginName, PY_SAMPLE_DOC_SETTINGS_KEY, strEncodedData)
            return True
        except Exception as e:
                win32ui.MessageBox(str(e))
                return False

    def DeSerialize(self,strXMLData):
        try:
            info = ExperimentInfo()
            info.Deserilize(strXMLData)
            self.m_strRootText = info.RootText
            self.m_strExperimentName = info.ExperimentName
            self.m_strExperimentGroup = info.ExperimentGroup
            self.m_ObjectDemoExperiment.DeSerialize(info)
            return True
        except Exception as e:
                win32ui.MessageBox(e)
                return False

    def SetSimulationStatus(self,bActive):
        self.m_bSimulationActive = bActive

    def SetStatusBarMessage(self,strStatus,bPostMessage=True):
        try:
            self.m_strCurrentStatusBarMessage = strStatus
            mWindow = MainWindow()
            mWindow.SetStatusbarMessage(strStatus,bPostMessage)
        except Exception as e:
                win32ui.MessageBox(e)
    
        
  
