#pragma once

/* BeginMode */
#define GL_POINTS                         0x0000
#define GL_LINES                          0x0001
#define GL_LINE_LOOP                      0x0002
#define GL_LINE_STRIP                     0x0003
#define GL_TRIANGLES                      0x0004
#define GL_TRIANGLE_STRIP                 0x0005
#define GL_TRIANGLE_FAN                   0x0006
#define GL_QUADS                          0x0007
#define GL_QUAD_STRIP                     0x0008
#define GL_POLYGON                        0x0009

#define OBJECT_3D_TREE_ROOT_TITLE					_T("3D Object Demo")
#define OBJECT_3D_TREE_LEAF_PATTERN_TITLE			_T("Object Pattern Demo")

#define MECHANICS_TREE_ROOT_TITLE					_T("Physics")
#define MECHANICS_TREE_SIMPLE_PENDULUM_TITLE		_T("Simple Pendulum")
#define MECHANICS_TREE_PROJECTILE_MOTION_TITLE		_T("Projectile Motion")
#define MECHANICS_TREE_PLANETORY_MOTION_TITLE		_T("Planetory Motion")

#define OBJECT_PROPERTIES_TITLE						_T("Select Object | Properties")
#define OBJECT_TYPE_TITLE							_T("Select The Object Type")
#define OBJECT_COLOR_TITLE							_T("Select Background Color")
#define OBJECT_SIMULATION_PATTERN_TITLE				_T("Simulation Pattern")
#define OBJECT_SIMULATION_INTERVAL_TITLE			_T("Simulation Interval")

#define OBJECT_TYPES								_T("Pendulum,EmWave")
//#define OBJECT_TYPE_CUBE							_T("Cube")
//#define OBJECT_TYPE_BALL							_T("Ball")
//#define OBJECT_TYPE_PYRAMID						_T("Pyramid")
//#define OBJECT_TYPE_AEROPLANE						_T("Aeroplane")
//#define OBJECT_TYPE_CLOCK							_T("Clock")
#define OBJECT_TYPE_PENDULUM						_T("Pendulum")
#define OBJECT_TYPE_EMWAVE						    _T("EmWave") 

#define OBJECT_PATTERN_TYPES						_T("Rotate,Random Movement,Swing Movement,Propagate")
#define OBJECT_PATTERN_TYPE_ROTATE					_T("Rotate")
#define OBJECT_PATTERN_TYPE_RANDOM					_T("Random Movement")
#define OBJECT_PATTERN_TYPE_SWING					_T("Swing Movement")
#define OBJECT_PATTERN_TYPE_PROPAGATE				_T("Propagate")

// CPlusTwoPhysicsExperiment command target
class CObjectPattern : public CObject
{
public:
	CString		m_strObjectType;
	COLORREF	m_Color;
	CString		m_strSimulationPattern;
	long		m_lSimulationInterval;
	CObjectPattern()
	{
		m_strObjectType = _T("Cube");
		m_Color = RGB(0, 0, 255);
		m_strSimulationPattern = _T("Rotate");
		m_lSimulationInterval = 100;
		
	}
	virtual void Serialize(CArchive& ar)
	{
		if (ar.IsStoring())
		{
			ar << m_strObjectType;
			ar << m_Color;
			ar << m_strSimulationPattern;
			ar << m_lSimulationInterval;
		}
		else
		{
			ar >> m_strObjectType;
			ar >> m_Color;
			ar >> m_strSimulationPattern;
			ar >> m_lSimulationInterval;
		}
	}
	void OnPropertyChanged(BSTR GroupName, BSTR PropertyName, BSTR PropertyValue)
	{
		if (CString(GroupName) != OBJECT_PROPERTIES_TITLE)
		{
			return;
		}
		if (CString(PropertyName) == OBJECT_TYPE_TITLE)
		{
			m_strObjectType = CString(PropertyValue);
		}
		else if (CString(PropertyName) == OBJECT_COLOR_TITLE)
		{
			m_Color = (COLORREF)_ttol(CString(PropertyValue));
		}
		else if (CString(PropertyName) == OBJECT_SIMULATION_PATTERN_TITLE)
		{
			m_strSimulationPattern = CString(CString(PropertyValue));
		}
		else if (CString(PropertyName) == OBJECT_SIMULATION_INTERVAL_TITLE)
		{
			m_lSimulationInterval= _ttol(CString(PropertyValue));
		}

	}

};
class CAddinSimulationManager;

class CGraphPoints : public CObject
{
public:
	float m_Angle;
	float m_x;
	float m_y;
	float m_z;
	CGraphPoints()
	{
		m_Angle = 0.0;
		m_x = 0.0;
		m_y = 0.0;
		m_z = 0.0;

	}
};
class CObjectDemoExperiment : public CObject
{
	CAddinSimulationManager*		m_pManager;
	CObArray						m_PlotInfoArray;
	

public:
	CObjectPattern			m_ObjectPattern;

	CObjectDemoExperiment(CAddinSimulationManager* pManager);
	virtual ~CObjectDemoExperiment();
	void LoadAllExperiments();

	void OnTreeNodeSelect(BSTR ExperimentGroup, BSTR ExperimentName);
	void OnTreeNodeDblClick(BSTR ExperimentGroup, BSTR ExperimentName);
	void OnReloadExperiment(BSTR ExperimentGroup, BSTR ExperimentName);

	void ShowObjectProperties();

	virtual void Serialize(CArchive& ar);
	void OnPropertyChanged(BSTR GroupName, BSTR PropertyName, BSTR PropertyValue);

	void DrawScene();
	void DrawObject(CString ExperimentName);
	void DrawCube();
	void DrawBall();
	void DrawPyramid();
	void DrawAeroplane();
	void StartSimulation(BSTR ExperimentGroup, BSTR ExperimentName);
	void StartObjectSimulation();
	void OnNextSimulationPoint(float Angle, float x, float y, float z);
	void PlotSimulationPoint(float Angle, float x, float y, float z);
	void InitializeSimulationGraph(CString ExperimentName);
	void DisplayObjectDemoGraph();
	void DrawClock();
	void DrawCircle(float segments, float radius, float sx, float sy);
	void DrawPendulum();
	void DrawEmWave();
};


