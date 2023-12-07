// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "CubeCPPAddin_i.h"
#include "dllmain.h"
#include "xdlldata.h"

CCubeCPPAddinModule _AtlModule;

class CCubeCPPAddinApp : public CWinApp
{
public:

// Overrides
	virtual BOOL InitInstance();
	virtual int ExitInstance();

	DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(CCubeCPPAddinApp, CWinApp)
END_MESSAGE_MAP()

CCubeCPPAddinApp theApp;

BOOL CCubeCPPAddinApp::InitInstance()
{
#ifdef _MERGE_PROXYSTUB
	if (!PrxDllMain(m_hInstance, DLL_PROCESS_ATTACH, NULL))
		return FALSE;
#endif
	return CWinApp::InitInstance();
}

int CCubeCPPAddinApp::ExitInstance()
{
	return CWinApp::ExitInstance();
}
