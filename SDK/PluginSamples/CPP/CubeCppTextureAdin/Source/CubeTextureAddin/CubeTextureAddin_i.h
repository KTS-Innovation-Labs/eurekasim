

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0622 */
/* at Tue Jan 19 08:44:07 2038
 */
/* Compiler settings for CubeTextureAddin.idl:
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

#ifndef __CubeTextureAddin_i_h__
#define __CubeTextureAddin_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __ICubeTextureAddinSimulation_FWD_DEFINED__
#define __ICubeTextureAddinSimulation_FWD_DEFINED__
typedef interface ICubeTextureAddinSimulation ICubeTextureAddinSimulation;

#endif 	/* __ICubeTextureAddinSimulation_FWD_DEFINED__ */


#ifndef __CubeTextureAddinSimulation_FWD_DEFINED__
#define __CubeTextureAddinSimulation_FWD_DEFINED__

#ifdef __cplusplus
typedef class CubeTextureAddinSimulation CubeTextureAddinSimulation;
#else
typedef struct CubeTextureAddinSimulation CubeTextureAddinSimulation;
#endif /* __cplusplus */

#endif 	/* __CubeTextureAddinSimulation_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __ICubeTextureAddinSimulation_INTERFACE_DEFINED__
#define __ICubeTextureAddinSimulation_INTERFACE_DEFINED__

/* interface ICubeTextureAddinSimulation */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ICubeTextureAddinSimulation;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("CF2C8CEE-F445-4F90-99D2-1277679D8597")
    ICubeTextureAddinSimulation : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokePreferenceSettings( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokeDefaultSettings( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokeOnExperimentChanged( void) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct ICubeTextureAddinSimulationVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ICubeTextureAddinSimulation * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ICubeTextureAddinSimulation * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ICubeTextureAddinSimulation * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ICubeTextureAddinSimulation * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ICubeTextureAddinSimulation * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ICubeTextureAddinSimulation * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ICubeTextureAddinSimulation * This,
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
            ICubeTextureAddinSimulation * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokeDefaultSettings )( 
            ICubeTextureAddinSimulation * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokeOnExperimentChanged )( 
            ICubeTextureAddinSimulation * This);
        
        END_INTERFACE
    } ICubeTextureAddinSimulationVtbl;

    interface ICubeTextureAddinSimulation
    {
        CONST_VTBL struct ICubeTextureAddinSimulationVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ICubeTextureAddinSimulation_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ICubeTextureAddinSimulation_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ICubeTextureAddinSimulation_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ICubeTextureAddinSimulation_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define ICubeTextureAddinSimulation_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define ICubeTextureAddinSimulation_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define ICubeTextureAddinSimulation_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define ICubeTextureAddinSimulation_InvokePreferenceSettings(This)	\
    ( (This)->lpVtbl -> InvokePreferenceSettings(This) ) 

#define ICubeTextureAddinSimulation_InvokeDefaultSettings(This)	\
    ( (This)->lpVtbl -> InvokeDefaultSettings(This) ) 

#define ICubeTextureAddinSimulation_InvokeOnExperimentChanged(This)	\
    ( (This)->lpVtbl -> InvokeOnExperimentChanged(This) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ICubeTextureAddinSimulation_INTERFACE_DEFINED__ */



#ifndef __CubeTextureAddinLib_LIBRARY_DEFINED__
#define __CubeTextureAddinLib_LIBRARY_DEFINED__

/* library CubeTextureAddinLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_CubeTextureAddinLib;

EXTERN_C const CLSID CLSID_CubeTextureAddinSimulation;

#ifdef __cplusplus

class DECLSPEC_UUID("f79810d7-16a0-41a4-a0e9-01d74a153ffd")
CubeTextureAddinSimulation;
#endif
#endif /* __CubeTextureAddinLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


