

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Tue Feb 08 14:27:56 2022
 */
/* Compiler settings for MyCppAddin.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.00.0603 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __MyCppAddin_i_h__
#define __MyCppAddin_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IMySimulationAddin_FWD_DEFINED__
#define __IMySimulationAddin_FWD_DEFINED__
typedef interface IMySimulationAddin IMySimulationAddin;

#endif 	/* __IMySimulationAddin_FWD_DEFINED__ */


#ifndef __MySimulationAddin_FWD_DEFINED__
#define __MySimulationAddin_FWD_DEFINED__

#ifdef __cplusplus
typedef class MySimulationAddin MySimulationAddin;
#else
typedef struct MySimulationAddin MySimulationAddin;
#endif /* __cplusplus */

#endif 	/* __MySimulationAddin_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IMySimulationAddin_INTERFACE_DEFINED__
#define __IMySimulationAddin_INTERFACE_DEFINED__

/* interface IMySimulationAddin */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IMySimulationAddin;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("CF2C8CEE-F445-4F90-99D2-1277679D8597")
    IMySimulationAddin : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokePreferenceSettings( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokeDefaultSettings( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokeOnExperimentChanged( void) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IMySimulationAddinVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IMySimulationAddin * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IMySimulationAddin * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IMySimulationAddin * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IMySimulationAddin * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IMySimulationAddin * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IMySimulationAddin * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IMySimulationAddin * This,
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
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokePreferenceSettings )( 
            IMySimulationAddin * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokeDefaultSettings )( 
            IMySimulationAddin * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokeOnExperimentChanged )( 
            IMySimulationAddin * This);
        
        END_INTERFACE
    } IMySimulationAddinVtbl;

    interface IMySimulationAddin
    {
        CONST_VTBL struct IMySimulationAddinVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IMySimulationAddin_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IMySimulationAddin_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IMySimulationAddin_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IMySimulationAddin_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IMySimulationAddin_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IMySimulationAddin_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IMySimulationAddin_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IMySimulationAddin_InvokePreferenceSettings(This)	\
    ( (This)->lpVtbl -> InvokePreferenceSettings(This) ) 

#define IMySimulationAddin_InvokeDefaultSettings(This)	\
    ( (This)->lpVtbl -> InvokeDefaultSettings(This) ) 

#define IMySimulationAddin_InvokeOnExperimentChanged(This)	\
    ( (This)->lpVtbl -> InvokeOnExperimentChanged(This) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IMySimulationAddin_INTERFACE_DEFINED__ */



#ifndef __MyCppAddinLib_LIBRARY_DEFINED__
#define __MyCppAddinLib_LIBRARY_DEFINED__

/* library MyCppAddinLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_MyCppAddinLib;

EXTERN_C const CLSID CLSID_MySimulationAddin;

#ifdef __cplusplus

class DECLSPEC_UUID("EB46B87E-21C7-49A6-B6A4-17D6F3D89D7E")
MySimulationAddin;
#endif
#endif /* __MyCppAddinLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


