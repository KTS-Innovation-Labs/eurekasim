'====================================================================================================
'The Free Edition of Instant VB limits conversion output to 100 lines per file.

'To purchase the Premium Edition, visit our website:
'https://www.tangiblesoftwaresolutions.com/order/order-instant-vb.html
'====================================================================================================

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading.Tasks
Imports EurekaSim.Net
Imports System.Windows.Forms
Imports System.Reflection
Imports System.IO
Imports System.Xml
Imports System.Drawing

Namespace MyVbDotNetAddin

    'Change this GUID for your own plugin...


    <ClassInterface(ClassInterfaceType.AutoDispatch)>
    <Guid("CA20D230-8AB6-4df7-B77C-06C3C4F9EC17")>
    Public Class MyVbDotNetAddinImp
        Implements EurekaSim.Net.Addin, IExperimentTreeViewEvents, IExperimentEvents, IApplicationDocumentEvents, IApplicationViewEvents, IMainApplicationEvents, IMainWindowEvents, IPropertyWindowEvents, IRibbonToolbarEvents, IFileSettingsTreeViewEvents

#Region "Member Variables"
        Public m_pAddinSimulationManager As AddinSimulationManager 'Addin Simulation Manager
        Public m_lSessionID As Integer
        Public m_ExperimentName As String

#End Region

#Region "Constructor"
        Public Sub New()
            m_pAddinSimulationManager = New AddinSimulationManager(Me) 'Addin Simulation Manager
            m_lSessionID = -1
            m_ExperimentName = ""
        End Sub
#End Region

#Region "Component Category Registration"


        <ComRegisterFunction>
        <ComVisible(False)>
        Public Shared Sub RegisterFunction(ByVal t As Type)


            Dim sKey As String = "\CLSID\{" & t.GUID.ToString() & "}\Implemented Categories"

            Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(sKey, True)

            If regKey IsNot Nothing Then


                regKey.CreateSubKey("{66EBC8D5-A4D0-48C4-978B-55D1B34E157B}")

            End If

        End Sub

        <ComUnregisterFunction>
        <ComVisible(False)>
        Public Shared Sub UnregisterFunction(ByVal t As Type)


            Dim sKey As String = "\CLSID\{" & t.GUID.ToString() & "}\Implemented Categories"

            Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(sKey, True)

            If regKey IsNot Nothing Then


                regKey.DeleteSubKey("{66EBC8D5-A4D0-48C4-978B-55D1B34E157B}")


            End If

        End Sub
#End Region

#Region "IAddin"
        Public Sub Initialize(ByVal lSessionID As Integer, ByVal pApp As MainApplication, ByVal bFirstTime As Object) Implements EurekaSim.Net.IAddin.Initialize
            ' TODO:  Add CAddinDotNet.SalesMatePlusLib.ISmpAddin.Initialize implementation
            'MessageBox.Show("C#.Initialize");
            Dim thisAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            'Read the embedded XML menu resource
            Dim rgbxml As Stream = thisAssembly.GetManifestResourceStream("AddinRibbon.xml")

            Dim rgbBmp As Stream = thisAssembly.GetManifestResourceStream("toolbar1.bmp")

            Dim bmp As New Bitmap(rgbBmp)

            Dim doc As New XmlDocument()
            doc.Load(rgbxml)
            LoadAddinName(doc)
            Dim strMenuXML As String = doc.InnerXml
            'MessageBox.Show(strMenuXML);
            Dim bitMapHandle As IntPtr = bmp.GetHbitmap()
            m_lSessionID = lSessionID
            pApp.SetAddInInfo(lSessionID, 0, strMenuXML, CLng(bitMapHandle), "")
            Dim experiment As New Experiment()
            experiment.AddExperiment(lSessionID, Constants.CS_SAMPLE_MAIN_EXPERIMENT_NAME)

        End Sub

        Private Sub LoadAddinName(ByVal doc As XmlDocument)
            Dim root As XmlElement = doc.DocumentElement
            Dim strPluginName As String = root.Attributes("label").Value
            m_pAddinSimulationManager.SetPluginName(strPluginName)
        End Sub

        Public Sub Uninitialize(ByVal lSessionID As Integer) Implements EurekaSim.Net.IAddin.Uninitialize
            GC.Collect()
        End Sub
        Public Sub About() Implements EurekaSim.Net.IAddin.About
            MessageBox.Show("The C# Addin About Box")
        End Sub
#End Region

        Public Sub InvokePreferenceSettings()
            MessageBox.Show("C#.InvokePreferenceSettings")
        End Sub

        Public Sub InvokeDefaultSettings()
            MessageBox.Show("C#.InvokeDefaultSettings")
        End Sub
#Region "IExperimentTreeViewEvents"
        Public Sub OnTreeNodeSelect(ByVal SessionID As Integer, ByVal RootText As String, ByVal ExperimentGroup As String, ByVal ExperimentName As String) Implements EurekaSim.Net.IExperimentTreeViewEvents.OnTreeNodeSelect
            m_pAddinSimulationManager.OnTreeNodeSelect(SessionID, RootText, ExperimentGroup, ExperimentName)
        End Sub

        Public Sub OnTreeNodeDblClick(ByVal SessionID As Integer, ByVal RootText As String, ByVal ExperimentGroup As String, ByVal ExperimentName As String) Implements EurekaSim.Net.IExperimentTreeViewEvents.OnTreeNodeDblClick
            m_pAddinSimulationManager.OnTreeNodeDblClick(SessionID, RootText, ExperimentGroup, ExperimentName)
        End Sub

        Public Sub OnReloadExperiment(ByVal SessionID As Integer, ByVal RootText As String, ByVal ExperimentGroup As String, ByVal ExperimentName As String) Implements EurekaSim.Net.IExperimentTreeViewEvents.OnReloadExperiment
            m_pAddinSimulationManager.OnReloadExperiment(SessionID, RootText, ExperimentGroup, ExperimentName)
        End Sub
#End Region

#Region "IExperimentEvents"
        Public Sub OnStartSimulation(ByVal ExperimentName As String) Implements EurekaSim.Net.IExperimentEvents.OnStartSimulation
            m_pAddinSimulationManager.OnStartSimulation(ExperimentName)
        End Sub

        Public Sub OnStopSimulation(ByVal ExperimentName As String) Implements EurekaSim.Net.IExperimentEvents.OnStopSimulation
            m_pAddinSimulationManager.OnStopSimulation(ExperimentName)
        End Sub

        Public Sub OnStatusChange(ByVal StatusCode As Integer, ByVal StatusDesc As String, ByVal AdditionalParam As String) Implements EurekaSim.Net.IExperimentEvents.OnStatusChange
            m_pAddinSimulationManager.OnStatusChange(StatusCode, StatusDesc, AdditionalParam)
        End Sub

        Public Sub OnError(ByVal ErrorCode As Integer, ByVal ErrorDesc As String, ByVal AdditionalParam As String) Implements EurekaSim.Net.IExperimentEvents.OnError
            m_pAddinSimulationManager.OnError(ErrorCode, ErrorDesc, AdditionalParam)
        End Sub

        Public Sub OnInitializeLogFileInfo(ByVal ExperimentName As String) Implements EurekaSim.Net.IExperimentEvents.OnInitializeLogFileInfo
            m_pAddinSimulationManager.OnInitializeLogFileInfo(ExperimentName)
        End Sub

        Public Sub OnInitializeSimulationGraph(ByVal ExperimentName As String) Implements EurekaSim.Net.IExperimentEvents.OnInitializeSimulationGraph
            m_pAddinSimulationManager.OnInitializeSimulationGraph(ExperimentName)
        End Sub

        Public Sub OnInitializeSimulationVideoRecording(ByVal ExperimentName As String) Implements EurekaSim.Net.IExperimentEvents.OnInitializeSimulationVideoRecording
            m_pAddinSimulationManager.OnInitializeSimulationVideoRecording(ExperimentName)
        End Sub
#End Region

#Region "IApplicationDocumentEvents"
        Public Sub OnNewDocument(ByVal DocumentName As String) Implements EurekaSim.Net.IApplicationDocumentEvents.OnNewDocument
            m_pAddinSimulationManager.OnNewDocument(DocumentName)
        End Sub

        Public Sub OnDocumentOpened(ByVal DocumentPath As String) Implements EurekaSim.Net.IApplicationDocumentEvents.OnDocumentOpened
            m_pAddinSimulationManager.OnDocumentOpened(DocumentPath)
        End Sub

        Public Sub OnCloseDocument(ByVal DocumentPath As String) Implements EurekaSim.Net.IApplicationDocumentEvents.OnCloseDocument
            m_pAddinSimulationManager.OnCloseDocument(DocumentPath)
        End Sub

        Public Sub OnBeforeSaveDocument(ByVal DocumentPath As String) Implements EurekaSim.Net.IApplicationDocumentEvents.OnBeforeSaveDocument
            m_pAddinSimulationManager.OnBeforeSaveDocument(DocumentPath)
        End Sub
		Public Sub OnAfterSaveDocument(ByVal DocumentPath As String) Implements EurekaSim.Net.IApplicationDocumentEvents.OnAfterSaveDocument
            
        End Sub
#End Region

#Region "IApplicationViewEvents"
        Public Sub OnDrawSimulation() Implements EurekaSim.Net.IApplicationViewEvents.OnDrawSimulation
            m_pAddinSimulationManager.OnDrawSimulation()
        End Sub

        Public Sub OnInitializeSimulation(ByVal b3DMode As Integer, ByVal VisualizationMode As Integer, ByVal Experiment As String) Implements EurekaSim.Net.IApplicationViewEvents.OnInitializeSimulation
            m_pAddinSimulationManager.OnInitializeSimulation(b3DMode, VisualizationMode, Experiment)
        End Sub

        Public Sub OnDrawPredefinedScene(ByVal Experiment As String) Implements EurekaSim.Net.IApplicationViewEvents.OnDrawPredefinedScene
            m_pAddinSimulationManager.OnDrawPredefinedScene(Experiment)
        End Sub

        Public Sub OnOwnerDrawSimulation() Implements EurekaSim.Net.IApplicationViewEvents.OnOwnerDrawSimulation
            m_pAddinSimulationManager.OnOwnerDrawSimulation()
        End Sub

        Public Sub OnOwnerDrawCreate() Implements EurekaSim.Net.IApplicationViewEvents.OnOwnerDrawCreate
            m_pAddinSimulationManager.OnOwnerDrawCreate()
        End Sub

        Public Sub ViewWndProc(ByVal MsgID As Integer, ByVal wParam As Object, ByVal lParam As Object) Implements EurekaSim.Net.IApplicationViewEvents.ViewWndProc
            m_pAddinSimulationManager.ViewWndProc(MsgID, wParam, lParam)
        End Sub

        Public Sub OnActivateView(ByVal bActivate As Integer, ByVal CurrentViewFilePath As String, ByVal PreviousViewFilePath As String) Implements EurekaSim.Net.IApplicationViewEvents.OnActivateView
            m_pAddinSimulationManager.OnActivateView(bActivate, CurrentViewFilePath, PreviousViewFilePath)
        End Sub
#End Region

#Region "IMainApplicationEvents"
        Public Sub OnApplicationLaunched() Implements EurekaSim.Net.IMainApplicationEvents.OnApplicationLaunched
            m_pAddinSimulationManager.OnApplicationLaunched()
        End Sub

        Public Sub OnApplicationClose() Implements EurekaSim.Net.IMainApplicationEvents.OnApplicationClose
            m_pAddinSimulationManager.OnApplicationClose()
        End Sub
#End Region

#Region "IMainWindowEvents"
        Public Sub MianWndProc(ByVal MsgID As Integer, ByVal wParam As Object, ByVal lParam As Object) Implements EurekaSim.Net.IMainWindowEvents.MianWndProc
            m_pAddinSimulationManager.MianWndProc(MsgID, wParam, lParam)
        End Sub
#End Region
#Region "IPropertyWindowEvents"

        Public Sub OnPropertyChanged(ByVal GroupName As String, ByVal PropertyName As String, ByVal PropertyValue As String) Implements EurekaSim.Net.IPropertyWindowEvents.OnPropertyChanged
            m_pAddinSimulationManager.OnPropertyChanged(GroupName, PropertyName, PropertyValue)
        End Sub
#End Region

#Region "IRibbonToolbarEvents"
        Public Sub OnBeforeAddinControlsLoad() Implements EurekaSim.Net.IRibbonToolbarEvents.OnBeforeAddinControlsLoad
            m_pAddinSimulationManager.OnBeforeAddinControlsLoad()
        End Sub

        Public Sub OnAfterAddinControlsLoad() Implements EurekaSim.Net.IRibbonToolbarEvents.OnAfterAddinControlsLoad
            m_pAddinSimulationManager.OnAfterAddinControlsLoad()
        End Sub

        Public Sub GetControlStatus(ByVal CtrlID As String, ByRef pStatus As Integer) Implements EurekaSim.Net.IRibbonToolbarEvents.GetControlStatus
            m_pAddinSimulationManager.GetControlStatus(CtrlID, pStatus)
        End Sub

        Public Sub RibbonWndProc(ByVal MsgID As Integer, ByVal wParam As Object, ByVal lParam As Object) Implements EurekaSim.Net.IRibbonToolbarEvents.RibbonWndProc
            m_pAddinSimulationManager.RibbonWndProc(MsgID, wParam, lParam)
        End Sub
#End Region

#Region "IFileSettingsTreeViewEvents"

        Public Sub OnActivateSnapshot(ByVal FilePath As String, ByVal GroupName As String, ByVal SnapshotName As String) Implements EurekaSim.Net.IFileSettingsTreeViewEvents.OnActivateSnapshot
            m_pAddinSimulationManager.OnActivateSnapshot(FilePath, GroupName, SnapshotName)
        End Sub

        Public Sub OnAddSnapshot(ByVal FilePath As String, ByVal GroupName As String, ByVal SnapshotName As String) Implements EurekaSim.Net.IFileSettingsTreeViewEvents.OnAddSnapshot
            m_pAddinSimulationManager.OnAddSnapshot(FilePath, GroupName, SnapshotName)
        End Sub

        Public Sub OnDeleteSnapshot(ByVal FilePath As String, ByVal GroupName As String, ByVal SnapshotName As String) Implements EurekaSim.Net.IFileSettingsTreeViewEvents.OnDeleteSnapshot
            m_pAddinSimulationManager.OnDeleteSnapshot(FilePath, GroupName, SnapshotName)
        End Sub

        Public Sub OnDeleteAllSnapshot(ByVal FilePath As String) Implements EurekaSim.Net.IFileSettingsTreeViewEvents.OnDeleteAllSnapshot
            m_pAddinSimulationManager.OnDeleteAllSnapshot(FilePath)
        End Sub
#End Region

    End Class
End Namespace
