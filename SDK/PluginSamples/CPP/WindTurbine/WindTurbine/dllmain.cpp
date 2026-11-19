// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "WindTurbine_i.h"
#include "dllmain.h"
#include "xdlldata.h"

CWindTurbineModule _AtlModule;

class CWindTurbineApp : public CWinApp
{
public:

// Overrides
	virtual BOOL InitInstance();
	virtual int ExitInstance();

	DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(CWindTurbineApp, CWinApp)
END_MESSAGE_MAP()

CWindTurbineApp theApp;

BOOL CWindTurbineApp::InitInstance()
{
#ifdef _MERGE_PROXYSTUB
	if (!PrxDllMain(m_hInstance, DLL_PROCESS_ATTACH, NULL))
		return FALSE;
#endif
	return CWinApp::InitInstance();
}

int CWindTurbineApp::ExitInstance()
{
	return CWinApp::ExitInstance();
}
