import math
from dicttoxml import dicttoxml
import xmltodict
from Constants import *
import win32ui
from EurekaSimLib import *
import random
import time
import math
from enum import IntEnum
from win32com.client import VARIANT
import pythoncom

class EAxisPos(IntEnum):
    LeftAxis = 0
    BottomAxis = 1
    RightAxis = 2
    TopAxis = 3

class Colour:
    def __init__(self, red=0, green=0, blue=0):
        self.R = red
        self.G = green
        self.B = blue

    def setRGB(self, red=0, green=0, blue=0):
        self.R = red
        self.G = green
        self.B = blue

    def toRGB(self, iColor):
        hexColor = '%08x' % iColor
        self.R = int(hexColor[0:2], 16)
        self.G = int(hexColor[2:4], 16)
        self.B = int(hexColor[4:6], 16)

    def toInt(self):
        colourHex = '00%02x%02x%02x' % (self.B, self.G, self.R)
        icolour = int(colourHex, 16)
        return icolour
    
    @staticmethod
    def fromInt(iColor):
        hexColor = '%08x' % iColor
        blue = int(hexColor[2:4], 16)
        green = int(hexColor[4:6], 16)
        red = int(hexColor[6:8], 16)
        return Colour(red, green, blue)


class ExperimentInfo():
    def __init__(self):
        self.RootText = ""
        self.ExperimentGroup = ""
        self.ExperimentName = ""
        self.ObjectType = ""
        self.Colour = 0
        self.SimulationPattern = ""
        self.SimulationInterval = 0

    def Serilize(self):
        dicForm = vars(self)
        xml = dicttoxml(dicForm, attr_type=False, custom_root='ExperimentInfo')
        return xml
    
    def Deserilize(self, strXML):
        doc = xmltodict.parse(strXML)
        infoDic = doc['ExperimentInfo']
        for k, v in infoDic.items():
            setattr(self, k, v)


class FootballPenaltyGameState:
    def __init__(self):
        self.ballX = 0.0
        self.ballY = 0.2
        self.ballZ = -1.6
        self.ballSpeedX = 0.0
        self.ballSpeedY = 0.0
        self.ballSpeedZ = 0.0
        self.kickerAngle = 0.0
        self.kickPower = 0.0
        self.goalkeeperX = 0.0
        self.goalkeeperZ = 4.0
        self.goalkeeperSpeed = 0.0
        self.goalkeeperTimer = 0.0
        self.isBallMoving = False
        self.isKicking = False
        self.score = 0
        self.attempts = 0
        self.gameTime = 0.0
        self.goalkeeperDiveLeft = False
        self.goalkeeperDiveRight = False
        self.isGoalScored = False
        self.isShotSaved = False
        self.isShotMissed = False
        self.isGameActive = True
        self.kickAnimationTime = 0.0

    def Reset(self):
        self.ballX = 0.0
        self.ballY = 0.2
        self.ballZ = -1.6
        self.ballSpeedX = 0.0
        self.ballSpeedY = 0.0
        self.ballSpeedZ = 0.0
        self.kickerAngle = 0.0
        self.kickPower = 0.0
        self.goalkeeperX = 0.0
        self.goalkeeperZ = 4.0
        self.goalkeeperSpeed = 0.0
        self.goalkeeperTimer = 0.0
        self.isBallMoving = False
        self.isKicking = False
        self.score = 0
        self.attempts = 0
        self.gameTime = 0.0
        self.goalkeeperDiveLeft = False
        self.goalkeeperDiveRight = False
        self.isGoalScored = False
        self.isShotSaved = False
        self.isShotMissed = False
        self.isGameActive = True
        self.kickAnimationTime = 0.0


class ElephantGameState:
    def __init__(self):
        self.positionX = 0.0
        self.positionY = 0.0
        self.positionZ = 0.0
        self.rotationY = 0.0
        self.animationTime = 0.0
        self.currentState = 0
        self.walkSpeed = 0.015
        self.runSpeed = 0.03
        self.scale = 1.2
        self.isTrumpeting = False
        self.trumpetProgress = 0.0
        self.headBob = 0.0
        self.trunkSwing = 0.0
        self.earFlap = 0.0

    def Reset(self):
        self.positionX = 0.0
        self.positionY = 0.0
        self.positionZ = 0.0
        self.rotationY = 0.0
        self.animationTime = 0.0
        self.currentState = 0
        self.walkSpeed = 0.015
        self.runSpeed = 0.03
        self.scale = 1.2
        self.isTrumpeting = False
        self.trumpetProgress = 0.0
        self.headBob = 0.0
        self.trunkSwing = 0.0
        self.earFlap = 0.0


class ObjectPattern:
    def __init__(self):
        self.m_strObjectType = "FootballPenalty"
        self.m_Color = Colour(0, 0, 255)
        self.m_strSimulationPattern = "Penalty Kick View"
        self.m_lSimulationInterval = 100

    def Serialize(self):
        info = ExperimentInfo()
        info.ObjectType = self.m_strObjectType
        info.Colour = self.m_Color.toInt()
        info.SimulationPattern = self.m_strSimulationPattern
        info.SimulationInterval = self.m_lSimulationInterval
        return info

    def DeSerialize(self, info):
        self.m_strObjectType = info.ObjectType
        self.m_Color = Colour.fromInt(int(info.Colour))
        self.m_strSimulationPattern = info.SimulationPattern
        self.m_lSimulationInterval = int(info.SimulationInterval, 10)

    def OnPropertyChanged(self, GroupName, PropertyName, PropertyValue):
        if GroupName != OBJECT_PROPERTIES_TITLE:
            return
        elif PropertyName == OBJECT_TYPE_TITLE:
            self.m_strObjectType = PropertyValue
        elif PropertyName == OBJECT_COLOR_TITLE:
            self.m_Color = Colour.fromInt(int(PropertyValue, 10))
        elif PropertyName == OBJECT_SIMULATION_PATTERN_TITLE:
            self.m_strSimulationPattern = PropertyValue
        elif PropertyName == OBJECT_SIMULATION_INTERVAL_TITLE:
            self.m_lSimulationInterval = int(PropertyValue, 10)


class GraphPoints():
    def __init__(self):
        self.m_Angle = 0.0
        self.m_x = 0.0
        self.m_y = 0.0
        self.m_z = 0.0


class ObjectDemoExperiment():
    def __init__(self, objManager):
        self.m_PlotInfoArray = []
        self.m_ObjectPattern = ObjectPattern()
        self.m_objManager = objManager
        self.m_FootballGameState = FootballPenaltyGameState()
        self.m_ElephantGameState = ElephantGameState()
        self.m_FootballGameState.Reset()
        self.m_ElephantGameState.Reset()
        self.m_ElephantGameState.scale = 0.8
        self.m_bPenaltyKickView = False
        self.m_bShowAimingReticle = True
        self.m_bShowPowerMeter = True
        self.m_bKeyboardActive = False

    def LoadAllExperiments(self):
        SessionID = self.m_objManager.m_objAddin.m_lSessionID
        objExperimentTreeView = ExperimentTreeView()
        try:
            objExperimentTreeView.DeleteAllExperiments(SessionID)
            objExperimentTreeView.SetRootNodeName(PY_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES, 1)
            objExperimentTreeView.AddExperiment(SessionID, OBJECT_3D_TREE_ROOT_TITLE, OBJECT_3D_TREE_LEAF_PATTERN_TITLE)
            objExperimentTreeView.Refresh()
        except Exception as ex:
            win32ui.MessageBox(str(ex))

    def OnTreeNodeSelect(self, ExperimentGroup, ExperimentName):
        try:
            self.OnReloadExperiment(ExperimentGroup, ExperimentName)
        except Exception as ex:
            win32ui.MessageBox(str(ex))

    def OnTreeNodeDblClick(self, ExperimentGroup, ExperimentName):
        try:
            if ExperimentGroup == OBJECT_3D_TREE_ROOT_TITLE and ExperimentName == OBJECT_3D_TREE_LEAF_PATTERN_TITLE:
                self.ShowObjectProperties()
            else:
                self.m_objManager.ResetPropertyGrid()
        except Exception as ex:
            win32ui.MessageBox(str(ex))

    def OnReloadExperiment(self, ExperimentGroup, ExperimentName):
        try:
            if ExperimentGroup == OBJECT_3D_TREE_ROOT_TITLE:
                self.DrawObject(ExperimentName)
        except Exception as ex:
            win32ui.MessageBox(str(ex))

    def ShowObjectProperties(self):
        objPropertyWindow = PropertyWindow()
        strGroupName = ""
        try:
            objPropertyWindow.RemoveAll()
            strGroupName = OBJECT_PROPERTIES_TITLE
            objPropertyWindow.AddPropertyGroup(strGroupName)
            objPropertyWindow.AddPropertyItemsAsString(strGroupName, OBJECT_TYPE_TITLE, OBJECT_TYPES, 
                                                       self.m_ObjectPattern.m_strObjectType, 
                                                       "Select the Object from the List", False)
            objPropertyWindow.AddColorPropertyItem(strGroupName, OBJECT_COLOR_TITLE,
                                                   self.m_ObjectPattern.m_Color.toInt(), "Select the Color")
            objPropertyWindow.AddPropertyItemsAsString(strGroupName, OBJECT_SIMULATION_PATTERN_TITLE,
                                                       OBJECT_PATTERN_TYPES, self.m_ObjectPattern.m_strSimulationPattern,
                                                       "Select the Simulation Pattern", False)
            strInterval = str(self.m_ObjectPattern.m_lSimulationInterval)
            objPropertyWindow.AddPropertyItemAsString(strGroupName, OBJECT_SIMULATION_INTERVAL_TITLE,
                                                      strInterval, "Simulation Interval In Milli Seconds")
            objPropertyWindow.EnableHeaderCtrl(False)
            objPropertyWindow.EnableDescriptionArea(True)
            objPropertyWindow.SetVSDotNetLook(True)
            objPropertyWindow.MarkModifiedProperties(True, True)
        except Exception as ex:
            win32ui.MessageBox(str(ex))
    
    def Serialize(self):
        try:
            return self.m_ObjectPattern.Serialize()
        except Exception as ex:
            win32ui.MessageBox(str(ex))
            return ExperimentInfo()

    def DeSerialize(self, info):
        try:
            return self.m_ObjectPattern.DeSerialize(info)
        except Exception as ex:
            win32ui.MessageBox(str(ex))

    def OnPropertyChanged(self, GroupName, PropertyName, PropertyValue):
        try:
            if GroupName == OBJECT_PROPERTIES_TITLE:
                self.m_ObjectPattern.OnPropertyChanged(GroupName, PropertyName, PropertyValue)
            self.DrawScene()
        except Exception as ex:
            win32ui.MessageBox(str(ex))

    def DrawScene(self):
        try:
            self.OnReloadExperiment(self.m_objManager.m_strExperimentGroup, self.m_objManager.m_strExperimentName)
        except Exception as ex:
            win32ui.MessageBox(str(ex))

    def DrawObject(self, ExperimentName):
        try:
            if self.m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_PENALTY_KICK:
                self.m_bPenaltyKickView = True
                self.DrawPenaltyKickView()
            else:
                self.m_bPenaltyKickView = False
                if self.m_ObjectPattern.m_strObjectType == OBJECT_TYPE_FOOTBALLPENALTY:
                    self.DrawFootballPenalty()
                elif self.m_ObjectPattern.m_strObjectType == OBJECT_TYPE_ELEPHANT:
                    self.DrawElephant()
                elif self.m_ObjectPattern.m_strObjectType == OBJECT_TYPE_CUBE:
                    self.DrawCube()
                elif self.m_ObjectPattern.m_strObjectType == OBJECT_TYPE_BALL:
                    self.DrawBall()
                elif self.m_ObjectPattern.m_strObjectType == OBJECT_TYPE_PYRAMID:
                    self.DrawPyramid()
                elif self.m_ObjectPattern.m_strObjectType == OBJECT_TYPE_AEROPLANE:
                    self.DrawAeroplane()
                elif self.m_ObjectPattern.m_strObjectType == OBJECT_TYPE_CLOCK:
                    self.DrawClock()
        except Exception as ex:
            win32ui.MessageBox(str(ex))

    def StartSimulation(self, ExperimentGroup, ExperimentName):
        if ExperimentGroup == OBJECT_3D_TREE_ROOT_TITLE and ExperimentName == OBJECT_3D_TREE_LEAF_PATTERN_TITLE:
            try:
                if self.m_ObjectPattern.m_strObjectType == OBJECT_TYPE_ELEPHANT:
                    self.m_ElephantGameState.positionX = 0.0
                    self.m_ElephantGameState.positionY = 0.0
                    self.m_ElephantGameState.positionZ = 0.0
                    self.m_ElephantGameState.currentState = 0
                    self.m_ElephantGameState.animationTime = 0.0
                self.StartObjectSimulation()
            except Exception as ex:
                win32ui.MessageBox(str(ex))

    def StartObjectSimulation(self):
        self.m_objManager.SetSimulationStatus(True)
        applicationView = ApplicationView()
        Angle = 0.0
        x = 0.0
        y = 0.0 
        z = 0.0
        i = 0
        while self.m_objManager.m_bSimulationActive:
            applicationView.BeginGraphicsCommands()
            
            if self.m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_ROTATE:
                x = 0.1
                y = 1.0
                z = 0.1
            elif self.m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_PENALTY_KICK:
                self.UpdateFootballPenalty()
                x = 0
                y = 0
                z = 0
            elif self.m_ObjectPattern.m_strSimulationPattern == OBJECT_PATTERN_TYPE_RANDOM:
                if i == 0:
                    x = 1.0
                    y = 0.1
                    z = 0.1
                elif i == 1:
                    x = 0.1
                    y = 1.0
                    z = 0.1
                elif i == 2:
                    x = 0.1
                    y = 0.1
                    z = 1.0
                i = random.randint(0, 2)
            
            if self.m_ObjectPattern.m_strObjectType != OBJECT_TYPE_ELEPHANT:
                if not self.m_objManager.m_b3DMode and self.m_ObjectPattern.m_strSimulationPattern != OBJECT_PATTERN_TYPE_PENALTY_KICK:
                    x = 0
                    y = 0
                if self.m_ObjectPattern.m_strSimulationPattern != OBJECT_PATTERN_TYPE_PENALTY_KICK:
                    applicationView.RotateObject(Angle, x, y, z)
            else:
                self.m_ElephantGameState.animationTime += 0.016
                self.m_ElephantGameState.trunkSwing = math.sin(self.m_ElephantGameState.animationTime * 2.0) * 0.5
                self.m_ElephantGameState.earFlap = math.sin(self.m_ElephantGameState.animationTime * 1.5) * 0.3
                self.m_ElephantGameState.headBob = math.sin(self.m_ElephantGameState.animationTime * 0.5) * 0.005
                self.DrawElephant()
            
            applicationView.EndGraphicsCommands()
            applicationView.Refresh()
            self.OnNextSimulationPoint(Angle, x, y, z)
            
            Angle = Angle + 5
            if Angle > 360:
                Angle = 0
            
            time.sleep(self.m_ObjectPattern.m_lSimulationInterval / 1000.0)

    def OnNextSimulationPoint(self, Angle, x, y, z):
        strStatus = "Simulation Points (Angle:{0},X:{1},Y:{2},Z:{3})\n".format(Angle, x, y, z)
                                            
        if self.m_objManager.m_bShowExperimentalParamaters:
            self.m_objManager.AddOperationStatus(strStatus)

        if self.m_objManager.m_bLogSimulationResultsToCSVFile:
            strLog = "Simulation Points (Angle:{0},X:{1},Y:{2},Z:{3})\n".format(Angle, x, y, z)
            self.m_objManager.LogSimulationPoint(strLog)

        if self.m_objManager.m_bDisplayRealTimeGraph:
            self.PlotSimulationPoint(Angle, x, y, z)
    
    def PlotSimulationPoint(self, Angle, x, y, z):
        Point = GraphPoints()
        Point.m_Angle = Angle
        Point.m_x = x
        Point.m_y = y
        Point.m_z = z
        self.m_PlotInfoArray.append(Point)
        strStatus = "Plot Data Points Count ={0}".format(len(self.m_PlotInfoArray))
        self.m_objManager.SetStatusBarMessage(strStatus)
        self.DisplayObjectDemoGraph()

    def InitializeSimulationGraph(self, ExperimentName):
        for point in self.m_PlotInfoArray:
            del point
        self.m_PlotInfoArray.clear()
        applicationChart = ApplicationChart()
        try:
            applicationChart.DeleteAllCharts()
            applicationChart.Initialize2dChart(3)
            applicationChart.Set2dGraphInfo(0, "Angle Vs X", "Angle(Degree)", "X", True)
            applicationChart.Set2dAxisRange(0, int(EAxisPos.BottomAxis), 0, 365)
            applicationChart.Set2dAxisRange(0, int(EAxisPos.LeftAxis), 0, 2)
            applicationChart.Set2dGraphInfo(1, "Angle Vs Y", "Angle(Degree)", "Y", True)
            applicationChart.Set2dAxisRange(1, int(EAxisPos.BottomAxis), 0, 365)
            applicationChart.Set2dAxisRange(1, int(EAxisPos.LeftAxis), 0, 2)
            applicationChart.Set2dGraphInfo(2, "Angle Vs Z", "Angle(Degree)", "Z", True)
            applicationChart.Set2dAxisRange(2, int(EAxisPos.BottomAxis), 0, 365)
            applicationChart.Set2dAxisRange(2, int(EAxisPos.LeftAxis), 0, 2)
            applicationChart.ResizeChartWindow()
        except:
            pass

    def DisplayObjectDemoGraph(self):
        try:
            iArraySize = len(self.m_PlotInfoArray)
            if iArraySize < 2 or iArraySize % 10 == 0:
                return
            sabX = [[1.0, 1.0, 1.0] for i in range(0, iArraySize)]
            sabY = [[1.0, 1.0, 1.0] for i in range(0, iArraySize)]
            sabZ = [[1.0, 1.0, 1.0] for i in range(0, iArraySize)]
            for i in range(iArraySize):
                try:
                    info = self.m_PlotInfoArray[i]
                    val = info.m_Angle
                    sabX[i][1] = val
                    sabY[i][1] = val
                    sabZ[i][1] = val
                    val = info.m_x
                    sabX[i][2] = val
                    val = info.m_y
                    sabY[i][2] = val
                    val = info.m_z
                    sabZ[i][2] = val
                except Exception as e:
                    win32ui.MessageBox(str(e))
            saX = VARIANT(pythoncom.VT_ARRAY | pythoncom.VT_R8, sabX)
            saY = VARIANT(pythoncom.VT_ARRAY | pythoncom.VT_R8, sabY)
            saZ = VARIANT(pythoncom.VT_ARRAY | pythoncom.VT_R8, sabZ)
            if iArraySize % 5 == 0:
                applicationChart = ApplicationChart()
                applicationChart.Set2dChartData(0, saX)
                applicationChart.Set2dChartData(1, saY)
                applicationChart.Set2dChartData(2, saZ)
        except Exception as e:
            win32ui.MessageBox(str(e))
         
    def DrawClock(self):
        applicationView = ApplicationView()
        applicationView.InitializeEnvironment(True)
        applicationView.BeginGraphicsCommands()
        applicationView.SetBkgColor(self.m_ObjectPattern.m_Color.R / 255.0,
                                    self.m_ObjectPattern.m_Color.G / 255.0,
                                    self.m_ObjectPattern.m_Color.B / 255.0, 1)
        applicationView.StartNewDisplayList()
        x1 = 0.0
        y1 = 0.0
        segments = 100
        radius = 1.0
        applicationView.SetLineWidth(4)
        applicationView.SetColorf(1, 0, 0)
        self.DrawCircle(segments, radius, x1, y1)
        applicationView.SetColorf(1, 1, 0)
        applicationView.SetLineWidth(2)
        applicationView.BeginDraw(int(GL_LINES))
        applicationView.Set2DVertexf(x1, y1)
        applicationView.Set2DVertexf(x1, ((radius / 3.0) * 2.0))
        applicationView.EndDraw()
        applicationView.SetColorf(1, 0, 0)
        applicationView.SetLineWidth(2)
        applicationView.BeginDraw(int(GL_LINES))
        applicationView.Set2DVertexf(x1, y1)
        applicationView.Set2DVertexf((radius / 3.0), (radius / 3.0))
        applicationView.EndDraw()
        applicationView.EndNewDisplayList()
        applicationView.EndGraphicsCommands()
        applicationView.Refresh()

    def DrawCircle(self, segments, radius, sx, sy):
        try:
            openGLView = OpenGLView()
            openGLView.glBegin(GL_LINE_LOOP)
            for i in range(0, segments):
                theta = (2.0 * 3.142 * i / segments)
                x = (radius * math.cos(theta))
                y = (radius * math.sin(theta))
                openGLView.glVertex2f(x + sx, y + sy)
            openGLView.glEnd()
        except Exception as e:
            win32ui.MessageBox(str(e))

    # ==================== FOOTBALL PENALTY GAME ====================
    
    def ResetFootballPenalty(self):
        self.m_FootballGameState.Reset()

    def DrawFootballPenalty(self):
        applicationView = ApplicationView()
        applicationView.InitializeEnvironment(True)
        applicationView.BeginGraphicsCommands()
        applicationView.SetBkgColor(0.2, 0.6, 0.8, 1.0)
        try:
            applicationView.StartNewDisplayList()
        except Exception:
            applicationView.EndGraphicsCommands()
            return
        self.UpdateFootballPenalty()
        self.DrawStadium()
        self.DrawFootballField()
        self.DrawGoalPost()
        self.DrawNet()
        self.DrawFootball()
        self.DrawKicker()
        self.DrawGoalkeeper()
        self.DrawResultMessage()
        applicationView.EndNewDisplayList()
        applicationView.EndGraphicsCommands()
        applicationView.Refresh()

    def UpdateFootballPenalty(self):
        self.m_FootballGameState.gameTime += 0.016
        self.HandleKeyboardInput()
        
        if self.m_FootballGameState.isBallMoving:
            self.m_FootballGameState.ballX += self.m_FootballGameState.ballSpeedX
            self.m_FootballGameState.ballY += self.m_FootballGameState.ballSpeedY
            self.m_FootballGameState.ballZ += self.m_FootballGameState.ballSpeedZ
            self.m_FootballGameState.ballSpeedY -= 0.098 * 0.5
            
            if self.m_FootballGameState.ballY <= 0.2 and self.m_FootballGameState.ballZ < 4.0:
                self.m_FootballGameState.ballY = 0.2
                self.m_FootballGameState.ballSpeedY *= -0.6
                self.m_FootballGameState.ballSpeedX *= 0.8
                self.m_FootballGameState.ballSpeedZ *= 0.8
                
                if (abs(self.m_FootballGameState.ballSpeedY) < 0.1 and
                    abs(self.m_FootballGameState.ballSpeedX) < 0.1 and
                    abs(self.m_FootballGameState.ballSpeedZ) < 0.1 and
                    self.m_FootballGameState.ballZ < 4.0):
                    self.m_FootballGameState.isBallMoving = False
                    self.m_FootballGameState.isShotMissed = True
                    self.ResetBall()
        
        self.CheckGoal()
        self.UpdateGoalkeeper()
        
        if self.m_FootballGameState.isKicking and (self.m_FootballGameState.gameTime - self.m_FootballGameState.kickAnimationTime) > 0.5:
            self.m_FootballGameState.isKicking = False

    def HandleKeyboardInput(self):
        try:
            import ctypes
            user32 = ctypes.windll.user32
            
            if not self.m_FootballGameState.isBallMoving:
                if user32.GetAsyncKeyState(ord('D')) & 0x8000:
                    self.m_FootballGameState.kickerAngle = -45.0
                    self.m_FootballGameState.kickPower = 1.0
                    self.ProcessKick()
                    time.sleep(0.002)
                elif user32.GetAsyncKeyState(ord('S')) & 0x8000:
                    self.m_FootballGameState.kickerAngle = 0.0
                    self.m_FootballGameState.kickPower = 1.0
                    self.ProcessKick()
                    time.sleep(0.002)
                elif user32.GetAsyncKeyState(ord('A')) & 0x8000:
                    self.m_FootballGameState.kickerAngle = 45.0
                    self.m_FootballGameState.kickPower = 1.0
                    self.ProcessKick()
                    time.sleep(0.002)
        except:
            pass

    def ProcessKick(self):
        angleRad = self.m_FootballGameState.kickerAngle * math.pi / 180.0
        powerMultiplier = 9.0
        
        if abs(self.m_FootballGameState.kickerAngle) > 30.0:
            cornerPower = powerMultiplier * 0.9
            cornerHeight = 1.2
            maxCornerX = 3.5
            targetX = math.sin(angleRad) * maxCornerX
            self.m_FootballGameState.ballSpeedX = targetX * 0.8
            self.m_FootballGameState.ballSpeedZ = math.cos(angleRad) * cornerPower
            self.m_FootballGameState.ballSpeedY = cornerHeight
        else:
            self.m_FootballGameState.ballSpeedX = math.sin(angleRad) * powerMultiplier * 0.4
            self.m_FootballGameState.ballSpeedZ = math.cos(angleRad) * powerMultiplier
            self.m_FootballGameState.ballSpeedY = 1.5
        
        maxBallSpeedX = 3.0
        if abs(self.m_FootballGameState.ballSpeedX) > maxBallSpeedX:
            self.m_FootballGameState.ballSpeedX = maxBallSpeedX if self.m_FootballGameState.ballSpeedX > 0 else -maxBallSpeedX
        
        self.m_FootballGameState.isBallMoving = True
        self.m_FootballGameState.attempts += 1
        self.m_FootballGameState.isKicking = True
        self.m_FootballGameState.kickAnimationTime = self.m_FootballGameState.gameTime
        self.ResetShotResult()
        self.DrawScene()

    def ResetShotResult(self):
        self.m_FootballGameState.isGoalScored = False
        self.m_FootballGameState.isShotSaved = False
        self.m_FootballGameState.isShotMissed = False
        self.m_FootballGameState.isKicking = False

    def UpdateGoalkeeper(self):
        self.m_FootballGameState.goalkeeperTimer -= 0.016
        
        if self.m_FootballGameState.goalkeeperTimer <= 0.0 and self.m_FootballGameState.isBallMoving:
            if not self.m_FootballGameState.goalkeeperDiveLeft and not self.m_FootballGameState.goalkeeperDiveRight:
                timeToGoal = (4.0 - self.m_FootballGameState.ballZ) / self.m_FootballGameState.ballSpeedZ if self.m_FootballGameState.ballSpeedZ != 0 else 0
                if timeToGoal > 0:
                    predictedBallX = self.m_FootballGameState.ballX + (self.m_FootballGameState.ballSpeedX * timeToGoal)
                    goalHalfWidth = 3.0
                    
                    if predictedBallX < -goalHalfWidth * 0.7:
                        self.m_FootballGameState.goalkeeperDiveLeft = True
                    elif predictedBallX > goalHalfWidth * 0.7:
                        self.m_FootballGameState.goalkeeperDiveRight = True
                    elif predictedBallX < -1.0:
                        self.m_FootballGameState.goalkeeperDiveLeft = True
                    elif predictedBallX > 1.0:
                        self.m_FootballGameState.goalkeeperDiveRight = True
                    else:
                        if random.randint(0, 1) == 0:
                            self.m_FootballGameState.goalkeeperDiveLeft = True
                        else:
                            self.m_FootballGameState.goalkeeperDiveRight = True
        
        maxGoalkeeperX = 2.8
        if self.m_FootballGameState.goalkeeperDiveLeft:
            self.m_FootballGameState.goalkeeperX -= 0.08
            if self.m_FootballGameState.goalkeeperX < -maxGoalkeeperX:
                self.m_FootballGameState.goalkeeperX = -maxGoalkeeperX
        elif self.m_FootballGameState.goalkeeperDiveRight:
            self.m_FootballGameState.goalkeeperX += 0.08
            if self.m_FootballGameState.goalkeeperX > maxGoalkeeperX:
                self.m_FootballGameState.goalkeeperX = maxGoalkeeperX
        
        if not self.m_FootballGameState.isBallMoving:
            self.m_FootballGameState.goalkeeperX = 0.0
            self.m_FootballGameState.goalkeeperDiveLeft = False
            self.m_FootballGameState.goalkeeperDiveRight = False
            self.m_FootballGameState.goalkeeperTimer = 0.5

    def ResetBall(self):
        self.m_FootballGameState.ballX = 0.0
        self.m_FootballGameState.ballY = 0.2
        self.m_FootballGameState.ballZ = -2.9
        self.m_FootballGameState.ballSpeedX = 0.0
        self.m_FootballGameState.ballSpeedY = 0.0
        self.m_FootballGameState.ballSpeedZ = 0.0
        self.m_FootballGameState.kickPower = 0.0
        self.m_FootballGameState.isKicking = False

    def CheckGoal(self):
        goalWidth = 6.0
        goalDepth = 1.0
        
        if (self.m_FootballGameState.ballZ >= 3.8 and
            self.m_FootballGameState.ballZ <= 4.0 + goalDepth and
            abs(self.m_FootballGameState.ballX) <= goalWidth / 2.0 + 0.2 and
            self.m_FootballGameState.ballY >= 0.1 and
            self.m_FootballGameState.ballY <= 1.8):
            
            isSaved = False
            
            if self.m_FootballGameState.goalkeeperDiveLeft and self.m_FootballGameState.ballX < -1.0:
                if abs(self.m_FootballGameState.goalkeeperX - self.m_FootballGameState.ballX) < 1.8:
                    isSaved = (random.randint(0, 99) < 60)
            elif self.m_FootballGameState.goalkeeperDiveRight and self.m_FootballGameState.ballX > 1.0:
                if abs(self.m_FootballGameState.goalkeeperX - self.m_FootballGameState.ballX) < 1.8:
                    isSaved = (random.randint(0, 99) < 60)
            elif abs(self.m_FootballGameState.ballX) < 1.5 and abs(self.m_FootballGameState.goalkeeperX) < 1.5:
                isSaved = (random.randint(0, 99) < 50)
            
            if not isSaved:
                self.m_FootballGameState.score += 1
                self.m_FootballGameState.isGoalScored = True
                self.m_FootballGameState.ballSpeedZ *= 0.5
                self.m_FootballGameState.ballSpeedX *= 0.5
                self.m_FootballGameState.ballSpeedY *= 0.3
                if self.m_FootballGameState.ballZ > 7.0:
                    self.m_FootballGameState.isBallMoving = False
                    self.ResetBall()
            else:
                self.m_FootballGameState.isShotSaved = True
                self.m_FootballGameState.ballSpeedX *= -0.7
                self.m_FootballGameState.ballSpeedZ *= -0.4
                self.m_FootballGameState.ballSpeedY *= 0.5
            return
        
        if (self.m_FootballGameState.ballZ > 12.0 or
            abs(self.m_FootballGameState.ballX) > 10.0 or
            self.m_FootballGameState.ballY < -2.0):
            self.m_FootballGameState.isBallMoving = False
            self.m_FootballGameState.isShotMissed = True
            self.ResetBall()

    def DrawFootballField(self):
        openGLView = OpenGLView()
        openGLView.glBegin(GL_QUADS)
        openGLView.glColor3f(0.3, 0.7, 0.3)
        openGLView.glVertex3f(-8.0, 0.0, -4.0)
        openGLView.glVertex3f(8.0, 0.0, -4.0)
        openGLView.glVertex3f(8.0, 0.0, 8.0)
        openGLView.glVertex3f(-8.0, 0.0, 8.0)
        openGLView.glEnd()
        
        openGLView.glBegin(GL_LINES)
        openGLView.glColor3f(1.0, 1.0, 1.0)
        openGLView.glVertex3f(-6.0, 0.01, -3.0)
        openGLView.glVertex3f(6.0, 0.01, -3.0)
        openGLView.glVertex3f(6.0, 0.01, -3.0)
        openGLView.glVertex3f(6.0, 0.01, 6.0)
        openGLView.glVertex3f(6.0, 0.01, 6.0)
        openGLView.glVertex3f(-6.0, 0.01, 6.0)
        openGLView.glVertex3f(-6.0, 0.01, 6.0)
        openGLView.glVertex3f(-6.0, 0.01, -3.0)
        openGLView.glVertex3f(-6.0, 0.01, 1.5)
        openGLView.glVertex3f(6.0, 0.01, 1.5)
        openGLView.glVertex3f(-0.1, 0.01, -2.8)
        openGLView.glVertex3f(0.1, 0.01, -2.8)
        openGLView.glVertex3f(0.0, 0.01, -2.9)
        openGLView.glVertex3f(0.0, 0.01, -2.7)
        openGLView.glEnd()

    def DrawGoalPost(self):
        openGLView = OpenGLView()
        openGLView.glColor3f(1.0, 1.0, 1.0)
        goalWidth = 6.0
        goalHeight = 1.8
        
        openGLView.glPushMatrix()
        openGLView.glTranslatef(-goalWidth / 2, goalHeight / 2, 4.0)
        openGLView.glScalef(0.1, goalHeight, 0.1)
        self.DrawCubePrimitive(1.0)
        openGLView.glPopMatrix()
        
        openGLView.glPushMatrix()
        openGLView.glTranslatef(goalWidth / 2, goalHeight / 2, 4.0)
        openGLView.glScalef(0.1, goalHeight, 0.1)
        self.DrawCubePrimitive(1.0)
        openGLView.glPopMatrix()
        
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, goalHeight, 4.0)
        openGLView.glScalef(goalWidth, 0.1, 0.1)
        self.DrawCubePrimitive(1.0)
        openGLView.glPopMatrix()

    def DrawFootball(self):
        applicationView = ApplicationView()
        openGLView = OpenGLView()
        
        openGLView.glPushMatrix()
        openGLView.glTranslatef(self.m_FootballGameState.ballX, self.m_FootballGameState.ballY, self.m_FootballGameState.ballZ)
        spin = self.m_FootballGameState.gameTime * 10.0
        openGLView.glRotatef(spin * 300.0, 1.0, 1.0, 0.5)
        radius = 0.13
        applicationView.SetColorf(0.12, 0.12, 0.12)
        applicationView.DrawSphere(radius, 40, 40)
        openGLView.glPopMatrix()

    def DrawKicker(self):
        openGLView = OpenGLView()
        
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, 0.0, -2.0)
        openGLView.glRotatef(self.m_FootballGameState.kickerAngle, 0.0, 1.0, 0.0)
        
        kickT = 0.0
        if self.m_FootballGameState.isKicking:
            kickT = math.sin(self.m_FootballGameState.gameTime * 12.0)
            if kickT < 0.0:
                kickT = 0.0
        
        kickAngle = -15.0 + 100.0 * kickT
        
        # Body segments
        openGLView.glColor3f(0.95, 0.8, 0.65)
        self.DrawCubeWithTransform(openGLView, 0.0, 0.90, 0.0, 0.20, 0.18, 0.20)
        
        openGLView.glColor3f(0.0, 0.3, 0.9)
        self.DrawCubeWithTransform(openGLView, 0.0, 0.65, 0.0, 0.44, 0.35, 0.26)
        
        openGLView.glColor3f(0.0, 0.25, 0.8)
        self.DrawCubeWithRotation(openGLView, -0.28, 0.65, 0.0, 20.0, 0.13, 0.30, 0.13)
        self.DrawCubeWithRotation(openGLView, 0.28, 0.65, 0.0, -20.0, 0.13, 0.30, 0.13)
        
        openGLView.glColor3f(1.0, 1.0, 1.0)
        self.DrawCubeWithTransform(openGLView, 0.0, 0.40, 0.0, 0.48, 0.18, 0.30)
        
        openGLView.glColor3f(0.95, 0.8, 0.65)
        self.DrawCubeWithTransform(openGLView, -0.11, 0.25, 0.0, 0.13, 0.28, 0.13)
        
        openGLView.glColor3f(1.0, 1.0, 1.0)
        self.DrawCubeWithTransform(openGLView, -0.11, 0.05, 0.0, 0.15, 0.20, 0.15)
        
        # Leg with kick animation
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.11, 0.40, 0.0)
        openGLView.glRotatef(kickAngle, 1.0, 0.0, 0.0)
        openGLView.glColor3f(0.95, 0.8, 0.65)
        self.DrawCubeWithTransform(openGLView, 0.0, -0.14, 0.0, 0.13, 0.28, 0.13)
        openGLView.glTranslatef(0.0, -0.28, 0.0)
        kneeBend = 40.0 * (kickT - 0.4) / 0.6 if kickT > 0.4 else 0.0
        openGLView.glRotatef(-kneeBend, 1.0, 0.0, 0.0)
        self.DrawCubeWithTransform(openGLView, 0.0, -0.12, 0.0, 0.11, 0.24, 0.11)
        openGLView.glPopMatrix()
        
        openGLView.glPopMatrix()

    def DrawGoalkeeper(self):
        openGLView = OpenGLView()
        
        openGLView.glPushMatrix()
        openGLView.glTranslatef(self.m_FootballGameState.goalkeeperX, 0.0, 4.0)
        
        diveAngle = 0.0
        diveOffsetY = 0.0
        diveOffsetX = 0.0
        
        if self.m_FootballGameState.goalkeeperDiveLeft:
            diveAngle = -48.0
            diveOffsetY = 0.6
            diveOffsetX = -0.45
        elif self.m_FootballGameState.goalkeeperDiveRight:
            diveAngle = 48.0
            diveOffsetY = 0.25
            diveOffsetX = 0.45
        
        openGLView.glTranslatef(diveOffsetX, diveOffsetY, 0.0)
        openGLView.glRotatef(diveAngle, 0.0, 0.0, 1.0)
        
        openGLView.glColor3f(0.95, 0.8, 0.65)
        self.DrawCubeWithTransform(openGLView, 0.0, 1.12, 0.0, 0.22, 0.24, 0.22)
        
        openGLView.glColor3f(0.0, 0.9, 0.3)
        self.DrawCubeWithTransform(openGLView, 0.0, 0.80, 0.0, 0.48, 0.48, 0.28)
        
        openGLView.glColor3f(0.0, 0.8, 0.25)
        self.DrawCubeWithRotation(openGLView, -0.35, 0.90, 0.0, -70.0 + diveAngle * 0.8, 0.14, 0.45, 0.14)
        self.DrawCubeWithRotation(openGLView, 0.35, 0.90, 0.0, 70.0 + diveAngle * 0.8, 0.14, 0.45, 0.14)
        
        openGLView.glColor3f(0.1, 0.1, 0.1)
        self.DrawCubeWithTransform(openGLView, -0.58, 0.90, 0.0, 0.18, 0.14, 0.18)
        self.DrawCubeWithTransform(openGLView, 0.58, 0.90, 0.0, 0.18, 0.14, 0.18)
        self.DrawCubeWithTransform(openGLView, 0.0, 0.55, 0.0, 0.50, 0.24, 0.32)
        
        openGLView.glColor3f(0.95, 0.8, 0.65)
        self.DrawCubeWithRotation(openGLView, -0.11, 0.35, 0.0, 20.0 + diveAngle * 0.6, 0.14, 0.38, 0.14)
        self.DrawCubeWithRotation(openGLView, 0.11, 0.35, 0.0, -30.0 + diveAngle * 0.7, 0.14, 0.38, 0.14)
        
        openGLView.glColor3f(0.0, 0.7, 0.2)
        self.DrawCubeWithTransform(openGLView, -0.11, 0.08, 0.0, 0.16, 0.25, 0.16)
        self.DrawCubeWithTransform(openGLView, 0.11, 0.08, 0.0, 0.16, 0.25, 0.16)
        
        openGLView.glColor3f(0.1, 0.1, 0.1)
        self.DrawCubeWithTransform(openGLView, -0.11, 0.0, 0.06, 0.16, 0.10, 0.26)
        self.DrawCubeWithTransform(openGLView, 0.11, 0.0, 0.06, 0.16, 0.10, 0.26)
        
        openGLView.glPopMatrix()

    def DrawCubePrimitive(self, size):
        openGLView = OpenGLView()
        s = size / 2.0
        
        openGLView.glBegin(GL_QUADS)
        # Front face
        openGLView.glVertex3f(-s, -s, s)
        openGLView.glVertex3f(s, -s, s)
        openGLView.glVertex3f(s, s, s)
        openGLView.glVertex3f(-s, s, s)
        # Back face
        openGLView.glVertex3f(-s, -s, -s)
        openGLView.glVertex3f(-s, s, -s)
        openGLView.glVertex3f(s, s, -s)
        openGLView.glVertex3f(s, -s, -s)
        # Top face
        openGLView.glVertex3f(-s, s, -s)
        openGLView.glVertex3f(-s, s, s)
        openGLView.glVertex3f(s, s, s)
        openGLView.glVertex3f(s, s, -s)
        # Bottom face
        openGLView.glVertex3f(-s, -s, -s)
        openGLView.glVertex3f(s, -s, -s)
        openGLView.glVertex3f(s, -s, s)
        openGLView.glVertex3f(-s, -s, s)
        # Right face
        openGLView.glVertex3f(s, -s, -s)
        openGLView.glVertex3f(s, s, -s)
        openGLView.glVertex3f(s, s, s)
        openGLView.glVertex3f(s, -s, s)
        # Left face
        openGLView.glVertex3f(-s, -s, -s)
        openGLView.glVertex3f(-s, -s, s)
        openGLView.glVertex3f(-s, s, s)
        openGLView.glVertex3f(-s, s, -s)
        openGLView.glEnd()

    def DrawCubeWithTransform(self, openGLView, x, y, z, sx, sy, sz):
        openGLView.glPushMatrix()
        openGLView.glTranslatef(x, y, z)
        openGLView.glScalef(sx, sy, sz)
        self.DrawCubePrimitive(1.0)
        openGLView.glPopMatrix()

    def DrawCubeWithRotation(self, openGLView, x, y, z, angle, sx, sy, sz):
        openGLView.glPushMatrix()
        openGLView.glTranslatef(x, y, z)
        openGLView.glRotatef(angle, 0.0, 0.0, 1.0)
        openGLView.glScalef(sx, sy, sz)
        self.DrawCubePrimitive(1.0)
        openGLView.glPopMatrix()

    def DrawStadium(self):
        openGLView = OpenGLView()
        openGLView.glColor3f(0.4, 0.4, 0.4)
        openGLView.glBegin(GL_QUADS)
        openGLView.glVertex3f(-8.0, 0.0, -4.0)
        openGLView.glVertex3f(-8.0, 3.0, -4.0)
        openGLView.glVertex3f(-8.0, 3.0, 8.0)
        openGLView.glVertex3f(-8.0, 0.0, 8.0)
        openGLView.glVertex3f(8.0, 0.0, -4.0)
        openGLView.glVertex3f(8.0, 3.0, -4.0)
        openGLView.glVertex3f(8.0, 3.0, 8.0)
        openGLView.glVertex3f(8.0, 0.0, 8.0)
        openGLView.glVertex3f(-8.0, 0.0, 8.0)
        openGLView.glVertex3f(-8.0, 3.0, 8.0)
        openGLView.glVertex3f(8.0, 3.0, 8.0)
        openGLView.glVertex3f(8.0, 0.0, 8.0)
        openGLView.glEnd()

    def DrawNet(self):
        openGLView = OpenGLView()
        goalWidth = 6.0
        goalHeight = 1.8
        
        openGLView.glColor4f(1.0, 1.0, 1.0, 0.3)
        for i in range(0, 9):
            x = -goalWidth / 2 + (goalWidth / 8) * i
            openGLView.glBegin(GL_LINES)
            openGLView.glVertex3f(x, 0.0, 4.0)
            openGLView.glVertex3f(x, goalHeight, 4.0)
            openGLView.glEnd()
        
        for i in range(0, 5):
            y = (goalHeight / 4) * i
            openGLView.glBegin(GL_LINES)
            openGLView.glVertex3f(-goalWidth / 2, y, 4.0)
            openGLView.glVertex3f(goalWidth / 2, y, 4.0)
            openGLView.glEnd()

    def DrawResultMessage(self):
        applicationView = ApplicationView()
        
        if self.m_FootballGameState.isGoalScored or self.m_FootballGameState.isShotSaved or self.m_FootballGameState.isShotMissed:
            strMessage = ""
            if self.m_FootballGameState.isGoalScored:
                strMessage = "GOAL!"
                applicationView.SetColorf(0.0, 1.0, 0.0)
            elif self.m_FootballGameState.isShotSaved:
                strMessage = "SAVED!"
                applicationView.SetColorf(1.0, 1.0, 0.0)
            elif self.m_FootballGameState.isShotMissed:
                strMessage = "MISSED!"
                applicationView.SetColorf(1.0, 0.0, 0.0)
            
            applicationView.BeginDraw(int(GL_QUADS))
            applicationView.Set2DVertexf(-0.2, 0.1)
            applicationView.Set2DVertexf(0.2, 0.1)
            applicationView.Set2DVertexf(0.2, 0.2)
            applicationView.Set2DVertexf(-0.2, 0.2)
            applicationView.EndDraw()
            
            applicationView.SetColorf(1.0, 1.0, 1.0)
            applicationView.BeginDraw(int(GL_LINE_LOOP))
            applicationView.Set2DVertexf(-0.2, 0.1)
            applicationView.Set2DVertexf(0.2, 0.1)
            applicationView.Set2DVertexf(0.2, 0.2)
            applicationView.Set2DVertexf(-0.2, 0.2)
            applicationView.EndDraw()

    def DrawPenaltyKickView(self):
        applicationView = ApplicationView()
        applicationView.InitializeEnvironment(True)
        applicationView.BeginGraphicsCommands()
        applicationView.SetBkgColor(0.2, 0.6, 0.8, 1.0)
        try:
            applicationView.StartNewDisplayList()
        except Exception:
            applicationView.EndGraphicsCommands()
            return
        self.UpdateFootballPenalty()
        self.DrawStadium()
        self.DrawFootballField()
        self.DrawGoalPost()
        self.DrawNet()
        self.DrawFootball()
        self.DrawKicker()
        self.DrawGoalkeeper()
        self.DrawAimingReticle()
        self.DrawResultMessage()
        applicationView.EndNewDisplayList()
        applicationView.EndGraphicsCommands()
        applicationView.Refresh()

    def DrawAimingReticle(self):
        if not self.m_bShowAimingReticle:
            return
        
        applicationView = ApplicationView()
        applicationView.SetColorf(1.0, 1.0, 1.0)
        applicationView.SetLineWidth(2.0)
        
        applicationView.BeginDraw(int(GL_LINES))
        applicationView.Set2DVertexf(-0.05, 0.0)
        applicationView.Set2DVertexf(0.05, 0.0)
        applicationView.Set2DVertexf(0.0, -0.05)
        applicationView.Set2DVertexf(0.0, 0.05)
        applicationView.EndDraw()
        
        angleIndicatorX = math.sin(self.m_FootballGameState.kickerAngle * math.pi / 180.0) * 0.1
        applicationView.SetColorf(1.0, 0.0, 0.0)
        applicationView.BeginDraw(int(GL_LINES))
        applicationView.Set2DVertexf(0.0, -0.05)
        applicationView.Set2DVertexf(angleIndicatorX, -0.08)
        applicationView.EndDraw()

    # ==================== ELEPHANT DRAWING METHODS ====================
    
    def DrawElephant(self):
        applicationView = ApplicationView()
        applicationView.InitializeEnvironment(True)
        applicationView.BeginGraphicsCommands()
        applicationView.SetBkgColor(0.45, 0.75, 0.98, 1.0)
        
        try:
            applicationView.StartNewDisplayList()
        except Exception:
            applicationView.EndGraphicsCommands()
            return
        
        openGLView = OpenGLView()
        self.UpdateElephantAnimation()
        self.DrawSavannahEnvironmentWithPond()  # This now includes the pond
        
        openGLView.glPushMatrix()
        
        baseLift = 0.02
        openGLView.glTranslatef(self.m_ElephantGameState.positionX, baseLift, self.m_ElephantGameState.positionZ)
        openGLView.glRotatef(self.m_ElephantGameState.rotationY, 0.0, 1.0, 0.0)
        
        overallScale = 0.8
        openGLView.glScalef(overallScale, overallScale, overallScale)
        
        self.DrawElephantLegs()
        self.DrawElephantBody()
        self.DrawElephantTail()  # Added tail here
        self.DrawElephantHead()
        self.DrawElephantEars()
        self.DrawElephantTusks()
        self.DrawElephantTrunk()
        
        openGLView.glPopMatrix()
        
        applicationView.EndNewDisplayList()
        applicationView.EndGraphicsCommands()
        applicationView.Refresh()

    def UpdateElephantAnimation(self):
        self.m_ElephantGameState.animationTime += 0.016
        breathe = math.sin(self.m_ElephantGameState.animationTime * 1.5) * 0.008
        self.m_ElephantGameState.headBob = breathe + math.sin(self.m_ElephantGameState.animationTime * 0.8) * 0.005
        self.m_ElephantGameState.trunkSwing = math.sin(self.m_ElephantGameState.animationTime * 1.8) * 0.2
        self.m_ElephantGameState.earFlap = math.sin(self.m_ElephantGameState.animationTime * 1.2) * 0.15
        
        if self.m_ElephantGameState.currentState != 0:
            moveSpeed = self.m_ElephantGameState.walkSpeed
            radY = self.m_ElephantGameState.rotationY * math.pi / 180.0
            self.m_ElephantGameState.positionZ -= math.cos(radY) * moveSpeed
            self.m_ElephantGameState.positionX -= math.sin(radY) * moveSpeed
        
        if self.m_ElephantGameState.isTrumpeting:
            self.m_ElephantGameState.trumpetProgress += 0.03
            if self.m_ElephantGameState.trumpetProgress >= 1.0:
                self.m_ElephantGameState.isTrumpeting = False
                self.m_ElephantGameState.trumpetProgress = 0.0

    def DrawElephantBody(self):
        openGLView = OpenGLView()
        bodyR, bodyG, bodyB = 0.58, 0.58, 0.60
        
        bob = abs(math.sin(self.m_ElephantGameState.animationTime * 4.0)) * 0.02 if self.m_ElephantGameState.currentState != 0 else 0.0
        
        openGLView.glPushMatrix()
        bodyBaseY = 0.85
        openGLView.glTranslatef(0.0, bodyBaseY + bob, 0.0)
        
        openGLView.glColor3f(bodyR, bodyG, bodyB)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, 0.25, 0.0)
        openGLView.glScalef(0.9, 0.5, 1.4)
        self.DrawEllipsoid(1.0, 40, 30)
        openGLView.glPopMatrix()
        
        openGLView.glColor3f(bodyR * 1.05, bodyG * 1.05, bodyB * 1.05)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, 0.5, -0.4)
        openGLView.glScalef(0.7, 0.45, 0.7)
        self.DrawEllipsoid(1.0, 35, 25)
        openGLView.glPopMatrix()
        
        openGLView.glColor3f(bodyR * 0.95, bodyG * 0.95, bodyB * 0.95)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, 0.1, 0.15)
        openGLView.glScalef(0.8, 0.3, 1.1)
        self.DrawEllipsoid(1.0, 40, 30)
        openGLView.glPopMatrix()
        
        openGLView.glColor3f(bodyR * 1.02, bodyG * 1.02, bodyB * 1.02)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, 0.4, -0.75)
        openGLView.glScalef(0.65, 0.4, 0.5)
        self.DrawEllipsoid(1.0, 35, 25)
        openGLView.glPopMatrix()
        
        openGLView.glColor3f(bodyR * 0.98, bodyG * 0.98, bodyB * 0.98)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, 0.35, 0.9)
        openGLView.glScalef(0.45, 0.35, 0.4)
        self.DrawEllipsoid(1.0, 30, 20)
        openGLView.glPopMatrix()
        
        openGLView.glColor3f(bodyR * 0.97, bodyG * 0.97, bodyB * 0.97)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, 0.2, -1.0)
        openGLView.glScalef(0.6, 0.45, 0.55)
        self.DrawEllipsoid(1.0, 30, 20)
        openGLView.glPopMatrix()
        
        openGLView.glPopMatrix()

    def DrawElephantHead(self):
        openGLView = OpenGLView()
        headR, headG, headB = 0.56, 0.56, 0.58
        
        openGLView.glPushMatrix()
        bodyBaseY = 0.8
        neckBaseY = bodyBaseY + 0.35
        headY = neckBaseY + 0.1 + self.m_ElephantGameState.headBob
        headZ = 1.15
        
        openGLView.glTranslatef(0.0, headY, headZ)
        
        openGLView.glColor3f(headR, headG, headB)
        openGLView.glPushMatrix()
        openGLView.glScalef(0.55, 0.45, 0.5)
        self.DrawEllipsoid(1.0, 35, 25)
        openGLView.glPopMatrix()
        
        openGLView.glColor3f(headR * 1.02, headG * 1.02, headB * 1.02)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, 0.15, 0.1)
        openGLView.glScalef(0.45, 0.25, 0.35)
        self.DrawEllipsoid(1.0, 30, 20)
        openGLView.glPopMatrix()
        
        # Eyes
        openGLView.glColor3f(0.55, 0.35, 0.15)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.46, 0.20, 0.16)
        openGLView.glScalef(0.035, 0.035, 0.035)
        self.DrawEllipsoid(1.0, 12, 8)
        openGLView.glPopMatrix()
        
        openGLView.glColor3f(0.05, 0.05, 0.05)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.46, 0.20, 0.162)
        openGLView.glScalef(0.018, 0.018, 0.018)
        self.DrawEllipsoid(1.0, 12, 8)
        openGLView.glPopMatrix()
        
        openGLView.glColor3f(0.55, 0.35, 0.15)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(-0.46, 0.20, 0.16)
        openGLView.glScalef(0.035, 0.035, 0.035)
        self.DrawEllipsoid(1.0, 12, 8)
        openGLView.glPopMatrix()
        
        openGLView.glColor3f(0.05, 0.05, 0.05)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(-0.46, 0.20, 0.162)
        openGLView.glScalef(0.018, 0.018, 0.018)
        self.DrawEllipsoid(1.0, 12, 8)
        openGLView.glPopMatrix()
        
        openGLView.glColor3f(0.05, 0.05, 0.05)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.48, 0.22, 0.155)
        openGLView.glScalef(0.025, 0.02, 0.03)
        self.DrawEllipsoid(1.0, 8, 6)
        openGLView.glPopMatrix()
        
        openGLView.glPushMatrix()
        openGLView.glTranslatef(-0.48, 0.22, 0.155)
        openGLView.glScalef(0.025, 0.02, 0.03)
        self.DrawEllipsoid(1.0, 8, 6)
        openGLView.glPopMatrix()
        
        openGLView.glColor3f(headR * 0.98, headG * 0.98, headB * 0.98)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(-0.3, 0.0, 0.05)
        openGLView.glScalef(0.2, 0.175, 0.15)
        self.DrawEllipsoid(1.0, 25, 15)
        openGLView.glPopMatrix()
        
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.3, 0.0, 0.05)
        openGLView.glScalef(0.2, 0.175, 0.15)
        self.DrawEllipsoid(1.0, 25, 15)
        openGLView.glPopMatrix()
        
        openGLView.glColor3f(headR * 0.95, headG * 0.95, headB * 0.95)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, -0.15, 0.15)
        openGLView.glScalef(0.2, 0.1, 0.15)
        self.DrawEllipsoid(1.0, 20, 15)
        openGLView.glPopMatrix()
        
        openGLView.glPopMatrix()

    def DrawElephantEars(self):
        openGLView = OpenGLView()
        earR, earG, earB = 0.56, 0.56, 0.58
        flap = self.m_ElephantGameState.earFlap
        
        bodyBaseY = 0.8
        neckBaseY = bodyBaseY + 0.35
        headY = neckBaseY + 0.1
        
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, headY, 1.15)
        
        # Left ear
        openGLView.glPushMatrix()
        openGLView.glTranslatef(-0.45, 0.0, -0.25)
        openGLView.glRotatef(-25.0 + flap * 15.0, 0.0, 0.0, 1.0)
        openGLView.glRotatef(-10.0, 1.0, 0.0, 0.0)
        openGLView.glColor3f(earR, earG, earB)
        openGLView.glPushMatrix()
        openGLView.glScalef(0.9, 0.5, 0.025)
        self.DrawEllipsoid(1.0, 35, 20)
        openGLView.glPopMatrix()
        openGLView.glPopMatrix()
        
        # Right ear
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.45, 0.0, -0.25)
        openGLView.glRotatef(25.0 - flap * 15.0, 0.0, 0.0, 1.0)
        openGLView.glRotatef(-10.0, 1.0, 0.0, 0.0)
        openGLView.glColor3f(earR, earG, earB)
        openGLView.glPushMatrix()
        openGLView.glScalef(0.9, 0.5, 0.025)
        self.DrawEllipsoid(1.0, 35, 20)
        openGLView.glPopMatrix()
        openGLView.glPopMatrix()
        
        openGLView.glPopMatrix()

    def DrawElephantTrunk(self):
        openGLView = OpenGLView()
        trunkR, trunkG, trunkB = 0.57, 0.57, 0.59
        swing = self.m_ElephantGameState.trunkSwing * 0.3
        trunkLift = 0.4 if self.m_ElephantGameState.isTrumpeting else 0.0
        trunkScale = 0.6
        
        bodyBaseY = 0.8
        neckBaseY = bodyBaseY + 0.35
        headY = neckBaseY + 0.1
        
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, headY, 1.15)
        openGLView.glTranslatef(0.0, -0.2, 0.5)
        
        openGLView.glColor3f(trunkR, trunkG, trunkB)
        openGLView.glPushMatrix()
        openGLView.glScalef(0.2, 0.25 * trunkScale, 0.1)
        self.DrawEllipsoid(1.0, 30, 20)
        openGLView.glPopMatrix()
        
        openGLView.glTranslatef(swing * 0.1, trunkLift * 0.15 * trunkScale - 0.2 * trunkScale, 0.0)
        openGLView.glRotatef(swing * 10.0, 0.0, 1.0, 0.0)
        
        numSegments = 6
        segmentLengths = [0.3, 0.275, 0.25, 0.225, 0.2, 0.175]
        segmentRadii = [0.175, 0.16, 0.145, 0.13, 0.115, 0.1]
        
        for i in range(numSegments):
            segLen = segmentLengths[i] * trunkScale
            openGLView.glPushMatrix()
            openGLView.glScalef(segmentRadii[i], segLen, segmentRadii[i])
            self.DrawEllipsoid(1.0, 25, 18)
            openGLView.glPopMatrix()
            openGLView.glTranslatef(0.0, -segLen, 0.0)
        
        openGLView.glColor3f(trunkR * 0.9, trunkG * 0.9, trunkB * 0.9)
        openGLView.glPushMatrix()
        openGLView.glScalef(0.06, 0.1 * trunkScale, 0.06)
        self.DrawEllipsoid(1.0, 20, 15)
        openGLView.glPopMatrix()
        
        # Nostrils
        openGLView.glColor3f(0.15, 0.15, 0.15)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(-0.025, -0.075 * trunkScale, 0.0)
        openGLView.glScalef(0.02, 0.02, 0.02)
        self.DrawSphere(1.0, 8, 6)
        openGLView.glPopMatrix()
        
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.025, -0.075 * trunkScale, 0.0)
        openGLView.glScalef(0.02, 0.02, 0.02)
        self.DrawSphere(1.0, 8, 6)
        openGLView.glPopMatrix()
        
        openGLView.glPopMatrix()

    def DrawElephantLegs(self):
        openGLView = OpenGLView()
        legR, legG, legB = 0.55, 0.55, 0.57
        walkCycle = self.m_ElephantGameState.animationTime * 4.0
        frontLift = math.sin(walkCycle) * 0.1 if self.m_ElephantGameState.currentState != 0 else 0.0
        backLift = math.sin(walkCycle + math.pi) * 0.1
        
        legs = [
            (-0.6, 0.5, frontLift, 1),
            (0.6, 0.5, frontLift, 1),
            (-0.45, -0.75, backLift, 0),
            (0.45, -0.75, backLift, 0)
        ]
        
        legLength = 0.85
        frontLegRadius = 0.18
        backLegRadius = 0.20
        baseLift = 0.02
        
        for leg in legs:
            openGLView.glPushMatrix()
            openGLView.glTranslatef(leg[0], leg[2] + baseLift, leg[1])
            currentRadius = frontLegRadius if leg[3] > 0.5 else backLegRadius
            
            openGLView.glColor3f(legR, legG, legB)
            self.DrawCylinder(currentRadius, legLength)
            
            openGLView.glColor3f(0.45, 0.45, 0.48)
            openGLView.glPushMatrix()
            openGLView.glTranslatef(0.0, -0.005, 0.0)
            footScale = currentRadius * 1.45 if leg[3] > 0.5 else currentRadius * 1.5
            openGLView.glScalef(footScale, 0.025, footScale)
            self.DrawEllipsoid(1.0, 40, 40)
            openGLView.glPopMatrix()
            
            openGLView.glColor3f(0.65, 0.58, 0.50)
            toeSpacing = 0.12 if leg[3] > 0.5 else 0.10
            toePositions = [
                (-toeSpacing * 2.0, -0.02),
                (-toeSpacing, -0.06),
                (0.0, -0.08),
                (toeSpacing, -0.06),
                (toeSpacing * 2.0, -0.02)
            ]
            
            for toe in toePositions:
                openGLView.glPushMatrix()
                openGLView.glTranslatef(toe[0], 0.02, toe[1])
                openGLView.glRotatef(15.0, 1.0, 0.0, 0.0)
                nailScaleX = 0.055 if toe == (0.0, -0.08) else 0.040
                nailScaleY = 0.015
                nailScaleZ = 0.045 if toe == (0.0, -0.08) else 0.035
                openGLView.glScalef(nailScaleX, nailScaleY, nailScaleZ)
                self.DrawEllipsoid(1.0, 16, 12)
                openGLView.glPopMatrix()
            
            openGLView.glPopMatrix()

    def DrawElephantTail(self):
        openGLView = OpenGLView()
        tailR, tailG, tailB = 0.50, 0.50, 0.52
        sway = math.sin(self.m_ElephantGameState.animationTime * 1.4) * 8.0
        
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, 1.15, -1.3)
        openGLView.glRotatef(sway, 0.0, 1.0, 0.0)
        
        tailHang = 40.0 + math.sin(self.m_ElephantGameState.animationTime * 0.7) * 4.0
        openGLView.glRotatef(-tailHang, 1.0, 0.0, 0.0)
        
        openGLView.glColor3f(tailR, tailG, tailB)
        openGLView.glPushMatrix()
        openGLView.glRotatef(-50.0, 1.0, 0.0, 0.0)
        openGLView.glBegin(GL_TRIANGLE_FAN)
        openGLView.glVertex3f(0.0, 0.0, 0.0)
        baseRadius = 0.07
        discSegments = 16
        for j in range(discSegments + 1):
            angle = float(j) / float(discSegments) * 2.0 * math.pi
            x = baseRadius * math.cos(angle)
            y = baseRadius * math.sin(angle)
            openGLView.glVertex3f(x, y, 0.0)
        openGLView.glEnd()
        openGLView.glPopMatrix()
        
        # Use simpler tail segments for better compatibility
        tipRadius = 0.018
        tailLength = 0.85
        segments = 14
        circleSegments = 10
        
        openGLView.glBegin(GL_QUAD_STRIP)
        for i in range(segments + 1):
            t = float(i) / float(segments)
            radius = baseRadius * (1.0 - t) + tipRadius * t
            z = -0.02 - tailLength * t
            curve = t * t * 0.6
            x = math.sin(self.m_ElephantGameState.animationTime * 0.3 + t * 2.5) * 0.015
            y = -curve
            
            if i < segments:
                t2 = float(i + 1) / float(segments)
                radius2 = baseRadius * (1.0 - t2) + tipRadius * t2
                z2 = -0.02 - tailLength * t2
                curve2 = t2 * t2 * 0.6
                x2 = math.sin(self.m_ElephantGameState.animationTime * 0.3 + t2 * 2.5) * 0.015
                y2 = -curve2
                
                for j in range(circleSegments + 1):
                    angle = float(j) / float(circleSegments) * 2.0 * math.pi
                    cosA = math.cos(angle)
                    sinA = math.sin(angle)
                    
                    circleX = x + radius * cosA
                    circleY = y + radius * sinA * 0.5
                    circleZ = z
                    
                    nextCircleX = x2 + radius2 * cosA
                    nextCircleY = y2 + radius2 * sinA * 0.5
                    nextCircleZ = z2
                    
                    openGLView.glVertex3f(circleX, circleY, circleZ)
                    openGLView.glVertex3f(nextCircleX, nextCircleY, nextCircleZ)
        openGLView.glEnd()
        
        # Tail tip hair
        openGLView.glColor3f(0.18, 0.18, 0.20)
        tipZ = -0.02 - tailLength
        tipY = -0.6
        
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, tipY, tipZ)
        openGLView.glRotatef(-tailHang * 0.8, 1.0, 0.0, 0.0)
        openGLView.glRotatef(sway * 2.0, 0.0, 1.0, 0.0)
        
        numHairs = 20
        hairLength = 0.45
        fanAngle = 70.0
        
        for i in range(numHairs):
            angle = (i - (numHairs - 1) / 2.0) * (fanAngle / (numHairs - 1))
            openGLView.glPushMatrix()
            openGLView.glRotatef(angle, 0.0, 0.0, 1.0)
            wave = math.sin(self.m_ElephantGameState.animationTime * 2.8 + i * 1.3) * 10.0
            openGLView.glRotatef(-80.0 + wave, 1.0, 0.0, 0.0)
            
            openGLView.glBegin(GL_LINES)
            openGLView.glVertex3f(0.0, 0.0, 0.0)
            openGLView.glVertex3f(0.0, 0.0, -hairLength)
            openGLView.glEnd()
            
            openGLView.glPopMatrix()
        
        openGLView.glPopMatrix()
        openGLView.glPopMatrix()

    def DrawElephantTusks(self):
        openGLView = OpenGLView()
        openGLView.glColor3f(0.95, 0.95, 0.88)
        tuskLength = 0.8
        baseRadius = 0.05
        tipRadius = 0.015
        
        bodyBaseY = 0.8
        neckBaseY = bodyBaseY + 0.35
        headY = neckBaseY + 0.1 + self.m_ElephantGameState.headBob
        
        # Left tusk
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, headY, 1.15)
        openGLView.glTranslatef(-0.2, -0.15, 0.4)
        openGLView.glRotatef(-20.0, 0.0, 1.0, 0.0)
        openGLView.glRotatef(-5.0, 1.0, 0.0, 0.0)
        
        segments = 12
        for i in range(segments):
            t = float(i) / float(segments)
            radius = baseRadius * (1.0 - t) + tipRadius * t
            y = -t * t * 0.15
            z = t * tuskLength
            openGLView.glPushMatrix()
            openGLView.glTranslatef(0.0, y, z)
            openGLView.glScalef(radius, radius, tuskLength / float(segments))
            self.DrawEllipsoid(1.0, 10, 8)
            openGLView.glPopMatrix()
        openGLView.glPopMatrix()
        
        # Right tusk
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, headY, 1.15)
        openGLView.glTranslatef(0.2, -0.15, 0.4)
        openGLView.glRotatef(20.0, 0.0, 1.0, 0.0)
        openGLView.glRotatef(-5.0, 1.0, 0.0, 0.0)
        
        for i in range(segments):
            t = float(i) / float(segments)
            radius = baseRadius * (1.0 - t) + tipRadius * t
            y = -t * t * 0.15
            z = t * tuskLength
            openGLView.glPushMatrix()
            openGLView.glTranslatef(0.0, y, z)
            openGLView.glScalef(radius, radius, tuskLength / float(segments))
            self.DrawEllipsoid(1.0, 10, 8)
            openGLView.glPopMatrix()
        openGLView.glPopMatrix()

    # ==================== BASIC OBJECT DRAWING METHODS ====================
    
    def DrawCube(self):
        applicationView = ApplicationView()
        radius = 0.34
        applicationView.InitializeEnvironment(True)
        applicationView.BeginGraphicsCommands()
        applicationView.SetBkgColor(self.m_ObjectPattern.m_Color.R / 255.0,
                                    self.m_ObjectPattern.m_Color.G / 255.0,
                                    self.m_ObjectPattern.m_Color.B / 255.0, 1.0)
        applicationView.StartNewDisplayList()
        openGLView = OpenGLView()
        openGLView.glBegin(GL_QUAD_STRIP)
        openGLView.glColor3f(1.0, 0.0, 1.0)
        openGLView.glVertex3f(-0.3, 0.3, 0.3)
        openGLView.glColor3f(1.0, 0.0, 0.0)
        openGLView.glVertex3f(-0.3, -0.3, 0.3)
        openGLView.glColor3f(1.0, 1.0, 1.0)
        openGLView.glVertex3f(0.3, 0.3, 0.3)
        openGLView.glColor3f(1.0, 1.0, 0.0)
        openGLView.glVertex3f(0.3, -0.3, 0.3)
        openGLView.glColor3f(0.0, 1.0, 1.0)
        openGLView.glVertex3f(0.3, 0.3, -0.3)
        openGLView.glColor3f(0.0, 1.0, 0.0)
        openGLView.glVertex3f(0.3, -0.3, -0.3)
        openGLView.glColor3f(0.0, 0.0, 1.0)
        openGLView.glVertex3f(-0.3, 0.3, -0.3)
        openGLView.glColor3f(0.0, 0.0, 0.0)
        openGLView.glVertex3f(-0.3, -0.3, -0.3)
        openGLView.glColor3f(1.0, 0.0, 1.0)
        openGLView.glVertex3f(-0.3, 0.3, 0.3)
        openGLView.glColor3f(1.0, 0.0, 0.0)
        openGLView.glVertex3f(-0.3, -0.3, 0.3)
        openGLView.glEnd()
        openGLView.glBegin(GL_QUADS)
        openGLView.glColor3f(1.0, 0.0, 1.0)
        openGLView.glVertex3f(-0.3, 0.3, 0.3)
        openGLView.glColor3f(1.0, 1.0, 1.0)
        openGLView.glVertex3f(0.3, 0.3, 0.3)
        openGLView.glColor3f(0.0, 1.0, 1.0)
        openGLView.glVertex3f(0.3, 0.3, -0.3)
        openGLView.glColor3f(0.0, 0.0, 1.0)
        openGLView.glVertex3f(-0.3, 0.3, -0.3)
        openGLView.glColor3f(1.0, 0.0, 0.0)
        openGLView.glVertex3f(-0.3, -0.3, 0.3)
        openGLView.glColor3f(0.0, 0.0, 0.0)
        openGLView.glVertex3f(-0.3, -0.3, -0.3)
        openGLView.glColor3f(0.0, 1.0, 0.0)
        openGLView.glVertex3f(0.3, -0.3, -0.3)
        openGLView.glColor3f(1.0, 1.0, 0.0)
        openGLView.glVertex3f(0.3, -0.3, 0.3)
        openGLView.glEnd()
        openGLView.glColor3f(1.0, 1.0, 1.0)
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

    def DrawBall(self):
        applicationView = ApplicationView()
        applicationView.InitializeEnvironment(True)
        applicationView.BeginGraphicsCommands()
        applicationView.SetBkgColor(self.m_ObjectPattern.m_Color.R / 255.0,
                                    self.m_ObjectPattern.m_Color.G / 255.0,
                                    self.m_ObjectPattern.m_Color.B / 255.0, 1)
        SECTIONS = 25
        RADIUS = 1.0
        applicationView.StartNewDisplayList()
        applicationView.SetColorf(0.0, 0.0, 1.0)
        applicationView.DrawSphere(RADIUS, SECTIONS, SECTIONS)
        applicationView.SetColorf(1.0, 1.0, 1.0)
        applicationView.DrawSphere(RADIUS / 1.5, SECTIONS, SECTIONS)
        applicationView.EndNewDisplayList()
        applicationView.EndGraphicsCommands()
        applicationView.Refresh()

    def DrawPyramid(self):
        applicationView = ApplicationView()
        applicationView.ResetScene()
        applicationView.InitializeEnvironment(True)
        applicationView.BeginGraphicsCommands()
        applicationView.SetBkgColor(self.m_ObjectPattern.m_Color.R / 255.0,
                                    self.m_ObjectPattern.m_Color.G / 255.0,
                                    self.m_ObjectPattern.m_Color.B / 255.0, 1)
        applicationView.StartNewDisplayList()
        openGLView = OpenGLView()
        openGLView.glTranslatef(0.01, 0.0, 0.01)
        openGLView.glColor3f(0.0, 0.4, 0.8)
        openGLView.glBegin(GL_TRIANGLES)
        # Front
        openGLView.glColor3f(1.0, 0.0, 0.0)
        openGLView.glVertex3f(0.0, 1.0, 0.0)
        openGLView.glColor3f(0.0, 1.0, 0.0)
        openGLView.glVertex3f(-1.0, -1.0, 1.0)
        openGLView.glColor3f(0.0, 0.0, 1.0)
        openGLView.glVertex3f(1.0, -1.0, 1.0)
        # Right
        openGLView.glColor3f(1.0, 0.0, 0.0)
        openGLView.glVertex3f(0.0, 1.0, 0.0)
        openGLView.glColor3f(0.0, 1.0, 0.0)
        openGLView.glVertex3f(1.0, -1.0, 1.0)
        openGLView.glColor3f(0.0, 0.0, 1.0)
        openGLView.glVertex3f(1.0, -1.0, -1.0)
        # Back
        openGLView.glColor3f(1.0, 0.0, 0.0)
        openGLView.glVertex3f(0.0, 1.0, 0.0)
        openGLView.glColor3f(0.0, 1.0, 0.0)
        openGLView.glVertex3f(1.0, -1.0, -1.0)
        openGLView.glColor3f(0.0, 0.0, 1.0)
        openGLView.glVertex3f(-1.0, -1.0, -1.0)
        # Left
        openGLView.glColor3f(1.0, 0.0, 0.0)
        openGLView.glVertex3f(0.0, 1.0, 0.0)
        openGLView.glColor3f(0.0, 1.0, 0.0)
        openGLView.glVertex3f(-1.0, -1.0, -1.0)
        openGLView.glColor3f(0.0, 0.0, 1.0)
        openGLView.glVertex3f(-1.0, -1.0, 1.0)
        # Bottom 1
        openGLView.glColor3f(1.0, 0.0, 0.0)
        openGLView.glVertex3f(-1.0, -1.0, 1.0)
        openGLView.glColor3f(0.0, 1.0, 0.0)
        openGLView.glVertex3f(1.0, -1.0, 1.0)
        openGLView.glColor3f(0.0, 0.0, 1.0)
        openGLView.glVertex3f(-1.0, -1.0, -1.0)
        # Bottom 2
        openGLView.glColor3f(1.0, 0.0, 0.0)
        openGLView.glVertex3f(-1.0, -1.0, -1.0)
        openGLView.glColor3f(0.0, 1.0, 0.0)
        openGLView.glVertex3f(1.0, -1.0, -1.0)
        openGLView.glColor3f(0.0, 0.0, 1.0)
        openGLView.glVertex3f(1.0, -1.0, 1.0)
        openGLView.glEnd()
        applicationView.EndNewDisplayList()
        applicationView.EndGraphicsCommands()
        applicationView.Refresh()

    def DrawAeroplane(self):
        applicationView = ApplicationView()
        applicationView.InitializeEnvironment(True)
        applicationView.BeginGraphicsCommands()
        applicationView.SetBkgColor(self.m_ObjectPattern.m_Color.R / 255.0,
                                    self.m_ObjectPattern.m_Color.G / 255.0,
                                    self.m_ObjectPattern.m_Color.B / 255.0, 1)
        applicationView.StartNewDisplayList()
        openGLView = OpenGLView()
        openGLView.glTranslatef(0.01, 0.0, 0.01)
        openGLView.glColor3f(0.0, 0.4, 0.8)
        openGLView.glBegin(GL_TRIANGLES)
        openGLView.glVertex3f(0.0, 0.0, 0.001)
        openGLView.glVertex3f(0.0, -0.5, 1.0)
        openGLView.glVertex3f(0.0, 1.0, 0.001)
        openGLView.glEnd()
        openGLView.glColor3f(0.0, 0.3, 0.7)
        openGLView.glBegin(GL_TRIANGLE_STRIP)
        openGLView.glVertex3f(1.0, -0.5, 0.0)
        openGLView.glVertex3f(0.0, 0.0, 0.2)
        openGLView.glVertex3f(0.0, 2.0, 0.0)
        openGLView.glVertex3f(-1.0, -0.5, 0.0)
        openGLView.glEnd()
        applicationView.EndNewDisplayList()
        applicationView.EndGraphicsCommands()
        applicationView.Refresh()

    # ==================== ENVIRONMENT DRAWING METHODS WITH POND ====================
    
    def DrawSavannahEnvironmentWithPond(self):
        openGLView = OpenGLView()
        
        # Disable blending for the sky
        openGLView.glDisable(0x0BE2)
        
        # Sky gradient
        openGLView.glBegin(GL_QUADS)
        openGLView.glColor3f(0.15, 0.35, 0.65)
        openGLView.glVertex3f(-50.0, 50.0, -50.0)
        openGLView.glVertex3f(50.0, 50.0, -50.0)
        openGLView.glColor3f(0.35, 0.65, 0.95)
        openGLView.glVertex3f(50.0, 15.0, -50.0)
        openGLView.glVertex3f(-50.0, 15.0, -50.0)
        openGLView.glColor3f(0.45, 0.75, 0.98)
        openGLView.glVertex3f(50.0, 0.0, -50.0)
        openGLView.glVertex3f(-50.0, 0.0, -50.0)
        openGLView.glEnd()
        
        # Sky on back side
        openGLView.glBegin(GL_QUADS)
        openGLView.glColor3f(0.15, 0.35, 0.65)
        openGLView.glVertex3f(-50.0, 50.0, 50.0)
        openGLView.glVertex3f(50.0, 50.0, 50.0)
        openGLView.glColor3f(0.35, 0.65, 0.95)
        openGLView.glVertex3f(50.0, 15.0, 50.0)
        openGLView.glVertex3f(-50.0, 15.0, 50.0)
        openGLView.glColor3f(0.45, 0.75, 0.98)
        openGLView.glVertex3f(50.0, 0.0, 50.0)
        openGLView.glVertex3f(-50.0, 0.0, 50.0)
        openGLView.glEnd()
        
        # Enable blending
        openGLView.glEnable(0x0BE2)
        openGLView.glBlendFunc(0x0300, 0x0303)
        
        # Ground
        openGLView.glBegin(GL_QUADS)
        openGLView.glColor3f(0.55, 0.68, 0.35)
        openGLView.glVertex3f(-25.0, 0.0, -25.0)
        openGLView.glVertex3f(25.0, 0.0, -25.0)
        openGLView.glVertex3f(25.0, 0.0, 25.0)
        openGLView.glVertex3f(-25.0, 0.0, 25.0)
        openGLView.glEnd()
        
        # Grass patches
        random.seed(42)
        for patch in range(15):
            x = random.uniform(-22, 22)
            z = random.uniform(-22, 22)
            size = 2.0 + random.uniform(0, 4)
            openGLView.glColor3f(0.45, 0.58, 0.30)
            openGLView.glBegin(GL_QUADS)
            openGLView.glVertex3f(x - size, 0.01, z - size)
            openGLView.glVertex3f(x + size, 0.01, z - size)
            openGLView.glVertex3f(x + size, 0.01, z + size)
            openGLView.glVertex3f(x - size, 0.01, z + size)
            openGLView.glEnd()
        
        # ==================== POND ====================
        pondCenterX = 0.0
        pondCenterZ = 8.0
        pondRadius = 4.5
        
        # Pond bank
        openGLView.glBegin(GL_QUADS)
        openGLView.glColor3f(0.65, 0.55, 0.35)
        for i in range(36):
            angle1 = i * (360.0 / 36) * math.pi / 180.0
            angle2 = (i + 1) * (360.0 / 36) * math.pi / 180.0
            bankWidth = 0.8
            
            x1_outer = math.cos(angle1) * (pondRadius + bankWidth)
            z1_outer = math.sin(angle1) * (pondRadius + bankWidth) + pondCenterZ
            x2_outer = math.cos(angle2) * (pondRadius + bankWidth)
            z2_outer = math.sin(angle2) * (pondRadius + bankWidth) + pondCenterZ
            x1_inner = math.cos(angle1) * pondRadius
            z1_inner = math.sin(angle1) * pondRadius + pondCenterZ
            x2_inner = math.cos(angle2) * pondRadius
            z2_inner = math.sin(angle2) * pondRadius + pondCenterZ
            
            openGLView.glVertex3f(x1_outer, 0.02, z1_outer)
            openGLView.glVertex3f(x2_outer, 0.02, z2_outer)
            openGLView.glVertex3f(x2_inner, 0.02, z2_inner)
            openGLView.glVertex3f(x1_inner, 0.02, z1_inner)
        openGLView.glEnd()
        
        # Pond water layers
        for layer in range(4):
            innerRadius = pondRadius * (1.0 - layer * 0.12)
            alpha = 0.5 - layer * 0.08
            y = 0.01 + layer * 0.003
            r = 0.20 + layer * 0.05
            g = 0.50 + layer * 0.05
            b = 0.75 + layer * 0.05
            
            openGLView.glColor4f(r, g, b, alpha)
            openGLView.glBegin(GL_TRIANGLE_FAN)
            openGLView.glVertex3f(pondCenterX, y, pondCenterZ)
            for i in range(37):
                angle = i * (360.0 / 36) * math.pi / 180.0
                x = math.cos(angle) * innerRadius
                z = math.sin(angle) * innerRadius + pondCenterZ
                openGLView.glVertex3f(x, y, z)
            openGLView.glEnd()
        
        # Water surface
        openGLView.glColor4f(0.25, 0.55, 0.85, 0.60)
        openGLView.glBegin(GL_TRIANGLE_FAN)
        openGLView.glVertex3f(pondCenterX, 0.018, pondCenterZ)
        for i in range(37):
            angle = i * (360.0 / 36) * math.pi / 180.0
            x = math.cos(angle) * pondRadius
            z = math.sin(angle) * pondRadius + pondCenterZ
            openGLView.glVertex3f(x, 0.018, z)
        openGLView.glEnd()
        
        # Water ripples
        rippleTime = self.m_ElephantGameState.animationTime
        openGLView.glBegin(GL_LINES)
        openGLView.glColor4f(0.90, 0.95, 1.0, 0.5)
        
        for ring in range(3):
            rippleRadius = 1.2 + ring * 0.9 + math.sin(rippleTime * 2.5 + ring) * 0.25
            for i in range(24):
                angle1 = i * (360.0 / 24) * math.pi / 180.0
                angle2 = (i + 1) * (360.0 / 24) * math.pi / 180.0
                x1 = math.cos(angle1) * rippleRadius
                z1 = math.sin(angle1) * rippleRadius + pondCenterZ
                x2 = math.cos(angle2) * rippleRadius
                z2 = math.sin(angle2) * rippleRadius + pondCenterZ
                openGLView.glVertex3f(x1, 0.020, z1)
                openGLView.glVertex3f(x2, 0.020, z2)
        openGLView.glEnd()
        
        # Lily pads
        openGLView.glColor3f(0.20, 0.50, 0.20)
        for lily in range(8):
            angle = lily * 45.0 * math.pi / 180.0
            radius = 1.5 + (lily % 3) * 0.6
            x = math.cos(angle) * radius
            z = math.sin(angle) * radius + pondCenterZ
            
            openGLView.glPushMatrix()
            openGLView.glTranslatef(x, 0.019, z)
            openGLView.glBegin(GL_TRIANGLE_FAN)
            openGLView.glVertex3f(0.0, 0.0, 0.0)
            for i in range(13):
                lilyAngle = i * (360.0 / 12) * math.pi / 180.0
                lilyX = math.cos(lilyAngle) * 0.35
                lilyZ = math.sin(lilyAngle) * 0.35
                openGLView.glVertex3f(lilyX, 0.0, lilyZ)
            openGLView.glEnd()
            openGLView.glPopMatrix()
        
        # Sun
        openGLView.glPushMatrix()
        openGLView.glTranslatef(12.0, 10.0, -15.0)
        openGLView.glColor4f(1.0, 0.95, 0.70, 0.25)
        openGLView.glBegin(GL_TRIANGLE_FAN)
        openGLView.glVertex3f(0.0, 0.0, 0.0)
        for i in range(37):
            glowAngle = i * (360.0 / 36) * math.pi / 180.0
            glowX = math.cos(glowAngle) * 2.5
            glowY = math.sin(glowAngle) * 2.5
            openGLView.glVertex3f(glowX, glowY, 0.0)
        openGLView.glEnd()
        
        openGLView.glColor3f(1.0, 0.95, 0.70)
        openGLView.glBegin(GL_TRIANGLE_FAN)
        openGLView.glVertex3f(0.0, 0.0, 0.0)
        for i in range(25):
            sunAngle = i * (360.0 / 24) * math.pi / 180.0
            sunX = math.cos(sunAngle) * 1.5
            sunY = math.sin(sunAngle) * 1.5
            openGLView.glVertex3f(sunX, sunY, 0.0)
        openGLView.glEnd()
        openGLView.glPopMatrix()
        
        # Trees around pond
        for i in range(12):
            angle = i * 30.0 * math.pi / 180.0
            distance = 6.0 + (i % 3) * 1.0
            x = math.cos(angle) * distance
            z = math.sin(angle) * distance + pondCenterZ
            self.DrawAcaciaTree(x, z)
        
        # Additional trees in distance
        self.DrawAcaciaTree(5.0, -3.0)
        self.DrawAcaciaTree(-5.0, -3.0)
        self.DrawAcaciaTree(7.0, -1.0)
        self.DrawAcaciaTree(-7.0, -1.0)
        self.DrawAcaciaTree(0.0, -8.0)
        
        # Clouds
        cloudTime = self.m_ElephantGameState.animationTime * 0.08
        self.DrawCloud(-10.0 + math.sin(cloudTime) * 3.0, 8.0, -12.0, 1.3)
        self.DrawCloud(5.0 + math.sin(cloudTime * 0.9 + 1.0) * 2.0, 7.0, 5.0, 1.0)
        self.DrawCloud(-5.0 + math.sin(cloudTime * 1.1 + 2.0) * 1.5, 9.0, 10.0, 1.5)
        
        openGLView.glDisable(0x0BE2)

    def DrawAcaciaTree(self, x, z):
        openGLView = OpenGLView()
        openGLView.glPushMatrix()
        openGLView.glTranslatef(x, 0.0, z)
        
        # Trunk
        openGLView.glColor3f(0.45, 0.35, 0.25)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, 1.2, 0.0)
        openGLView.glScalef(0.22, 2.5, 0.22)
        self.DrawEllipsoid(1.0, 10, 8)
        openGLView.glPopMatrix()
        
        # Branches
        openGLView.glColor3f(0.4, 0.3, 0.2)
        for branch in range(3):
            angle = branch * 120.0 * math.pi / 180.0
            branchHeight = 2.5 + (branch % 2) * 0.3
            branchLength = 1.6 + random.randint(0, 5) * 0.1
            
            openGLView.glPushMatrix()
            openGLView.glTranslatef(0.0, branchHeight, 0.0)
            openGLView.glRotatef(angle * 180.0 / math.pi, 0, 1, 0)
            openGLView.glRotatef(20.0, 1, 0, 0)
            openGLView.glScalef(0.10, 0.10, branchLength)
            self.DrawEllipsoid(1.0, 8, 6)
            openGLView.glPopMatrix()
        
        # Foliage
        openGLView.glColor3f(0.25, 0.35, 0.2)
        openGLView.glPushMatrix()
        openGLView.glTranslatef(0.0, 3.2, 0.0)
        openGLView.glScalef(1.8, 0.6, 1.8)
        self.DrawEllipsoid(1.0, 12, 10)
        openGLView.glPopMatrix()
        
        openGLView.glPopMatrix()

    def DrawCloud(self, x, y, z, scale):
        openGLView = OpenGLView()
        openGLView.glPushMatrix()
        openGLView.glTranslatef(x, y, z)
        openGLView.glScalef(scale, scale * 0.5, scale)
        
        openGLView.glColor4f(1.0, 1.0, 1.0, 0.8)
        
        cloudParts = [
            (0.0, 0.0, 0.0),
            (0.5, 0.2, 0.2),
            (-0.4, 0.1, 0.3),
            (0.3, -0.1, -0.4),
            (-0.3, -0.2, -0.2)
        ]
        
        for part in cloudParts:
            openGLView.glPushMatrix()
            openGLView.glTranslatef(part[0], part[1], part[2])
            openGLView.glScalef(0.7, 0.5, 0.7)
            self.DrawEllipsoid(1.0, 12, 10)
            openGLView.glPopMatrix()
        
        openGLView.glPopMatrix()

    # ==================== GEOMETRIC PRIMITIVE DRAWING METHODS ====================
    
    def DrawSphere(self, radius, slices, stacks):
        openGLView = OpenGLView()
        PI = math.pi
        
        for i in range(stacks):
            phi1 = (i * PI) / stacks
            phi2 = ((i + 1) * PI) / stacks
            
            openGLView.glBegin(GL_QUAD_STRIP)
            for j in range(slices + 1):
                theta = (2.0 * j * PI) / slices
                x1 = radius * math.sin(phi1) * math.cos(theta)
                y1 = radius * math.cos(phi1)
                z1 = radius * math.sin(phi1) * math.sin(theta)
                
                x2 = radius * math.sin(phi2) * math.cos(theta)
                y2 = radius * math.cos(phi2)
                z2 = radius * math.sin(phi2) * math.sin(theta)
                
                openGLView.glVertex3f(x1, y1, z1)
                openGLView.glVertex3f(x2, y2, z2)
            openGLView.glEnd()

    def DrawCylinder(self, radius, height):
        openGLView = OpenGLView()
        slices = 16
        PI = math.pi
        
        openGLView.glBegin(GL_QUAD_STRIP)
        for i in range(slices + 1):
            angle = 2.0 * PI * i / slices
            x = math.cos(angle) * radius
            z = math.sin(angle) * radius
            openGLView.glVertex3f(x, height, z)
            openGLView.glVertex3f(x, 0.0, z)
        openGLView.glEnd()
        
        openGLView.glBegin(GL_TRIANGLE_FAN)
        openGLView.glVertex3f(0.0, height, 0.0)
        for i in range(slices + 1):
            angle = 2.0 * PI * i / slices
            x = math.cos(angle) * radius
            z = math.sin(angle) * radius
            openGLView.glVertex3f(x, height, z)
        openGLView.glEnd()
        
        openGLView.glBegin(GL_TRIANGLE_FAN)
        openGLView.glVertex3f(0.0, 0.0, 0.0)
        for i in range(slices, -1, -1):
            angle = 2.0 * PI * i / slices
            x = math.cos(angle) * radius
            z = math.sin(angle) * radius
            openGLView.glVertex3f(x, 0.0, z)
        openGLView.glEnd()

    def DrawEllipsoid(self, radius, slices, stacks):
        openGLView = OpenGLView()
        PI = math.pi
        
        for i in range(stacks):
            phi1 = (i * PI) / stacks
            phi2 = ((i + 1) * PI) / stacks
            
            openGLView.glBegin(GL_QUAD_STRIP)
            for j in range(slices + 1):
                theta = (2.0 * j * PI) / slices
                x1 = radius * math.sin(phi1) * math.cos(theta)
                y1 = radius * math.cos(phi1)
                z1 = radius * math.sin(phi1) * math.sin(theta)
                
                x2 = radius * math.sin(phi2) * math.cos(theta)
                y2 = radius * math.cos(phi2)
                z2 = radius * math.sin(phi2) * math.sin(theta)
                
                openGLView.glVertex3f(x1, y1, z1)
                openGLView.glVertex3f(x2, y2, z2)
            openGLView.glEnd()