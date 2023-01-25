// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "MyCppAddin_i.h"
#include "dllmain.h"
#include "xdlldata.h"

CMyCppAddinModule _AtlModule;

class CMyCppAddinApp : public CWinApp
{
public:

// Overrides
	virtual BOOL InitInstance();
	virtual int ExitInstance();

	DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(CMyCppAddinApp, CWinApp)
END_MESSAGE_MAP()

CMyCppAddinApp theApp;

BOOL CMyCppAddinApp::InitInstance()
{
#ifdef _MERGE_PROXYSTUB
	if (!PrxDllMain(m_hInstance, DLL_PROCESS_ATTACH, NULL))
		return FALSE;
#endif
	return CWinApp::InitInstance();
}

int CMyCppAddinApp::ExitInstance()
{
	return CWinApp::ExitInstance();
}
