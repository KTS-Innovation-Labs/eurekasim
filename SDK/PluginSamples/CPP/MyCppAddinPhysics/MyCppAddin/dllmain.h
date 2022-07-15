// dllmain.h : Declaration of module class.

class CMyCppAddinModule : public ATL::CAtlDllModuleT< CMyCppAddinModule >
{
public :
	DECLARE_LIBID(LIBID_MyCppAddinLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_MYCPPADDIN, "{BAE70F40-138A-49A3-AAF1-83533AB5A7A4}")
};

extern class CMyCppAddinModule _AtlModule;
