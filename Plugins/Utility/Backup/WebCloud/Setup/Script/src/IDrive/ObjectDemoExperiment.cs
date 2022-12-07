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


namespace IDrive
{
    public class ExperimentInfo
    {
        public string RootText { get; set; }
        public string ExperimentGroup { get; set; }
        public string ExperimentName { get; set; }
        public string ExperimentType { get; set; }
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

        public const string OBJECT_TREE_ROOT_TITLE = "Object Demo";
        public const string OBJECT_TREE_LEAF_PATTERN_TITLE = "Object Pattern Demo";
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

        public const string PENDULUM_PROPERTIES_TITLE = "Select Pendulum | Properties";
        public const string PENDULUM_LENGTH = "Pendulum Length";
        public const string PENDULUM_BOB_RADIUS = "Bob Radius";
        public const string PENDULUM_MAX_SWING_ANGLE = "Max Swing Angle";
        public const string PENDULUM_BOB_MASS = "Mass of Bob";
        public const string PENDULUM_AMPITUDE = "Amplitude";
        public const string PENDULUM_COLOR_TITLE = "Select Background Color";

        public const string OBJECT_TYPES = "Cube,Ball,Pyramid,Aeroplane,Clock";
        public const string OBJECT_TYPE_CUBE = "Cube";
        public const string OBJECT_TYPE_BALL = "Ball";
        public const string OBJECT_TYPE_PYRAMID = "Pyramid";
        public const string OBJECT_TYPE_AEROPLANE = "Aeroplane";
        public const string OBJECT_TYPE_CLOCK = "Clock";

        public const string OBJECT_PATTERN_TYPES = "Rotate,Random Movement";
        public const string OBJECT_PATTERN_TYPE_ROTATE = "Rotate";
        public const string OBJECT_PATTERN_TYPE_RANDOM = "Random Movement";
        public const string CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES = "Experiment Group 1 Properties";
        public const string CS_SAMPLE_DOC_SETTINGS_KEY = "Cs.Sample.Addin.Settings";
        public const string CS_SAMPLE_MAIN_EXPERIMENT_NAME = "testcsharpthree Experiment Simulation Demo";
        public const string CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1 = "Experiment Group 1";

        

        public static int BOOL(bool bValue)
        {
            int res = bValue ? 1 : 0;
            return res;
        }
        public static ulong HexConverter(Color color)
        {
            string strCOLORREF= "0x00"+color.B.ToString("X2") + 
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

    public class PhysicsExperiments
    {
        public Color m_Color = new Color();
        public long m_lSimulationInterval;
        public double m_Length;
        public double m_BobRadius;
        public int m_MaxAngle;
        public PhysicsExperiments()
        {
            m_Color = Color.FromArgb(0, 0, 255);
            m_lSimulationInterval = 100;
            m_Length = 1.0;
            m_BobRadius = 0.1;
            m_MaxAngle = 45;
        }

        public void OnPropertyChanged(string GroupName, string PropertyName, string PropertyValue)
        {
            
            if (GroupName != Constants.PENDULUM_PROPERTIES_TITLE)
                return;
            else if (PropertyName == Constants.PENDULUM_LENGTH)
                m_Length = Convert.ToDouble(PropertyValue);
            else if (PropertyName == Constants.PENDULUM_BOB_RADIUS)
                m_BobRadius = Convert.ToDouble(PropertyValue);
            else if (PropertyName == Constants.PENDULUM_COLOR_TITLE)
                m_Color = Color.FromArgb(Convert.ToInt32(PropertyValue));
            else if (PropertyName == Constants.OBJECT_SIMULATION_INTERVAL_TITLE)
                m_lSimulationInterval = Convert.ToInt64(PropertyValue);
            else if (PropertyName == Constants.PENDULUM_MAX_SWING_ANGLE)
                m_MaxAngle = Convert.ToInt16(PropertyValue);
            else
                return;
        }
        
    }
    public class CObjectPattern
    {
        public string m_strObjectType;
        public Color m_Color=new Color();
        public string m_strSimulationPattern;
        public long m_lSimulationInterval;
        public CObjectPattern()
        {
            m_strObjectType = "Cube";
            m_Color = Color.FromArgb(0, 0, 255);
            m_strSimulationPattern = "Rotate";
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
            m_strObjectType= info.ObjectType;
            m_Color = Color.FromArgb(info.Colour);
            m_strSimulationPattern = info.SimulationPattern;
            m_lSimulationInterval= info.SimulationInterval;
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
    public class ObjectDemoExperiment
    {
        AddinSimulationManager m_pManager;
        List<CGraphPoints> m_PlotInfoArray = new List<CGraphPoints>();
        public CObjectPattern m_ObjectPattern=new CObjectPattern();
        public PhysicsExperiments m_objPhysicExp = new PhysicsExperiments();
        float m_PendulumAngle = 0.0f;
        public ObjectDemoExperiment(AddinSimulationManager pManager)
        {
            m_pManager = pManager;
        }
        public void LoadAllExperiments()
        {
            int SessionID = (int)m_pManager.m_pAddin.m_lSessionID;
            ExperimentTreeView objExperimentTreeView = new ExperimentTreeView();
            try
            {
                objExperimentTreeView.DeleteAllExperiments(SessionID);
                objExperimentTreeView.SetRootNodeName(Constants.CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES, 1);

                objExperimentTreeView.AddExperiment(SessionID, Constants.MECHANICS_TREE_ROOT_TITLE, Constants.MECHANICS_TREE_SIMPLE_PENDULUM_TITLE);
                //objExperimentTreeView.AddExperiment(SessionID, Constants.MECHANICS_TREE_ROOT_TITLE, Constants.MECHANICS_TREE_PROJECTILE_MOTION_TITLE);
                //objExperimentTreeView.AddExperiment(SessionID, Constants.MECHANICS_TREE_ROOT_TITLE, Constants.MECHANICS_TREE_PLANETORY_MOTION_TITLE);
                objExperimentTreeView.AddExperiment(SessionID, Constants.OBJECT_TREE_ROOT_TITLE, Constants.OBJECT_TREE_LEAF_PATTERN_TITLE);
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
            if (ExperimentGroup == Constants.OBJECT_TREE_ROOT_TITLE && ExperimentName == Constants.OBJECT_TREE_LEAF_PATTERN_TITLE)
            {
                ShowObjectProperties();
            }
            else if (ExperimentGroup == Constants.MECHANICS_TREE_ROOT_TITLE && ExperimentName == Constants.MECHANICS_TREE_SIMPLE_PENDULUM_TITLE)
            {
                ShowPhysicsExpProperties();
            }
            else
            {
                m_pManager.ResetPropertyGrid();
            }
        }


        public void OnReloadExperiment(string ExperimentGroup, string ExperimentName)
        {
            if (ExperimentGroup == Constants.OBJECT_TREE_ROOT_TITLE)
            {
                DrawObject(ExperimentName);
            }
            else if(ExperimentGroup == Constants.MECHANICS_TREE_ROOT_TITLE)
            {
                DrawExpSetup(ExperimentName);
            }
            else
            {

            }
        }

        private void DrawExpSetup(string ExperimentName)
        {
            if(ExperimentName== Constants.MECHANICS_TREE_SIMPLE_PENDULUM_TITLE)
            {
                DrawSimplePendulum();
            }
            else if(ExperimentName == Constants.MECHANICS_TREE_PROJECTILE_MOTION_TITLE)
            {
                
            }
            else if (ExperimentName == Constants.MECHANICS_TREE_PLANETORY_MOTION_TITLE)
            {

            }
            else
            {

            }
        }

        private void DrawSimplePendulum()
        {
            ApplicationView applicationView = new ApplicationView();
            applicationView.InitializeEnvironment(1);
            applicationView.BeginGraphicsCommands();
            float pendulumLen = (float)m_objPhysicExp.m_Length;
            int SECTIONS = 7;
            double RADIUS = m_objPhysicExp.m_BobRadius;
            //Set the Background Color
            applicationView.SetBkgColor(m_objPhysicExp.m_Color.R / 255,
                                        m_objPhysicExp.m_Color.G / 255,
                                        m_objPhysicExp.m_Color.B / 255, 1);

            applicationView.StartNewDisplayList();
            applicationView.SetColorf(1.0f, 1.0f, 1.0f);
            OpenGLView openGLView = new OpenGLView();
            openGLView.glColor3f(1.0f, 1.0f, 1.0f);
            openGLView.glBegin(Constants.GL_LINES);
            openGLView.glVertex2f(0, pendulumLen);
            openGLView.glVertex2f(0, 0);
            openGLView.glEnd();
            if (m_pManager.m_b3DMode)
                applicationView.DrawSphere(RADIUS, SECTIONS, SECTIONS);
            else
            {
                openGLView.glBegin(Constants.GL_LINE_LOOP);
                int segments = 100;
                for(int i=0;i< segments;i++)
                {
                    double theta = 2.0 * Math.PI * i / segments; //get the current angle
                    float x = (float)(RADIUS * Math.Cos(theta));
                    float y = (float)(RADIUS * Math.Sin(theta));
                    openGLView.glVertex2f(x + 0, y + 0);
                }
                openGLView.glEnd();
            }
            applicationView.EndNewDisplayList();
            applicationView.EndGraphicsCommands();
            applicationView.Refresh();
        }
        private void ShowPhysicsExpProperties()
        {
            PropertyWindow objPropertyWindow = new PropertyWindow();
            string strGroupName = "";
        try
            {
                objPropertyWindow.RemoveAll();
                strGroupName = Constants.PENDULUM_PROPERTIES_TITLE;
                objPropertyWindow.AddPropertyGroup(strGroupName);
                string strInterval = m_objPhysicExp.m_lSimulationInterval.ToString();
                objPropertyWindow.AddPropertyItemAsString(strGroupName, Constants.PENDULUM_LENGTH, m_objPhysicExp.m_Length.ToString(), "Length in mm");
                objPropertyWindow.AddColorPropertyItem(strGroupName, Constants.PENDULUM_COLOR_TITLE, Constants.HexConverter(m_objPhysicExp.m_Color), "Select the Color");
                objPropertyWindow.AddPropertyItemAsString(strGroupName, Constants.OBJECT_SIMULATION_INTERVAL_TITLE, strInterval, "Simulation Interval In Milli Seconds");
                objPropertyWindow.AddPropertyItemAsString(strGroupName, Constants.PENDULUM_BOB_RADIUS,m_objPhysicExp.m_BobRadius.ToString(), "Length in mm");
                objPropertyWindow.AddPropertyItemAsString(strGroupName, Constants.PENDULUM_MAX_SWING_ANGLE, m_objPhysicExp.m_MaxAngle.ToString(), "Maximum angle(in degree) of Swinging from normal");
                objPropertyWindow.EnableHeaderCtrl(0);
                objPropertyWindow.EnableDescriptionArea(1);
                objPropertyWindow.SetVSDotNetLook(1);
                objPropertyWindow.MarkModifiedProperties(1, 1);
            }
        catch( Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                objPropertyWindow.AddPropertyItemsAsString(strGroupName, Constants.OBJECT_TYPE_TITLE, Constants.OBJECT_TYPES, m_ObjectPattern.m_strObjectType, "Select the Object from the List", Constants.FALSE);
                try
                {
                    objPropertyWindow.AddColorPropertyItem(strGroupName, Constants.OBJECT_COLOR_TITLE, Constants.HexConverter(m_ObjectPattern.m_Color), "Select the Color");
                }
                catch (Exception)
                {
                }
                
                objPropertyWindow.AddPropertyItemsAsString(strGroupName, Constants.OBJECT_SIMULATION_PATTERN_TITLE, Constants.OBJECT_PATTERN_TYPES, m_ObjectPattern.m_strSimulationPattern, "Select the Simulation Pattern", Constants.FALSE);
                string strInterval= m_ObjectPattern.m_lSimulationInterval.ToString();

                objPropertyWindow.AddPropertyItemAsString(strGroupName, Constants.OBJECT_SIMULATION_INTERVAL_TITLE, strInterval, "Simulation Interval In Milli Seconds");


                objPropertyWindow.EnableHeaderCtrl(Constants.FALSE);
                objPropertyWindow.EnableDescriptionArea(Constants.TRUE);
                objPropertyWindow.SetVSDotNetLook(Constants.TRUE);
                objPropertyWindow.MarkModifiedProperties(Constants.TRUE, Constants.TRUE);
            }
            catch (Exception)
            {
                
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
            if (GroupName == Constants.OBJECT_PROPERTIES_TITLE)
            {
                m_ObjectPattern.OnPropertyChanged(GroupName, PropertyName, PropertyValue);
            }
            else if (GroupName == Constants.PENDULUM_PROPERTIES_TITLE)
            {
                m_objPhysicExp.OnPropertyChanged(GroupName, PropertyName, PropertyValue);
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

            //Set the Inner Sphere Color


            applicationView.EndNewDisplayList();
            applicationView.EndGraphicsCommands();
            applicationView.Refresh();
        }
        public void DrawBall()
        {
            ApplicationView applicationView = new ApplicationView();
            applicationView.InitializeEnvironment(Constants.TRUE);
            applicationView.BeginGraphicsCommands();

            //Set the Background Color
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
            //Draw One more spehere inside it

            applicationView.SetColorf(1.0f, 1.0f, 1.0f);

            applicationView.DrawSphere(RADIUS / 1.5, SECTIONS, SECTIONS);

            applicationView.EndNewDisplayList();
            applicationView.EndGraphicsCommands();
            applicationView.Refresh();
        }
        public void DrawPyramid()
        {
            ApplicationView applicationView = new ApplicationView();
            //We can use all the normal OpenGL API defined in the standard Opengl header file
            //const float radius = 0.34f;
            applicationView.ResetScene();
            applicationView.InitializeEnvironment(Constants.TRUE);
            applicationView.BeginGraphicsCommands();

            //Set the Background Color
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

            //Draw using Native IOpenGLView Interface
            OpenGLView openGLView = new OpenGLView();
            openGLView.glTranslatef(0.01f, 0.0f, 0.01f);
            openGLView.glColor3f(0.0f, 0.4f, 0.8f);

            // We're telling OpenGL that we want to render triangles.
            openGLView.glBegin(Constants.GL_TRIANGLES);

            // Each of the pyramid's faces will have 3 vertices.
            // We'll start drawing at the top, then go down to the bottom left,
            // then to the right.

            // When we start our next triangle, we're going to be going back to
            // the top-middle. Imagine drawing a pyramid without ever lifting your
            // pen up.

            // New Triangle - Front
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(0.0f, 1.0f, 0.0f);

            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, 1.0f);

            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(1.0f, -1.0f, 1.0f);

            // New Triangle - Right
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(0.0f, 1.0f, 0.0f);

            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(1.0f, -1.0f, 1.0f);

            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(1.0f, -1.0f, -1.0f);

            // New Triangle - Back
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(0.0f, 1.0f, 0.0f);

            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(1.0f, -1.0f, -1.0f);

            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, -1.0f);

            // New Triangle - left
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(0.0f, 1.0f, 0.0f);

            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, -1.0f);

            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, 1.0f);

            // New Triangle - Bottom 1
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, 1.0f);

            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glVertex3f(1.0f, -1.0f, 1.0f);

            openGLView.glColor3f(0.0f, 0.0f, 1.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, -1.0f);

            // New Triangle - Bottom 2
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glVertex3f(-1.0f, -1.0f, -1.0f);      // Note: we're starting from the last point
                                                              // of the previous triangle.

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

            //We can use all the normal OpenGL API defined in the standard Opengl header file
            //const float radius = 0.34f;

            applicationView.InitializeEnvironment(Constants.TRUE);
            applicationView.BeginGraphicsCommands();

            //Set the Background Color
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
            //Draw using Native IopenGLView Interface
            OpenGLView openGLView =new OpenGLView();

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
            if (ExperimentGroup == Constants.OBJECT_TREE_ROOT_TITLE && ExperimentName == Constants.OBJECT_TREE_LEAF_PATTERN_TITLE)
            {
                StartObjectSimulation();
            }
            else if (ExperimentGroup == Constants.MECHANICS_TREE_ROOT_TITLE && ExperimentName == Constants.MECHANICS_TREE_SIMPLE_PENDULUM_TITLE)
            {
                StartPendulumSimulation();
            }
            else
            {

            }
        }

        private void StartPendulumSimulation()
        {
            m_pManager.SetSimulationStatus(1);
            ApplicationView applicationView = new ApplicationView();
            float pendulumLen = (float)m_objPhysicExp.m_Length;
            float Angle = 5.0f;
            while (m_pManager.m_bSimulationActive)
            {
                applicationView.BeginGraphicsCommands();
                applicationView.TranslateObject(0.0f, pendulumLen, 0.0f);
                applicationView.RotateObject(Angle, 0.0f, 0.0f, 1.0f);
                applicationView.TranslateObject(0.0f, -1 * pendulumLen, 0.0f);
                applicationView.EndGraphicsCommands();
                applicationView.Refresh();
                m_PendulumAngle = m_PendulumAngle + Angle;
                if (m_PendulumAngle >= m_objPhysicExp.m_MaxAngle)
                    Angle *= -1;
                else if (m_PendulumAngle <= -1* m_objPhysicExp.m_MaxAngle)
                    Angle *= -1;
                float x = (float)(pendulumLen * Math.Sin(m_PendulumAngle * Math.PI / 180));
                float y = (float)(pendulumLen - pendulumLen * Math.Cos(m_PendulumAngle * Math.PI / 180));
                float X= (float)Math.Round(x * 100f) / 100f;
                float Y = (float)Math.Round(y * 100f) / 100f;
                OnNextSimulationPoint(m_PendulumAngle, X, Y, 0.0f);
                Thread.Sleep((int)m_objPhysicExp.m_lSimulationInterval); //Sleep for 100 Milli seconds
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
                //Rotate the Object with the specified angle
                applicationView.RotateObject(Angle, x, y, z);
                applicationView.EndGraphicsCommands();
                applicationView.Refresh();
                //Process the Results
                OnNextSimulationPoint(Angle, x, y, z);

                Angle = Angle + 5;
                if (Angle > 360)
                {
                    Angle = 0;
                }
                Thread.Sleep((int)m_ObjectPattern.m_lSimulationInterval); //Sleep for 500 Milli seconds
            }
        }
        public void OnNextSimulationPoint(float Angle, float x, float y, float z)
        {
            string strStatus =string.Format("Simulation Points (Angle:{0},X:{1},Y:{2},Z:{3})\n",
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

            string strStatus= string.Format("Plot Data Points Count ={0}", m_PlotInfoArray.Count);

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

                applicationChart.Set2dGraphInfo(0, "Angle Vs X", "Angle(Degree)","X", Constants.TRUE);
                applicationChart.Set2dAxisRange(0, (int)EAxisPos.BottomAxis, 0, 365);
                applicationChart.Set2dAxisRange(0, (int)EAxisPos.LeftAxis, 0, 2);

                applicationChart.Set2dGraphInfo(1, "Angle Vs Y", "Angle(Degree)","Y", Constants.TRUE);
                applicationChart.Set2dAxisRange(1, (int)EAxisPos.BottomAxis, 0, 365);
                applicationChart.Set2dAxisRange(1, (int)EAxisPos.LeftAxis, 0, 2);

                applicationChart.Set2dGraphInfo(2, "Angle Vs Z", "Angle(Degree)","Z", Constants.TRUE);
                applicationChart.Set2dAxisRange(2, (int)EAxisPos.BottomAxis, 0, 365);
                applicationChart.Set2dAxisRange(2, (int)EAxisPos.LeftAxis, 0, 2);

                applicationChart.ResizeChartWindow();
            }
            catch(Exception)
            {

            }
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
                index[0] = i+1;
                index[1] = 1;
                double pValue = pInfo.m_Angle;
                saX.SetValue(pValue, index[0], index[1]);
                saY.SetValue(pValue, index[0], index[1]);
                saZ.SetValue(pValue, index[0], index[1]);

                //Now plot the other Y Value for each data
                index[1] = 2;
                pValue = pInfo.m_x; //set the X 
                saX.SetValue(pValue, index[0], index[1]);

                pValue = pInfo.m_y; //set the Y
                saY.SetValue(pValue, index[0], index[1]);

                pValue = pInfo.m_z; //set the Z
                saZ.SetValue(pValue, index[0], index[1]);
                i=i+1;
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
                catch
                {

                }

            }
        }
    }
}
