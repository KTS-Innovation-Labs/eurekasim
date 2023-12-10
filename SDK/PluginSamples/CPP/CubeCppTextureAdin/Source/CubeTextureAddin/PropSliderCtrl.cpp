// PropSliderCtrl.cpp : implementation file
//

#include "stdafx.h"
#include "PropSliderCtrl.h"


// CPropSliderCtrl



CPropSliderCtrl::CPropSliderCtrl(CSliderProp* pProp, COLORREF clrBack)
{
	m_clrBack = clrBack;
	m_brBackground.CreateSolidBrush(m_clrBack);
	m_pProp = pProp;
}

CPropSliderCtrl::~CPropSliderCtrl()
{
}

BEGIN_MESSAGE_MAP(CPropSliderCtrl, CSliderCtrl)
	//{{AFX_MSG_MAP(CPropSliderCtrl)
	ON_WM_CTLCOLOR_REFLECT()
	ON_WM_HSCROLL_REFLECT()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CPropSliderCtrl message handlers

HBRUSH CPropSliderCtrl::CtlColor(CDC* pDC, UINT nCtlColor)
{
	pDC->SetBkColor(m_clrBack);
	return m_brBackground;
}

void CPropSliderCtrl::HScroll(UINT nSBCode, UINT nPos)
{
	ASSERT_VALID(m_pProp);
	m_pProp->OnUpdateValue();
	m_pProp->Redraw();
	
}

////////////////////////////////////////////////////////////////////////////////
// CSliderProp class

CSliderProp::CSliderProp(const CString& strName, long nValue, LPCTSTR lpszDescr, DWORD dwData) :
	CMFCPropertyGridProperty(strName, nValue, lpszDescr, dwData)
{
	
}

CWnd* CSliderProp::CreateInPlaceEdit(CRect rectEdit, BOOL& bDefaultFormat)
{
	CPropSliderCtrl* pWndSlider = new CPropSliderCtrl(this, m_pWndList->GetBkColor());

	rectEdit.left += rectEdit.Height() + 15;
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	pWndSlider->Create(WS_VISIBLE | WS_CHILD, rectEdit, m_pWndList, AFX_PROPLIST_ID_INPLACE);
	pWndSlider->SetPos(m_varValue.lVal);

	bDefaultFormat = TRUE;
	return pWndSlider;
}

BOOL CSliderProp::OnUpdateValue()
{

	ASSERT_VALID(this);
	ASSERT_VALID(m_pWndInPlace);
	ASSERT_VALID(m_pWndList);
	ASSERT(::IsWindow(m_pWndInPlace->GetSafeHwnd()));

	long lCurrValue = m_varValue.lVal;

	CSliderCtrl* pSlider = (CSliderCtrl*)m_pWndInPlace;

	m_varValue = (long)pSlider->GetPos();

	if (lCurrValue != m_varValue.lVal)
	{
		m_pWndList->OnPropertyChanged(this);
	}

	return TRUE;
}


// CPropSliderCtrl message handlers


