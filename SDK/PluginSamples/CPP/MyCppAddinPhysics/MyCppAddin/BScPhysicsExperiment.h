#pragma once

// CBScPhysicsExperiment command target
class CAddinSimulationManager;

class CBScPhysicsExperiment : public CObject
{
	CAddinSimulationManager* m_pManager;
public:
	CBScPhysicsExperiment(CAddinSimulationManager* pManager);
	virtual ~CBScPhysicsExperiment();
	void LoadAllExperiments();

	void OnTreeNodeSelect(BSTR ExperimentGroup, BSTR ExperimentName);
	void OnTreeNodeDblClick(BSTR ExperimentGroup, BSTR ExperimentName);
	void OnReloadExperiment( BSTR ExperimentGroup, BSTR ExperimentName);

	virtual void Serialize(CArchive& ar);

};


