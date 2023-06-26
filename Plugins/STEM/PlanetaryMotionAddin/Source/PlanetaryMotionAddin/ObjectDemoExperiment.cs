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


namespace PlanetaryMotionAddin
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
        public const uint GL_SPHERE = 0x0010;

        public const string OBJECT_TREE_ROOT_TITLE = "Object Demo";

        public const string MECHANICS_TREE_ROOT_TITLE = "Physics";
        public const string MECHANICS_TREE_SOLAR_SYSTEM = "Solar System";


        public const string SOLAR_PROPERTIES_TITLE = "Select Solar | Properties";
        public const string SUN_COLOR_TITLE = "Sun Color";
        public const string MERCURY_COLOR_TITLE = "Mercury Color";
        public const string VENUS_COLOR_TITLE = "Venus Color";
        public const string EARTH_COLOR_TITLE = "Earth Color";
        public const string MARS_COLOR_TITLE = "Mars Color";
        public const string SOLAR_COLOR_TITLE = "Select Background Color";
        public const string SOLAR_SIMULATION_PATTERN_TITLE = "Simulation Pattern";
        public const string SOLAR_SIMULATION_INTERVAL_TITLE = "Simulation Interval";

        public const string OBJECT_TYPES = "SolarSystem";
        public const string OBJECT_TYPE_SOLAR_SYSTEM = "SolarSystem";

        public const string OBJECT_PATTERN_TYPES = "Rotate,Random Movement";
        public const string OBJECT_PATTERN_TYPE_ROTATE = "Rotate";
        public const string OBJECT_PATTERN_TYPE_RANDOM = "Random Movement";
        public const string CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES = "Experiment Group 1 Properties";
        public const string CS_SAMPLE_DOC_SETTINGS_KEY = "Cs.Sample.Addin.Settings";
        public const string CS_SAMPLE_MAIN_EXPERIMENT_NAME = "PlanetaryMotionAddin Experiment Simulation Demo";
        public const string CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1 = "Experiment Group 1";

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


    public class SolarExperiments
    {
        public string m_strObjectType;
        public Color s_Color = new Color();
        public Color sun_Color = new Color();
        public Color mercury_Color = new Color();
        public Color venus_Color = new Color();
        public Color earth_Color = new Color();
        public Color mars_Color = new Color();
        public long m_lSimulationInterval;
        public string m_strSimulationPattern;

        public SolarExperiments()
        {
            s_Color = Color.FromArgb(100, 200, 255); //black
            sun_Color = Color.FromArgb(255, 255, 0); //yellow
            mercury_Color = Color.FromArgb(255, 0, 0);//red
            venus_Color = Color.FromArgb(160, 32, 240);//purple    
            earth_Color = Color.FromArgb(0, 128, 0);//green
            mars_Color = Color.FromArgb(0, 255, 255);//cyan
            m_lSimulationInterval = 100;
        }

        public ExperimentInfo Serialize()
        {
            ExperimentInfo info = new ExperimentInfo();
            info.ObjectType = m_strObjectType;
            info.Colour = s_Color.ToArgb();

            info.SimulationPattern = m_strSimulationPattern;
            info.SimulationInterval = m_lSimulationInterval;
            return info;
        }
        public void DeSerialize(ExperimentInfo info)
        {
            m_strObjectType = info.ObjectType;
            s_Color = Color.FromArgb(info.Colour);
            m_strSimulationPattern = info.SimulationPattern;
            m_lSimulationInterval = info.SimulationInterval;
        }
        public void OnPropertyChanged(string GroupName, string PropertyName, string PropertyValue)
        {

            if (GroupName != Constants.SOLAR_PROPERTIES_TITLE)
                return;
            else if (PropertyName == Constants.SOLAR_SIMULATION_INTERVAL_TITLE)
                m_lSimulationInterval = Convert.ToInt64(PropertyValue);
            else if (PropertyName == Constants.SOLAR_COLOR_TITLE)
                s_Color = Color.FromArgb(Convert.ToInt32(PropertyValue));
            else if (PropertyName == Constants.SUN_COLOR_TITLE)
                sun_Color = Color.FromArgb(Convert.ToInt32(PropertyValue));
            else if (PropertyName == Constants.MERCURY_COLOR_TITLE)
                mercury_Color = Color.FromArgb(Convert.ToInt32(PropertyValue));
            else if (PropertyName == Constants.VENUS_COLOR_TITLE)
                venus_Color = Color.FromArgb(Convert.ToInt32(PropertyValue));
            else if (PropertyName == Constants.EARTH_COLOR_TITLE)
                earth_Color = Color.FromArgb(Convert.ToInt32(PropertyValue));
            else if (PropertyName == Constants.MARS_COLOR_TITLE)
                mars_Color = Color.FromArgb(Convert.ToInt32(PropertyValue));

            else
                return;
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
        public SolarExperiments m_objSolarExp = new SolarExperiments();
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
                objExperimentTreeView.AddExperiment(SessionID, Constants.MECHANICS_TREE_ROOT_TITLE, Constants.MECHANICS_TREE_SOLAR_SYSTEM);
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
            if (ExperimentGroup == Constants.MECHANICS_TREE_ROOT_TITLE && ExperimentName == Constants.MECHANICS_TREE_SOLAR_SYSTEM)
            {
                ShowSolarProperties();
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
            else if (ExperimentGroup == Constants.MECHANICS_TREE_ROOT_TITLE)
            {
                DrawExpSetup(ExperimentName);
            }
            else
            {

            }
        }
        private void DrawExpSetup(string ExperimentName)
        {

            if (ExperimentName == Constants.MECHANICS_TREE_SOLAR_SYSTEM)
            {
                DrawSolarSystem();

            }
            else
            {

            }
        }
        public void ShowSolarProperties()
        {
            PropertyWindow objPropertyWindow = new PropertyWindow();
            string strGroupName = "";
            try
            {
                objPropertyWindow.RemoveAll();
                strGroupName = Constants.SOLAR_PROPERTIES_TITLE;
                objPropertyWindow.AddPropertyGroup(strGroupName);
                string strInterval = m_objSolarExp.m_lSimulationInterval.ToString();

                objPropertyWindow.AddPropertyItemAsString(strGroupName, Constants.SOLAR_SIMULATION_INTERVAL_TITLE, strInterval, "Simulation Interval In Milli Seconds");
                objPropertyWindow.AddColorPropertyItem(strGroupName, Constants.SOLAR_COLOR_TITLE, Constants.HexConverter(m_objSolarExp.s_Color), "Select Background Color");

                objPropertyWindow.AddColorPropertyItem(strGroupName, Constants.SUN_COLOR_TITLE, Constants.HexConverter(m_objSolarExp.sun_Color), "Select the Color");

                objPropertyWindow.AddColorPropertyItem(strGroupName, Constants.MERCURY_COLOR_TITLE, Constants.HexConverter(m_objSolarExp.mercury_Color), "Select the Color");

                objPropertyWindow.AddColorPropertyItem(strGroupName, Constants.VENUS_COLOR_TITLE, Constants.HexConverter(m_objSolarExp.venus_Color), "Select the Color");

                objPropertyWindow.AddColorPropertyItem(strGroupName, Constants.EARTH_COLOR_TITLE, Constants.HexConverter(m_objSolarExp.earth_Color), "Select the Color");

                objPropertyWindow.AddColorPropertyItem(strGroupName, Constants.MARS_COLOR_TITLE, Constants.HexConverter(m_objSolarExp.mars_Color), "Select the Color");

                objPropertyWindow.EnableHeaderCtrl(Constants.FALSE);
                objPropertyWindow.EnableDescriptionArea(Constants.TRUE);
                objPropertyWindow.SetVSDotNetLook(Constants.TRUE);
                objPropertyWindow.MarkModifiedProperties(Constants.TRUE, Constants.TRUE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ExperimentInfo Serialize()
        {
            return m_objSolarExp.Serialize();
        }

        public void DeSerialize(ExperimentInfo info)
        {
            m_objSolarExp.DeSerialize(info);
        }
        public void OnPropertyChanged(string GroupName, string PropertyName, string PropertyValue)
        {

            if (GroupName == Constants.SOLAR_PROPERTIES_TITLE)
            {
                m_objSolarExp.OnPropertyChanged(GroupName, PropertyName, PropertyValue);
            }
            DrawScene();
        }

        public void DrawScene()
        {
            OnReloadExperiment(m_pManager.m_strExperimentGroup, m_pManager.m_strExperimentName);
        }
        public void DrawObject(string ExperimentName)
        {

            if (m_objSolarExp.m_strObjectType == Constants.OBJECT_TYPE_SOLAR_SYSTEM)
            {
                DrawSolarSystem();
            }

        }
        public void DrawSolarSystem()
        {
            ApplicationView applicationView = new ApplicationView();
            applicationView.InitializeEnvironment(Constants.TRUE);
            applicationView.BeginGraphicsCommands();

            //Set the Background Color
            applicationView.SetBkgColor(m_objSolarExp.s_Color.R / 255,
                                        m_objSolarExp.s_Color.G / 255,
                                        m_objSolarExp.s_Color.B / 255, 1);

            try
            {
                applicationView.StartNewDisplayList();
            }
            catch (Exception)
            {

                return;
            }
            applicationView.SetColorf(1.0f, 1.0f, 1.0f);

            OpenGLView openGLView = new OpenGLView();

            //Draw 1st orbits
            openGLView.glColor3f(1.0f, 0.0f, 0.0f);
            openGLView.glLineWidth(3.0f);
            openGLView.glBegin(Constants.GL_LINE_LOOP);


            int segments = 200;
            for (int i = 0; i < segments; i++)
            {
                double theta = 2.0 * Math.PI * i / segments; //get the current angle
                float x = (float)(0.5 * Math.Cos(theta));
                float y = (float)(0.5 * Math.Sin(theta));
                openGLView.glVertex2f(x + 0, y + 0);

            }
            openGLView.glEnd();

            //Draw 2nd orbits
            openGLView.glColor3f(0.5f, 0.0f, 1.0f);
            openGLView.glBegin(Constants.GL_LINE_LOOP);
            for (int i = 0; i < segments; i++)
            {
                double theta = 2.0 * Math.PI * i / segments; //get the current angle
                float x = (float)(0.8 * Math.Cos(theta));
                float y = (float)(0.8 * Math.Sin(theta));
                openGLView.glVertex2f(x + 0, y + 0);
            }
            openGLView.glEnd();

            //Draw 3rd orbits
            openGLView.glColor3f(0.0f, 1.0f, 0.0f);
            openGLView.glBegin(Constants.GL_LINE_LOOP);
            for (int i = 0; i < segments; i++)
            {
                double theta = 2.0 * Math.PI * i / segments; //get the current angle
                float x = (float)(1.1 * Math.Cos(theta));
                float y = (float)(1.1 * Math.Sin(theta));
                openGLView.glVertex2f(x + 0, y + 0);
            }
            openGLView.glEnd();

            //Draw 4th orbits
            openGLView.glColor3f(0.5f, 1.0f, 1.0f);
            openGLView.glBegin(Constants.GL_LINE_LOOP);
            for (int i = 0; i < segments; i++)
            {
                double theta = 2.0 * Math.PI * i / segments; //get the current angle
                float x = (float)(1.4 * Math.Cos(theta));
                float y = (float)(1.4 * Math.Sin(theta));
                openGLView.glVertex2f(x + 0, y + 0);
            }

            openGLView.glEnd();


            //Draw Sun
            // Begin drawing the sphere
            openGLView.glBegin(Constants.GL_TRIANGLE_FAN);
            openGLView.glColor3f((float)m_objSolarExp.sun_Color.R / 255,
                                        (float)m_objSolarExp.sun_Color.G / 255,
                                       (float)m_objSolarExp.sun_Color.B / 255);
            // Define the center point of the sphere
            openGLView.glVertex3f(0.0f, 0.0f, 0.0f);

            // Define the radius of the sphere
            float radius = 0.3f;

            // Define the number of segments and rings
            int rings = 200;

            // Generate the vertex data for the sphere
            for (int i = 0; i <= rings; i++)
            {
                float phi = (float)(Math.PI * i / rings);
                float sinPhi = (float)Math.Sin(phi);
                float cosPhi = (float)Math.Cos(phi);

                for (int j = 0; j <= segments; j++)
                {
                    float theta = 2.0f * (float)Math.PI * j / segments;
                    float sinTheta = (float)Math.Sin(theta);
                    float cosTheta = (float)Math.Cos(theta);

                    float x = cosTheta * sinPhi;
                    float y = cosPhi;
                    float z = sinTheta * sinPhi;

                    openGLView.glVertex3f(x * radius, y * radius, z * radius);
                }
            }

            // End drawing the sphere
            openGLView.glEnd();

            DrawMercury();
            DrawVenus();
            DrawEarth();
            DrawMars();

            applicationView.EndNewDisplayList();
            applicationView.EndGraphicsCommands();
            applicationView.Refresh();

        }

        public void DrawMercury()
        {
            OpenGLView openGLView = new OpenGLView();
            openGLView.glTranslatef(0.31f, 0.4f, 0.0f);
            // Begin drawing the sphere
            openGLView.glBegin(Constants.GL_TRIANGLE_FAN);
            openGLView.glColor3f((float)m_objSolarExp.mercury_Color.R / 255,
                                        (float)m_objSolarExp.mercury_Color.G / 255,
                                        (float)m_objSolarExp.mercury_Color.B / 255);
            // Define the center point of the sphere
            openGLView.glVertex3f(0.0f, 0.0f, 0.0f);

            // Define the radius of the sphere
            float radius = 0.05f;

            // Define the number of segments and rings
            int segments = 200;
            int rings = 200;

            // Generate the vertex data for the sphere
            for (int i = 0; i <= rings; i++)
            {
                float phi = (float)(Math.PI * i / rings);
                float sinPhi = (float)Math.Sin(phi);
                float cosPhi = (float)Math.Cos(phi);

                for (int j = 0; j <= segments; j++)
                {
                    float theta = 2.0f * (float)Math.PI * j / segments;
                    float sinTheta = (float)Math.Sin(theta);
                    float cosTheta = (float)Math.Cos(theta);

                    float x = cosTheta * sinPhi;
                    float y = cosPhi;
                    float z = sinTheta * sinPhi;

                    openGLView.glVertex3f(x * radius, y * radius, z * radius);
                }
            }
            // End drawing the sphere
            openGLView.glEnd();

        }

        public void DrawVenus()
        {
            OpenGLView openGLView = new OpenGLView();
            openGLView.glTranslatef(0.5f, -0.4f, 0.0f);
            // Begin drawing the sphere
            openGLView.glBegin(Constants.GL_TRIANGLE_FAN);
            openGLView.glColor3f((float)m_objSolarExp.venus_Color.R / 255,
                                        (float)m_objSolarExp.venus_Color.G / 255,
                                        (float)m_objSolarExp.venus_Color.B / 255);
            // Define the center point of the sphere
            openGLView.glVertex3f(0.0f, 0.0f, 0.0f);

            // Define the radius of the sphere
            float radius = 0.08f;

            // Define the number of segments and rings
            int segments = 200;
            int rings = 200;

            // Generate the vertex data for the sphere
            for (int i = 0; i <= rings; i++)
            {
                float phi = (float)(Math.PI * i / rings);
                float sinPhi = (float)Math.Sin(phi);
                float cosPhi = (float)Math.Cos(phi);

                for (int j = 0; j <= segments; j++)
                {
                    float theta = 2.0f * (float)Math.PI * j / segments;
                    float sinTheta = (float)Math.Sin(theta);
                    float cosTheta = (float)Math.Cos(theta);

                    float x = cosTheta * sinPhi;
                    float y = cosPhi;
                    float z = sinTheta * sinPhi;

                    openGLView.glVertex3f(x * radius, y * radius, z * radius);
                }
            }

            // End drawing the sphere
            openGLView.glEnd();
        }

        public void DrawEarth()
        {
            OpenGLView openGLView = new OpenGLView();
            openGLView.glTranslatef(-1.84f, 0.4f, 0.0f);
            // Begin drawing the sphere
            openGLView.glBegin(Constants.GL_TRIANGLE_FAN);
            openGLView.glColor3f((float)m_objSolarExp.earth_Color.R / 255,
                                        (float)m_objSolarExp.earth_Color.G / 255,
                                       (float)m_objSolarExp.earth_Color.B / 255);
            // Define the center point of the sphere
            openGLView.glVertex3f(0.0f, 0.0f, 0.0f);

            // Define the radius of the sphere
            float radius = 0.1f;

            // Define the number of segments and rings
            int segments = 200;
            int rings = 200;

            // Generate the vertex data for the sphere
            for (int i = 0; i <= rings; i++)
            {
                float phi = (float)(Math.PI * i / rings);
                float sinPhi = (float)Math.Sin(phi);
                float cosPhi = (float)Math.Cos(phi);

                for (int j = 0; j <= segments; j++)
                {
                    float theta = 2.0f * (float)Math.PI * j / segments;
                    float sinTheta = (float)Math.Sin(theta);
                    float cosTheta = (float)Math.Cos(theta);

                    float x = cosTheta * sinPhi;
                    float y = cosPhi;
                    float z = sinTheta * sinPhi;

                    openGLView.glVertex3f(x * radius, y * radius, z * radius);
                }
            }

            // End drawing the sphere
            openGLView.glEnd();
        }

        public void DrawMars()
        {
            OpenGLView openGLView = new OpenGLView();
            openGLView.glTranslatef(-0.0f, -1.35f, 0.0f);
            // Begin drawing the sphere
            openGLView.glBegin(Constants.GL_TRIANGLE_FAN);
            openGLView.glColor3f((float)m_objSolarExp.mars_Color.R / 255,
                                        (float)m_objSolarExp.mars_Color.G / 255,
                                        (float)m_objSolarExp.mars_Color.B / 255);
            // Define the center point of the sphere
            openGLView.glVertex3f(0.0f, 0.0f, 0.0f);

            // Define the radius of the sphere
            float radius = 0.15f;

            // Define the number of segments and rings
            int segments = 200;
            int rings = 200;

            // Generate the vertex data for the sphere
            for (int i = 0; i <= rings; i++)
            {
                float phi = (float)(Math.PI * i / rings);
                float sinPhi = (float)Math.Sin(phi);
                float cosPhi = (float)Math.Cos(phi);

                for (int j = 0; j <= segments; j++)
                {
                    float theta = 2.0f * (float)Math.PI * j / segments;
                    float sinTheta = (float)Math.Sin(theta);
                    float cosTheta = (float)Math.Cos(theta);

                    float x = cosTheta * sinPhi;
                    float y = cosPhi;
                    float z = sinTheta * sinPhi;

                    openGLView.glVertex3f(x * radius, y * radius, z * radius);
                }
            }

            // End drawing the sphere
            openGLView.glEnd();
        }

        public void StartSimulation(string ExperimentGroup, string ExperimentName)
        {

            if (ExperimentGroup == Constants.MECHANICS_TREE_ROOT_TITLE && ExperimentName == Constants.MECHANICS_TREE_SOLAR_SYSTEM)
            {
                StartSolarSystemSimulation();

            }
            else
            {

            }
        }

        private void StartSolarSystemSimulation()
        {
            m_pManager.SetSimulationStatus(Constants.TRUE);
            ApplicationView applicationView = new ApplicationView();
            float Angle = (float)0.0, x = (float)0.0, y = (float)0.0, z = (float)0.1;
            int i = 0; //Indicate Random Movment after each iteration
            Random rnd = new Random();
            while (m_pManager.m_bSimulationActive)
            {
                applicationView.BeginGraphicsCommands();

                if (m_objSolarExp.m_strSimulationPattern == Constants.OBJECT_PATTERN_TYPE_ROTATE)
                {
                    //Rotate the object with respect to Y Axis
                    x = (float)0.1; y = (float)1.0; z = (float)0.1;

                }
                else if (m_objSolarExp.m_strSimulationPattern == Constants.OBJECT_PATTERN_TYPE_RANDOM)
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
                Thread.Sleep((int)m_objSolarExp.m_lSimulationInterval); //Sleep for 500 Milli seconds
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

                //Now plot the other Y Value for each data
                index[1] = 2;
                pValue = pInfo.m_x; //set the X 
                saX.SetValue(pValue, index[0], index[1]);

                pValue = pInfo.m_y; //set the Y
                saY.SetValue(pValue, index[0], index[1]);

                pValue = pInfo.m_z; //set the Z
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
                catch
                {

                }

            }
        }


    }
}
