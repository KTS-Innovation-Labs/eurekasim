// BScPhysicsExperiment.cpp : implementation file
//

#include "stdafx.h"
#include "BScPhysicsExperiment.h"
#include "MySimulationAddin.h"
#include "AddinSimulationManager.h"

// CBScPhysicsExperiment

CBScPhysicsExperiment::CBScPhysicsExperiment(CAddinSimulationManager* pManager)
{
	m_pManager = pManager;
}

CBScPhysicsExperiment::~CBScPhysicsExperiment()
{
}

void CBScPhysicsExperiment::LoadAllExperiments()
{
	CComPtr<IExperimentTreeView> ExperimentTreeView;
	HRESULT HR = ExperimentTreeView.CoCreateInstance(CLSID_ExperimentTreeView);
	if (FAILED(HR))
	{
		return;
	}
	long SessionID = m_pManager->m_pAddin->m_lSessionID;
	ExperimentTreeView->DeleteAllExperiments(SessionID);
	ExperimentTreeView->SetRootNodeName(CString(_T("B.Sc Physics Experiment Properties")).AllocSysString(), TRUE);

	ExperimentTreeView->AddExperiment(SessionID, CString(_T("Mechanics")).AllocSysString(), CString(_T("Cantiliver")).AllocSysString());
	ExperimentTreeView->AddExperiment(SessionID, CString(_T("Mechanics")).AllocSysString(), CString(_T("Damped Harmonic Oscillator")).AllocSysString());
	ExperimentTreeView->AddExperiment(SessionID, CString(_T("Optics")).AllocSysString(), CString(_T("Newtons rings")).AllocSysString());
	ExperimentTreeView->AddExperiment(SessionID, CString(_T("Optics")).AllocSysString(), CString(_T("Difraction")).AllocSysString());
	ExperimentTreeView->AddExperiment(SessionID, CString(_T("Electronics")).AllocSysString(), CString(_T("Waveform Generator")).AllocSysString());

	ExperimentTreeView->Refresh();
}

void CBScPhysicsExperiment::OnTreeNodeSelect(BSTR ExperimentGroup, BSTR ExperimentName)
{
	//AfxMessageBox(_T("CBScPhysicsExperiment::OnTreeNodeSelect(BSTR ExperimentGroup, BSTR ExperimentName)"));
}
void CBScPhysicsExperiment::OnTreeNodeDblClick(BSTR ExperimentGroup, BSTR ExperimentName)
{
	//AfxMessageBox(_T("CBScPhysicsExperiment::OnTreeNodeDblClick(BSTR ExperimentGroup, BSTR ExperimentName)"));
}
void CBScPhysicsExperiment::OnReloadExperiment(BSTR ExperimentGroup, BSTR ExperimentName)
{
	//AfxMessageBox(_T("CBScPhysicsExperiment::OnReloadExperiment(BSTR ExperimentGroup, BSTR ExperimentName)"));
}

void CBScPhysicsExperiment::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		
	}
	else
	{

	}
}

// CBScPhysicsExperiment member functions
