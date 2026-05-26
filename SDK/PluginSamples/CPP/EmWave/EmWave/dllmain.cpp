// dllmain.cpp : Implementation of DllMain.

#include "stdafx.h"
#include "resource.h"
#include "EmWave_i.h"
#include "dllmain.h"
#include "xdlldata.h"

CEmWaveModule _AtlModule;

class CEmWaveApp : public CWinApp
{
public:

// Overrides
	virtual BOOL InitInstance();
	virtual int ExitInstance();

	DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(CEmWaveApp, CWinApp)
END_MESSAGE_MAP()

CEmWaveApp theApp;

BOOL CEmWaveApp::InitInstance()
{
#ifdef _MERGE_PROXYSTUB
	if (!PrxDllMain(m_hInstance, DLL_PROCESS_ATTACH, NULL))
		return FALSE;
#endif
	return CWinApp::InitInstance();
}

int CEmWaveApp::ExitInstance()
{
	return CWinApp::ExitInstance();
}
