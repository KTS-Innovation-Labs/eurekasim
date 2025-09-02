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


namespace TowerOfHanoiCS
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
        public int NumberOfRings { get; set; }
        public double AnimationSpeed { get; set; }
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

        public const string OBJECT_TYPES = "Cube,Ball,Pyramid,Aeroplane,Clock,Tower of Hanoi Solver";
        public const string OBJECT_TYPE_CUBE = "Cube";
        public const string OBJECT_TYPE_BALL = "Ball";
        public const string OBJECT_TYPE_PYRAMID = "Pyramid";
        public const string OBJECT_TYPE_AEROPLANE = "Aeroplane";
        public const string OBJECT_TYPE_CLOCK = "Clock";
        public const string OBJECT_TYPE_TOWEROFHANOI = "Tower of Hanoi Solver";


        public const string OBJECT_PATTERN_TYPES = "Rotate,Random Movement";
        public const string OBJECT_PATTERN_TYPE_ROTATE = "Rotate";
        public const string OBJECT_PATTERN_TYPE_RANDOM = "Random Movement";

        public const string TOWEROFHANOI_PROPERTIES_TITLE = "Tower of Hanoi | Properties";
        public const string TOWEROFHANOI_RINGS_TITLE = "Number of Rings";
        public const string TOWEROFHANOI_SPEED_TITLE = "Animation Speed";
        public const string TOWEROFHANOI_SPEED_OPTIONS = "0.25,0.50,0.75,1.00,1.25,1.50,1.75,2.00";

        public const string CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES = "Experiment Group 1 Properties";
        public const string CS_SAMPLE_DOC_SETTINGS_KEY = "Cs.Sample.Addin.Settings";
        public const string CS_SAMPLE_MAIN_EXPERIMENT_NAME = "TowerOfHanoiCS Experiment Simulation Demo";
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
        public int m_nNumberOfRings;
        public double m_dAnimationSpeed;

        public CObjectPattern()
        {
            m_strObjectType = "Cube";
            m_Color = Color.FromArgb(0, 0, 255);
            m_strSimulationPattern = "Rotate";
            m_lSimulationInterval = 100;
            m_nNumberOfRings = 3;
            m_dAnimationSpeed = 1.0;
        }
        public ExperimentInfo Serialize()
        {
            ExperimentInfo info = new ExperimentInfo();
            info.ObjectType = m_strObjectType;
            info.Colour = m_Color.ToArgb();
            info.SimulationPattern = m_strSimulationPattern;
            info.SimulationInterval = m_lSimulationInterval;
            info.NumberOfRings = m_nNumberOfRings;
            info.AnimationSpeed = m_dAnimationSpeed;
            return info;
        }
        public void DeSerialize(ExperimentInfo info)
        {
            m_strObjectType = info.ObjectType;
            m_Color = Color.FromArgb(info.Colour);
            m_strSimulationPattern = info.SimulationPattern;
            m_lSimulationInterval = info.SimulationInterval;
            m_nNumberOfRings = info.NumberOfRings;
            m_dAnimationSpeed = info.AnimationSpeed;
        }
        public void OnPropertyChanged(string GroupName, string PropertyName, string PropertyValue)
        {

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
            else if (PropertyName == Constants.TOWEROFHANOI_RINGS_TITLE)
            {
                int rings = Convert.ToInt32(PropertyValue);
                if (rings >= 3 && rings <= 8) // Basic validation
                {
                    m_nNumberOfRings = rings;
                }
            }
            else if (PropertyName == Constants.TOWEROFHANOI_SPEED_TITLE)
            {
                m_dAnimationSpeed = Convert.ToDouble(PropertyValue);
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

    public struct MoveCommand
    {
        public int from, to;
    };

    public class ObjectDemoExperiment
    {
        AddinSimulationManager m_pManager;
        List<CGraphPoints> m_PlotInfoArray = new List<CGraphPoints>();

        // Tower of Hanoi state
        List<Stack<int>> m_pegs;
        List<MoveCommand> m_moves;

        public CObjectPattern m_ObjectPattern = new CObjectPattern();
        public ObjectDemoExperiment(AddinSimulationManager pManager)
        {
            m_pManager = pManager;
            m_pegs = new List<Stack<int>>(3);
            m_moves = new List<MoveCommand>();
        }
        ~ObjectDemoExperiment()
        {
            m_pManager = null;
            m_PlotInfoArray = null;
            m_ObjectPattern = null;
            GC.Collect();
        }
        public void LoadAllExperiments()
        {
            int SessionID = (int)m_pManager.m_pAddin.m_lSessionID;
            ExperimentTreeView objExperimentTreeView = new ExperimentTreeView();
            try
            {
                objExperimentTreeView.DeleteAllExperiments(SessionID);
                objExperimentTreeView.SetRootNodeName(Constants.CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES, 1);
                objExperimentTreeView.AddExperiment(SessionID, Constants.OBJECT_3D_TREE_ROOT_TITLE, Constants.OBJECT_3D_TREE_LEAF_PATTERN_TITLE);
                objExperimentTreeView.AddExperiment(SessionID, Constants.OBJECT_3D_TREE_ROOT_TITLE, Constants.OBJECT_TYPE_TOWEROFHANOI);
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
            if (ExperimentGroup == Constants.OBJECT_3D_TREE_ROOT_TITLE)
            {
                m_ObjectPattern.m_strObjectType = ExperimentName;
                ShowObjectProperties();
            }
            else
            {
                m_pManager.ResetPropertyGrid();
            }
        }
        public void OnReloadExperiment(string ExperimentGroup, string ExperimentName)
        {
            if (m_pManager.m_bSimulationActive)
            {
                m_pManager.SetSimulationStatus(Constants.FALSE);
                Thread.Sleep(100); // Give the thread a moment to exit
            }

            if (ExperimentGroup == Constants.OBJECT_3D_TREE_ROOT_TITLE)
            {
                m_ObjectPattern.m_strObjectType = ExperimentName;
                if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_TOWEROFHANOI)
                {
                    ShowObjectProperties();
                }
                DrawObject(ExperimentName);
            }
        }

        public void ShowObjectProperties()
        {
            PropertyWindow PropertyWindow = new PropertyWindow();
            string strGroupName = string.Empty;
            try
            {
                PropertyWindow.RemoveAll();

                if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_TOWEROFHANOI)
                {
                    strGroupName = Constants.TOWEROFHANOI_PROPERTIES_TITLE;
                    PropertyWindow.AddPropertyGroup(strGroupName);

                    string strRings = m_ObjectPattern.m_nNumberOfRings.ToString();
                    PropertyWindow.AddPropertyItemAsString(strGroupName, Constants.TOWEROFHANOI_RINGS_TITLE, strRings, "Number of rings on the starting peg (3-8)");

                    string strSpeed = m_ObjectPattern.m_dAnimationSpeed.ToString("0.00");
                    PropertyWindow.AddPropertyItemsAsString(strGroupName, Constants.TOWEROFHANOI_SPEED_TITLE, Constants.TOWEROFHANOI_SPEED_OPTIONS, strSpeed, "Select the animation speed multiplier.", Constants.FALSE);
                }
                else
                {
                    strGroupName = Constants.OBJECT_PROPERTIES_TITLE;
                    PropertyWindow.AddPropertyGroup(strGroupName);
                    PropertyWindow.AddPropertyItemsAsString(strGroupName, Constants.OBJECT_TYPE_TITLE, Constants.OBJECT_TYPES, m_ObjectPattern.m_strObjectType, "Select the Object from the List", Constants.FALSE);
                    PropertyWindow.AddPropertyItemsAsString(strGroupName, Constants.OBJECT_SIMULATION_PATTERN_TITLE, Constants.OBJECT_PATTERN_TYPES, m_ObjectPattern.m_strSimulationPattern, "Select the Simulation Pattern", Constants.FALSE);
                    string strInterval = m_ObjectPattern.m_lSimulationInterval.ToString();
                    PropertyWindow.AddPropertyItemAsString(strGroupName, Constants.OBJECT_SIMULATION_INTERVAL_TITLE, strInterval, "Simulation Interval In Milli Seconds");
                }

                PropertyWindow.AddColorPropertyItem(strGroupName, Constants.OBJECT_COLOR_TITLE, Constants.HexConverter(m_ObjectPattern.m_Color), "Select the Color");

                PropertyWindow.EnableHeaderCtrl(Constants.FALSE);
                PropertyWindow.EnableDescriptionArea(Constants.TRUE);
                PropertyWindow.SetVSDotNetLook(Constants.TRUE);
                PropertyWindow.MarkModifiedProperties(Constants.TRUE, Constants.TRUE);

                if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_TOWEROFHANOI)
                {
                    SyncRibbonWithProperties();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error showing properties: " + ex.Message);
            }
        }

        public void SyncRibbonWithProperties()
        {
            if (m_pManager != null && m_pManager.m_pAddin != null)
            {
                // Sync number of rings
                string ringsText = m_ObjectPattern.m_nNumberOfRings.ToString();
                m_pManager.m_pAddin.SetRibbonControlText("NumRingsCombo", ringsText);

                // Sync animation speed
                string speedText = string.Format("{0:0.00}x", m_ObjectPattern.m_dAnimationSpeed);
                m_pManager.m_pAddin.SetRibbonControlText("SpeedCombo", speedText);
            }
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
            if (GroupName == Constants.OBJECT_PROPERTIES_TITLE || GroupName == Constants.TOWEROFHANOI_PROPERTIES_TITLE)
            {
                m_ObjectPattern.OnPropertyChanged(GroupName, PropertyName, PropertyValue);
                SyncRibbonWithProperties();
            }

            if (m_pManager.m_bSimulationActive)
            {
                m_pManager.SetSimulationStatus(Constants.FALSE);
                Thread.Sleep(100); // give thread time to exit
            }
            DrawScene();
        }

        public void DrawScene()
        {
            OnReloadExperiment(m_pManager.m_strExperimentGroup, m_pManager.m_strExperimentName);
        }
        public void DrawObject(string ExperimentName)
        {
            if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_CUBE)
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
            else if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_TOWEROFHANOI)
            {
                InitializePegs();
                DrawTowerOfHanoi();
            }
        }
        public void DrawCube()
        {
            ApplicationView applicationView = new ApplicationView();
            //We can use all the normal OpenGL API defined in the standard Opengl header file
            const float radius = 0.34f;

            applicationView.InitializeEnvironment(1);
            applicationView.BeginGraphicsCommands();

            //Set the Background Color
            applicationView.SetBkgColor(m_ObjectPattern.m_Color.R / (float)255.0, m_ObjectPattern.m_Color.G / (float)255.0, m_ObjectPattern.m_Color.B / (float)255.0, (float)1.0);

            try
            {
                applicationView.StartNewDisplayList();
            }
            catch (Exception)
            {

                return;
            }

            //Draw using Native IOpenGLView Interface
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
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(0.0f, 1.0f, 0.0f);
            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, 1.0f);
            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(1.0f, -1.0f, 1.0f);
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(0.0f, 1.0f, 0.0f);
            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(1.0f, -1.0f, 1.0f);
            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(1.0f, -1.0f, -1.0f);
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(0.0f, 1.0f, 0.0f);
            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(1.0f, -1.0f, -1.0f);
            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, -1.0f);
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(0.0f, 1.0f, 0.0f);
            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, -1.0f);
            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, 1.0f);
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, 1.0f);
            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(1.0f, -1.0f, 1.0f);
            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, -1.0f);
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
        public void StartSimulation(string ExperimentGroup, string ExperimentName)
        {
            if (m_ObjectPattern.m_strObjectType == Constants.OBJECT_TYPE_TOWEROFHANOI)
            {
                StartTowerOfHanoiSimulation();
            }
            else if (ExperimentGroup == Constants.OBJECT_3D_TREE_ROOT_TITLE && ExperimentName == Constants.OBJECT_3D_TREE_LEAF_PATTERN_TITLE)
            {
                StartObjectSimulation();
            }
        }
        public void StartObjectSimulation()
        {
            m_pManager.SetSimulationStatus(Constants.TRUE);
            ApplicationView applicationView = new ApplicationView();
            float Angle = (float)0.0, x = (float)0.0, y = (float)0.0, z = (float)0.0;
            int i = 0; //Indicate Random Movment after each iteration
            Random rnd = new Random();
            while (m_pManager.m_bSimulationActive)
            {
                applicationView.BeginGraphicsCommands();

                if (m_ObjectPattern.m_strSimulationPattern == Constants.OBJECT_PATTERN_TYPE_ROTATE)
                {
                    //Rotate the object with respect to Y Axis
                    x = (float)0.1; y = (float)1.0; z = (float)0.1;

                }
                else if (m_ObjectPattern.m_strSimulationPattern == Constants.OBJECT_PATTERN_TYPE_RANDOM)
                {
                    //Simulate Random Rotation
                    switch (i)
                    {
                        case 0:
                            x = (float)1.0; y = (float)0.1; z = (float)0.1;
                            break;
                        case 1:
                            x = (float)0.1; y = (float)1.0; z = (float)0.1;
                            break;
                        case 2:
                            x = (float)0.1; y = (float)0.1; z = (float)1.0;
                            break;
                    }
                    i = rnd.Next(0, 3);
                }

                if (!m_pManager.m_b3DMode)
                {
                    //Set the x y Rotation point to zero for two d view
                    x = 0;
                    y = 0;
                }
                applicationView.RotateObject(Angle, x, y, z);
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
            m_pManager.SetSimulationStatus(Constants.FALSE);
        }
        public void OnNextSimulationPoint(float Angle, float x, float y, float z)
        {
            string strStatus = string.Format("Simulation Points (Angle:{0:F3},X:{1:F3},Y:{2:F3},Z:{3:F3})\n",
                                            Angle, x, y, z);

            if (m_pManager.m_bShowExperimentalParamaters)
            {
                m_pManager.AddOperationStatus(strStatus);
            }

            if (m_pManager.m_bLogSimulationResultsToCSVFile)
            {
                string strLog = string.Format("{0:F3},{1:F3},{2:F3},{3:F3}\n", Angle, x, y, z);
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
                applicationChart.Set2dAxisRange(0, (int)EAxisPos.BottomAxis, 0, 365);
                applicationChart.Set2dAxisRange(0, (int)EAxisPos.LeftAxis, 0, 2);

                applicationChart.Set2dGraphInfo(1, "Angle Vs Y", "Angle(Degree)", "Y", Constants.TRUE);
                applicationChart.Set2dAxisRange(1, (int)EAxisPos.BottomAxis, 0, 365);
                applicationChart.Set2dAxisRange(1, (int)EAxisPos.LeftAxis, 0, 2);

                applicationChart.Set2dGraphInfo(2, "Angle Vs Z", "Angle(Degree)", "Z", Constants.TRUE);
                applicationChart.Set2dAxisRange(2, (int)EAxisPos.BottomAxis, 0, 365);
                applicationChart.Set2dAxisRange(2, (int)EAxisPos.LeftAxis, 0, 2);

                applicationChart.ResizeChartWindow();
            }
            catch (Exception)
            {
            }
        }
        public void DisplayObjectDemoGraph()
        {
            int iArraySize = (int)m_PlotInfoArray.Count;
            if (iArraySize < 2) return;

            int[] arraySize = { iArraySize, 2 };
            int[] lowerBounds = { 1, 1 };
            Array saX = Array.CreateInstance(typeof(double), arraySize, lowerBounds);
            Array saY = Array.CreateInstance(typeof(double), arraySize, lowerBounds);
            Array saZ = Array.CreateInstance(typeof(double), arraySize, lowerBounds);

            for (int i = 0; i < iArraySize; i++)
            {
                CGraphPoints pInfo = m_PlotInfoArray[i];
                saX.SetValue((double)pInfo.m_Angle, i + 1, 1);
                saY.SetValue((double)pInfo.m_Angle, i + 1, 1);
                saZ.SetValue((double)pInfo.m_Angle, i + 1, 1);
                saX.SetValue((double)pInfo.m_x, i + 1, 2);
                saY.SetValue((double)pInfo.m_y, i + 1, 2);
                saZ.SetValue((double)pInfo.m_z, i + 1, 2);
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
            catch (Exception) { return; }

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
                float theta = (float)(2.0 * Math.PI * i / segments);
                float x = (float)(radius * Math.Cos(theta));
                float y = (float)(radius * Math.Sin(theta));
                openGLView.glVertex2f(x + sx, y + sy);
            }
            openGLView.glEnd();
        }


        #region Tower Of Hanoi Implementation

        void DrawCylinder(OpenGLView pView, float radius, float height, int slices)
        {
            if (pView == null) return;

            const float PI = 3.1415926535f;
            float angle_step = 2.0f * PI / slices;

            pView.glBegin(Constants.GL_QUAD_STRIP);
            for (int i = 0; i <= slices; i++)
            {
                float angle = i * angle_step;
                float x = radius * (float)Math.Cos(angle);
                float z = radius * (float)Math.Sin(angle);
                pView.glNormal3f(x / radius, 0.0f, z / radius);
                pView.glVertex3f(x, height / 2.0f, z);
                pView.glVertex3f(x, -height / 2.0f, z);
            }
            pView.glEnd();

            pView.glBegin(Constants.GL_TRIANGLE_FAN);
            pView.glNormal3f(0.0f, 1.0f, 0.0f);
            pView.glVertex3f(0.0f, height / 2.0f, 0.0f);
            for (int i = 0; i <= slices; i++)
            {
                float angle = i * angle_step;
                pView.glVertex3f(radius * (float)Math.Cos(angle), height / 2.0f, radius * (float)Math.Sin(angle));
            }
            pView.glEnd();

            pView.glBegin(Constants.GL_TRIANGLE_FAN);
            pView.glNormal3f(0.0f, -1.0f, 0.0f);
            pView.glVertex3f(0.0f, -height / 2.0f, 0.0f);
            for (int i = slices; i >= 0; i--)
            {
                float angle = i * angle_step;
                pView.glVertex3f(radius * (float)Math.Cos(angle), -height / 2.0f, radius * (float)Math.Sin(angle));
            }
            pView.glEnd();
        }

        void DrawRing(OpenGLView pView, float innerRadius, float outerRadius, float height, int slices)
        {
            if (pView == null) return;
            const float PI = 3.1415926535f;
            float angle_step = 2.0f * PI / slices;

            pView.glBegin(Constants.GL_QUAD_STRIP);
            for (int i = 0; i <= slices; i++)
            {
                float angle = i * angle_step;
                float x = outerRadius * (float)Math.Cos(angle);
                float z = outerRadius * (float)Math.Sin(angle);
                pView.glNormal3f(x / outerRadius, 0.0f, z / outerRadius);
                pView.glVertex3f(x, height / 2.0f, z);
                pView.glVertex3f(x, -height / 2.0f, z);
            }
            pView.glEnd();

            pView.glBegin(Constants.GL_QUAD_STRIP);
            for (int i = 0; i <= slices; i++)
            {
                float angle = i * angle_step;
                float x = innerRadius * (float)Math.Cos(angle);
                float z = innerRadius * (float)Math.Sin(angle);
                pView.glNormal3f(-x / innerRadius, 0.0f, -z / innerRadius);
                pView.glVertex3f(x, -height / 2.0f, z);
                pView.glVertex3f(x, height / 2.0f, z);
            }
            pView.glEnd();

            pView.glBegin(Constants.GL_QUAD_STRIP);
            pView.glNormal3f(0.0f, 1.0f, 0.0f);
            for (int i = 0; i <= slices; i++)
            {
                float angle = i * angle_step;
                pView.glVertex3f(outerRadius * (float)Math.Cos(angle), height / 2.0f, outerRadius * (float)Math.Sin(angle));
                pView.glVertex3f(innerRadius * (float)Math.Cos(angle), height / 2.0f, innerRadius * (float)Math.Sin(angle));
            }
            pView.glEnd();

            pView.glBegin(Constants.GL_QUAD_STRIP);
            pView.glNormal3f(0.0f, -1.0f, 0.0f);
            for (int i = 0; i <= slices; i++)
            {
                float angle = i * angle_step;
                pView.glVertex3f(innerRadius * (float)Math.Cos(angle), -height / 2.0f, innerRadius * (float)Math.Sin(angle));
                pView.glVertex3f(outerRadius * (float)Math.Cos(angle), -height / 2.0f, outerRadius * (float)Math.Sin(angle));
            }
            pView.glEnd();
        }

        public void InitializePegs()
        {
            m_pegs.Clear();
            for (int i = 0; i < 3; i++)
            {
                m_pegs.Add(new Stack<int>());
            }

            for (int i = m_ObjectPattern.m_nNumberOfRings; i > 0; --i)
            {
                m_pegs[0].Push(i);
            }
        }

        void TowerOfHanoiSolver(int n, int from_peg, int to_peg, int aux_peg)
        {
            if (n == 0)
            {
                return;
            }
            TowerOfHanoiSolver(n - 1, from_peg, aux_peg, to_peg);
            m_moves.Add(new MoveCommand { from = from_peg, to = to_peg });
            TowerOfHanoiSolver(n - 1, aux_peg, to_peg, from_peg);
        }

        public void DrawTowerOfHanoi()
        {
            DrawTowerOfHanoi(-1, 0, 0, 0);
        }

        public void DrawTowerOfHanoi(int movingDisk, float mx, float my, float mz)
        {
            ApplicationView ApplicationView = new ApplicationView();
            ApplicationView.InitializeEnvironment(Constants.TRUE);
            ApplicationView.BeginGraphicsCommands();
            ApplicationView.SetBkgColor(m_ObjectPattern.m_Color.R / 255.0f, m_ObjectPattern.m_Color.G / 255.0f, m_ObjectPattern.m_Color.B / 255.0f, 1.0f);

            try
            {
                ApplicationView.StartNewDisplayList();
            }
            catch (Exception)
            {
                ApplicationView.EndGraphicsCommands();
                ApplicationView.Refresh();
                return;
            }

            OpenGLView OpenGLView = new OpenGLView();
            float baseWidth = 2.5f;
            float baseHeight = 0.1f;
            float pegHeight = 1.0f;
            float pegRadius = 0.02f;
            float pegSpacing = 0.8f;
            float diskThickness = 0.08f;
            float maxDiskRadius = 0.35f;
            float minDiskRadius = 0.1f;
            float[] pegPositions = { -pegSpacing, 0.0f, pegSpacing };
            float diskHoleRadius = pegRadius + 0.02f;

            OpenGLView.glPushMatrix();
            OpenGLView.glTranslatef(0.0f, -pegHeight / 2.0f - baseHeight / 2.0f, 0.0f);
            OpenGLView.glColor3f(0.4f, 0.2f, 0.1f);
            DrawCylinder(OpenGLView, baseWidth / 2.0f, baseHeight, 32);
            OpenGLView.glPopMatrix();

            for (int i = 0; i < 3; i++)
            {
                OpenGLView.glPushMatrix();
                OpenGLView.glTranslatef(pegPositions[i], 0.0f, 0.0f);
                OpenGLView.glColor3f(0.8f, 0.7f, 0.6f);
                DrawCylinder(OpenGLView, pegRadius, pegHeight, 16);
                OpenGLView.glPopMatrix();
            }

            for (int i = 0; i < 3; ++i)
            {
                if (i < m_pegs.Count)
                {
                    int diskCount = 0;
                    foreach (int diskID in m_pegs[i].Reverse())
                    {
                        if (diskID == movingDisk) continue;

                        float currentDiskY = -pegHeight / 2.0f + diskThickness / 2.0f + diskCount * diskThickness;
                        float radius = minDiskRadius + (maxDiskRadius - minDiskRadius) * ((float)(diskID - 1) / (m_ObjectPattern.m_nNumberOfRings - 1));

                        OpenGLView.glPushMatrix();
                        OpenGLView.glTranslatef(pegPositions[i], currentDiskY, 0.0f);

                        float r = (float)((diskID * 37) % 256) / 255.0f;
                        float g = (float)((diskID * 59) % 256) / 255.0f;
                        float b = (float)((diskID * 73) % 256) / 255.0f;
                        OpenGLView.glColor3f(r, g, b);

                        DrawRing(OpenGLView, diskHoleRadius, radius, diskThickness, 32);
                        OpenGLView.glPopMatrix();
                        diskCount++;
                    }
                }
            }

            if (movingDisk != -1)
            {
                float radius = minDiskRadius + (maxDiskRadius - minDiskRadius) * ((float)(movingDisk - 1) / (m_ObjectPattern.m_nNumberOfRings - 1));
                OpenGLView.glPushMatrix();
                OpenGLView.glTranslatef(mx, my, mz);
                float r = (float)((movingDisk * 37) % 256) / 255.0f;
                float g = (float)((movingDisk * 59) % 256) / 255.0f;
                float b = (float)((movingDisk * 73) % 256) / 255.0f;
                OpenGLView.glColor3f(r, g, b);
                DrawRing(OpenGLView, diskHoleRadius, radius, diskThickness, 32);
                OpenGLView.glPopMatrix();
            }

            ApplicationView.EndNewDisplayList();
            ApplicationView.EndGraphicsCommands();
            ApplicationView.Refresh();
        }

        public void StartTowerOfHanoiSimulation()
        {
            m_pManager.SetSimulationStatus(Constants.TRUE);
            m_pManager.SetStatusBarMessage("Simulation Started...", true);
            InitializePegs();
            m_moves.Clear();

            TowerOfHanoiSolver(m_ObjectPattern.m_nNumberOfRings, 0, 2, 1);

            foreach (var move in m_moves)
            {
                Application.DoEvents();

                if (!m_pManager.m_bSimulationActive)
                {
                    break;
                }
                AnimateMove(move.from, move.to);
            }

            if (m_pManager.m_bSimulationActive)
            {
                m_pManager.SetStatusBarMessage("Simulation Finished.", true);
            }
            m_pManager.SetSimulationStatus(Constants.FALSE);

            InitializePegs();
            DrawTowerOfHanoi();
        }

        void AnimateMove(int fromPeg, int toPeg)
        {
            if (fromPeg < 0 || fromPeg >= m_pegs.Count || m_pegs[fromPeg].Count == 0) return;

            int diskID = m_pegs[fromPeg].Peek();
            const float baseDuration = 500.0f;
            int animationDuration = (int)(baseDuration / m_ObjectPattern.m_dAnimationSpeed);
            if (animationDuration < 1) animationDuration = 1;

            float liftHeight = 0.8f;
            float pegHeight = 1.0f;
            float pegSpacing = 0.8f;
            float diskThickness = 0.08f;
            float[] pegPositions = { -pegSpacing, 0.0f, pegSpacing };

            float startX = pegPositions[fromPeg];
            float startY = -pegHeight / 2.0f + diskThickness / 2.0f + (m_pegs[fromPeg].Count - 1) * diskThickness;

            float endX = pegPositions[toPeg];
            float endY = -pegHeight / 2.0f + diskThickness / 2.0f + m_pegs[toPeg].Count * diskThickness;

            m_pegs[fromPeg].Pop();

            Func<bool> CheckStop = () =>
            {
                Application.DoEvents();
                return !m_pManager.m_bSimulationActive;
            };

            int startTime = Environment.TickCount;
            float progress;

            do
            {
                if (CheckStop()) { m_pegs[fromPeg].Push(diskID); return; }
                int elapsed = Environment.TickCount - startTime;
                progress = (float)elapsed / animationDuration;
                if (progress > 1.0f) progress = 1.0f;
                float currentY = startY + (liftHeight - startY) * progress;
                DrawTowerOfHanoi(diskID, startX, currentY, 0.0f);
            } while (progress < 1.0f);

            startTime = Environment.TickCount;
            do
            {
                if (CheckStop()) { m_pegs[fromPeg].Push(diskID); return; }
                int elapsed = Environment.TickCount - startTime;
                progress = (float)elapsed / animationDuration;
                if (progress > 1.0f) progress = 1.0f;
                float currentX = startX + (endX - startX) * progress;
                DrawTowerOfHanoi(diskID, currentX, liftHeight, 0.0f);
            } while (progress < 1.0f);

            startTime = Environment.TickCount;
            do
            {
                if (CheckStop()) { m_pegs[fromPeg].Push(diskID); return; }
                int elapsed = Environment.TickCount - startTime;
                progress = (float)elapsed / animationDuration;
                if (progress > 1.0f) progress = 1.0f;
                float currentY = liftHeight + (endY - liftHeight) * progress;
                DrawTowerOfHanoi(diskID, endX, currentY, 0.0f);
            } while (progress < 1.0f);

            m_pegs[toPeg].Push(diskID);
            DrawTowerOfHanoi();
        }

        #endregion
    }
}