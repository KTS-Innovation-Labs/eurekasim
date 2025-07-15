// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "StarDemo_i.h"
#include "dllmain.h"
#include "xdlldata.h"

CStarDemoModule _AtlModule;

class CStarDemoApp : public CWinApp
{
public:

// Overrides
	virtual BOOL InitInstance();
	virtual int ExitInstance();

	DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(CStarDemoApp, CWinApp)
END_MESSAGE_MAP()

CStarDemoApp theApp;

BOOL CStarDemoApp::InitInstance()
{
#ifdef _MERGE_PROXYSTUB
	if (!PrxDllMain(m_hInstance, DLL_PROCESS_ATTACH, NULL))
		return FALSE;
#endif
	return CWinApp::InitInstance();
}

int CStarDemoApp::ExitInstance()
{
	return CWinApp::ExitInstance();
}
