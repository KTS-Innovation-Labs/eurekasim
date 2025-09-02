#pragma once

#include <vector>
#include <stack>

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

#define OBJECT_TYPES								_T("Cube,Ball,Pyramid,Aeroplane,Clock,Tower of Hanoi Solver")
#define OBJECT_TYPE_CUBE							_T("Cube")
#define OBJECT_TYPE_BALL							_T("Ball")
#define OBJECT_TYPE_PYRAMID							_T("Pyramid")
#define OBJECT_TYPE_AEROPLANE						_T("Aeroplane")
#define OBJECT_TYPE_CLOCK							_T("Clock")
#define OBJECT_TYPE_TOWEROFHANOI					_T("Tower of Hanoi Solver")

#define OBJECT_PATTERN_TYPES						_T("Rotate,Random Movement")
#define OBJECT_PATTERN_TYPE_ROTATE					_T("Rotate")
#define OBJECT_PATTERN_TYPE_RANDOM					_T("Random Movement")

#define TOWEROFHANOI_PROPERTIES_TITLE				_T("Tower of Hanoi | Properties")
#define TOWEROFHANOI_RINGS_TITLE					_T("Number of Rings")
#define TOWEROFHANOI_SPEED_TITLE					_T("Animation Speed")
#define TOWEROFHANOI_SPEED_OPTIONS					_T("0.25,0.50,0.75,1.00,1.25,1.50,1.75,2.00")


// CPlusTwoPhysicsExperiment command target
class CObjectPattern : public CObject
{
public:
	CString		m_strObjectType;
	COLORREF	m_Color;
	CString		m_strSimulationPattern;
	long		m_lSimulationInterval;
	int			m_nNumberOfRings;
	double		m_dAnimationSpeed;

	CObjectPattern()
	{
		m_strObjectType = _T("Cube");
		m_Color = RGB(0, 0, 255);
		m_strSimulationPattern = _T("Rotate");
		m_lSimulationInterval = 100;
		m_nNumberOfRings = 3;
		m_dAnimationSpeed = 1.0;
	}
	virtual void Serialize(CArchive& ar)
	{
		if (ar.IsStoring())
		{
			ar << m_strObjectType;
			ar << m_Color;
			ar << m_strSimulationPattern;
			ar << m_lSimulationInterval;
			ar << m_nNumberOfRings;
			ar.Write(&m_dAnimationSpeed, sizeof(m_dAnimationSpeed));

		}
		else
		{
			ar >> m_strObjectType;
			ar >> m_Color;
			ar >> m_strSimulationPattern;
			ar >> m_lSimulationInterval;
			ar >> m_nNumberOfRings;
			ar.Read(&m_dAnimationSpeed, sizeof(m_dAnimationSpeed));
		}
	}
	void OnPropertyChanged(BSTR GroupName, BSTR PropertyName, BSTR PropertyValue)
	{
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
			m_lSimulationInterval = _ttol(CString(PropertyValue));
		}
		else if (CString(PropertyName) == TOWEROFHANOI_RINGS_TITLE)
		{
			int rings = _ttoi(CString(PropertyValue));
			if (rings >= 3 && rings <= 8) // Basic validation
			{
				m_nNumberOfRings = rings;
			}
		}
		else if (CString(PropertyName) == TOWEROFHANOI_SPEED_TITLE)
		{
			m_dAnimationSpeed = _ttof(CString(PropertyValue));
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

struct MoveCommand
{
	int from, to;
};

class CObjectDemoExperiment : public CObject
{
	CAddinSimulationManager*		m_pManager;
	CObArray						m_PlotInfoArray;

	// Tower of Hanoi state
	std::vector<std::stack<int>>	m_pegs;
	std::vector<MoveCommand>		m_moves;

	void InitializePegs();
	void TowerOfHanoiSolver(int n, int from_peg, int to_peg, int aux_peg);
	void StartTowerOfHanoiSimulation();
	void AnimateMove(int fromPeg, int toPeg);
	void DrawTowerOfHanoi(int movingDisk, float mx, float my, float mz);


public:
	CObjectPattern			m_ObjectPattern;
	void SyncRibbonWithProperties();

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
	void DrawTowerOfHanoi();
	void StartSimulation(BSTR ExperimentGroup, BSTR ExperimentName);
	void StartObjectSimulation();
	void OnNextSimulationPoint(float Angle, float x, float y, float z);
	void PlotSimulationPoint(float Angle, float x, float y, float z);
	void InitializeSimulationGraph(CString ExperimentName);
	void DisplayObjectDemoGraph();
	void DrawClock();
	void DrawCircle(float segments, float radius, float sx, float sy);
};