// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "GameDemo_i.h"
#include "dllmain.h"
#include "xdlldata.h"

CGameDemoModule _AtlModule;

class CGameDemoApp : public CWinApp
{
public:

// Overrides
	virtual BOOL InitInstance();
	virtual int ExitInstance();

	DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(CGameDemoApp, CWinApp)
END_MESSAGE_MAP()

CGameDemoApp theApp;

BOOL CGameDemoApp::InitInstance()
{
#ifdef _MERGE_PROXYSTUB
	if (!PrxDllMain(m_hInstance, DLL_PROCESS_ATTACH, NULL))
		return FALSE;
#endif
	return CWinApp::InitInstance();
}

int CGameDemoApp::ExitInstance()
{
	return CWinApp::ExitInstance();
}
