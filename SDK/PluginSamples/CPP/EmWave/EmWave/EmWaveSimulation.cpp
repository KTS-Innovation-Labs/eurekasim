// MySimulationAddin.cpp : Implementation of CMySimulationAddin

#include "stdafx.h"
#include "EmWaveSimulation.h"


// CMySimulationAddin


BOOL CEmWaveSimulation::LoadLongResource(CString &str, UINT nID)
{
	try
	{
		HRSRC               hRes;
		BOOL                bResult = FALSE;

		//if you want standard HTML type
		hRes = FindResource(AfxGetInstanceHandle(), MAKEINTRESOURCE(nID), RT_HTML);
		if (hRes == NULL)
		{
			//trace error
			str = _T("Error: Resource could not be found\r\n");
		}
		else
		{
			DWORD dwSize = SizeofResource(AfxGetInstanceHandle(), hRes);
			if (dwSize == 0)
			{
				str.Empty();
				bResult = TRUE;
			}
			else
			{
				HGLOBAL hGlob = LoadResource(AfxGetInstanceHandle(), hRes);
				if (hGlob != NULL)
				{
					LPVOID lpData = LockResource(hGlob);
					if (lpData != NULL)
					{
						str = (LPCSTR)lpData;
						bResult = TRUE;
					}
					FreeResource(hGlob);
				}
			}
			if (!bResult)
				str = _T("Error: Resource could not be load\r\n");
		}
		return bResult;
	}
	catch (CResourceException* ex)
	{
		ex->Delete();
		return FALSE;
	}
	catch (...)
	{
		return FALSE;
	}

}

STDMETHODIMP CEmWaveSimulation::InvokePreferenceSettings()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	AfxMessageBox(_T("CEmWaveSimulation::InvokePreferenceSettings()"));

	return S_OK;
}


STDMETHODIMP CEmWaveSimulation::InvokeDefaultSettings()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	AfxMessageBox(_T("CEmWaveSimulation::InvokeDefaultSettings(Hai)"));

	return S_OK;
}


STDMETHODIMP CEmWaveSimulation::InvokeOnExperimentChanged()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	return S_OK;

}

void CEmWaveSimulation::SetRibbonControlText(CString ID, CString Text)
{
	CComPtr<IRibbonToolbar> RibbonToolbar;
	HRESULT HR = RibbonToolbar.CoCreateInstance(CLSID_RibbonToolbar);
	if (SUCCEEDED(HR))
	{
		RibbonToolbar->SetControlText(ID.AllocSysString(), Text.AllocSysString());
	}
}



void CEmWaveSimulation::LoadAddinName(CString strXML)
{
	try
	{
		USES_CONVERSION;

		CComPtr<IXMLDOMDocument> spXMLDOM;
		IXMLDOMNodeList		*pXMLDOMNodeList = NULL;
		IXMLDOMNode			*pXMLDOMNode = NULL;
		IXMLDOMNode			*pAttributeValue = NULL;
		IXMLDOMNamedNodeMap *pAttributeMap = NULL;

		HRESULT hr = spXMLDOM.CoCreateInstance(__uuidof(DOMDocument));
		if (FAILED(hr))
		{
			AfxMessageBox(_T("Unable to create XML parser object"));

		}
		if (spXMLDOM.p == NULL)
		{
			AfxMessageBox(_T("Unable to create XML parser object"));

		}
		VARIANT_BOOL bSuccess = false;
		hr = spXMLDOM->loadXML(strXML.AllocSysString(), &bSuccess);
		if (FAILED(hr))
		{
			AfxMessageBox(_T("Unable to load XML document into the parser"));

		}
		if (!bSuccess)
		{
			AfxMessageBox(_T("Unable to load XML document into the parser"));

		}
		BSTR strVal;
		hr = spXMLDOM->getElementsByTagName(CComBSTR("customUI"), &pXMLDOMNodeList);

		if (FAILED(hr))
		{
			AfxMessageBox(_T("Failed to get the Addin Name"));

		}
		else
		{
			hr = pXMLDOMNodeList->nextNode(&pXMLDOMNode);
			pXMLDOMNode->get_attributes(&pAttributeMap);
			pAttributeMap->getNamedItem(CComBSTR("label"), &pAttributeValue);
			pAttributeValue->get_text(&strVal);
			m_pAddinSimulationManager->SetPluginName(CString(strVal)); //Got the addin Name

		}
	}
	catch (...)
	{

	}
}
