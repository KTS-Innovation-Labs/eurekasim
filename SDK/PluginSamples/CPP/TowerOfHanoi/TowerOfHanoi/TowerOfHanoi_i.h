

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0628 */
/* at Tue Jan 19 08:44:07 2038
 */
/* Compiler settings for TowerOfHanoi.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.01.0628 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */



/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 500
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __TowerOfHanoi_i_h__
#define __TowerOfHanoi_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

#ifndef DECLSPEC_XFGVIRT
#if defined(_CONTROL_FLOW_GUARD_XFG)
#define DECLSPEC_XFGVIRT(base, func) __declspec(xfg_virtual(base, func))
#else
#define DECLSPEC_XFGVIRT(base, func)
#endif
#endif

/* Forward Declarations */ 

#ifndef __ITowerOfHanoiSimulation_FWD_DEFINED__
#define __ITowerOfHanoiSimulation_FWD_DEFINED__
typedef interface ITowerOfHanoiSimulation ITowerOfHanoiSimulation;

#endif 	/* __ITowerOfHanoiSimulation_FWD_DEFINED__ */


#ifndef __TowerOfHanoiSimulation_FWD_DEFINED__
#define __TowerOfHanoiSimulation_FWD_DEFINED__

#ifdef __cplusplus
typedef class TowerOfHanoiSimulation TowerOfHanoiSimulation;
#else
typedef struct TowerOfHanoiSimulation TowerOfHanoiSimulation;
#endif /* __cplusplus */

#endif 	/* __TowerOfHanoiSimulation_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __ITowerOfHanoiSimulation_INTERFACE_DEFINED__
#define __ITowerOfHanoiSimulation_INTERFACE_DEFINED__

/* interface ITowerOfHanoiSimulation */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ITowerOfHanoiSimulation;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("CF2C8CEE-F445-4F90-99D2-1277679D8597")
    ITowerOfHanoiSimulation : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokePreferenceSettings( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokeDefaultSettings( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokeOnExperimentChanged( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE OnNumRingsChange( 
            /* [in] */ BSTR CtrlID,
            /* [in] */ long Index) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE OnSpeedChange( 
            /* [in] */ BSTR CtrlID,
            /* [in] */ long Index) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct ITowerOfHanoiSimulationVtbl
    {
        BEGIN_INTERFACE
        
        DECLSPEC_XFGVIRT(IUnknown, QueryInterface)
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ITowerOfHanoiSimulation * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        DECLSPEC_XFGVIRT(IUnknown, AddRef)
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ITowerOfHanoiSimulation * This);
        
        DECLSPEC_XFGVIRT(IUnknown, Release)
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ITowerOfHanoiSimulation * This);
        
        DECLSPEC_XFGVIRT(IDispatch, GetTypeInfoCount)
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ITowerOfHanoiSimulation * This,
            /* [out] */ UINT *pctinfo);
        
        DECLSPEC_XFGVIRT(IDispatch, GetTypeInfo)
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ITowerOfHanoiSimulation * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        DECLSPEC_XFGVIRT(IDispatch, GetIDsOfNames)
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ITowerOfHanoiSimulation * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        DECLSPEC_XFGVIRT(IDispatch, Invoke)
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ITowerOfHanoiSimulation * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        DECLSPEC_XFGVIRT(ITowerOfHanoiSimulation, InvokePreferenceSettings)
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokePreferenceSettings )( 
            ITowerOfHanoiSimulation * This);
        
        DECLSPEC_XFGVIRT(ITowerOfHanoiSimulation, InvokeDefaultSettings)
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokeDefaultSettings )( 
            ITowerOfHanoiSimulation * This);
        
        DECLSPEC_XFGVIRT(ITowerOfHanoiSimulation, InvokeOnExperimentChanged)
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokeOnExperimentChanged )( 
            ITowerOfHanoiSimulation * This);
        
        DECLSPEC_XFGVIRT(ITowerOfHanoiSimulation, OnNumRingsChange)
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *OnNumRingsChange )( 
            ITowerOfHanoiSimulation * This,
            /* [in] */ BSTR CtrlID,
            /* [in] */ long Index);
        
        DECLSPEC_XFGVIRT(ITowerOfHanoiSimulation, OnSpeedChange)
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *OnSpeedChange )( 
            ITowerOfHanoiSimulation * This,
            /* [in] */ BSTR CtrlID,
            /* [in] */ long Index);
        
        END_INTERFACE
    } ITowerOfHanoiSimulationVtbl;

    interface ITowerOfHanoiSimulation
    {
        CONST_VTBL struct ITowerOfHanoiSimulationVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ITowerOfHanoiSimulation_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ITowerOfHanoiSimulation_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ITowerOfHanoiSimulation_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ITowerOfHanoiSimulation_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define ITowerOfHanoiSimulation_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define ITowerOfHanoiSimulation_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define ITowerOfHanoiSimulation_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define ITowerOfHanoiSimulation_InvokePreferenceSettings(This)	\
    ( (This)->lpVtbl -> InvokePreferenceSettings(This) ) 

#define ITowerOfHanoiSimulation_InvokeDefaultSettings(This)	\
    ( (This)->lpVtbl -> InvokeDefaultSettings(This) ) 

#define ITowerOfHanoiSimulation_InvokeOnExperimentChanged(This)	\
    ( (This)->lpVtbl -> InvokeOnExperimentChanged(This) ) 

#define ITowerOfHanoiSimulation_OnNumRingsChange(This,CtrlID,Index)	\
    ( (This)->lpVtbl -> OnNumRingsChange(This,CtrlID,Index) ) 

#define ITowerOfHanoiSimulation_OnSpeedChange(This,CtrlID,Index)	\
    ( (This)->lpVtbl -> OnSpeedChange(This,CtrlID,Index) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ITowerOfHanoiSimulation_INTERFACE_DEFINED__ */



#ifndef __TowerOfHanoiLib_LIBRARY_DEFINED__
#define __TowerOfHanoiLib_LIBRARY_DEFINED__

/* library TowerOfHanoiLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_TowerOfHanoiLib;

EXTERN_C const CLSID CLSID_TowerOfHanoiSimulation;

#ifdef __cplusplus

class DECLSPEC_UUID("d8a5bb51-5b4a-417c-86ca-055f4809fd7e")
TowerOfHanoiSimulation;
#endif
#endif /* __TowerOfHanoiLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

unsigned long             __RPC_USER  BSTR_UserSize64(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal64(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal64(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree64(     unsigned long *, BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


