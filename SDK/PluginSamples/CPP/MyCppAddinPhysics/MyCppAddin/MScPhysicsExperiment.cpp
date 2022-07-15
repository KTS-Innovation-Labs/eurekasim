// MScPhysicsExperiment.cpp : implementation file
//

#include "stdafx.h"
#include "MScPhysicsExperiment.h"
#include "MySimulationAddin.h"
#include "AddinSimulationManager.h"

// CMScPhysicsExperiment

CMScPhysicsExperiment::CMScPhysicsExperiment(CAddinSimulationManager* pManager)
{
	m_pManager = pManager;
}

CMScPhysicsExperiment::~CMScPhysicsExperiment()
{
}

void CMScPhysicsExperiment::LoadAllExperiments()
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
void CMScPhysicsExperiment::OnTreeNodeSelect(BSTR ExperimentGroup, BSTR ExperimentName)
{
	//AfxMessageBox(_T("CMScPhysicsExperiment::OnTreeNodeSelect(BSTR ExperimentGroup, BSTR ExperimentName)"));
}
void CMScPhysicsExperiment::OnTreeNodeDblClick(BSTR ExperimentGroup, BSTR ExperimentName)
{
	//AfxMessageBox(_T("CMScPhysicsExperiment::OnTreeNodeDblClick(BSTR ExperimentGroup, BSTR ExperimentName)"));
}
void CMScPhysicsExperiment::OnReloadExperiment(BSTR ExperimentGroup, BSTR ExperimentName)
{
	//AfxMessageBox(_T("CMScPhysicsExperiment::OnReloadExperiment(BSTR ExperimentGroup, BSTR ExperimentName)"));
}

void CMScPhysicsExperiment::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{

	}
	else
	{

	}
}


// CMScPhysicsExperiment member functions
