#include "stdafx.h"
#include "CDice.h"

#include "CubeCPPAddinSimulation.h"
#include "AddinSimulationManager.h"
#include "PropSliderCtrl.h"

//#include <GL/gl.h>
//#include <GL/glu.h>
//#include <GL/freeglut.h> 

CDice::CDice(CAddinSimulationManager* pManager)
{
	m_pManager = pManager;
}

CDice::~CDice()
{
}

void CDice::LoadAllExperiments()
{
	CComPtr<IExperimentTreeView> ExperimentTreeView;
	HRESULT HR = ExperimentTreeView.CoCreateInstance(CLSID_ExperimentTreeView);
	if (FAILED(HR))
	{
		return;
	}
	long SessionID = m_pManager->m_pAddin->m_lSessionID;
	ExperimentTreeView->DeleteAllExperiments(SessionID);
	ExperimentTreeView->SetRootNodeName(CString(_T("M.Sc Physics Experiment Properties")).AllocSysString(), TRUE);

	ExperimentTreeView->AddExperiment(SessionID, CString(_T("Mechanics")).AllocSysString(), CString(_T("Gyroscope")).AllocSysString());
	ExperimentTreeView->AddExperiment(SessionID, CString(_T("Mechanics")).AllocSysString(), CString(_T("Least Action Principle")).AllocSysString());

	ExperimentTreeView->Refresh();
}
void CDice::OnTreeNodeSelect(BSTR ExperimentGroup, BSTR ExperimentName)
{
	//AfxMessageBox(_T("CMScPhysicsExperiment::OnTreeNodeSelect(BSTR ExperimentGroup, BSTR ExperimentName)"));
}
void CDice::OnTreeNodeDblClick(BSTR ExperimentGroup, BSTR ExperimentName)
{
	//AfxMessageBox(_T("CMScPhysicsExperiment::OnTreeNodeDblClick(BSTR ExperimentGroup, BSTR ExperimentName)"));
}
void CDice::OnReloadExperiment(BSTR ExperimentGroup, BSTR ExperimentName)
{
	//AfxMessageBox(_T("CMScPhysicsExperiment::OnReloadExperiment(BSTR ExperimentGroup, BSTR ExperimentName)"));
}

void CDice::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{

	}
	else
	{

	}
}


