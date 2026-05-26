// dllmain.h : Declaration of module class.

class CEmWaveModule : public ATL::CAtlDllModuleT< CEmWaveModule >
{
public :
	DECLARE_LIBID(LIBID_EmWaveLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_MYCPPADDIN, "{BAE70F40-138A-49A3-AAF1-83533AB5A7A4}")
};

extern class CEmWaveModule _AtlModule;
