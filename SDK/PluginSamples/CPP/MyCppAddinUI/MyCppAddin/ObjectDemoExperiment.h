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

#define PI								3.1415926535

#define OBJECT_TREE_ROOT_TITLE						_T("Object Demo")
#define OBJECT_TREE_LEAF_PATTERN_TITLE				_T("Object Pattern Demo")

#define MECHANICS_TREE_ROOT_TITLE					_T("Physics")
#define MECHANICS_TREE_SIMPLE_PENDULUM_TITLE		_T("Simple Pendulum")
#define MECHANICS_TREE_PROJECTILE_MOTION_TITLE		_T("Projectile Motion")
#define MECHANICS_TREE_PLANETORY_MOTION_TITLE		_T("Planetory Motion")

#define OBJECT_PROPERTIES_TITLE						_T("Select Object | Properties")
#define OBJECT_TYPE_TITLE							_T("Select The Object Type")
#define OBJECT_COLOR_TITLE							_T("Select Background Color")
#define OBJECT_SIMULATION_PATTERN_TITLE				_T("Simulation Pattern")
#define OBJECT_SIMULATION_INTERVAL_TITLE			_T("Simulation Interval")

#define OBJECT_TYPES								_T("Cube,Ball,Pyramid,Aeroplane")
#define OBJECT_TYPE_CUBE							_T("Cube")
#define OBJECT_TYPE_BALL							_T("Ball")
#define OBJECT_TYPE_PYRAMID							_T("Pyramid")
#define OBJECT_TYPE_AEROPLANE						_T("Aeroplane")

#define OBJECT_PATTERN_TYPES						_T("Rotate,Random Movement")
#define OBJECT_PATTERN_TYPE_ROTATE					_T("Rotate")
#define OBJECT_PATTERN_TYPE_RANDOM					_T("Random Movement")

#define PENDULUM_PROPERTIES_TITLE					_T("Select Pendulum | Properties")
#define PENDULUM_LENGTH								_T("Pendulum Length")
#define PENDULUM_BOB_RADIUS							_T("Bob Radius")
#define PENDULUM_MAX_SWING_ANGLE					_T("Max Swing Angle")
#define PENDULUM_BOB_MASS							_T("Mass of Bob")
#define PENDULUM_AMPITUDE							_T("Amplitude")
#define PENDULUM_COLOR_TITLE						_T("Select Background Color")
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
		m_lSimulationInterval = 500;
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
class CPhysicsExperiments
{
public:
	COLORREF	m_Color;
	long		m_lSimulationInterval;
	double		m_Length;
	double		m_BobRadius;
	int			m_MaxAngle;

	CPhysicsExperiments()
	{
		m_Color = RGB(0, 0, 255);
		m_lSimulationInterval = 500;
		m_Length = 1.0;
		m_BobRadius = 0.1;
		m_MaxAngle = 45;
	}
	void OnPropertyChanged(BSTR GroupName, BSTR PropertyName, BSTR PropertyValue)
	{
		if (CString(GroupName) != PENDULUM_PROPERTIES_TITLE)
		{
			return;
		}
		if (CString(PropertyName) == PENDULUM_LENGTH)
		{
			m_Length = _tstof(CString(PropertyValue));
		}
		else if (CString(PropertyName) == PENDULUM_COLOR_TITLE)
		{
			m_Color = (COLORREF)_ttol(CString(PropertyValue));
		}
		else if (CString(PropertyName) == OBJECT_SIMULATION_INTERVAL_TITLE)
		{
			m_lSimulationInterval = _ttol(CString(PropertyValue));
		}
		else if (CString(PropertyName) == PENDULUM_BOB_RADIUS)
		{
			m_BobRadius = _tstof(CString(PropertyValue));
		}
		else if (CString(PropertyName) == PENDULUM_MAX_SWING_ANGLE)
		{
			m_MaxAngle = _ttol(CString(PropertyValue));
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
	float m_PendulumAngle;
public:
	CObjectPattern			m_ObjectPattern;
	CPhysicsExperiments*	m_pPhysicsExperiments;

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
	void DrawExpSetup(BSTR ExperimentName);
	void DrawSimplePendulum();
	
	void ShowPendulumProperties();
	void StartPendulumSimulation();
};


