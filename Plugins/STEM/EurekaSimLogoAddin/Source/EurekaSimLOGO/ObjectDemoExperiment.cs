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


namespace EurekaSimLOGO
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


        public const string OBJECT_TREE_ROOT_TITLE = "Object Demo";

        public const string MECHANICS_TREE_ROOT_TITLE = "LOGO";
        public const string MECHANICS_TREE_LOGO_SYSTEM = "Logo Motion";


        public const string LOGO_PROPERTIES_TITLE = "Select Logo | Properties";
        public const string E_COLOR_TITLE = "E Color";
        public const string LOGO_COLOR_TITLE = "Select Background Color";
        public const string LOGO_SIMULATION_PATTERN_TITLE = "Simulation Pattern";
        public const string LOGO_SIMULATION_INTERVAL_TITLE = "Simulation Interval";

        public const string OBJECT_TYPES = "LogoSystem";
        public const string OBJECT_TYPE_LOGO_SYSTEM = "LogoSystem";

        public const string OBJECT_PATTERN_TYPES = "Rotate,Random Movement";
        public const string OBJECT_PATTERN_TYPE_ROTATE = "Rotate";
        public const string OBJECT_PATTERN_TYPE_RANDOM = "Random Movement";
        public const string CS_SAMPLE_EXPERIMENT_TYPE_GROUP_1_PROPERTIES = "Experiment Group 1 Properties";
        public const string CS_SAMPLE_DOC_SETTINGS_KEY = "Cs.Sample.Addin.Settings";
        public const string CS_SAMPLE_MAIN_EXPERIMENT_NAME = "EurekaSimLOGO Experiment Simulation Demo";
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


    public class LogoExperiments
    {
        public string m_strObjectType;
        public Color s_Color = new Color();
        public long m_lSimulationInterval;
        public string m_strSimulationPattern;

        public LogoExperiments()
        {
            s_Color = Color.FromArgb(100, 200, 255); //black
            m_strSimulationPattern = "Rotate";
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

            if (GroupName != Constants.LOGO_PROPERTIES_TITLE)
                return;
            else if (PropertyName == Constants.LOGO_SIMULATION_INTERVAL_TITLE)
                m_lSimulationInterval = Convert.ToInt64(PropertyValue);
            else if (PropertyName == Constants.LOGO_COLOR_TITLE)
                s_Color = Color.FromArgb(Convert.ToInt32(PropertyValue));
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
        public LogoExperiments m_objLogoExp = new LogoExperiments();
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
                objExperimentTreeView.AddExperiment(SessionID, Constants.MECHANICS_TREE_ROOT_TITLE, Constants.MECHANICS_TREE_LOGO_SYSTEM);
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
            if (ExperimentGroup == Constants.MECHANICS_TREE_ROOT_TITLE && ExperimentName == Constants.MECHANICS_TREE_LOGO_SYSTEM)
            {
                ShowLogoProperties();
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

            if (ExperimentName == Constants.MECHANICS_TREE_LOGO_SYSTEM)
            {
                DrawLogoSystem();

            }
            else
            {

            }
        }
        public void ShowLogoProperties()
        {
            PropertyWindow objPropertyWindow = new PropertyWindow();
            string strGroupName = "";
            try
            {
                objPropertyWindow.RemoveAll();
                strGroupName = Constants.LOGO_PROPERTIES_TITLE;
                objPropertyWindow.AddPropertyGroup(strGroupName);
                string strInterval = m_objLogoExp.m_lSimulationInterval.ToString();
                objPropertyWindow.AddPropertyItemAsString(strGroupName, Constants.LOGO_SIMULATION_INTERVAL_TITLE, strInterval, "Simulation Interval In Milli Seconds");
                objPropertyWindow.AddColorPropertyItem(strGroupName, Constants.LOGO_COLOR_TITLE, Constants.HexConverter(m_objLogoExp.s_Color), "Select Background Color");
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
            return m_objLogoExp.Serialize();
        }

        public void DeSerialize(ExperimentInfo info)
        {
            m_objLogoExp.DeSerialize(info);
        }
        public void OnPropertyChanged(string GroupName, string PropertyName, string PropertyValue)
        {

            if (GroupName == Constants.LOGO_PROPERTIES_TITLE)
            {
                m_objLogoExp.OnPropertyChanged(GroupName, PropertyName, PropertyValue);
            }
            DrawScene();
        }

        public void DrawScene()
        {
            OnReloadExperiment(m_pManager.m_strExperimentGroup, m_pManager.m_strExperimentName);
        }
        public void DrawObject(string ExperimentName)
        {

            if (m_objLogoExp.m_strObjectType == Constants.OBJECT_TYPE_LOGO_SYSTEM)
            {
                DrawLogoSystem();
            }

        }
        public void DrawLogoSystem()
        {
            ApplicationView applicationView = new ApplicationView();
            applicationView.InitializeEnvironment(Constants.TRUE);
            applicationView.BeginGraphicsCommands();

            // Set the Background Color
            applicationView.SetBkgColor(m_objLogoExp.s_Color.R / 200f,
                                        m_objLogoExp.s_Color.G / 255f,
                                        m_objLogoExp.s_Color.B / 255f,
                                        1f);


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
            //semi oval
            {

                // Apply rotation transformation
                //float radiusX = 0.2f;
                float radiusY = 0.5f;
                float startValue = 0.2f;
                float endValue = 0.1f;
                float t = 0.5f;  // Value between 0 and 1 indicating the interpolation point

                float radiusX = startValue + t * (endValue - startValue);

                float rotationAngle = 0.0f;
                openGLView.glRotatef(rotationAngle, 0.0f, 0.0f, 1.0f);


                openGLView.glColor3f(0.0f, 0.5f, 0.0f);

                openGLView.glBegin(Constants.GL_LINE_STRIP);

                int segments = 100;
                int startCut = 20;  // Number of segments to cut from the start
                int endCut = 20;    // Number of segments to cut from the end

                int validSegments = segments - startCut - endCut;

                for (int j = 0; j <= validSegments; j++)
                {
                    double theta = Math.PI * (j + startCut) / segments;
                    float orbitX = (float)(radiusX * Math.Cos(theta));
                    float orbitY = (float)(radiusY * Math.Sin(theta));
                    openGLView.glVertex2f(orbitX, orbitY);
                }

                openGLView.glEnd();


            }


            // Second semi-circle
            {
                // Apply rotation transformation
                //float radiusX = 0.2f;
                float radiusY = 0.5f;
                float startValue = 0.2f;
                float endValue = 0.1f;
                float t = 0.5f;  // Value between 0 and 1 indicating the interpolation point

                float radiusX = startValue + t * (endValue - startValue);

                float rotationAngle = 60.0f;
                openGLView.glRotatef(rotationAngle, 0.0f, 0.0f, 1.0f);

                openGLView.glColor3f(1.0f, 1.0f, 0.0f);

                openGLView.glBegin(Constants.GL_LINE_STRIP);

                int segments = 100;
                int startCut = 20;  // Number of segments to cut from the start
                int endCut = 20;    // Number of segments to cut from the end

                int validSegments = segments - startCut - endCut;

                for (int j = 0; j <= validSegments; j++)
                {
                    double theta = Math.PI * (j + startCut) / segments;
                    float orbitX = (float)(radiusX * Math.Cos(theta));
                    float orbitY = (float)(radiusY * Math.Sin(theta));
                    openGLView.glVertex2f(orbitX, orbitY);
                }

                openGLView.glEnd();
            }

            // Third semi-circle
            {
                // Apply rotation transformation
                //float radiusX = 0.2f;
                float radiusY = 0.5f;
                float startValue = 0.2f;
                float endValue = 0.1f;
                float t = 0.5f;  // Value between 0 and 1 indicating the interpolation point

                float radiusX = startValue + t * (endValue - startValue);

                float rotationAngle = 120.0f;
                openGLView.glRotatef(rotationAngle, 0.0f, 0.0f, 1.0f);

                openGLView.glColor3f(0.6f, 0.2f, 0.4f); //Dark rose

                openGLView.glBegin(Constants.GL_LINE_STRIP);

                int segments = 100;
                int startCut = 20;  // Number of segments to cut from the start
                int endCut = 20;    // Number of segments to cut from the end

                int validSegments = segments - startCut - endCut;

                for (int j = 0; j <= validSegments; j++)
                {
                    double theta = Math.PI * (j + startCut) / segments;
                    float orbitX = (float)(radiusX * Math.Cos(theta));
                    float orbitY = (float)(radiusY * Math.Sin(theta));
                    openGLView.glVertex2f(orbitX, orbitY);
                }

                openGLView.glEnd();
            }

            // Fourth semi-circle
            {
                // Apply rotation transformation
                //float radiusX = 0.2f;
                float radiusY = 0.5f;
                float startValue = 0.2f;
                float endValue = 0.1f;
                float t = 0.5f;  // Value between 0 and 1 indicating the interpolation point

                float radiusX = startValue + t * (endValue - startValue);

                float rotationAngle = 300.0f;
                openGLView.glRotatef(rotationAngle, 0.0f, 0.0f, 1.0f);

                openGLView.glColor3f(1.0f, 0.5f, 0.0f);



                openGLView.glBegin(Constants.GL_LINE_STRIP);

                int segments = 100;
                int startCut = 20;  // Number of segments to cut from the start
                int endCut = 20;    // Number of segments to cut from the end

                int validSegments = segments - startCut - endCut;

                for (int j = 0; j <= validSegments; j++)
                {
                    double theta = Math.PI * (j + startCut) / segments;
                    float orbitX = (float)(radiusX * Math.Cos(theta));
                    float orbitY = (float)(radiusY * Math.Sin(theta));
                    openGLView.glVertex2f(orbitX, orbitY);
                }

                openGLView.glEnd();
            }

            // Fifth semi-circle
            {
                // Apply rotation transformation
                //float radiusX = 0.2f;
                float radiusY = 0.5f;
                float startValue = 0.2f;
                float endValue = 0.1f;
                float t = 0.5f;  // Value between 0 and 1 indicating the interpolation point

                float radiusX = startValue + t * (endValue - startValue);

                float rotationAngle = 480.0f;
                openGLView.glRotatef(rotationAngle, 0.0f, 0.0f, 1.0f);

                openGLView.glColor3f(0.6f, 0.0f, 1.0f);

                openGLView.glBegin(Constants.GL_LINE_STRIP);

                int segments = 100;
                int startCut = 20;  // Number of segments to cut from the start
                int endCut = 20;    // Number of segments to cut from the end

                int validSegments = segments - startCut - endCut;

                for (int j = 0; j <= validSegments; j++)
                {
                    double theta = Math.PI * (j + startCut) / segments;
                    float orbitX = (float)(radiusX * Math.Cos(theta));
                    float orbitY = (float)(radiusY * Math.Sin(theta));
                    openGLView.glVertex2f(orbitX, orbitY);
                }

                openGLView.glEnd();
            }

            // Sixth semi-circle
            {
                // Apply rotation transformation
                //float radiusX = 0.2f;
                float radiusY = 0.5f;
                float startValue = 0.2f;
                float endValue = 0.1f;
                float t = 0.5f;  // Value between 0 and 1 indicating the interpolation point

                float radiusX = startValue + t * (endValue - startValue);

                float rotationAngle = 780.0f;
                openGLView.glRotatef(rotationAngle, 0.0f, 0.0f, 1.0f);

                openGLView.glColor3f(0.6f, 0.8f, 1.0f);  // light blue
                openGLView.glBegin(Constants.GL_LINE_STRIP);

                int segments = 100;
                int startCut = 20;  // Number of segments to cut from the start
                int endCut = 20;    // Number of segments to cut from the end

                int validSegments = segments - startCut - endCut;

                for (int j = 0; j <= validSegments; j++)
                {
                    double theta = Math.PI * (j + startCut) / segments;
                    float orbitX = (float)(radiusX * Math.Cos(theta));
                    float orbitY = (float)(radiusY * Math.Sin(theta));
                    openGLView.glVertex2f(orbitX, orbitY);
                }

                openGLView.glEnd();
            }

            // Draw e
            openGLView.glLineWidth(10.0f); // Set the desired line thickness (increased to 10.0f)

            openGLView.glBegin(Constants.GL_LINE_STRIP);

            openGLView.glColor3f(0.2f, 0.0f, 0.4f);

            // Define the parameters for the oval
            float sizeX = 0.3f;  // Adjust the size along the X-axis as desired
            float sizeY = 0.6f;  // Adjust the size along the Y-axis as desired
            float centerX1 = 0.0f; // Set the center X-coordinate
            float centerY1 = 0.0f; // Set the center Y-coordinate

            // Calculate the half-size values
            float halfSizeX = sizeX / 2.0f;
            float halfSizeY = sizeY / 2.0f;

            // Draw the oval
            float radiusX2 = halfSizeX;
            float radiusY2 = halfSizeY;
            int numSegments = 100; // Adjust the number of segments as desired

            int skipSegments = 5; // Number of segments to skip

            for (int i = 0; i <= numSegments; i++)
            {
                if (i >= numSegments - skipSegments && i <= numSegments)
                    continue; // Skip the specified segments

                float angle = ((float)i / numSegments) * (float)Math.PI * 2; // Angle in radians
                float x = centerX1 + radiusX2 * (float)Math.Cos(angle);
                float y = centerY1 + radiusY2 * (float)Math.Sin(angle);
                openGLView.glVertex2f(x, y);
            }

            openGLView.glEnd();

            // Draw the crossbar
            float crossbarLength = 0.6f; // Adjust the length of the crossbar as desired
            float startX = centerX1 - radiusX2;
            float endX = centerX1 + radiusX2;
            float crossbarY = centerY1;

            openGLView.glBegin(Constants.GL_LINES);
            openGLView.glVertex2f(startX, crossbarY);
            openGLView.glVertex2f(endX, crossbarY);
            openGLView.glEnd();
            applicationView.EndNewDisplayList();
            applicationView.EndGraphicsCommands();
            applicationView.Refresh();

        }




        public void StartSimulation(string ExperimentGroup, string ExperimentName)
        {

            if (ExperimentGroup == Constants.MECHANICS_TREE_ROOT_TITLE && ExperimentName == Constants.MECHANICS_TREE_LOGO_SYSTEM)
            {
                StartLogoSystemSimulation();

            }
            else
            {

            }
        }

        private void StartLogoSystemSimulation()
        {
            m_pManager.SetSimulationStatus(Constants.TRUE);
            ApplicationView applicationView = new ApplicationView();
            float Angle = (float)0.0, x = (float)0.0, y = (float)0.0, z = (float)0.0;
            int i = 0; //Indicate Random Movment after each iteration
            Random rnd = new Random();
            while (m_pManager.m_bSimulationActive)
            {
                applicationView.BeginGraphicsCommands();

                if (m_objLogoExp.m_strSimulationPattern == Constants.OBJECT_PATTERN_TYPE_ROTATE)
                {
                    //Rotate the object with respect to Y Axis
                    x = (float)0.1; y = (float)1.0; z = (float)0.1;

                }
                else if (m_objLogoExp.m_strSimulationPattern == Constants.OBJECT_PATTERN_TYPE_RANDOM)
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
                Thread.Sleep((int)m_objLogoExp.m_lSimulationInterval); //Sleep for 500 Milli seconds
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
