// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "TowerOfHanoi_i.h"
#include "dllmain.h"
#include "xdlldata.h"

CTowerOfHanoiModule _AtlModule;

class CTowerOfHanoiApp : public CWinApp
{
public:

// Overrides
	virtual BOOL InitInstance();
	virtual int ExitInstance();

	DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(CTowerOfHanoiApp, CWinApp)
END_MESSAGE_MAP()

CTowerOfHanoiApp theApp;

BOOL CTowerOfHanoiApp::InitInstance()
{
#ifdef _MERGE_PROXYSTUB
	if (!PrxDllMain(m_hInstance, DLL_PROCESS_ATTACH, NULL))
		return FALSE;
#endif
	return CWinApp::InitInstance();
}

int CTowerOfHanoiApp::ExitInstance()
{
	return CWinApp::ExitInstance();
}
