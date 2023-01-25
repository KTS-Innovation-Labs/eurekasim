// MySimulationAddin.h : Declaration of the CMySimulationAddin

#pragma once
#include "resource.h"       // main symbols



#include "MyCppAddin_i.h"



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;


// CMySimulationAddin
static const GUID CATID_PROJECT_FRAMEWORK_ADDINS_RIBBON =
{ 0x66ebc8d5, 0xa4d0, 0x48c4,{ 0x97, 0x8b, 0x55, 0xd1, 0xb3, 0x4e,0x15, 0x7b } };

class ATL_NO_VTABLE CMyCppAddinSimulation :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CMyCppAddinSimulation, &CLSID_MyCppAddinSimulation>,
	public IDispatchImpl<IMyCppAddinSimulation, &IID_IMyCppAddinSimulation, &LIBID_MyCppAddinLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
	public IDispatchImpl<IAddin, &__uuidof(IAddin), &LIBID_EurekaSim, /* wMajor = */ 1>
{
	IMainApplication *			m_pMainApplication;
public:
	CMyCppAddinSimulation()
	{
	}

	DECLARE_REGISTRY_RESOURCEID(IDR_MYSIMULATIONADDIN)


	BEGIN_COM_MAP(CMyCppAddinSimulation)
		COM_INTERFACE_ENTRY(IMyCppAddinSimulation)
		COM_INTERFACE_ENTRY2(IDispatch, IMyCppAddinSimulation)
		COM_INTERFACE_ENTRY(IAddin)
	END_COM_MAP()

	BEGIN_CATEGORY_MAP(CAddinObject)
		IMPLEMENTED_CATEGORY(CATID_PROJECT_FRAMEWORK_ADDINS_RIBBON)
	END_CATEGORY_MAP()


	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:

	// IAddin Methods
public:
	STDMETHOD(Initialize)(long lSessionID, IMainApplication * pApp, VARIANT bFirstTime)
	{
		AFX_MANAGE_STATE(AfxGetStaticModuleState())
			CString strXMLCommand;
		m_pMainApplication = pApp;
		//You have to berak the string table if you want to accomodate all 
		//words that is there in the XML String
		//Load the XML String
		HINSTANCE lAppHandle = AfxGetInstanceHandle();
		CString strReserved = _T("");

		if (LoadLongResource(strXMLCommand, IDR_HTML_RIBBON_ELMENTS))
		{
			strXMLCommand.Replace(_T("\n"), _T("")); //Replace all new line characters
			strXMLCommand.Replace(_T("\t"), _T("")); //Replace all tab characters
		}
		pApp->SetAddInInfo(lSessionID, (LONGLONG)lAppHandle, strXMLCommand.AllocSysString(), IDR_TOOLBAR_ADDIN, strReserved.AllocSysString());
		return S_OK;
	}
	STDMETHOD(Uninitialize)(long lSessionID)
	{
		return S_OK;
	}
	STDMETHOD(About)()
	{
		AfxMessageBox(_T("About My Addin"));
		return S_OK;
	}
	
	BOOL LoadLongResource(CString &str, UINT nID);

	STDMETHOD(InvokePreferenceSettings)();
	STDMETHOD(InvokeDefaultSettings)();
};

OBJECT_ENTRY_AUTO(__uuidof(MyCppAddinSimulation), CMyCppAddinSimulation)
