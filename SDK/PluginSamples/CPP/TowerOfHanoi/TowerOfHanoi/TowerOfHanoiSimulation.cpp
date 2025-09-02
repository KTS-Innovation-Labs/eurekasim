#include "stdafx.h"
#include "TowerOfHanoiSimulation.h"


// CMySimulationAddin


BOOL CTowerOfHanoiSimulation::LoadLongResource(CString &str, UINT nID)
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

STDMETHODIMP CTowerOfHanoiSimulation::InvokePreferenceSettings()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	AfxMessageBox(_T("CTowerOfHanoiSimulation::InvokePreferenceSettings()"));

	return S_OK;
}


STDMETHODIMP CTowerOfHanoiSimulation::InvokeDefaultSettings()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	if (m_pAddinSimulationManager && m_pAddinSimulationManager->m_pObjectDemoExperiment)
	{
		// Set the object type to Tower of Hanoi to ensure the correct properties are displayed
		m_pAddinSimulationManager->m_pObjectDemoExperiment->m_ObjectPattern.m_strObjectType = OBJECT_TYPE_TOWEROFHANOI;

		// Show the corresponding properties in the property window
		m_pAddinSimulationManager->m_pObjectDemoExperiment->ShowObjectProperties();
	}

	return S_OK;
}


STDMETHODIMP CTowerOfHanoiSimulation::InvokeOnExperimentChanged()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	return S_OK;

}

STDMETHODIMP CTowerOfHanoiSimulation::OnNumRingsChange(BSTR CtrlID, long Index)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	if (m_pAddinSimulationManager && m_pAddinSimulationManager->m_pObjectDemoExperiment)
	{
		int numRings = Index + 3; // Index is 0-based, rings start at 3
		if (m_pAddinSimulationManager->m_pObjectDemoExperiment->m_ObjectPattern.m_nNumberOfRings != numRings)
		{
			if (m_pAddinSimulationManager->m_bSimulationActive)
			{
				m_pAddinSimulationManager->SetSimulationStatus(FALSE);
				Sleep(100); // Give simulation loop time to terminate
			}

			m_pAddinSimulationManager->m_pObjectDemoExperiment->m_ObjectPattern.m_nNumberOfRings = numRings;
			// Update property grid and redraw scene
			m_pAddinSimulationManager->m_pObjectDemoExperiment->ShowObjectProperties();
			m_pAddinSimulationManager->m_pObjectDemoExperiment->DrawScene();
		}
	}
	return S_OK;
}

STDMETHODIMP CTowerOfHanoiSimulation::OnSpeedChange(BSTR CtrlID, long Index)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	const double speeds[] = { 0.25, 0.5, 0.75, 1.0, 1.25, 1.5, 1.75, 2.0 };
	if (Index >= 0 && Index < (sizeof(speeds) / sizeof(speeds[0])))
	{
		double speed = speeds[Index];
		if (m_pAddinSimulationManager && m_pAddinSimulationManager->m_pObjectDemoExperiment)
		{
			m_pAddinSimulationManager->m_pObjectDemoExperiment->m_ObjectPattern.m_dAnimationSpeed = speed;
			// Update property grid
			m_pAddinSimulationManager->m_pObjectDemoExperiment->ShowObjectProperties();
		}
	}
	return S_OK;
}

void CTowerOfHanoiSimulation::SetRibbonControlText(CString ID, CString Text)
{
	CComPtr<IRibbonToolbar> RibbonToolbar;
	HRESULT HR = RibbonToolbar.CoCreateInstance(CLSID_RibbonToolbar);
	if (SUCCEEDED(HR))
	{
		RibbonToolbar->SetControlText(ID.AllocSysString(), Text.AllocSysString());
	}
}



void CTowerOfHanoiSimulation::LoadAddinName(CString strXML)
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