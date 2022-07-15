Imports EurekaSim.Net
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Xml.Serialization

Namespace MyVbDotNetAddin
    Enum EAxisPos
        LeftAxis = 0
        BottomAxis
        RightAxis
        TopAxis
    End Enum

    Public Class AddinSimulationManager
        Private m_strCurrentStatusBarMessage As String
        Private m_strSelectedExperiment As String
        Private m_strPluginName As String
        Private m_strRootText As String
        Public m_strExperimentGroup As String
        Public m_strExperimentName As String
        Public m_bSimulationActive As Boolean
        Public m_pAddin As MyVbDotNetAddinImp
        Public m_pObjectDemoExperiment As ObjectDemoExperiment
        Public m_b3DMode As Boolean
        Public m_lVisualizationMode As Long
        Public m_bLogSimulationResultsToCSVFile As Boolean
        Public m_bDisplayRealTimeGraph As Boolean
        Public m_bRecordSimulationAsVideo As Boolean
        Public m_bShowExperimentalParamaters As Boolean
        Public m_strCurrentOutputStatusMessage As String

        Public Sub New(ByVal pAddin As MyVbDotNetAddinImp)
            m_pAddin = pAddin
            m_pObjectDemoExperiment = New ObjectDemoExperiment(Me)
            m_bSimulationActive = False
        End Sub

        Protected Overrides Sub Finalize()
            m_pAddin = Nothing
            m_pObjectDemoExperiment = Nothing
            m_bSimulationActive = False
            GC.Collect()
        End Sub

        Public Sub OnTreeNodeSelect(ByVal SessionID As Long, ByVal RootText As String, ByVal ExperimentGroup As String, ByVal ExperimentName As String)
            If m_pAddin.m_lSessionID <> SessionID Then
                Return
            Else
                m_strRootText = RootText
                m_strExperimentGroup = ExperimentGroup
                m_strExperimentName = ExperimentName
                SetStatusBarMessage(RootText & " | " & ExperimentGroup & " | " & ExperimentName, False)
            End If

            If RootText = Constants.CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES Then
                m_pObjectDemoExperiment.OnTreeNodeSelect(ExperimentGroup, ExperimentName)
            End If
        End Sub

        Public Sub OnTreeNodeDblClick(ByVal SessionID As Integer, ByVal RootText As String, ByVal ExperimentGroup As String, ByVal ExperimentName As String)
            If m_pAddin.m_lSessionID <> SessionID Then
                Return
            Else
                m_strRootText = RootText
                m_strExperimentGroup = ExperimentGroup
                m_strExperimentName = ExperimentName
                SetStatusBarMessage(RootText & " | " & ExperimentGroup & " | " & ExperimentName, False)
            End If

            If RootText = Constants.CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES Then
                m_pObjectDemoExperiment.OnTreeNodeDblClick(ExperimentGroup, ExperimentName)
            End If
        End Sub

        Public Sub OnReloadExperiment(ByVal SessionID As Integer, ByVal RootText As String, ByVal ExperimentGroup As String, ByVal ExperimentName As String)
            If m_pAddin.m_lSessionID <> SessionID Then
                Return
            End If

            If RootText = Constants.CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES Then
                m_pObjectDemoExperiment.OnReloadExperiment(ExperimentGroup, ExperimentName)
            End If
        End Sub

        Public Sub OnStartSimulation(ByVal ExperimentName As String)
            If Not IsAddinExperimentSelected() Then
                Dim strMessage As String = String.Format("Failed to Start {0}. Please select Experimental Parameters before starting the Simulation Experiment..", ExperimentName)
                MessageBox.Show(strMessage)
                Return
            End If

            LoadOtherSimulationOptions()
            ResetAllStatusWindows()
            m_pObjectDemoExperiment.StartSimulation(m_strExperimentGroup, m_strExperimentName)
        End Sub

        Public Sub OnStopSimulation(ByVal experimentName As String)
            SetSimulationStatus(Constants.[FALSE])
        End Sub

        Public Sub OnStatusChange(ByVal statusCode As Integer, ByVal statusDesc As String, ByVal additionalParam As String)
            If statusCode = 10000 Then
            ElseIf statusCode = 10001 AndAlso m_pAddin.m_lSessionID = Convert.ToInt32(additionalParam) Then
                m_pObjectDemoExperiment.LoadAllExperiments()
                m_pObjectDemoExperiment.OnReloadExperiment(m_strExperimentGroup, m_strExperimentName)
                m_strSelectedExperiment = statusDesc
                LoadDefaultSelection()
            End If
        End Sub

        Public Sub OnError(ByVal errorCode As Integer, ByVal errorDesc As String, ByVal additionalParam As String)
        End Sub

        Public Sub OnInitializeLogFileInfo(ByVal experimentName As String)
            Dim experiment As Experiment = New Experiment()

            Try
                experiment.WriteCSVLogFileHeaderInfo("Angle,X,Y,Z" & vbLf)
            Catch __unusedException1__ As Exception
            End Try
        End Sub

        Public Sub OnInitializeSimulationGraph(ByVal ExperimentName As String)
            m_pObjectDemoExperiment.InitializeSimulationGraph(ExperimentName)
        End Sub

        Public Sub OnInitializeSimulationVideoRecording(ByVal experimentName As String)
        End Sub

        Public Sub OnNewDocument(ByVal DocumentName As String)
        End Sub

        Public Sub OnDocumentOpened(ByVal DocumentPath As String)
            If LoadDataFromDocument() Then
                LoadDefaultSelection()
            Else
            End If
        End Sub

        Public Sub OnCloseDocument(ByVal documentPath As String)
        End Sub

        Public Sub OnBeforeSaveDocument(ByVal documentPath As String)
            SetDataToDocument()
        End Sub

        Public Sub OnDrawSimulation()
        End Sub

        Public Sub OnInitializeSimulation(ByVal b3DMode As Integer, ByVal VisualizationMode As Integer, ByVal Experiment As String)
            m_b3DMode = Convert.ToBoolean(b3DMode)
            m_lVisualizationMode = VisualizationMode
            LoadOtherSimulationOptions()
            OnReloadExperiment(m_pAddin.m_lSessionID, m_strRootText, m_strExperimentGroup, m_strExperimentName)
        End Sub

        Public Sub OnDrawPredefinedScene(ByVal Experiment As String)
        End Sub

        Public Sub OnOwnerDrawSimulation()
        End Sub

        Public Sub OnOwnerDrawCreate()
        End Sub

        Public Sub ViewWndProc(ByVal msgID As Integer, ByVal wParam As Object, ByVal lParam As Object)
        End Sub

        Public Sub OnActivateView(ByVal bActivate As Integer, ByVal currentViewFilePath As String, ByVal previousViewFilePath As String)
            If Convert.ToBoolean(bActivate) Then
                LoadDefaultSelection()
            End If
        End Sub

        Public Sub MianWndProc(ByVal msgID As Integer, ByVal wParam As Object, ByVal lParam As Object)
        End Sub

        Public Sub OnPropertyChanged(ByVal GroupName As String, ByVal PropertyName As String, ByVal PropertyValue As String)
            If GroupName = Constants.OBJECT_PROPERTIES_TITLE Then
                m_pObjectDemoExperiment.OnPropertyChanged(GroupName, PropertyName, PropertyValue)
            End If
        End Sub

        Public Sub OnApplicationClose()
        End Sub

        Public Sub OnApplicationLaunched()
        End Sub

        Public Sub OnBeforeAddinControlsLoad()
        End Sub

        Public Sub OnAfterAddinControlsLoad()
        End Sub

        Public Sub GetControlStatus(ByVal ctrlID As String, ByRef pStatus As Integer)
            pStatus = Constants.[TRUE]
        End Sub

        Public Sub RibbonWndProc(ByVal msgID As Integer, ByVal wParam As Object, ByVal lParam As Object)
        End Sub

        Public Sub OnActivateSnapshot(ByVal filePath As String, ByVal groupName As String, ByVal snapshotName As String)
            LoadDataFromDocument()
        End Sub

        Public Sub OnAddSnapshot(ByVal filePath As String, ByVal groupName As String, ByVal snapshotName As String)
        End Sub

        Public Sub OnDeleteSnapshot(ByVal filePath As String, ByVal groupName As String, ByVal snapshotName As String)
        End Sub

        Public Sub OnDeleteAllSnapshot(ByVal filePath As String)
        End Sub

        Private Sub LoadDefaultSelection()
            Dim SelectedExperiment As String = String.Empty
            Dim SessionID As Integer = -1
            Dim experiment As Experiment = New Experiment()
            experiment.GetSelectedExperiment(SelectedExperiment, SessionID)

            If m_pAddin.m_lSessionID = SessionID Then
                LoadExperiments()

                If Not String.IsNullOrEmpty(m_strExperimentName) Then
                    OnTreeNodeDblClick(SessionID, m_strRootText, m_strExperimentGroup, m_strExperimentName)
                    Dim experimentTreeView As ExperimentTreeView = New ExperimentTreeView()
                    experimentTreeView.SetTreeGroupState(m_strExperimentGroup, Constants.TVE_EXPAND)
                    experimentTreeView.SelectActiveExperiment(SessionID, m_strExperimentGroup, m_strExperimentName)
                    OnReloadExperiment(SessionID, m_strRootText, m_strExperimentGroup, m_strExperimentName)
                    experimentTreeView = Nothing
                End If
            End If

            experiment = Nothing
        End Sub

        Private Sub LoadExperiments()
            If Not IsAddinExperimentSelected() Then
                Return
            End If

            m_pObjectDemoExperiment.LoadAllExperiments()
        End Sub

        Private Function IsAddinExperimentSelected() As Boolean
            Try
                m_strSelectedExperiment = String.Empty
                Dim SelectedExperiment As String = String.Empty
                Dim SessionID As Integer = -1
                Dim experiment As Experiment = New Experiment()
                experiment.GetSelectedExperiment(SelectedExperiment, SessionID)

                If m_pAddin.m_lSessionID <> SessionID Then
                    Dim strMessage As String
                    strMessage = String.Format("Error in Loading Experiment. Selected Experiment Name {0} | Addin SessionID {1} | Current SessionID {2} ", SelectedExperiment, m_pAddin.m_lSessionID, SessionID)
                    MessageBox.Show(strMessage)
                    Return False
                End If

                m_strSelectedExperiment = SelectedExperiment
                experiment = Nothing
                Return True
            Catch Ex As Exception
                Dim LastError As String = Ex.ToString()
                Dim strMessage As String = String.Format("Failed to Start Experiment Interface | GetLastError Returns {0}", LastError)
                MessageBox.Show(strMessage)
                Return False
            End Try
        End Function

        Public Sub SetPluginName(ByVal strPluginName As String)
            m_strPluginName = strPluginName
        End Sub

        Private Sub ResetAllStatusWindows()
            Dim mainWindow As MainWindow = New MainWindow()

            Try
                mainWindow.ResetAllStatusWindows()
            Catch __unusedException1__ As Exception
            Finally
                mainWindow = Nothing
            End Try
        End Sub

        Private Sub LoadOtherSimulationOptions()
            Dim applicationDocument As ApplicationDocument = New ApplicationDocument()

            Try
                m_bLogSimulationResultsToCSVFile = Convert.ToBoolean(applicationDocument.LogToCSVFileStatus)
                m_bDisplayRealTimeGraph = Convert.ToBoolean(applicationDocument.DisplayRealTimeGraphStatus)
                m_bRecordSimulationAsVideo = Convert.ToBoolean(applicationDocument.RecordSimulationAsVideoStatus)
                m_bShowExperimentalParamaters = Convert.ToBoolean(applicationDocument.DisplayExpParamStatus)
            Catch __unusedException1__ As Exception
            Finally
                applicationDocument = Nothing
            End Try
        End Sub

        Private Function LoadDataFromDocument() As Boolean
            Dim applicationDocument As ApplicationDocument = New ApplicationDocument()

            Try
                Dim strEncodedData As String = String.Empty
                applicationDocument.GetAddinSettingsAsString(m_strPluginName, Constants.CS_SAMPLE_DOC_SETTINGS_KEY, strEncodedData)

                If String.IsNullOrEmpty(strEncodedData) Then
                    Return False
                End If

                If Not DeSerialize(strEncodedData) Then
                    Return False
                End If

                Return True
            Catch __unusedException1__ As Exception
                Return False
            Finally
                applicationDocument = Nothing
            End Try
        End Function

        Public Sub LogSimulationPoint(ByVal strLogData As String)
            Try
                Dim ObjExperiment As Experiment = New Experiment()
                ObjExperiment.WriteToCSVLogFile(strLogData)
            Catch __unusedException1__ As Exception
            End Try
        End Sub

        Public Sub ResetPropertyGrid()
            Dim objPropertyWindow As PropertyWindow = New PropertyWindow()
            Dim strGroupName As String = String.Empty
            objPropertyWindow.RemoveAll()
            objPropertyWindow.EnableHeaderCtrl(Constants.[FALSE])
            objPropertyWindow.EnableDescriptionArea(Constants.[TRUE])
            objPropertyWindow.SetVSDotNetLook(Constants.[TRUE])
            objPropertyWindow.MarkModifiedProperties(Constants.[TRUE], Constants.[TRUE])
            objPropertyWindow = Nothing
        End Sub

        Public Sub AddOperationStatus(ByVal strStatus As String, ByVal Optional bPostMessage As Integer = Constants.[TRUE])
            m_strCurrentOutputStatusMessage = strStatus

            Try
                Dim ObjMainWindow As MainWindow = New MainWindow()
                ObjMainWindow.AddOperationStatus(strStatus, bPostMessage)
            Catch __unusedException1__ As Exception
            End Try
        End Sub

        Public Function Serialize() As String
            Dim info As ExperimentInfo = m_pObjectDemoExperiment.Serialize()

            Try
                info.RootText = m_strRootText
                info.ExperimentName = m_strExperimentName
                info.ExperimentGroup = m_strExperimentGroup
                Dim xmlSerializer As XmlSerializer = New XmlSerializer(GetType(ExperimentInfo))
                Dim textWriter As System.IO.StringWriter = New System.IO.StringWriter()
                xmlSerializer.Serialize(textWriter, info)
                Return textWriter.ToString()
            Catch __unusedException1__ As Exception
                Return String.Empty
            End Try
        End Function

        Public Function SetDataToDocument() As Boolean
            Dim applicationDocument As ApplicationDocument = New ApplicationDocument()

            Try
                Dim strEncodedData As String = Serialize()

                If String.IsNullOrEmpty(strEncodedData) Then
                    Return False
                End If

                applicationDocument.SetAddinSettingsAsString(m_strPluginName, Constants.CS_SAMPLE_DOC_SETTINGS_KEY, strEncodedData)
                Return True
            Catch __unusedException1__ As Exception
                Return False
            Finally
                applicationDocument = Nothing
            End Try
        End Function

        Public Function DeSerialize(ByVal strXML As String) As Boolean
            Dim info As ExperimentInfo = New ExperimentInfo()

            Try
                info.RootText = m_strRootText
                info.ExperimentName = m_strExperimentName
                info.ExperimentGroup = m_strExperimentGroup
                Dim xmlSerializer As XmlSerializer = New XmlSerializer(GetType(ExperimentInfo))
                Dim textReader As System.IO.StringReader = New System.IO.StringReader(strXML)
                info = CType(xmlSerializer.Deserialize(textReader), ExperimentInfo)
                m_strRootText = info.RootText
                m_strExperimentName = info.ExperimentName
                m_strExperimentGroup = info.ExperimentGroup
                m_pObjectDemoExperiment.DeSerialize(info)
                Return True
            Catch __unusedException1__ As Exception
                Return False
            End Try
        End Function

        Public Sub SetSimulationStatus(ByVal bActive As Integer)
            m_bSimulationActive = Convert.ToBoolean(bActive)
        End Sub

        Public Sub SetStatusBarMessage(ByVal strStatus As String, ByVal Optional bPostMessage As Boolean = True)
            m_strCurrentStatusBarMessage = strStatus
            Dim mWindow As MainWindow = New MainWindow()
            mWindow.SetStatusbarMessage(strStatus, Constants.BOOL(bPostMessage))
            mWindow = Nothing
        End Sub
    End Class
End Namespace
