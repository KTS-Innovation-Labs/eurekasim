

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0622 */
/* at Tue Jan 19 08:44:07 2038
 */
/* Compiler settings for MyCppAddin.idl:
    Oicf, W1, Zp8, env=Win64 (32b run), target_arch=AMD64 8.01.0622 
    protocol : all , ms_ext, c_ext, robust
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

#ifndef __MyCppAddin_i_h__
#define __MyCppAddin_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IMyCppAddinSimulation_FWD_DEFINED__
#define __IMyCppAddinSimulation_FWD_DEFINED__
typedef interface IMyCppAddinSimulation IMyCppAddinSimulation;

#endif 	/* __IMyCppAddinSimulation_FWD_DEFINED__ */


#ifndef __MyCppAddinSimulation_FWD_DEFINED__
#define __MyCppAddinSimulation_FWD_DEFINED__

#ifdef __cplusplus
typedef class MyCppAddinSimulation MyCppAddinSimulation;
#else
typedef struct MyCppAddinSimulation MyCppAddinSimulation;
#endif /* __cplusplus */

#endif 	/* __MyCppAddinSimulation_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IMyCppAddinSimulation_INTERFACE_DEFINED__
#define __IMyCppAddinSimulation_INTERFACE_DEFINED__

/* interface IMyCppAddinSimulation */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IMyCppAddinSimulation;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("CF2C8CEE-F445-4F90-99D2-1277679D8597")
    IMyCppAddinSimulation : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokePreferenceSettings( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokeDefaultSettings( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokeOnExperimentChanged( void) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IMyCppAddinSimulationVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IMyCppAddinSimulation * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IMyCppAddinSimulation * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IMyCppAddinSimulation * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IMyCppAddinSimulation * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IMyCppAddinSimulation * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IMyCppAddinSimulation * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IMyCppAddinSimulation * This,
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
            IMyCppAddinSimulation * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokeDefaultSettings )( 
            IMyCppAddinSimulation * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokeOnExperimentChanged )( 
            IMyCppAddinSimulation * This);
        
        END_INTERFACE
    } IMyCppAddinSimulationVtbl;

    interface IMyCppAddinSimulation
    {
        CONST_VTBL struct IMyCppAddinSimulationVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IMyCppAddinSimulation_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IMyCppAddinSimulation_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IMyCppAddinSimulation_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IMyCppAddinSimulation_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IMyCppAddinSimulation_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IMyCppAddinSimulation_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IMyCppAddinSimulation_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IMyCppAddinSimulation_InvokePreferenceSettings(This)	\
    ( (This)->lpVtbl -> InvokePreferenceSettings(This) ) 

#define IMyCppAddinSimulation_InvokeDefaultSettings(This)	\
    ( (This)->lpVtbl -> InvokeDefaultSettings(This) ) 

#define IMyCppAddinSimulation_InvokeOnExperimentChanged(This)	\
    ( (This)->lpVtbl -> InvokeOnExperimentChanged(This) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IMyCppAddinSimulation_INTERFACE_DEFINED__ */



#ifndef __MyCppAddinLib_LIBRARY_DEFINED__
#define __MyCppAddinLib_LIBRARY_DEFINED__

/* library MyCppAddinLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_MyCppAddinLib;

EXTERN_C const CLSID CLSID_MyCppAddinSimulation;

#ifdef __cplusplus

class DECLSPEC_UUID("EB46B87E-21C7-49A6-B6A4-17D6F3D89D7E")
MyCppAddinSimulation;
#endif
#endif /* __MyCppAddinLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


