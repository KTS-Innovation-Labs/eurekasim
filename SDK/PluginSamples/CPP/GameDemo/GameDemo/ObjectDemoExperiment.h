#pragma once

#include <GL/gl.h>
#include <atlcomcli.h>  
#include <GL/gl.h>


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

#define OBJECT_3D_TREE_ROOT_TITLE                  _T("3D Object Demo")
#define OBJECT_3D_TREE_LEAF_PATTERN_TITLE          _T("Object Pattern Demo")

#define MECHANICS_TREE_ROOT_TITLE                  _T("Physics")
#define MECHANICS_TREE_SIMPLE_PENDULUM_TITLE       _T("Simple Pendulum")
#define MECHANICS_TREE_PROJECTILE_MOTION_TITLE     _T("Projectile Motion")
#define MECHANICS_TREE_PLANETORY_MOTION_TITLE      _T("Planetory Motion")

#define OBJECT_PROPERTIES_TITLE                    _T("Select Object | Properties")
#define OBJECT_TYPE_TITLE                          _T("Select The Object Type")
#define OBJECT_COLOR_TITLE                         _T("Select Background Color")
#define OBJECT_SIMULATION_PATTERN_TITLE            _T("Simulation Pattern")
#define OBJECT_SIMULATION_INTERVAL_TITLE           _T("Simulation Interval")

#define OBJECT_TYPES                               _T("FootballPenalty,Elephant")
#define OBJECT_TYPE_FOOTBALLPENALTY                _T("FootballPenalty")
#define OBJECT_TYPE_ELEPHANT                       _T("Elephant")

#define OBJECT_PATTERN_TYPES                       _T("Penalty Kick View,Elephant Animation")
#define OBJECT_PATTERN_TYPE_ROTATE                 _T("Rotate")
#define OBJECT_PATTERN_TYPE_PENALTY_KICK           _T("Penalty Kick View")
#define OBJECT_PATTERN_TYPE_ELEPHANT_ANIMATION     _T("Elephant Animation")

#define BALL_SPEED_INCREMENT               0.1f
#define MAX_BALL_SPEED                     8.0f
#define GOALKEEPER_SPEED                   0.015f
#define GOALKEEPER_REACTION_TIME           0.5f
#define BALL_GRAVITY                       0.008f
#define KICK_POWER_MULTIPLIER              5.0f

#define ELEPHANT_STATE_IDLE                    0
#define ELEPHANT_STATE_WALKING                 1
#define ELEPHANT_STATE_TRUMPETING              2
#define ELEPHANT_STATE_RUNNING                 3
#define ELEPHANT_STATE_IDLE   0
#define ELEPHANT_STATE_WALK   1
#define ELEPHANT_STATE_RUN    2
#define ELEPHANT_EYE_WIDTH 0.08f
#define ELEPHANT_EYE_HEIGHT 0.05f
#define ELEPHANT_EYE_DEPTH 0.03f
#define _USE_MATH_DEFINES


// Forward declarations
class CAddinSimulationManager;

// CPlusTwoPhysicsExperiment command target
class CObjectPattern : public CObject
{
public:
	CString     m_strObjectType;
	COLORREF    m_Color;
	CString     m_strSimulationPattern;
	long        m_lSimulationInterval;

	CObjectPattern();
	virtual void Serialize(CArchive& ar);
	void OnPropertyChanged(BSTR GroupName, BSTR PropertyName, BSTR PropertyValue);
};

class CGraphPoints : public CObject
{
public:
	float m_Angle;
	float m_x;
	float m_y;
	float m_z;

	CGraphPoints();
};

class CFootballPenaltyGameState : public CObject
{
public:
	float ballX;
	float ballY;
	float ballZ;
	float ballSpeedX;
	float ballSpeedY;
	float ballSpeedZ;
	float kickerAngle;
	float kickPower;
	float goalkeeperX;
	float goalkeeperZ;
	float goalkeeperSpeed;
	float goalkeeperTimer;
	bool isBallMoving;
	bool isKicking;
	int score;
	int attempts;
	float gameTime;
	bool goalkeeperDiveLeft;
	bool goalkeeperDiveRight;
	bool isGoalScored;
	bool isShotSaved;
	bool isShotMissed;
	bool isGameActive;
	float kickAnimationTime;

	CFootballPenaltyGameState();
	void Reset();
};

class CElephantGameState : public CObject
{
public:
	float positionX;
	float positionY;
	float positionZ;
	float rotationY;
	float animationTime;
	int currentState;
	float walkSpeed;
	float runSpeed;
	float scale;
	bool isTrumpeting;
	float trumpetProgress;
	float headBob;
	float trunkSwing;
	float earFlap;

	CElephantGameState();
	void Reset();
};

class CObjectDemoExperiment : public CObject
{
private:
	CAddinSimulationManager*        m_pManager;
	CObArray                        m_PlotInfoArray;
	CFootballPenaltyGameState       m_FootballGameState;
	CElephantGameState              m_ElephantGameState;

	// Enhanced penalty kick view variables
	bool m_bPenaltyKickView;
	bool m_bShowAimingReticle;
	bool m_bShowPowerMeter;
	bool m_bKeyboardActive;

public:
	CObjectPattern          m_ObjectPattern;

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

	// Simulation methods
	void StartSimulation(BSTR ExperimentGroup, BSTR ExperimentName);
	void StartObjectSimulation();
	void OnNextSimulationPoint(float Angle, float x, float y, float z);
	void PlotSimulationPoint(float Angle, float x, float y, float z);
	void InitializeSimulationGraph(CString ExperimentName);
	void DisplayObjectDemoGraph();

	// Football Penalty methods
	void DrawFootballPenalty();
	void UpdateFootballPenalty();
	void DrawFootball();
	void DrawKicker();
	void DrawGoalkeeper();
	void DrawPentagon(float radius);
	void DrawGoalPost();
	void DrawFootballField();
	void DrawEnvironment();
	void ResetFootballPenalty();
	void CheckGoal();
	void UpdateGoalkeeper();
	void ResetBall();
	void DrawCubePrimitive(float size);
	void ProcessKick();
	void ResetShotResult();
	void HandleKeyboardInput();

	// Penalty Kick View methods
	void DrawPenaltyKickView();
	void DrawPenaltyKickHUD();
	void DrawAimingReticle();
	void DrawResultMessage();
	void DrawStadium();
	void DrawNet();

	// Elephant Simulator Methods
	void DrawElephant();
	void UpdateElephantAnimation();

	// Elephant drawing parts
	void DrawElephantBody();
	void DrawElephantHead();
	void DrawElephantEars();
	void DrawElephantTrunk();
	void DrawElephantTusks();
	void DrawElephantLegs();
	void DrawElephantTail();
	void DrawRoundedCube(float size);
	void DrawSavannahEnvironmentWithPond();
	void DrawAcaciaTree(float x, float z);
	void DrawGrassClump(float x, float z);
	void DrawCloud(float x, float y, float z, float scale);
	void DrawEllipsoid(float radius, int slices, int stacks);
	void DrawSphere(float radius, int slices, int stacks);

	void DrawCylinder(float radius, float height);

};