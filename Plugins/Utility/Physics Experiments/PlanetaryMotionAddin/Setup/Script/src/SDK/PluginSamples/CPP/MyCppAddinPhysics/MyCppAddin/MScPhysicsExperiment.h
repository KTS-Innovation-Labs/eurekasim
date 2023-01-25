#pragma once

// CMScPhysicsExperiment command target
class CAddinSimulationManager;

class CMScPhysicsExperiment : public CObject
{
	CAddinSimulationManager* m_pManager;
public:
	CMScPhysicsExperiment(CAddinSimulationManager* pManager);
	virtual ~CMScPhysicsExperiment();
	void LoadAllExperiments();

	void OnTreeNodeSelect(BSTR ExperimentGroup, BSTR ExperimentName);
	void OnTreeNodeDblClick(BSTR ExperimentGroup, BSTR ExperimentName);
	void OnReloadExperiment(BSTR ExperimentGroup, BSTR ExperimentName);

	virtual void Serialize(CArchive& ar);
};


