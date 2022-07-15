Imports EurekaSim.Net
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace MyVbDotNetAddin
    Public Class ExperimentInfo
        Public Property RootText As String
        Public Property ExperimentGroup As String
        Public Property ExperimentName As String
        Public Property ExperimentType As String
        Public Property ObjectType As String
        Public Property Colour As Integer
        Public Property SimulationPattern As String
        Public Property SimulationInterval As Long
    End Class

    Module Constants
        Public Const TVE_EXPAND As Integer = &H2
        Public Const [FALSE] As Integer = 0
        Public Const [TRUE] As Integer = 1
        Public Const GL_POINTS As UInteger = &H0
        Public Const GL_LINES As UInteger = &H1
        Public Const GL_LINE_LOOP As UInteger = &H2
        Public Const GL_LINE_STRIP As UInteger = &H3
        Public Const GL_TRIANGLES As UInteger = &H4
        Public Const GL_TRIANGLE_STRIP As UInteger = &H5
        Public Const GL_TRIANGLE_FAN As UInteger = &H6
        Public Const GL_QUADS As UInteger = &H7
        Public Const GL_QUAD_STRIP As UInteger = &H8
        Public Const GL_POLYGON As UInteger = &H9
        Public Const OBJECT_TREE_ROOT_TITLE As String = "Object Demo"
        Public Const OBJECT_TREE_LEAF_PATTERN_TITLE As String = "Object Pattern Demo"
        Public Const OBJECT_3D_TREE_ROOT_TITLE As String = "3D Object Demo"
        Public Const OBJECT_3D_TREE_LEAF_PATTERN_TITLE As String = "Object Pattern Demo"
        Public Const MECHANICS_TREE_ROOT_TITLE As String = "Physics"
        Public Const MECHANICS_TREE_SIMPLE_PENDULUM_TITLE As String = "Simple Pendulum"
        Public Const MECHANICS_TREE_PROJECTILE_MOTION_TITLE As String = "Projectile Motion"
        Public Const MECHANICS_TREE_PLANETORY_MOTION_TITLE As String = "Planetory Motion"
        Public Const OBJECT_PROPERTIES_TITLE As String = "Select Object | Properties"
        Public Const OBJECT_TYPE_TITLE As String = "Select The Object Type"
        Public Const OBJECT_COLOR_TITLE As String = "Select Background Color"
        Public Const OBJECT_SIMULATION_PATTERN_TITLE As String = "Simulation Pattern"
        Public Const OBJECT_SIMULATION_INTERVAL_TITLE As String = "Simulation Interval"
        Public Const PENDULUM_PROPERTIES_TITLE As String = "Select Pendulum | Properties"
        Public Const PENDULUM_LENGTH As String = "Pendulum Length"
        Public Const PENDULUM_BOB_RADIUS As String = "Bob Radius"
        Public Const PENDULUM_MAX_SWING_ANGLE As String = "Max Swing Angle"
        Public Const PENDULUM_BOB_MASS As String = "Mass of Bob"
        Public Const PENDULUM_AMPITUDE As String = "Amplitude"
        Public Const PENDULUM_COLOR_TITLE As String = "Select Background Color"
        Public Const OBJECT_TYPES As String = "Cube,Ball,Pyramid,Aeroplane,Clock"
        Public Const OBJECT_TYPE_CUBE As String = "Cube"
        Public Const OBJECT_TYPE_BALL As String = "Ball"
        Public Const OBJECT_TYPE_PYRAMID As String = "Pyramid"
        Public Const OBJECT_TYPE_AEROPLANE As String = "Aeroplane"
        Public Const OBJECT_TYPE_CLOCK As String = "Clock"
        Public Const OBJECT_PATTERN_TYPES As String = "Rotate,Random Movement"
        Public Const OBJECT_PATTERN_TYPE_ROTATE As String = "Rotate"
        Public Const OBJECT_PATTERN_TYPE_RANDOM As String = "Random Movement"
        Public Const CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES As String = "Experiment Group 1 Properties"
        Public Const CS_SAMPLE_DOC_SETTINGS_KEY As String = "Cs.Sample.Addin.Settings"
        Public Const VB_SAMPLE_MAIN_EXPERIMENT_NAME As String = "MyVbDotNetAddin Experiment Simulation Demo"
        Public Const CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1 As String = "Experiment Group 1"

        Function BOOL(ByVal bValue As Boolean) As Integer
            Dim res As Integer = If(bValue, 1, 0)
            Return res
        End Function

        Function HexConverter(ByVal color As Color) As ULong
            Dim strCOLORREF As String = "0x00" & color.B.ToString("X2") + color.G.ToString("X2") + color.R.ToString("X2")
            Dim iCOLORREF As UInteger

            Try
                iCOLORREF = Convert.ToUInt32(strCOLORREF, 16)
            Catch __unusedException1__ As Exception
                iCOLORREF = Convert.ToUInt32(&HFF0000)
            End Try

            Return iCOLORREF
        End Function
    End Module

    Public Class PhysicsExperiments
        Public m_Color As Color = New Color()
        Public m_lSimulationInterval As Long
        Public m_Length As Double
        Public m_BobRadius As Double
        Public m_MaxAngle As Integer

        Public Sub New()
            m_Color = Color.FromArgb(0, 0, 255)
            m_lSimulationInterval = 100
            m_Length = 1.0
            m_BobRadius = 0.1
            m_MaxAngle = 45
        End Sub

        Public Sub OnPropertyChanged(ByVal GroupName As String, ByVal PropertyName As String, ByVal PropertyValue As String)
            If GroupName <> Constants.PENDULUM_PROPERTIES_TITLE Then
                Return
            ElseIf PropertyName = Constants.PENDULUM_LENGTH Then
                m_Length = Convert.ToDouble(PropertyValue)
            ElseIf PropertyName = Constants.PENDULUM_BOB_RADIUS Then
                m_BobRadius = Convert.ToDouble(PropertyValue)
            ElseIf PropertyName = Constants.PENDULUM_COLOR_TITLE Then
                m_Color = Color.FromArgb(Convert.ToInt32(PropertyValue))
            ElseIf PropertyName = Constants.OBJECT_SIMULATION_INTERVAL_TITLE Then
                m_lSimulationInterval = Convert.ToInt64(PropertyValue)
            ElseIf PropertyName = Constants.PENDULUM_MAX_SWING_ANGLE Then
                m_MaxAngle = Convert.ToInt16(PropertyValue)
            Else
                Return
            End If
        End Sub
    End Class

    Public Class CObjectPattern
        Public m_strObjectType As String
        Public m_Color As Color = New Color()
        Public m_strSimulationPattern As String
        Public m_lSimulationInterval As Long

        Public Sub New()
            m_strObjectType = "Cube"
            m_Color = Color.FromArgb(0, 0, 255)
            m_strSimulationPattern = "Rotate"
            m_lSimulationInterval = 100
        End Sub

        Public Function Serialize() As ExperimentInfo
            Dim info As ExperimentInfo = New ExperimentInfo()
            info.ObjectType = m_strObjectType
            info.Colour = m_Color.ToArgb()
            info.SimulationPattern = m_strSimulationPattern
            info.SimulationInterval = m_lSimulationInterval
            Return info
        End Function

        Public Sub DeSerialize(ByVal info As ExperimentInfo)
            m_strObjectType = info.ObjectType
            m_Color = Color.FromArgb(info.Colour)
            m_strSimulationPattern = info.SimulationPattern
            m_lSimulationInterval = info.SimulationInterval
        End Sub

        Public Sub OnPropertyChanged(ByVal GroupName As String, ByVal PropertyName As String, ByVal PropertyValue As String)
            If GroupName <> Constants.OBJECT_PROPERTIES_TITLE Then
                Return
            End If

            If PropertyName = Constants.OBJECT_TYPE_TITLE Then
                m_strObjectType = PropertyValue
            ElseIf PropertyName = Constants.OBJECT_COLOR_TITLE Then
                m_Color = Color.FromArgb(Convert.ToInt32(PropertyValue))
            ElseIf PropertyName = Constants.OBJECT_SIMULATION_PATTERN_TITLE Then
                m_strSimulationPattern = PropertyValue
            ElseIf PropertyName = Constants.OBJECT_SIMULATION_INTERVAL_TITLE Then
                m_lSimulationInterval = Convert.ToInt32(PropertyValue)
            End If
        End Sub
    End Class

    Public Class CGraphPoints
        Public m_Angle As Single
        Public m_x As Single
        Public m_y As Single
        Public m_z As Single

        Public Sub New()
            m_Angle = 0.0F
            m_x = 0.0F
            m_y = 0.0F
            m_z = 0.0F
        End Sub
    End Class

    Public Class ObjectDemoExperiment
        Private m_pManager As AddinSimulationManager
        Private m_PlotInfoArray As List(Of CGraphPoints) = New List(Of CGraphPoints)()
        Public m_ObjectPattern As CObjectPattern = New CObjectPattern()
        Public m_objPhysicExp As PhysicsExperiments = New PhysicsExperiments()
        Private m_PendulumAngle As Single = 0.0F

        Public Sub New(ByVal pManager As AddinSimulationManager)
            m_pManager = pManager
        End Sub

        Public Sub LoadAllExperiments()
            Dim SessionID As Integer = CInt(m_pManager.m_pAddin.m_lSessionID)
            Dim objExperimentTreeView As ExperimentTreeView = New ExperimentTreeView()

            Try
                objExperimentTreeView.DeleteAllExperiments(SessionID)
                objExperimentTreeView.SetRootNodeName(Constants.CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES, 1)
                objExperimentTreeView.AddExperiment(SessionID, Constants.MECHANICS_TREE_ROOT_TITLE, Constants.MECHANICS_TREE_SIMPLE_PENDULUM_TITLE)
                objExperimentTreeView.AddExperiment(SessionID, Constants.OBJECT_TREE_ROOT_TITLE, Constants.OBJECT_TREE_LEAF_PATTERN_TITLE)
                objExperimentTreeView.Refresh()
            Catch Ex As Exception
                MessageBox.Show(Ex.ToString())
            End Try
        End Sub

        Public Sub OnTreeNodeSelect(ByVal ExperimentGroup As String, ByVal ExperimentName As String)
            OnReloadExperiment(ExperimentGroup, ExperimentName)
        End Sub

        Public Sub OnTreeNodeDblClick(ByVal ExperimentGroup As String, ByVal ExperimentName As String)
            If ExperimentGroup = Constants.OBJECT_TREE_ROOT_TITLE AndAlso ExperimentName = Constants.OBJECT_TREE_LEAF_PATTERN_TITLE Then
                ShowObjectProperties()
            ElseIf ExperimentGroup = Constants.MECHANICS_TREE_ROOT_TITLE AndAlso ExperimentName = Constants.MECHANICS_TREE_SIMPLE_PENDULUM_TITLE Then
                ShowPhysicsExpProperties()
            Else
                m_pManager.ResetPropertyGrid()
            End If
        End Sub

        Public Sub OnReloadExperiment(ByVal ExperimentGroup As String, ByVal ExperimentName As String)
            If ExperimentGroup = Constants.OBJECT_TREE_ROOT_TITLE Then
                DrawObject(ExperimentName)
            ElseIf ExperimentGroup = Constants.MECHANICS_TREE_ROOT_TITLE Then
                DrawExpSetup(ExperimentName)
            Else
            End If
        End Sub

        Private Sub DrawExpSetup(ByVal ExperimentName As String)
            If ExperimentName = Constants.MECHANICS_TREE_SIMPLE_PENDULUM_TITLE Then
                DrawSimplePendulum()
            ElseIf ExperimentName = Constants.MECHANICS_TREE_PROJECTILE_MOTION_TITLE Then
            ElseIf ExperimentName = Constants.MECHANICS_TREE_PLANETORY_MOTION_TITLE Then
            Else
            End If
        End Sub

        Private Sub DrawSimplePendulum()
            Dim applicationView As ApplicationView = New ApplicationView()
            applicationView.InitializeEnvironment(1)
            applicationView.BeginGraphicsCommands()
            Dim pendulumLen As Single = CSng(m_objPhysicExp.m_Length)
            Dim SECTIONS As Integer = 7
            Dim RADIUS As Double = m_objPhysicExp.m_BobRadius
            applicationView.SetBkgColor(Convert.ToSingle(m_objPhysicExp.m_Color.R / 255), Convert.ToSingle(m_objPhysicExp.m_Color.G / 255), Convert.ToSingle(m_objPhysicExp.m_Color.B / 255), 1)
            applicationView.StartNewDisplayList()
            applicationView.SetColorf(1.0F, 1.0F, 1.0F)
            Dim openGLView As OpenGLView = New OpenGLView()
            openGLView.glColor3f(1.0F, 1.0F, 1.0F)
            openGLView.glBegin(Constants.GL_LINES)
            openGLView.glVertex2f(0, pendulumLen)
            openGLView.glVertex2f(0, 0)
            openGLView.glEnd()

            If m_pManager.m_b3DMode Then
                applicationView.DrawSphere(RADIUS, SECTIONS, SECTIONS)
            Else
                openGLView.glBegin(Constants.GL_LINE_LOOP)
                Dim segments As Integer = 100

                For i As Integer = 0 To segments - 1
                    Dim theta As Double = 2.0 * Math.PI * i / segments
                    Dim x As Single = CSng((RADIUS * Math.Cos(theta)))
                    Dim y As Single = CSng((RADIUS * Math.Sin(theta)))
                    openGLView.glVertex2f(x + 0, y + 0)
                Next

                openGLView.glEnd()
            End If

            applicationView.EndNewDisplayList()
            applicationView.EndGraphicsCommands()
            applicationView.Refresh()
        End Sub

        Private Sub ShowPhysicsExpProperties()
            Dim objPropertyWindow As PropertyWindow = New PropertyWindow()
            Dim strGroupName As String = ""

            Try
                objPropertyWindow.RemoveAll()
                strGroupName = Constants.PENDULUM_PROPERTIES_TITLE
                objPropertyWindow.AddPropertyGroup(strGroupName)
                Dim strInterval As String = m_objPhysicExp.m_lSimulationInterval.ToString()
                objPropertyWindow.AddPropertyItemAsString(strGroupName, Constants.PENDULUM_LENGTH, m_objPhysicExp.m_Length.ToString(), "Length in mm")
                objPropertyWindow.AddColorPropertyItem(strGroupName, Constants.PENDULUM_COLOR_TITLE, Constants.HexConverter(m_objPhysicExp.m_Color), "Select the Color")
                objPropertyWindow.AddPropertyItemAsString(strGroupName, Constants.OBJECT_SIMULATION_INTERVAL_TITLE, strInterval, "Simulation Interval In Milli Seconds")
                objPropertyWindow.AddPropertyItemAsString(strGroupName, Constants.PENDULUM_BOB_RADIUS, m_objPhysicExp.m_BobRadius.ToString(), "Length in mm")
                objPropertyWindow.AddPropertyItemAsString(strGroupName, Constants.PENDULUM_MAX_SWING_ANGLE, m_objPhysicExp.m_MaxAngle.ToString(), "Maximum angle(in degree) of Swinging from normal")
                objPropertyWindow.EnableHeaderCtrl(0)
                objPropertyWindow.EnableDescriptionArea(1)
                objPropertyWindow.SetVSDotNetLook(1)
                objPropertyWindow.MarkModifiedProperties(1, 1)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Sub ShowObjectProperties()
            Dim objPropertyWindow As PropertyWindow = New PropertyWindow()
            Dim strGroupName As String = String.Empty

            Try
                objPropertyWindow.RemoveAll()
                strGroupName = Constants.OBJECT_PROPERTIES_TITLE
                objPropertyWindow.AddPropertyGroup(strGroupName)
                objPropertyWindow.AddPropertyItemsAsString(strGroupName, Constants.OBJECT_TYPE_TITLE, Constants.OBJECT_TYPES, m_ObjectPattern.m_strObjectType, "Select the Object from the List", Constants.[FALSE])

                Try
                    objPropertyWindow.AddColorPropertyItem(strGroupName, Constants.OBJECT_COLOR_TITLE, Constants.HexConverter(m_ObjectPattern.m_Color), "Select the Color")
                Catch __unusedException1__ As Exception
                End Try

                objPropertyWindow.AddPropertyItemsAsString(strGroupName, Constants.OBJECT_SIMULATION_PATTERN_TITLE, Constants.OBJECT_PATTERN_TYPES, m_ObjectPattern.m_strSimulationPattern, "Select the Simulation Pattern", Constants.[FALSE])
                Dim strInterval As String = m_ObjectPattern.m_lSimulationInterval.ToString()
                objPropertyWindow.AddPropertyItemAsString(strGroupName, Constants.OBJECT_SIMULATION_INTERVAL_TITLE, strInterval, "Simulation Interval In Milli Seconds")
                objPropertyWindow.EnableHeaderCtrl(Constants.[FALSE])
                objPropertyWindow.EnableDescriptionArea(Constants.[TRUE])
                objPropertyWindow.SetVSDotNetLook(Constants.[TRUE])
                objPropertyWindow.MarkModifiedProperties(Constants.[TRUE], Constants.[TRUE])
            Catch __unusedException1__ As Exception
            End Try
        End Sub

        Public Function Serialize() As ExperimentInfo
            Return m_ObjectPattern.Serialize()
        End Function

        Public Sub DeSerialize(ByVal info As ExperimentInfo)
            m_ObjectPattern.DeSerialize(info)
        End Sub

        Public Sub OnPropertyChanged(ByVal GroupName As String, ByVal PropertyName As String, ByVal PropertyValue As String)
            If GroupName = Constants.OBJECT_PROPERTIES_TITLE Then
                m_ObjectPattern.OnPropertyChanged(GroupName, PropertyName, PropertyValue)
            ElseIf GroupName = Constants.PENDULUM_PROPERTIES_TITLE Then
                m_objPhysicExp.OnPropertyChanged(GroupName, PropertyName, PropertyValue)
            End If

            DrawScene()
        End Sub

        Public Sub DrawScene()
            OnReloadExperiment(m_pManager.m_strExperimentGroup, m_pManager.m_strExperimentName)
        End Sub

        Public Sub DrawObject(ByVal ExperimentName As String)
            If m_ObjectPattern.m_strObjectType = Constants.OBJECT_TYPE_CUBE Then
                DrawCube()
            ElseIf m_ObjectPattern.m_strObjectType = Constants.OBJECT_TYPE_BALL Then
                DrawBall()
            ElseIf m_ObjectPattern.m_strObjectType = Constants.OBJECT_TYPE_PYRAMID Then
                DrawPyramid()
            ElseIf m_ObjectPattern.m_strObjectType = Constants.OBJECT_TYPE_AEROPLANE Then
                DrawAeroplane()
            End If
        End Sub

        Public Sub DrawCube()
            Dim applicationView As ApplicationView = New ApplicationView()
            Const radius As Single = 0.34F
            applicationView.InitializeEnvironment(1)
            applicationView.BeginGraphicsCommands()
            applicationView.SetBkgColor(m_ObjectPattern.m_Color.R / CSng(255.0), m_ObjectPattern.m_Color.G / CSng(255.0), m_ObjectPattern.m_Color.B / CSng(255.0), CSng(1.0))

            Try
                applicationView.StartNewDisplayList()
            Catch __unusedException1__ As Exception
                Return
            End Try

            Dim openGLView As OpenGLView = New OpenGLView()
            openGLView.glBegin(Constants.GL_QUAD_STRIP)
            openGLView.glColor3f(1.0F, 0.0F, 1.0F)
            openGLView.glVertex3f(-0.3F, 0.3F, 0.3F)
            openGLView.glColor3f(1.0F, 0.0F, 0.0F)
            openGLView.glVertex3f(-0.3F, -0.3F, 0.3F)
            openGLView.glColor3f(1.0F, 1.0F, 1.0F)
            openGLView.glVertex3f(0.3F, 0.3F, 0.3F)
            openGLView.glColor3f(1.0F, 1.0F, 0.0F)
            openGLView.glVertex3f(0.3F, -0.3F, 0.3F)
            openGLView.glColor3f(0.0F, 1.0F, 1.0F)
            openGLView.glVertex3f(0.3F, 0.3F, -0.3F)
            openGLView.glColor3f(0.0F, 1.0F, 0.0F)
            openGLView.glVertex3f(0.3F, -0.3F, -0.3F)
            openGLView.glColor3f(0.0F, 0.0F, 1.0F)
            openGLView.glVertex3f(-0.3F, 0.3F, -0.3F)
            openGLView.glColor3f(0.0F, 0.0F, 0.0F)
            openGLView.glVertex3f(-0.3F, -0.3F, -0.3F)
            openGLView.glColor3f(1.0F, 0.0F, 1.0F)
            openGLView.glVertex3f(-0.3F, 0.3F, 0.3F)
            openGLView.glColor3f(1.0F, 0.0F, 0.0F)
            openGLView.glVertex3f(-0.3F, -0.3F, 0.3F)
            openGLView.glEnd()
            openGLView.glBegin(Constants.GL_QUADS)
            openGLView.glColor3f(1.0F, 0.0F, 1.0F)
            openGLView.glVertex3f(-0.3F, 0.3F, 0.3F)
            openGLView.glColor3f(1.0F, 1.0F, 1.0F)
            openGLView.glVertex3f(0.3F, 0.3F, 0.3F)
            openGLView.glColor3f(0.0F, 1.0F, 1.0F)
            openGLView.glVertex3f(0.3F, 0.3F, -0.3F)
            openGLView.glColor3f(0.0F, 0.0F, 1.0F)
            openGLView.glVertex3f(-0.3F, 0.3F, -0.3F)
            openGLView.glColor3f(1.0F, 0.0F, 0.0F)
            openGLView.glVertex3f(-0.3F, -0.3F, 0.3F)
            openGLView.glColor3f(0.0F, 0.0F, 0.0F)
            openGLView.glVertex3f(-0.3F, -0.3F, -0.3F)
            openGLView.glColor3f(0.0F, 1.0F, 0.0F)
            openGLView.glVertex3f(0.3F, -0.3F, -0.3F)
            openGLView.glColor3f(1.0F, 1.0F, 0.0F)
            openGLView.glVertex3f(0.3F, -0.3F, 0.3F)
            openGLView.glEnd()
            openGLView.glColor3f(1.0F, 1.0F, 1.0F)
            openGLView.glRasterPos3f(-radius, radius, radius)
            openGLView.glRasterPos3f(-radius, -radius, radius)
            openGLView.glRasterPos3f(radius, radius, radius)
            openGLView.glRasterPos3f(radius, -radius, radius)
            openGLView.glRasterPos3f(radius, radius, -radius)
            openGLView.glRasterPos3f(radius, -radius, -radius)
            openGLView.glRasterPos3f(-radius, radius, -radius)
            openGLView.glRasterPos3f(-radius, -radius, -radius)
            applicationView.EndNewDisplayList()
            applicationView.EndGraphicsCommands()
            applicationView.Refresh()
        End Sub

        Public Sub DrawBall()
            Dim applicationView As ApplicationView = New ApplicationView()
            applicationView.InitializeEnvironment(Constants.[TRUE])
            applicationView.BeginGraphicsCommands()
            applicationView.SetBkgColor(m_ObjectPattern.m_Color.R / CSng(255), m_ObjectPattern.m_Color.G / CSng(255), m_ObjectPattern.m_Color.B / CSng(255), 1)
            Dim SECTIONS As Integer = 25
            Dim RADIUS As Double = 1.0

            Try
                applicationView.StartNewDisplayList()
            Catch __unusedException1__ As Exception
                Return
            End Try

            applicationView.SetColorf(0.0F, 0.0F, 1.0F)
            applicationView.DrawSphere(RADIUS, SECTIONS, SECTIONS)
            applicationView.SetColorf(1.0F, 1.0F, 1.0F)
            applicationView.DrawSphere(RADIUS / 1.5, SECTIONS, SECTIONS)
            applicationView.EndNewDisplayList()
            applicationView.EndGraphicsCommands()
            applicationView.Refresh()
        End Sub

        Public Sub DrawPyramid()
            Dim applicationView As ApplicationView = New ApplicationView()
            applicationView.ResetScene()
            applicationView.InitializeEnvironment(Constants.[TRUE])
            applicationView.BeginGraphicsCommands()
            applicationView.SetBkgColor(m_ObjectPattern.m_Color.R / CSng(255), m_ObjectPattern.m_Color.G / CSng(255), m_ObjectPattern.m_Color.B / CSng(255), 1)

            Try
                applicationView.StartNewDisplayList()
            Catch __unusedException1__ As Exception
                Return
            End Try

            Dim openGLView As OpenGLView = New OpenGLView()
            openGLView.glTranslatef(0.01F, 0.0F, 0.01F)
            openGLView.glColor3f(0.0F, 0.4F, 0.8F)
            openGLView.glBegin(Constants.GL_TRIANGLES)
            openGLView.glColor3f(1.0F, 0.0F, 0.0F)
            openGLView.glVertex3f(0.0F, 1.0F, 0.0F)
            openGLView.glColor3f(0.0F, 1.0F, 0.0F)
            openGLView.glVertex3f(-1.0F, -1.0F, 1.0F)
            openGLView.glColor3f(0.0F, 0.0F, 1.0F)
            openGLView.glVertex3f(1.0F, -1.0F, 1.0F)
            openGLView.glColor3f(1.0F, 0.0F, 0.0F)
            openGLView.glVertex3f(0.0F, 1.0F, 0.0F)
            openGLView.glColor3f(0.0F, 1.0F, 0.0F)
            openGLView.glVertex3f(1.0F, -1.0F, 1.0F)
            openGLView.glColor3f(0.0F, 0.0F, 1.0F)
            openGLView.glVertex3f(1.0F, -1.0F, -1.0F)
            openGLView.glColor3f(1.0F, 0.0F, 0.0F)
            openGLView.glVertex3f(0.0F, 1.0F, 0.0F)
            openGLView.glColor3f(0.0F, 1.0F, 0.0F)
            openGLView.glVertex3f(1.0F, -1.0F, -1.0F)
            openGLView.glColor3f(0.0F, 0.0F, 1.0F)
            openGLView.glVertex3f(-1.0F, -1.0F, -1.0F)
            openGLView.glColor3f(1.0F, 0.0F, 0.0F)
            openGLView.glVertex3f(0.0F, 1.0F, 0.0F)
            openGLView.glColor3f(0.0F, 1.0F, 0.0F)
            openGLView.glVertex3f(-1.0F, -1.0F, -1.0F)
            openGLView.glColor3f(0.0F, 0.0F, 1.0F)
            openGLView.glVertex3f(-1.0F, -1.0F, 1.0F)
            openGLView.glColor3f(1.0F, 0.0F, 0.0F)
            openGLView.glVertex3f(-1.0F, -1.0F, 1.0F)
            openGLView.glColor3f(0.0F, 1.0F, 0.0F)
            openGLView.glVertex3f(1.0F, -1.0F, 1.0F)
            openGLView.glColor3f(0.0F, 0.0F, 1.0F)
            openGLView.glVertex3f(-1.0F, -1.0F, -1.0F)
            openGLView.glColor3f(1.0F, 0.0F, 0.0F)
            openGLView.glVertex3f(-1.0F, -1.0F, -1.0F)
            openGLView.glColor3f(0.0F, 1.0F, 0.0F)
            openGLView.glVertex3f(1.0F, -1.0F, -1.0F)
            openGLView.glColor3f(0.0F, 0.0F, 1.0F)
            openGLView.glVertex3f(1.0F, -1.0F, 1.0F)
            openGLView.glEnd()
            applicationView.EndNewDisplayList()
            applicationView.EndGraphicsCommands()
            applicationView.Refresh()
        End Sub

        Public Sub DrawAeroplane()
            Dim applicationView As ApplicationView = New ApplicationView()
            applicationView.InitializeEnvironment(Constants.[TRUE])
            applicationView.BeginGraphicsCommands()
            applicationView.SetBkgColor(m_ObjectPattern.m_Color.R / CSng(255), m_ObjectPattern.m_Color.G / CSng(255), m_ObjectPattern.m_Color.B / CSng(255), 1)

            Try
                applicationView.StartNewDisplayList()
            Catch __unusedException1__ As Exception
                Return
            End Try

            Dim openGLView As OpenGLView = New OpenGLView()
            openGLView.glTranslatef(0.01F, 0.0F, 0.01F)
            openGLView.glColor3f(0.0F, 0.4F, 0.8F)
            openGLView.glBegin(Constants.GL_TRIANGLES)
            openGLView.glVertex3f(0.0F, 0.0F, 0.001F)
            openGLView.glVertex3f(0.0F, -0.5F, 1.0F)
            openGLView.glVertex3f(0.0F, 1.0F, 0.001F)
            openGLView.glEnd()
            openGLView.glColor3f(0.0F, 0.3F, 0.7F)
            openGLView.glBegin(Constants.GL_TRIANGLE_STRIP)
            openGLView.glVertex3f(1.0F, -0.5F, 0.0F)
            openGLView.glVertex3f(0.0F, 0.0F, 0.2F)
            openGLView.glVertex3f(0.0F, 2.0F, 0.0F)
            openGLView.glVertex3f(-1.0F, -0.5F, 0.0F)
            openGLView.glEnd()
            applicationView.EndNewDisplayList()
            applicationView.EndGraphicsCommands()
            applicationView.Refresh()
        End Sub

        Public Sub StartSimulation(ByVal ExperimentGroup As String, ByVal ExperimentName As String)
            If ExperimentGroup = Constants.OBJECT_TREE_ROOT_TITLE AndAlso ExperimentName = Constants.OBJECT_TREE_LEAF_PATTERN_TITLE Then
                StartObjectSimulation()
            ElseIf ExperimentGroup = Constants.MECHANICS_TREE_ROOT_TITLE AndAlso ExperimentName = Constants.MECHANICS_TREE_SIMPLE_PENDULUM_TITLE Then
                StartPendulumSimulation()
            Else
            End If
        End Sub

        Private Sub StartPendulumSimulation()
            m_pManager.SetSimulationStatus(1)
            Dim applicationView As ApplicationView = New ApplicationView()
            Dim pendulumLen As Single = CSng(m_objPhysicExp.m_Length)
            Dim Angle As Single = 5.0F

            While m_pManager.m_bSimulationActive
                applicationView.BeginGraphicsCommands()
                applicationView.TranslateObject(0.0F, pendulumLen, 0.0F)
                applicationView.RotateObject(Angle, 0.0F, 0.0F, 1.0F)
                applicationView.TranslateObject(0.0F, -1 * pendulumLen, 0.0F)
                applicationView.EndGraphicsCommands()
                applicationView.Refresh()
                m_PendulumAngle = m_PendulumAngle + Angle

                If m_PendulumAngle >= m_objPhysicExp.m_MaxAngle Then
                    Angle = Angle * -1
                ElseIf m_PendulumAngle <= -1 * m_objPhysicExp.m_MaxAngle Then
                    Angle = Angle * -1
                End If

                Dim x As Single = CSng((pendulumLen * Math.Sin(m_PendulumAngle * Math.PI / 180)))
                Dim y As Single = CSng((pendulumLen - pendulumLen * Math.Cos(m_PendulumAngle * Math.PI / 180)))
                OnNextSimulationPoint(m_PendulumAngle, x, y, 0.0F)
                Thread.Sleep(CInt(m_objPhysicExp.m_lSimulationInterval))
            End While
        End Sub

        Public Sub StartObjectSimulation()
            m_pManager.SetSimulationStatus(Constants.[TRUE])
            Dim applicationView As ApplicationView = New ApplicationView()
            Dim Angle As Single = CSng(0.0), x As Single = CSng(0.0), y As Single = CSng(0.0), z As Single = CSng(0.0)
            Dim i As Integer = 0
            Dim rnd As Random = New Random()

            While m_pManager.m_bSimulationActive
                applicationView.BeginGraphicsCommands()

                If m_ObjectPattern.m_strSimulationPattern = Constants.OBJECT_PATTERN_TYPE_ROTATE Then
                    x = CSng(0.1)
                    y = CSng(1.0)
                    z = CSng(0.1)
                ElseIf m_ObjectPattern.m_strSimulationPattern = Constants.OBJECT_PATTERN_TYPE_RANDOM Then

                    Select Case i
                        Case 0
                            x = CSng(1.0)
                            y = CSng(0.1)
                            z = CSng(0.1)
                        Case 1
                            x = CSng(0.1)
                            y = CSng(1.0)
                            z = CSng(0.1)
                        Case 2
                            x = CSng(0.1)
                            y = CSng(0.1)
                            z = CSng(1.0)
                    End Select

                    i = rnd.[Next](0, 3)
                End If

                If Not m_pManager.m_b3DMode Then
                    x = 0
                    y = 0
                End If

                applicationView.RotateObject(Angle, x, y, z)
                applicationView.EndGraphicsCommands()
                applicationView.Refresh()
                OnNextSimulationPoint(Angle, x, y, z)
                Angle = Angle + 5

                If Angle > 360 Then
                    Angle = 0
                End If

                Thread.Sleep(CInt(m_ObjectPattern.m_lSimulationInterval))
            End While
        End Sub

        Public Sub OnNextSimulationPoint(ByVal Angle As Single, ByVal x As Single, ByVal y As Single, ByVal z As Single)
            Dim strStatus As String = String.Format("Simulation Points (Angle:{0},X:{1},Y:{2},Z:{3})" & vbLf, Angle, x, y, z)

            If m_pManager.m_bShowExperimentalParamaters Then
                m_pManager.AddOperationStatus(strStatus)
            End If

            If m_pManager.m_bLogSimulationResultsToCSVFile Then
                Dim strLog As String = String.Format("{0},{1},{2},{3}" & vbLf, Angle, x, y, z)
                m_pManager.LogSimulationPoint(strLog)
            End If

            If m_pManager.m_bDisplayRealTimeGraph Then
                PlotSimulationPoint(Angle, x, y, z)
            End If
        End Sub

        Public Sub PlotSimulationPoint(ByVal Angle As Single, ByVal x As Single, ByVal y As Single, ByVal z As Single)
            Dim pPoint As CGraphPoints = New CGraphPoints()
            pPoint.m_Angle = Angle
            pPoint.m_x = x
            pPoint.m_y = y
            pPoint.m_z = z
            m_PlotInfoArray.Add(pPoint)
            Dim strStatus As String = String.Format("Plot Data Points Count ={0}", m_PlotInfoArray.Count)
            m_pManager.SetStatusBarMessage(strStatus)
            DisplayObjectDemoGraph()
        End Sub

        Public Sub InitializeSimulationGraph(ByVal ExperimentName As String)
            m_PlotInfoArray.Clear()
            Dim applicationChart As ApplicationChart = New ApplicationChart()

            Try
                applicationChart.DeleteAllCharts()
                applicationChart.Initialize2dChart(3)
                applicationChart.Set2dGraphInfo(0, "Angle Vs X", "Angle(Degree)", "X", Constants.[TRUE])
                applicationChart.Set2dAxisRange(0, CInt(EAxisPos.BottomAxis), 0, 365)
                applicationChart.Set2dAxisRange(0, CInt(EAxisPos.LeftAxis), 0, 2)
                applicationChart.Set2dGraphInfo(1, "Angle Vs Y", "Angle(Degree)", "Y", Constants.[TRUE])
                applicationChart.Set2dAxisRange(1, CInt(EAxisPos.BottomAxis), 0, 365)
                applicationChart.Set2dAxisRange(1, CInt(EAxisPos.LeftAxis), 0, 2)
                applicationChart.Set2dGraphInfo(2, "Angle Vs Z", "Angle(Degree)", "Z", Constants.[TRUE])
                applicationChart.Set2dAxisRange(2, CInt(EAxisPos.BottomAxis), 0, 365)
                applicationChart.Set2dAxisRange(2, CInt(EAxisPos.LeftAxis), 0, 2)
                applicationChart.ResizeChartWindow()
            Catch __unusedException1__ As Exception
            End Try
        End Sub

        Public Sub DisplayObjectDemoGraph()
            Dim iArraySize As Integer = CInt(m_PlotInfoArray.Count)

            If iArraySize < 2 Then
                Return
            End If

            Dim arraySize As Integer() = {iArraySize, 2}
            Dim lowerBounds As Integer() = {1, 1}
            Dim saX As Array = Array.CreateInstance(GetType(Double), arraySize, lowerBounds)
            Dim saY As Array = Array.CreateInstance(GetType(Double), arraySize, lowerBounds)
            Dim saZ As Array = Array.CreateInstance(GetType(Double), arraySize, lowerBounds)
            Dim index As Integer() = {0, 0}
            Dim i As Integer = 0

            For Each pInfo As CGraphPoints In m_PlotInfoArray
                index(0) = i + 1
                index(1) = 1
                Dim pValue As Double = pInfo.m_Angle
                saX.SetValue(pValue, index(0), index(1))
                saY.SetValue(pValue, index(0), index(1))
                saZ.SetValue(pValue, index(0), index(1))
                index(1) = 2
                pValue = pInfo.m_x
                saX.SetValue(pValue, index(0), index(1))
                pValue = pInfo.m_y
                saY.SetValue(pValue, index(0), index(1))
                pValue = pInfo.m_z
                saZ.SetValue(pValue, index(0), index(1))
                i = i + 1
            Next

            If iArraySize Mod 5 = 0 Then
                Dim applicationChart As ApplicationChart = New ApplicationChart()

                Try
                    applicationChart.Set2dChartData(0, saX)
                    applicationChart.Set2dChartData(1, saY)
                    applicationChart.Set2dChartData(2, saZ)
                Catch
                End Try
            End If
        End Sub
    End Class
End Namespace
