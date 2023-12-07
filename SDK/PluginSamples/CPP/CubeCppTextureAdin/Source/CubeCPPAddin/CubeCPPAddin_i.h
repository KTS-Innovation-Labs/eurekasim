

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0622 */
/* at Tue Jan 19 08:44:07 2038
 */
/* Compiler settings for CubeCPPAddin.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.01.0622 
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

#ifndef __CubeCPPAddin_i_h__
#define __CubeCPPAddin_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ICubeCPPAddinSimulation_FWD_DEFINED__
#define __ICubeCPPAddinSimulation_FWD_DEFINED__
typedef interface ICubeCPPAddinSimulation ICubeCPPAddinSimulation;

#endif 	/* __ICubeCPPAddinSimulation_FWD_DEFINED__ */


#ifndef __CubeCPPAddinSimulation_FWD_DEFINED__
#define __CubeCPPAddinSimulation_FWD_DEFINED__

#ifdef __cplusplus
typedef class CubeCPPAddinSimulation CubeCPPAddinSimulation;
#else
typedef struct CubeCPPAddinSimulation CubeCPPAddinSimulation;
#endif /* __cplusplus */

#endif 	/* __CubeCPPAddinSimulation_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __ICubeCPPAddinSimulation_INTERFACE_DEFINED__
#define __ICubeCPPAddinSimulation_INTERFACE_DEFINED__

/* interface ICubeCPPAddinSimulation */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ICubeCPPAddinSimulation;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("CF2C8CEE-F445-4F90-99D2-1277679D8597")
    ICubeCPPAddinSimulation : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokePreferenceSettings( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokeDefaultSettings( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokeOnExperimentChanged( void) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct ICubeCPPAddinSimulationVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ICubeCPPAddinSimulation * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ICubeCPPAddinSimulation * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ICubeCPPAddinSimulation * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ICubeCPPAddinSimulation * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ICubeCPPAddinSimulation * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ICubeCPPAddinSimulation * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ICubeCPPAddinSimulation * This,
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
            ICubeCPPAddinSimulation * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokeDefaultSettings )( 
            ICubeCPPAddinSimulation * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokeOnExperimentChanged )( 
            ICubeCPPAddinSimulation * This);
        
        END_INTERFACE
    } ICubeCPPAddinSimulationVtbl;

    interface ICubeCPPAddinSimulation
    {
        CONST_VTBL struct ICubeCPPAddinSimulationVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ICubeCPPAddinSimulation_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ICubeCPPAddinSimulation_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ICubeCPPAddinSimulation_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ICubeCPPAddinSimulation_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define ICubeCPPAddinSimulation_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define ICubeCPPAddinSimulation_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define ICubeCPPAddinSimulation_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define ICubeCPPAddinSimulation_InvokePreferenceSettings(This)	\
    ( (This)->lpVtbl -> InvokePreferenceSettings(This) ) 

#define ICubeCPPAddinSimulation_InvokeDefaultSettings(This)	\
    ( (This)->lpVtbl -> InvokeDefaultSettings(This) ) 

#define ICubeCPPAddinSimulation_InvokeOnExperimentChanged(This)	\
    ( (This)->lpVtbl -> InvokeOnExperimentChanged(This) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ICubeCPPAddinSimulation_INTERFACE_DEFINED__ */



#ifndef __CubeCPPAddinLib_LIBRARY_DEFINED__
#define __CubeCPPAddinLib_LIBRARY_DEFINED__

/* library CubeCPPAddinLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_CubeCPPAddinLib;

EXTERN_C const CLSID CLSID_CubeCPPAddinSimulation;

#ifdef __cplusplus

class DECLSPEC_UUID("a5603221-5cb4-43f4-8e66-87462d8cff91")
CubeCPPAddinSimulation;
#endif
#endif /* __CubeCPPAddinLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


