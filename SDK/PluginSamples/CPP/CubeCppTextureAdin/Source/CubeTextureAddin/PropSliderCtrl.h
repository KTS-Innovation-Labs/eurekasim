#pragma once


// CPropSliderCtrl
class CSliderProp : public CMFCPropertyGridProperty
{
public:
	CSliderProp(const CString& strName, long nValue, LPCTSTR lpszDescr = NULL, DWORD dwData = 0);

	virtual BOOL OnUpdateValue();

protected:
	virtual CWnd* CreateInPlaceEdit(CRect rectEdit, BOOL& bDefaultFormat);
	virtual BOOL OnSetCursor() const { return FALSE; /* Use default */ }
};

class CPropSliderCtrl : public CSliderCtrl
{
	// Construction
public:
	CPropSliderCtrl(CSliderProp* pProp, COLORREF clrBack);

	// Attributes
protected:
	CBrush m_brBackground;
	COLORREF m_clrBack;
	CSliderProp* m_pProp;

	// Implementation
public:
	virtual ~CPropSliderCtrl();

protected:
	//{{AFX_MSG(CPropSliderCtrl)
	afx_msg HBRUSH CtlColor(CDC* pDC, UINT nCtlColor);
	//}}AFX_MSG
	afx_msg void HScroll(UINT nSBCode, UINT nPos);

	DECLARE_MESSAGE_MAP()
};



