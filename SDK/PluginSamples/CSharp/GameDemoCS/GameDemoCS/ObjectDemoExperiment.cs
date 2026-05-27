using EurekaSim.Net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameDemoCS
{
    public class ExperimentInfo
    {
        public string RootText { get; set; }
        public string ExperimentGroup { get; set; }
        public string ExperimentName { get; set; }
        public string ObjectType { get; set; }
        public int Colour { get; set; }
        public string SimulationPattern { get; set; }
        public long SimulationInterval { get; set; }
    }

    static class Constants
    {
        public const int TVE_EXPAND = 0x0002;

        public const int FALSE = 0;
        public const int TRUE = 1;
        public const uint GL_POINTS = 0x0000;
        public const uint GL_LINES = 0x0001;
        public const uint GL_LINE_LOOP = 0x0002;
        public const uint GL_LINE_STRIP = 0x0003;
        public const uint GL_TRIANGLES = 0x0004;
        public const uint GL_TRIANGLE_STRIP = 0x0005;
        public const uint GL_TRIANGLE_FAN = 0x0006;
        public const uint GL_QUADS = 0x0007;
        public const uint GL_QUAD_STRIP = 0x0008;
        public const uint GL_POLYGON = 0x0009;

        public const string OBJECT_3D_TREE_ROOT_TITLE = "3D Object Demo";
        public const string OBJECT_3D_TREE_LEAF_PATTERN_TITLE = "Object Pattern Demo";

        public const string MECHANICS_TREE_ROOT_TITLE = "Physics";
        public const string MECHANICS_TREE_SIMPLE_PENDULUM_TITLE = "Simple Pendulum";
        public const string MECHANICS_TREE_PROJECTILE_MOTION_TITLE = "Projectile Motion";
        public const string MECHANICS_TREE_PLANETORY_MOTION_TITLE = "Planetory Motion";

        public const string OBJECT_PROPERTIES_TITLE = "Select Object | Properties";
        public const string OBJECT_TYPE_TITLE = "Select The Object Type";
        public const string OBJECT_COLOR_TITLE = "Select Background Color";
        public const string OBJECT_SIMULATION_PATTERN_TITLE = "Simulation Pattern";
        public const string OBJECT_SIMULATION_INTERVAL_TITLE = "Simulation Interval";

        // Updated to include new object types
        public const string OBJECT_TYPES = "Cube,Ball,Pyramid,Aeroplane,Clock,FootballPenalty,Elephant";
        public const string OBJECT_TYPE_CUBE = "Cube";
        public const string OBJECT_TYPE_BALL = "Ball";
        public const string OBJECT_TYPE_PYRAMID = "Pyramid";
        public const string OBJECT_TYPE_AEROPLANE = "Aeroplane";
        public const string OBJECT_TYPE_CLOCK = "Clock";
        public const string OBJECT_TYPE_FOOTBALLPENALTY = "FootballPenalty";
        public const string OBJECT_TYPE_ELEPHANT = "Elephant";

        // Updated to include new simulation patterns
        public const string OBJECT_PATTERN_TYPES = "Rotate,Random Movement,Penalty Kick View,Elephant Animation";
        public const string OBJECT_PATTERN_TYPE_ROTATE = "Rotate";
        public const string OBJECT_PATTERN_TYPE_RANDOM = "Random Movement";
        public const string OBJECT_PATTERN_TYPE_PENALTY_KICK = "Penalty Kick View";
        public const string OBJECT_PATTERN_TYPE_ELEPHANT = "Elephant Animation";


        public const string CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES = "Experiment Group 1 Properties";
        public const string CS_SAMPLE_DOC_SETTINGS_KEY = "Cs.Sample.Addin.Settings";
        public const string CS_SAMPLE_MAIN_EXPERIMENT_NAME = "GameDemoCS Experiment Simulation Demo";

        // Football game constants
        public const string FOOTBALL_PENALTY_TITLE = "Football Penalty Game";
        public const string ELEPHANT_TITLE = "Elephant Animation";

        public static int BOOL(bool bValue)
        {
            int res = bValue ? 1 : 0;
            return res;
        }

        public static ulong HexConverter(Color color)
        {
            string strCOLORREF = "0x00" + color.B.ToString("X2") +
                color.G.ToString("X2") + color.R.ToString("X2");
            uint iCOLORREF;
            try
            {
                iCOLORREF = Convert.ToUInt32(strCOLORREF, 16);
            }
            catch (Exception)
            {
                iCOLORREF = Convert.ToUInt32(0x00ff0000);
            }

            return iCOLORREF;
        }
    }

    public class CObjectPattern
    {
        public string m_strObjectType;
        public Color m_Color = new Color();
        public string m_strSimulationPattern;
        public long m_lSimulationInterval;

        public CObjectPattern()
        {
            m_strObjectType = "FootballPenalty";
            m_Color = Color.FromArgb(0, 0, 255);
            m_strSimulationPattern = "Penalty Kick View";
            m_lSimulationInterval = 100;
        }

        public ExperimentInfo Serialize()
        {
            ExperimentInfo info = new ExperimentInfo();
            info.ObjectType = m_strObjectType;
            info.Colour = m_Color.ToArgb();
            info.SimulationPattern = m_strSimulationPattern;
            info.SimulationInterval = m_lSimulationInterval;
            return info;
        }

        public void DeSerialize(ExperimentInfo info)
        {
            m_strObjectType = info.ObjectType;
            m_Color = Color.FromArgb(info.Colour);
            m_strSimulationPattern = info.SimulationPattern;
            m_lSimulationInterval = info.SimulationInterval;
        }

        public void OnPropertyChanged(string GroupName, string PropertyName, string PropertyValue)
        {
            if (GroupName != Constants.OBJECT_PROPERTIES_TITLE)
            {
                return;
            }

            if (PropertyName == Constants.OBJECT_TYPE_TITLE)
            {
                m_strObjectType = PropertyValue;
            }
            else if (PropertyName == Constants.OBJECT_COLOR_TITLE)
            {
                m_Color = Color.FromArgb(Convert.ToInt32(PropertyValue));
            }
            else if (PropertyName == Constants.OBJECT_SIMULATION_PATTERN_TITLE)
            {
                m_strSimulationPattern = PropertyValue;
            }
            else if (PropertyName == Constants.OBJECT_SIMULATION_INTERVAL_TITLE)
            {
                m_lSimulationInterval = Convert.ToInt32(PropertyValue);
            }
        }
    }

    public class CGraphPoints
    {
        public float m_Angle;
        public float m_x;
        public float m_y;
        public float m_z;

        public CGraphPoints()
        {
            m_Angle = 0.0F;
            m_x = 0.0F;
            m_y = 0.0F;
            m_z = 0.0F;
        }
    }

    public class CFootballPenaltyGameState
    {
        public float ballX = 0.0f;
        public float ballY = 0.2f;
        public float ballZ = -1.6f;
        public float ballSpeedX = 0.0f;
        public float ballSpeedY = 0.0f;
        public float ballSpeedZ = 0.0f;
        public float kickerAngle = 0.0f;
        public float kickPower = 0.0f;
        public float goalkeeperX = 0.0f;
        public float goalkeeperZ = 4.0f;
        public float goalkeeperSpeed = 0.0f;
        public float goalkeeperTimer = 0.0f;
        public bool isBallMoving = false;
        public bool isKicking = false;
        public int score = 0;
        public int attempts = 0;
        public float gameTime = 0.0f;
        public bool goalkeeperDiveLeft = false;
        public bool goalkeeperDiveRight = false;
        public bool isGoalScored = false;
        public bool isShotSaved = false;
        public bool isShotMissed = false;
        public bool isGameActive = true;
        public float kickAnimationTime = 0.0f;

        public void Reset()
        {
            ballX = 0.0f;
            ballY = 0.2f;
            ballZ = -1.6f;
            ballSpeedX = 0.0f;
            ballSpeedY = 0.0f;
            ballSpeedZ = 0.0f;
            kickerAngle = 0.0f;
            kickPower = 0.0f;
            goalkeeperX = 0.0f;
            goalkeeperZ = 4.0f;
            goalkeeperSpeed = 0.0f;
            goalkeeperTimer = 0.0f;
            isBallMoving = false;
            isKicking = false;
            score = 0;
            attempts = 0;
            gameTime = 0.0f;
            goalkeeperDiveLeft = false;
            goalkeeperDiveRight = false;
            isGoalScored = false;
            isShotSaved = false;
            isShotMissed = false;
            isGameActive = true;
            kickAnimationTime = 0.0f;
        }
    }

    public class CElephantGameState
    {
        public float positionX = 0.0f;
        public float positionY = 0.0f;
        public float positionZ = 0.0f;
        public float rotationY = 0.0f;
        public float animationTime = 0.0f;
        public int currentState = 0;
        public float walkSpeed = 0.015f;
        public float runSpeed = 0.03f;
        public float scale = 1.2f;
        public bool isTrumpeting = false;
        public float trumpetProgress = 0.0f;
        public float headBob = 0.0f;
        public float trunkSwing = 0.0f;
        public float earFlap = 0.0f;

        public void Reset()
        {
            positionX = 0.0f;
            positionY = 0.0f;
            positionZ = 0.0f;
            rotationY = 0.0f;
            animationTime = 0.0f;
            currentState = 0;
            walkSpeed = 0.015f;
            runSpeed = 0.03f;
            scale = 1.2f;
            isTrumpeting = false;
            trumpetProgress = 0.0f;
            headBob = 0.0f;
            trunkSwing = 0.0f;
            earFlap = 0.0f;
        }
    }

    public class ObjectDemoExperiment
    {
        private AddinSimulationManager m_pManager;
        private List<CGraphPoints> m_PlotInfoArray = new List<CGraphPoints>();
        public CObjectPattern m_ObjectPattern = new CObjectPattern();
        private CFootballPenaltyGameState m_FootballGameState = new CFootballPenaltyGameState();
        private CElephantGameState m_ElephantGameState = new CElephantGameState();
        private bool m_bPenaltyKickView = false;
        private bool m_bShowAimingReticle = true;
        private bool m_bShowPowerMeter = true;
        private bool m_bKeyboardActive = false;

        public ObjectDemoExperiment(AddinSimulationManager pManager)
        {
            m_pManager = pManager;
            ResetFootballPenalty();
            m_ElephantGameState.Reset();
            m_ElephantGameState.scale = 0.8f;
            m_bPenaltyKickView = false;
            m_bShowAimingReticle = true;
            m_bShowPowerMeter = true;
            m_bKeyboardActive = false;
        }

        ~ObjectDemoExperiment()
        {
            m_pManager = null;
            m_PlotInfoArray = null;
            m_ObjectPattern = null;
            GC.Collect();
        }

        #region Tree View and Property Management
        public void LoadAllExperiments()
        {
            int SessionID = (int)m_pManager.m_pAddin.m_lSessionID;
            ExperimentTreeView objExperimentTreeView = new ExperimentTreeView();

            try
            {
                objExperimentTreeView.DeleteAllExperiments(SessionID);
                objExperimentTreeView.SetRootNodeName(Constants.CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES, 1);
                objExperimentTreeView.AddExperiment(SessionID, Constants.OBJECT_3D_TREE_ROOT_TITLE, Constants.OBJECT_3D_TREE_LEAF_PATTERN_TITLE);
                objExperimentTreeView.Refresh();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
        }

        public void OnTreeNodeSelect(string ExperimentGroup, string ExperimentName)
        {
            OnReloadExperiment(ExperimentGroup, ExperimentName);
        }

        public void OnTreeNodeDblClick(string ExperimentGroup, string ExperimentName)
        {
            if (ExperimentGroup == Constants.OBJECT_3D_TREE_ROOT_TITLE && ExperimentName == Constants.OBJECT_3D_TREE_LEAF_PATTERN_TITLE)
            {
                ShowObjectProperties();
            }
            else
            {
                m_pManager.ResetPropertyGrid();
            }
        }

        public void OnReloadExperiment(string ExperimentGroup, string ExperimentName)
        {
            if (ExperimentGroup == Constants.OBJECT_3D_TREE_ROOT_TITLE)
            {
                DrawObject(ExperimentName);
            }
        }

        public void ShowObjectProperties()
        {
            PropertyWindow objPropertyWindow = new PropertyWindow();
            string strGroupName = string.Empty;

            try
            {
                objPropertyWindow.RemoveAll();
                strGroupName = Constants.OBJECT_PROPERTIES_TITLE;
                objPropertyWindow.AddPropertyGroup(strGroupName);

                objPropertyWindow.AddPropertyItemsAsString(strGroupName, Constants.OBJECT_TYPE_TITLE,
                    Constants.OBJECT_TYPES, m_ObjectPattern.m_strObjectType, "Select the Object from the List", Constants.FALSE);

                try
                {
                    objPropertyWindow.AddColorPropertyItem(strGroupName, Constants.OBJECT_COLOR_TITLE,
                        Constants.HexConverter(m_ObjectPattern.m_Color), "Select the Color");
                }
                catch (Exception) { }

                objPropertyWindow.AddPropertyItemsAsString(strGroupName, Constants.OBJECT_SIMULATION_PATTERN_TITLE,
                    Constants.OBJECT_PATTERN_TYPES, m_ObjectPattern.m_strSimulationPattern, "Select the Simulation Pattern", Constants.FALSE);

                string strInterval = m_ObjectPattern.m_lSimulationInterval.ToString();
                objPropertyWindow.AddPropertyItemAsString(strGroupName, Constants.OBJECT_SIMULATION_INTERVAL_TITLE,
                    strInterval, "Simulation Interval In Milli Seconds");

                objPropertyWindow.EnableHeaderCtrl(Constants.FALSE);
                objPropertyWindow.EnableDescriptionArea(Constants.TRUE);
                objPropertyWindow.SetVSDotNetLook(Constants.TRUE);
                objPropertyWindow.MarkModifiedProperties(Constants.TRUE, Constants.TRUE);
            }
            catch (Exception) { }
        }

        public ExperimentInfo Serialize()
        {
            return m_ObjectPattern.Serialize();
        }

        public void DeSerialize(ExperimentInfo info)
        {
            m_ObjectPattern.DeSerialize(info);
        }

        public void OnPropertyChanged(string GroupName, string PropertyName, string PropertyValue)
        {
            if (GroupName == Constants.OBJECT_PROPERTIES_TITLE)
            {
                m_ObjectPattern.OnPropertyChanged(GroupName, PropertyName, PropertyValue);
            }
            DrawScene();
        }

        public void DrawScene()
        {
            OnReloadExperiment(m_pManager.m_strExperimentGroup, m_pManager.m_strExperimentName);
        }
        #endregion

        #region Object Drawing and Simulation
        public void DrawObject(string ExperimentName)
        {
            if (m_ObjectPattern.m_strSimulationPattern == Constants.OBJECT_PATTERN_TYPE_PENALTY_KICK)
            {
                m_bPenaltyKickView = true;
                DrawPenaltyKickView();
            }
            else
            {
                m_bPenaltyKickView = false;
                if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_FOOTBALLPENALTY)
                {
                    DrawFootballPenalty();
                }
                else if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_ELEPHANT)
                {
                    DrawElephant();
                }
                else if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_CUBE)
                {
                    DrawCube();
                }
                else if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_BALL)
                {
                    DrawBall();
                }
                else if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_PYRAMID)
                {
                    DrawPyramid();
                }
                else if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_AEROPLANE)
                {
                    DrawAeroplane();
                }
                else if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_CLOCK)
                {
                    DrawClock();
                }
            }
        }

        public void StartSimulation(string ExperimentGroup, string ExperimentName)
        {
            if (ExperimentGroup == Constants.OBJECT_3D_TREE_ROOT_TITLE &&
                ExperimentName == Constants.OBJECT_3D_TREE_LEAF_PATTERN_TITLE)
            {
                if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_ELEPHANT)
                {
                    m_ElephantGameState.positionX = 0.0f;
                    m_ElephantGameState.positionY = 0.0f;
                    m_ElephantGameState.positionZ = 0.0f;
                    m_ElephantGameState.currentState = 0;
                    m_ElephantGameState.animationTime = 0.0f;
                }
                StartObjectSimulation();
            }
        }

        public void StartObjectSimulation()
        {
            m_pManager.SetSimulationStatus(Constants.TRUE);
            ApplicationView applicationView = new ApplicationView();
            float Angle = 0.0f, x = 0.0f, y = 0.0f, z = 0.0f;
            int i = 0;
            Random rnd = new Random();

            while (m_pManager.m_bSimulationActive)
            {
                applicationView.BeginGraphicsCommands();

                if (m_ObjectPattern.m_strSimulationPattern == Constants.OBJECT_PATTERN_TYPE_ROTATE)
                {
                    x = 0.1f; y = 1.0f; z = 0.1f;
                }
                else if (m_ObjectPattern.m_strSimulationPattern == Constants.OBJECT_PATTERN_TYPE_PENALTY_KICK)
                {
                    UpdateFootballPenalty();
                    x = 0; y = 0; z = 0;
                }
                else if (m_ObjectPattern.m_strSimulationPattern == Constants.OBJECT_PATTERN_TYPE_RANDOM)
                {
                    switch (i)
                    {
                        case 0:
                            x = 1.0f; y = 0.1f; z = 0.1f;
                            break;
                        case 1:
                            x = 0.1f; y = 1.0f; z = 0.1f;
                            break;
                        case 2:
                            x = 0.1f; y = 0.1f; z = 1.0f;
                            break;
                    }
                    i = rnd.Next(0, 3);
                }

                if (m_ObjectPattern.m_strObjectType != Constants.OBJECT_TYPE_ELEPHANT)
                {
                    if (!m_pManager.m_b3DMode && m_ObjectPattern.m_strSimulationPattern != Constants.OBJECT_PATTERN_TYPE_PENALTY_KICK)
                    {
                        x = 0;
                        y = 0;
                    }
                    if (m_ObjectPattern.m_strSimulationPattern != Constants.OBJECT_PATTERN_TYPE_PENALTY_KICK)
                    {
                        applicationView.RotateObject(Angle, x, y, z);
                    }
                }
                else
                {
                    m_ElephantGameState.animationTime += 0.016f;
                    m_ElephantGameState.trunkSwing = (float)Math.Sin(m_ElephantGameState.animationTime * 2.0f) * 0.5f;
                    m_ElephantGameState.earFlap = (float)Math.Sin(m_ElephantGameState.animationTime * 1.5f) * 0.3f;
                    m_ElephantGameState.headBob = (float)Math.Sin(m_ElephantGameState.animationTime * 0.5f) * 0.005f;
                    DrawElephant();
                }

                applicationView.EndGraphicsCommands();
                applicationView.Refresh();
                OnNextSimulationPoint(Angle, x, y, z);

                Angle = Angle + 5;
                if (Angle > 360)
                {
                    Angle = 0;
                }

                Thread.Sleep((int)m_ObjectPattern.m_lSimulationInterval);
            }
        }

        public void OnNextSimulationPoint(float Angle, float x, float y, float z)
        {
            string strStatus = string.Format("Simulation Points (Angle:{0},X:{1},Y:{2},Z:{3})\n",
                                            Angle, x, y, z);

            if (m_pManager.m_bShowExperimentalParamaters)
            {
                m_pManager.AddOperationStatus(strStatus);
            }

            if (m_pManager.m_bLogSimulationResultsToCSVFile)
            {
                string strLog = string.Format("{0},{1},{2},{3}\n", Angle, x, y, z);
                m_pManager.LogSimulationPoint(strLog);
            }

            if (m_pManager.m_bDisplayRealTimeGraph)
            {
                PlotSimulationPoint(Angle, x, y, z);
            }
        }

        public void PlotSimulationPoint(float Angle, float x, float y, float z)
        {
            CGraphPoints pPoint = new CGraphPoints();
            pPoint.m_Angle = Angle;
            pPoint.m_x = x;
            pPoint.m_y = y;
            pPoint.m_z = z;

            m_PlotInfoArray.Add(pPoint);

            string strStatus = string.Format("Plot Data Points Count ={0}", m_PlotInfoArray.Count);
            m_pManager.SetStatusBarMessage(strStatus);

            DisplayObjectDemoGraph();
        }

        public void InitializeSimulationGraph(string ExperimentName)
        {
            m_PlotInfoArray.Clear();

            ApplicationChart applicationChart = new ApplicationChart();
            try
            {
                applicationChart.DeleteAllCharts();
                applicationChart.Initialize2dChart(3);

                applicationChart.Set2dGraphInfo(0, "Angle Vs X", "Angle(Degree)", "X", Constants.TRUE);
                applicationChart.Set2dAxisRange(0, 0, 0, 365); // Bottom axis
                applicationChart.Set2dAxisRange(0, 1, 0, 2);   // Left axis

                applicationChart.Set2dGraphInfo(1, "Angle Vs Y", "Angle(Degree)", "Y", Constants.TRUE);
                applicationChart.Set2dAxisRange(1, 0, 0, 365);
                applicationChart.Set2dAxisRange(1, 1, 0, 2);

                applicationChart.Set2dGraphInfo(2, "Angle Vs Z", "Angle(Degree)", "Z", Constants.TRUE);
                applicationChart.Set2dAxisRange(2, 0, 0, 365);
                applicationChart.Set2dAxisRange(2, 1, 0, 2);

                applicationChart.ResizeChartWindow();
            }
            catch (Exception) { }
        }

        public void DisplayObjectDemoGraph()
        {
            int iArraySize = (int)m_PlotInfoArray.Count;

            if (iArraySize < 2)
            {
                return;
            }

            int[] arraySize = { iArraySize, 2 };
            int[] lowerBounds = { 1, 1 };
            Array saX = Array.CreateInstance(typeof(double), arraySize, lowerBounds);
            Array saY = Array.CreateInstance(typeof(double), arraySize, lowerBounds);
            Array saZ = Array.CreateInstance(typeof(double), arraySize, lowerBounds);

            int[] index = { 0, 0 };
            int i = 0;
            foreach (CGraphPoints pInfo in m_PlotInfoArray)
            {
                index[0] = i + 1;
                index[1] = 1;
                double pValue = pInfo.m_Angle;
                saX.SetValue(pValue, index[0], index[1]);
                saY.SetValue(pValue, index[0], index[1]);
                saZ.SetValue(pValue, index[0], index[1]);

                index[1] = 2;
                pValue = pInfo.m_x;
                saX.SetValue(pValue, index[0], index[1]);

                pValue = pInfo.m_y;
                saY.SetValue(pValue, index[0], index[1]);

                pValue = pInfo.m_z;
                saZ.SetValue(pValue, index[0], index[1]);
                i = i + 1;
            }

            if (iArraySize % 5 == 0)
            {
                ApplicationChart applicationChart = new ApplicationChart();
                try
                {
                    applicationChart.Set2dChartData(0, saX);
                    applicationChart.Set2dChartData(1, saY);
                    applicationChart.Set2dChartData(2, saZ);
                }
                catch { }
            }
        }
        #endregion

        #region Original Basic Objects Drawing
        public void DrawCube()
        {
            ApplicationView applicationView = new ApplicationView();
            const float radius = 0.34f;

            applicationView.InitializeEnvironment(1);
            applicationView.BeginGraphicsCommands();
            applicationView.SetBkgColor(m_ObjectPattern.m_Color.R / (float)255.0,
                m_ObjectPattern.m_Color.G / (float)255.0, m_ObjectPattern.m_Color.B / (float)255.0, (float)1.0);

            try
            {
                applicationView.StartNewDisplayList();
            }
            catch (Exception)
            {
                return;
            }

            OpenGLView openGLView = new OpenGLView();
            openGLView.glBegin(Constants.GL_QUAD_STRIP);
            openGLView.glColor3f(1.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(-0.3f, 0.3f, 0.3f);
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(-0.3f, -0.3f, 0.3f);
            openGLView.glColor3f(1.0f, 1.0f, 1.0f);
            openGLView.glVertex3f(0.3f, 0.3f, 0.3f);
            openGLView.glColor3f(1.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(0.3f, -0.3f, 0.3f);
            openGLView.glColor3f(0.0f, 1.0f, 1.0f);
            openGLView.glVertex3f(0.3f, 0.3f, -0.3f);
            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(0.3f, -0.3f, -0.3f);
            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(-0.3f, 0.3f, -0.3f);
            openGLView.glColor3f(0.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(-0.3f, -0.3f, -0.3f);
            openGLView.glColor3f(1.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(-0.3f, 0.3f, 0.3f);
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(-0.3f, -0.3f, 0.3f);
            openGLView.glEnd();

            openGLView.glBegin(Constants.GL_QUADS);
            openGLView.glColor3f(1.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(-0.3f, 0.3f, 0.3f);
            openGLView.glColor3f(1.0f, 1.0f, 1.0f);
            openGLView.glVertex3f(0.3f, 0.3f, 0.3f);
            openGLView.glColor3f(0.0f, 1.0f, 1.0f);
            openGLView.glVertex3f(0.3f, 0.3f, -0.3f);
            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(-0.3f, 0.3f, -0.3f);
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(-0.3f, -0.3f, 0.3f);
            openGLView.glColor3f(0.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(-0.3f, -0.3f, -0.3f);
            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(0.3f, -0.3f, -0.3f);
            openGLView.glColor3f(1.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(0.3f, -0.3f, 0.3f);
            openGLView.glEnd();

            openGLView.glColor3f(1.0f, 1.0f, 1.0f);
            openGLView.glRasterPos3f(-radius, radius, radius);
            openGLView.glRasterPos3f(-radius, -radius, radius);
            openGLView.glRasterPos3f(radius, radius, radius);
            openGLView.glRasterPos3f(radius, -radius, radius);
            openGLView.glRasterPos3f(radius, radius, -radius);
            openGLView.glRasterPos3f(radius, -radius, -radius);
            openGLView.glRasterPos3f(-radius, radius, -radius);
            openGLView.glRasterPos3f(-radius, -radius, -radius);

            applicationView.EndNewDisplayList();
            applicationView.EndGraphicsCommands();
            applicationView.Refresh();
        }

        public void DrawBall()
        {
            ApplicationView applicationView = new ApplicationView();
            applicationView.InitializeEnvironment(Constants.TRUE);
            applicationView.BeginGraphicsCommands();
            applicationView.SetBkgColor(m_ObjectPattern.m_Color.R / (float)255,
                                        m_ObjectPattern.m_Color.G / (float)255,
                                        m_ObjectPattern.m_Color.B / (float)255, 1);

            int SECTIONS = 25;
            double RADIUS = 1.0;
            try
            {
                applicationView.StartNewDisplayList();
            }
            catch (Exception)
            {
                return;
            }

            applicationView.SetColorf(0.0f, 0.0f, 1.0f);
            applicationView.DrawSphere(RADIUS, SECTIONS, SECTIONS);
            applicationView.SetColorf(1.0f, 1.0f, 1.0f);
            applicationView.DrawSphere(RADIUS / 1.5, SECTIONS, SECTIONS);

            applicationView.EndNewDisplayList();
            applicationView.EndGraphicsCommands();
            applicationView.Refresh();
        }

        public void DrawPyramid()
        {
            ApplicationView applicationView = new ApplicationView();
            applicationView.ResetScene();
            applicationView.InitializeEnvironment(Constants.TRUE);
            applicationView.BeginGraphicsCommands();
            applicationView.SetBkgColor(m_ObjectPattern.m_Color.R / (float)255,
                                        m_ObjectPattern.m_Color.G / (float)255,
                                        m_ObjectPattern.m_Color.B / (float)255, 1);

            try
            {
                applicationView.StartNewDisplayList();
            }
            catch (Exception)
            {
                return;
            }

            OpenGLView openGLView = new OpenGLView();
            openGLView.glTranslatef(0.01f, 0.0f, 0.01f);
            openGLView.glColor3f(0.0f, 0.4f, 0.8f);
            openGLView.glBegin(Constants.GL_TRIANGLES);

            // Front
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(0.0f, 1.0f, 0.0f);
            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, 1.0f);
            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(1.0f, -1.0f, 1.0f);

            // Right
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(0.0f, 1.0f, 0.0f);
            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(1.0f, -1.0f, 1.0f);
            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(1.0f, -1.0f, -1.0f);

            // Back
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(0.0f, 1.0f, 0.0f);
            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(1.0f, -1.0f, -1.0f);
            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, -1.0f);

            // Left
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(0.0f, 1.0f, 0.0f);
            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, -1.0f);
            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, 1.0f);

            // Bottom 1
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, 1.0f);
            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(1.0f, -1.0f, 1.0f);
            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, -1.0f);

            // Bottom 2
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, -1.0f);
            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(1.0f, -1.0f, -1.0f);
            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(1.0f, -1.0f, 1.0f);
            openGLView.glEnd();

            applicationView.EndNewDisplayList();
            applicationView.EndGraphicsCommands();
            applicationView.Refresh();
        }

        public void DrawAeroplane()
        {
            ApplicationView applicationView = new ApplicationView();
            applicationView.InitializeEnvironment(Constants.TRUE);
            applicationView.BeginGraphicsCommands();
            applicationView.SetBkgColor(m_ObjectPattern.m_Color.R / (float)255,
                                        m_ObjectPattern.m_Color.G / (float)255,
                                        m_ObjectPattern.m_Color.B / (float)255, 1);

            try
            {
                applicationView.StartNewDisplayList();
            }
            catch (Exception)
            {
                return;
            }

            OpenGLView openGLView = new OpenGLView();
            openGLView.glTranslatef(0.01f, 0.0f, 0.01f);
            openGLView.glColor3f(0.0f, 0.4f, 0.8f);
            openGLView.glBegin(Constants.GL_TRIANGLES);
            openGLView.glVertex3f(0.0f, 0.0f, 0.001f);
            openGLView.glVertex3f(0.0f, -0.5f, 1.0f);
            openGLView.glVertex3f(0.0f, 1.0f, 0.001f);
            openGLView.glEnd();

            openGLView.glColor3f(0.0f, 0.3f, 0.7f);
            openGLView.glBegin(Constants.GL_TRIANGLE_STRIP);
            openGLView.glVertex3f(1.0f, -0.5f, 0.0f);
            openGLView.glVertex3f(0.0f, 0.0f, 0.2f);
            openGLView.glVertex3f(0.0f, 2.0f, 0.0f);
            openGLView.glVertex3f(-1.0f, -0.5f, 0.0f);
            openGLView.glEnd();

            applicationView.EndNewDisplayList();
            applicationView.EndGraphicsCommands();
            applicationView.Refresh();
        }

        public void DrawClock()
        {
            ApplicationView applicationView = new ApplicationView();
            applicationView.InitializeEnvironment(Constants.TRUE);
            applicationView.BeginGraphicsCommands();
            applicationView.SetBkgColor(m_ObjectPattern.m_Color.R / (float)255,
                                        m_ObjectPattern.m_Color.G / (float)255,
                                        m_ObjectPattern.m_Color.B / (float)255, 1);

            try
            {
                applicationView.StartNewDisplayList();
            }
            catch (Exception)
            {
                return;
            }

            float x1 = 0.0f, y1 = 0.0f;
            float segments = 100;
            float radius = 1.0f;

            applicationView.SetLineWidth(4);
            applicationView.SetColorf(1, 0, 0);
            DrawCircle(segments, radius, x1, y1);

            applicationView.SetColorf(1, 1, 0);
            applicationView.SetLineWidth(2);
            applicationView.BeginDraw((int)Constants.GL_LINES);
            applicationView.Set2DVertexf(x1, y1);
            applicationView.Set2DVertexf(x1, (float)((radius / 3.0) * 2.0));
            applicationView.EndDraw();

            applicationView.SetColorf(1, 0, 0);
            applicationView.SetLineWidth(2);
            applicationView.BeginDraw((int)Constants.GL_LINES);
            applicationView.Set2DVertexf(x1, y1);
            applicationView.Set2DVertexf((float)(radius / 3.0), (float)(radius / 3.0));
            applicationView.EndDraw();

            applicationView.EndNewDisplayList();
            applicationView.EndGraphicsCommands();
            applicationView.Refresh();
        }

        public void DrawCircle(float segments, float radius, float sx, float sy)
        {
            OpenGLView openGLView = new OpenGLView();
            openGLView.glBegin(Constants.GL_LINE_LOOP);
            for (int i = 0; i < segments; i++)
            {
                float theta = (float)(2.0 * 3.142 * (float)i / (float)segments);
                float x = (float)(radius * Math.Cos(theta));
                float y = (float)(radius * Math.Sin(theta));
                openGLView.glVertex2f(x + sx, y + sy);
            }
            openGLView.glEnd();
        }
        #endregion

        #region Football Penalty Game
        public void ResetFootballPenalty()
        {
            m_FootballGameState.Reset();
        }

        public void DrawFootballPenalty()
        {
            ApplicationView applicationView = new ApplicationView();
            applicationView.InitializeEnvironment(Constants.TRUE);
            applicationView.BeginGraphicsCommands();
            applicationView.SetBkgColor(0.2f, 0.6f, 0.8f, 1.0f);

            try
            {
                applicationView.StartNewDisplayList();
            }
            catch (Exception)
            {
                applicationView.EndGraphicsCommands();
                return;
            }

            UpdateFootballPenalty();
            DrawStadium();
            DrawEnvironment();
            DrawFootballField();
            DrawGoalPost();
            DrawNet();
            DrawFootball();
            DrawKicker();
            DrawGoalkeeper();
            DrawResultMessage();

            applicationView.EndNewDisplayList();
            applicationView.EndGraphicsCommands();
            applicationView.Refresh();
        }

        public void UpdateFootballPenalty()
        {
            m_FootballGameState.gameTime += 0.016f;
            HandleKeyboardInput();

            if (m_FootballGameState.isBallMoving)
            {
                m_FootballGameState.ballX += m_FootballGameState.ballSpeedX;
                m_FootballGameState.ballY += m_FootballGameState.ballSpeedY;
                m_FootballGameState.ballZ += m_FootballGameState.ballSpeedZ;
                m_FootballGameState.ballSpeedY -= 0.098f * 0.5f;

                if (m_FootballGameState.ballY <= 0.2f && m_FootballGameState.ballZ < 4.0f)
                {
                    m_FootballGameState.ballY = 0.2f;
                    m_FootballGameState.ballSpeedY *= -0.6f;
                    m_FootballGameState.ballSpeedX *= 0.8f;
                    m_FootballGameState.ballSpeedZ *= 0.8f;

                    if (Math.Abs(m_FootballGameState.ballSpeedY) < 0.1f &&
                        Math.Abs(m_FootballGameState.ballSpeedX) < 0.1f &&
                        Math.Abs(m_FootballGameState.ballSpeedZ) < 0.1f &&
                        m_FootballGameState.ballZ < 4.0f)
                    {
                        m_FootballGameState.isBallMoving = false;
                        m_FootballGameState.isShotMissed = true;
                        ResetBall();
                    }
                }
                CheckGoal();
            }

            UpdateGoalkeeper();

            if (m_FootballGameState.isKicking &&
                (m_FootballGameState.gameTime - m_FootballGameState.kickAnimationTime) > 0.5f)
            {
                m_FootballGameState.isKicking = false;
            }
        }

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public void HandleKeyboardInput()
        {
            if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_FOOTBALLPENALTY ||
                m_ObjectPattern.m_strSimulationPattern == Constants.OBJECT_PATTERN_TYPE_PENALTY_KICK)
            {
                if (!m_FootballGameState.isBallMoving)
                {
                    if ((GetAsyncKeyState('D') & 0x8000) != 0)
                    {
                        m_FootballGameState.kickerAngle = -45.0f;
                        m_FootballGameState.kickPower = 1.0f;
                        ProcessKick();
                        Thread.Sleep(2);
                    }
                    else if ((GetAsyncKeyState('S') & 0x8000) != 0)
                    {
                        m_FootballGameState.kickerAngle = 0.0f;
                        m_FootballGameState.kickPower = 1.0f;
                        ProcessKick();
                        Thread.Sleep(2);
                    }
                    else if ((GetAsyncKeyState('A') & 0x8000) != 0)
                    {
                        m_FootballGameState.kickerAngle = 45.0f;
                        m_FootballGameState.kickPower = 1.0f;
                        ProcessKick();
                        Thread.Sleep(2);
                    }
                }
            }
        }

        public void ProcessKick()
        {
            float angleRad = m_FootballGameState.kickerAngle * (float)Math.PI / 180.0f;
            float powerMultiplier = 9.0f;

            if (Math.Abs(m_FootballGameState.kickerAngle) > 30.0f)
            {
                float cornerPower = powerMultiplier * 0.9f;
                float cornerHeight = 1.2f;
                float maxCornerX = 3.5f;
                float targetX = (float)Math.Sin(angleRad) * maxCornerX;
                m_FootballGameState.ballSpeedX = targetX * 0.8f;
                m_FootballGameState.ballSpeedZ = (float)Math.Cos(angleRad) * cornerPower;
                m_FootballGameState.ballSpeedY = cornerHeight;
            }
            else
            {
                m_FootballGameState.ballSpeedX = (float)Math.Sin(angleRad) * powerMultiplier * 0.4f;
                m_FootballGameState.ballSpeedZ = (float)Math.Cos(angleRad) * powerMultiplier;
                m_FootballGameState.ballSpeedY = 1.5f;
            }

            float maxBallSpeedX = 3.0f;
            if (Math.Abs(m_FootballGameState.ballSpeedX) > maxBallSpeedX)
            {
                m_FootballGameState.ballSpeedX = (m_FootballGameState.ballSpeedX > 0) ? maxBallSpeedX : -maxBallSpeedX;
            }

            m_FootballGameState.isBallMoving = true;
            m_FootballGameState.attempts++;
            m_FootballGameState.isKicking = true;
            m_FootballGameState.kickAnimationTime = m_FootballGameState.gameTime;
            ResetShotResult();
            DrawScene();
        }

        public void ResetShotResult()
        {
            m_FootballGameState.isGoalScored = false;
            m_FootballGameState.isShotSaved = false;
            m_FootballGameState.isShotMissed = false;
            m_FootballGameState.isKicking = false;
        }

        public void UpdateGoalkeeper()
        {
            m_FootballGameState.goalkeeperTimer -= 0.016f;

            if (m_FootballGameState.goalkeeperTimer <= 0.0f && m_FootballGameState.isBallMoving)
            {
                if (!m_FootballGameState.goalkeeperDiveLeft && !m_FootballGameState.goalkeeperDiveRight)
                {
                    float timeToGoal = (4.0f - m_FootballGameState.ballZ) / m_FootballGameState.ballSpeedZ;
                    if (timeToGoal > 0)
                    {
                        float predictedBallX = m_FootballGameState.ballX + (m_FootballGameState.ballSpeedX * timeToGoal);
                        float goalHalfWidth = 3.0f;

                        if (predictedBallX < -goalHalfWidth * 0.7f)
                        {
                            m_FootballGameState.goalkeeperDiveLeft = true;
                        }
                        else if (predictedBallX > goalHalfWidth * 0.7f)
                        {
                            m_FootballGameState.goalkeeperDiveRight = true;
                        }
                        else if (predictedBallX < -1.0f)
                        {
                            m_FootballGameState.goalkeeperDiveLeft = true;
                        }
                        else if (predictedBallX > 1.0f)
                        {
                            m_FootballGameState.goalkeeperDiveRight = true;
                        }
                        else
                        {
                            Random rand = new Random();
                            if (rand.Next(2) == 0)
                            {
                                m_FootballGameState.goalkeeperDiveLeft = true;
                            }
                            else
                            {
                                m_FootballGameState.goalkeeperDiveRight = true;
                            }
                        }
                    }
                }

                float maxGoalkeeperX = 2.8f;
                if (m_FootballGameState.goalkeeperDiveLeft)
                {
                    m_FootballGameState.goalkeeperX -= 0.08f;
                    if (m_FootballGameState.goalkeeperX < -maxGoalkeeperX)
                        m_FootballGameState.goalkeeperX = -maxGoalkeeperX;
                }
                else if (m_FootballGameState.goalkeeperDiveRight)
                {
                    m_FootballGameState.goalkeeperX += 0.08f;
                    if (m_FootballGameState.goalkeeperX > maxGoalkeeperX)
                        m_FootballGameState.goalkeeperX = maxGoalkeeperX;
                }
            }
            else if (!m_FootballGameState.isBallMoving)
            {
                m_FootballGameState.goalkeeperX = 0.0f;
                m_FootballGameState.goalkeeperDiveLeft = false;
                m_FootballGameState.goalkeeperDiveRight = false;
                m_FootballGameState.goalkeeperTimer = 0.5f;
            }
        }

        public void ResetBall()
        {
            m_FootballGameState.ballX = 0.0f;
            m_FootballGameState.ballY = 0.2f;
            m_FootballGameState.ballZ = -2.9f;
            m_FootballGameState.ballSpeedX = 0.0f;
            m_FootballGameState.ballSpeedY = 0.0f;
            m_FootballGameState.ballSpeedZ = 0.0f;
            m_FootballGameState.kickPower = 0.0f;
            m_FootballGameState.isKicking = false;
        }

        public void CheckGoal()
        {
            float goalWidth = 6.0f;
            float goalDepth = 1.0f;

            if (m_FootballGameState.ballZ >= 3.8f &&
                m_FootballGameState.ballZ <= 4.0f + goalDepth &&
                Math.Abs(m_FootballGameState.ballX) <= goalWidth / 2.0f + 0.2f &&
                m_FootballGameState.ballY >= 0.1f &&
                m_FootballGameState.ballY <= 1.8f)
            {
                bool isSaved = false;
                Random rand = new Random();

                if (m_FootballGameState.goalkeeperDiveLeft && m_FootballGameState.ballX < -1.0f)
                {
                    if (Math.Abs(m_FootballGameState.goalkeeperX - m_FootballGameState.ballX) < 1.8f)
                    {
                        isSaved = (rand.Next(100) < 60);
                    }
                }
                else if (m_FootballGameState.goalkeeperDiveRight && m_FootballGameState.ballX > 1.0f)
                {
                    if (Math.Abs(m_FootballGameState.goalkeeperX - m_FootballGameState.ballX) < 1.8f)
                    {
                        isSaved = (rand.Next(100) < 60);
                    }
                }
                else if (Math.Abs(m_FootballGameState.ballX) < 1.5f && Math.Abs(m_FootballGameState.goalkeeperX) < 1.5f)
                {
                    isSaved = (rand.Next(100) < 50);
                }

                if (!isSaved)
                {
                    m_FootballGameState.score++;
                    m_FootballGameState.isGoalScored = true;
                    m_FootballGameState.ballSpeedZ *= 0.5f;
                    m_FootballGameState.ballSpeedX *= 0.5f;
                    m_FootballGameState.ballSpeedY *= 0.3f;

                    if (m_FootballGameState.ballZ > 7.0f)
                    {
                        m_FootballGameState.isBallMoving = false;
                        ResetBall();
                    }
                }
                else
                {
                    m_FootballGameState.isShotSaved = true;
                    m_FootballGameState.ballSpeedX *= -0.7f;
                    m_FootballGameState.ballSpeedZ *= -0.4f;
                    m_FootballGameState.ballSpeedY *= 0.5f;
                }
                return;
            }

            if (m_FootballGameState.ballZ > 12.0f ||
                Math.Abs(m_FootballGameState.ballX) > 10.0f ||
                m_FootballGameState.ballY < -2.0f)
            {
                m_FootballGameState.isBallMoving = false;
                m_FootballGameState.isShotMissed = true;
                ResetBall();
            }
        }

        public void DrawFootballField()
        {
            OpenGLView openGLView = new OpenGLView();

            openGLView.glBegin(Constants.GL_QUADS);
            openGLView.glColor3f(0.3f, 0.7f, 0.3f);
            openGLView.glVertex3f(-8.0f, 0.0f, -4.0f);
            openGLView.glVertex3f(8.0f, 0.0f, -4.0f);
            openGLView.glVertex3f(8.0f, 0.0f, 8.0f);
            openGLView.glVertex3f(-8.0f, 0.0f, 8.0f);
            openGLView.glEnd();

            openGLView.glBegin(Constants.GL_LINES);
            openGLView.glColor3f(1.0f, 1.0f, 1.0f);

            openGLView.glVertex3f(-6.0f, 0.01f, -3.0f);
            openGLView.glVertex3f(6.0f, 0.01f, -3.0f);

            openGLView.glVertex3f(6.0f, 0.01f, -3.0f);
            openGLView.glVertex3f(6.0f, 0.01f, 6.0f);

            openGLView.glVertex3f(6.0f, 0.01f, 6.0f);
            openGLView.glVertex3f(-6.0f, 0.01f, 6.0f);

            openGLView.glVertex3f(-6.0f, 0.01f, 6.0f);
            openGLView.glVertex3f(-6.0f, 0.01f, -3.0f);

            openGLView.glVertex3f(-6.0f, 0.01f, 1.5f);
            openGLView.glVertex3f(6.0f, 0.01f, 1.5f);

            openGLView.glVertex3f(-0.1f, 0.01f, -2.8f);
            openGLView.glVertex3f(0.1f, 0.01f, -2.8f);

            openGLView.glVertex3f(0.0f, 0.01f, -2.9f);
            openGLView.glVertex3f(0.0f, 0.01f, -2.7f);

            openGLView.glEnd();
        }

        public void DrawGoalPost()
        {
            OpenGLView openGLView = new OpenGLView();
            openGLView.glColor3f(1.0f, 1.0f, 1.0f);

            float goalWidth = 6.0f;
            float goalHeight = 1.8f;

            openGLView.glPushMatrix();
            openGLView.glTranslatef(-goalWidth / 2, goalHeight / 2, 4.0f);
            openGLView.glScalef(0.1f, goalHeight, 0.1f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(goalWidth / 2, goalHeight / 2, 4.0f);
            openGLView.glScalef(0.1f, goalHeight, 0.1f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, goalHeight, 4.0f);
            openGLView.glScalef(goalWidth, 0.1f, 0.1f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();
        }

        public void DrawPentagon(float radius)
        {
            OpenGLView openGLView = new OpenGLView();
            openGLView.glBegin(Constants.GL_POLYGON);
            for (int i = 0; i < 5; i++)
            {
                float angle = i * 72.0f * (float)Math.PI / 180.0f;
                float x = (float)Math.Cos(angle) * radius;
                float y = (float)Math.Sin(angle) * radius;
                openGLView.glVertex2f(x, y);
            }
            openGLView.glEnd();
        }

        public void DrawFootball()
        {
            ApplicationView applicationView = new ApplicationView();
            OpenGLView openGLView = new OpenGLView();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(m_FootballGameState.ballX, m_FootballGameState.ballY, m_FootballGameState.ballZ);

            float spin = m_FootballGameState.gameTime * 10.0f;
            openGLView.glRotatef(spin * 300.0f, 1.0f, 1.0f, 0.5f);

            float radius = 0.13f;
            applicationView.SetColorf(0.12f, 0.12f, 0.12f);
            applicationView.DrawSphere(radius, 40, 40);

            openGLView.glPopMatrix();
        }

        public void DrawKicker()
        {
            OpenGLView openGLView = new OpenGLView();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 0.0f, -2.0f);
            openGLView.glRotatef(m_FootballGameState.kickerAngle, 0.0f, 1.0f, 0.0f);

            float kickT = 0.0f;
            if (m_FootballGameState.isKicking)
            {
                kickT = (float)Math.Sin(m_FootballGameState.gameTime * 12.0f);
                if (kickT < 0.0f) kickT = 0.0f;
            }

            float kickAngle = -15.0f + 100.0f * kickT;

            openGLView.glColor3f(0.95f, 0.8f, 0.65f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 0.90f, 0.0f);
            openGLView.glScalef(0.20f, 0.18f, 0.20f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.0f, 0.3f, 0.9f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 0.65f, 0.0f);
            openGLView.glScalef(0.44f, 0.35f, 0.26f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.0f, 0.25f, 0.8f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(-0.28f, 0.65f, 0.0f);
            openGLView.glRotatef(20.0f, 0.0f, 0.0f, 1.0f);
            openGLView.glScalef(0.13f, 0.30f, 0.13f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.28f, 0.65f, 0.0f);
            openGLView.glRotatef(-20.0f, 0.0f, 0.0f, 1.0f);
            openGLView.glScalef(0.13f, 0.30f, 0.13f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glColor3f(1.0f, 1.0f, 1.0f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 0.40f, 0.0f);
            openGLView.glScalef(0.48f, 0.18f, 0.30f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.95f, 0.8f, 0.65f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(-0.11f, 0.25f, 0.0f);
            openGLView.glScalef(0.13f, 0.28f, 0.13f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glColor3f(1.0f, 1.0f, 1.0f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(-0.11f, 0.05f, 0.0f);
            openGLView.glScalef(0.15f, 0.20f, 0.15f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.11f, 0.40f, 0.0f);
            openGLView.glRotatef(kickAngle, 1.0f, 0.0f, 0.0f);

            openGLView.glColor3f(0.95f, 0.8f, 0.65f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, -0.14f, 0.0f);
            openGLView.glScalef(0.13f, 0.28f, 0.13f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glTranslatef(0.0f, -0.28f, 0.0f);
            float kneeBend = kickT > 0.4f ? 40.0f * (kickT - 0.4f) / 0.6f : 0.0f;
            openGLView.glRotatef(-kneeBend, 1.0f, 0.0f, 0.0f);

            openGLView.glColor3f(0.95f, 0.8f, 0.65f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, -0.12f, 0.0f);
            openGLView.glScalef(0.11f, 0.24f, 0.11f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glPopMatrix();
            openGLView.glPopMatrix();
        }

        public void DrawGoalkeeper()
        {
            OpenGLView openGLView = new OpenGLView();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(m_FootballGameState.goalkeeperX, 0.0f, 4.0f);
            openGLView.glRotatef(m_FootballGameState.kickerAngle, 0.0f, 1.0f, 0.0f);

            float diveAngle = 0.0f;
            float diveOffsetY = 0.0f;
            float diveOffsetX = 0.0f;

            if (m_FootballGameState.goalkeeperDiveLeft)
            {
                diveAngle = -48.0f;
                diveOffsetY = 0.6f;
                diveOffsetX = -0.45f;
            }
            else if (m_FootballGameState.goalkeeperDiveRight)
            {
                diveAngle = 48.0f;
                diveOffsetY = 0.25f;
                diveOffsetX = 0.45f;
            }

            openGLView.glTranslatef(diveOffsetX, diveOffsetY, 0.0f);
            openGLView.glRotatef(diveAngle, 0.0f, 0.0f, 1.0f);

            openGLView.glColor3f(0.95f, 0.8f, 0.65f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 1.12f, 0.0f);
            openGLView.glScalef(0.22f, 0.24f, 0.22f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.0f, 0.9f, 0.3f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 0.80f, 0.0f);
            openGLView.glScalef(0.48f, 0.48f, 0.28f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.0f, 0.8f, 0.25f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(-0.35f, 0.90f, 0.0f);
            openGLView.glRotatef(-70.0f + diveAngle * 0.8f, 0.0f, 0.0f, 1.0f);
            openGLView.glScalef(0.14f, 0.45f, 0.14f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.35f, 0.90f, 0.0f);
            openGLView.glRotatef(70.0f + diveAngle * 0.8f, 0.0f, 0.0f, 1.0f);
            openGLView.glScalef(0.14f, 0.45f, 0.14f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.1f, 0.1f, 0.1f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(-0.58f, 0.90f, 0.0f);
            openGLView.glScalef(0.18f, 0.14f, 0.18f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.58f, 0.90f, 0.0f);
            openGLView.glScalef(0.18f, 0.14f, 0.18f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.1f, 0.1f, 0.1f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 0.55f, 0.0f);
            openGLView.glScalef(0.50f, 0.24f, 0.32f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.95f, 0.8f, 0.65f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(-0.11f, 0.35f, 0.0f);
            openGLView.glRotatef(20.0f + diveAngle * 0.6f, 1.0f, 0.0f, 0.0f);
            openGLView.glScalef(0.14f, 0.38f, 0.14f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.11f, 0.35f, 0.0f);
            openGLView.glRotatef(-30.0f + diveAngle * 0.7f, 1.0f, 0.0f, 0.0f);
            openGLView.glScalef(0.14f, 0.38f, 0.14f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.0f, 0.7f, 0.2f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(-0.11f, 0.08f, 0.0f);
            openGLView.glScalef(0.16f, 0.25f, 0.16f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.11f, 0.08f, 0.0f);
            openGLView.glScalef(0.16f, 0.25f, 0.16f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.1f, 0.1f, 0.1f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(-0.11f, 0.0f, 0.06f);
            openGLView.glScalef(0.16f, 0.10f, 0.26f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.11f, 0.0f, 0.06f);
            openGLView.glScalef(0.16f, 0.10f, 0.26f);
            DrawCubePrimitive(1.0f);
            openGLView.glPopMatrix();

            openGLView.glPopMatrix();
        }

        public void DrawEnvironment()
        {
            OpenGLView openGLView = new OpenGLView();

            // Draw Sky with gradient
            openGLView.glBegin(Constants.GL_QUADS);

            // Top sky - light blue
            openGLView.glColor3f(0.45f, 0.75f, 0.98f);
            openGLView.glVertex3f(-50.0f, 25.0f, -50.0f);
            openGLView.glVertex3f(50.0f, 25.0f, -50.0f);
            openGLView.glVertex3f(50.0f, 25.0f, 60.0f);
            openGLView.glVertex3f(-50.0f, 25.0f, 60.0f);

            // Bottom sky - lighter blue (gradient effect)
            openGLView.glColor3f(0.70f, 0.88f, 0.99f);
            openGLView.glVertex3f(-50.0f, 0.0f, -50.0f);
            openGLView.glVertex3f(50.0f, 0.0f, -50.0f);

            openGLView.glColor3f(0.65f, 0.85f, 0.99f);
            openGLView.glVertex3f(50.0f, 0.0f, 60.0f);
            openGLView.glVertex3f(-50.0f, 0.0f, 60.0f);

            openGLView.glEnd();

            // Draw main grass area
            openGLView.glBegin(Constants.GL_QUADS);
            openGLView.glColor3f(0.18f, 0.55f, 0.10f);  // Dark green grass
            openGLView.glVertex3f(-12.0f, 0.01f, -8.0f);
            openGLView.glVertex3f(12.0f, 0.01f, -8.0f);
            openGLView.glVertex3f(12.0f, 0.01f, 12.0f);
            openGLView.glVertex3f(-12.0f, 0.01f, 12.0f);
            openGLView.glEnd();

            // Draw field markings
            openGLView.glColor3f(1.0f, 1.0f, 1.0f);  // White lines
            openGLView.glLineWidth(3.0f);
            openGLView.glBegin(Constants.GL_LINES);

            // Center line (halfway line)
            openGLView.glVertex3f(-12.0f, 0.02f, 2.0f);
            openGLView.glVertex3f(12.0f, 0.02f, 2.0f);

            // Center circle
            for (int i = 0; i < 32; i++)
            {
                float angle1 = i * 11.25f * (float)Math.PI / 180.0f;
                float x1 = (float)Math.Cos(angle1) * 2.0f;
                float z1 = (float)Math.Sin(angle1) * 2.0f + 2.0f;

                float angle2 = (i + 1) * 11.25f * (float)Math.PI / 180.0f;
                float x2 = (float)Math.Cos(angle2) * 2.0f;
                float z2 = (float)Math.Sin(angle2) * 2.0f + 2.0f;

                openGLView.glVertex3f(x1, 0.02f, z1);
                openGLView.glVertex3f(x2, 0.02f, z2);
            }

            // Goal area (6-yard box)
            openGLView.glVertex3f(-4.0f, 0.02f, 9.0f);
            openGLView.glVertex3f(4.0f, 0.02f, 9.0f);

            openGLView.glVertex3f(-4.0f, 0.02f, 9.0f);
            openGLView.glVertex3f(-4.0f, 0.02f, 12.0f);

            openGLView.glVertex3f(4.0f, 0.02f, 9.0f);
            openGLView.glVertex3f(4.0f, 0.02f, 12.0f);

            // Penalty area line
            openGLView.glVertex3f(-5.0f, 0.02f, 12.0f);
            openGLView.glVertex3f(5.0f, 0.02f, 12.0f);

            openGLView.glEnd();

            // Draw stadium stands
            openGLView.glBegin(Constants.GL_QUADS);

            // Left stand (red team side)
            openGLView.glColor3f(0.15f, 0.15f, 0.20f);  // Dark gray structure
            openGLView.glVertex3f(-20.0f, 0.0f, -15.0f);
            openGLView.glVertex3f(-12.0f, 0.0f, -10.0f);
            openGLView.glVertex3f(-12.0f, 8.0f, -10.0f);
            openGLView.glVertex3f(-20.0f, 6.0f, -15.0f);

            // Left stand seats (red)
            openGLView.glColor3f(0.9f, 0.3f, 0.1f);  // Red seats
            openGLView.glVertex3f(-19.5f, 1.0f, -14.5f);
            openGLView.glVertex3f(-12.5f, 1.0f, -10.5f);
            openGLView.glVertex3f(-12.5f, 7.0f, -10.5f);
            openGLView.glVertex3f(-19.5f, 5.0f, -14.5f);

            // Right stand (blue team side)
            openGLView.glColor3f(0.15f, 0.15f, 0.20f);  // Dark gray structure
            openGLView.glVertex3f(20.0f, 0.0f, -15.0f);
            openGLView.glVertex3f(12.0f, 0.0f, -10.0f);
            openGLView.glVertex3f(12.0f, 8.0f, -10.0f);
            openGLView.glVertex3f(20.0f, 6.0f, -15.0f);

            // Right stand seats (blue)
            openGLView.glColor3f(0.1f, 0.3f, 0.9f);  // Blue seats
            openGLView.glVertex3f(19.5f, 1.0f, -14.5f);
            openGLView.glVertex3f(12.5f, 1.0f, -10.5f);
            openGLView.glVertex3f(12.5f, 7.0f, -10.5f);
            openGLView.glVertex3f(19.5f, 5.0f, -14.5f);

            openGLView.glEnd();

            // Draw background hills/terrain
            openGLView.glBegin(Constants.GL_QUADS);
            openGLView.glColor3f(0.4f, 0.7f, 0.2f);  // Light green background
            openGLView.glVertex3f(-40.0f, 0.0f, 50.0f);
            openGLView.glVertex3f(40.0f, 0.0f, 50.0f);
            openGLView.glVertex3f(40.0f, 20.0f, 50.0f);
            openGLView.glVertex3f(-40.0f, 20.0f, 50.0f);
            openGLView.glEnd();

            // Draw corner flags
            // Top-left corner flag (red)
            openGLView.glColor3f(0.7f, 0.7f, 0.7f);  // Silver pole
            openGLView.glBegin(Constants.GL_LINES);
            openGLView.glVertex3f(-11.5f, 0.0f, -7.5f);
            openGLView.glVertex3f(-11.5f, 2.5f, -7.5f);
            openGLView.glEnd();

            openGLView.glColor3f(1.0f, 0.0f, 0.0f);  // Red flag
            openGLView.glBegin(Constants.GL_TRIANGLES);
            openGLView.glVertex3f(-11.5f, 2.5f, -7.5f);
            openGLView.glVertex3f(-11.0f, 2.0f, -7.5f);
            openGLView.glVertex3f(-11.5f, 1.5f, -7.5f);
            openGLView.glEnd();

            // Top-right corner flag (blue)
            openGLView.glColor3f(0.7f, 0.7f, 0.7f);
            openGLView.glBegin(Constants.GL_LINES);
            openGLView.glVertex3f(11.5f, 0.0f, -7.5f);
            openGLView.glVertex3f(11.5f, 2.5f, -7.5f);
            openGLView.glEnd();

            openGLView.glColor3f(0.0f, 0.0f, 1.0f);  // Blue flag
            openGLView.glBegin(Constants.GL_TRIANGLES);
            openGLView.glVertex3f(11.5f, 2.5f, -7.5f);
            openGLView.glVertex3f(12.0f, 2.0f, -7.5f);
            openGLView.glVertex3f(11.5f, 1.5f, -7.5f);
            openGLView.glEnd();

            // Draw sun in the sky
            openGLView.glPushMatrix();
            openGLView.glTranslatef(15.0f, 20.0f, -30.0f);

            // Sun core
            openGLView.glColor3f(1.0f, 0.95f, 0.7f);
            openGLView.glBegin(Constants.GL_TRIANGLE_FAN);
            openGLView.glVertex3f(0.0f, 0.0f, 0.0f);
            for (int i = 0; i <= 32; i++)
            {
                float angle = i * (360.0f / 32) * (float)Math.PI / 180.0f;
                float sunX = (float)Math.Cos(angle) * 2.0f;
                float sunY = (float)Math.Sin(angle) * 2.0f;
                openGLView.glVertex3f(sunX, sunY, 0.0f);
            }
            openGLView.glEnd();
            openGLView.glPopMatrix();
        }

        public void DrawCubePrimitive(float size)
        {
            OpenGLView openGLView = new OpenGLView();
            float s = size / 2.0f;

            openGLView.glBegin(Constants.GL_QUADS);

            openGLView.glVertex3f(-s, -s, s);
            openGLView.glVertex3f(s, -s, s);
            openGLView.glVertex3f(s, s, s);
            openGLView.glVertex3f(-s, s, s);

            openGLView.glVertex3f(-s, -s, -s);
            openGLView.glVertex3f(-s, s, -s);
            openGLView.glVertex3f(s, s, -s);
            openGLView.glVertex3f(s, -s, -s);

            openGLView.glVertex3f(-s, s, -s);
            openGLView.glVertex3f(-s, s, s);
            openGLView.glVertex3f(s, s, s);
            openGLView.glVertex3f(s, s, -s);

            openGLView.glVertex3f(-s, -s, -s);
            openGLView.glVertex3f(s, -s, -s);
            openGLView.glVertex3f(s, -s, s);
            openGLView.glVertex3f(-s, -s, s);

            openGLView.glVertex3f(s, -s, -s);
            openGLView.glVertex3f(s, s, -s);
            openGLView.glVertex3f(s, s, s);
            openGLView.glVertex3f(s, -s, s);

            openGLView.glVertex3f(-s, -s, -s);
            openGLView.glVertex3f(-s, -s, s);
            openGLView.glVertex3f(-s, s, s);
            openGLView.glVertex3f(-s, s, -s);

            openGLView.glEnd();
        }

        public void DrawPenaltyKickView()
        {
            ApplicationView applicationView = new ApplicationView();
            applicationView.InitializeEnvironment(Constants.TRUE);
            applicationView.BeginGraphicsCommands();
            applicationView.SetBkgColor(0.2f, 0.6f, 0.8f, 1.0f);

            try
            {
                applicationView.StartNewDisplayList();
            }
            catch (Exception)
            {
                applicationView.EndGraphicsCommands();
                return;
            }

            UpdateFootballPenalty();
            DrawStadium();
            DrawEnvironment();
            DrawFootballField();
            DrawGoalPost();
            DrawNet();
            DrawFootball();
            DrawKicker();
            DrawGoalkeeper();
            DrawPenaltyKickHUD();
            DrawResultMessage();

            applicationView.EndNewDisplayList();
            applicationView.EndGraphicsCommands();
            applicationView.Refresh();
        }

        public void DrawPenaltyKickHUD()
        {
            DrawAimingReticle();
        }

        public void DrawAimingReticle()
        {
            if (!m_bShowAimingReticle) return;

            ApplicationView applicationView = new ApplicationView();
            applicationView.SetColorf(1.0f, 1.0f, 1.0f);
            applicationView.SetLineWidth(2.0f);

            applicationView.BeginDraw((int)Constants.GL_LINES);
            applicationView.Set2DVertexf(-0.05f, 0.0f);
            applicationView.Set2DVertexf(0.05f, 0.0f);
            applicationView.Set2DVertexf(0.0f, -0.05f);
            applicationView.Set2DVertexf(0.0f, 0.05f);
            applicationView.EndDraw();

            float angleIndicatorX = (float)Math.Sin(m_FootballGameState.kickerAngle * (float)Math.PI / 180.0f) * 0.1f;
            applicationView.SetColorf(1.0f, 0.0f, 0.0f);
            applicationView.BeginDraw((int)Constants.GL_LINES);
            applicationView.Set2DVertexf(0.0f, -0.05f);
            applicationView.Set2DVertexf(angleIndicatorX, -0.08f);
            applicationView.EndDraw();
        }

        public void DrawResultMessage()
        {
            ApplicationView applicationView = new ApplicationView();

            if (m_FootballGameState.isGoalScored || m_FootballGameState.isShotSaved || m_FootballGameState.isShotMissed)
            {
                string strMessage = "";
                if (m_FootballGameState.isGoalScored)
                {
                    strMessage = "GOAL!";
                    applicationView.SetColorf(0.0f, 1.0f, 0.0f);
                }
                else if (m_FootballGameState.isShotSaved)
                {
                    strMessage = "SAVED!";
                    applicationView.SetColorf(1.0f, 1.0f, 0.0f);
                }
                else if (m_FootballGameState.isShotMissed)
                {
                    strMessage = "MISSED!";
                    applicationView.SetColorf(1.0f, 0.0f, 0.0f);
                }

                applicationView.BeginDraw((int)Constants.GL_QUADS);
                applicationView.Set2DVertexf(-0.2f, 0.1f);
                applicationView.Set2DVertexf(0.2f, 0.1f);
                applicationView.Set2DVertexf(0.2f, 0.2f);
                applicationView.Set2DVertexf(-0.2f, 0.2f);
                applicationView.EndDraw();

                applicationView.SetColorf(1.0f, 1.0f, 1.0f);
                applicationView.BeginDraw((int)Constants.GL_LINE_LOOP);
                applicationView.Set2DVertexf(-0.2f, 0.1f);
                applicationView.Set2DVertexf(0.2f, 0.1f);
                applicationView.Set2DVertexf(0.2f, 0.2f);
                applicationView.Set2DVertexf(-0.2f, 0.2f);
                applicationView.EndDraw();
            }
        }

        public void DrawStadium()
        {
            OpenGLView openGLView = new OpenGLView();
            openGLView.glColor3f(0.4f, 0.4f, 0.4f);

            openGLView.glBegin(Constants.GL_QUADS);
            openGLView.glVertex3f(-8.0f, 0.0f, -4.0f);
            openGLView.glVertex3f(-8.0f, 3.0f, -4.0f);
            openGLView.glVertex3f(-8.0f, 3.0f, 8.0f);
            openGLView.glVertex3f(-8.0f, 0.0f, 8.0f);

            openGLView.glVertex3f(8.0f, 0.0f, -4.0f);
            openGLView.glVertex3f(8.0f, 3.0f, -4.0f);
            openGLView.glVertex3f(8.0f, 3.0f, 8.0f);
            openGLView.glVertex3f(8.0f, 0.0f, 8.0f);

            openGLView.glVertex3f(-8.0f, 0.0f, 8.0f);
            openGLView.glVertex3f(-8.0f, 3.0f, 8.0f);
            openGLView.glVertex3f(8.0f, 3.0f, 8.0f);
            openGLView.glVertex3f(8.0f, 0.0f, 8.0f);
            openGLView.glEnd();
        }

        public void DrawNet()
        {
            OpenGLView openGLView = new OpenGLView();
            float goalWidth = 6.0f;
            float goalHeight = 1.8f;

            openGLView.glColor4f(1.0f, 1.0f, 1.0f, 0.3f);

            for (int i = 0; i <= 8; i++)
            {
                float x = -goalWidth / 2 + (goalWidth / 8) * i;
                openGLView.glBegin(Constants.GL_LINES);
                openGLView.glVertex3f(x, 0.0f, 4.0f);
                openGLView.glVertex3f(x, goalHeight, 4.0f);
                openGLView.glEnd();
            }

            for (int i = 0; i <= 4; i++)
            {
                float y = (goalHeight / 4) * i;
                openGLView.glBegin(Constants.GL_LINES);
                openGLView.glVertex3f(-goalWidth / 2, y, 4.0f);
                openGLView.glVertex3f(goalWidth / 2, y, 4.0f);
                openGLView.glEnd();
            }
        }
        #endregion

        #region Elephant Drawing Methods
        public void DrawElephant()
        {
            ApplicationView applicationView = new ApplicationView();
            applicationView.InitializeEnvironment(Constants.TRUE);
            applicationView.BeginGraphicsCommands();
            applicationView.SetBkgColor(0.45f, 0.75f, 0.98f, 1.0f) ;

            try
            {
                applicationView.StartNewDisplayList();
            }
            catch (Exception)
            {
                applicationView.EndGraphicsCommands();
                return;
            }

            OpenGLView openGLView = new OpenGLView();
            UpdateElephantAnimation();
            DrawSavannahEnvironmentWithPond();

            openGLView.glPushMatrix();

            float baseLift = 0.02f;
            openGLView.glTranslatef(m_ElephantGameState.positionX,
                                    baseLift,
                                    m_ElephantGameState.positionZ);
            openGLView.glRotatef(m_ElephantGameState.rotationY, 0.0f, 1.0f, 0.0f);

            float overallScale = 0.8f;
            openGLView.glScalef(overallScale, overallScale, overallScale);

            DrawElephantLegs();
            DrawElephantBody();
            DrawElephantTail();
            DrawElephantHead();
            DrawElephantEars();
            DrawElephantTusks();
            DrawElephantTrunk();

            openGLView.glPopMatrix();

            applicationView.EndNewDisplayList();
            applicationView.EndGraphicsCommands();
            applicationView.Refresh();
        }

        public void DrawElephantBody()
        {
            OpenGLView openGLView = new OpenGLView();
            float bodyR = 0.58f;
            float bodyG = 0.58f;
            float bodyB = 0.60f;

            float bob = (m_ElephantGameState.currentState != 0) ?
                Math.Abs((float)Math.Sin(m_ElephantGameState.animationTime * 4.0f)) * 0.02f : 0.0f;

            openGLView.glPushMatrix();
            float bodyBaseY = 0.85f;
            openGLView.glTranslatef(0.0f, bodyBaseY + bob, 0.0f);

            openGLView.glColor3f(bodyR, bodyG, bodyB);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 0.25f, 0.0f);
            openGLView.glScalef(0.9f, 0.5f, 1.4f);
            DrawEllipsoid(1.0f, 40, 30);
            openGLView.glPopMatrix();

            openGLView.glColor3f(bodyR * 1.05f, bodyG * 1.05f, bodyB * 1.05f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 0.5f, -0.4f);
            openGLView.glScalef(0.7f, 0.45f, 0.7f);
            DrawEllipsoid(1.0f, 35, 25);
            openGLView.glPopMatrix();

            openGLView.glColor3f(bodyR * 0.95f, bodyG * 0.95f, bodyB * 0.95f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 0.1f, 0.15f);
            openGLView.glScalef(0.8f, 0.3f, 1.1f);
            DrawEllipsoid(1.0f, 40, 30);
            openGLView.glPopMatrix();

            openGLView.glColor3f(bodyR * 1.02f, bodyG * 1.02f, bodyB * 1.02f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 0.4f, -0.75f);
            openGLView.glScalef(0.65f, 0.4f, 0.5f);
            DrawEllipsoid(1.0f, 35, 25);
            openGLView.glPopMatrix();

            openGLView.glColor3f(bodyR * 0.98f, bodyG * 0.98f, bodyB * 0.98f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 0.35f, 0.9f);
            openGLView.glScalef(0.45f, 0.35f, 0.4f);
            DrawEllipsoid(1.0f, 30, 20);
            openGLView.glPopMatrix();

            openGLView.glColor3f(bodyR * 0.97f, bodyG * 0.97f, bodyB * 0.97f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 0.2f, -1.0f);
            openGLView.glScalef(0.6f, 0.45f, 0.55f);
            DrawEllipsoid(1.0f, 30, 20);
            openGLView.glPopMatrix();

            openGLView.glPopMatrix();
        }

        public void DrawElephantHead()
        {
            OpenGLView openGLView = new OpenGLView();
            float headR = 0.56f;
            float headG = 0.56f;
            float headB = 0.58f;

            openGLView.glPushMatrix();
            float bodyBaseY = 0.8f;
            float neckBaseY = bodyBaseY + 0.35f;
            float headY = neckBaseY + 0.1f + m_ElephantGameState.headBob;
            float headZ = 1.15f;

            openGLView.glTranslatef(0.0f, headY, headZ);

            openGLView.glColor3f(headR, headG, headB);
            openGLView.glPushMatrix();
            openGLView.glScalef(0.55f, 0.45f, 0.5f);
            DrawEllipsoid(1.0f, 35, 25);
            openGLView.glPopMatrix();

            openGLView.glColor3f(headR * 1.02f, headG * 1.02f, headB * 1.02f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 0.15f, 0.1f);
            openGLView.glScalef(0.45f, 0.25f, 0.35f);
            DrawEllipsoid(1.0f, 30, 20);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.55f, 0.35f, 0.15f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.46f, 0.20f, 0.16f);
            openGLView.glScalef(0.035f, 0.035f, 0.035f);
            DrawEllipsoid(1.0f, 12, 8);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.05f, 0.05f, 0.05f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.46f, 0.20f, 0.162f);
            openGLView.glScalef(0.018f, 0.018f, 0.018f);
            DrawEllipsoid(1.0f, 12, 8);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.55f, 0.35f, 0.15f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(-0.46f, 0.20f, 0.16f);
            openGLView.glScalef(0.035f, 0.035f, 0.035f);
            DrawEllipsoid(1.0f, 12, 8);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.05f, 0.05f, 0.05f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(-0.46f, 0.20f, 0.162f);
            openGLView.glScalef(0.018f, 0.018f, 0.018f);
            DrawEllipsoid(1.0f, 12, 8);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.05f, 0.05f, 0.05f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.48f, 0.22f, 0.155f);
            openGLView.glScalef(0.025f, 0.02f, 0.03f);
            DrawEllipsoid(1.0f, 8, 6);
            openGLView.glPopMatrix();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(-0.48f, 0.22f, 0.155f);
            openGLView.glScalef(0.025f, 0.02f, 0.03f);
            DrawEllipsoid(1.0f, 8, 6);
            openGLView.glPopMatrix();

            openGLView.glColor3f(headR * 0.98f, headG * 0.98f, headB * 0.98f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(-0.3f, 0.0f, 0.05f);
            openGLView.glScalef(0.2f, 0.175f, 0.15f);
            DrawEllipsoid(1.0f, 25, 15);
            openGLView.glPopMatrix();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.3f, 0.0f, 0.05f);
            openGLView.glScalef(0.2f, 0.175f, 0.15f);
            DrawEllipsoid(1.0f, 25, 15);
            openGLView.glPopMatrix();

            openGLView.glColor3f(headR * 0.95f, headG * 0.95f, headB * 0.95f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, -0.15f, 0.15f);
            openGLView.glScalef(0.2f, 0.1f, 0.15f);
            DrawEllipsoid(1.0f, 20, 15);
            openGLView.glPopMatrix();

            openGLView.glColor3f(headR * 0.9f, headG * 0.9f, headB * 0.9f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, -0.2f, 0.2f);
            openGLView.glScalef(0.1f, 0.04f, 0.075f);
            DrawEllipsoid(1.0f, 20, 12);
            openGLView.glPopMatrix();

            openGLView.glPopMatrix();
        }

        public void DrawElephantEars()
        {
            OpenGLView openGLView = new OpenGLView();
            // Removed glEnable(0x0BE2); to disable blending for opacity
            // openGLView.glBlendFunc(0x0300, 0x0302); // Not needed if blending is disabled
            // Use the same color as the body/head for ears
            float earR = 0.56f; // Same as headR (0.56)
            float earG = 0.56f; // Same as headG (0.56)
            float earB = 0.58f; // Same as headB (0.58)
            float alpha = 1.0f; // No transparency
            float flap = m_ElephantGameState.earFlap;
            float bodyBaseY = 0.8f;
            float neckBaseY = bodyBaseY + 0.35f;
            float headY = neckBaseY + 0.1f;
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, headY, 1.15f);
            // Left ear
            openGLView.glPushMatrix();
            openGLView.glTranslatef(-0.45f, 0.0f, -0.25f);
            openGLView.glRotatef(-25.0f + flap * 15.0f, 0.0f, 0.0f, 1.0f);
            openGLView.glRotatef(-10.0f, 1.0f, 0.0f, 0.0f);
            // Main ear surface
            openGLView.glColor4f(earR, earG, earB, alpha);
            openGLView.glPushMatrix();
            openGLView.glScalef(0.9f, 0.5f, 0.025f);
            DrawEllipsoid(1.0f, 35, 20);
            openGLView.glPopMatrix();
            // Ear wrinkles - slightly darker
            openGLView.glColor4f(earR * 0.92f, earG * 0.92f, earB * 0.92f, alpha);
            for (int wrinkle = 0; wrinkle < 3; wrinkle++)
            {
                float offset = (wrinkle - 1.0f) * 0.2f;
                openGLView.glPushMatrix();
                openGLView.glTranslatef(offset, 0.0f, 0.0f);
                openGLView.glScalef(0.7f - wrinkle * 0.1f, 0.4f, 0.02f);
                DrawEllipsoid(1.0f, 20, 15);
                openGLView.glPopMatrix();
            }
            openGLView.glPopMatrix();
            // Right ear
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.45f, 0.0f, -0.25f);
            openGLView.glRotatef(25.0f - flap * 15.0f, 0.0f, 0.0f, 1.0f);
            openGLView.glRotatef(-10.0f, 1.0f, 0.0f, 0.0f);
            // Main ear surface
            openGLView.glColor4f(earR, earG, earB, alpha);
            openGLView.glPushMatrix();
            openGLView.glScalef(0.9f, 0.5f, 0.025f);
            DrawEllipsoid(1.0f, 35, 20);
            openGLView.glPopMatrix();
            // Ear wrinkles - slightly darker
            openGLView.glColor4f(earR * 0.92f, earG * 0.92f, earB * 0.92f, alpha);
            for (int wrinkle = 0; wrinkle < 3; wrinkle++)
            {
                float offset = (wrinkle - 1.0f) * 0.2f;
                openGLView.glPushMatrix();
                openGLView.glTranslatef(offset, 0.0f, 0.0f);
                openGLView.glScalef(0.7f - wrinkle * 0.1f, 0.4f, 0.02f);
                DrawEllipsoid(1.0f, 20, 15);
                openGLView.glPopMatrix();
            }
            openGLView.glPopMatrix();
            openGLView.glPopMatrix();
            // Removed glDisable(0x0BE2); since blending was not enabled
        }
        public void DrawElephantTrunk()
        {
            OpenGLView openGLView = new OpenGLView();
            float trunkR = 0.57f;
            float trunkG = 0.57f;
            float trunkB = 0.59f;
            float swing = m_ElephantGameState.trunkSwing * 0.3f;
            float trunkLift = m_ElephantGameState.isTrumpeting ? 0.4f : 0.0f;
            float trunkScale = 0.6f;

            float bodyBaseY = 0.8f;
            float neckBaseY = bodyBaseY + 0.35f;
            float headY = neckBaseY + 0.1f;

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, headY, 1.15f);
            openGLView.glTranslatef(0.0f, -0.2f, 0.5f);

            openGLView.glColor3f(trunkR, trunkG, trunkB);
            openGLView.glPushMatrix();
            openGLView.glScalef(0.2f, 0.25f * trunkScale, 0.1f);
            DrawEllipsoid(1.0f, 30, 20);
            openGLView.glPopMatrix();

            openGLView.glTranslatef(swing * 0.1f, trunkLift * 0.15f * trunkScale - 0.2f * trunkScale, 0.0f);
            openGLView.glRotatef(swing * 10.0f, 0.0f, 1.0f, 0.0f);

            int numSegments = 6;
            float[] segmentLengths = { 0.3f, 0.275f, 0.25f, 0.225f, 0.2f, 0.175f };
            float[] segmentRadii = { 0.175f, 0.16f, 0.145f, 0.13f, 0.115f, 0.1f };

            for (int i = 0; i < numSegments; i++)
            {
                float segLen = segmentLengths[i] * trunkScale;
                openGLView.glPushMatrix();
                float curveAngle = 0.0f;
                openGLView.glRotatef(curveAngle, 1.0f, 0.0f, 0.0f);
                openGLView.glScalef(segmentRadii[i], segLen, segmentRadii[i]);
                DrawEllipsoid(1.0f, 25, 18);
                openGLView.glPopMatrix();
                openGLView.glTranslatef(0.0f, -segLen, 0.0f);
            }

            openGLView.glColor3f(trunkR * 0.9f, trunkG * 0.9f, trunkB * 0.9f);
            openGLView.glPushMatrix();
            openGLView.glScalef(0.06f, 0.1f * trunkScale, 0.06f);
            DrawEllipsoid(1.0f, 20, 15);
            openGLView.glPopMatrix();

            openGLView.glColor3f(0.15f, 0.15f, 0.15f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(-0.025f, -0.075f * trunkScale, 0.0f);
            openGLView.glScalef(0.02f, 0.02f, 0.02f);
            DrawSphere(1.0f, 8, 6);
            openGLView.glPopMatrix();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.025f, -0.075f * trunkScale, 0.0f);
            openGLView.glScalef(0.02f, 0.02f, 0.02f);
            DrawSphere(1.0f, 8, 6);
            openGLView.glPopMatrix();

            openGLView.glPopMatrix();
        }

        public void DrawElephantLegs()
        {
            OpenGLView openGLView = new OpenGLView();
            float legR = 0.55f;
            float legG = 0.55f;
            float legB = 0.57f;
            float walkCycle = m_ElephantGameState.animationTime * 4.0f;
            float frontLift = (m_ElephantGameState.currentState != 0) ?
                (float)Math.Sin(walkCycle) * 0.1f : 0.0f;
            float backLift = (float)Math.Sin(walkCycle + (float)Math.PI) * 0.1f;

            float[,] legs = {
                { -0.6f, 0.5f, frontLift, 1 },
                { 0.6f, 0.5f, frontLift, 1 },
                { -0.45f, -0.75f, backLift, 0 },
                { 0.45f, -0.75f, backLift, 0 }
            };

            float legLength = 0.85f;
            float frontLegRadius = 0.18f;
            float backLegRadius = 0.20f;
            float baseLift = 0.02f;

            for (int i = 0; i < 4; i++)
            {
                openGLView.glPushMatrix();
                openGLView.glTranslatef(legs[i, 0], legs[i, 2] + baseLift, legs[i, 1]);
                float currentRadius = legs[i, 3] > 0.5f ? frontLegRadius : backLegRadius;

                openGLView.glColor3f(legR, legG, legB);
                DrawCylinder(currentRadius, legLength);

                openGLView.glColor3f(0.45f, 0.45f, 0.48f);
                openGLView.glPushMatrix();
                openGLView.glTranslatef(0.0f, -0.005f, 0.0f);
                float footScale = legs[i, 3] > 0.5f ? currentRadius * 1.45f : currentRadius * 1.5f;
                openGLView.glScalef(footScale, 0.025f, footScale);
                DrawEllipsoid(1.0f, 40, 40);
                openGLView.glPopMatrix();

                openGLView.glColor3f(0.65f, 0.58f, 0.50f);
                float toeSpacing = legs[i, 3] > 0.5f ? 0.12f : 0.10f;
                float[,] toePositions = {
                    { -toeSpacing * 2.0f, -0.02f },
                    { -toeSpacing, -0.06f },
                    { 0.0f, -0.08f },
                    { toeSpacing, -0.06f },
                    { toeSpacing * 2.0f, -0.02f }
                };

                for (int toe = 0; toe < 5; toe++)
                {
                    openGLView.glPushMatrix();
                    openGLView.glTranslatef(toePositions[toe, 0], 0.02f, toePositions[toe, 1]);
                    openGLView.glRotatef(15.0f, 1.0f, 0.0f, 0.0f);
                    float nailScaleX = (toe == 2) ? 0.055f : 0.040f;
                    float nailScaleY = 0.015f;
                    float nailScaleZ = (toe == 2) ? 0.045f : 0.035f;
                    openGLView.glScalef(nailScaleX, nailScaleY, nailScaleZ);
                    DrawEllipsoid(1.0f, 16, 12);
                    openGLView.glPopMatrix();
                }

                openGLView.glPopMatrix();
            }
        }

        public void DrawElephantTail()
        {
            OpenGLView openGLView = new OpenGLView();
            float tailR = 0.50f, tailG = 0.50f, tailB = 0.52f;
            float hairR = 0.18f, hairG = 0.18f, hairB = 0.20f;
            float sway = (float)Math.Sin(m_ElephantGameState.animationTime * 1.4f) * 8.0f;

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 1.15f, -1.3f);
            openGLView.glRotatef(sway, 0.0f, 1.0f, 0.0f);

            float tailHang = 40.0f + (float)Math.Sin(m_ElephantGameState.animationTime * 0.7f) * 4.0f;
            openGLView.glRotatef(-tailHang, 1.0f, 0.0f, 0.0f);

            openGLView.glColor3f(tailR, tailG, tailB);
            openGLView.glPushMatrix();
            openGLView.glRotatef(-50.0f, 1.0f, 0.0f, 0.0f);

            openGLView.glBegin(Constants.GL_TRIANGLE_FAN);
            openGLView.glVertex3f(0.0f, 0.0f, 0.0f);
            float baseRadius = 0.07f;
            int discSegments = 16;

            for (int j = 0; j <= discSegments; ++j)
            {
                float angle = (float)j / (float)discSegments * 2.0f * (float)Math.PI;
                float x = baseRadius * (float)Math.Cos(angle);
                float y = baseRadius * (float)Math.Sin(angle);
                openGLView.glVertex3f(x, y, 0.0f);
            }
            openGLView.glEnd();
            openGLView.glPopMatrix();

            float tipRadius = 0.018f;
            float tailLength = 0.85f;
            int segments = 14;
            int circleSegments = 10;

            openGLView.glBegin(Constants.GL_QUAD_STRIP);
            for (int i = 0; i <= segments; ++i)
            {
                float t = (float)i / (float)segments;
                float radius = baseRadius * (1.0f - t) + tipRadius * t;
                float z = -0.02f - tailLength * t;
                float curve = t * t * 0.6f;
                float x = (float)Math.Sin(m_ElephantGameState.animationTime * 0.3f + t * 2.5f) * 0.015f;
                float y = -curve;

                float t2 = (i < segments) ? ((float)(i + 1) / (float)segments) : t;
                float z2 = -0.02f - tailLength * t2;
                float curve2 = t2 * t2 * 0.6f;
                float x2 = (float)Math.Sin(m_ElephantGameState.animationTime * 0.3f + t2 * 2.5f) * 0.015f;
                float y2 = -curve2;

                float dx = x2 - x;
                float dy = y2 - y;
                float dz = z2 - z;
                float upX = 0.0f, upY = 1.0f, upZ = 0.0f;
                float rightX = dy * upZ - dz * upY;
                float rightY = dz * upX - dx * upZ;
                float rightZ = dx * upY - dy * upX;

                float rightLen = (float)Math.Sqrt(rightX * rightX + rightY * rightY + rightZ * rightZ);
                if (rightLen > 0.001f)
                {
                    rightX /= rightLen;
                    rightY /= rightLen;
                    rightZ /= rightLen;
                }

                float dirLen = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
                if (dirLen > 0.001f)
                {
                    dx /= dirLen;
                    dy /= dirLen;
                    dz /= dirLen;
                }

                float trueUpX = rightY * dz - rightZ * dy;
                float trueUpY = rightZ * dx - rightX * dz;
                float trueUpZ = rightX * dy - rightY * dx;

                for (int j = 0; j <= circleSegments; ++j)
                {
                    float angle = (float)j / (float)circleSegments * 2.0f * (float)Math.PI;
                    float cosA = (float)Math.Cos(angle);
                    float sinA = (float)Math.Sin(angle);

                    float circleX = x + radius * (cosA * rightX + sinA * trueUpX);
                    float circleY = y + radius * (cosA * rightY + sinA * trueUpY);
                    float circleZ = z + radius * (cosA * rightZ + sinA * trueUpZ);

                    openGLView.glVertex3f(circleX, circleY, circleZ);

                    if (i < segments)
                    {
                        float nextCircleX = x2 + radius * (cosA * rightX + sinA * trueUpX);
                        float nextCircleY = y2 + radius * (cosA * rightY + sinA * trueUpY);
                        float nextCircleZ = z2 + radius * (cosA * rightZ + sinA * trueUpZ);
                        openGLView.glVertex3f(nextCircleX, nextCircleY, nextCircleZ);
                    }
                    else
                    {
                        openGLView.glVertex3f(circleX, circleY, circleZ);
                    }
                }
            }
            openGLView.glEnd();

            openGLView.glColor3f(hairR, hairG, hairB);
            float tipZ = -0.02f - tailLength;
            float tipY = -0.6f;

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, tipY, tipZ);
            openGLView.glRotatef(-tailHang * 0.8f, 1.0f, 0.0f, 0.0f);
            openGLView.glRotatef(sway * 2.0f, 0.0f, 1.0f, 0.0f);

            int numHairs = 20;
            float hairLength = 0.45f;
            float fanAngle = 70.0f;

            for (int i = 0; i < numHairs; ++i)
            {
                float angle = (i - (numHairs - 1) / 2.0f) * (fanAngle / (numHairs - 1));
                openGLView.glPushMatrix();
                openGLView.glRotatef(angle, 0.0f, 0.0f, 1.0f);
                float wave = (float)Math.Sin(m_ElephantGameState.animationTime * 2.8f + i * 1.3f) * 10.0f;
                openGLView.glRotatef(-80.0f + wave, 1.0f, 0.0f, 0.0f);

                openGLView.glBegin(Constants.GL_LINES);
                openGLView.glVertex3f(0.0f, 0.0f, 0.0f);
                openGLView.glVertex3f(0.0f, 0.0f, -hairLength);
                openGLView.glEnd();

                openGLView.glPopMatrix();
            }

            openGLView.glPopMatrix();
            openGLView.glPopMatrix();
        }

        public void DrawElephantTusks()
        {
            OpenGLView openGLView = new OpenGLView();
            openGLView.glColor3f(0.95f, 0.95f, 0.88f);
            float tuskLength = 0.8f;
            float baseRadius = 0.05f;
            float tipRadius = 0.015f;

            float bodyBaseY = 0.8f;
            float neckBaseY = bodyBaseY + 0.35f;
            float headY = neckBaseY + 0.1f + m_ElephantGameState.headBob;

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, headY, 1.15f);
            openGLView.glTranslatef(-0.2f, -0.15f, 0.4f);
            openGLView.glRotatef(-20.0f, 0.0f, 1.0f, 0.0f);
            openGLView.glRotatef(-5.0f, 1.0f, 0.0f, 0.0f);

            int segments = 12;
            for (int i = 0; i < segments; i++)
            {
                float t = (float)i / (float)segments;
                float radius = baseRadius * (1.0f - t) + tipRadius * t;
                float y = -t * t * 0.15f;
                float z = t * tuskLength;

                openGLView.glPushMatrix();
                openGLView.glTranslatef(0.0f, y, z);
                openGLView.glScalef(radius, radius, tuskLength / (float)segments);
                DrawEllipsoid(1.0f, 10, 8);
                openGLView.glPopMatrix();
            }
            openGLView.glPopMatrix();

            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, headY, 1.15f);
            openGLView.glTranslatef(0.2f, -0.15f, 0.4f);
            openGLView.glRotatef(20.0f, 0.0f, 1.0f, 0.0f);
            openGLView.glRotatef(-5.0f, 1.0f, 0.0f, 0.0f);

            for (int i = 0; i < segments; i++)
            {
                float t = (float)i / (float)segments;
                float radius = baseRadius * (1.0f - t) + tipRadius * t;
                float y = -t * t * 0.15f;
                float z = t * tuskLength;

                openGLView.glPushMatrix();
                openGLView.glTranslatef(0.0f, y, z);
                openGLView.glScalef(radius, radius, tuskLength / (float)segments);
                DrawEllipsoid(1.0f, 10, 8);
                openGLView.glPopMatrix();
            }
            openGLView.glPopMatrix();
        }

        public void UpdateElephantAnimation()
        {
            m_ElephantGameState.animationTime += 0.016f;
            float breathe = (float)Math.Sin(m_ElephantGameState.animationTime * 1.5f) * 0.008f;
            m_ElephantGameState.headBob = breathe + (float)Math.Sin(m_ElephantGameState.animationTime * 0.8f) * 0.005f;
            m_ElephantGameState.trunkSwing = (float)Math.Sin(m_ElephantGameState.animationTime * 1.8f) * 0.2f;
            m_ElephantGameState.earFlap = (float)Math.Sin(m_ElephantGameState.animationTime * 1.2f) * 0.15f;

            if (m_ElephantGameState.currentState != 0)
            {
                float moveSpeed = m_ElephantGameState.walkSpeed;
                float radY = m_ElephantGameState.rotationY * (float)Math.PI / 180.0f;
                m_ElephantGameState.positionZ -= (float)Math.Cos(radY) * moveSpeed;
                m_ElephantGameState.positionX -= (float)Math.Sin(radY) * moveSpeed;
            }

            if (m_ElephantGameState.isTrumpeting)
            {
                m_ElephantGameState.trumpetProgress += 0.03f;
                if (m_ElephantGameState.trumpetProgress >= 1.0f)
                {
                    m_ElephantGameState.isTrumpeting = false;
                    m_ElephantGameState.trumpetProgress = 0.0f;
                }
            }
        }
        #endregion

        #region Geometric Primitive Drawing Methods
        public void DrawSphere(float radius, int slices, int stacks)
        {
            OpenGLView openGLView = new OpenGLView();
            const float PI = (float)Math.PI;

            for (int i = 0; i < stacks; ++i)
            {
                float phi1 = (i * PI) / stacks;
                float phi2 = ((i + 1) * PI) / stacks;

                openGLView.glBegin(Constants.GL_QUAD_STRIP);
                for (int j = 0; j <= slices; ++j)
                {
                    float theta = (2.0f * j * PI) / slices;
                    float x1 = radius * (float)Math.Sin(phi1) * (float)Math.Cos(theta);
                    float y1 = radius * (float)Math.Cos(phi1);
                    float z1 = radius * (float)Math.Sin(phi1) * (float)Math.Sin(theta);

                    float x2 = radius * (float)Math.Sin(phi2) * (float)Math.Cos(theta);
                    float y2 = radius * (float)Math.Cos(phi2);
                    float z2 = radius * (float)Math.Sin(phi2) * (float)Math.Sin(theta);

                    openGLView.glVertex3f(x1, y1, z1);
                    openGLView.glVertex3f(x2, y2, z2);
                }
                openGLView.glEnd();
            }
        }

        public void DrawCylinder(float radius, float height)
        {
            OpenGLView openGLView = new OpenGLView();
            const int slices = 16;
            const float PI = (float)Math.PI;

            openGLView.glBegin(Constants.GL_QUAD_STRIP);
            for (int i = 0; i <= slices; i++)
            {
                float angle = 2.0f * PI * i / slices;
                float x = (float)Math.Cos(angle) * radius;
                float z = (float)Math.Sin(angle) * radius;
                openGLView.glVertex3f(x, height, z);
                openGLView.glVertex3f(x, 0.0f, z);
            }
            openGLView.glEnd();

            openGLView.glBegin(Constants.GL_TRIANGLE_FAN);
            openGLView.glVertex3f(0.0f, height, 0.0f);
            for (int i = 0; i <= slices; i++)
            {
                float angle = 2.0f * PI * i / slices;
                float x = (float)Math.Cos(angle) * radius;
                float z = (float)Math.Sin(angle) * radius;
                openGLView.glVertex3f(x, height, z);
            }
            openGLView.glEnd();

            openGLView.glBegin(Constants.GL_TRIANGLE_FAN);
            openGLView.glVertex3f(0.0f, 0.0f, 0.0f);
            for (int i = slices; i >= 0; i--)
            {
                float angle = 2.0f * PI * i / slices;
                float x = (float)Math.Cos(angle) * radius;
                float z = (float)Math.Sin(angle) * radius;
                openGLView.glVertex3f(x, 0.0f, z);
            }
            openGLView.glEnd();
        }

        public void DrawEllipsoid(float radius, int slices, int stacks)
        {
            OpenGLView openGLView = new OpenGLView();
            const float PI = (float)Math.PI;

            for (int i = 0; i < stacks; ++i)
            {
                float phi1 = (i * PI) / stacks;
                float phi2 = ((i + 1) * PI) / stacks;

                openGLView.glBegin(Constants.GL_QUAD_STRIP);
                for (int j = 0; j <= slices; ++j)
                {
                    float theta = (2.0f * j * PI) / slices;
                    float x1 = radius * (float)Math.Sin(phi1) * (float)Math.Cos(theta);
                    float y1 = radius * (float)Math.Cos(phi1);
                    float z1 = radius * (float)Math.Sin(phi1) * (float)Math.Sin(theta);

                    float x2 = radius * (float)Math.Sin(phi2) * (float)Math.Cos(theta);
                    float y2 = radius * (float)Math.Cos(phi2);
                    float z2 = radius * (float)Math.Sin(phi2) * (float)Math.Sin(theta);

                    float nx1 = x1 / radius;
                    float ny1 = y1 / radius;
                    float nz1 = z1 / radius;

                    float nx2 = x2 / radius;
                    float ny2 = y2 / radius;
                    float nz2 = z2 / radius;

                    openGLView.glNormal3f(nx1, ny1, nz1);
                    openGLView.glVertex3f(x1, y1, z1);
                    openGLView.glNormal3f(nx2, ny2, nz2);
                    openGLView.glVertex3f(x2, y2, z2);
                }
                openGLView.glEnd();
            }
        }

        public void DrawRoundedCube(float size)
        {
            DrawSphere(size, 10, 10);
        }

        public void DrawSavannahEnvironmentWithPond()
        {
            OpenGLView openGLView = new OpenGLView();

            // Disable blending for the sky to ensure it covers everything
            openGLView.glDisable(0x0BE2); // GL_BLEND

            // Draw Sky first (gradient from light blue at horizon to darker blue at top)
            openGLView.glBegin(Constants.GL_QUADS);

            // Top of sky - darker blue
            openGLView.glColor3f(0.15f, 0.35f, 0.65f);
            openGLView.glVertex3f(-50.0f, 50.0f, -50.0f);
            openGLView.glVertex3f(50.0f, 50.0f, -50.0f);

            // Middle of sky - medium blue
            openGLView.glColor3f(0.35f, 0.65f, 0.95f);
            openGLView.glVertex3f(50.0f, 15.0f, -50.0f);
            openGLView.glVertex3f(-50.0f, 15.0f, -50.0f);

            // Bottom of sky - lighter blue near horizon
            openGLView.glColor3f(0.45f, 0.75f, 0.98f);
            openGLView.glVertex3f(50.0f, 0.0f, -50.0f);
            openGLView.glVertex3f(-50.0f, 0.0f, -50.0f);
            openGLView.glEnd();

            // Draw sky on back side
            openGLView.glBegin(Constants.GL_QUADS);
            openGLView.glColor3f(0.15f, 0.35f, 0.65f);
            openGLView.glVertex3f(-50.0f, 50.0f, 50.0f);
            openGLView.glVertex3f(50.0f, 50.0f, 50.0f);
            openGLView.glColor3f(0.35f, 0.65f, 0.95f);
            openGLView.glVertex3f(50.0f, 15.0f, 50.0f);
            openGLView.glVertex3f(-50.0f, 15.0f, 50.0f);
            openGLView.glColor3f(0.45f, 0.75f, 0.98f);
            openGLView.glVertex3f(50.0f, 0.0f, 50.0f);
            openGLView.glVertex3f(-50.0f, 0.0f, 50.0f);
            openGLView.glEnd();

            // Enable blending for other elements
            openGLView.glEnable(0x0BE2); // GL_BLEND
            openGLView.glBlendFunc(0x0300, 0x0303); // GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA

            // Ground
            openGLView.glBegin(Constants.GL_QUADS);
            openGLView.glColor3f(0.55f, 0.68f, 0.35f);
            openGLView.glVertex3f(-20.0f, 0.0f, -20.0f);
            openGLView.glVertex3f(20.0f, 0.0f, -20.0f);
            openGLView.glVertex3f(20.0f, 0.0f, 20.0f);
            openGLView.glVertex3f(-20.0f, 0.0f, 20.0f);

            // Grass patches
            Random rand = new Random();
            for (int patch = 0; patch < 6; patch++)
            {
                float x = (rand.Next(15) - 7.5f);
                float z = (rand.Next(15) - 7.5f);
                float size = 3.0f + rand.Next(5);
                openGLView.glColor3f(0.65f, 0.75f, 0.45f);
                openGLView.glVertex3f(x - size, 0.01f, z - size);
                openGLView.glVertex3f(x + size, 0.01f, z - size);
                openGLView.glVertex3f(x + size, 0.01f, z + size);
                openGLView.glVertex3f(x - size, 0.01f, z + size);
            }
            openGLView.glEnd();

            // Pond
            float pondCenterZ = 8.0f;
            float pondRadius = 4.5f;

            // Pond bank
            openGLView.glBegin(Constants.GL_QUADS);
            openGLView.glColor3f(0.75f, 0.65f, 0.45f);
            for (int i = 0; i < 32; i++)
            {
                float angle1 = i * (360.0f / 32) * (float)Math.PI / 180.0f;
                float angle2 = (i + 1) * (360.0f / 32) * (float)Math.PI / 180.0f;
                float bankWidth = 1.2f;

                float x1_outer = (float)Math.Cos(angle1) * (pondRadius + bankWidth);
                float z1_outer = (float)Math.Sin(angle1) * (pondRadius + bankWidth) + pondCenterZ;
                float x2_outer = (float)Math.Cos(angle2) * (pondRadius + bankWidth);
                float z2_outer = (float)Math.Sin(angle2) * (pondRadius + bankWidth) + pondCenterZ;

                float x1_inner = (float)Math.Cos(angle1) * pondRadius * 1.05f;
                float z1_inner = (float)Math.Sin(angle1) * pondRadius * 1.05f + pondCenterZ;
                float x2_inner = (float)Math.Cos(angle2) * pondRadius * 1.05f;
                float z2_inner = (float)Math.Sin(angle2) * pondRadius * 1.05f + pondCenterZ;

                openGLView.glVertex3f(x1_outer, 0.02f, z1_outer);
                openGLView.glVertex3f(x2_outer, 0.02f, z2_outer);
                openGLView.glVertex3f(x2_inner, 0.02f, z2_inner);
                openGLView.glVertex3f(x1_inner, 0.02f, z1_inner);
            }
            openGLView.glEnd();

            // Pond water layers
            openGLView.glBegin(Constants.GL_QUADS);
            for (int layer = 0; layer < 4; layer++)
            {
                float innerRadius = pondRadius * (1.0f - layer * 0.15f);
                float alpha = 0.5f - layer * 0.08f;
                float y = 0.01f + layer * 0.002f;
                openGLView.glColor4f(0.25f, 0.45f, 0.75f, alpha);

                for (int i = 0; i < 32; i++)
                {
                    float angle1 = i * (360.0f / 32) * (float)Math.PI / 180.0f;
                    float angle2 = (i + 1) * (360.0f / 32) * (float)Math.PI / 180.0f;

                    float x1 = (float)Math.Cos(angle1) * innerRadius;
                    float z1 = (float)Math.Sin(angle1) * innerRadius + pondCenterZ;
                    float x2 = (float)Math.Cos(angle2) * innerRadius;
                    float z2 = (float)Math.Sin(angle2) * innerRadius + pondCenterZ;

                    openGLView.glVertex3f(0.0f, y, pondCenterZ);
                    openGLView.glVertex3f(x1, y, z1);
                    openGLView.glVertex3f(x2, y, z2);
                    openGLView.glVertex3f(0.0f, y, pondCenterZ);
                }
            }
            openGLView.glEnd();

            // Pond ripples
            float rippleTime = m_ElephantGameState.animationTime;
            openGLView.glBegin(Constants.GL_LINES);
            openGLView.glColor4f(0.9f, 0.9f, 1.0f, 0.4f);

            for (int ring = 0; ring < 3; ring++)
            {
                float rippleRadius = 1.5f + ring * 0.8f + (float)Math.Sin(rippleTime * 2.0f + ring) * 0.3f;

                for (int i = 0; i < 24; i++)
                {
                    float angle1 = i * (360.0f / 24) * (float)Math.PI / 180.0f;
                    float angle2 = (i + 1) * (360.0f / 24) * (float)Math.PI / 180.0f;

                    float x1 = (float)Math.Cos(angle1) * rippleRadius;
                    float z1 = (float)Math.Sin(angle1) * rippleRadius + pondCenterZ;
                    float x2 = (float)Math.Cos(angle2) * rippleRadius;
                    float z2 = (float)Math.Sin(angle2) * rippleRadius + pondCenterZ;

                    openGLView.glVertex3f(x1, 0.015f, z1);
                    openGLView.glVertex3f(x2, 0.015f, z2);
                }
            }
            openGLView.glEnd();

            // Lily pads
            openGLView.glColor3f(0.3f, 0.6f, 0.3f);
            for (int lily = 0; lily < 8; lily++)
            {
                float angle = lily * 45.0f * (float)Math.PI / 180.0f;
                float radius = 2.0f + (lily % 3) * 0.4f;
                float x = (float)Math.Cos(angle) * radius;
                float z = (float)Math.Sin(angle) * radius + pondCenterZ;

                openGLView.glPushMatrix();
                openGLView.glTranslatef(x, 0.02f, z);

                openGLView.glBegin(Constants.GL_TRIANGLE_FAN);
                openGLView.glVertex3f(0.0f, 0.0f, 0.0f);

                for (int i = 0; i <= 12; i++)
                {
                    float lilyAngle = i * (360.0f / 12) * (float)Math.PI / 180.0f;
                    float lilyX = (float)Math.Cos(lilyAngle) * 0.4f;
                    float lilyZ = (float)Math.Sin(lilyAngle) * 0.4f;
                    openGLView.glVertex3f(lilyX, 0.0f, lilyZ);
                }
                openGLView.glEnd();
                openGLView.glPopMatrix();
            }

            // Sun
            openGLView.glPushMatrix();
            openGLView.glTranslatef(12.0f, 8.0f, -15.0f);

            openGLView.glColor3f(1.0f, 0.95f, 0.7f);
            openGLView.glBegin(Constants.GL_TRIANGLE_FAN);
            openGLView.glVertex3f(0.0f, 0.0f, 0.0f);

            for (int i = 0; i <= 24; i++)
            {
                float sunAngle = i * (360.0f / 24) * (float)Math.PI / 180.0f;
                float sunX = (float)Math.Cos(sunAngle) * 1.5f;
                float sunY = (float)Math.Sin(sunAngle) * 1.5f;
                openGLView.glVertex3f(sunX, sunY, 0.0f);
            }
            openGLView.glEnd();

            // Sun rays
            openGLView.glColor4f(1.0f, 0.9f, 0.6f, 0.3f);
            for (int ray = 0; ray < 12; ray++)
            {
                float rayAngle = ray * 30.0f * (float)Math.PI / 180.0f;
                float rayLength = 2.5f;

                openGLView.glBegin(Constants.GL_TRIANGLES);
                openGLView.glVertex3f(0.0f, 0.0f, 0.0f);

                float rayX1 = (float)Math.Cos(rayAngle - 0.1f) * rayLength;
                float rayY1 = (float)Math.Sin(rayAngle - 0.1f) * rayLength;
                float rayX2 = (float)Math.Cos(rayAngle + 0.1f) * rayLength;
                float rayY2 = (float)Math.Sin(rayAngle + 0.1f) * rayLength;

                openGLView.glVertex3f(rayX1, rayY1, 0.0f);
                openGLView.glVertex3f(rayX2, rayY2, 0.0f);
                openGLView.glEnd();
            }
            openGLView.glPopMatrix();

            // Trees around pond
            for (int tree = 0; tree < 10; tree++)
            {
                float angle = tree * 36.0f * (float)Math.PI / 180.0f;
                float baseDistance = 5.0f;
                float distance = baseDistance + (tree % 3) * 1.0f;
                float x = (float)Math.Cos(angle) * distance;
                float z = (float)Math.Sin(angle) * distance;

                if (Math.Sqrt((x * x) + ((z - pondCenterZ) * (z - pondCenterZ))) < pondRadius + 3.0f)
                    continue;

                float distanceFromOrigin = (float)Math.Sqrt(x * x + z * z);
                if (distanceFromOrigin < 3.0f)
                    continue;

                DrawAcaciaTree(x, z);
            }

            // Additional trees
            DrawAcaciaTree(4.0f, 2.0f);
            DrawAcaciaTree(5.0f, -1.0f);
            DrawAcaciaTree(-4.0f, 2.0f);
            DrawAcaciaTree(-5.0f, -1.0f);
            DrawAcaciaTree(0.0f, -6.0f);
            DrawAcaciaTree(3.0f, 6.0f);
            DrawAcaciaTree(-3.0f, 6.0f);

            // Grass clumps
            openGLView.glColor3f(0.45f, 0.58f, 0.3f);
            for (int clump = 0; clump < 50; clump++)
            {
                float x = (rand.Next(38) - 19.0f);
                float z = (rand.Next(38) - 19.0f);

                if (Math.Sqrt((x * x) + ((z - pondCenterZ) * (z - pondCenterZ))) < pondRadius + 1.5f)
                    continue;

                DrawGrassClump(x, z);
            }

            // Clouds
            float cloudTime = m_ElephantGameState.animationTime * 0.1f;
            DrawCloud(-8.0f + (float)Math.Sin(cloudTime) * 2.0f, 6.0f, -10.0f, 1.2f);
            DrawCloud(5.0f + (float)Math.Sin(cloudTime * 0.8f + 1.0f) * 1.5f, 5.5f, 5.0f, 0.9f);
            DrawCloud(-15.0f + (float)Math.Sin(cloudTime * 1.2f + 2.0f) * 1.8f, 7.0f, 8.0f, 1.5f);

            // Distant hills
            openGLView.glColor3f(0.4f, 0.5f, 0.3f);
            for (int hill = 0; hill < 3; hill++)
            {
                float hillZ = -18.0f - hill * 3.0f;
                float hillHeight = 3.0f + hill * 1.5f;

                openGLView.glBegin(Constants.GL_TRIANGLE_STRIP);
                for (int x = -20; x <= 20; x += 2)
                {
                    float heightVariation = (float)Math.Sin(x * 0.3f + hill * 2.0f) * 0.5f;
                    openGLView.glVertex3f(x, 0.0f, hillZ);
                    openGLView.glVertex3f(x, hillHeight + heightVariation, hillZ - 2.0f);
                }
                openGLView.glEnd();
            }

            openGLView.glDisable(0x0BE2); // GL_BLEND
        }

        private void DrawAcaciaTree(float x, float z)
        {
            OpenGLView openGLView = new OpenGLView();
            openGLView.glPushMatrix();
            openGLView.glTranslatef(x, 0.0f, z);

            // Trunk
            openGLView.glColor3f(0.45f, 0.35f, 0.25f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 1.2f, 0.0f);
            openGLView.glRotatef((float)Math.Sin(x * 0.5f) * 15.0f, 0, 1, 0);
            openGLView.glScalef(0.22f, 2.5f, 0.22f);
            DrawEllipsoid(1.0f, 10, 8);
            openGLView.glPopMatrix();

            // Branches
            openGLView.glColor3f(0.4f, 0.3f, 0.2f);
            for (int branch = 0; branch < 3; branch++)
            {
                float angle = branch * 120.0f * (float)Math.PI / 180.0f;
                float branchHeight = 2.5f + (branch % 2) * 0.3f;
                float branchLength = 1.6f + new Random().Next(6) * 0.1f;

                openGLView.glPushMatrix();
                openGLView.glTranslatef(0.0f, branchHeight, 0.0f);
                openGLView.glRotatef(angle * (180.0f / (float)Math.PI), 0, 1, 0);
                openGLView.glRotatef(20.0f, 1, 0, 0);

                openGLView.glPushMatrix();
                openGLView.glScalef(0.10f, 0.10f, branchLength);
                DrawEllipsoid(1.0f, 8, 6);
                openGLView.glPopMatrix();

                // Sub-branches
                for (int sub = 0; sub < 2; sub++)
                {
                    float subAngle = sub * 180.0f * (float)Math.PI / 180.0f;
                    float subLength = 0.6f + new Random().Next(3) * 0.1f;

                    openGLView.glPushMatrix();
                    openGLView.glTranslatef(0.0f, 0.0f, branchLength * 0.5f);
                    openGLView.glRotatef(subAngle * (180.0f / (float)Math.PI), 0, 0, 1);
                    openGLView.glRotatef(30.0f, 0, 1, 0);

                    openGLView.glPushMatrix();
                    openGLView.glScalef(0.05f, 0.05f, subLength);
                    DrawEllipsoid(1.0f, 6, 4);
                    openGLView.glPopMatrix();

                    openGLView.glPopMatrix();
                }
                openGLView.glPopMatrix();
            }

            // Main foliage
            openGLView.glColor3f(0.25f, 0.35f, 0.2f);
            openGLView.glPushMatrix();
            openGLView.glTranslatef(0.0f, 3.2f, 0.0f);
            openGLView.glScalef(1.8f, 0.6f, 1.8f);
            DrawEllipsoid(1.0f, 12, 10);
            openGLView.glPopMatrix();

            // Leaves
            openGLView.glColor3f(0.2f, 0.3f, 0.15f);
            Random leafRand = new Random();
            for (int leaf = 0; leaf < 5; leaf++)
            {
                float leafAngle = leaf * 72.0f * (float)Math.PI / 180.0f;
                float leafRadius = 0.8f + leafRand.Next(3) * 0.1f;
                float leafX = (float)Math.Cos(leafAngle) * leafRadius;
                float leafZ = (float)Math.Sin(leafAngle) * leafRadius;
                float leafY = 3.0f + leafRand.Next(2) * 0.1f;

                openGLView.glPushMatrix();
                openGLView.glTranslatef(leafX, leafY, leafZ);
                openGLView.glScalef(0.5f, 0.25f, 0.5f);
                DrawEllipsoid(1.0f, 6, 4);
                openGLView.glPopMatrix();
            }

            openGLView.glPopMatrix();
        }

        private void DrawGrassClump(float x, float z)
        {
            OpenGLView openGLView = new OpenGLView();
            openGLView.glPushMatrix();
            openGLView.glTranslatef(x, 0.0f, z);

            Random rand = new Random();
            for (int blade = 0; blade < 7; blade++)
            {
                float bladeAngle = blade * (360.0f / 7) * (float)Math.PI / 180.0f;
                float bladeX = (float)Math.Cos(bladeAngle) * 0.2f;
                float bladeZ = (float)Math.Sin(bladeAngle) * 0.2f;
                float bladeHeight = 0.3f + rand.Next(10) * 0.03f;
                float bladeWidth = 0.02f;
                float windSway = (float)Math.Sin(m_ElephantGameState.animationTime * 2.0f + x * 0.1f) * 0.1f;

                openGLView.glPushMatrix();
                openGLView.glTranslatef(bladeX, 0.0f, bladeZ);
                openGLView.glRotatef(windSway * 30.0f, 0, 1, 0);

                openGLView.glBegin(Constants.GL_TRIANGLES);
                openGLView.glVertex3f(-bladeWidth, 0.0f, 0.0f);
                openGLView.glVertex3f(bladeWidth, 0.0f, 0.0f);
                openGLView.glVertex3f(0.0f, bladeHeight, 0.0f);
                openGLView.glEnd();

                openGLView.glPopMatrix();
            }

            openGLView.glPopMatrix();
        }

        private void DrawCloud(float x, float y, float z, float scale)
        {
            OpenGLView openGLView = new OpenGLView();
            openGLView.glPushMatrix();
            openGLView.glTranslatef(x, y, z);
            openGLView.glScalef(scale, scale * 0.5f, scale);

            openGLView.glColor4f(1.0f, 1.0f, 1.0f, 0.8f);

            float[,] cloudParts = {
        { 0.0f, 0.0f, 0.0f },
        { 0.5f, 0.2f, 0.2f },
        { -0.4f, 0.1f, 0.3f },
        { 0.3f, -0.1f, -0.4f },
        { -0.3f, -0.2f, -0.2f }
    };

            for (int i = 0; i < 5; i++)
            {
                openGLView.glPushMatrix();
                openGLView.glTranslatef(cloudParts[i, 0], cloudParts[i, 1], cloudParts[i, 2]);
                openGLView.glScalef(0.7f, 0.5f, 0.7f);
                DrawEllipsoid(1.0f, 12, 10);
                openGLView.glPopMatrix();
            }

            openGLView.glPopMatrix();
        }

        #endregion
    }
}