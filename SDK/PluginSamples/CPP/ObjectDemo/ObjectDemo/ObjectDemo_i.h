

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Mon Nov 02 22:07:11 2026
 */
/* Compiler settings for ObjectDemo.idl:
    Oicf, W1, Zp8, env=Win64 (32b run), target_arch=AMD64 8.00.0603 
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

#ifndef __ObjectDemo_i_h__
#define __ObjectDemo_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IObjectDemoSimulation_FWD_DEFINED__
#define __IObjectDemoSimulation_FWD_DEFINED__
typedef interface IObjectDemoSimulation IObjectDemoSimulation;

#endif 	/* __IObjectDemoSimulation_FWD_DEFINED__ */


#ifndef __ObjectDemoSimulation_FWD_DEFINED__
#define __ObjectDemoSimulation_FWD_DEFINED__

#ifdef __cplusplus
typedef class ObjectDemoSimulation ObjectDemoSimulation;
#else
typedef struct ObjectDemoSimulation ObjectDemoSimulation;
#endif /* __cplusplus */

#endif 	/* __ObjectDemoSimulation_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IObjectDemoSimulation_INTERFACE_DEFINED__
#define __IObjectDemoSimulation_INTERFACE_DEFINED__

/* interface IObjectDemoSimulation */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IObjectDemoSimulation;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("CF2C8CEE-F445-4F90-99D2-1277679D8597")
    IObjectDemoSimulation : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokePreferenceSettings( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokeDefaultSettings( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokeOnExperimentChanged( void) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IObjectDemoSimulationVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IObjectDemoSimulation * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IObjectDemoSimulation * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IObjectDemoSimulation * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IObjectDemoSimulation * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IObjectDemoSimulation * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IObjectDemoSimulation * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IObjectDemoSimulation * This,
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
            IObjectDemoSimulation * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokeDefaultSettings )( 
            IObjectDemoSimulation * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokeOnExperimentChanged )( 
            IObjectDemoSimulation * This);
        
        END_INTERFACE
    } IObjectDemoSimulationVtbl;

    interface IObjectDemoSimulation
    {
        CONST_VTBL struct IObjectDemoSimulationVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IObjectDemoSimulation_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IObjectDemoSimulation_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IObjectDemoSimulation_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IObjectDemoSimulation_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IObjectDemoSimulation_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IObjectDemoSimulation_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IObjectDemoSimulation_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IObjectDemoSimulation_InvokePreferenceSettings(This)	\
    ( (This)->lpVtbl -> InvokePreferenceSettings(This) ) 

#define IObjectDemoSimulation_InvokeDefaultSettings(This)	\
    ( (This)->lpVtbl -> InvokeDefaultSettings(This) ) 

#define IObjectDemoSimulation_InvokeOnExperimentChanged(This)	\
    ( (This)->lpVtbl -> InvokeOnExperimentChanged(This) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IObjectDemoSimulation_INTERFACE_DEFINED__ */



#ifndef __ObjectDemoLib_LIBRARY_DEFINED__
#define __ObjectDemoLib_LIBRARY_DEFINED__

/* library ObjectDemoLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_ObjectDemoLib;

EXTERN_C const CLSID CLSID_ObjectDemoSimulation;

#ifdef __cplusplus

class DECLSPEC_UUID("40642f86-2be2-4fe6-aa24-a44ed20b7153")
ObjectDemoSimulation;
#endif
#endif /* __ObjectDemoLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


