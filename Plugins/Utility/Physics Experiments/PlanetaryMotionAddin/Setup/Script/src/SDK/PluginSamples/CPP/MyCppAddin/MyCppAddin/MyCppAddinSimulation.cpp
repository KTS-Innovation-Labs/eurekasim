// MySimulationAddin.cpp : Implementation of CMySimulationAddin

#include "stdafx.h"
#include "MyCppAddinSimulation.h"


// CMySimulationAddin


BOOL CMyCppAddinSimulation::LoadLongResource(CString &str, UINT nID)
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

STDMETHODIMP CMyCppAddinSimulation::InvokePreferenceSettings()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	AfxMessageBox(_T("CMyCppAddinSimulation::InvokePreferenceSettings()"));

	return S_OK;
}


STDMETHODIMP CMyCppAddinSimulation::InvokeDefaultSettings()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	AfxMessageBox(_T("CMyCppAddinSimulation::InvokeDefaultSettings()"));

	return S_OK;
}
