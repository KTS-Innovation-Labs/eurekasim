// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "ObjectDemo_i.h"
#include "dllmain.h"
#include "xdlldata.h"

CObjectDemoModule _AtlModule;

class CObjectDemoApp : public CWinApp
{
public:

// Overrides
	virtual BOOL InitInstance();
	virtual int ExitInstance();

	DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(CObjectDemoApp, CWinApp)
END_MESSAGE_MAP()

CObjectDemoApp theApp;

BOOL CObjectDemoApp::InitInstance()
{
#ifdef _MERGE_PROXYSTUB
	if (!PrxDllMain(m_hInstance, DLL_PROCESS_ATTACH, NULL))
		return FALSE;
#endif
	return CWinApp::InitInstance();
}

int CObjectDemoApp::ExitInstance()
{
	return CWinApp::ExitInstance();
}
