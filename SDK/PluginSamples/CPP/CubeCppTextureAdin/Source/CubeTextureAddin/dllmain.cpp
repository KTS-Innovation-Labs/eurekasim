// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "CubeTextureAddin_i.h"
#include "dllmain.h"
#include "xdlldata.h"

CCubeTextureAddinModule _AtlModule;

class CCubeTextureAddinApp : public CWinApp
{
public:

// Overrides
	virtual BOOL InitInstance();
	virtual int ExitInstance();

	DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(CCubeTextureAddinApp, CWinApp)
END_MESSAGE_MAP()

CCubeTextureAddinApp theApp;

BOOL CCubeTextureAddinApp::InitInstance()
{
#ifdef _MERGE_PROXYSTUB
	if (!PrxDllMain(m_hInstance, DLL_PROCESS_ATTACH, NULL))
		return FALSE;
#endif
	return CWinApp::InitInstance();
}

int CCubeTextureAddinApp::ExitInstance()
{
	return CWinApp::ExitInstance();
}
