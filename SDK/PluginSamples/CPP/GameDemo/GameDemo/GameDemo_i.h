

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0603 */
/* at Sat Nov 29 10:58:45 2025
 */
/* Compiler settings for GameDemo.idl:
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

#ifndef __GameDemo_i_h__
#define __GameDemo_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IGameDemoSimulation_FWD_DEFINED__
#define __IGameDemoSimulation_FWD_DEFINED__
typedef interface IGameDemoSimulation IGameDemoSimulation;

#endif 	/* __IGameDemoSimulation_FWD_DEFINED__ */


#ifndef __GameDemoSimulation_FWD_DEFINED__
#define __GameDemoSimulation_FWD_DEFINED__

#ifdef __cplusplus
typedef class GameDemoSimulation GameDemoSimulation;
#else
typedef struct GameDemoSimulation GameDemoSimulation;
#endif /* __cplusplus */

#endif 	/* __GameDemoSimulation_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IGameDemoSimulation_INTERFACE_DEFINED__
#define __IGameDemoSimulation_INTERFACE_DEFINED__

/* interface IGameDemoSimulation */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IGameDemoSimulation;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("CF2C8CEE-F445-4F90-99D2-1277679D8597")
    IGameDemoSimulation : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokePreferenceSettings( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokeDefaultSettings( void) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE InvokeOnExperimentChanged( void) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IGameDemoSimulationVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IGameDemoSimulation * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IGameDemoSimulation * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IGameDemoSimulation * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IGameDemoSimulation * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IGameDemoSimulation * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IGameDemoSimulation * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IGameDemoSimulation * This,
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
            IGameDemoSimulation * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokeDefaultSettings )( 
            IGameDemoSimulation * This);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *InvokeOnExperimentChanged )( 
            IGameDemoSimulation * This);
        
        END_INTERFACE
    } IGameDemoSimulationVtbl;

    interface IGameDemoSimulation
    {
        CONST_VTBL struct IGameDemoSimulationVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IGameDemoSimulation_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IGameDemoSimulation_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IGameDemoSimulation_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IGameDemoSimulation_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IGameDemoSimulation_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IGameDemoSimulation_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IGameDemoSimulation_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IGameDemoSimulation_InvokePreferenceSettings(This)	\
    ( (This)->lpVtbl -> InvokePreferenceSettings(This) ) 

#define IGameDemoSimulation_InvokeDefaultSettings(This)	\
    ( (This)->lpVtbl -> InvokeDefaultSettings(This) ) 

#define IGameDemoSimulation_InvokeOnExperimentChanged(This)	\
    ( (This)->lpVtbl -> InvokeOnExperimentChanged(This) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IGameDemoSimulation_INTERFACE_DEFINED__ */



#ifndef __GameDemoLib_LIBRARY_DEFINED__
#define __GameDemoLib_LIBRARY_DEFINED__

/* library GameDemoLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_GameDemoLib;

EXTERN_C const CLSID CLSID_GameDemoSimulation;

#ifdef __cplusplus

class DECLSPEC_UUID("aaf1b6e9-178e-4645-8e6d-78c1cb2cdacb")
GameDemoSimulation;
#endif
#endif /* __GameDemoLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


