# -*- coding: mbcs -*-
# Created by makepy.py version 0.5.01
# By python version 3.10.5 (tags/v3.10.5:f377153, Jun  6 2022, 16:14:13) [MSC v.1929 64 bit (AMD64)]
# From type library 'EurekaSim.tlb'
# On Wed Nov 22 13:18:09 2023
''
makepy_version = '0.5.01'
python_version = 0x30a05f0

import win32com.client.CLSIDToClass, pythoncom, pywintypes
import win32com.client.util
from pywintypes import IID
from win32com.client import Dispatch

# The following 3 lines may need tweaking for the particular server
# Candidates are pythoncom.Missing, .Empty and .ArgNotFound
defaultNamedOptArg=pythoncom.Empty
defaultNamedNotOptArg=pythoncom.Empty
defaultUnnamedArg=pythoncom.Empty

CLSID = IID('{38379857-2986-4CD5-B26F-C2FED468D342}')
MajorVersion = 1
MinorVersion = 0
LibraryFlags = 8
LCID = 0x0

from win32com.client import DispatchBaseClass
class IAddin(DispatchBaseClass):
	CLSID = IID('{1186BC6B-64A7-4415-BDF6-4B192BB27733}')
	coclass_clsid = IID('{70AFC313-A879-4012-812D-AA714AEF8C6F}')

	def About(self):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), (),)

	def Initialize(self, lSessionID=defaultNamedNotOptArg, pApp=defaultNamedNotOptArg, bFirstTime=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((3, 0), (9, 0), (12, 0)),lSessionID
			, pApp, bFirstTime)

	def Uninitialize(self, lSessionID=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((3, 0),),lSessionID
			)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IApplicationChart(DispatchBaseClass):
	CLSID = IID('{CDFE7248-527E-4890-B1B3-24E372679CBE}')
	coclass_clsid = IID('{9D8447A5-48EA-45D9-9946-F1153C1C4888}')

	def DeleteAllCharts(self):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), (),)

	# The method GraphCtrl2dObj is actually a property, but must be used as a method to correctly pass the arguments
	def GraphCtrl2dObj(self, Index=defaultNamedNotOptArg):
		ret = self._oleobj_.InvokeTypes(11, LCID, 2, (9, 0), ((3, 0),),Index
			)
		if ret is not None:
			ret = Dispatch(ret, 'GraphCtrl2dObj', None)
		return ret

	# The method GraphCtrl3dObj is actually a property, but must be used as a method to correctly pass the arguments
	def GraphCtrl3dObj(self, Index=defaultNamedNotOptArg):
		ret = self._oleobj_.InvokeTypes(10, LCID, 2, (9, 0), ((3, 0),),Index
			)
		if ret is not None:
			ret = Dispatch(ret, 'GraphCtrl3dObj', None)
		return ret

	def Initialize2dChart(self, NoOfCharts=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((3, 0),),NoOfCharts
			)

	def Initialize3dChart(self, NoOfCharts=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(8, LCID, 1, (24, 0), ((3, 0),),NoOfCharts
			)

	def InitializeChart(self, NoOfCharts=defaultNamedNotOptArg, pChartTypeArray=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(9, LCID, 1, (24, 0), ((3, 0), (16387, 0)),NoOfCharts
			, pChartTypeArray)

	def ResizeChartWindow(self):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), (),)

	def Set2dAxisRange(self, GraphIndex=defaultNamedNotOptArg, AxisType=defaultNamedNotOptArg, MinValue=defaultNamedNotOptArg, MaxValue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(7, LCID, 1, (24, 0), ((3, 0), (3, 0), (5, 0), (5, 0)),GraphIndex
			, AxisType, MinValue, MaxValue)

	def Set2dChartData(self, Index=defaultNamedNotOptArg, DataArray=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(6, LCID, 1, (24, 0), ((3, 0), (12, 0)),Index
			, DataArray)

	def Set2dGraphInfo(self, GraphIndex=defaultNamedNotOptArg, GraphTitle=defaultNamedNotOptArg, GraphXAxisTitle=defaultNamedNotOptArg, GraphYAxisTitle=defaultNamedNotOptArg
			, ShowGraph=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), ((3, 0), (8, 0), (8, 0), (8, 0), (3, 0)),GraphIndex
			, GraphTitle, GraphXAxisTitle, GraphYAxisTitle, ShowGraph)

	_prop_map_get_ = {
		"ChartWindowMode": (1, 2, (3, 0), (), "ChartWindowMode", None),
	}
	_prop_map_put_ = {
		"ChartWindowMode": ((1, LCID, 4, 0),()),
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IApplicationDocument(DispatchBaseClass):
	CLSID = IID('{E03BD2AF-D443-4E17-AD85-C0D2004321C7}')
	coclass_clsid = IID('{9025BDCC-4135-4453-99F8-6FF16C1822DC}')

	def CloseActiveDocument(self):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), (),)

	def GetAddinSettingsAsString(self, PluginName=defaultNamedNotOptArg, Key=defaultNamedNotOptArg, Value=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((8, 0), (8, 0), (16392, 0)),PluginName
			, Key, Value)

	def LaunchNewDocument(self):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), (),)

	def OpenDocument(self, FilePath=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((8, 0),),FilePath
			)

	def SetAddinSettingsAsString(self, PluginName=defaultNamedNotOptArg, Key=defaultNamedNotOptArg, Value=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((8, 0), (8, 0), (8, 0)),PluginName
			, Key, Value)

	_prop_map_get_ = {
		"DisplayExpParamStatus": (9, 2, (3, 0), (), "DisplayExpParamStatus", None),
		"DisplayRealTimeGraphStatus": (7, 2, (3, 0), (), "DisplayRealTimeGraphStatus", None),
		"EnableVisualizationMode": (12, 2, (3, 0), (), "EnableVisualizationMode", None),
		"LogToCSVFileStatus": (6, 2, (3, 0), (), "LogToCSVFileStatus", None),
		"RecordSimulationAsVideoStatus": (8, 2, (3, 0), (), "RecordSimulationAsVideoStatus", None),
		"ShowGraphInMainWindow": (13, 2, (3, 0), (), "ShowGraphInMainWindow", None),
		"VisualizationMode": (10, 2, (3, 0), (), "VisualizationMode", None),
		"VisualizationMode3D": (11, 2, (3, 0), (), "VisualizationMode3D", None),
	}
	_prop_map_put_ = {
		"DisplayExpParamStatus": ((9, LCID, 4, 0),()),
		"DisplayRealTimeGraphStatus": ((7, LCID, 4, 0),()),
		"EnableVisualizationMode": ((12, LCID, 4, 0),()),
		"LogToCSVFileStatus": ((6, LCID, 4, 0),()),
		"RecordSimulationAsVideoStatus": ((8, LCID, 4, 0),()),
		"ShowGraphInMainWindow": ((13, LCID, 4, 0),()),
		"VisualizationMode": ((10, LCID, 4, 0),()),
		"VisualizationMode3D": ((11, LCID, 4, 0),()),
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IApplicationDocumentEvents(DispatchBaseClass):
	CLSID = IID('{902AEB11-0B73-4BE9-9000-3FE482B340A6}')
	coclass_clsid = IID('{41E563DF-22CA-4D51-B40A-EE3DCD62DB6B}')

	def OnAfterSaveDocument(self, DocumentPath=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), ((8, 0),),DocumentPath
			)

	def OnBeforeSaveDocument(self, DocumentPath=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), ((8, 0),),DocumentPath
			)

	def OnCloseDocument(self, DocumentPath=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((8, 0),),DocumentPath
			)

	def OnDocumentOpened(self, DocumentPath=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((8, 0),),DocumentPath
			)

	def OnNewDocument(self, DocumentName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((8, 0),),DocumentName
			)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IApplicationSettings(DispatchBaseClass):
	CLSID = IID('{AB315F3B-305B-44D1-871B-65FAD00B2038}')
	coclass_clsid = IID('{8BCCE11F-D19F-4483-8737-EE54E5696D7F}')

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IApplicationView(DispatchBaseClass):
	CLSID = IID('{497CADA6-3A37-4F5E-A41D-010221D7DEB2}')
	coclass_clsid = IID('{7F4547B3-FC11-4E6E-AFDA-AD77BE3A65A8}')

	def BeginDraw(self, Mode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(7, LCID, 1, (24, 0), ((3, 0),),Mode
			)

	def BeginGraphicsCommands(self):
		return self._oleobj_.InvokeTypes(12, LCID, 1, (24, 0), (),)

	def ClearStockDispLists(self):
		return self._oleobj_.InvokeTypes(11, LCID, 1, (24, 0), (),)

	def ClientRectHeight(self, height=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(45, LCID, 1, (24, 0), ((16387, 0),),height
			)

	def ClientRectWidth(self, Width=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(44, LCID, 1, (24, 0), ((16387, 0),),Width
			)

	def Disable(self, Capability=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(41, LCID, 1, (24, 0), ((3, 0),),Capability
			)

	def Draw2DLine(self, x1=defaultNamedNotOptArg, y1=defaultNamedNotOptArg, x2=defaultNamedNotOptArg, y2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(26, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0), (4, 0)),x1
			, y1, x2, y2)

	def Draw3DLine(self, x1=defaultNamedNotOptArg, y1=defaultNamedNotOptArg, z1=defaultNamedNotOptArg, x2=defaultNamedNotOptArg
			, y2=defaultNamedNotOptArg, z2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(27, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0), (4, 0), (4, 0), (4, 0)),x1
			, y1, z1, x2, y2, z2
			)

	def DrawCylinder(self, baseRadius=defaultNamedNotOptArg, topRadius=defaultNamedNotOptArg, height=defaultNamedNotOptArg, slices=defaultNamedNotOptArg
			, stacks=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(19, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (3, 0), (3, 0)),baseRadius
			, topRadius, height, slices, stacks)

	def DrawDisk(self, innerRadius=defaultNamedNotOptArg, outerRadius=defaultNamedNotOptArg, slices=defaultNamedNotOptArg, loops=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(20, LCID, 1, (24, 0), ((5, 0), (5, 0), (3, 0), (3, 0)),innerRadius
			, outerRadius, slices, loops)

	def DrawPartialDisk(self, innerRadius=defaultNamedNotOptArg, outerRadius=defaultNamedNotOptArg, slices=defaultNamedNotOptArg, loops=defaultNamedNotOptArg
			, startAngle=defaultNamedNotOptArg, sweepAngle=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(21, LCID, 1, (24, 0), ((5, 0), (5, 0), (3, 0), (3, 0), (5, 0), (5, 0)),innerRadius
			, outerRadius, slices, loops, startAngle, sweepAngle
			)

	def DrawPolygon(self, xAxisArray=defaultNamedNotOptArg, yAxisArray=defaultNamedNotOptArg, ArrayCount=defaultNamedNotOptArg, bFill=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(28, LCID, 1, (24, 0), ((16388, 0), (16388, 0), (3, 0), (3, 0)),xAxisArray
			, yAxisArray, ArrayCount, bFill)

	def DrawPredefinedQuadrics(self):
		return self._oleobj_.InvokeTypes(34, LCID, 1, (24, 0), (),)

	def DrawRectangle(self, x1=defaultNamedNotOptArg, y1=defaultNamedNotOptArg, x2=defaultNamedNotOptArg, y2=defaultNamedNotOptArg
			, bFill=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(25, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0), (4, 0), (3, 0)),x1
			, y1, x2, y2, bFill)

	def DrawRotatedObject(self):
		return self._oleobj_.InvokeTypes(36, LCID, 1, (24, 0), (),)

	def DrawSphere(self, Radius=defaultNamedNotOptArg, LogitudeSubdiv=defaultNamedNotOptArg, LatitudeSubdiv=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(18, LCID, 1, (24, 0), ((5, 0), (3, 0), (3, 0)),Radius
			, LogitudeSubdiv, LatitudeSubdiv)

	def DrawStockDispLists(self):
		return self._oleobj_.InvokeTypes(42, LCID, 1, (24, 0), (),)

	def Enable(self, Capability=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(40, LCID, 1, (24, 0), ((3, 0),),Capability
			)

	def EndDraw(self):
		return self._oleobj_.InvokeTypes(8, LCID, 1, (24, 0), (),)

	def EndGraphicsCommands(self):
		return self._oleobj_.InvokeTypes(13, LCID, 1, (24, 0), (),)

	def EndNewDisplayList(self):
		return self._oleobj_.InvokeTypes(15, LCID, 1, (24, 0), (),)

	def EndStockListDef(self):
		return self._oleobj_.InvokeTypes(6, LCID, 1, (24, 0), (),)

	def GetClientRectInfo(self, Left=defaultNamedNotOptArg, Top=defaultNamedNotOptArg, Right=defaultNamedNotOptArg, Bottom=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(43, LCID, 1, (24, 0), ((16387, 0), (16387, 0), (16387, 0), (16387, 0)),Left
			, Top, Right, Bottom)

	def InitailizeDisplayLists(self):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), (),)

	def Initialize(self):
		return self._oleobj_.InvokeTypes(31, LCID, 1, (24, 0), (),)

	def InitializeEnvironment(self, bShowAxis=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(29, LCID, 1, (24, 0), ((3, 0),),bShowAxis
			)

	def IssueRotation(self):
		return self._oleobj_.InvokeTypes(35, LCID, 1, (24, 0), (),)

	def LaunchManageScriptDialog(self):
		return self._oleobj_.InvokeTypes(52, LCID, 1, (24, 0), (),)

	def PopAttrribute(self):
		return self._oleobj_.InvokeTypes(39, LCID, 1, (24, 0), (),)

	def PopMatrix(self):
		return self._oleobj_.InvokeTypes(33, LCID, 1, (24, 0), (),)

	def PushAttrribute(self, Mask=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(38, LCID, 1, (24, 0), ((19, 0),),Mask
			)

	def PushMatrix(self):
		return self._oleobj_.InvokeTypes(32, LCID, 1, (24, 0), (),)

	def Refresh(self):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), (),)

	def ResetScene(self):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), (),)

	def RotateObject(self, Angle=defaultNamedNotOptArg, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(17, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0), (4, 0)),Angle
			, x, y, z)

	def RunScript(self, Script=defaultNamedNotOptArg, pResult=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(50, LCID, 1, (24, 0), ((8, 0), (16392, 0)),Script
			, pResult)

	def RunScriptEx(self, Script=defaultNamedNotOptArg, ScriptType=defaultNamedNotOptArg, pResult=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(51, LCID, 1, (24, 0), ((8, 0), (3, 0), (16392, 0)),Script
			, ScriptType, pResult)

	def Set2DVertexf(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(24, LCID, 1, (24, 0), ((4, 0), (4, 0)),x
			, y)

	def SetBkgColor(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg, Alpha=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(46, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0), (4, 0)),red
			, green, blue, Alpha)

	def SetColorf(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(9, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0)),red
			, green, blue)

	def SetDepth(self, Depth=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(47, LCID, 1, (24, 0), ((4, 0),),Depth
			)

	def SetFontInfo(self, FontName=defaultNamedNotOptArg, height=defaultNamedNotOptArg, bBold=defaultNamedNotOptArg, bItalic=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(49, LCID, 1, (24, 0), ((8, 0), (3, 0), (3, 0), (3, 0)),FontName
			, height, bBold, bItalic)

	def SetLightInfo(self, Light=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(48, LCID, 1, (24, 0), ((3, 0), (3, 0), (16388, 0)),Light
			, PName, Params)

	def SetLineWidth(self, Width=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(23, LCID, 1, (24, 0), ((4, 0),),Width
			)

	def SetVertexf(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(10, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0)),x
			, y, z)

	def SetViewPort(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, Width=defaultNamedNotOptArg, height=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(37, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0), (3, 0)),x
			, y, Width, height)

	def StartNewDisplayList(self):
		return self._oleobj_.InvokeTypes(14, LCID, 1, (24, 0), (),)

	def StartStockDListDef(self):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), (),)

	def TranslateObject(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(16, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0)),x
			, y, z)

	def UpdateDisplay(self):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), (),)

	def UpdatePredefinedScene(self):
		return self._oleobj_.InvokeTypes(22, LCID, 1, (24, 0), (),)

	_prop_map_get_ = {
		"EnableOwnerDraw": (30, 2, (3, 0), (), "EnableOwnerDraw", None),
	}
	_prop_map_put_ = {
		"EnableOwnerDraw": ((30, LCID, 4, 0),()),
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IApplicationViewEvents(DispatchBaseClass):
	CLSID = IID('{5E1D1E5E-FA59-41EF-B2F0-45007F703110}')
	coclass_clsid = IID('{75CCE2A4-4996-4C5E-ACF0-5779519C7A18}')

	def OnActivateView(self, bActivate=defaultNamedNotOptArg, CurrentViewFilePath=defaultNamedNotOptArg, PreviousViewFilePath=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(7, LCID, 1, (24, 0), ((3, 0), (8, 0), (8, 0)),bActivate
			, CurrentViewFilePath, PreviousViewFilePath)

	def OnDrawPredefinedScene(self, Experiment=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((8, 0),),Experiment
			)

	def OnDrawSimulation(self):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), (),)

	def OnInitializeSimulation(self, b3DMode=defaultNamedNotOptArg, VisualizationMode=defaultNamedNotOptArg, Experiment=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((3, 0), (3, 0), (8, 0)),b3DMode
			, VisualizationMode, Experiment)

	def OnOwnerDrawCreate(self):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), (),)

	def OnOwnerDrawSimulation(self):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), (),)

	def ViewWndProc(self, MsgID=defaultNamedNotOptArg, wParam=defaultNamedNotOptArg, lParam=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(6, LCID, 1, (24, 0), ((3, 0), (12, 0), (12, 0)),MsgID
			, wParam, lParam)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IExperiment(DispatchBaseClass):
	CLSID = IID('{C8EEE054-055F-42D5-9C32-AAE2F5DB8CA3}')
	coclass_clsid = IID('{0F56F152-82CD-4B00-9A56-EAE622E22554}')

	def AddExperiment(self, SessionID=defaultNamedNotOptArg, ExperimentName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((3, 0), (8, 0)),SessionID
			, ExperimentName)

	def ClearLogFileContents(self):
		return self._oleobj_.InvokeTypes(7, LCID, 1, (24, 0), (),)

	def CloseLogFile(self):
		return self._oleobj_.InvokeTypes(6, LCID, 1, (24, 0), (),)

	def GetSelectedExperiment(self, pExperimentName=defaultNamedNotOptArg, pAddinSesssionID=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(11, LCID, 1, (24, 0), ((16392, 0), (16387, 0)),pExperimentName
			, pAddinSesssionID)

	def OpenLogFile(self, FilePath=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), ((8, 0),),FilePath
			)

	def StartSimulation(self):
		return self._oleobj_.InvokeTypes(9, LCID, 1, (24, 0), (),)

	def StopSimulation(self):
		return self._oleobj_.InvokeTypes(10, LCID, 1, (24, 0), (),)

	def WriteCSVLogFileHeaderInfo(self, HeaderInfo=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((8, 0),),HeaderInfo
			)

	def WriteToCSVLogFile(self, Info=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((8, 0),),Info
			)

	_prop_map_get_ = {
		"LogFilePath": (4, 2, (8, 0), (), "LogFilePath", None),
		"SimulationState": (8, 2, (3, 0), (), "SimulationState", None),
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IExperimentEvents(DispatchBaseClass):
	CLSID = IID('{92F075D5-45DE-40BF-954F-9BC097FC043B}')
	coclass_clsid = IID('{E9240855-9646-4564-971D-9958CB48CB5C}')

	def OnError(self, ErrorCode=defaultNamedNotOptArg, ErrorDesc=defaultNamedNotOptArg, AdditionalParam=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), ((3, 0), (8, 0), (8, 0)),ErrorCode
			, ErrorDesc, AdditionalParam)

	def OnInitializeLogFileInfo(self, ExperimentName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), ((8, 0),),ExperimentName
			)

	def OnInitializeSimulationGraph(self, ExperimentName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(6, LCID, 1, (24, 0), ((8, 0),),ExperimentName
			)

	def OnInitializeSimulationVideoRecording(self, ExperimentName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(7, LCID, 1, (24, 0), ((8, 0),),ExperimentName
			)

	def OnStartSimulation(self, ExperimentName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((8, 0),),ExperimentName
			)

	def OnStatusChange(self, StatusCode=defaultNamedNotOptArg, StatusDesc=defaultNamedNotOptArg, AdditionalParam=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((3, 0), (8, 0), (8, 0)),StatusCode
			, StatusDesc, AdditionalParam)

	def OnStopSimulation(self, ExperimentName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((8, 0),),ExperimentName
			)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IExperimentTreeView(DispatchBaseClass):
	CLSID = IID('{30038C10-4BA8-4AE5-820F-D22D9C947ECF}')
	coclass_clsid = IID('{894BE66A-5355-47A5-852C-822C1829FF34}')

	def AddExperiment(self, SessionID=defaultNamedNotOptArg, ExperimentGroup=defaultNamedNotOptArg, ExperimentName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((3, 0), (8, 0), (8, 0)),SessionID
			, ExperimentGroup, ExperimentName)

	def DeleteAllExperiments(self, SessionID=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((3, 0),),SessionID
			)

	def DeleteExperiment(self, SessionID=defaultNamedNotOptArg, ExperimentGroup=defaultNamedNotOptArg, ExperimentName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((3, 0), (8, 0), (8, 0)),SessionID
			, ExperimentGroup, ExperimentName)

	def DeleteExperimentGroup(self, SessionID=defaultNamedNotOptArg, GroupName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(7, LCID, 1, (24, 0), ((3, 0), (8, 0)),SessionID
			, GroupName)

	def Refresh(self):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), (),)

	def SelectActiveExperiment(self, SessionID=defaultNamedNotOptArg, ExperimentGroup=defaultNamedNotOptArg, ExperimentName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(9, LCID, 1, (24, 0), ((3, 0), (8, 0), (8, 0)),SessionID
			, ExperimentGroup, ExperimentName)

	def SetRootNodeName(self, RootNodeName=defaultNamedNotOptArg, bRedraw=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(8, LCID, 1, (24, 0), ((8, 0), (3, 0)),RootNodeName
			, bRedraw)

	def SetTreeGroupState(self, ExperimentGroup=defaultNamedNotOptArg, TreeState=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(6, LCID, 1, (24, 0), ((8, 0), (3, 0)),ExperimentGroup
			, TreeState)

	def Show(self, bShow=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), ((3, 0),),bShow
			)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IExperimentTreeViewEvents(DispatchBaseClass):
	CLSID = IID('{845DFB21-303B-4751-951E-6F0DF8A013C1}')
	coclass_clsid = IID('{A219A39D-2A0F-4658-A810-8AA440B19852}')

	def OnReloadExperiment(self, SessionID=defaultNamedNotOptArg, RootText=defaultNamedNotOptArg, ExperimentGroup=defaultNamedNotOptArg, ExperimentName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((3, 0), (8, 0), (8, 0), (8, 0)),SessionID
			, RootText, ExperimentGroup, ExperimentName)

	def OnTreeNodeDblClick(self, SessionID=defaultNamedNotOptArg, RootText=defaultNamedNotOptArg, ExperimentGroup=defaultNamedNotOptArg, ExperimentName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((3, 0), (8, 0), (8, 0), (8, 0)),SessionID
			, RootText, ExperimentGroup, ExperimentName)

	def OnTreeNodeSelect(self, SessionID=defaultNamedNotOptArg, RootText=defaultNamedNotOptArg, ExperimentGroup=defaultNamedNotOptArg, ExperimentName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((3, 0), (8, 0), (8, 0), (8, 0)),SessionID
			, RootText, ExperimentGroup, ExperimentName)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IFileSettingsTreeView(DispatchBaseClass):
	CLSID = IID('{54ABD01E-3A09-4AB9-A594-A652A697F2B1}')
	coclass_clsid = IID('{1D8EDC8A-F049-4BA0-8BC5-955A82BFC601}')

	def AddSnapshot(self, FilePath=defaultNamedNotOptArg, GroupName=defaultNamedNotOptArg, SnapshotName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((8, 0), (8, 0), (8, 0)),FilePath
			, GroupName, SnapshotName)

	def DeleteAllSnapshots(self, FilePath=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((8, 0),),FilePath
			)

	def DeleteSnapshot(self, FilePath=defaultNamedNotOptArg, GroupName=defaultNamedNotOptArg, SnapshotName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((8, 0), (8, 0), (8, 0)),FilePath
			, GroupName, SnapshotName)

	def ReloadAllSnapshots(self, FilePath=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), ((8, 0),),FilePath
			)

	def SetRootNodeName(self, RootNodeName=defaultNamedNotOptArg, bRedraw=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(7, LCID, 1, (24, 0), ((8, 0), (3, 0)),RootNodeName
			, bRedraw)

	def SetTreeGroupState(self, GroupName=defaultNamedNotOptArg, TreeState=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(6, LCID, 1, (24, 0), ((8, 0), (3, 0)),GroupName
			, TreeState)

	def Show(self, bShow=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), ((3, 0),),bShow
			)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IFileSettingsTreeViewEvents(DispatchBaseClass):
	CLSID = IID('{EE1F0283-ED2F-4D38-B2E4-FCCEB1D460F0}')
	coclass_clsid = IID('{25A65E7E-E249-425F-90BD-4BD61C727883}')

	def OnActivateSnapshot(self, FilePath=defaultNamedNotOptArg, GroupName=defaultNamedNotOptArg, SnapshotName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((8, 0), (8, 0), (8, 0)),FilePath
			, GroupName, SnapshotName)

	def OnAddSnapshot(self, FilePath=defaultNamedNotOptArg, GroupName=defaultNamedNotOptArg, SnapshotName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((8, 0), (8, 0), (8, 0)),FilePath
			, GroupName, SnapshotName)

	def OnDeleteAllSnapshot(self, FilePath=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), ((8, 0),),FilePath
			)

	def OnDeleteSnapshot(self, FilePath=defaultNamedNotOptArg, GroupName=defaultNamedNotOptArg, SnapshotName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((8, 0), (8, 0), (8, 0)),FilePath
			, GroupName, SnapshotName)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IGraphCtl(DispatchBaseClass):
	CLSID = IID('{1BA3E124-9276-487A-9CAE-D9A2276E80F8}')
	coclass_clsid = IID('{7D7E6BCD-0089-4DB9-B4E4-8FE903732413}')

	def AddElement(self):
		'method AddElement'
		return self._oleobj_.InvokeTypes(16, LCID, 1, (24, 0), (),)

	def AutoRange(self):
		'method AutoRange'
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), (),)

	def ClearGraph(self):
		'method ClearGraph'
		return self._oleobj_.InvokeTypes(18, LCID, 1, (24, 0), (),)

	def CopyToClipboard(self):
		'method CopyToClipboard'
		return self._oleobj_.InvokeTypes(38, LCID, 1, (24, 0), (),)

	def DeleteElement(self, ElementID=defaultNamedNotOptArg):
		'method DeleteElement'
		return self._oleobj_.InvokeTypes(17, LCID, 1, (24, 0), ((2, 0),),ElementID
			)

	# The method ElementLight is actually a property, but must be used as a method to correctly pass the arguments
	def ElementLight(self, ElementID=defaultNamedNotOptArg):
		'property ElementLight'
		return self._oleobj_.InvokeTypes(28, LCID, 2, (3, 0), ((2, 0),),ElementID
			)

	# The method ElementLightingAmbient is actually a property, but must be used as a method to correctly pass the arguments
	def ElementLightingAmbient(self, ElementID=defaultNamedNotOptArg):
		'property ElementLightingAmbient'
		return self._oleobj_.InvokeTypes(29, LCID, 2, (2, 0), ((2, 0),),ElementID
			)

	# The method ElementLightingDiffuse is actually a property, but must be used as a method to correctly pass the arguments
	def ElementLightingDiffuse(self, ElementID=defaultNamedNotOptArg):
		'property ElementLightingDiffuse'
		return self._oleobj_.InvokeTypes(30, LCID, 2, (2, 0), ((2, 0),),ElementID
			)

	# The method ElementLightingSpecular is actually a property, but must be used as a method to correctly pass the arguments
	def ElementLightingSpecular(self, ElementID=defaultNamedNotOptArg):
		'property ElementLightingSpecular'
		return self._oleobj_.InvokeTypes(31, LCID, 2, (2, 0), ((2, 0),),ElementID
			)

	# The method ElementLineColor is actually a property, but must be used as a method to correctly pass the arguments
	def ElementLineColor(self, ElementID=defaultNamedNotOptArg):
		'property ElementLineColor'
		return self._oleobj_.InvokeTypes(19, LCID, 2, (19, 0), ((2, 0),),ElementID
			)

	# The method ElementLineWidth is actually a property, but must be used as a method to correctly pass the arguments
	def ElementLineWidth(self, ElementID=defaultNamedNotOptArg):
		'property ElementLineWidth'
		return self._oleobj_.InvokeTypes(21, LCID, 2, (4, 0), ((2, 0),),ElementID
			)

	# The method ElementMaterialAmbient is actually a property, but must be used as a method to correctly pass the arguments
	def ElementMaterialAmbient(self, ElementID=defaultNamedNotOptArg):
		'property ElementMaterialAmbient'
		return self._oleobj_.InvokeTypes(32, LCID, 2, (2, 0), ((2, 0),),ElementID
			)

	# The method ElementMaterialDiffuse is actually a property, but must be used as a method to correctly pass the arguments
	def ElementMaterialDiffuse(self, ElementID=defaultNamedNotOptArg):
		'property ElementMaterialDiffuse'
		return self._oleobj_.InvokeTypes(33, LCID, 2, (2, 0), ((2, 0),),ElementID
			)

	# The method ElementMaterialEmission is actually a property, but must be used as a method to correctly pass the arguments
	def ElementMaterialEmission(self, ElementID=defaultNamedNotOptArg):
		'property ElementMaterialEmission'
		return self._oleobj_.InvokeTypes(36, LCID, 2, (2, 0), ((2, 0),),ElementID
			)

	# The method ElementMaterialShinines is actually a property, but must be used as a method to correctly pass the arguments
	def ElementMaterialShinines(self, ElementID=defaultNamedNotOptArg):
		'property ElementMaterialShinines'
		return self._oleobj_.InvokeTypes(35, LCID, 2, (2, 0), ((2, 0),),ElementID
			)

	# The method ElementMaterialSpecular is actually a property, but must be used as a method to correctly pass the arguments
	def ElementMaterialSpecular(self, ElementID=defaultNamedNotOptArg):
		'property ElementMaterialSpecular'
		return self._oleobj_.InvokeTypes(34, LCID, 2, (2, 0), ((2, 0),),ElementID
			)

	# The method ElementPointColor is actually a property, but must be used as a method to correctly pass the arguments
	def ElementPointColor(self, ElementID=defaultNamedNotOptArg):
		'property ElementPointColor'
		return self._oleobj_.InvokeTypes(20, LCID, 2, (19, 0), ((2, 0),),ElementID
			)

	# The method ElementPointSize is actually a property, but must be used as a method to correctly pass the arguments
	def ElementPointSize(self, ElementID=defaultNamedNotOptArg):
		'property ElementPointSize'
		return self._oleobj_.InvokeTypes(22, LCID, 2, (4, 0), ((2, 0),),ElementID
			)

	# The method ElementShow is actually a property, but must be used as a method to correctly pass the arguments
	def ElementShow(self, ElementID=defaultNamedNotOptArg):
		'property ElementShow'
		return self._oleobj_.InvokeTypes(25, LCID, 2, (3, 0), ((2, 0),),ElementID
			)

	# The method ElementSurfaceFill is actually a property, but must be used as a method to correctly pass the arguments
	def ElementSurfaceFill(self, ElementID=defaultNamedNotOptArg):
		'property ElementSurfaceFill'
		return self._oleobj_.InvokeTypes(26, LCID, 2, (3, 0), ((2, 0),),ElementID
			)

	# The method ElementSurfaceFlat is actually a property, but must be used as a method to correctly pass the arguments
	def ElementSurfaceFlat(self, ElementID=defaultNamedNotOptArg):
		'property ElementSurfaceFlat'
		return self._oleobj_.InvokeTypes(27, LCID, 2, (3, 0), ((2, 0),),ElementID
			)

	# The method ElementType is actually a property, but must be used as a method to correctly pass the arguments
	def ElementType(self, ElementID=defaultNamedNotOptArg):
		'property ElementType'
		return self._oleobj_.InvokeTypes(23, LCID, 2, (2, 0), ((2, 0),),ElementID
			)

	# The method Lighting is actually a property, but must be used as a method to correctly pass the arguments
	def Lighting(self, ElementID=defaultNamedNotOptArg):
		'property Lighting'
		return self._oleobj_.InvokeTypes(39, LCID, 2, (3, 0), ((2, 0),),ElementID
			)

	def OnPrint(self, pDC=defaultNamedNotOptArg, pPrintInfo=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(41, LCID, 1, (24, 0), ((12, 0), (12, 0)),pDC
			, pPrintInfo)

	def PlotXYZ(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg, ElementID=defaultNamedNotOptArg):
		'method PlotXYZ'
		return self._oleobj_.InvokeTypes(24, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (2, 0)),x
			, y, z, ElementID)

	def Print(self, Title=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(42, LCID, 1, (24, 0), ((8, 0),),Title
			)

	# The method SetElementLight is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementLight(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementLight'
		return self._oleobj_.InvokeTypes(28, LCID, 4, (24, 0), ((2, 0), (3, 1)),ElementID
			, arg1)

	# The method SetElementLightingAmbient is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementLightingAmbient(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementLightingAmbient'
		return self._oleobj_.InvokeTypes(29, LCID, 4, (24, 0), ((2, 0), (2, 1)),ElementID
			, arg1)

	# The method SetElementLightingDiffuse is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementLightingDiffuse(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementLightingDiffuse'
		return self._oleobj_.InvokeTypes(30, LCID, 4, (24, 0), ((2, 0), (2, 1)),ElementID
			, arg1)

	# The method SetElementLightingSpecular is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementLightingSpecular(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementLightingSpecular'
		return self._oleobj_.InvokeTypes(31, LCID, 4, (24, 0), ((2, 0), (2, 1)),ElementID
			, arg1)

	# The method SetElementLineColor is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementLineColor(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementLineColor'
		return self._oleobj_.InvokeTypes(19, LCID, 4, (24, 0), ((2, 0), (19, 1)),ElementID
			, arg1)

	# The method SetElementLineWidth is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementLineWidth(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementLineWidth'
		return self._oleobj_.InvokeTypes(21, LCID, 4, (24, 0), ((2, 0), (4, 1)),ElementID
			, arg1)

	# The method SetElementMaterialAmbient is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementMaterialAmbient(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementMaterialAmbient'
		return self._oleobj_.InvokeTypes(32, LCID, 4, (24, 0), ((2, 0), (2, 1)),ElementID
			, arg1)

	# The method SetElementMaterialDiffuse is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementMaterialDiffuse(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementMaterialDiffuse'
		return self._oleobj_.InvokeTypes(33, LCID, 4, (24, 0), ((2, 0), (2, 1)),ElementID
			, arg1)

	# The method SetElementMaterialEmission is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementMaterialEmission(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementMaterialEmission'
		return self._oleobj_.InvokeTypes(36, LCID, 4, (24, 0), ((2, 0), (2, 1)),ElementID
			, arg1)

	# The method SetElementMaterialShinines is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementMaterialShinines(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementMaterialShinines'
		return self._oleobj_.InvokeTypes(35, LCID, 4, (24, 0), ((2, 0), (2, 1)),ElementID
			, arg1)

	# The method SetElementMaterialSpecular is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementMaterialSpecular(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementMaterialSpecular'
		return self._oleobj_.InvokeTypes(34, LCID, 4, (24, 0), ((2, 0), (2, 1)),ElementID
			, arg1)

	# The method SetElementPointColor is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementPointColor(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementPointColor'
		return self._oleobj_.InvokeTypes(20, LCID, 4, (24, 0), ((2, 0), (19, 1)),ElementID
			, arg1)

	# The method SetElementPointSize is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementPointSize(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementPointSize'
		return self._oleobj_.InvokeTypes(22, LCID, 4, (24, 0), ((2, 0), (4, 1)),ElementID
			, arg1)

	# The method SetElementShow is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementShow(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementShow'
		return self._oleobj_.InvokeTypes(25, LCID, 4, (24, 0), ((2, 0), (3, 1)),ElementID
			, arg1)

	# The method SetElementSurfaceFill is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementSurfaceFill(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementSurfaceFill'
		return self._oleobj_.InvokeTypes(26, LCID, 4, (24, 0), ((2, 0), (3, 1)),ElementID
			, arg1)

	# The method SetElementSurfaceFlat is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementSurfaceFlat(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementSurfaceFlat'
		return self._oleobj_.InvokeTypes(27, LCID, 4, (24, 0), ((2, 0), (3, 1)),ElementID
			, arg1)

	# The method SetElementType is actually a property, but must be used as a method to correctly pass the arguments
	def SetElementType(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property ElementType'
		return self._oleobj_.InvokeTypes(23, LCID, 4, (24, 0), ((2, 0), (2, 1)),ElementID
			, arg1)

	def SetLightCoordinates(self, ElementID=defaultNamedNotOptArg, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		'method SetLightCoordinates'
		return self._oleobj_.InvokeTypes(37, LCID, 1, (24, 0), ((2, 0), (4, 0), (4, 0), (4, 0)),ElementID
			, x, y, z)

	# The method SetLighting is actually a property, but must be used as a method to correctly pass the arguments
	def SetLighting(self, ElementID=defaultNamedNotOptArg, arg1=defaultUnnamedArg):
		'property Lighting'
		return self._oleobj_.InvokeTypes(39, LCID, 4, (24, 0), ((2, 0), (3, 1)),ElementID
			, arg1)

	def SetRange(self, xmin=defaultNamedNotOptArg, xmax=defaultNamedNotOptArg, ymin=defaultNamedNotOptArg, ymax=defaultNamedNotOptArg
			, zmin=defaultNamedNotOptArg, zmax=defaultNamedNotOptArg):
		'method SetRange'
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (5, 0), (5, 0), (5, 0)),xmin
			, xmax, ymin, ymax, zmin, zmax
			)

	def ShowPropertyPages(self):
		'method ShowPropertyPages'
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), (),)

	_prop_map_get_ = {
		"CaptionColor": (1, 2, (19, 0), (), "CaptionColor", None),
		"GraphCtrlObj": (40, 2, (9, 0), (), "GraphCtrlObj", None),
		"Projection": (6, 2, (2, 0), (), "Projection", None),
		"TrackMode": (5, 2, (2, 0), (), "TrackMode", None),
		"XGridColor": (13, 2, (19, 0), (), "XGridColor", None),
		"XGridNumber": (10, 2, (2, 0), (), "XGridNumber", None),
		"XLabel": (7, 2, (8, 0), (), "XLabel", None),
		"YGridColor": (14, 2, (19, 0), (), "YGridColor", None),
		"YGridNumber": (11, 2, (2, 0), (), "YGridNumber", None),
		"YLabel": (8, 2, (8, 0), (), "YLabel", None),
		"ZGridColor": (15, 2, (19, 0), (), "ZGridColor", None),
		"ZGridNumber": (12, 2, (2, 0), (), "ZGridNumber", None),
		"ZLabel": (9, 2, (8, 0), (), "ZLabel", None),
	}
	_prop_map_put_ = {
		"CaptionColor": ((1, LCID, 4, 0),()),
		"Projection": ((6, LCID, 4, 0),()),
		"TrackMode": ((5, LCID, 4, 0),()),
		"XGridColor": ((13, LCID, 4, 0),()),
		"XGridNumber": ((10, LCID, 4, 0),()),
		"XLabel": ((7, LCID, 4, 0),()),
		"YGridColor": ((14, LCID, 4, 0),()),
		"YGridNumber": ((11, LCID, 4, 0),()),
		"YLabel": ((8, LCID, 4, 0),()),
		"ZGridColor": ((15, LCID, 4, 0),()),
		"ZGridNumber": ((12, LCID, 4, 0),()),
		"ZLabel": ((9, LCID, 4, 0),()),
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IGraphCtrl2d(DispatchBaseClass):
	CLSID = IID('{0FFFBABB-9F1A-43E8-A2FF-6CF97D6D272A}')
	coclass_clsid = IID('{40C1D140-3B42-46EF-848D-B1BA009692EF}')

	def AddChartPoint(self, ChartID=defaultNamedNotOptArg, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, Value=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(27, LCID, 1, (24, 0), ((3, 0), (5, 0), (5, 0), (5, 0)),ChartID
			, x, y, Value)

	def ClearChart(self, ChartID=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(18, LCID, 1, (24, 0), ((3, 0),),ChartID
			)

	def CreateBarChart(self, pChartID=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(21, LCID, 1, (24, 0), ((16387, 0),),pChartID
			)

	def CreateCandlestickChart(self, pChartID=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(22, LCID, 1, (24, 0), ((16387, 0),),pChartID
			)

	def CreateGanttChart(self, pChartID=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(23, LCID, 1, (24, 0), ((16387, 0),),pChartID
			)

	def CreateLineChart(self, pChartID=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(17, LCID, 1, (24, 0), ((16387, 0),),pChartID
			)

	def CreatePointsChart(self, pChartID=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(20, LCID, 1, (24, 0), ((16387, 0),),pChartID
			)

	def CreateStandardAxis(self, AxisType=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(15, LCID, 1, (24, 0), ((3, 0),),AxisType
			)

	def CreateSurfaceChart(self, pChartID=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(19, LCID, 1, (24, 0), ((16387, 0),),pChartID
			)

	def DeleteAllCharts(self):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), (),)

	def OnPrint(self, pDC=defaultNamedNotOptArg, pPrintInfo=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(29, LCID, 1, (24, 0), ((12, 0), (12, 0)),pDC
			, pPrintInfo)

	def Print(self, Title=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(30, LCID, 1, (24, 0), ((8, 0),),Title
			)

	def Refresh(self):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), (),)

	def SaveAsImage(self, FilePath=defaultNamedNotOptArg, nBPP=defaultNamedNotOptArg, Width=defaultNamedNotOptArg, height=defaultNamedNotOptArg
			, FileType=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(10, LCID, 1, (24, 0), ((8, 0), (3, 0), (3, 0), (3, 0), (3, 0)),FilePath
			, nBPP, Width, height, FileType)

	def Set2dAxisRange(self, AxisType=defaultNamedNotOptArg, MinValue=defaultNamedNotOptArg, MaxValue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((3, 0), (5, 0), (5, 0)),AxisType
			, MinValue, MaxValue)

	def Set2dChartData(self, GraphData=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((12, 0),),GraphData
			)

	def Set2dGraphInfo(self, GraphTitle=defaultNamedNotOptArg, XAxisGraphTitle=defaultNamedNotOptArg, YAxisGraphTitle=defaultNamedNotOptArg, bShow=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((8, 0), (8, 0), (8, 0), (3, 0)),GraphTitle
			, XAxisGraphTitle, YAxisGraphTitle, bShow)

	def SetAxisTitle(self, AxisType=defaultNamedNotOptArg, Title=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(24, LCID, 1, (24, 0), ((3, 0), (8, 0)),AxisType
			, Title)

	def SetChartData(self, ChartID=defaultNamedNotOptArg, ChartData=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(26, LCID, 1, (24, 0), ((3, 0), (12, 0)),ChartID
			, ChartData)

	def UndoPanZoom(self):
		return self._oleobj_.InvokeTypes(13, LCID, 1, (24, 0), (),)

	_prop_map_get_ = {
		"BkgColor": (6, 2, (19, 0), (), "BkgColor", None),
		"BorderColor": (7, 2, (19, 0), (), "BorderColor", None),
		"ChartCount": (14, 2, (3, 0), (), "ChartCount", None),
		"EnablePan": (11, 2, (3, 0), (), "EnablePan", None),
		"EnableReferesh": (16, 2, (3, 0), (), "EnableReferesh", None),
		"EnableZoom": (12, 2, (3, 0), (), "EnableZoom", None),
		"GraphCtrlObj": (9, 2, (9, 0), (), "GraphCtrlObj", None),
		"ShowMainTitle": (25, 2, (3, 0), (), "ShowMainTitle", None),
		"Title": (8, 2, (8, 0), (), "Title", None),
		"TitleVisible": (28, 2, (3, 0), (), "TitleVisible", None),
	}
	_prop_map_put_ = {
		"BkgColor": ((6, LCID, 4, 0),()),
		"BorderColor": ((7, LCID, 4, 0),()),
		"EnablePan": ((11, LCID, 4, 0),()),
		"EnableReferesh": ((16, LCID, 4, 0),()),
		"EnableZoom": ((12, LCID, 4, 0),()),
		"ShowMainTitle": ((25, LCID, 4, 0),()),
		"Title": ((8, LCID, 4, 0),()),
		"TitleVisible": ((28, LCID, 4, 0),()),
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IMainApplication(DispatchBaseClass):
	CLSID = IID('{9E6C4518-AF98-4654-B294-709D0440698B}')
	coclass_clsid = IID('{36F4F7BF-6A25-47A4-A5DB-5C84B82811A1}')

	def SetAddInInfo(self, lSessionID=defaultNamedNotOptArg, lInstanceHandle=defaultNamedNotOptArg, strXMLMenuToolbarIDCmdInfo=defaultNamedNotOptArg, lToobarRes=defaultNamedNotOptArg
			, Reserved=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((3, 0), (20, 0), (8, 0), (20, 0), (8, 0)),lSessionID
			, lInstanceHandle, strXMLMenuToolbarIDCmdInfo, lToobarRes, Reserved)

	def SetGraphInfo(self, SessionID=defaultNamedNotOptArg, NoOfCharts=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((3, 0), (3, 0)),SessionID
			, NoOfCharts)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IMainApplicationEvents(DispatchBaseClass):
	CLSID = IID('{42C2B21E-0555-439C-8905-222DD02234FB}')
	coclass_clsid = IID('{D729E31D-CC69-48BF-AC5A-A5860DFD667B}')

	def OnApplicationClose(self):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), (),)

	def OnApplicationLaunched(self):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), (),)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IMainWindow(DispatchBaseClass):
	CLSID = IID('{76AF030A-35A8-405B-B649-ACE818A007BC}')
	coclass_clsid = IID('{005FD9DA-A89C-4D58-9F17-F1CAA02484F9}')

	def AddErrorStatus(self, Status=defaultNamedNotOptArg, bPostMessage=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(7, LCID, 1, (24, 0), ((8, 0), (3, 0)),Status
			, bPostMessage)

	def AddOperationStatus(self, Status=defaultNamedNotOptArg, bPostMessage=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), ((8, 0), (3, 0)),Status
			, bPostMessage)

	def AddResultStatus(self, Status=defaultNamedNotOptArg, bPostMessage=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(9, LCID, 1, (24, 0), ((8, 0), (3, 0)),Status
			, bPostMessage)

	# The method ChildWindow is actually a property, but must be used as a method to correctly pass the arguments
	def ChildWindow(self, WindowType=defaultNamedNotOptArg):
		return self._ApplyTypes_(2, 2, (12, 0), ((3, 0),), 'ChildWindow', None,WindowType
			)

	def ResetAllOutputStatusWindows(self):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), (),)

	def ResetAllStatusWindows(self):
		return self._oleobj_.InvokeTypes(10, LCID, 1, (24, 0), (),)

	def ResetErrorStatusWindow(self):
		return self._oleobj_.InvokeTypes(6, LCID, 1, (24, 0), (),)

	def ResetOperationStatusWindow(self):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), (),)

	def ResetResultStatusWindow(self):
		return self._oleobj_.InvokeTypes(8, LCID, 1, (24, 0), (),)

	def Set2ndStatusbarMessage(self, Msg=defaultNamedNotOptArg, bPostMessage=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(12, LCID, 1, (24, 0), ((8, 0), (3, 0)),Msg
			, bPostMessage)

	def SetStatusbarMessage(self, StatusMessage=defaultNamedNotOptArg, bPostMessage=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(11, LCID, 1, (24, 0), ((8, 0), (3, 0)),StatusMessage
			, bPostMessage)

	_prop_map_get_ = {
		"Window": (1, 2, (12, 0), (), "Window", None),
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IMainWindowEvents(DispatchBaseClass):
	CLSID = IID('{99F323B8-BD88-4DCE-AA10-4BA0061BAF24}')
	coclass_clsid = IID('{FF8F1BC0-4715-4EB8-ADC7-F40154565E33}')

	def MianWndProc(self, MsgID=defaultNamedNotOptArg, wParam=defaultNamedNotOptArg, lParam=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((3, 0), (12, 0), (12, 0)),MsgID
			, wParam, lParam)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IOpenGLUtilView(DispatchBaseClass):
	CLSID = IID('{C648D646-603D-4F95-BB6D-A8B6FBD0471A}')
	coclass_clsid = IID('{D37FFE74-89CB-4272-A00B-78AAE721793C}')

	def gluBeginCurve(self, nobj=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(38, LCID, 1, (24, 0), ((12, 0),),nobj
			)

	def gluBeginPolygon(self, tess=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(50, LCID, 1, (24, 0), ((12, 0),),tess
			)

	def gluBeginSurface(self, nobj=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(37, LCID, 1, (24, 0), ((12, 0),),nobj
			)

	def gluBeginTrim(self, nobj=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(41, LCID, 1, (24, 0), ((12, 0),),nobj
			)

	def gluBuild1DMipmaps(self, target=defaultNamedNotOptArg, components=defaultNamedNotOptArg, Width=defaultNamedNotOptArg, format=defaultNamedNotOptArg
			, type=defaultNamedNotOptArg, data=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(11, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0), (3, 0), (3, 0), (12, 0)),target
			, components, Width, format, type, data
			)

	def gluBuild2DMipmaps(self, target=defaultNamedNotOptArg, components=defaultNamedNotOptArg, Width=defaultNamedNotOptArg, height=defaultNamedNotOptArg
			, format=defaultNamedNotOptArg, type=defaultNamedNotOptArg, data=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(12, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0), (3, 0), (3, 0), (3, 0), (12, 0)),target
			, components, Width, height, format, type
			, data)

	def gluCylinder(self, qobj=defaultNamedNotOptArg, baseRadius=defaultNamedNotOptArg, topRadius=defaultNamedNotOptArg, height=defaultNamedNotOptArg
			, slices=defaultNamedNotOptArg, stacks=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(19, LCID, 1, (24, 0), ((12, 0), (5, 0), (5, 0), (5, 0), (3, 0), (3, 0)),qobj
			, baseRadius, topRadius, height, slices, stacks
			)

	def gluDeleteNurbsRenderer(self, nobj=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(36, LCID, 1, (24, 0), ((12, 0),),nobj
			)

	def gluDeleteQuadric(self, state=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(14, LCID, 1, (24, 0), ((12, 0),),state
			)

	def gluDeleteTess(self, tess=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(25, LCID, 1, (24, 0), ((12, 0),),tess
			)

	def gluDisk(self, qobj=defaultNamedNotOptArg, innerRadius=defaultNamedNotOptArg, outerRadius=defaultNamedNotOptArg, slices=defaultNamedNotOptArg
			, loops=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(20, LCID, 1, (24, 0), ((12, 0), (5, 0), (5, 0), (3, 0), (3, 0)),qobj
			, innerRadius, outerRadius, slices, loops)

	def gluEndCurve(self, nobj=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(39, LCID, 1, (24, 0), ((12, 0),),nobj
			)

	def gluEndPolygon(self, tess=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(52, LCID, 1, (24, 0), ((12, 0),),tess
			)

	def gluEndSurface(self, nobj=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(40, LCID, 1, (24, 0), ((12, 0),),nobj
			)

	def gluEndTrim(self, nobj=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(42, LCID, 1, (24, 0), ((12, 0),),nobj
			)

	def gluErrorString(self, errCode=defaultNamedNotOptArg, errString=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((19, 0), (16401, 0)),errCode
			, errString)

	def gluErrorUnicodeStringEXT(self, errCode=defaultNamedNotOptArg, errString=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((19, 0), (16392, 0)),errCode
			, errString)

	def gluGetNurbsProperty(self, nobj=defaultNamedNotOptArg, lproperty=defaultNamedNotOptArg, pValue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(48, LCID, 1, (24, 0), ((12, 0), (19, 0), (16388, 0)),nobj
			, lproperty, pValue)

	def gluGetString(self, name=defaultNamedNotOptArg, strString=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((19, 0), (16401, 0)),name
			, strString)

	def gluGetTessProperty(self, tess=defaultNamedNotOptArg, which=defaultNamedNotOptArg, Value=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(34, LCID, 1, (24, 0), ((12, 0), (19, 0), (16389, 0)),tess
			, which, Value)

	def gluLoadSamplingMatrices(self, nobj=defaultNamedNotOptArg, modelMatrix=defaultNamedNotOptArg, projMatrix=defaultNamedNotOptArg, viewport=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(46, LCID, 1, (24, 0), ((12, 0), (16388, 0), (16388, 0), (16387, 0)),nobj
			, modelMatrix, projMatrix, viewport)

	def gluLookAt(self, eyex=defaultNamedNotOptArg, eyey=defaultNamedNotOptArg, eyez=defaultNamedNotOptArg, centerx=defaultNamedNotOptArg
			, centery=defaultNamedNotOptArg, centerz=defaultNamedNotOptArg, upx=defaultNamedNotOptArg, upy=defaultNamedNotOptArg, upz=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(7, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (5, 0), (5, 0), (5, 0), (5, 0), (5, 0), (5, 0)),eyex
			, eyey, eyez, centerx, centery, centerz
			, upx, upy, upz)

	def gluNewNurbsRenderer(self, ewNurbsRenderer=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(35, LCID, 1, (24, 0), ((16396, 0),),ewNurbsRenderer
			)

	def gluNewQuadric(self, pNewQuadricObj=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(13, LCID, 1, (24, 0), ((16396, 0),),pNewQuadricObj
			)

	def gluNewTess(self, pNewTess=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(24, LCID, 1, (24, 0), ((16396, 0),),pNewTess
			)

	def gluNextContour(self, tess=defaultNamedNotOptArg, lType=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(51, LCID, 1, (24, 0), ((12, 0), (19, 0)),tess
			, lType)

	def gluNurbsCallback(self, nobj=defaultNamedNotOptArg, which=defaultNamedNotOptArg, pCallback=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(49, LCID, 1, (24, 0), ((12, 0), (19, 0), (12, 0)),nobj
			, which, pCallback)

	def gluNurbsCurve(self, nobj=defaultNamedNotOptArg, nknots=defaultNamedNotOptArg, knot=defaultNamedNotOptArg, lstride=defaultNamedNotOptArg
			, ctlarray=defaultNamedNotOptArg, lOrder=defaultNamedNotOptArg, lType=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(44, LCID, 1, (24, 0), ((12, 0), (3, 0), (16388, 0), (3, 0), (16388, 0), (3, 0), (19, 0)),nobj
			, nknots, knot, lstride, ctlarray, lOrder
			, lType)

	def gluNurbsProperty(self, nobj=defaultNamedNotOptArg, lproperty=defaultNamedNotOptArg, lValue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(47, LCID, 1, (24, 0), ((12, 0), (19, 0), (4, 0)),nobj
			, lproperty, lValue)

	def gluNurbsSurface(self, nobj=defaultNamedNotOptArg, sknot_count=defaultNamedNotOptArg, sknot=defaultNamedNotOptArg, tknot_count=defaultNamedNotOptArg
			, tknot=defaultNamedNotOptArg, s_stride=defaultNamedNotOptArg, t_stride=defaultNamedNotOptArg, ctlarray=defaultNamedNotOptArg, sorder=defaultNamedNotOptArg
			, torder=defaultNamedNotOptArg, lType=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(45, LCID, 1, (24, 0), ((12, 0), (3, 0), (16388, 0), (3, 0), (16388, 0), (3, 0), (3, 0), (16388, 0), (3, 0), (3, 0), (3, 0)),nobj
			, sknot_count, sknot, tknot_count, tknot, s_stride
			, t_stride, ctlarray, sorder, torder, lType
			)

	def gluOrtho2D(self, Left=defaultNamedNotOptArg, Right=defaultNamedNotOptArg, Bottom=defaultNamedNotOptArg, Top=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (5, 0)),Left
			, Right, Bottom, Top)

	def gluPartialDisk(self, qobj=defaultNamedNotOptArg, innerRadius=defaultNamedNotOptArg, outerRadius=defaultNamedNotOptArg, slices=defaultNamedNotOptArg
			, loops=defaultNamedNotOptArg, startAngle=defaultNamedNotOptArg, sweepAngle=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(21, LCID, 1, (24, 0), ((12, 0), (5, 0), (5, 0), (3, 0), (3, 0), (5, 0), (5, 0)),qobj
			, innerRadius, outerRadius, slices, loops, startAngle
			, sweepAngle)

	def gluPerspective(self, fovy=defaultNamedNotOptArg, aspect=defaultNamedNotOptArg, zNear=defaultNamedNotOptArg, zFar=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (5, 0)),fovy
			, aspect, zNear, zFar)

	def gluPickMatrix(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, Width=defaultNamedNotOptArg, height=defaultNamedNotOptArg
			, viewport=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(6, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (5, 0), (16387, 0)),x
			, y, Width, height, viewport)

	def gluProject(self, objx=defaultNamedNotOptArg, objy=defaultNamedNotOptArg, objz=defaultNamedNotOptArg, modelMatrix=defaultNamedNotOptArg
			, projMatrix=defaultNamedNotOptArg, viewport=defaultNamedNotOptArg, winx=defaultNamedNotOptArg, winy=defaultNamedNotOptArg, winz=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(8, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (16389, 0), (16389, 0), (16387, 0), (16389, 0), (16389, 0), (16389, 0)),objx
			, objy, objz, modelMatrix, projMatrix, viewport
			, winx, winy, winz)

	def gluPwlCurve(self, nobj=defaultNamedNotOptArg, count=defaultNamedNotOptArg, pArray=defaultNamedNotOptArg, stride=defaultNamedNotOptArg
			, lType=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(43, LCID, 1, (24, 0), ((12, 0), (3, 0), (16388, 0), (3, 0), (19, 0)),nobj
			, count, pArray, stride, lType)

	def gluQuadricCallback(self, qobj=defaultNamedNotOptArg, which=defaultNamedNotOptArg, fnCallback=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(23, LCID, 1, (24, 0), ((12, 0), (3, 0), (12, 0)),qobj
			, which, fnCallback)

	def gluQuadricDrawStyle(self, quadObject=defaultNamedNotOptArg, drawStyle=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(18, LCID, 1, (24, 0), ((12, 0), (19, 0)),quadObject
			, drawStyle)

	def gluQuadricNormals(self, quadObject=defaultNamedNotOptArg, normals=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(15, LCID, 1, (24, 0), ((12, 0), (19, 0)),quadObject
			, normals)

	def gluQuadricOrientation(self, quadObject=defaultNamedNotOptArg, orientation=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(17, LCID, 1, (24, 0), ((12, 0), (19, 0)),quadObject
			, orientation)

	def gluQuadricTexture(self, quadObject=defaultNamedNotOptArg, textureCoords=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(16, LCID, 1, (24, 0), ((12, 0), (17, 0)),quadObject
			, textureCoords)

	def gluScaleImage(self, format=defaultNamedNotOptArg, widthin=defaultNamedNotOptArg, heightin=defaultNamedNotOptArg, typein=defaultNamedNotOptArg
			, datain=defaultNamedNotOptArg, widthout=defaultNamedNotOptArg, heightout=defaultNamedNotOptArg, typeout=defaultNamedNotOptArg, dataout=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(10, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0), (3, 0), (12, 0), (3, 0), (3, 0), (3, 0), (16396, 0)),format
			, widthin, heightin, typein, datain, widthout
			, heightout, typeout, dataout)

	def gluSphere(self, qobj=defaultNamedNotOptArg, Radius=defaultNamedNotOptArg, slices=defaultNamedNotOptArg, stacks=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(22, LCID, 1, (24, 0), ((12, 0), (5, 0), (3, 0), (3, 0)),qobj
			, Radius, slices, stacks)

	def gluTessBeginContour(self, tess=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(27, LCID, 1, (24, 0), ((12, 0),),tess
			)

	def gluTessBeginPolygon(self, tess=defaultNamedNotOptArg, polygon_data=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(26, LCID, 1, (24, 0), ((12, 0), (12, 0)),tess
			, polygon_data)

	def gluTessCallback(self, tess=defaultNamedNotOptArg, which=defaultNamedNotOptArg, Callback=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(33, LCID, 1, (24, 0), ((12, 0), (19, 0), (12, 0)),tess
			, which, Callback)

	def gluTessEndContour(self, tess=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(29, LCID, 1, (24, 0), ((12, 0),),tess
			)

	def gluTessEndPolygon(self, tess=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(30, LCID, 1, (24, 0), ((12, 0),),tess
			)

	def gluTessNormal(self, tess=defaultNamedNotOptArg, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(32, LCID, 1, (24, 0), ((12, 0), (5, 0), (5, 0), (5, 0)),tess
			, x, y, z)

	def gluTessProperty(self, tess=defaultNamedNotOptArg, which=defaultNamedNotOptArg, Value=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(31, LCID, 1, (24, 0), ((12, 0), (19, 0), (5, 0)),tess
			, which, Value)

	def gluTessVertex(self, tess=defaultNamedNotOptArg, coords=defaultNamedNotOptArg, data=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(28, LCID, 1, (24, 0), ((12, 0), (16389, 0), (12, 0)),tess
			, coords, data)

	def gluUnProject(self, winx=defaultNamedNotOptArg, winy=defaultNamedNotOptArg, winz=defaultNamedNotOptArg, modelMatrix=defaultNamedNotOptArg
			, projMatrix=defaultNamedNotOptArg, viewport=defaultNamedNotOptArg, objx=defaultNamedNotOptArg, objy=defaultNamedNotOptArg, objz=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(9, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (16389, 0), (16389, 0), (16387, 0), (16389, 0), (16389, 0), (16389, 0)),winx
			, winy, winz, modelMatrix, projMatrix, viewport
			, objx, objy, objz)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IOpenGLUtilViewEvents(DispatchBaseClass):
	CLSID = IID('{0A6B74CB-F443-4416-A121-A16031745EBF}')
	coclass_clsid = IID('{E6DC451D-1949-4E5E-A9DD-B1B1BBD4E298}')

	def GLUnurbsErrorProc(self, Param1=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(14, LCID, 1, (24, 0), ((19, 0),),Param1
			)

	def GLUquadricErrorProc(self, errorNo=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((19, 0),),errorNo
			)

	def GLUtessBeginDataProc(self, Param1=defaultNamedNotOptArg, Param2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(8, LCID, 1, (24, 0), ((19, 0), (16396, 0)),Param1
			, Param2)

	def GLUtessBeginProc(self, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((19, 0),),param
			)

	def GLUtessCombineDataProc(self, Param1=defaultNamedNotOptArg, Param2=defaultNamedNotOptArg, Param3=defaultNamedNotOptArg, Param4=defaultNamedNotOptArg
			, Param5=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(13, LCID, 1, (24, 0), ((16389, 0), (16396, 0), (16388, 0), (16396, 0), (12, 0)),Param1
			, Param2, Param3, Param4, Param5)

	def GLUtessCombineProc(self, Param1=defaultNamedNotOptArg, Param2=defaultNamedNotOptArg, Param3=defaultNamedNotOptArg, Param4=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(7, LCID, 1, (24, 0), ((16389, 0), (16396, 0), (16388, 0), (16396, 0)),Param1
			, Param2, Param3, Param4)

	def GLUtessEdgeFlagDataProc(self, Param1=defaultNamedNotOptArg, Param2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(9, LCID, 1, (24, 0), ((17, 0), (16396, 0)),Param1
			, Param2)

	def GLUtessEdgeFlagProc(self, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((17, 0),),param
			)

	def GLUtessEndDataProc(self, Param1=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(11, LCID, 1, (24, 0), ((16396, 0),),Param1
			)

	def GLUtessEndProc(self, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), ((12, 0),),param
			)

	def GLUtessErrorDataProc(self, Param1=defaultNamedNotOptArg, Param2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(12, LCID, 1, (24, 0), ((19, 0), (16396, 0)),Param1
			, Param2)

	def GLUtessErrorProc(self, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(6, LCID, 1, (24, 0), ((19, 0),),param
			)

	def GLUtessVertexDataProc(self, Param1=defaultNamedNotOptArg, Param2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(10, LCID, 1, (24, 0), ((16396, 0), (16396, 0)),Param1
			, Param2)

	def GLUtessVertexProc(self, pParam=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), ((16396, 0),),pParam
			)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IOpenGLView(DispatchBaseClass):
	CLSID = IID('{ADB4E5E0-CA47-44BF-9744-3178ADE86F6D}')
	coclass_clsid = IID('{0234EA06-BACD-4930-8F94-11034FE5FCC1}')

	def glAccum(self, op=defaultNamedNotOptArg, Value=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((19, 0), (4, 0)),op
			, Value)

	def glAlphaFunc(self, func=defaultNamedNotOptArg, ref=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((19, 0), (4, 0)),func
			, ref)

	def glAreTexturesResident(self, n=defaultNamedNotOptArg, textures=defaultNamedNotOptArg, residences=defaultNamedNotOptArg, bResult=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((3, 0), (16403, 0), (16401, 0), (16401, 0)),n
			, textures, residences, bResult)

	def glArrayElement(self, i=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), ((3, 0),),i
			)

	def glBegin(self, Mode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), ((19, 0),),Mode
			)

	def glBindTexture(self, target=defaultNamedNotOptArg, texture=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(6, LCID, 1, (24, 0), ((19, 0), (19, 0)),target
			, texture)

	def glBitmap(self, Width=defaultNamedNotOptArg, height=defaultNamedNotOptArg, xorig=defaultNamedNotOptArg, yorig=defaultNamedNotOptArg
			, xmove=defaultNamedNotOptArg, ymove=defaultNamedNotOptArg, bitmap=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(7, LCID, 1, (24, 0), ((3, 0), (3, 0), (4, 0), (4, 0), (4, 0), (4, 0), (16401, 0)),Width
			, height, xorig, yorig, xmove, ymove
			, bitmap)

	def glBlendFunc(self, sfactor=defaultNamedNotOptArg, dfactor=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(8, LCID, 1, (24, 0), ((19, 0), (19, 0)),sfactor
			, dfactor)

	def glCallList(self, list=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(9, LCID, 1, (24, 0), ((19, 0),),list
			)

	def glCallLists(self, n=defaultNamedNotOptArg, type=defaultNamedNotOptArg, lists=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(10, LCID, 1, (24, 0), ((3, 0), (19, 0), (12, 0)),n
			, type, lists)

	def glClear(self, Mask=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(11, LCID, 1, (24, 0), ((19, 0),),Mask
			)

	def glClearAccum(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg, Alpha=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(12, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0), (4, 0)),red
			, green, blue, Alpha)

	def glClearColor(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg, Alpha=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(13, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0), (4, 0)),red
			, green, blue, Alpha)

	def glClearDepth(self, Depth=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(14, LCID, 1, (24, 0), ((5, 0),),Depth
			)

	def glClearIndex(self, c=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(15, LCID, 1, (24, 0), ((4, 0),),c
			)

	def glClearStencil(self, s=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(16, LCID, 1, (24, 0), ((3, 0),),s
			)

	def glClipPlane(self, plane=defaultNamedNotOptArg, equation=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(17, LCID, 1, (24, 0), ((19, 0), (16389, 0)),plane
			, equation)

	def glColor3b(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(18, LCID, 1, (24, 0), ((16, 0), (16, 0), (16, 0)),red
			, green, blue)

	def glColor3bv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(19, LCID, 1, (24, 0), ((16400, 0),),v
			)

	def glColor3d(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(20, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0)),red
			, green, blue)

	def glColor3dv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(21, LCID, 1, (24, 0), ((16389, 0),),v
			)

	def glColor3f(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(22, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0)),red
			, green, blue)

	def glColor3fv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(23, LCID, 1, (24, 0), ((16388, 0),),v
			)

	def glColor3i(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(24, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0)),red
			, green, blue)

	def glColor3iv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(25, LCID, 1, (24, 0), ((16387, 0),),v
			)

	def glColor3s(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(26, LCID, 1, (24, 0), ((2, 0), (2, 0), (2, 0)),red
			, green, blue)

	def glColor3sv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(27, LCID, 1, (24, 0), ((16386, 0),),v
			)

	def glColor3ub(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(28, LCID, 1, (24, 0), ((17, 0), (17, 0), (17, 0)),red
			, green, blue)

	def glColor3ubv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(29, LCID, 1, (24, 0), ((16401, 0),),v
			)

	def glColor3ui(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(30, LCID, 1, (24, 0), ((19, 0), (19, 0), (19, 0)),red
			, green, blue)

	def glColor3uiv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(31, LCID, 1, (24, 0), ((16403, 0),),v
			)

	def glColor3us(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(32, LCID, 1, (24, 0), ((18, 0), (18, 0), (18, 0)),red
			, green, blue)

	def glColor3usv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(33, LCID, 1, (24, 0), ((16402, 0),),v
			)

	def glColor4b(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg, Alpha=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(34, LCID, 1, (24, 0), ((16, 0), (16, 0), (16, 0), (16, 0)),red
			, green, blue, Alpha)

	def glColor4bv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(35, LCID, 1, (24, 0), ((16400, 0),),v
			)

	def glColor4d(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg, Alpha=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(36, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (5, 0)),red
			, green, blue, Alpha)

	def glColor4dv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(37, LCID, 1, (24, 0), ((16389, 0),),v
			)

	def glColor4f(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg, Alpha=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(38, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0), (4, 0)),red
			, green, blue, Alpha)

	def glColor4fv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(39, LCID, 1, (24, 0), ((16388, 0),),v
			)

	def glColor4i(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg, Alpha=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(40, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0), (3, 0)),red
			, green, blue, Alpha)

	def glColor4iv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(41, LCID, 1, (24, 0), ((16387, 0),),v
			)

	def glColor4s(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg, Alpha=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(42, LCID, 1, (24, 0), ((2, 0), (2, 0), (2, 0), (2, 0)),red
			, green, blue, Alpha)

	def glColor4sv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(43, LCID, 1, (24, 0), ((16386, 0),),v
			)

	def glColor4ub(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg, Alpha=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(44, LCID, 1, (24, 0), ((17, 0), (17, 0), (17, 0), (17, 0)),red
			, green, blue, Alpha)

	def glColor4ubv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(45, LCID, 1, (24, 0), ((16401, 0),),v
			)

	def glColor4ui(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg, Alpha=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(46, LCID, 1, (24, 0), ((19, 0), (19, 0), (19, 0), (19, 0)),red
			, green, blue, Alpha)

	def glColor4uiv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(47, LCID, 1, (24, 0), ((16403, 0),),v
			)

	def glColor4us(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg, Alpha=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(48, LCID, 1, (24, 0), ((18, 0), (18, 0), (18, 0), (18, 0)),red
			, green, blue, Alpha)

	def glColor4usv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(49, LCID, 1, (24, 0), ((16402, 0),),v
			)

	def glColorMask(self, red=defaultNamedNotOptArg, green=defaultNamedNotOptArg, blue=defaultNamedNotOptArg, Alpha=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(50, LCID, 1, (24, 0), ((17, 0), (17, 0), (17, 0), (17, 0)),red
			, green, blue, Alpha)

	def glColorMaterial(self, face=defaultNamedNotOptArg, Mode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(51, LCID, 1, (24, 0), ((19, 0), (19, 0)),face
			, Mode)

	def glColorPointer(self, size=defaultNamedNotOptArg, type=defaultNamedNotOptArg, stride=defaultNamedNotOptArg, pointer=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(52, LCID, 1, (24, 0), ((3, 0), (19, 0), (3, 0), (12, 0)),size
			, type, stride, pointer)

	def glCopyPixels(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, Width=defaultNamedNotOptArg, height=defaultNamedNotOptArg
			, type=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(53, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0), (3, 0), (19, 0)),x
			, y, Width, height, type)

	def glCopyTexImage1D(self, target=defaultNamedNotOptArg, level=defaultNamedNotOptArg, internalFormat=defaultNamedNotOptArg, x=defaultNamedNotOptArg
			, y=defaultNamedNotOptArg, Width=defaultNamedNotOptArg, border=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(54, LCID, 1, (24, 0), ((19, 0), (3, 0), (19, 0), (3, 0), (3, 0), (3, 0), (3, 0)),target
			, level, internalFormat, x, y, Width
			, border)

	def glCopyTexImage2D(self, target=defaultNamedNotOptArg, level=defaultNamedNotOptArg, internalFormat=defaultNamedNotOptArg, x=defaultNamedNotOptArg
			, y=defaultNamedNotOptArg, Width=defaultNamedNotOptArg, height=defaultNamedNotOptArg, border=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(55, LCID, 1, (24, 0), ((19, 0), (3, 0), (19, 0), (3, 0), (3, 0), (3, 0), (3, 0), (3, 0)),target
			, level, internalFormat, x, y, Width
			, height, border)

	def glCopyTexSubImage1D(self, target=defaultNamedNotOptArg, level=defaultNamedNotOptArg, xoffset=defaultNamedNotOptArg, x=defaultNamedNotOptArg
			, y=defaultNamedNotOptArg, Width=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(56, LCID, 1, (24, 0), ((19, 0), (3, 0), (3, 0), (3, 0), (3, 0), (3, 0)),target
			, level, xoffset, x, y, Width
			)

	def glCopyTexSubImage2D(self, target=defaultNamedNotOptArg, level=defaultNamedNotOptArg, xoffset=defaultNamedNotOptArg, yoffset=defaultNamedNotOptArg
			, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, Width=defaultNamedNotOptArg, height=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(57, LCID, 1, (24, 0), ((19, 0), (3, 0), (3, 0), (3, 0), (3, 0), (3, 0), (3, 0), (3, 0)),target
			, level, xoffset, yoffset, x, y
			, Width, height)

	def glCullFace(self, Mode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(58, LCID, 1, (24, 0), ((19, 0),),Mode
			)

	def glDeleteLists(self, list=defaultNamedNotOptArg, range=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(59, LCID, 1, (24, 0), ((19, 0), (3, 0)),list
			, range)

	def glDeleteTextures(self, n=defaultNamedNotOptArg, textures=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(60, LCID, 1, (24, 0), ((3, 0), (16403, 0)),n
			, textures)

	def glDepthFunc(self, func=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(61, LCID, 1, (24, 0), ((19, 0),),func
			)

	def glDepthMask(self, flag=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(62, LCID, 1, (24, 0), ((17, 0),),flag
			)

	def glDepthRange(self, zNear=defaultNamedNotOptArg, zFar=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(63, LCID, 1, (24, 0), ((5, 0), (5, 0)),zNear
			, zFar)

	def glDisable(self, cap=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(64, LCID, 1, (24, 0), ((19, 0),),cap
			)

	def glDisableClientState(self, array=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(65, LCID, 1, (24, 0), ((19, 0),),array
			)

	def glDrawArrays(self, Mode=defaultNamedNotOptArg, first=defaultNamedNotOptArg, count=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(66, LCID, 1, (24, 0), ((19, 0), (3, 0), (3, 0)),Mode
			, first, count)

	def glDrawBuffer(self, Mode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(67, LCID, 1, (24, 0), ((19, 0),),Mode
			)

	def glDrawElements(self, Mode=defaultNamedNotOptArg, count=defaultNamedNotOptArg, type=defaultNamedNotOptArg, indices=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(68, LCID, 1, (24, 0), ((19, 0), (3, 0), (19, 0), (12, 0)),Mode
			, count, type, indices)

	def glDrawPixels(self, Width=defaultNamedNotOptArg, height=defaultNamedNotOptArg, format=defaultNamedNotOptArg, type=defaultNamedNotOptArg
			, pixels=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(69, LCID, 1, (24, 0), ((3, 0), (3, 0), (19, 0), (19, 0), (12, 0)),Width
			, height, format, type, pixels)

	def glEdgeFlag(self, flag=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(70, LCID, 1, (24, 0), ((17, 0),),flag
			)

	def glEdgeFlagPointer(self, stride=defaultNamedNotOptArg, pointer=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(71, LCID, 1, (24, 0), ((3, 0), (12, 0)),stride
			, pointer)

	def glEdgeFlagv(self, flag=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(72, LCID, 1, (24, 0), ((16401, 0),),flag
			)

	def glEnable(self, cap=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(73, LCID, 1, (24, 0), ((19, 0),),cap
			)

	def glEnableClientState(self, array=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(74, LCID, 1, (24, 0), ((19, 0),),array
			)

	def glEnd(self):
		return self._oleobj_.InvokeTypes(75, LCID, 1, (24, 0), (),)

	def glEndList(self):
		return self._oleobj_.InvokeTypes(76, LCID, 1, (24, 0), (),)

	def glEvalCoord1d(self, u=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(77, LCID, 1, (24, 0), ((5, 0),),u
			)

	def glEvalCoord1dv(self, u=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(78, LCID, 1, (24, 0), ((16389, 0),),u
			)

	def glEvalCoord1f(self, u=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(79, LCID, 1, (24, 0), ((4, 0),),u
			)

	def glEvalCoord1fv(self, u=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(80, LCID, 1, (24, 0), ((16388, 0),),u
			)

	def glEvalCoord2d(self, u=defaultNamedNotOptArg, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(81, LCID, 1, (24, 0), ((5, 0), (5, 0)),u
			, v)

	def glEvalCoord2dv(self, u=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(82, LCID, 1, (24, 0), ((16389, 0),),u
			)

	def glEvalCoord2f(self, u=defaultNamedNotOptArg, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(83, LCID, 1, (24, 0), ((4, 0), (4, 0)),u
			, v)

	def glEvalCoord2fv(self, u=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(84, LCID, 1, (24, 0), ((16388, 0),),u
			)

	def glEvalMesh1(self, Mode=defaultNamedNotOptArg, i1=defaultNamedNotOptArg, i2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(85, LCID, 1, (24, 0), ((19, 0), (3, 0), (3, 0)),Mode
			, i1, i2)

	def glEvalMesh2(self, Mode=defaultNamedNotOptArg, i1=defaultNamedNotOptArg, i2=defaultNamedNotOptArg, j1=defaultNamedNotOptArg
			, j2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(86, LCID, 1, (24, 0), ((19, 0), (3, 0), (3, 0), (3, 0), (3, 0)),Mode
			, i1, i2, j1, j2)

	def glEvalPoint1(self, i=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(87, LCID, 1, (24, 0), ((3, 0),),i
			)

	def glEvalPoint2(self, i=defaultNamedNotOptArg, j=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(88, LCID, 1, (24, 0), ((3, 0), (3, 0)),i
			, j)

	def glFeedbackBuffer(self, size=defaultNamedNotOptArg, type=defaultNamedNotOptArg, buffer=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(89, LCID, 1, (24, 0), ((3, 0), (19, 0), (16388, 0)),size
			, type, buffer)

	def glFinish(self):
		return self._oleobj_.InvokeTypes(90, LCID, 1, (24, 0), (),)

	def glFlush(self):
		return self._oleobj_.InvokeTypes(91, LCID, 1, (24, 0), (),)

	def glFogf(self, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(92, LCID, 1, (24, 0), ((19, 0), (4, 0)),PName
			, param)

	def glFogfv(self, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(93, LCID, 1, (24, 0), ((19, 0), (16388, 0)),PName
			, Params)

	def glFogi(self, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(94, LCID, 1, (24, 0), ((19, 0), (3, 0)),PName
			, param)

	def glFogiv(self, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(95, LCID, 1, (24, 0), ((19, 0), (16387, 0)),PName
			, Params)

	def glFrontFace(self, Mode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(96, LCID, 1, (24, 0), ((19, 0),),Mode
			)

	def glFrustum(self, Left=defaultNamedNotOptArg, Right=defaultNamedNotOptArg, Bottom=defaultNamedNotOptArg, Top=defaultNamedNotOptArg
			, zNear=defaultNamedNotOptArg, zFar=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(97, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (5, 0), (5, 0), (5, 0)),Left
			, Right, Bottom, Top, zNear, zFar
			)

	def glGenLists(self, range=defaultNamedNotOptArg, pResult=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(98, LCID, 1, (24, 0), ((3, 0), (16403, 0)),range
			, pResult)

	def glGenTextures(self, n=defaultNamedNotOptArg, textures=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(99, LCID, 1, (24, 0), ((3, 0), (16403, 0)),n
			, textures)

	def glGetBooleanv(self, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(100, LCID, 1, (24, 0), ((19, 0), (16401, 0)),PName
			, Params)

	def glGetClipPlane(self, plane=defaultNamedNotOptArg, equation=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(101, LCID, 1, (24, 0), ((19, 0), (16389, 0)),plane
			, equation)

	def glGetDoublev(self, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(102, LCID, 1, (24, 0), ((19, 0), (16389, 0)),PName
			, Params)

	def glGetError(self, pResult=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(103, LCID, 1, (24, 0), ((16403, 0),),pResult
			)

	def glGetFloatv(self, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(104, LCID, 1, (24, 0), ((19, 0), (16388, 0)),PName
			, Params)

	def glGetIntegerv(self, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(105, LCID, 1, (24, 0), ((19, 0), (16387, 0)),PName
			, Params)

	def glGetLightfv(self, Light=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(106, LCID, 1, (24, 0), ((19, 0), (19, 0), (16388, 0)),Light
			, PName, Params)

	def glGetLightiv(self, Light=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(107, LCID, 1, (24, 0), ((19, 0), (19, 0), (16387, 0)),Light
			, PName, Params)

	def glGetMapdv(self, target=defaultNamedNotOptArg, query=defaultNamedNotOptArg, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(108, LCID, 1, (24, 0), ((19, 0), (19, 0), (16389, 0)),target
			, query, v)

	def glGetMapfv(self, target=defaultNamedNotOptArg, query=defaultNamedNotOptArg, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(109, LCID, 1, (24, 0), ((19, 0), (19, 0), (16388, 0)),target
			, query, v)

	def glGetMapiv(self, target=defaultNamedNotOptArg, query=defaultNamedNotOptArg, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(110, LCID, 1, (24, 0), ((19, 0), (19, 0), (16387, 0)),target
			, query, v)

	def glGetMaterialfv(self, face=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(111, LCID, 1, (24, 0), ((19, 0), (19, 0), (16388, 0)),face
			, PName, Params)

	def glGetMaterialiv(self, face=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(112, LCID, 1, (24, 0), ((19, 0), (19, 0), (16387, 0)),face
			, PName, Params)

	def glGetPixelMapfv(self, map=defaultNamedNotOptArg, Values=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(113, LCID, 1, (24, 0), ((19, 0), (16388, 0)),map
			, Values)

	def glGetPixelMapuiv(self, map=defaultNamedNotOptArg, Values=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(114, LCID, 1, (24, 0), ((19, 0), (16403, 0)),map
			, Values)

	def glGetPixelMapusv(self, map=defaultNamedNotOptArg, Values=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(115, LCID, 1, (24, 0), ((19, 0), (16402, 0)),map
			, Values)

	def glGetPointerv(self, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(116, LCID, 1, (24, 0), ((19, 0), (16396, 0)),PName
			, Params)

	def glGetPolygonStipple(self, Mask=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(117, LCID, 1, (24, 0), ((16401, 0),),Mask
			)

	def glGetString(self, name=defaultNamedNotOptArg, pResult=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(118, LCID, 1, (24, 0), ((19, 0), (16401, 0)),name
			, pResult)

	def glGetTexEnvfv(self, target=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(119, LCID, 1, (24, 0), ((19, 0), (19, 0), (16388, 0)),target
			, PName, Params)

	def glGetTexEnviv(self, target=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(120, LCID, 1, (24, 0), ((19, 0), (19, 0), (16387, 0)),target
			, PName, Params)

	def glGetTexGendv(self, coord=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(121, LCID, 1, (24, 0), ((19, 0), (19, 0), (16389, 0)),coord
			, PName, Params)

	def glGetTexGenfv(self, coord=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(122, LCID, 1, (24, 0), ((19, 0), (19, 0), (16388, 0)),coord
			, PName, Params)

	def glGetTexGeniv(self, coord=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(123, LCID, 1, (24, 0), ((19, 0), (19, 0), (16387, 0)),coord
			, PName, Params)

	def glGetTexImage(self, target=defaultNamedNotOptArg, level=defaultNamedNotOptArg, format=defaultNamedNotOptArg, type=defaultNamedNotOptArg
			, pixels=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(124, LCID, 1, (24, 0), ((19, 0), (3, 0), (19, 0), (19, 0), (12, 0)),target
			, level, format, type, pixels)

	def glGetTexLevelParameterfv(self, target=defaultNamedNotOptArg, level=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(125, LCID, 1, (24, 0), ((19, 0), (3, 0), (19, 0), (16388, 0)),target
			, level, PName, Params)

	def glGetTexLevelParameteriv(self, target=defaultNamedNotOptArg, level=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(126, LCID, 1, (24, 0), ((19, 0), (3, 0), (19, 0), (16387, 0)),target
			, level, PName, Params)

	def glGetTexParameterfv(self, target=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(127, LCID, 1, (24, 0), ((19, 0), (19, 0), (16388, 0)),target
			, PName, Params)

	def glGetTexParameteriv(self, target=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(128, LCID, 1, (24, 0), ((19, 0), (19, 0), (16387, 0)),target
			, PName, Params)

	def glHint(self, target=defaultNamedNotOptArg, Mode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(129, LCID, 1, (24, 0), ((19, 0), (19, 0)),target
			, Mode)

	def glIndexMask(self, Mask=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(130, LCID, 1, (24, 0), ((19, 0),),Mask
			)

	def glIndexPointer(self, type=defaultNamedNotOptArg, stride=defaultNamedNotOptArg, pointer=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(131, LCID, 1, (24, 0), ((19, 0), (3, 0), (12, 0)),type
			, stride, pointer)

	def glIndexd(self, c=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(132, LCID, 1, (24, 0), ((5, 0),),c
			)

	def glIndexdv(self, c=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(133, LCID, 1, (24, 0), ((16389, 0),),c
			)

	def glIndexf(self, c=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(134, LCID, 1, (24, 0), ((4, 0),),c
			)

	def glIndexfv(self, c=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(135, LCID, 1, (24, 0), ((16388, 0),),c
			)

	def glIndexi(self, c=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(136, LCID, 1, (24, 0), ((3, 0),),c
			)

	def glIndexiv(self, c=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(137, LCID, 1, (24, 0), ((16387, 0),),c
			)

	def glIndexs(self, c=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(138, LCID, 1, (24, 0), ((2, 0),),c
			)

	def glIndexsv(self, c=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(139, LCID, 1, (24, 0), ((16386, 0),),c
			)

	def glIndexub(self, c=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(140, LCID, 1, (24, 0), ((17, 0),),c
			)

	def glIndexubv(self, c=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(141, LCID, 1, (24, 0), ((16401, 0),),c
			)

	def glInitNames(self):
		return self._oleobj_.InvokeTypes(142, LCID, 1, (24, 0), (),)

	def glInterleavedArrays(self, format=defaultNamedNotOptArg, stride=defaultNamedNotOptArg, pointer=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(143, LCID, 1, (24, 0), ((19, 0), (3, 0), (12, 0)),format
			, stride, pointer)

	def glIsEnabled(self, cap=defaultNamedNotOptArg, pResult=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(144, LCID, 1, (24, 0), ((19, 0), (16401, 0)),cap
			, pResult)

	def glIsList(self, list=defaultNamedNotOptArg, pResult=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(145, LCID, 1, (24, 0), ((19, 0), (16401, 0)),list
			, pResult)

	def glIsTexture(self, texture=defaultNamedNotOptArg, pResult=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(146, LCID, 1, (24, 0), ((19, 0), (16401, 0)),texture
			, pResult)

	def glLightModelf(self, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(147, LCID, 1, (24, 0), ((19, 0), (4, 0)),PName
			, param)

	def glLightModelfv(self, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(148, LCID, 1, (24, 0), ((19, 0), (16388, 0)),PName
			, Params)

	def glLightModeli(self, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(149, LCID, 1, (24, 0), ((19, 0), (3, 0)),PName
			, param)

	def glLightModeliv(self, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(150, LCID, 1, (24, 0), ((19, 0), (16387, 0)),PName
			, Params)

	def glLightf(self, Light=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(151, LCID, 1, (24, 0), ((19, 0), (19, 0), (4, 0)),Light
			, PName, param)

	def glLightfv(self, Light=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(152, LCID, 1, (24, 0), ((19, 0), (19, 0), (16388, 0)),Light
			, PName, Params)

	def glLighti(self, Light=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(153, LCID, 1, (24, 0), ((19, 0), (19, 0), (3, 0)),Light
			, PName, param)

	def glLightiv(self, Light=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(154, LCID, 1, (24, 0), ((19, 0), (19, 0), (16387, 0)),Light
			, PName, Params)

	def glLineStipple(self, factor=defaultNamedNotOptArg, pattern=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(155, LCID, 1, (24, 0), ((3, 0), (18, 0)),factor
			, pattern)

	def glLineWidth(self, Width=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(156, LCID, 1, (24, 0), ((4, 0),),Width
			)

	def glListBase(self, base=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(157, LCID, 1, (24, 0), ((19, 0),),base
			)

	def glLoadIdentity(self):
		return self._oleobj_.InvokeTypes(158, LCID, 1, (24, 0), (),)

	def glLoadMatrixd(self, m=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(159, LCID, 1, (24, 0), ((16389, 0),),m
			)

	def glLoadMatrixf(self, m=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(160, LCID, 1, (24, 0), ((16388, 0),),m
			)

	def glLoadName(self, name=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(161, LCID, 1, (24, 0), ((19, 0),),name
			)

	def glLogicOp(self, opcode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(162, LCID, 1, (24, 0), ((19, 0),),opcode
			)

	def glMap1d(self, target=defaultNamedNotOptArg, u1=defaultNamedNotOptArg, u2=defaultNamedNotOptArg, stride=defaultNamedNotOptArg
			, order=defaultNamedNotOptArg, points=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(163, LCID, 1, (24, 0), ((19, 0), (5, 0), (5, 0), (3, 0), (3, 0), (16389, 0)),target
			, u1, u2, stride, order, points
			)

	def glMap1f(self, target=defaultNamedNotOptArg, u1=defaultNamedNotOptArg, u2=defaultNamedNotOptArg, stride=defaultNamedNotOptArg
			, order=defaultNamedNotOptArg, points=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(164, LCID, 1, (24, 0), ((19, 0), (4, 0), (4, 0), (3, 0), (3, 0), (16388, 0)),target
			, u1, u2, stride, order, points
			)

	def glMap2d(self, target=defaultNamedNotOptArg, u1=defaultNamedNotOptArg, u2=defaultNamedNotOptArg, ustride=defaultNamedNotOptArg
			, uorder=defaultNamedNotOptArg, v1=defaultNamedNotOptArg, v2=defaultNamedNotOptArg, vstride=defaultNamedNotOptArg, vorder=defaultNamedNotOptArg
			, points=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(336, LCID, 1, (24, 0), ((19, 0), (5, 0), (5, 0), (3, 0), (3, 0), (5, 0), (5, 0), (3, 0), (3, 0), (16389, 0)),target
			, u1, u2, ustride, uorder, v1
			, v2, vstride, vorder, points)

	def glMap2f(self, target=defaultNamedNotOptArg, u1=defaultNamedNotOptArg, u2=defaultNamedNotOptArg, ustride=defaultNamedNotOptArg
			, uorder=defaultNamedNotOptArg, v1=defaultNamedNotOptArg, v2=defaultNamedNotOptArg, vstride=defaultNamedNotOptArg, vorder=defaultNamedNotOptArg
			, points=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(165, LCID, 1, (24, 0), ((19, 0), (4, 0), (4, 0), (3, 0), (3, 0), (4, 0), (4, 0), (3, 0), (3, 0), (16388, 0)),target
			, u1, u2, ustride, uorder, v1
			, v2, vstride, vorder, points)

	def glMapGrid1d(self, un=defaultNamedNotOptArg, u1=defaultNamedNotOptArg, u2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(166, LCID, 1, (24, 0), ((3, 0), (5, 0), (5, 0)),un
			, u1, u2)

	def glMapGrid1f(self, un=defaultNamedNotOptArg, u1=defaultNamedNotOptArg, u2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(167, LCID, 1, (24, 0), ((3, 0), (4, 0), (4, 0)),un
			, u1, u2)

	def glMapGrid2d(self, un=defaultNamedNotOptArg, u1=defaultNamedNotOptArg, u2=defaultNamedNotOptArg, vn=defaultNamedNotOptArg
			, v1=defaultNamedNotOptArg, v2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(168, LCID, 1, (24, 0), ((3, 0), (5, 0), (5, 0), (3, 0), (5, 0), (5, 0)),un
			, u1, u2, vn, v1, v2
			)

	def glMapGrid2f(self, un=defaultNamedNotOptArg, u1=defaultNamedNotOptArg, u2=defaultNamedNotOptArg, vn=defaultNamedNotOptArg
			, v1=defaultNamedNotOptArg, v2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(169, LCID, 1, (24, 0), ((3, 0), (4, 0), (4, 0), (3, 0), (4, 0), (4, 0)),un
			, u1, u2, vn, v1, v2
			)

	def glMaterialf(self, face=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(170, LCID, 1, (24, 0), ((19, 0), (19, 0), (4, 0)),face
			, PName, param)

	def glMaterialfv(self, face=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(171, LCID, 1, (24, 0), ((19, 0), (19, 0), (16388, 0)),face
			, PName, Params)

	def glMateriali(self, face=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(172, LCID, 1, (24, 0), ((19, 0), (19, 0), (3, 0)),face
			, PName, param)

	def glMaterialiv(self, face=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(173, LCID, 1, (24, 0), ((19, 0), (19, 0), (16387, 0)),face
			, PName, Params)

	def glMatrixMode(self, Mode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(174, LCID, 1, (24, 0), ((19, 0),),Mode
			)

	def glMultMatrixd(self, m=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(175, LCID, 1, (24, 0), ((16389, 0),),m
			)

	def glMultMatrixf(self, m=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(176, LCID, 1, (24, 0), ((16388, 0),),m
			)

	def glNewList(self, list=defaultNamedNotOptArg, Mode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(177, LCID, 1, (24, 0), ((19, 0), (19, 0)),list
			, Mode)

	def glNormal3b(self, nx=defaultNamedNotOptArg, ny=defaultNamedNotOptArg, nz=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(178, LCID, 1, (24, 0), ((16, 0), (16, 0), (16, 0)),nx
			, ny, nz)

	def glNormal3bv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(179, LCID, 1, (24, 0), ((16400, 0),),v
			)

	def glNormal3d(self, nx=defaultNamedNotOptArg, ny=defaultNamedNotOptArg, nz=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(180, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0)),nx
			, ny, nz)

	def glNormal3dv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(181, LCID, 1, (24, 0), ((16389, 0),),v
			)

	def glNormal3f(self, nx=defaultNamedNotOptArg, ny=defaultNamedNotOptArg, nz=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(182, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0)),nx
			, ny, nz)

	def glNormal3fv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(183, LCID, 1, (24, 0), ((16388, 0),),v
			)

	def glNormal3i(self, nx=defaultNamedNotOptArg, ny=defaultNamedNotOptArg, nz=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(184, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0)),nx
			, ny, nz)

	def glNormal3iv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(185, LCID, 1, (24, 0), ((16387, 0),),v
			)

	def glNormal3s(self, nx=defaultNamedNotOptArg, ny=defaultNamedNotOptArg, nz=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(186, LCID, 1, (24, 0), ((2, 0), (2, 0), (2, 0)),nx
			, ny, nz)

	def glNormal3sv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(187, LCID, 1, (24, 0), ((16386, 0),),v
			)

	def glNormalPointer(self, type=defaultNamedNotOptArg, stride=defaultNamedNotOptArg, pointer=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(188, LCID, 1, (24, 0), ((19, 0), (3, 0), (12, 0)),type
			, stride, pointer)

	def glOrtho(self, Left=defaultNamedNotOptArg, Right=defaultNamedNotOptArg, Bottom=defaultNamedNotOptArg, Top=defaultNamedNotOptArg
			, zNear=defaultNamedNotOptArg, zFar=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(189, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (5, 0), (5, 0), (5, 0)),Left
			, Right, Bottom, Top, zNear, zFar
			)

	def glPassThrough(self, token=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(190, LCID, 1, (24, 0), ((4, 0),),token
			)

	def glPixelMapfv(self, map=defaultNamedNotOptArg, mapsize=defaultNamedNotOptArg, Values=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(191, LCID, 1, (24, 0), ((19, 0), (3, 0), (16388, 0)),map
			, mapsize, Values)

	def glPixelMapuiv(self, map=defaultNamedNotOptArg, mapsize=defaultNamedNotOptArg, Values=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(192, LCID, 1, (24, 0), ((19, 0), (3, 0), (16403, 0)),map
			, mapsize, Values)

	def glPixelMapusv(self, map=defaultNamedNotOptArg, mapsize=defaultNamedNotOptArg, Values=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(193, LCID, 1, (24, 0), ((19, 0), (3, 0), (16402, 0)),map
			, mapsize, Values)

	def glPixelStoref(self, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(194, LCID, 1, (24, 0), ((19, 0), (4, 0)),PName
			, param)

	def glPixelStorei(self, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(195, LCID, 1, (24, 0), ((19, 0), (3, 0)),PName
			, param)

	def glPixelTransferf(self, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(196, LCID, 1, (24, 0), ((19, 0), (4, 0)),PName
			, param)

	def glPixelTransferi(self, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(197, LCID, 1, (24, 0), ((19, 0), (3, 0)),PName
			, param)

	def glPixelZoom(self, xfactor=defaultNamedNotOptArg, yfactor=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(198, LCID, 1, (24, 0), ((4, 0), (4, 0)),xfactor
			, yfactor)

	def glPointSize(self, size=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(199, LCID, 1, (24, 0), ((4, 0),),size
			)

	def glPolygonMode(self, face=defaultNamedNotOptArg, Mode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(200, LCID, 1, (24, 0), ((19, 0), (19, 0)),face
			, Mode)

	def glPolygonOffset(self, factor=defaultNamedNotOptArg, units=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(201, LCID, 1, (24, 0), ((4, 0), (4, 0)),factor
			, units)

	def glPolygonStipple(self, Mask=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(202, LCID, 1, (24, 0), ((16401, 0),),Mask
			)

	def glPopAttrib(self):
		return self._oleobj_.InvokeTypes(203, LCID, 1, (24, 0), (),)

	def glPopClientAttrib(self):
		return self._oleobj_.InvokeTypes(204, LCID, 1, (24, 0), (),)

	def glPopMatrix(self):
		return self._oleobj_.InvokeTypes(205, LCID, 1, (24, 0), (),)

	def glPopName(self):
		return self._oleobj_.InvokeTypes(206, LCID, 1, (24, 0), (),)

	def glPrioritizeTextures(self, n=defaultNamedNotOptArg, textures=defaultNamedNotOptArg, priorities=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(207, LCID, 1, (24, 0), ((3, 0), (16403, 0), (16388, 0)),n
			, textures, priorities)

	def glPushAttrib(self, Mask=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(208, LCID, 1, (24, 0), ((19, 0),),Mask
			)

	def glPushClientAttrib(self, Mask=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(209, LCID, 1, (24, 0), ((19, 0),),Mask
			)

	def glPushMatrix(self):
		return self._oleobj_.InvokeTypes(210, LCID, 1, (24, 0), (),)

	def glPushName(self, name=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(211, LCID, 1, (24, 0), ((19, 0),),name
			)

	def glRasterPos2d(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(212, LCID, 1, (24, 0), ((5, 0), (5, 0)),x
			, y)

	def glRasterPos2dv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(213, LCID, 1, (24, 0), ((16389, 0),),v
			)

	def glRasterPos2f(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(214, LCID, 1, (24, 0), ((4, 0), (4, 0)),x
			, y)

	def glRasterPos2fv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(215, LCID, 1, (24, 0), ((16388, 0),),v
			)

	def glRasterPos2i(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(216, LCID, 1, (24, 0), ((3, 0), (3, 0)),x
			, y)

	def glRasterPos2iv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(217, LCID, 1, (24, 0), ((16387, 0),),v
			)

	def glRasterPos2s(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(218, LCID, 1, (24, 0), ((2, 0), (2, 0)),x
			, y)

	def glRasterPos2sv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(219, LCID, 1, (24, 0), ((16386, 0),),v
			)

	def glRasterPos3d(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(220, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0)),x
			, y, z)

	def glRasterPos3dv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(221, LCID, 1, (24, 0), ((16389, 0),),v
			)

	def glRasterPos3f(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(222, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0)),x
			, y, z)

	def glRasterPos3fv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(223, LCID, 1, (24, 0), ((16388, 0),),v
			)

	def glRasterPos3i(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(224, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0)),x
			, y, z)

	def glRasterPos3iv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(225, LCID, 1, (24, 0), ((16387, 0),),v
			)

	def glRasterPos3s(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(226, LCID, 1, (24, 0), ((2, 0), (2, 0), (2, 0)),x
			, y, z)

	def glRasterPos3sv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(227, LCID, 1, (24, 0), ((16386, 0),),v
			)

	def glRasterPos4d(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg, w=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(228, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (5, 0)),x
			, y, z, w)

	def glRasterPos4dv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(229, LCID, 1, (24, 0), ((16389, 0),),v
			)

	def glRasterPos4f(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg, w=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(230, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0), (4, 0)),x
			, y, z, w)

	def glRasterPos4fv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(231, LCID, 1, (24, 0), ((16388, 0),),v
			)

	def glRasterPos4i(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg, w=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(232, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0), (3, 0)),x
			, y, z, w)

	def glRasterPos4iv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(233, LCID, 1, (24, 0), ((16387, 0),),v
			)

	def glRasterPos4s(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg, w=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(234, LCID, 1, (24, 0), ((2, 0), (2, 0), (2, 0), (2, 0)),x
			, y, z, w)

	def glRasterPos4sv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(235, LCID, 1, (24, 0), ((16386, 0),),v
			)

	def glReadBuffer(self, Mode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(236, LCID, 1, (24, 0), ((19, 0),),Mode
			)

	def glReadPixels(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, Width=defaultNamedNotOptArg, height=defaultNamedNotOptArg
			, format=defaultNamedNotOptArg, type=defaultNamedNotOptArg, pixels=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(237, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0), (3, 0), (19, 0), (19, 0), (16396, 0)),x
			, y, Width, height, format, type
			, pixels)

	def glRectd(self, x1=defaultNamedNotOptArg, y1=defaultNamedNotOptArg, x2=defaultNamedNotOptArg, y2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(238, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (5, 0)),x1
			, y1, x2, y2)

	def glRectdv(self, v1=defaultNamedNotOptArg, v2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(239, LCID, 1, (24, 0), ((16389, 0), (16389, 0)),v1
			, v2)

	def glRectf(self, x1=defaultNamedNotOptArg, y1=defaultNamedNotOptArg, x2=defaultNamedNotOptArg, y2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(240, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0), (4, 0)),x1
			, y1, x2, y2)

	def glRectfv(self, v1=defaultNamedNotOptArg, v2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(241, LCID, 1, (24, 0), ((16388, 0), (16388, 0)),v1
			, v2)

	def glRecti(self, x1=defaultNamedNotOptArg, y1=defaultNamedNotOptArg, x2=defaultNamedNotOptArg, y2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(242, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0), (3, 0)),x1
			, y1, x2, y2)

	def glRectiv(self, v1=defaultNamedNotOptArg, v2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(243, LCID, 1, (24, 0), ((16387, 0), (16387, 0)),v1
			, v2)

	def glRects(self, x1=defaultNamedNotOptArg, y1=defaultNamedNotOptArg, x2=defaultNamedNotOptArg, y2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(244, LCID, 1, (24, 0), ((2, 0), (2, 0), (2, 0), (2, 0)),x1
			, y1, x2, y2)

	def glRectsv(self, v1=defaultNamedNotOptArg, v2=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(245, LCID, 1, (24, 0), ((16386, 0), (16386, 0)),v1
			, v2)

	def glRenderMode(self, Mode=defaultNamedNotOptArg, pResult=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(246, LCID, 1, (24, 0), ((19, 0), (16387, 0)),Mode
			, pResult)

	def glRotated(self, Angle=defaultNamedNotOptArg, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(247, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (5, 0)),Angle
			, x, y, z)

	def glRotatef(self, Angle=defaultNamedNotOptArg, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(248, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0), (4, 0)),Angle
			, x, y, z)

	def glScaled(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(249, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0)),x
			, y, z)

	def glScalef(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(250, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0)),x
			, y, z)

	def glScissor(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, Width=defaultNamedNotOptArg, height=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(251, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0), (3, 0)),x
			, y, Width, height)

	def glSelectBuffer(self, size=defaultNamedNotOptArg, buffer=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(252, LCID, 1, (24, 0), ((3, 0), (16403, 0)),size
			, buffer)

	def glShadeModel(self, Mode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(253, LCID, 1, (24, 0), ((19, 0),),Mode
			)

	def glStencilFunc(self, func=defaultNamedNotOptArg, ref=defaultNamedNotOptArg, Mask=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(254, LCID, 1, (24, 0), ((19, 0), (3, 0), (19, 0)),func
			, ref, Mask)

	def glStencilMask(self, Mask=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(255, LCID, 1, (24, 0), ((19, 0),),Mask
			)

	def glStencilOp(self, fail=defaultNamedNotOptArg, zfail=defaultNamedNotOptArg, zpass=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(256, LCID, 1, (24, 0), ((19, 0), (19, 0), (19, 0)),fail
			, zfail, zpass)

	def glTexCoord1d(self, s=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(257, LCID, 1, (24, 0), ((5, 0),),s
			)

	def glTexCoord1dv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(258, LCID, 1, (24, 0), ((16389, 0),),v
			)

	def glTexCoord1f(self, s=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(259, LCID, 1, (24, 0), ((4, 0),),s
			)

	def glTexCoord1fv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(260, LCID, 1, (24, 0), ((16388, 0),),v
			)

	def glTexCoord1i(self, s=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(261, LCID, 1, (24, 0), ((3, 0),),s
			)

	def glTexCoord1iv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(262, LCID, 1, (24, 0), ((16387, 0),),v
			)

	def glTexCoord1s(self, s=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(263, LCID, 1, (24, 0), ((2, 0),),s
			)

	def glTexCoord1sv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(264, LCID, 1, (24, 0), ((16386, 0),),v
			)

	def glTexCoord2d(self, s=defaultNamedNotOptArg, t=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(265, LCID, 1, (24, 0), ((5, 0), (5, 0)),s
			, t)

	def glTexCoord2dv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(266, LCID, 1, (24, 0), ((16389, 0),),v
			)

	def glTexCoord2f(self, s=defaultNamedNotOptArg, t=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(267, LCID, 1, (24, 0), ((4, 0), (4, 0)),s
			, t)

	def glTexCoord2fv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(268, LCID, 1, (24, 0), ((16388, 0),),v
			)

	def glTexCoord2i(self, s=defaultNamedNotOptArg, t=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(269, LCID, 1, (24, 0), ((3, 0), (3, 0)),s
			, t)

	def glTexCoord2iv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(270, LCID, 1, (24, 0), ((16387, 0),),v
			)

	def glTexCoord2s(self, s=defaultNamedNotOptArg, t=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(271, LCID, 1, (24, 0), ((2, 0), (2, 0)),s
			, t)

	def glTexCoord2sv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(272, LCID, 1, (24, 0), ((16386, 0),),v
			)

	def glTexCoord3d(self, s=defaultNamedNotOptArg, t=defaultNamedNotOptArg, r=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(273, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0)),s
			, t, r)

	def glTexCoord3dv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(274, LCID, 1, (24, 0), ((16389, 0),),v
			)

	def glTexCoord3f(self, s=defaultNamedNotOptArg, t=defaultNamedNotOptArg, r=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(275, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0)),s
			, t, r)

	def glTexCoord3fv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(276, LCID, 1, (24, 0), ((16388, 0),),v
			)

	def glTexCoord3i(self, s=defaultNamedNotOptArg, t=defaultNamedNotOptArg, r=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(277, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0)),s
			, t, r)

	def glTexCoord3iv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(278, LCID, 1, (24, 0), ((16387, 0),),v
			)

	def glTexCoord3s(self, s=defaultNamedNotOptArg, t=defaultNamedNotOptArg, r=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(279, LCID, 1, (24, 0), ((2, 0), (2, 0), (2, 0)),s
			, t, r)

	def glTexCoord3sv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(280, LCID, 1, (24, 0), ((16386, 0),),v
			)

	def glTexCoord4d(self, s=defaultNamedNotOptArg, t=defaultNamedNotOptArg, r=defaultNamedNotOptArg, q=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(281, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (5, 0)),s
			, t, r, q)

	def glTexCoord4dv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(282, LCID, 1, (24, 0), ((16389, 0),),v
			)

	def glTexCoord4f(self, s=defaultNamedNotOptArg, t=defaultNamedNotOptArg, r=defaultNamedNotOptArg, q=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(283, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0), (4, 0)),s
			, t, r, q)

	def glTexCoord4fv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(284, LCID, 1, (24, 0), ((16388, 0),),v
			)

	def glTexCoord4i(self, s=defaultNamedNotOptArg, t=defaultNamedNotOptArg, r=defaultNamedNotOptArg, q=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(285, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0), (3, 0)),s
			, t, r, q)

	def glTexCoord4iv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(286, LCID, 1, (24, 0), ((16387, 0),),v
			)

	def glTexCoord4s(self, s=defaultNamedNotOptArg, t=defaultNamedNotOptArg, r=defaultNamedNotOptArg, q=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(287, LCID, 1, (24, 0), ((2, 0), (2, 0), (2, 0), (2, 0)),s
			, t, r, q)

	def glTexCoord4sv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(288, LCID, 1, (24, 0), ((16386, 0),),v
			)

	def glTexCoordPointer(self, size=defaultNamedNotOptArg, type=defaultNamedNotOptArg, stride=defaultNamedNotOptArg, pointer=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(289, LCID, 1, (24, 0), ((3, 0), (19, 0), (3, 0), (12, 0)),size
			, type, stride, pointer)

	def glTexEnvf(self, target=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(290, LCID, 1, (24, 0), ((19, 0), (19, 0), (4, 0)),target
			, PName, param)

	def glTexEnvfv(self, target=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(291, LCID, 1, (24, 0), ((19, 0), (19, 0), (16388, 0)),target
			, PName, Params)

	def glTexEnvi(self, target=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(292, LCID, 1, (24, 0), ((19, 0), (19, 0), (3, 0)),target
			, PName, param)

	def glTexEnviv(self, target=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(293, LCID, 1, (24, 0), ((19, 0), (19, 0), (16387, 0)),target
			, PName, Params)

	def glTexGend(self, coord=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(294, LCID, 1, (24, 0), ((19, 0), (19, 0), (5, 0)),coord
			, PName, param)

	def glTexGendv(self, coord=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(295, LCID, 1, (24, 0), ((19, 0), (19, 0), (16389, 0)),coord
			, PName, Params)

	def glTexGenf(self, coord=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(296, LCID, 1, (24, 0), ((19, 0), (19, 0), (4, 0)),coord
			, PName, param)

	def glTexGenfv(self, coord=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(297, LCID, 1, (24, 0), ((19, 0), (19, 0), (16388, 0)),coord
			, PName, Params)

	def glTexGeni(self, coord=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(298, LCID, 1, (24, 0), ((19, 0), (19, 0), (3, 0)),coord
			, PName, param)

	def glTexGeniv(self, coord=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(299, LCID, 1, (24, 0), ((19, 0), (19, 0), (16387, 0)),coord
			, PName, Params)

	def glTexImage1D(self, target=defaultNamedNotOptArg, level=defaultNamedNotOptArg, internalFormat=defaultNamedNotOptArg, Width=defaultNamedNotOptArg
			, border=defaultNamedNotOptArg, format=defaultNamedNotOptArg, type=defaultNamedNotOptArg, pixels=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(300, LCID, 1, (24, 0), ((19, 0), (3, 0), (3, 0), (3, 0), (3, 0), (19, 0), (19, 0), (12, 0)),target
			, level, internalFormat, Width, border, format
			, type, pixels)

	def glTexImage2D(self, target=defaultNamedNotOptArg, level=defaultNamedNotOptArg, internalFormat=defaultNamedNotOptArg, Width=defaultNamedNotOptArg
			, height=defaultNamedNotOptArg, border=defaultNamedNotOptArg, format=defaultNamedNotOptArg, type=defaultNamedNotOptArg, pixels=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(301, LCID, 1, (24, 0), ((19, 0), (3, 0), (3, 0), (3, 0), (3, 0), (3, 0), (19, 0), (19, 0), (12, 0)),target
			, level, internalFormat, Width, height, border
			, format, type, pixels)

	def glTexParameterf(self, target=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(302, LCID, 1, (24, 0), ((19, 0), (19, 0), (4, 0)),target
			, PName, param)

	def glTexParameterfv(self, target=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(303, LCID, 1, (24, 0), ((19, 0), (19, 0), (16388, 0)),target
			, PName, Params)

	def glTexParameteri(self, target=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, param=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(304, LCID, 1, (24, 0), ((19, 0), (19, 0), (3, 0)),target
			, PName, param)

	def glTexParameteriv(self, target=defaultNamedNotOptArg, PName=defaultNamedNotOptArg, Params=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(305, LCID, 1, (24, 0), ((19, 0), (19, 0), (16387, 0)),target
			, PName, Params)

	def glTexSubImage1D(self, target=defaultNamedNotOptArg, level=defaultNamedNotOptArg, xoffset=defaultNamedNotOptArg, Width=defaultNamedNotOptArg
			, format=defaultNamedNotOptArg, type=defaultNamedNotOptArg, pixels=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(306, LCID, 1, (24, 0), ((19, 0), (3, 0), (3, 0), (3, 0), (19, 0), (19, 0), (12, 0)),target
			, level, xoffset, Width, format, type
			, pixels)

	def glTexSubImage2D(self, target=defaultNamedNotOptArg, level=defaultNamedNotOptArg, xoffset=defaultNamedNotOptArg, yoffset=defaultNamedNotOptArg
			, Width=defaultNamedNotOptArg, height=defaultNamedNotOptArg, format=defaultNamedNotOptArg, type=defaultNamedNotOptArg, pixels=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(307, LCID, 1, (24, 0), ((19, 0), (3, 0), (3, 0), (3, 0), (3, 0), (3, 0), (19, 0), (19, 0), (12, 0)),target
			, level, xoffset, yoffset, Width, height
			, format, type, pixels)

	def glTranslated(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(308, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0)),x
			, y, z)

	def glTranslatef(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(309, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0)),x
			, y, z)

	def glVertex2d(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(310, LCID, 1, (24, 0), ((5, 0), (5, 0)),x
			, y)

	def glVertex2dv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(311, LCID, 1, (24, 0), ((16389, 0),),v
			)

	def glVertex2f(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(312, LCID, 1, (24, 0), ((4, 0), (4, 0)),x
			, y)

	def glVertex2fv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(313, LCID, 1, (24, 0), ((16388, 0),),v
			)

	def glVertex2i(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(314, LCID, 1, (24, 0), ((3, 0), (3, 0)),x
			, y)

	def glVertex2iv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(315, LCID, 1, (24, 0), ((16387, 0),),v
			)

	def glVertex2s(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(316, LCID, 1, (24, 0), ((2, 0), (2, 0)),x
			, y)

	def glVertex2sv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(317, LCID, 1, (24, 0), ((16386, 0),),v
			)

	def glVertex3d(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(318, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0)),x
			, y, z)

	def glVertex3dv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(319, LCID, 1, (24, 0), ((16389, 0),),v
			)

	def glVertex3f(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(320, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0)),x
			, y, z)

	def glVertex3fv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(321, LCID, 1, (24, 0), ((16388, 0),),v
			)

	def glVertex3i(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(322, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0)),x
			, y, z)

	def glVertex3iv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(323, LCID, 1, (24, 0), ((16387, 0),),v
			)

	def glVertex3s(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(324, LCID, 1, (24, 0), ((2, 0), (2, 0), (2, 0)),x
			, y, z)

	def glVertex3sv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(325, LCID, 1, (24, 0), ((16386, 0),),v
			)

	def glVertex4d(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg, w=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(326, LCID, 1, (24, 0), ((5, 0), (5, 0), (5, 0), (5, 0)),x
			, y, z, w)

	def glVertex4dv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(327, LCID, 1, (24, 0), ((16389, 0),),v
			)

	def glVertex4f(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg, w=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(328, LCID, 1, (24, 0), ((4, 0), (4, 0), (4, 0), (4, 0)),x
			, y, z, w)

	def glVertex4fv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(329, LCID, 1, (24, 0), ((16388, 0),),v
			)

	def glVertex4i(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg, w=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(330, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0), (3, 0)),x
			, y, z, w)

	def glVertex4iv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(331, LCID, 1, (24, 0), ((16387, 0),),v
			)

	def glVertex4s(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, z=defaultNamedNotOptArg, w=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(332, LCID, 1, (24, 0), ((2, 0), (2, 0), (2, 0), (2, 0)),x
			, y, z, w)

	def glVertex4sv(self, v=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(333, LCID, 1, (24, 0), ((16386, 0),),v
			)

	def glVertexPointer(self, size=defaultNamedNotOptArg, type=defaultNamedNotOptArg, stride=defaultNamedNotOptArg, pointer=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(334, LCID, 1, (24, 0), ((3, 0), (19, 0), (3, 0), (12, 0)),size
			, type, stride, pointer)

	def glViewport(self, x=defaultNamedNotOptArg, y=defaultNamedNotOptArg, Width=defaultNamedNotOptArg, height=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(335, LCID, 1, (24, 0), ((3, 0), (3, 0), (3, 0), (3, 0)),x
			, y, Width, height)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IOpenGLViewEvents(DispatchBaseClass):
	CLSID = IID('{800A76A8-A08C-47CF-A985-EA4DEC364E2F}')
	coclass_clsid = IID('{C6C4739C-4E00-488F-8024-76F793261B17}')

	def OnEvent(self, EventType=defaultNamedNotOptArg, Param1=defaultNamedNotOptArg, Param2=defaultNamedNotOptArg, Param3=defaultNamedNotOptArg
			, Param4=defaultNamedNotOptArg, Param5=defaultNamedNotOptArg, Param6=defaultNamedNotOptArg, Param7=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((3, 0), (12, 0), (12, 0), (12, 0), (12, 0), (12, 0), (12, 0), (12, 0)),EventType
			, Param1, Param2, Param3, Param4, Param5
			, Param6, Param7)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IPropertyWindow(DispatchBaseClass):
	CLSID = IID('{CC1583FE-2AB3-4D68-B050-502C588F3FE8}')
	coclass_clsid = IID('{C73BF8A4-E4DC-48E9-8A8A-68695D71C777}')

	def AddColorPropertyItem(self, GroupName=defaultNamedNotOptArg, PropertyName=defaultNamedNotOptArg, DefaultValue=defaultNamedNotOptArg, Description=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(16, LCID, 1, (24, 0), ((8, 0), (8, 0), (21, 0), (8, 0)),GroupName
			, PropertyName, DefaultValue, Description)

	def AddCustomPropertyItem(self, GroupName=defaultNamedNotOptArg, CustMFCPropWnd=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(18, LCID, 1, (24, 0), ((8, 0), (12, 0)),GroupName
			, CustMFCPropWnd)

	def AddFilePathItem(self, GroupName=defaultNamedNotOptArg, PropertyName=defaultNamedNotOptArg, DefValue=defaultNamedNotOptArg, bIsFilePath=defaultNamedNotOptArg
			, ExtensionFilter=defaultNamedNotOptArg, DefExt=defaultNamedNotOptArg, Description=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(15, LCID, 1, (24, 0), ((8, 0), (8, 0), (8, 0), (3, 0), (8, 0), (8, 0), (8, 0)),GroupName
			, PropertyName, DefValue, bIsFilePath, ExtensionFilter, DefExt
			, Description)

	def AddHierarchyItem(self, GroupName=defaultNamedNotOptArg, SubGroupItemNames=defaultNamedNotOptArg, PropertyName=defaultNamedNotOptArg, Description=defaultNamedNotOptArg
			, ItemType=defaultNamedNotOptArg, DefValue=defaultNamedNotOptArg, AddParam1=defaultNamedNotOptArg, AddParam2=defaultNamedNotOptArg, AddParam3=defaultNamedNotOptArg
			, AddParam4=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(17, LCID, 1, (24, 0), ((8, 0), (8, 0), (8, 0), (8, 0), (3, 0), (12, 0), (12, 0), (12, 0), (12, 0), (12, 0)),GroupName
			, SubGroupItemNames, PropertyName, Description, ItemType, DefValue
			, AddParam1, AddParam2, AddParam3, AddParam4)

	def AddPropertyGroup(self, PropertyGroupName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(7, LCID, 1, (24, 0), ((8, 0),),PropertyGroupName
			)

	def AddPropertyItem(self, GroupName=defaultNamedNotOptArg, ItemPropertyName=defaultNamedNotOptArg, pValue=defaultNamedNotOptArg, Description=defaultNamedNotOptArg
			, EditMode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(8, LCID, 1, (24, 0), ((8, 0), (8, 0), (12, 0), (8, 0), (3, 0)),GroupName
			, ItemPropertyName, pValue, Description, EditMode)

	def AddPropertyItemAsString(self, GroupName=defaultNamedNotOptArg, PropertyName=defaultNamedNotOptArg, Value=defaultNamedNotOptArg, Description=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(12, LCID, 1, (24, 0), ((8, 0), (8, 0), (8, 0), (8, 0)),GroupName
			, PropertyName, Value, Description)

	def AddPropertyItemsAsString(self, GroupName=defaultNamedNotOptArg, PropertyName=defaultNamedNotOptArg, Values=defaultNamedNotOptArg, DefValue=defaultNamedNotOptArg
			, Desc=defaultNamedNotOptArg, EditMode=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(14, LCID, 1, (24, 0), ((8, 0), (8, 0), (8, 0), (8, 0), (8, 0), (3, 0)),GroupName
			, PropertyName, Values, DefValue, Desc, EditMode
			)

	def AdjustLayout(self):
		return self._oleobj_.InvokeTypes(10, LCID, 1, (24, 0), (),)

	def EnableDescriptionArea(self, bEnable=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((3, 0),),bEnable
			)

	def EnableHeaderCtrl(self, bEnable=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((3, 0),),bEnable
			)

	def EnableHeaderCtrlEx(self, bEnable=defaultNamedNotOptArg, LeftColumnName=defaultNamedNotOptArg, RightColumnName=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(6, LCID, 1, (24, 0), ((3, 0), (8, 0), (8, 0)),bEnable
			, LeftColumnName, RightColumnName)

	def GetValue(self, GroupName=defaultNamedNotOptArg, PropertyName=defaultNamedNotOptArg, Value=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(11, LCID, 1, (24, 0), ((8, 0), (8, 0), (16396, 0)),GroupName
			, PropertyName, Value)

	def GetValueAsString(self, GroupName=defaultNamedNotOptArg, PropertyName=defaultNamedNotOptArg, pValue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(13, LCID, 1, (24, 0), ((8, 0), (8, 0), (16392, 0)),GroupName
			, PropertyName, pValue)

	def MarkModifiedProperties(self, bMark=defaultNamedNotOptArg, bRedraw=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(5, LCID, 1, (24, 0), ((3, 0), (3, 0)),bMark
			, bRedraw)

	def RemoveAll(self):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), (),)

	def SetPropListFont(self):
		return self._oleobj_.InvokeTypes(9, LCID, 1, (24, 0), (),)

	def SetVSDotNetLook(self, bEnable=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), ((3, 0),),bEnable
			)

	_prop_map_get_ = {
		"PropGrdWindow": (20, 2, (12, 0), (), "PropGrdWindow", None),
		"Window": (19, 2, (12, 0), (), "Window", None),
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IPropertyWindowEvents(DispatchBaseClass):
	CLSID = IID('{59950C9D-08FD-4C0D-B44C-E4C0F5C2E3AB}')
	coclass_clsid = IID('{5ADFA2F2-A0F4-4EED-92C3-6E91013A1F64}')

	def OnPropertyChanged(self, GroupName=defaultNamedNotOptArg, PropertyName=defaultNamedNotOptArg, PropertyValue=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((8, 0), (8, 0), (8, 0)),GroupName
			, PropertyName, PropertyValue)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IRibbonToolbar(DispatchBaseClass):
	CLSID = IID('{0ADD5A11-7B77-4094-AF8A-540B28C73D71}')
	coclass_clsid = IID('{9A19345B-2F40-4A04-B171-6EB1385637BB}')

	def GetControlText(self, CtrlID=defaultNamedNotOptArg, pCtrlText=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(8, LCID, 1, (24, 0), ((8, 0), (16392, 0)),CtrlID
			, pCtrlText)

	def ReLoadMainAppExperimentList(self):
		return self._oleobj_.InvokeTypes(7, LCID, 1, (24, 0), (),)

	def RecalculateLayout(self):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), (),)

	def SetAddinRibbonMenuInfo(self, lSessionID=defaultNamedNotOptArg, InsertBeforeMainMenu=defaultNamedNotOptArg, MenuName=defaultNamedNotOptArg, CtrlID=defaultNamedNotOptArg
			, FunctionName=defaultNamedNotOptArg, Desc=defaultNamedNotOptArg, ShortcutKey=defaultNamedNotOptArg, MenuToolbarIndex=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), ((3, 0), (8, 0), (8, 0), (8, 0), (8, 0), (8, 0), (8, 0), (3, 0)),lSessionID
			, InsertBeforeMainMenu, MenuName, CtrlID, FunctionName, Desc
			, ShortcutKey, MenuToolbarIndex)

	def SetAddinRibbonToolbarButtonInfo(self, lSessionID=defaultNamedNotOptArg, TabName=defaultNamedNotOptArg, PanelGroupName=defaultNamedNotOptArg, MenuName=defaultNamedNotOptArg
			, ButtonName=defaultNamedNotOptArg, CtrlID=defaultNamedNotOptArg, FunctionName=defaultNamedNotOptArg, Desc=defaultNamedNotOptArg, ShortcutKey=defaultNamedNotOptArg
			, RibbonToolbarIndex=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((3, 0), (8, 0), (8, 0), (8, 0), (8, 0), (8, 0), (8, 0), (8, 0), (8, 0), (3, 0)),lSessionID
			, TabName, PanelGroupName, MenuName, ButtonName, CtrlID
			, FunctionName, Desc, ShortcutKey, RibbonToolbarIndex)

	def SetAddinRibbonToolbarInfo(self, lSessionID=defaultNamedNotOptArg, TabName=defaultNamedNotOptArg, PanelGroupName=defaultNamedNotOptArg, ControlType=defaultNamedNotOptArg
			, ControlName=defaultNamedNotOptArg, CtrlID=defaultNamedNotOptArg, ControlFunction=defaultNamedNotOptArg, Desc=defaultNamedNotOptArg, ShortcutKey=defaultNamedNotOptArg
			, RibbonToolbarIndex=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), ((3, 0), (8, 0), (8, 0), (3, 0), (8, 0), (8, 0), (8, 0), (8, 0), (8, 0), (3, 0)),lSessionID
			, TabName, PanelGroupName, ControlType, ControlName, CtrlID
			, ControlFunction, Desc, ShortcutKey, RibbonToolbarIndex)

	def SetControlText(self, CrlID=defaultNamedNotOptArg, CtrlText=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(6, LCID, 1, (24, 0), ((8, 0), (8, 0)),CrlID
			, CtrlText)

	_prop_map_get_ = {
		"Window": (5, 2, (12, 0), (), "Window", None),
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

class IRibbonToolbarEvents(DispatchBaseClass):
	CLSID = IID('{86A64F82-5E60-4D4D-A3D8-3B512805F847}')
	coclass_clsid = IID('{27609E1D-AB3E-4058-8574-E9CEE5577708}')

	def GetControlStatus(self, CtrlID=defaultNamedNotOptArg, pStatus=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(3, LCID, 1, (24, 0), ((8, 0), (16387, 0)),CtrlID
			, pStatus)

	def OnAfterAddinControlsLoad(self):
		return self._oleobj_.InvokeTypes(2, LCID, 1, (24, 0), (),)

	def OnBeforeAddinControlsLoad(self):
		return self._oleobj_.InvokeTypes(1, LCID, 1, (24, 0), (),)

	def RibbonWndProc(self, MsgID=defaultNamedNotOptArg, wParam=defaultNamedNotOptArg, lParam=defaultNamedNotOptArg):
		return self._oleobj_.InvokeTypes(4, LCID, 1, (24, 0), ((3, 0), (12, 0), (12, 0)),MsgID
			, wParam, lParam)

	_prop_map_get_ = {
	}
	_prop_map_put_ = {
	}
	def __iter__(self):
		"Return a Python iterator for this object"
		try:
			ob = self._oleobj_.InvokeTypes(-4,LCID,3,(13, 10),())
		except pythoncom.error:
			raise TypeError("This object does not support enumeration")
		return win32com.client.util.Iterator(ob, None)

from win32com.client import CoClassBaseClass
# This CoClass is known by the name 'EurekaSim.Addin.1'
class Addin(CoClassBaseClass): # A CoClass
	CLSID = IID('{70AFC313-A879-4012-812D-AA714AEF8C6F}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IAddin,
	]
	default_interface = IAddin

# This CoClass is known by the name 'EurekaSim.ApplicationChart.1'
class ApplicationChart(CoClassBaseClass): # A CoClass
	CLSID = IID('{9D8447A5-48EA-45D9-9946-F1153C1C4888}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IApplicationChart,
	]
	default_interface = IApplicationChart

# This CoClass is known by the name 'EurekaSim.ApplicationDocument.1'
class ApplicationDocument(CoClassBaseClass): # A CoClass
	CLSID = IID('{9025BDCC-4135-4453-99F8-6FF16C1822DC}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IApplicationDocument,
	]
	default_interface = IApplicationDocument

# This CoClass is known by the name 'EurekaSim.ApplicationDocumentEvents.1'
class ApplicationDocumentEvents(CoClassBaseClass): # A CoClass
	CLSID = IID('{41E563DF-22CA-4D51-B40A-EE3DCD62DB6B}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IApplicationDocumentEvents,
	]
	default_interface = IApplicationDocumentEvents

# This CoClass is known by the name 'EurekaSim.ApplicationSettings.1'
class ApplicationSettings(CoClassBaseClass): # A CoClass
	CLSID = IID('{8BCCE11F-D19F-4483-8737-EE54E5696D7F}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IApplicationSettings,
	]
	default_interface = IApplicationSettings

# This CoClass is known by the name 'EurekaSim.ApplicationView.1'
class ApplicationView(CoClassBaseClass): # A CoClass
	CLSID = IID('{7F4547B3-FC11-4E6E-AFDA-AD77BE3A65A8}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IApplicationView,
	]
	default_interface = IApplicationView

# This CoClass is known by the name 'EurekaSim.ApplicationViewEvents.1'
class ApplicationViewEvents(CoClassBaseClass): # A CoClass
	CLSID = IID('{75CCE2A4-4996-4C5E-ACF0-5779519C7A18}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IApplicationViewEvents,
	]
	default_interface = IApplicationViewEvents

# This CoClass is known by the name 'EurekaSim.Experiment.1'
class Experiment(CoClassBaseClass): # A CoClass
	CLSID = IID('{0F56F152-82CD-4B00-9A56-EAE622E22554}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IExperiment,
	]
	default_interface = IExperiment

# This CoClass is known by the name 'EurekaSim.ExperimentEvents.1'
class ExperimentEvents(CoClassBaseClass): # A CoClass
	CLSID = IID('{E9240855-9646-4564-971D-9958CB48CB5C}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IExperimentEvents,
	]
	default_interface = IExperimentEvents

# This CoClass is known by the name 'EurekaSim.ExperimentTreeView.1'
class ExperimentTreeView(CoClassBaseClass): # A CoClass
	CLSID = IID('{894BE66A-5355-47A5-852C-822C1829FF34}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IExperimentTreeView,
	]
	default_interface = IExperimentTreeView

# This CoClass is known by the name 'EurekaSim.ExperimentTreeViewEvents.1'
class ExperimentTreeViewEvents(CoClassBaseClass): # A CoClass
	CLSID = IID('{A219A39D-2A0F-4658-A810-8AA440B19852}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IExperimentTreeViewEvents,
	]
	default_interface = IExperimentTreeViewEvents

# This CoClass is known by the name 'EurekaSim.FileSettingsTreeView.1'
class FileSettingsTreeView(CoClassBaseClass): # A CoClass
	CLSID = IID('{1D8EDC8A-F049-4BA0-8BC5-955A82BFC601}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IFileSettingsTreeView,
	]
	default_interface = IFileSettingsTreeView

# This CoClass is known by the name 'EurekaSim.FileSettingsTreeViewEvents.1'
class FileSettingsTreeViewEvents(CoClassBaseClass): # A CoClass
	CLSID = IID('{25A65E7E-E249-425F-90BD-4BD61C727883}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IFileSettingsTreeViewEvents,
	]
	default_interface = IFileSettingsTreeViewEvents

# This CoClass is known by the name 'EurekaSim.GraphCtl.1'
class GraphCtl(CoClassBaseClass): # A CoClass
	CLSID = IID('{7D7E6BCD-0089-4DB9-B4E4-8FE903732413}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IGraphCtl,
	]
	default_interface = IGraphCtl

# This CoClass is known by the name 'EurekaSim.GraphCtrl2d.1'
class GraphCtrl2d(CoClassBaseClass): # A CoClass
	CLSID = IID('{40C1D140-3B42-46EF-848D-B1BA009692EF}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IGraphCtrl2d,
	]
	default_interface = IGraphCtrl2d

# This CoClass is known by the name 'EurekaSim.MainApplication.1'
class MainApplication(CoClassBaseClass): # A CoClass
	CLSID = IID('{36F4F7BF-6A25-47A4-A5DB-5C84B82811A1}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IMainApplication,
	]
	default_interface = IMainApplication

# This CoClass is known by the name 'EurekaSim.MainApplicationEvents.1'
class MainApplicationEvents(CoClassBaseClass): # A CoClass
	CLSID = IID('{D729E31D-CC69-48BF-AC5A-A5860DFD667B}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IMainApplicationEvents,
	]
	default_interface = IMainApplicationEvents

# This CoClass is known by the name 'EurekaSim.MainWindow.1'
class MainWindow(CoClassBaseClass): # A CoClass
	CLSID = IID('{005FD9DA-A89C-4D58-9F17-F1CAA02484F9}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IMainWindow,
	]
	default_interface = IMainWindow

# This CoClass is known by the name 'EurekaSim.MainWindowEvents.1'
class MainWindowEvents(CoClassBaseClass): # A CoClass
	CLSID = IID('{FF8F1BC0-4715-4EB8-ADC7-F40154565E33}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IMainWindowEvents,
	]
	default_interface = IMainWindowEvents

class OpenGLUtilView(CoClassBaseClass): # A CoClass
	CLSID = IID('{D37FFE74-89CB-4272-A00B-78AAE721793C}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IOpenGLUtilView,
	]
	default_interface = IOpenGLUtilView

class OpenGLUtilViewEvents(CoClassBaseClass): # A CoClass
	CLSID = IID('{E6DC451D-1949-4E5E-A9DD-B1B1BBD4E298}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IOpenGLUtilViewEvents,
	]
	default_interface = IOpenGLUtilViewEvents

# This CoClass is known by the name 'EurekaSim.OpenGLView.1'
class OpenGLView(CoClassBaseClass): # A CoClass
	CLSID = IID('{0234EA06-BACD-4930-8F94-11034FE5FCC1}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IOpenGLView,
	]
	default_interface = IOpenGLView

# This CoClass is known by the name 'EurekaSim.OpenGLViewEvents.1'
class OpenGLViewEvents(CoClassBaseClass): # A CoClass
	CLSID = IID('{C6C4739C-4E00-488F-8024-76F793261B17}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IOpenGLViewEvents,
	]
	default_interface = IOpenGLViewEvents

# This CoClass is known by the name 'EurekaSim.PropertyWindow.1'
class PropertyWindow(CoClassBaseClass): # A CoClass
	CLSID = IID('{C73BF8A4-E4DC-48E9-8A8A-68695D71C777}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IPropertyWindow,
	]
	default_interface = IPropertyWindow

# This CoClass is known by the name 'EurekaSim.PropertyWindowEvents.1'
class PropertyWindowEvents(CoClassBaseClass): # A CoClass
	CLSID = IID('{5ADFA2F2-A0F4-4EED-92C3-6E91013A1F64}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IPropertyWindowEvents,
	]
	default_interface = IPropertyWindowEvents

# This CoClass is known by the name 'EurekaSim.RibbonToolbar.1'
class RibbonToolbar(CoClassBaseClass): # A CoClass
	CLSID = IID('{9A19345B-2F40-4A04-B171-6EB1385637BB}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IRibbonToolbar,
	]
	default_interface = IRibbonToolbar

# This CoClass is known by the name 'EurekaSim.RibbonToolbarEvents.1'
class RibbonToolbarEvents(CoClassBaseClass): # A CoClass
	CLSID = IID('{27609E1D-AB3E-4058-8574-E9CEE5577708}')
	coclass_sources = [
	]
	coclass_interfaces = [
		IRibbonToolbarEvents,
	]
	default_interface = IRibbonToolbarEvents

IAddin_vtables_dispatch_ = 1
IAddin_vtables_ = [
	(( 'Initialize' , 'lSessionID' , 'pApp' , 'bFirstTime' , ), 1, (1, (), [ 
			 (3, 0, None, None) , (9, 0, None, "IID('{9E6C4518-AF98-4654-B294-709D0440698B}')") , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'Uninitialize' , 'lSessionID' , ), 2, (2, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'About' , ), 3, (3, (), [ ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
]

IApplicationChart_vtables_dispatch_ = 1
IApplicationChart_vtables_ = [
	(( 'ChartWindowMode' , 'pVal' , ), 1, (1, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'ChartWindowMode' , 'pVal' , ), 1, (1, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'DeleteAllCharts' , ), 2, (2, (), [ ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'Initialize2dChart' , 'NoOfCharts' , ), 3, (3, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'Set2dGraphInfo' , 'GraphIndex' , 'GraphTitle' , 'GraphXAxisTitle' , 'GraphYAxisTitle' , 
			 'ShowGraph' , ), 4, (4, (), [ (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , 
			 (8, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'ResizeChartWindow' , ), 5, (5, (), [ ], 1 , 1 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'Set2dChartData' , 'Index' , 'DataArray' , ), 6, (6, (), [ (3, 0, None, None) , 
			 (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
	(( 'Set2dAxisRange' , 'GraphIndex' , 'AxisType' , 'MinValue' , 'MaxValue' , 
			 ), 7, (7, (), [ (3, 0, None, None) , (3, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 112 , (3, 0, None, None) , 0 , )),
	(( 'Initialize3dChart' , 'NoOfCharts' , ), 8, (8, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 120 , (3, 0, None, None) , 0 , )),
	(( 'InitializeChart' , 'NoOfCharts' , 'pChartTypeArray' , ), 9, (9, (), [ (3, 0, None, None) , 
			 (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 128 , (3, 0, None, None) , 0 , )),
	(( 'GraphCtrl3dObj' , 'Index' , 'pVal' , ), 10, (10, (), [ (3, 0, None, None) , 
			 (16393, 10, None, None) , ], 1 , 2 , 4 , 0 , 136 , (3, 0, None, None) , 0 , )),
	(( 'GraphCtrl2dObj' , 'Index' , 'pVal' , ), 11, (11, (), [ (3, 0, None, None) , 
			 (16393, 10, None, None) , ], 1 , 2 , 4 , 0 , 144 , (3, 0, None, None) , 0 , )),
]

IApplicationDocument_vtables_dispatch_ = 1
IApplicationDocument_vtables_ = [
	(( 'SetAddinSettingsAsString' , 'PluginName' , 'Key' , 'Value' , ), 1, (1, (), [ 
			 (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'GetAddinSettingsAsString' , 'PluginName' , 'Key' , 'Value' , ), 2, (2, (), [ 
			 (8, 0, None, None) , (8, 0, None, None) , (16392, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'OpenDocument' , 'FilePath' , ), 3, (3, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'CloseActiveDocument' , ), 4, (4, (), [ ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'LaunchNewDocument' , ), 5, (5, (), [ ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'LogToCSVFileStatus' , 'pVal' , ), 6, (6, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'LogToCSVFileStatus' , 'pVal' , ), 6, (6, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
	(( 'DisplayRealTimeGraphStatus' , 'pVal' , ), 7, (7, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 112 , (3, 0, None, None) , 0 , )),
	(( 'DisplayRealTimeGraphStatus' , 'pVal' , ), 7, (7, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 120 , (3, 0, None, None) , 0 , )),
	(( 'RecordSimulationAsVideoStatus' , 'pVal' , ), 8, (8, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 128 , (3, 0, None, None) , 0 , )),
	(( 'RecordSimulationAsVideoStatus' , 'pVal' , ), 8, (8, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 136 , (3, 0, None, None) , 0 , )),
	(( 'DisplayExpParamStatus' , 'pVal' , ), 9, (9, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 144 , (3, 0, None, None) , 0 , )),
	(( 'DisplayExpParamStatus' , 'pVal' , ), 9, (9, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 152 , (3, 0, None, None) , 0 , )),
	(( 'VisualizationMode' , 'pVal' , ), 10, (10, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 160 , (3, 0, None, None) , 0 , )),
	(( 'VisualizationMode' , 'pVal' , ), 10, (10, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 168 , (3, 0, None, None) , 0 , )),
	(( 'VisualizationMode3D' , 'pVal' , ), 11, (11, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 176 , (3, 0, None, None) , 0 , )),
	(( 'VisualizationMode3D' , 'pVal' , ), 11, (11, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 184 , (3, 0, None, None) , 0 , )),
	(( 'EnableVisualizationMode' , 'pVal' , ), 12, (12, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 192 , (3, 0, None, None) , 0 , )),
	(( 'EnableVisualizationMode' , 'pVal' , ), 12, (12, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 200 , (3, 0, None, None) , 0 , )),
	(( 'ShowGraphInMainWindow' , 'pVal' , ), 13, (13, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 208 , (3, 0, None, None) , 0 , )),
	(( 'ShowGraphInMainWindow' , 'pVal' , ), 13, (13, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 216 , (3, 0, None, None) , 0 , )),
]

IApplicationDocumentEvents_vtables_dispatch_ = 1
IApplicationDocumentEvents_vtables_ = [
	(( 'OnNewDocument' , 'DocumentName' , ), 1, (1, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'OnDocumentOpened' , 'DocumentPath' , ), 2, (2, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'OnCloseDocument' , 'DocumentPath' , ), 3, (3, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'OnBeforeSaveDocument' , 'DocumentPath' , ), 4, (4, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'OnAfterSaveDocument' , 'DocumentPath' , ), 5, (5, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
]

IApplicationSettings_vtables_dispatch_ = 1
IApplicationSettings_vtables_ = [
]

IApplicationView_vtables_dispatch_ = 1
IApplicationView_vtables_ = [
	(( 'Refresh' , ), 1, (1, (), [ ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'ResetScene' , ), 2, (2, (), [ ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'UpdateDisplay' , ), 3, (3, (), [ ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'InitailizeDisplayLists' , ), 4, (4, (), [ ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'StartStockDListDef' , ), 5, (5, (), [ ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'EndStockListDef' , ), 6, (6, (), [ ], 1 , 1 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'BeginDraw' , 'Mode' , ), 7, (7, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
	(( 'EndDraw' , ), 8, (8, (), [ ], 1 , 1 , 4 , 0 , 112 , (3, 0, None, None) , 0 , )),
	(( 'SetColorf' , 'red' , 'green' , 'blue' , ), 9, (9, (), [ 
			 (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 120 , (3, 0, None, None) , 0 , )),
	(( 'SetVertexf' , 'x' , 'y' , 'z' , ), 10, (10, (), [ 
			 (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 128 , (3, 0, None, None) , 0 , )),
	(( 'ClearStockDispLists' , ), 11, (11, (), [ ], 1 , 1 , 4 , 0 , 136 , (3, 0, None, None) , 0 , )),
	(( 'BeginGraphicsCommands' , ), 12, (12, (), [ ], 1 , 1 , 4 , 0 , 144 , (3, 0, None, None) , 0 , )),
	(( 'EndGraphicsCommands' , ), 13, (13, (), [ ], 1 , 1 , 4 , 0 , 152 , (3, 0, None, None) , 0 , )),
	(( 'StartNewDisplayList' , ), 14, (14, (), [ ], 1 , 1 , 4 , 0 , 160 , (3, 0, None, None) , 0 , )),
	(( 'EndNewDisplayList' , ), 15, (15, (), [ ], 1 , 1 , 4 , 0 , 168 , (3, 0, None, None) , 0 , )),
	(( 'TranslateObject' , 'x' , 'y' , 'z' , ), 16, (16, (), [ 
			 (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 176 , (3, 0, None, None) , 0 , )),
	(( 'RotateObject' , 'Angle' , 'x' , 'y' , 'z' , 
			 ), 17, (17, (), [ (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 184 , (3, 0, None, None) , 0 , )),
	(( 'DrawSphere' , 'Radius' , 'LogitudeSubdiv' , 'LatitudeSubdiv' , ), 18, (18, (), [ 
			 (5, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 192 , (3, 0, None, None) , 0 , )),
	(( 'DrawCylinder' , 'baseRadius' , 'topRadius' , 'height' , 'slices' , 
			 'stacks' , ), 19, (19, (), [ (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 200 , (3, 0, None, None) , 0 , )),
	(( 'DrawDisk' , 'innerRadius' , 'outerRadius' , 'slices' , 'loops' , 
			 ), 20, (20, (), [ (5, 0, None, None) , (5, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 208 , (3, 0, None, None) , 0 , )),
	(( 'DrawPartialDisk' , 'innerRadius' , 'outerRadius' , 'slices' , 'loops' , 
			 'startAngle' , 'sweepAngle' , ), 21, (21, (), [ (5, 0, None, None) , (5, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 216 , (3, 0, None, None) , 0 , )),
	(( 'UpdatePredefinedScene' , ), 22, (22, (), [ ], 1 , 1 , 4 , 0 , 224 , (3, 0, None, None) , 0 , )),
	(( 'SetLineWidth' , 'Width' , ), 23, (23, (), [ (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 232 , (3, 0, None, None) , 0 , )),
	(( 'Set2DVertexf' , 'x' , 'y' , ), 24, (24, (), [ (4, 0, None, None) , 
			 (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 240 , (3, 0, None, None) , 0 , )),
	(( 'DrawRectangle' , 'x1' , 'y1' , 'x2' , 'y2' , 
			 'bFill' , ), 25, (25, (), [ (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , 
			 (4, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 248 , (3, 0, None, None) , 0 , )),
	(( 'Draw2DLine' , 'x1' , 'y1' , 'x2' , 'y2' , 
			 ), 26, (26, (), [ (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 256 , (3, 0, None, None) , 0 , )),
	(( 'Draw3DLine' , 'x1' , 'y1' , 'z1' , 'x2' , 
			 'y2' , 'z2' , ), 27, (27, (), [ (4, 0, None, None) , (4, 0, None, None) , 
			 (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 264 , (3, 0, None, None) , 0 , )),
	(( 'DrawPolygon' , 'xAxisArray' , 'yAxisArray' , 'ArrayCount' , 'bFill' , 
			 ), 28, (28, (), [ (16388, 0, None, None) , (16388, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 272 , (3, 0, None, None) , 0 , )),
	(( 'InitializeEnvironment' , 'bShowAxis' , ), 29, (29, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 280 , (3, 0, None, None) , 0 , )),
	(( 'EnableOwnerDraw' , 'pVal' , ), 30, (30, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 288 , (3, 0, None, None) , 0 , )),
	(( 'EnableOwnerDraw' , 'pVal' , ), 30, (30, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 296 , (3, 0, None, None) , 0 , )),
	(( 'Initialize' , ), 31, (31, (), [ ], 1 , 1 , 4 , 0 , 304 , (3, 0, None, None) , 0 , )),
	(( 'PushMatrix' , ), 32, (32, (), [ ], 1 , 1 , 4 , 0 , 312 , (3, 0, None, None) , 0 , )),
	(( 'PopMatrix' , ), 33, (33, (), [ ], 1 , 1 , 4 , 0 , 320 , (3, 0, None, None) , 0 , )),
	(( 'DrawPredefinedQuadrics' , ), 34, (34, (), [ ], 1 , 1 , 4 , 0 , 328 , (3, 0, None, None) , 0 , )),
	(( 'IssueRotation' , ), 35, (35, (), [ ], 1 , 1 , 4 , 0 , 336 , (3, 0, None, None) , 0 , )),
	(( 'DrawRotatedObject' , ), 36, (36, (), [ ], 1 , 1 , 4 , 0 , 344 , (3, 0, None, None) , 0 , )),
	(( 'SetViewPort' , 'x' , 'y' , 'Width' , 'height' , 
			 ), 37, (37, (), [ (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 352 , (3, 0, None, None) , 0 , )),
	(( 'PushAttrribute' , 'Mask' , ), 38, (38, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 360 , (3, 0, None, None) , 0 , )),
	(( 'PopAttrribute' , ), 39, (39, (), [ ], 1 , 1 , 4 , 0 , 368 , (3, 0, None, None) , 0 , )),
	(( 'Enable' , 'Capability' , ), 40, (40, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 376 , (3, 0, None, None) , 0 , )),
	(( 'Disable' , 'Capability' , ), 41, (41, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 384 , (3, 0, None, None) , 0 , )),
	(( 'DrawStockDispLists' , ), 42, (42, (), [ ], 1 , 1 , 4 , 0 , 392 , (3, 0, None, None) , 0 , )),
	(( 'GetClientRectInfo' , 'Left' , 'Top' , 'Right' , 'Bottom' , 
			 ), 43, (43, (), [ (16387, 0, None, None) , (16387, 0, None, None) , (16387, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 400 , (3, 0, None, None) , 0 , )),
	(( 'ClientRectWidth' , 'Width' , ), 44, (44, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 408 , (3, 0, None, None) , 0 , )),
	(( 'ClientRectHeight' , 'height' , ), 45, (45, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 416 , (3, 0, None, None) , 0 , )),
	(( 'SetBkgColor' , 'red' , 'green' , 'blue' , 'Alpha' , 
			 ), 46, (46, (), [ (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 424 , (3, 0, None, None) , 0 , )),
	(( 'SetDepth' , 'Depth' , ), 47, (47, (), [ (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 432 , (3, 0, None, None) , 0 , )),
	(( 'SetLightInfo' , 'Light' , 'PName' , 'Params' , ), 48, (48, (), [ 
			 (3, 0, None, None) , (3, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 440 , (3, 0, None, None) , 0 , )),
	(( 'SetFontInfo' , 'FontName' , 'height' , 'bBold' , 'bItalic' , 
			 ), 49, (49, (), [ (8, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 448 , (3, 0, None, None) , 0 , )),
	(( 'RunScript' , 'Script' , 'pResult' , ), 50, (50, (), [ (8, 0, None, None) , 
			 (16392, 0, None, None) , ], 1 , 1 , 4 , 0 , 456 , (3, 0, None, None) , 0 , )),
	(( 'RunScriptEx' , 'Script' , 'ScriptType' , 'pResult' , ), 51, (51, (), [ 
			 (8, 0, None, None) , (3, 0, None, None) , (16392, 0, None, None) , ], 1 , 1 , 4 , 0 , 464 , (3, 0, None, None) , 0 , )),
	(( 'LaunchManageScriptDialog' , ), 52, (52, (), [ ], 1 , 1 , 4 , 0 , 472 , (3, 0, None, None) , 0 , )),
]

IApplicationViewEvents_vtables_dispatch_ = 1
IApplicationViewEvents_vtables_ = [
	(( 'OnDrawSimulation' , ), 1, (1, (), [ ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'OnInitializeSimulation' , 'b3DMode' , 'VisualizationMode' , 'Experiment' , ), 2, (2, (), [ 
			 (3, 0, None, None) , (3, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'OnDrawPredefinedScene' , 'Experiment' , ), 3, (3, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'OnOwnerDrawSimulation' , ), 4, (4, (), [ ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'OnOwnerDrawCreate' , ), 5, (5, (), [ ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'ViewWndProc' , 'MsgID' , 'wParam' , 'lParam' , ), 6, (6, (), [ 
			 (3, 0, None, None) , (12, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'OnActivateView' , 'bActivate' , 'CurrentViewFilePath' , 'PreviousViewFilePath' , ), 7, (7, (), [ 
			 (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
]

IExperiment_vtables_dispatch_ = 1
IExperiment_vtables_ = [
	(( 'AddExperiment' , 'SessionID' , 'ExperimentName' , ), 1, (1, (), [ (3, 0, None, None) , 
			 (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'WriteCSVLogFileHeaderInfo' , 'HeaderInfo' , ), 2, (2, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'WriteToCSVLogFile' , 'Info' , ), 3, (3, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'LogFilePath' , 'pVal' , ), 4, (4, (), [ (16392, 10, None, None) , ], 1 , 2 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'OpenLogFile' , 'FilePath' , ), 5, (5, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'CloseLogFile' , ), 6, (6, (), [ ], 1 , 1 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'ClearLogFileContents' , ), 7, (7, (), [ ], 1 , 1 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
	(( 'SimulationState' , 'pVal' , ), 8, (8, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 112 , (3, 0, None, None) , 0 , )),
	(( 'StartSimulation' , ), 9, (9, (), [ ], 1 , 1 , 4 , 0 , 120 , (3, 0, None, None) , 0 , )),
	(( 'StopSimulation' , ), 10, (10, (), [ ], 1 , 1 , 4 , 0 , 128 , (3, 0, None, None) , 0 , )),
	(( 'GetSelectedExperiment' , 'pExperimentName' , 'pAddinSesssionID' , ), 11, (11, (), [ (16392, 0, None, None) , 
			 (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 136 , (3, 0, None, None) , 0 , )),
]

IExperimentEvents_vtables_dispatch_ = 1
IExperimentEvents_vtables_ = [
	(( 'OnStartSimulation' , 'ExperimentName' , ), 1, (1, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'OnStopSimulation' , 'ExperimentName' , ), 2, (2, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'OnStatusChange' , 'StatusCode' , 'StatusDesc' , 'AdditionalParam' , ), 3, (3, (), [ 
			 (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'OnError' , 'ErrorCode' , 'ErrorDesc' , 'AdditionalParam' , ), 4, (4, (), [ 
			 (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'OnInitializeLogFileInfo' , 'ExperimentName' , ), 5, (5, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'OnInitializeSimulationGraph' , 'ExperimentName' , ), 6, (6, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'OnInitializeSimulationVideoRecording' , 'ExperimentName' , ), 7, (7, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
]

IExperimentTreeView_vtables_dispatch_ = 1
IExperimentTreeView_vtables_ = [
	(( 'DeleteAllExperiments' , 'SessionID' , ), 1, (1, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'AddExperiment' , 'SessionID' , 'ExperimentGroup' , 'ExperimentName' , ), 2, (2, (), [ 
			 (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'DeleteExperiment' , 'SessionID' , 'ExperimentGroup' , 'ExperimentName' , ), 3, (3, (), [ 
			 (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'Refresh' , ), 4, (4, (), [ ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'Show' , 'bShow' , ), 5, (5, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'SetTreeGroupState' , 'ExperimentGroup' , 'TreeState' , ), 6, (6, (), [ (8, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'DeleteExperimentGroup' , 'SessionID' , 'GroupName' , ), 7, (7, (), [ (3, 0, None, None) , 
			 (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
	(( 'SetRootNodeName' , 'RootNodeName' , 'bRedraw' , ), 8, (8, (), [ (8, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 112 , (3, 0, None, None) , 0 , )),
	(( 'SelectActiveExperiment' , 'SessionID' , 'ExperimentGroup' , 'ExperimentName' , ), 9, (9, (), [ 
			 (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 120 , (3, 0, None, None) , 0 , )),
]

IExperimentTreeViewEvents_vtables_dispatch_ = 1
IExperimentTreeViewEvents_vtables_ = [
	(( 'OnTreeNodeSelect' , 'SessionID' , 'RootText' , 'ExperimentGroup' , 'ExperimentName' , 
			 ), 1, (1, (), [ (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'OnTreeNodeDblClick' , 'SessionID' , 'RootText' , 'ExperimentGroup' , 'ExperimentName' , 
			 ), 2, (2, (), [ (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'OnReloadExperiment' , 'SessionID' , 'RootText' , 'ExperimentGroup' , 'ExperimentName' , 
			 ), 3, (3, (), [ (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
]

IFileSettingsTreeView_vtables_dispatch_ = 1
IFileSettingsTreeView_vtables_ = [
	(( 'DeleteAllSnapshots' , 'FilePath' , ), 1, (1, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'AddSnapshot' , 'FilePath' , 'GroupName' , 'SnapshotName' , ), 2, (2, (), [ 
			 (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'DeleteSnapshot' , 'FilePath' , 'GroupName' , 'SnapshotName' , ), 3, (3, (), [ 
			 (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'ReloadAllSnapshots' , 'FilePath' , ), 4, (4, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'Show' , 'bShow' , ), 5, (5, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'SetTreeGroupState' , 'GroupName' , 'TreeState' , ), 6, (6, (), [ (8, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'SetRootNodeName' , 'RootNodeName' , 'bRedraw' , ), 7, (7, (), [ (8, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
]

IFileSettingsTreeViewEvents_vtables_dispatch_ = 1
IFileSettingsTreeViewEvents_vtables_ = [
	(( 'OnActivateSnapshot' , 'FilePath' , 'GroupName' , 'SnapshotName' , ), 1, (1, (), [ 
			 (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'OnAddSnapshot' , 'FilePath' , 'GroupName' , 'SnapshotName' , ), 2, (2, (), [ 
			 (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'OnDeleteSnapshot' , 'FilePath' , 'GroupName' , 'SnapshotName' , ), 3, (3, (), [ 
			 (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'OnDeleteAllSnapshot' , 'FilePath' , ), 4, (4, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
]

IGraphCtl_vtables_dispatch_ = 1
IGraphCtl_vtables_ = [
	(( 'CaptionColor' , 'pVal' , ), 1, (1, (), [ (16403, 10, None, None) , ], 1 , 2 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'CaptionColor' , 'pVal' , ), 1, (1, (), [ (19, 1, None, None) , ], 1 , 4 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'SetRange' , 'xmin' , 'xmax' , 'ymin' , 'ymax' , 
			 'zmin' , 'zmax' , ), 2, (2, (), [ (5, 0, None, None) , (5, 0, None, None) , 
			 (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'AutoRange' , ), 3, (3, (), [ ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'ShowPropertyPages' , ), 4, (4, (), [ ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'TrackMode' , 'pVal' , ), 5, (5, (), [ (16386, 10, None, None) , ], 1 , 2 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'TrackMode' , 'pVal' , ), 5, (5, (), [ (2, 1, None, None) , ], 1 , 4 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
	(( 'Projection' , 'pVal' , ), 6, (6, (), [ (16386, 10, None, None) , ], 1 , 2 , 4 , 0 , 112 , (3, 0, None, None) , 0 , )),
	(( 'Projection' , 'pVal' , ), 6, (6, (), [ (2, 1, None, None) , ], 1 , 4 , 4 , 0 , 120 , (3, 0, None, None) , 0 , )),
	(( 'XLabel' , 'pVal' , ), 7, (7, (), [ (16392, 10, None, None) , ], 1 , 2 , 4 , 0 , 128 , (3, 0, None, None) , 0 , )),
	(( 'XLabel' , 'pVal' , ), 7, (7, (), [ (8, 1, None, None) , ], 1 , 4 , 4 , 0 , 136 , (3, 0, None, None) , 0 , )),
	(( 'YLabel' , 'pVal' , ), 8, (8, (), [ (16392, 10, None, None) , ], 1 , 2 , 4 , 0 , 144 , (3, 0, None, None) , 0 , )),
	(( 'YLabel' , 'pVal' , ), 8, (8, (), [ (8, 1, None, None) , ], 1 , 4 , 4 , 0 , 152 , (3, 0, None, None) , 0 , )),
	(( 'ZLabel' , 'pVal' , ), 9, (9, (), [ (16392, 10, None, None) , ], 1 , 2 , 4 , 0 , 160 , (3, 0, None, None) , 0 , )),
	(( 'ZLabel' , 'pVal' , ), 9, (9, (), [ (8, 1, None, None) , ], 1 , 4 , 4 , 0 , 168 , (3, 0, None, None) , 0 , )),
	(( 'XGridNumber' , 'pVal' , ), 10, (10, (), [ (16386, 10, None, None) , ], 1 , 2 , 4 , 0 , 176 , (3, 0, None, None) , 0 , )),
	(( 'XGridNumber' , 'pVal' , ), 10, (10, (), [ (2, 1, None, None) , ], 1 , 4 , 4 , 0 , 184 , (3, 0, None, None) , 0 , )),
	(( 'YGridNumber' , 'pVal' , ), 11, (11, (), [ (16386, 10, None, None) , ], 1 , 2 , 4 , 0 , 192 , (3, 0, None, None) , 0 , )),
	(( 'YGridNumber' , 'pVal' , ), 11, (11, (), [ (2, 1, None, None) , ], 1 , 4 , 4 , 0 , 200 , (3, 0, None, None) , 0 , )),
	(( 'ZGridNumber' , 'pVal' , ), 12, (12, (), [ (16386, 10, None, None) , ], 1 , 2 , 4 , 0 , 208 , (3, 0, None, None) , 0 , )),
	(( 'ZGridNumber' , 'pVal' , ), 12, (12, (), [ (2, 1, None, None) , ], 1 , 4 , 4 , 0 , 216 , (3, 0, None, None) , 0 , )),
	(( 'XGridColor' , 'pVal' , ), 13, (13, (), [ (16403, 10, None, None) , ], 1 , 2 , 4 , 0 , 224 , (3, 0, None, None) , 0 , )),
	(( 'XGridColor' , 'pVal' , ), 13, (13, (), [ (19, 1, None, None) , ], 1 , 4 , 4 , 0 , 232 , (3, 0, None, None) , 0 , )),
	(( 'YGridColor' , 'pVal' , ), 14, (14, (), [ (16403, 10, None, None) , ], 1 , 2 , 4 , 0 , 240 , (3, 0, None, None) , 0 , )),
	(( 'YGridColor' , 'pVal' , ), 14, (14, (), [ (19, 1, None, None) , ], 1 , 4 , 4 , 0 , 248 , (3, 0, None, None) , 0 , )),
	(( 'ZGridColor' , 'pVal' , ), 15, (15, (), [ (16403, 10, None, None) , ], 1 , 2 , 4 , 0 , 256 , (3, 0, None, None) , 0 , )),
	(( 'ZGridColor' , 'pVal' , ), 15, (15, (), [ (19, 1, None, None) , ], 1 , 4 , 4 , 0 , 264 , (3, 0, None, None) , 0 , )),
	(( 'AddElement' , ), 16, (16, (), [ ], 1 , 1 , 4 , 0 , 272 , (3, 0, None, None) , 0 , )),
	(( 'DeleteElement' , 'ElementID' , ), 17, (17, (), [ (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 280 , (3, 0, None, None) , 0 , )),
	(( 'ClearGraph' , ), 18, (18, (), [ ], 1 , 1 , 4 , 0 , 288 , (3, 0, None, None) , 0 , )),
	(( 'ElementLineColor' , 'ElementID' , 'pVal' , ), 19, (19, (), [ (2, 0, None, None) , 
			 (16403, 10, None, None) , ], 1 , 2 , 4 , 0 , 296 , (3, 0, None, None) , 0 , )),
	(( 'ElementLineColor' , 'ElementID' , 'pVal' , ), 19, (19, (), [ (2, 0, None, None) , 
			 (19, 1, None, None) , ], 1 , 4 , 4 , 0 , 304 , (3, 0, None, None) , 0 , )),
	(( 'ElementPointColor' , 'ElementID' , 'pVal' , ), 20, (20, (), [ (2, 0, None, None) , 
			 (16403, 10, None, None) , ], 1 , 2 , 4 , 0 , 312 , (3, 0, None, None) , 0 , )),
	(( 'ElementPointColor' , 'ElementID' , 'pVal' , ), 20, (20, (), [ (2, 0, None, None) , 
			 (19, 1, None, None) , ], 1 , 4 , 4 , 0 , 320 , (3, 0, None, None) , 0 , )),
	(( 'ElementLineWidth' , 'ElementID' , 'pVal' , ), 21, (21, (), [ (2, 0, None, None) , 
			 (16388, 10, None, None) , ], 1 , 2 , 4 , 0 , 328 , (3, 0, None, None) , 0 , )),
	(( 'ElementLineWidth' , 'ElementID' , 'pVal' , ), 21, (21, (), [ (2, 0, None, None) , 
			 (4, 1, None, None) , ], 1 , 4 , 4 , 0 , 336 , (3, 0, None, None) , 0 , )),
	(( 'ElementPointSize' , 'ElementID' , 'pVal' , ), 22, (22, (), [ (2, 0, None, None) , 
			 (16388, 10, None, None) , ], 1 , 2 , 4 , 0 , 344 , (3, 0, None, None) , 0 , )),
	(( 'ElementPointSize' , 'ElementID' , 'pVal' , ), 22, (22, (), [ (2, 0, None, None) , 
			 (4, 1, None, None) , ], 1 , 4 , 4 , 0 , 352 , (3, 0, None, None) , 0 , )),
	(( 'ElementType' , 'ElementID' , 'pVal' , ), 23, (23, (), [ (2, 0, None, None) , 
			 (16386, 10, None, None) , ], 1 , 2 , 4 , 0 , 360 , (3, 0, None, None) , 0 , )),
	(( 'ElementType' , 'ElementID' , 'pVal' , ), 23, (23, (), [ (2, 0, None, None) , 
			 (2, 1, None, None) , ], 1 , 4 , 4 , 0 , 368 , (3, 0, None, None) , 0 , )),
	(( 'PlotXYZ' , 'x' , 'y' , 'z' , 'ElementID' , 
			 ), 24, (24, (), [ (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 376 , (3, 0, None, None) , 0 , )),
	(( 'ElementShow' , 'ElementID' , 'pVal' , ), 25, (25, (), [ (2, 0, None, None) , 
			 (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 384 , (3, 0, None, None) , 0 , )),
	(( 'ElementShow' , 'ElementID' , 'pVal' , ), 25, (25, (), [ (2, 0, None, None) , 
			 (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 392 , (3, 0, None, None) , 0 , )),
	(( 'ElementSurfaceFill' , 'ElementID' , 'pVal' , ), 26, (26, (), [ (2, 0, None, None) , 
			 (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 400 , (3, 0, None, None) , 0 , )),
	(( 'ElementSurfaceFill' , 'ElementID' , 'pVal' , ), 26, (26, (), [ (2, 0, None, None) , 
			 (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 408 , (3, 0, None, None) , 0 , )),
	(( 'ElementSurfaceFlat' , 'ElementID' , 'pVal' , ), 27, (27, (), [ (2, 0, None, None) , 
			 (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 416 , (3, 0, None, None) , 0 , )),
	(( 'ElementSurfaceFlat' , 'ElementID' , 'pVal' , ), 27, (27, (), [ (2, 0, None, None) , 
			 (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 424 , (3, 0, None, None) , 0 , )),
	(( 'ElementLight' , 'ElementID' , 'pVal' , ), 28, (28, (), [ (2, 0, None, None) , 
			 (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 432 , (3, 0, None, None) , 0 , )),
	(( 'ElementLight' , 'ElementID' , 'pVal' , ), 28, (28, (), [ (2, 0, None, None) , 
			 (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 440 , (3, 0, None, None) , 0 , )),
	(( 'ElementLightingAmbient' , 'ElementID' , 'pVal' , ), 29, (29, (), [ (2, 0, None, None) , 
			 (16386, 10, None, None) , ], 1 , 2 , 4 , 0 , 448 , (3, 0, None, None) , 0 , )),
	(( 'ElementLightingAmbient' , 'ElementID' , 'pVal' , ), 29, (29, (), [ (2, 0, None, None) , 
			 (2, 1, None, None) , ], 1 , 4 , 4 , 0 , 456 , (3, 0, None, None) , 0 , )),
	(( 'ElementLightingDiffuse' , 'ElementID' , 'pVal' , ), 30, (30, (), [ (2, 0, None, None) , 
			 (16386, 10, None, None) , ], 1 , 2 , 4 , 0 , 464 , (3, 0, None, None) , 0 , )),
	(( 'ElementLightingDiffuse' , 'ElementID' , 'pVal' , ), 30, (30, (), [ (2, 0, None, None) , 
			 (2, 1, None, None) , ], 1 , 4 , 4 , 0 , 472 , (3, 0, None, None) , 0 , )),
	(( 'ElementLightingSpecular' , 'ElementID' , 'pVal' , ), 31, (31, (), [ (2, 0, None, None) , 
			 (16386, 10, None, None) , ], 1 , 2 , 4 , 0 , 480 , (3, 0, None, None) , 0 , )),
	(( 'ElementLightingSpecular' , 'ElementID' , 'pVal' , ), 31, (31, (), [ (2, 0, None, None) , 
			 (2, 1, None, None) , ], 1 , 4 , 4 , 0 , 488 , (3, 0, None, None) , 0 , )),
	(( 'ElementMaterialAmbient' , 'ElementID' , 'pVal' , ), 32, (32, (), [ (2, 0, None, None) , 
			 (16386, 10, None, None) , ], 1 , 2 , 4 , 0 , 496 , (3, 0, None, None) , 0 , )),
	(( 'ElementMaterialAmbient' , 'ElementID' , 'pVal' , ), 32, (32, (), [ (2, 0, None, None) , 
			 (2, 1, None, None) , ], 1 , 4 , 4 , 0 , 504 , (3, 0, None, None) , 0 , )),
	(( 'ElementMaterialDiffuse' , 'ElementID' , 'pVal' , ), 33, (33, (), [ (2, 0, None, None) , 
			 (16386, 10, None, None) , ], 1 , 2 , 4 , 0 , 512 , (3, 0, None, None) , 0 , )),
	(( 'ElementMaterialDiffuse' , 'ElementID' , 'pVal' , ), 33, (33, (), [ (2, 0, None, None) , 
			 (2, 1, None, None) , ], 1 , 4 , 4 , 0 , 520 , (3, 0, None, None) , 0 , )),
	(( 'ElementMaterialSpecular' , 'ElementID' , 'pVal' , ), 34, (34, (), [ (2, 0, None, None) , 
			 (16386, 10, None, None) , ], 1 , 2 , 4 , 0 , 528 , (3, 0, None, None) , 0 , )),
	(( 'ElementMaterialSpecular' , 'ElementID' , 'pVal' , ), 34, (34, (), [ (2, 0, None, None) , 
			 (2, 1, None, None) , ], 1 , 4 , 4 , 0 , 536 , (3, 0, None, None) , 0 , )),
	(( 'ElementMaterialShinines' , 'ElementID' , 'pVal' , ), 35, (35, (), [ (2, 0, None, None) , 
			 (16386, 10, None, None) , ], 1 , 2 , 4 , 0 , 544 , (3, 0, None, None) , 0 , )),
	(( 'ElementMaterialShinines' , 'ElementID' , 'pVal' , ), 35, (35, (), [ (2, 0, None, None) , 
			 (2, 1, None, None) , ], 1 , 4 , 4 , 0 , 552 , (3, 0, None, None) , 0 , )),
	(( 'ElementMaterialEmission' , 'ElementID' , 'pVal' , ), 36, (36, (), [ (2, 0, None, None) , 
			 (16386, 10, None, None) , ], 1 , 2 , 4 , 0 , 560 , (3, 0, None, None) , 0 , )),
	(( 'ElementMaterialEmission' , 'ElementID' , 'pVal' , ), 36, (36, (), [ (2, 0, None, None) , 
			 (2, 1, None, None) , ], 1 , 4 , 4 , 0 , 568 , (3, 0, None, None) , 0 , )),
	(( 'SetLightCoordinates' , 'ElementID' , 'x' , 'y' , 'z' , 
			 ), 37, (37, (), [ (2, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 576 , (3, 0, None, None) , 0 , )),
	(( 'CopyToClipboard' , ), 38, (38, (), [ ], 1 , 1 , 4 , 0 , 584 , (3, 0, None, None) , 0 , )),
	(( 'Lighting' , 'ElementID' , 'pVal' , ), 39, (39, (), [ (2, 0, None, None) , 
			 (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 592 , (3, 0, None, None) , 0 , )),
	(( 'Lighting' , 'ElementID' , 'pVal' , ), 39, (39, (), [ (2, 0, None, None) , 
			 (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 600 , (3, 0, None, None) , 0 , )),
	(( 'GraphCtrlObj' , 'pVal' , ), 40, (40, (), [ (16393, 10, None, None) , ], 1 , 2 , 4 , 0 , 608 , (3, 0, None, None) , 0 , )),
	(( 'OnPrint' , 'pDC' , 'pPrintInfo' , ), 41, (41, (), [ (12, 0, None, None) , 
			 (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 616 , (3, 0, None, None) , 0 , )),
	(( 'Print' , 'Title' , ), 42, (42, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 624 , (3, 0, None, None) , 0 , )),
]

IGraphCtrl2d_vtables_dispatch_ = 1
IGraphCtrl2d_vtables_ = [
	(( 'Set2dGraphInfo' , 'GraphTitle' , 'XAxisGraphTitle' , 'YAxisGraphTitle' , 'bShow' , 
			 ), 1, (1, (), [ (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'Set2dChartData' , 'GraphData' , ), 2, (2, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'Set2dAxisRange' , 'AxisType' , 'MinValue' , 'MaxValue' , ), 3, (3, (), [ 
			 (3, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'DeleteAllCharts' , ), 4, (4, (), [ ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'Refresh' , ), 5, (5, (), [ ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'BkgColor' , 'pVal' , ), 6, (6, (), [ (16403, 10, None, None) , ], 1 , 2 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'BkgColor' , 'pVal' , ), 6, (6, (), [ (19, 1, None, None) , ], 1 , 4 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
	(( 'BorderColor' , 'pVal' , ), 7, (7, (), [ (16403, 10, None, None) , ], 1 , 2 , 4 , 0 , 112 , (3, 0, None, None) , 0 , )),
	(( 'BorderColor' , 'pVal' , ), 7, (7, (), [ (19, 1, None, None) , ], 1 , 4 , 4 , 0 , 120 , (3, 0, None, None) , 0 , )),
	(( 'Title' , 'pVal' , ), 8, (8, (), [ (16392, 10, None, None) , ], 1 , 2 , 4 , 0 , 128 , (3, 0, None, None) , 0 , )),
	(( 'Title' , 'pVal' , ), 8, (8, (), [ (8, 1, None, None) , ], 1 , 4 , 4 , 0 , 136 , (3, 0, None, None) , 0 , )),
	(( 'GraphCtrlObj' , 'pVal' , ), 9, (9, (), [ (16393, 10, None, None) , ], 1 , 2 , 4 , 0 , 144 , (3, 0, None, None) , 0 , )),
	(( 'SaveAsImage' , 'FilePath' , 'nBPP' , 'Width' , 'height' , 
			 'FileType' , ), 10, (10, (), [ (8, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 152 , (3, 0, None, None) , 0 , )),
	(( 'EnablePan' , 'pVal' , ), 11, (11, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 160 , (3, 0, None, None) , 0 , )),
	(( 'EnablePan' , 'pVal' , ), 11, (11, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 168 , (3, 0, None, None) , 0 , )),
	(( 'EnableZoom' , 'pVal' , ), 12, (12, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 176 , (3, 0, None, None) , 0 , )),
	(( 'EnableZoom' , 'pVal' , ), 12, (12, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 184 , (3, 0, None, None) , 0 , )),
	(( 'UndoPanZoom' , ), 13, (13, (), [ ], 1 , 1 , 4 , 0 , 192 , (3, 0, None, None) , 0 , )),
	(( 'ChartCount' , 'pVal' , ), 14, (14, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 200 , (3, 0, None, None) , 0 , )),
	(( 'CreateStandardAxis' , 'AxisType' , ), 15, (15, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 208 , (3, 0, None, None) , 0 , )),
	(( 'EnableReferesh' , 'pVal' , ), 16, (16, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 216 , (3, 0, None, None) , 0 , )),
	(( 'EnableReferesh' , 'pVal' , ), 16, (16, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 224 , (3, 0, None, None) , 0 , )),
	(( 'CreateLineChart' , 'pChartID' , ), 17, (17, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 232 , (3, 0, None, None) , 0 , )),
	(( 'ClearChart' , 'ChartID' , ), 18, (18, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 240 , (3, 0, None, None) , 0 , )),
	(( 'CreateSurfaceChart' , 'pChartID' , ), 19, (19, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 248 , (3, 0, None, None) , 0 , )),
	(( 'CreatePointsChart' , 'pChartID' , ), 20, (20, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 256 , (3, 0, None, None) , 0 , )),
	(( 'CreateBarChart' , 'pChartID' , ), 21, (21, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 264 , (3, 0, None, None) , 0 , )),
	(( 'CreateCandlestickChart' , 'pChartID' , ), 22, (22, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 272 , (3, 0, None, None) , 0 , )),
	(( 'CreateGanttChart' , 'pChartID' , ), 23, (23, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 280 , (3, 0, None, None) , 0 , )),
	(( 'SetAxisTitle' , 'AxisType' , 'Title' , ), 24, (24, (), [ (3, 0, None, None) , 
			 (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 288 , (3, 0, None, None) , 0 , )),
	(( 'ShowMainTitle' , 'pVal' , ), 25, (25, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 296 , (3, 0, None, None) , 0 , )),
	(( 'ShowMainTitle' , 'pVal' , ), 25, (25, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 304 , (3, 0, None, None) , 0 , )),
	(( 'SetChartData' , 'ChartID' , 'ChartData' , ), 26, (26, (), [ (3, 0, None, None) , 
			 (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 312 , (3, 0, None, None) , 0 , )),
	(( 'AddChartPoint' , 'ChartID' , 'x' , 'y' , 'Value' , 
			 ), 27, (27, (), [ (3, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 320 , (3, 0, None, None) , 0 , )),
	(( 'TitleVisible' , 'pVal' , ), 28, (28, (), [ (16387, 10, None, None) , ], 1 , 2 , 4 , 0 , 328 , (3, 0, None, None) , 0 , )),
	(( 'TitleVisible' , 'pVal' , ), 28, (28, (), [ (3, 1, None, None) , ], 1 , 4 , 4 , 0 , 336 , (3, 0, None, None) , 0 , )),
	(( 'OnPrint' , 'pDC' , 'pPrintInfo' , ), 29, (29, (), [ (12, 0, None, None) , 
			 (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 344 , (3, 0, None, None) , 0 , )),
	(( 'Print' , 'Title' , ), 30, (30, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 352 , (3, 0, None, None) , 0 , )),
]

IMainApplication_vtables_dispatch_ = 1
IMainApplication_vtables_ = [
	(( 'SetAddInInfo' , 'lSessionID' , 'lInstanceHandle' , 'strXMLMenuToolbarIDCmdInfo' , 'lToobarRes' , 
			 'Reserved' , ), 1, (1, (), [ (3, 0, None, None) , (20, 0, None, None) , (8, 0, None, None) , 
			 (20, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'SetGraphInfo' , 'SessionID' , 'NoOfCharts' , ), 2, (2, (), [ (3, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
]

IMainApplicationEvents_vtables_dispatch_ = 1
IMainApplicationEvents_vtables_ = [
	(( 'OnApplicationLaunched' , ), 1, (1, (), [ ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'OnApplicationClose' , ), 2, (2, (), [ ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
]

IMainWindow_vtables_dispatch_ = 1
IMainWindow_vtables_ = [
	(( 'Window' , 'pVal' , ), 1, (1, (), [ (16396, 10, None, None) , ], 1 , 2 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'ChildWindow' , 'WindowType' , 'pVal' , ), 2, (2, (), [ (3, 0, None, None) , 
			 (16396, 10, None, None) , ], 1 , 2 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'ResetAllOutputStatusWindows' , ), 3, (3, (), [ ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'ResetOperationStatusWindow' , ), 4, (4, (), [ ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'AddOperationStatus' , 'Status' , 'bPostMessage' , ), 5, (5, (), [ (8, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'ResetErrorStatusWindow' , ), 6, (6, (), [ ], 1 , 1 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'AddErrorStatus' , 'Status' , 'bPostMessage' , ), 7, (7, (), [ (8, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
	(( 'ResetResultStatusWindow' , ), 8, (8, (), [ ], 1 , 1 , 4 , 0 , 112 , (3, 0, None, None) , 0 , )),
	(( 'AddResultStatus' , 'Status' , 'bPostMessage' , ), 9, (9, (), [ (8, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 120 , (3, 0, None, None) , 0 , )),
	(( 'ResetAllStatusWindows' , ), 10, (10, (), [ ], 1 , 1 , 4 , 0 , 128 , (3, 0, None, None) , 0 , )),
	(( 'SetStatusbarMessage' , 'StatusMessage' , 'bPostMessage' , ), 11, (11, (), [ (8, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 136 , (3, 0, None, None) , 0 , )),
	(( 'Set2ndStatusbarMessage' , 'Msg' , 'bPostMessage' , ), 12, (12, (), [ (8, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 144 , (3, 0, None, None) , 0 , )),
]

IMainWindowEvents_vtables_dispatch_ = 1
IMainWindowEvents_vtables_ = [
	(( 'MianWndProc' , 'MsgID' , 'wParam' , 'lParam' , ), 1, (1, (), [ 
			 (3, 0, None, None) , (12, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
]

IOpenGLUtilView_vtables_dispatch_ = 1
IOpenGLUtilView_vtables_ = [
	(( 'gluErrorString' , 'errCode' , 'errString' , ), 1, (1, (), [ (19, 0, None, None) , 
			 (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'gluErrorUnicodeStringEXT' , 'errCode' , 'errString' , ), 2, (2, (), [ (19, 0, None, None) , 
			 (16392, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'gluGetString' , 'name' , 'strString' , ), 3, (3, (), [ (19, 0, None, None) , 
			 (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'gluOrtho2D' , 'Left' , 'Right' , 'Bottom' , 'Top' , 
			 ), 4, (4, (), [ (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'gluPerspective' , 'fovy' , 'aspect' , 'zNear' , 'zFar' , 
			 ), 5, (5, (), [ (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'gluPickMatrix' , 'x' , 'y' , 'Width' , 'height' , 
			 'viewport' , ), 6, (6, (), [ (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , 
			 (5, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'gluLookAt' , 'eyex' , 'eyey' , 'eyez' , 'centerx' , 
			 'centery' , 'centerz' , 'upx' , 'upy' , 'upz' , 
			 ), 7, (7, (), [ (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , 
			 (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
	(( 'gluProject' , 'objx' , 'objy' , 'objz' , 'modelMatrix' , 
			 'projMatrix' , 'viewport' , 'winx' , 'winy' , 'winz' , 
			 ), 8, (8, (), [ (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (16389, 0, None, None) , 
			 (16389, 0, None, None) , (16387, 0, None, None) , (16389, 0, None, None) , (16389, 0, None, None) , (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 112 , (3, 0, None, None) , 0 , )),
	(( 'gluUnProject' , 'winx' , 'winy' , 'winz' , 'modelMatrix' , 
			 'projMatrix' , 'viewport' , 'objx' , 'objy' , 'objz' , 
			 ), 9, (9, (), [ (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (16389, 0, None, None) , 
			 (16389, 0, None, None) , (16387, 0, None, None) , (16389, 0, None, None) , (16389, 0, None, None) , (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 120 , (3, 0, None, None) , 0 , )),
	(( 'gluScaleImage' , 'format' , 'widthin' , 'heightin' , 'typein' , 
			 'datain' , 'widthout' , 'heightout' , 'typeout' , 'dataout' , 
			 ), 10, (10, (), [ (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , 
			 (12, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (16396, 0, None, None) , ], 1 , 1 , 4 , 0 , 128 , (3, 0, None, None) , 0 , )),
	(( 'gluBuild1DMipmaps' , 'target' , 'components' , 'Width' , 'format' , 
			 'type' , 'data' , ), 11, (11, (), [ (3, 0, None, None) , (3, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 136 , (3, 0, None, None) , 0 , )),
	(( 'gluBuild2DMipmaps' , 'target' , 'components' , 'Width' , 'height' , 
			 'format' , 'type' , 'data' , ), 12, (12, (), [ (3, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , 
			 (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 144 , (3, 0, None, None) , 0 , )),
	(( 'gluNewQuadric' , 'pNewQuadricObj' , ), 13, (13, (), [ (16396, 0, None, None) , ], 1 , 1 , 4 , 0 , 152 , (3, 0, None, None) , 0 , )),
	(( 'gluDeleteQuadric' , 'state' , ), 14, (14, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 160 , (3, 0, None, None) , 0 , )),
	(( 'gluQuadricNormals' , 'quadObject' , 'normals' , ), 15, (15, (), [ (12, 0, None, None) , 
			 (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 168 , (3, 0, None, None) , 0 , )),
	(( 'gluQuadricTexture' , 'quadObject' , 'textureCoords' , ), 16, (16, (), [ (12, 0, None, None) , 
			 (17, 0, None, None) , ], 1 , 1 , 4 , 0 , 176 , (3, 0, None, None) , 0 , )),
	(( 'gluQuadricOrientation' , 'quadObject' , 'orientation' , ), 17, (17, (), [ (12, 0, None, None) , 
			 (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 184 , (3, 0, None, None) , 0 , )),
	(( 'gluQuadricDrawStyle' , 'quadObject' , 'drawStyle' , ), 18, (18, (), [ (12, 0, None, None) , 
			 (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 192 , (3, 0, None, None) , 0 , )),
	(( 'gluCylinder' , 'qobj' , 'baseRadius' , 'topRadius' , 'height' , 
			 'slices' , 'stacks' , ), 19, (19, (), [ (12, 0, None, None) , (5, 0, None, None) , 
			 (5, 0, None, None) , (5, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 200 , (3, 0, None, None) , 0 , )),
	(( 'gluDisk' , 'qobj' , 'innerRadius' , 'outerRadius' , 'slices' , 
			 'loops' , ), 20, (20, (), [ (12, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 208 , (3, 0, None, None) , 0 , )),
	(( 'gluPartialDisk' , 'qobj' , 'innerRadius' , 'outerRadius' , 'slices' , 
			 'loops' , 'startAngle' , 'sweepAngle' , ), 21, (21, (), [ (12, 0, None, None) , 
			 (5, 0, None, None) , (5, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (5, 0, None, None) , 
			 (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 216 , (3, 0, None, None) , 0 , )),
	(( 'gluSphere' , 'qobj' , 'Radius' , 'slices' , 'stacks' , 
			 ), 22, (22, (), [ (12, 0, None, None) , (5, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 224 , (3, 0, None, None) , 0 , )),
	(( 'gluQuadricCallback' , 'qobj' , 'which' , 'fnCallback' , ), 23, (23, (), [ 
			 (12, 0, None, None) , (3, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 232 , (3, 0, None, None) , 0 , )),
	(( 'gluNewTess' , 'pNewTess' , ), 24, (24, (), [ (16396, 0, None, None) , ], 1 , 1 , 4 , 0 , 240 , (3, 0, None, None) , 0 , )),
	(( 'gluDeleteTess' , 'tess' , ), 25, (25, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 248 , (3, 0, None, None) , 0 , )),
	(( 'gluTessBeginPolygon' , 'tess' , 'polygon_data' , ), 26, (26, (), [ (12, 0, None, None) , 
			 (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 256 , (3, 0, None, None) , 0 , )),
	(( 'gluTessBeginContour' , 'tess' , ), 27, (27, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 264 , (3, 0, None, None) , 0 , )),
	(( 'gluTessVertex' , 'tess' , 'coords' , 'data' , ), 28, (28, (), [ 
			 (12, 0, None, None) , (16389, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 272 , (3, 0, None, None) , 0 , )),
	(( 'gluTessEndContour' , 'tess' , ), 29, (29, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 280 , (3, 0, None, None) , 0 , )),
	(( 'gluTessEndPolygon' , 'tess' , ), 30, (30, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 288 , (3, 0, None, None) , 0 , )),
	(( 'gluTessProperty' , 'tess' , 'which' , 'Value' , ), 31, (31, (), [ 
			 (12, 0, None, None) , (19, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 296 , (3, 0, None, None) , 0 , )),
	(( 'gluTessNormal' , 'tess' , 'x' , 'y' , 'z' , 
			 ), 32, (32, (), [ (12, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 304 , (3, 0, None, None) , 0 , )),
	(( 'gluTessCallback' , 'tess' , 'which' , 'Callback' , ), 33, (33, (), [ 
			 (12, 0, None, None) , (19, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 312 , (3, 0, None, None) , 0 , )),
	(( 'gluGetTessProperty' , 'tess' , 'which' , 'Value' , ), 34, (34, (), [ 
			 (12, 0, None, None) , (19, 0, None, None) , (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 320 , (3, 0, None, None) , 0 , )),
	(( 'gluNewNurbsRenderer' , 'ewNurbsRenderer' , ), 35, (35, (), [ (16396, 0, None, None) , ], 1 , 1 , 4 , 0 , 328 , (3, 0, None, None) , 0 , )),
	(( 'gluDeleteNurbsRenderer' , 'nobj' , ), 36, (36, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 336 , (3, 0, None, None) , 0 , )),
	(( 'gluBeginSurface' , 'nobj' , ), 37, (37, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 344 , (3, 0, None, None) , 0 , )),
	(( 'gluBeginCurve' , 'nobj' , ), 38, (38, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 352 , (3, 0, None, None) , 0 , )),
	(( 'gluEndCurve' , 'nobj' , ), 39, (39, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 360 , (3, 0, None, None) , 0 , )),
	(( 'gluEndSurface' , 'nobj' , ), 40, (40, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 368 , (3, 0, None, None) , 0 , )),
	(( 'gluBeginTrim' , 'nobj' , ), 41, (41, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 376 , (3, 0, None, None) , 0 , )),
	(( 'gluEndTrim' , 'nobj' , ), 42, (42, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 384 , (3, 0, None, None) , 0 , )),
	(( 'gluPwlCurve' , 'nobj' , 'count' , 'pArray' , 'stride' , 
			 'lType' , ), 43, (43, (), [ (12, 0, None, None) , (3, 0, None, None) , (16388, 0, None, None) , 
			 (3, 0, None, None) , (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 392 , (3, 0, None, None) , 0 , )),
	(( 'gluNurbsCurve' , 'nobj' , 'nknots' , 'knot' , 'lstride' , 
			 'ctlarray' , 'lOrder' , 'lType' , ), 44, (44, (), [ (12, 0, None, None) , 
			 (3, 0, None, None) , (16388, 0, None, None) , (3, 0, None, None) , (16388, 0, None, None) , (3, 0, None, None) , 
			 (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 400 , (3, 0, None, None) , 0 , )),
	(( 'gluNurbsSurface' , 'nobj' , 'sknot_count' , 'sknot' , 'tknot_count' , 
			 'tknot' , 's_stride' , 't_stride' , 'ctlarray' , 'sorder' , 
			 'torder' , 'lType' , ), 45, (45, (), [ (12, 0, None, None) , (3, 0, None, None) , 
			 (16388, 0, None, None) , (3, 0, None, None) , (16388, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , 
			 (16388, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 408 , (3, 0, None, None) , 0 , )),
	(( 'gluLoadSamplingMatrices' , 'nobj' , 'modelMatrix' , 'projMatrix' , 'viewport' , 
			 ), 46, (46, (), [ (12, 0, None, None) , (16388, 0, None, None) , (16388, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 416 , (3, 0, None, None) , 0 , )),
	(( 'gluNurbsProperty' , 'nobj' , 'lproperty' , 'lValue' , ), 47, (47, (), [ 
			 (12, 0, None, None) , (19, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 424 , (3, 0, None, None) , 0 , )),
	(( 'gluGetNurbsProperty' , 'nobj' , 'lproperty' , 'pValue' , ), 48, (48, (), [ 
			 (12, 0, None, None) , (19, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 432 , (3, 0, None, None) , 0 , )),
	(( 'gluNurbsCallback' , 'nobj' , 'which' , 'pCallback' , ), 49, (49, (), [ 
			 (12, 0, None, None) , (19, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 440 , (3, 0, None, None) , 0 , )),
	(( 'gluBeginPolygon' , 'tess' , ), 50, (50, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 448 , (3, 0, None, None) , 0 , )),
	(( 'gluNextContour' , 'tess' , 'lType' , ), 51, (51, (), [ (12, 0, None, None) , 
			 (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 456 , (3, 0, None, None) , 0 , )),
	(( 'gluEndPolygon' , 'tess' , ), 52, (52, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 464 , (3, 0, None, None) , 0 , )),
]

IOpenGLUtilViewEvents_vtables_dispatch_ = 1
IOpenGLUtilViewEvents_vtables_ = [
	(( 'GLUquadricErrorProc' , 'errorNo' , ), 1, (1, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'GLUtessBeginProc' , 'param' , ), 2, (2, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'GLUtessEdgeFlagProc' , 'param' , ), 3, (3, (), [ (17, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'GLUtessVertexProc' , 'pParam' , ), 4, (4, (), [ (16396, 0, None, None) , ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'GLUtessEndProc' , 'param' , ), 5, (5, (), [ (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'GLUtessErrorProc' , 'param' , ), 6, (6, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'GLUtessCombineProc' , 'Param1' , 'Param2' , 'Param3' , 'Param4' , 
			 ), 7, (7, (), [ (16389, 0, None, None) , (16396, 0, None, None) , (16388, 0, None, None) , (16396, 0, None, None) , ], 1 , 1 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
	(( 'GLUtessBeginDataProc' , 'Param1' , 'Param2' , ), 8, (8, (), [ (19, 0, None, None) , 
			 (16396, 0, None, None) , ], 1 , 1 , 4 , 0 , 112 , (3, 0, None, None) , 0 , )),
	(( 'GLUtessEdgeFlagDataProc' , 'Param1' , 'Param2' , ), 9, (9, (), [ (17, 0, None, None) , 
			 (16396, 0, None, None) , ], 1 , 1 , 4 , 0 , 120 , (3, 0, None, None) , 0 , )),
	(( 'GLUtessVertexDataProc' , 'Param1' , 'Param2' , ), 10, (10, (), [ (16396, 0, None, None) , 
			 (16396, 0, None, None) , ], 1 , 1 , 4 , 0 , 128 , (3, 0, None, None) , 0 , )),
	(( 'GLUtessEndDataProc' , 'Param1' , ), 11, (11, (), [ (16396, 0, None, None) , ], 1 , 1 , 4 , 0 , 136 , (3, 0, None, None) , 0 , )),
	(( 'GLUtessErrorDataProc' , 'Param1' , 'Param2' , ), 12, (12, (), [ (19, 0, None, None) , 
			 (16396, 0, None, None) , ], 1 , 1 , 4 , 0 , 144 , (3, 0, None, None) , 0 , )),
	(( 'GLUtessCombineDataProc' , 'Param1' , 'Param2' , 'Param3' , 'Param4' , 
			 'Param5' , ), 13, (13, (), [ (16389, 0, None, None) , (16396, 0, None, None) , (16388, 0, None, None) , 
			 (16396, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 152 , (3, 0, None, None) , 0 , )),
	(( 'GLUnurbsErrorProc' , 'Param1' , ), 14, (14, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 160 , (3, 0, None, None) , 0 , )),
]

IOpenGLView_vtables_dispatch_ = 1
IOpenGLView_vtables_ = [
	(( 'glAccum' , 'op' , 'Value' , ), 1, (1, (), [ (19, 0, None, None) , 
			 (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'glAlphaFunc' , 'func' , 'ref' , ), 2, (2, (), [ (19, 0, None, None) , 
			 (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'glAreTexturesResident' , 'n' , 'textures' , 'residences' , 'bResult' , 
			 ), 3, (3, (), [ (3, 0, None, None) , (16403, 0, None, None) , (16401, 0, None, None) , (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'glArrayElement' , 'i' , ), 4, (4, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'glBegin' , 'Mode' , ), 5, (5, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'glBindTexture' , 'target' , 'texture' , ), 6, (6, (), [ (19, 0, None, None) , 
			 (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'glBitmap' , 'Width' , 'height' , 'xorig' , 'yorig' , 
			 'xmove' , 'ymove' , 'bitmap' , ), 7, (7, (), [ (3, 0, None, None) , 
			 (3, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , 
			 (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
	(( 'glBlendFunc' , 'sfactor' , 'dfactor' , ), 8, (8, (), [ (19, 0, None, None) , 
			 (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 112 , (3, 0, None, None) , 0 , )),
	(( 'glCallList' , 'list' , ), 9, (9, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 120 , (3, 0, None, None) , 0 , )),
	(( 'glCallLists' , 'n' , 'type' , 'lists' , ), 10, (10, (), [ 
			 (3, 0, None, None) , (19, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 128 , (3, 0, None, None) , 0 , )),
	(( 'glClear' , 'Mask' , ), 11, (11, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 136 , (3, 0, None, None) , 0 , )),
	(( 'glClearAccum' , 'red' , 'green' , 'blue' , 'Alpha' , 
			 ), 12, (12, (), [ (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 144 , (3, 0, None, None) , 0 , )),
	(( 'glClearColor' , 'red' , 'green' , 'blue' , 'Alpha' , 
			 ), 13, (13, (), [ (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 152 , (3, 0, None, None) , 0 , )),
	(( 'glClearDepth' , 'Depth' , ), 14, (14, (), [ (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 160 , (3, 0, None, None) , 0 , )),
	(( 'glClearIndex' , 'c' , ), 15, (15, (), [ (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 168 , (3, 0, None, None) , 0 , )),
	(( 'glClearStencil' , 's' , ), 16, (16, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 176 , (3, 0, None, None) , 0 , )),
	(( 'glClipPlane' , 'plane' , 'equation' , ), 17, (17, (), [ (19, 0, None, None) , 
			 (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 184 , (3, 0, None, None) , 0 , )),
	(( 'glColor3b' , 'red' , 'green' , 'blue' , ), 18, (18, (), [ 
			 (16, 0, None, None) , (16, 0, None, None) , (16, 0, None, None) , ], 1 , 1 , 4 , 0 , 192 , (3, 0, None, None) , 0 , )),
	(( 'glColor3bv' , 'v' , ), 19, (19, (), [ (16400, 0, None, None) , ], 1 , 1 , 4 , 0 , 200 , (3, 0, None, None) , 0 , )),
	(( 'glColor3d' , 'red' , 'green' , 'blue' , ), 20, (20, (), [ 
			 (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 208 , (3, 0, None, None) , 0 , )),
	(( 'glColor3dv' , 'v' , ), 21, (21, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 216 , (3, 0, None, None) , 0 , )),
	(( 'glColor3f' , 'red' , 'green' , 'blue' , ), 22, (22, (), [ 
			 (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 224 , (3, 0, None, None) , 0 , )),
	(( 'glColor3fv' , 'v' , ), 23, (23, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 232 , (3, 0, None, None) , 0 , )),
	(( 'glColor3i' , 'red' , 'green' , 'blue' , ), 24, (24, (), [ 
			 (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 240 , (3, 0, None, None) , 0 , )),
	(( 'glColor3iv' , 'v' , ), 25, (25, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 248 , (3, 0, None, None) , 0 , )),
	(( 'glColor3s' , 'red' , 'green' , 'blue' , ), 26, (26, (), [ 
			 (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 256 , (3, 0, None, None) , 0 , )),
	(( 'glColor3sv' , 'v' , ), 27, (27, (), [ (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 264 , (3, 0, None, None) , 0 , )),
	(( 'glColor3ub' , 'red' , 'green' , 'blue' , ), 28, (28, (), [ 
			 (17, 0, None, None) , (17, 0, None, None) , (17, 0, None, None) , ], 1 , 1 , 4 , 0 , 272 , (3, 0, None, None) , 0 , )),
	(( 'glColor3ubv' , 'v' , ), 29, (29, (), [ (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 280 , (3, 0, None, None) , 0 , )),
	(( 'glColor3ui' , 'red' , 'green' , 'blue' , ), 30, (30, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 288 , (3, 0, None, None) , 0 , )),
	(( 'glColor3uiv' , 'v' , ), 31, (31, (), [ (16403, 0, None, None) , ], 1 , 1 , 4 , 0 , 296 , (3, 0, None, None) , 0 , )),
	(( 'glColor3us' , 'red' , 'green' , 'blue' , ), 32, (32, (), [ 
			 (18, 0, None, None) , (18, 0, None, None) , (18, 0, None, None) , ], 1 , 1 , 4 , 0 , 304 , (3, 0, None, None) , 0 , )),
	(( 'glColor3usv' , 'v' , ), 33, (33, (), [ (16402, 0, None, None) , ], 1 , 1 , 4 , 0 , 312 , (3, 0, None, None) , 0 , )),
	(( 'glColor4b' , 'red' , 'green' , 'blue' , 'Alpha' , 
			 ), 34, (34, (), [ (16, 0, None, None) , (16, 0, None, None) , (16, 0, None, None) , (16, 0, None, None) , ], 1 , 1 , 4 , 0 , 320 , (3, 0, None, None) , 0 , )),
	(( 'glColor4bv' , 'v' , ), 35, (35, (), [ (16400, 0, None, None) , ], 1 , 1 , 4 , 0 , 328 , (3, 0, None, None) , 0 , )),
	(( 'glColor4d' , 'red' , 'green' , 'blue' , 'Alpha' , 
			 ), 36, (36, (), [ (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 336 , (3, 0, None, None) , 0 , )),
	(( 'glColor4dv' , 'v' , ), 37, (37, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 344 , (3, 0, None, None) , 0 , )),
	(( 'glColor4f' , 'red' , 'green' , 'blue' , 'Alpha' , 
			 ), 38, (38, (), [ (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 352 , (3, 0, None, None) , 0 , )),
	(( 'glColor4fv' , 'v' , ), 39, (39, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 360 , (3, 0, None, None) , 0 , )),
	(( 'glColor4i' , 'red' , 'green' , 'blue' , 'Alpha' , 
			 ), 40, (40, (), [ (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 368 , (3, 0, None, None) , 0 , )),
	(( 'glColor4iv' , 'v' , ), 41, (41, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 376 , (3, 0, None, None) , 0 , )),
	(( 'glColor4s' , 'red' , 'green' , 'blue' , 'Alpha' , 
			 ), 42, (42, (), [ (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 384 , (3, 0, None, None) , 0 , )),
	(( 'glColor4sv' , 'v' , ), 43, (43, (), [ (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 392 , (3, 0, None, None) , 0 , )),
	(( 'glColor4ub' , 'red' , 'green' , 'blue' , 'Alpha' , 
			 ), 44, (44, (), [ (17, 0, None, None) , (17, 0, None, None) , (17, 0, None, None) , (17, 0, None, None) , ], 1 , 1 , 4 , 0 , 400 , (3, 0, None, None) , 0 , )),
	(( 'glColor4ubv' , 'v' , ), 45, (45, (), [ (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 408 , (3, 0, None, None) , 0 , )),
	(( 'glColor4ui' , 'red' , 'green' , 'blue' , 'Alpha' , 
			 ), 46, (46, (), [ (19, 0, None, None) , (19, 0, None, None) , (19, 0, None, None) , (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 416 , (3, 0, None, None) , 0 , )),
	(( 'glColor4uiv' , 'v' , ), 47, (47, (), [ (16403, 0, None, None) , ], 1 , 1 , 4 , 0 , 424 , (3, 0, None, None) , 0 , )),
	(( 'glColor4us' , 'red' , 'green' , 'blue' , 'Alpha' , 
			 ), 48, (48, (), [ (18, 0, None, None) , (18, 0, None, None) , (18, 0, None, None) , (18, 0, None, None) , ], 1 , 1 , 4 , 0 , 432 , (3, 0, None, None) , 0 , )),
	(( 'glColor4usv' , 'v' , ), 49, (49, (), [ (16402, 0, None, None) , ], 1 , 1 , 4 , 0 , 440 , (3, 0, None, None) , 0 , )),
	(( 'glColorMask' , 'red' , 'green' , 'blue' , 'Alpha' , 
			 ), 50, (50, (), [ (17, 0, None, None) , (17, 0, None, None) , (17, 0, None, None) , (17, 0, None, None) , ], 1 , 1 , 4 , 0 , 448 , (3, 0, None, None) , 0 , )),
	(( 'glColorMaterial' , 'face' , 'Mode' , ), 51, (51, (), [ (19, 0, None, None) , 
			 (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 456 , (3, 0, None, None) , 0 , )),
	(( 'glColorPointer' , 'size' , 'type' , 'stride' , 'pointer' , 
			 ), 52, (52, (), [ (3, 0, None, None) , (19, 0, None, None) , (3, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 464 , (3, 0, None, None) , 0 , )),
	(( 'glCopyPixels' , 'x' , 'y' , 'Width' , 'height' , 
			 'type' , ), 53, (53, (), [ (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , 
			 (3, 0, None, None) , (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 472 , (3, 0, None, None) , 0 , )),
	(( 'glCopyTexImage1D' , 'target' , 'level' , 'internalFormat' , 'x' , 
			 'y' , 'Width' , 'border' , ), 54, (54, (), [ (19, 0, None, None) , 
			 (3, 0, None, None) , (19, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 480 , (3, 0, None, None) , 0 , )),
	(( 'glCopyTexImage2D' , 'target' , 'level' , 'internalFormat' , 'x' , 
			 'y' , 'Width' , 'height' , 'border' , ), 55, (55, (), [ 
			 (19, 0, None, None) , (3, 0, None, None) , (19, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 488 , (3, 0, None, None) , 0 , )),
	(( 'glCopyTexSubImage1D' , 'target' , 'level' , 'xoffset' , 'x' , 
			 'y' , 'Width' , ), 56, (56, (), [ (19, 0, None, None) , (3, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 496 , (3, 0, None, None) , 0 , )),
	(( 'glCopyTexSubImage2D' , 'target' , 'level' , 'xoffset' , 'yoffset' , 
			 'x' , 'y' , 'Width' , 'height' , ), 57, (57, (), [ 
			 (19, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 504 , (3, 0, None, None) , 0 , )),
	(( 'glCullFace' , 'Mode' , ), 58, (58, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 512 , (3, 0, None, None) , 0 , )),
	(( 'glDeleteLists' , 'list' , 'range' , ), 59, (59, (), [ (19, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 520 , (3, 0, None, None) , 0 , )),
	(( 'glDeleteTextures' , 'n' , 'textures' , ), 60, (60, (), [ (3, 0, None, None) , 
			 (16403, 0, None, None) , ], 1 , 1 , 4 , 0 , 528 , (3, 0, None, None) , 0 , )),
	(( 'glDepthFunc' , 'func' , ), 61, (61, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 536 , (3, 0, None, None) , 0 , )),
	(( 'glDepthMask' , 'flag' , ), 62, (62, (), [ (17, 0, None, None) , ], 1 , 1 , 4 , 0 , 544 , (3, 0, None, None) , 0 , )),
	(( 'glDepthRange' , 'zNear' , 'zFar' , ), 63, (63, (), [ (5, 0, None, None) , 
			 (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 552 , (3, 0, None, None) , 0 , )),
	(( 'glDisable' , 'cap' , ), 64, (64, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 560 , (3, 0, None, None) , 0 , )),
	(( 'glDisableClientState' , 'array' , ), 65, (65, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 568 , (3, 0, None, None) , 0 , )),
	(( 'glDrawArrays' , 'Mode' , 'first' , 'count' , ), 66, (66, (), [ 
			 (19, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 576 , (3, 0, None, None) , 0 , )),
	(( 'glDrawBuffer' , 'Mode' , ), 67, (67, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 584 , (3, 0, None, None) , 0 , )),
	(( 'glDrawElements' , 'Mode' , 'count' , 'type' , 'indices' , 
			 ), 68, (68, (), [ (19, 0, None, None) , (3, 0, None, None) , (19, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 592 , (3, 0, None, None) , 0 , )),
	(( 'glDrawPixels' , 'Width' , 'height' , 'format' , 'type' , 
			 'pixels' , ), 69, (69, (), [ (3, 0, None, None) , (3, 0, None, None) , (19, 0, None, None) , 
			 (19, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 600 , (3, 0, None, None) , 0 , )),
	(( 'glEdgeFlag' , 'flag' , ), 70, (70, (), [ (17, 0, None, None) , ], 1 , 1 , 4 , 0 , 608 , (3, 0, None, None) , 0 , )),
	(( 'glEdgeFlagPointer' , 'stride' , 'pointer' , ), 71, (71, (), [ (3, 0, None, None) , 
			 (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 616 , (3, 0, None, None) , 0 , )),
	(( 'glEdgeFlagv' , 'flag' , ), 72, (72, (), [ (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 624 , (3, 0, None, None) , 0 , )),
	(( 'glEnable' , 'cap' , ), 73, (73, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 632 , (3, 0, None, None) , 0 , )),
	(( 'glEnableClientState' , 'array' , ), 74, (74, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 640 , (3, 0, None, None) , 0 , )),
	(( 'glEnd' , ), 75, (75, (), [ ], 1 , 1 , 4 , 0 , 648 , (3, 0, None, None) , 0 , )),
	(( 'glEndList' , ), 76, (76, (), [ ], 1 , 1 , 4 , 0 , 656 , (3, 0, None, None) , 0 , )),
	(( 'glEvalCoord1d' , 'u' , ), 77, (77, (), [ (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 664 , (3, 0, None, None) , 0 , )),
	(( 'glEvalCoord1dv' , 'u' , ), 78, (78, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 672 , (3, 0, None, None) , 0 , )),
	(( 'glEvalCoord1f' , 'u' , ), 79, (79, (), [ (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 680 , (3, 0, None, None) , 0 , )),
	(( 'glEvalCoord1fv' , 'u' , ), 80, (80, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 688 , (3, 0, None, None) , 0 , )),
	(( 'glEvalCoord2d' , 'u' , 'v' , ), 81, (81, (), [ (5, 0, None, None) , 
			 (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 696 , (3, 0, None, None) , 0 , )),
	(( 'glEvalCoord2dv' , 'u' , ), 82, (82, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 704 , (3, 0, None, None) , 0 , )),
	(( 'glEvalCoord2f' , 'u' , 'v' , ), 83, (83, (), [ (4, 0, None, None) , 
			 (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 712 , (3, 0, None, None) , 0 , )),
	(( 'glEvalCoord2fv' , 'u' , ), 84, (84, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 720 , (3, 0, None, None) , 0 , )),
	(( 'glEvalMesh1' , 'Mode' , 'i1' , 'i2' , ), 85, (85, (), [ 
			 (19, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 728 , (3, 0, None, None) , 0 , )),
	(( 'glEvalMesh2' , 'Mode' , 'i1' , 'i2' , 'j1' , 
			 'j2' , ), 86, (86, (), [ (19, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 736 , (3, 0, None, None) , 0 , )),
	(( 'glEvalPoint1' , 'i' , ), 87, (87, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 744 , (3, 0, None, None) , 0 , )),
	(( 'glEvalPoint2' , 'i' , 'j' , ), 88, (88, (), [ (3, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 752 , (3, 0, None, None) , 0 , )),
	(( 'glFeedbackBuffer' , 'size' , 'type' , 'buffer' , ), 89, (89, (), [ 
			 (3, 0, None, None) , (19, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 760 , (3, 0, None, None) , 0 , )),
	(( 'glFinish' , ), 90, (90, (), [ ], 1 , 1 , 4 , 0 , 768 , (3, 0, None, None) , 0 , )),
	(( 'glFlush' , ), 91, (91, (), [ ], 1 , 1 , 4 , 0 , 776 , (3, 0, None, None) , 0 , )),
	(( 'glFogf' , 'PName' , 'param' , ), 92, (92, (), [ (19, 0, None, None) , 
			 (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 784 , (3, 0, None, None) , 0 , )),
	(( 'glFogfv' , 'PName' , 'Params' , ), 93, (93, (), [ (19, 0, None, None) , 
			 (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 792 , (3, 0, None, None) , 0 , )),
	(( 'glFogi' , 'PName' , 'param' , ), 94, (94, (), [ (19, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 800 , (3, 0, None, None) , 0 , )),
	(( 'glFogiv' , 'PName' , 'Params' , ), 95, (95, (), [ (19, 0, None, None) , 
			 (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 808 , (3, 0, None, None) , 0 , )),
	(( 'glFrontFace' , 'Mode' , ), 96, (96, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 816 , (3, 0, None, None) , 0 , )),
	(( 'glFrustum' , 'Left' , 'Right' , 'Bottom' , 'Top' , 
			 'zNear' , 'zFar' , ), 97, (97, (), [ (5, 0, None, None) , (5, 0, None, None) , 
			 (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 824 , (3, 0, None, None) , 0 , )),
	(( 'glGenLists' , 'range' , 'pResult' , ), 98, (98, (), [ (3, 0, None, None) , 
			 (16403, 0, None, None) , ], 1 , 1 , 4 , 0 , 832 , (3, 0, None, None) , 0 , )),
	(( 'glGenTextures' , 'n' , 'textures' , ), 99, (99, (), [ (3, 0, None, None) , 
			 (16403, 0, None, None) , ], 1 , 1 , 4 , 0 , 840 , (3, 0, None, None) , 0 , )),
	(( 'glGetBooleanv' , 'PName' , 'Params' , ), 100, (100, (), [ (19, 0, None, None) , 
			 (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 848 , (3, 0, None, None) , 0 , )),
	(( 'glGetClipPlane' , 'plane' , 'equation' , ), 101, (101, (), [ (19, 0, None, None) , 
			 (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 856 , (3, 0, None, None) , 0 , )),
	(( 'glGetDoublev' , 'PName' , 'Params' , ), 102, (102, (), [ (19, 0, None, None) , 
			 (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 864 , (3, 0, None, None) , 0 , )),
	(( 'glGetError' , 'pResult' , ), 103, (103, (), [ (16403, 0, None, None) , ], 1 , 1 , 4 , 0 , 872 , (3, 0, None, None) , 0 , )),
	(( 'glGetFloatv' , 'PName' , 'Params' , ), 104, (104, (), [ (19, 0, None, None) , 
			 (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 880 , (3, 0, None, None) , 0 , )),
	(( 'glGetIntegerv' , 'PName' , 'Params' , ), 105, (105, (), [ (19, 0, None, None) , 
			 (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 888 , (3, 0, None, None) , 0 , )),
	(( 'glGetLightfv' , 'Light' , 'PName' , 'Params' , ), 106, (106, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 896 , (3, 0, None, None) , 0 , )),
	(( 'glGetLightiv' , 'Light' , 'PName' , 'Params' , ), 107, (107, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 904 , (3, 0, None, None) , 0 , )),
	(( 'glGetMapdv' , 'target' , 'query' , 'v' , ), 108, (108, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 912 , (3, 0, None, None) , 0 , )),
	(( 'glGetMapfv' , 'target' , 'query' , 'v' , ), 109, (109, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 920 , (3, 0, None, None) , 0 , )),
	(( 'glGetMapiv' , 'target' , 'query' , 'v' , ), 110, (110, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 928 , (3, 0, None, None) , 0 , )),
	(( 'glGetMaterialfv' , 'face' , 'PName' , 'Params' , ), 111, (111, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 936 , (3, 0, None, None) , 0 , )),
	(( 'glGetMaterialiv' , 'face' , 'PName' , 'Params' , ), 112, (112, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 944 , (3, 0, None, None) , 0 , )),
	(( 'glGetPixelMapfv' , 'map' , 'Values' , ), 113, (113, (), [ (19, 0, None, None) , 
			 (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 952 , (3, 0, None, None) , 0 , )),
	(( 'glGetPixelMapuiv' , 'map' , 'Values' , ), 114, (114, (), [ (19, 0, None, None) , 
			 (16403, 0, None, None) , ], 1 , 1 , 4 , 0 , 960 , (3, 0, None, None) , 0 , )),
	(( 'glGetPixelMapusv' , 'map' , 'Values' , ), 115, (115, (), [ (19, 0, None, None) , 
			 (16402, 0, None, None) , ], 1 , 1 , 4 , 0 , 968 , (3, 0, None, None) , 0 , )),
	(( 'glGetPointerv' , 'PName' , 'Params' , ), 116, (116, (), [ (19, 0, None, None) , 
			 (16396, 0, None, None) , ], 1 , 1 , 4 , 0 , 976 , (3, 0, None, None) , 0 , )),
	(( 'glGetPolygonStipple' , 'Mask' , ), 117, (117, (), [ (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 984 , (3, 0, None, None) , 0 , )),
	(( 'glGetString' , 'name' , 'pResult' , ), 118, (118, (), [ (19, 0, None, None) , 
			 (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 992 , (3, 0, None, None) , 0 , )),
	(( 'glGetTexEnvfv' , 'target' , 'PName' , 'Params' , ), 119, (119, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1000 , (3, 0, None, None) , 0 , )),
	(( 'glGetTexEnviv' , 'target' , 'PName' , 'Params' , ), 120, (120, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 1008 , (3, 0, None, None) , 0 , )),
	(( 'glGetTexGendv' , 'coord' , 'PName' , 'Params' , ), 121, (121, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 1016 , (3, 0, None, None) , 0 , )),
	(( 'glGetTexGenfv' , 'coord' , 'PName' , 'Params' , ), 122, (122, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1024 , (3, 0, None, None) , 0 , )),
	(( 'glGetTexGeniv' , 'coord' , 'PName' , 'Params' , ), 123, (123, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 1032 , (3, 0, None, None) , 0 , )),
	(( 'glGetTexImage' , 'target' , 'level' , 'format' , 'type' , 
			 'pixels' , ), 124, (124, (), [ (19, 0, None, None) , (3, 0, None, None) , (19, 0, None, None) , 
			 (19, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 1040 , (3, 0, None, None) , 0 , )),
	(( 'glGetTexLevelParameterfv' , 'target' , 'level' , 'PName' , 'Params' , 
			 ), 125, (125, (), [ (19, 0, None, None) , (3, 0, None, None) , (19, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1048 , (3, 0, None, None) , 0 , )),
	(( 'glGetTexLevelParameteriv' , 'target' , 'level' , 'PName' , 'Params' , 
			 ), 126, (126, (), [ (19, 0, None, None) , (3, 0, None, None) , (19, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 1056 , (3, 0, None, None) , 0 , )),
	(( 'glGetTexParameterfv' , 'target' , 'PName' , 'Params' , ), 127, (127, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1064 , (3, 0, None, None) , 0 , )),
	(( 'glGetTexParameteriv' , 'target' , 'PName' , 'Params' , ), 128, (128, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 1072 , (3, 0, None, None) , 0 , )),
	(( 'glHint' , 'target' , 'Mode' , ), 129, (129, (), [ (19, 0, None, None) , 
			 (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 1080 , (3, 0, None, None) , 0 , )),
	(( 'glIndexMask' , 'Mask' , ), 130, (130, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 1088 , (3, 0, None, None) , 0 , )),
	(( 'glIndexPointer' , 'type' , 'stride' , 'pointer' , ), 131, (131, (), [ 
			 (19, 0, None, None) , (3, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 1096 , (3, 0, None, None) , 0 , )),
	(( 'glIndexd' , 'c' , ), 132, (132, (), [ (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 1104 , (3, 0, None, None) , 0 , )),
	(( 'glIndexdv' , 'c' , ), 133, (133, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 1112 , (3, 0, None, None) , 0 , )),
	(( 'glIndexf' , 'c' , ), 134, (134, (), [ (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1120 , (3, 0, None, None) , 0 , )),
	(( 'glIndexfv' , 'c' , ), 135, (135, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1128 , (3, 0, None, None) , 0 , )),
	(( 'glIndexi' , 'c' , ), 136, (136, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 1136 , (3, 0, None, None) , 0 , )),
	(( 'glIndexiv' , 'c' , ), 137, (137, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 1144 , (3, 0, None, None) , 0 , )),
	(( 'glIndexs' , 'c' , ), 138, (138, (), [ (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 1152 , (3, 0, None, None) , 0 , )),
	(( 'glIndexsv' , 'c' , ), 139, (139, (), [ (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 1160 , (3, 0, None, None) , 0 , )),
	(( 'glIndexub' , 'c' , ), 140, (140, (), [ (17, 0, None, None) , ], 1 , 1 , 4 , 0 , 1168 , (3, 0, None, None) , 0 , )),
	(( 'glIndexubv' , 'c' , ), 141, (141, (), [ (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 1176 , (3, 0, None, None) , 0 , )),
	(( 'glInitNames' , ), 142, (142, (), [ ], 1 , 1 , 4 , 0 , 1184 , (3, 0, None, None) , 0 , )),
	(( 'glInterleavedArrays' , 'format' , 'stride' , 'pointer' , ), 143, (143, (), [ 
			 (19, 0, None, None) , (3, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 1192 , (3, 0, None, None) , 0 , )),
	(( 'glIsEnabled' , 'cap' , 'pResult' , ), 144, (144, (), [ (19, 0, None, None) , 
			 (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 1200 , (3, 0, None, None) , 0 , )),
	(( 'glIsList' , 'list' , 'pResult' , ), 145, (145, (), [ (19, 0, None, None) , 
			 (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 1208 , (3, 0, None, None) , 0 , )),
	(( 'glIsTexture' , 'texture' , 'pResult' , ), 146, (146, (), [ (19, 0, None, None) , 
			 (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 1216 , (3, 0, None, None) , 0 , )),
	(( 'glLightModelf' , 'PName' , 'param' , ), 147, (147, (), [ (19, 0, None, None) , 
			 (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1224 , (3, 0, None, None) , 0 , )),
	(( 'glLightModelfv' , 'PName' , 'Params' , ), 148, (148, (), [ (19, 0, None, None) , 
			 (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1232 , (3, 0, None, None) , 0 , )),
	(( 'glLightModeli' , 'PName' , 'param' , ), 149, (149, (), [ (19, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 1240 , (3, 0, None, None) , 0 , )),
	(( 'glLightModeliv' , 'PName' , 'Params' , ), 150, (150, (), [ (19, 0, None, None) , 
			 (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 1248 , (3, 0, None, None) , 0 , )),
	(( 'glLightf' , 'Light' , 'PName' , 'param' , ), 151, (151, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1256 , (3, 0, None, None) , 0 , )),
	(( 'glLightfv' , 'Light' , 'PName' , 'Params' , ), 152, (152, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1264 , (3, 0, None, None) , 0 , )),
	(( 'glLighti' , 'Light' , 'PName' , 'param' , ), 153, (153, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 1272 , (3, 0, None, None) , 0 , )),
	(( 'glLightiv' , 'Light' , 'PName' , 'Params' , ), 154, (154, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 1280 , (3, 0, None, None) , 0 , )),
	(( 'glLineStipple' , 'factor' , 'pattern' , ), 155, (155, (), [ (3, 0, None, None) , 
			 (18, 0, None, None) , ], 1 , 1 , 4 , 0 , 1288 , (3, 0, None, None) , 0 , )),
	(( 'glLineWidth' , 'Width' , ), 156, (156, (), [ (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1296 , (3, 0, None, None) , 0 , )),
	(( 'glListBase' , 'base' , ), 157, (157, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 1304 , (3, 0, None, None) , 0 , )),
	(( 'glLoadIdentity' , ), 158, (158, (), [ ], 1 , 1 , 4 , 0 , 1312 , (3, 0, None, None) , 0 , )),
	(( 'glLoadMatrixd' , 'm' , ), 159, (159, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 1320 , (3, 0, None, None) , 0 , )),
	(( 'glLoadMatrixf' , 'm' , ), 160, (160, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1328 , (3, 0, None, None) , 0 , )),
	(( 'glLoadName' , 'name' , ), 161, (161, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 1336 , (3, 0, None, None) , 0 , )),
	(( 'glLogicOp' , 'opcode' , ), 162, (162, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 1344 , (3, 0, None, None) , 0 , )),
	(( 'glMap1d' , 'target' , 'u1' , 'u2' , 'stride' , 
			 'order' , 'points' , ), 163, (163, (), [ (19, 0, None, None) , (5, 0, None, None) , 
			 (5, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 1352 , (3, 0, None, None) , 0 , )),
	(( 'glMap1f' , 'target' , 'u1' , 'u2' , 'stride' , 
			 'order' , 'points' , ), 164, (164, (), [ (19, 0, None, None) , (4, 0, None, None) , 
			 (4, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1360 , (3, 0, None, None) , 0 , )),
	(( 'glMap2f' , 'target' , 'u1' , 'u2' , 'ustride' , 
			 'uorder' , 'v1' , 'v2' , 'vstride' , 'vorder' , 
			 'points' , ), 165, (165, (), [ (19, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (3, 0, None, None) , 
			 (3, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1368 , (3, 0, None, None) , 0 , )),
	(( 'glMapGrid1d' , 'un' , 'u1' , 'u2' , ), 166, (166, (), [ 
			 (3, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 1376 , (3, 0, None, None) , 0 , )),
	(( 'glMapGrid1f' , 'un' , 'u1' , 'u2' , ), 167, (167, (), [ 
			 (3, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1384 , (3, 0, None, None) , 0 , )),
	(( 'glMapGrid2d' , 'un' , 'u1' , 'u2' , 'vn' , 
			 'v1' , 'v2' , ), 168, (168, (), [ (3, 0, None, None) , (5, 0, None, None) , 
			 (5, 0, None, None) , (3, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 1392 , (3, 0, None, None) , 0 , )),
	(( 'glMapGrid2f' , 'un' , 'u1' , 'u2' , 'vn' , 
			 'v1' , 'v2' , ), 169, (169, (), [ (3, 0, None, None) , (4, 0, None, None) , 
			 (4, 0, None, None) , (3, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1400 , (3, 0, None, None) , 0 , )),
	(( 'glMaterialf' , 'face' , 'PName' , 'param' , ), 170, (170, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1408 , (3, 0, None, None) , 0 , )),
	(( 'glMaterialfv' , 'face' , 'PName' , 'Params' , ), 171, (171, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1416 , (3, 0, None, None) , 0 , )),
	(( 'glMateriali' , 'face' , 'PName' , 'param' , ), 172, (172, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 1424 , (3, 0, None, None) , 0 , )),
	(( 'glMaterialiv' , 'face' , 'PName' , 'Params' , ), 173, (173, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 1432 , (3, 0, None, None) , 0 , )),
	(( 'glMatrixMode' , 'Mode' , ), 174, (174, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 1440 , (3, 0, None, None) , 0 , )),
	(( 'glMultMatrixd' , 'm' , ), 175, (175, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 1448 , (3, 0, None, None) , 0 , )),
	(( 'glMultMatrixf' , 'm' , ), 176, (176, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1456 , (3, 0, None, None) , 0 , )),
	(( 'glNewList' , 'list' , 'Mode' , ), 177, (177, (), [ (19, 0, None, None) , 
			 (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 1464 , (3, 0, None, None) , 0 , )),
	(( 'glNormal3b' , 'nx' , 'ny' , 'nz' , ), 178, (178, (), [ 
			 (16, 0, None, None) , (16, 0, None, None) , (16, 0, None, None) , ], 1 , 1 , 4 , 0 , 1472 , (3, 0, None, None) , 0 , )),
	(( 'glNormal3bv' , 'v' , ), 179, (179, (), [ (16400, 0, None, None) , ], 1 , 1 , 4 , 0 , 1480 , (3, 0, None, None) , 0 , )),
	(( 'glNormal3d' , 'nx' , 'ny' , 'nz' , ), 180, (180, (), [ 
			 (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 1488 , (3, 0, None, None) , 0 , )),
	(( 'glNormal3dv' , 'v' , ), 181, (181, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 1496 , (3, 0, None, None) , 0 , )),
	(( 'glNormal3f' , 'nx' , 'ny' , 'nz' , ), 182, (182, (), [ 
			 (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1504 , (3, 0, None, None) , 0 , )),
	(( 'glNormal3fv' , 'v' , ), 183, (183, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1512 , (3, 0, None, None) , 0 , )),
	(( 'glNormal3i' , 'nx' , 'ny' , 'nz' , ), 184, (184, (), [ 
			 (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 1520 , (3, 0, None, None) , 0 , )),
	(( 'glNormal3iv' , 'v' , ), 185, (185, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 1528 , (3, 0, None, None) , 0 , )),
	(( 'glNormal3s' , 'nx' , 'ny' , 'nz' , ), 186, (186, (), [ 
			 (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 1536 , (3, 0, None, None) , 0 , )),
	(( 'glNormal3sv' , 'v' , ), 187, (187, (), [ (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 1544 , (3, 0, None, None) , 0 , )),
	(( 'glNormalPointer' , 'type' , 'stride' , 'pointer' , ), 188, (188, (), [ 
			 (19, 0, None, None) , (3, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 1552 , (3, 0, None, None) , 0 , )),
	(( 'glOrtho' , 'Left' , 'Right' , 'Bottom' , 'Top' , 
			 'zNear' , 'zFar' , ), 189, (189, (), [ (5, 0, None, None) , (5, 0, None, None) , 
			 (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 1560 , (3, 0, None, None) , 0 , )),
	(( 'glPassThrough' , 'token' , ), 190, (190, (), [ (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1568 , (3, 0, None, None) , 0 , )),
	(( 'glPixelMapfv' , 'map' , 'mapsize' , 'Values' , ), 191, (191, (), [ 
			 (19, 0, None, None) , (3, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1576 , (3, 0, None, None) , 0 , )),
	(( 'glPixelMapuiv' , 'map' , 'mapsize' , 'Values' , ), 192, (192, (), [ 
			 (19, 0, None, None) , (3, 0, None, None) , (16403, 0, None, None) , ], 1 , 1 , 4 , 0 , 1584 , (3, 0, None, None) , 0 , )),
	(( 'glPixelMapusv' , 'map' , 'mapsize' , 'Values' , ), 193, (193, (), [ 
			 (19, 0, None, None) , (3, 0, None, None) , (16402, 0, None, None) , ], 1 , 1 , 4 , 0 , 1592 , (3, 0, None, None) , 0 , )),
	(( 'glPixelStoref' , 'PName' , 'param' , ), 194, (194, (), [ (19, 0, None, None) , 
			 (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1600 , (3, 0, None, None) , 0 , )),
	(( 'glPixelStorei' , 'PName' , 'param' , ), 195, (195, (), [ (19, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 1608 , (3, 0, None, None) , 0 , )),
	(( 'glPixelTransferf' , 'PName' , 'param' , ), 196, (196, (), [ (19, 0, None, None) , 
			 (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1616 , (3, 0, None, None) , 0 , )),
	(( 'glPixelTransferi' , 'PName' , 'param' , ), 197, (197, (), [ (19, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 1624 , (3, 0, None, None) , 0 , )),
	(( 'glPixelZoom' , 'xfactor' , 'yfactor' , ), 198, (198, (), [ (4, 0, None, None) , 
			 (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1632 , (3, 0, None, None) , 0 , )),
	(( 'glPointSize' , 'size' , ), 199, (199, (), [ (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1640 , (3, 0, None, None) , 0 , )),
	(( 'glPolygonMode' , 'face' , 'Mode' , ), 200, (200, (), [ (19, 0, None, None) , 
			 (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 1648 , (3, 0, None, None) , 0 , )),
	(( 'glPolygonOffset' , 'factor' , 'units' , ), 201, (201, (), [ (4, 0, None, None) , 
			 (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1656 , (3, 0, None, None) , 0 , )),
	(( 'glPolygonStipple' , 'Mask' , ), 202, (202, (), [ (16401, 0, None, None) , ], 1 , 1 , 4 , 0 , 1664 , (3, 0, None, None) , 0 , )),
	(( 'glPopAttrib' , ), 203, (203, (), [ ], 1 , 1 , 4 , 0 , 1672 , (3, 0, None, None) , 0 , )),
	(( 'glPopClientAttrib' , ), 204, (204, (), [ ], 1 , 1 , 4 , 0 , 1680 , (3, 0, None, None) , 0 , )),
	(( 'glPopMatrix' , ), 205, (205, (), [ ], 1 , 1 , 4 , 0 , 1688 , (3, 0, None, None) , 0 , )),
	(( 'glPopName' , ), 206, (206, (), [ ], 1 , 1 , 4 , 0 , 1696 , (3, 0, None, None) , 0 , )),
	(( 'glPrioritizeTextures' , 'n' , 'textures' , 'priorities' , ), 207, (207, (), [ 
			 (3, 0, None, None) , (16403, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1704 , (3, 0, None, None) , 0 , )),
	(( 'glPushAttrib' , 'Mask' , ), 208, (208, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 1712 , (3, 0, None, None) , 0 , )),
	(( 'glPushClientAttrib' , 'Mask' , ), 209, (209, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 1720 , (3, 0, None, None) , 0 , )),
	(( 'glPushMatrix' , ), 210, (210, (), [ ], 1 , 1 , 4 , 0 , 1728 , (3, 0, None, None) , 0 , )),
	(( 'glPushName' , 'name' , ), 211, (211, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 1736 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos2d' , 'x' , 'y' , ), 212, (212, (), [ (5, 0, None, None) , 
			 (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 1744 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos2dv' , 'v' , ), 213, (213, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 1752 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos2f' , 'x' , 'y' , ), 214, (214, (), [ (4, 0, None, None) , 
			 (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1760 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos2fv' , 'v' , ), 215, (215, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1768 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos2i' , 'x' , 'y' , ), 216, (216, (), [ (3, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 1776 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos2iv' , 'v' , ), 217, (217, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 1784 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos2s' , 'x' , 'y' , ), 218, (218, (), [ (2, 0, None, None) , 
			 (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 1792 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos2sv' , 'v' , ), 219, (219, (), [ (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 1800 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos3d' , 'x' , 'y' , 'z' , ), 220, (220, (), [ 
			 (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 1808 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos3dv' , 'v' , ), 221, (221, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 1816 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos3f' , 'x' , 'y' , 'z' , ), 222, (222, (), [ 
			 (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1824 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos3fv' , 'v' , ), 223, (223, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1832 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos3i' , 'x' , 'y' , 'z' , ), 224, (224, (), [ 
			 (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 1840 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos3iv' , 'v' , ), 225, (225, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 1848 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos3s' , 'x' , 'y' , 'z' , ), 226, (226, (), [ 
			 (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 1856 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos3sv' , 'v' , ), 227, (227, (), [ (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 1864 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos4d' , 'x' , 'y' , 'z' , 'w' , 
			 ), 228, (228, (), [ (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 1872 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos4dv' , 'v' , ), 229, (229, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 1880 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos4f' , 'x' , 'y' , 'z' , 'w' , 
			 ), 230, (230, (), [ (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1888 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos4fv' , 'v' , ), 231, (231, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1896 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos4i' , 'x' , 'y' , 'z' , 'w' , 
			 ), 232, (232, (), [ (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 1904 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos4iv' , 'v' , ), 233, (233, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 1912 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos4s' , 'x' , 'y' , 'z' , 'w' , 
			 ), 234, (234, (), [ (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 1920 , (3, 0, None, None) , 0 , )),
	(( 'glRasterPos4sv' , 'v' , ), 235, (235, (), [ (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 1928 , (3, 0, None, None) , 0 , )),
	(( 'glReadBuffer' , 'Mode' , ), 236, (236, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 1936 , (3, 0, None, None) , 0 , )),
	(( 'glReadPixels' , 'x' , 'y' , 'Width' , 'height' , 
			 'format' , 'type' , 'pixels' , ), 237, (237, (), [ (3, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (19, 0, None, None) , (19, 0, None, None) , 
			 (16396, 0, None, None) , ], 1 , 1 , 4 , 0 , 1944 , (3, 0, None, None) , 0 , )),
	(( 'glRectd' , 'x1' , 'y1' , 'x2' , 'y2' , 
			 ), 238, (238, (), [ (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 1952 , (3, 0, None, None) , 0 , )),
	(( 'glRectdv' , 'v1' , 'v2' , ), 239, (239, (), [ (16389, 0, None, None) , 
			 (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 1960 , (3, 0, None, None) , 0 , )),
	(( 'glRectf' , 'x1' , 'y1' , 'x2' , 'y2' , 
			 ), 240, (240, (), [ (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 1968 , (3, 0, None, None) , 0 , )),
	(( 'glRectfv' , 'v1' , 'v2' , ), 241, (241, (), [ (16388, 0, None, None) , 
			 (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 1976 , (3, 0, None, None) , 0 , )),
	(( 'glRecti' , 'x1' , 'y1' , 'x2' , 'y2' , 
			 ), 242, (242, (), [ (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 1984 , (3, 0, None, None) , 0 , )),
	(( 'glRectiv' , 'v1' , 'v2' , ), 243, (243, (), [ (16387, 0, None, None) , 
			 (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 1992 , (3, 0, None, None) , 0 , )),
	(( 'glRects' , 'x1' , 'y1' , 'x2' , 'y2' , 
			 ), 244, (244, (), [ (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 2000 , (3, 0, None, None) , 0 , )),
	(( 'glRectsv' , 'v1' , 'v2' , ), 245, (245, (), [ (16386, 0, None, None) , 
			 (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 2008 , (3, 0, None, None) , 0 , )),
	(( 'glRenderMode' , 'Mode' , 'pResult' , ), 246, (246, (), [ (19, 0, None, None) , 
			 (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 2016 , (3, 0, None, None) , 0 , )),
	(( 'glRotated' , 'Angle' , 'x' , 'y' , 'z' , 
			 ), 247, (247, (), [ (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 2024 , (3, 0, None, None) , 0 , )),
	(( 'glRotatef' , 'Angle' , 'x' , 'y' , 'z' , 
			 ), 248, (248, (), [ (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 2032 , (3, 0, None, None) , 0 , )),
	(( 'glScaled' , 'x' , 'y' , 'z' , ), 249, (249, (), [ 
			 (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 2040 , (3, 0, None, None) , 0 , )),
	(( 'glScalef' , 'x' , 'y' , 'z' , ), 250, (250, (), [ 
			 (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 2048 , (3, 0, None, None) , 0 , )),
	(( 'glScissor' , 'x' , 'y' , 'Width' , 'height' , 
			 ), 251, (251, (), [ (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 2056 , (3, 0, None, None) , 0 , )),
	(( 'glSelectBuffer' , 'size' , 'buffer' , ), 252, (252, (), [ (3, 0, None, None) , 
			 (16403, 0, None, None) , ], 1 , 1 , 4 , 0 , 2064 , (3, 0, None, None) , 0 , )),
	(( 'glShadeModel' , 'Mode' , ), 253, (253, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 2072 , (3, 0, None, None) , 0 , )),
	(( 'glStencilFunc' , 'func' , 'ref' , 'Mask' , ), 254, (254, (), [ 
			 (19, 0, None, None) , (3, 0, None, None) , (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 2080 , (3, 0, None, None) , 0 , )),
	(( 'glStencilMask' , 'Mask' , ), 255, (255, (), [ (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 2088 , (3, 0, None, None) , 0 , )),
	(( 'glStencilOp' , 'fail' , 'zfail' , 'zpass' , ), 256, (256, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (19, 0, None, None) , ], 1 , 1 , 4 , 0 , 2096 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord1d' , 's' , ), 257, (257, (), [ (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 2104 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord1dv' , 'v' , ), 258, (258, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 2112 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord1f' , 's' , ), 259, (259, (), [ (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 2120 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord1fv' , 'v' , ), 260, (260, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 2128 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord1i' , 's' , ), 261, (261, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 2136 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord1iv' , 'v' , ), 262, (262, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 2144 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord1s' , 's' , ), 263, (263, (), [ (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 2152 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord1sv' , 'v' , ), 264, (264, (), [ (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 2160 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord2d' , 's' , 't' , ), 265, (265, (), [ (5, 0, None, None) , 
			 (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 2168 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord2dv' , 'v' , ), 266, (266, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 2176 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord2f' , 's' , 't' , ), 267, (267, (), [ (4, 0, None, None) , 
			 (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 2184 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord2fv' , 'v' , ), 268, (268, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 2192 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord2i' , 's' , 't' , ), 269, (269, (), [ (3, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 2200 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord2iv' , 'v' , ), 270, (270, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 2208 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord2s' , 's' , 't' , ), 271, (271, (), [ (2, 0, None, None) , 
			 (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 2216 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord2sv' , 'v' , ), 272, (272, (), [ (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 2224 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord3d' , 's' , 't' , 'r' , ), 273, (273, (), [ 
			 (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 2232 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord3dv' , 'v' , ), 274, (274, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 2240 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord3f' , 's' , 't' , 'r' , ), 275, (275, (), [ 
			 (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 2248 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord3fv' , 'v' , ), 276, (276, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 2256 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord3i' , 's' , 't' , 'r' , ), 277, (277, (), [ 
			 (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 2264 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord3iv' , 'v' , ), 278, (278, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 2272 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord3s' , 's' , 't' , 'r' , ), 279, (279, (), [ 
			 (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 2280 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord3sv' , 'v' , ), 280, (280, (), [ (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 2288 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord4d' , 's' , 't' , 'r' , 'q' , 
			 ), 281, (281, (), [ (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 2296 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord4dv' , 'v' , ), 282, (282, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 2304 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord4f' , 's' , 't' , 'r' , 'q' , 
			 ), 283, (283, (), [ (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 2312 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord4fv' , 'v' , ), 284, (284, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 2320 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord4i' , 's' , 't' , 'r' , 'q' , 
			 ), 285, (285, (), [ (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 2328 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord4iv' , 'v' , ), 286, (286, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 2336 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord4s' , 's' , 't' , 'r' , 'q' , 
			 ), 287, (287, (), [ (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 2344 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoord4sv' , 'v' , ), 288, (288, (), [ (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 2352 , (3, 0, None, None) , 0 , )),
	(( 'glTexCoordPointer' , 'size' , 'type' , 'stride' , 'pointer' , 
			 ), 289, (289, (), [ (3, 0, None, None) , (19, 0, None, None) , (3, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 2360 , (3, 0, None, None) , 0 , )),
	(( 'glTexEnvf' , 'target' , 'PName' , 'param' , ), 290, (290, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 2368 , (3, 0, None, None) , 0 , )),
	(( 'glTexEnvfv' , 'target' , 'PName' , 'Params' , ), 291, (291, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 2376 , (3, 0, None, None) , 0 , )),
	(( 'glTexEnvi' , 'target' , 'PName' , 'param' , ), 292, (292, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 2384 , (3, 0, None, None) , 0 , )),
	(( 'glTexEnviv' , 'target' , 'PName' , 'Params' , ), 293, (293, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 2392 , (3, 0, None, None) , 0 , )),
	(( 'glTexGend' , 'coord' , 'PName' , 'param' , ), 294, (294, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 2400 , (3, 0, None, None) , 0 , )),
	(( 'glTexGendv' , 'coord' , 'PName' , 'Params' , ), 295, (295, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 2408 , (3, 0, None, None) , 0 , )),
	(( 'glTexGenf' , 'coord' , 'PName' , 'param' , ), 296, (296, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 2416 , (3, 0, None, None) , 0 , )),
	(( 'glTexGenfv' , 'coord' , 'PName' , 'Params' , ), 297, (297, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 2424 , (3, 0, None, None) , 0 , )),
	(( 'glTexGeni' , 'coord' , 'PName' , 'param' , ), 298, (298, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 2432 , (3, 0, None, None) , 0 , )),
	(( 'glTexGeniv' , 'coord' , 'PName' , 'Params' , ), 299, (299, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 2440 , (3, 0, None, None) , 0 , )),
	(( 'glTexImage1D' , 'target' , 'level' , 'internalFormat' , 'Width' , 
			 'border' , 'format' , 'type' , 'pixels' , ), 300, (300, (), [ 
			 (19, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , 
			 (19, 0, None, None) , (19, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 2448 , (3, 0, None, None) , 0 , )),
	(( 'glTexImage2D' , 'target' , 'level' , 'internalFormat' , 'Width' , 
			 'height' , 'border' , 'format' , 'type' , 'pixels' , 
			 ), 301, (301, (), [ (19, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , (19, 0, None, None) , (19, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 2456 , (3, 0, None, None) , 0 , )),
	(( 'glTexParameterf' , 'target' , 'PName' , 'param' , ), 302, (302, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 2464 , (3, 0, None, None) , 0 , )),
	(( 'glTexParameterfv' , 'target' , 'PName' , 'Params' , ), 303, (303, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 2472 , (3, 0, None, None) , 0 , )),
	(( 'glTexParameteri' , 'target' , 'PName' , 'param' , ), 304, (304, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 2480 , (3, 0, None, None) , 0 , )),
	(( 'glTexParameteriv' , 'target' , 'PName' , 'Params' , ), 305, (305, (), [ 
			 (19, 0, None, None) , (19, 0, None, None) , (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 2488 , (3, 0, None, None) , 0 , )),
	(( 'glTexSubImage1D' , 'target' , 'level' , 'xoffset' , 'Width' , 
			 'format' , 'type' , 'pixels' , ), 306, (306, (), [ (19, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (19, 0, None, None) , (19, 0, None, None) , 
			 (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 2496 , (3, 0, None, None) , 0 , )),
	(( 'glTexSubImage2D' , 'target' , 'level' , 'xoffset' , 'yoffset' , 
			 'Width' , 'height' , 'format' , 'type' , 'pixels' , 
			 ), 307, (307, (), [ (19, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , (19, 0, None, None) , (19, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 2504 , (3, 0, None, None) , 0 , )),
	(( 'glTranslated' , 'x' , 'y' , 'z' , ), 308, (308, (), [ 
			 (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 2512 , (3, 0, None, None) , 0 , )),
	(( 'glTranslatef' , 'x' , 'y' , 'z' , ), 309, (309, (), [ 
			 (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 2520 , (3, 0, None, None) , 0 , )),
	(( 'glVertex2d' , 'x' , 'y' , ), 310, (310, (), [ (5, 0, None, None) , 
			 (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 2528 , (3, 0, None, None) , 0 , )),
	(( 'glVertex2dv' , 'v' , ), 311, (311, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 2536 , (3, 0, None, None) , 0 , )),
	(( 'glVertex2f' , 'x' , 'y' , ), 312, (312, (), [ (4, 0, None, None) , 
			 (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 2544 , (3, 0, None, None) , 0 , )),
	(( 'glVertex2fv' , 'v' , ), 313, (313, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 2552 , (3, 0, None, None) , 0 , )),
	(( 'glVertex2i' , 'x' , 'y' , ), 314, (314, (), [ (3, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 2560 , (3, 0, None, None) , 0 , )),
	(( 'glVertex2iv' , 'v' , ), 315, (315, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 2568 , (3, 0, None, None) , 0 , )),
	(( 'glVertex2s' , 'x' , 'y' , ), 316, (316, (), [ (2, 0, None, None) , 
			 (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 2576 , (3, 0, None, None) , 0 , )),
	(( 'glVertex2sv' , 'v' , ), 317, (317, (), [ (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 2584 , (3, 0, None, None) , 0 , )),
	(( 'glVertex3d' , 'x' , 'y' , 'z' , ), 318, (318, (), [ 
			 (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 2592 , (3, 0, None, None) , 0 , )),
	(( 'glVertex3dv' , 'v' , ), 319, (319, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 2600 , (3, 0, None, None) , 0 , )),
	(( 'glVertex3f' , 'x' , 'y' , 'z' , ), 320, (320, (), [ 
			 (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 2608 , (3, 0, None, None) , 0 , )),
	(( 'glVertex3fv' , 'v' , ), 321, (321, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 2616 , (3, 0, None, None) , 0 , )),
	(( 'glVertex3i' , 'x' , 'y' , 'z' , ), 322, (322, (), [ 
			 (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 2624 , (3, 0, None, None) , 0 , )),
	(( 'glVertex3iv' , 'v' , ), 323, (323, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 2632 , (3, 0, None, None) , 0 , )),
	(( 'glVertex3s' , 'x' , 'y' , 'z' , ), 324, (324, (), [ 
			 (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 2640 , (3, 0, None, None) , 0 , )),
	(( 'glVertex3sv' , 'v' , ), 325, (325, (), [ (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 2648 , (3, 0, None, None) , 0 , )),
	(( 'glVertex4d' , 'x' , 'y' , 'z' , 'w' , 
			 ), 326, (326, (), [ (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , ], 1 , 1 , 4 , 0 , 2656 , (3, 0, None, None) , 0 , )),
	(( 'glVertex4dv' , 'v' , ), 327, (327, (), [ (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 2664 , (3, 0, None, None) , 0 , )),
	(( 'glVertex4f' , 'x' , 'y' , 'z' , 'w' , 
			 ), 328, (328, (), [ (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , (4, 0, None, None) , ], 1 , 1 , 4 , 0 , 2672 , (3, 0, None, None) , 0 , )),
	(( 'glVertex4fv' , 'v' , ), 329, (329, (), [ (16388, 0, None, None) , ], 1 , 1 , 4 , 0 , 2680 , (3, 0, None, None) , 0 , )),
	(( 'glVertex4i' , 'x' , 'y' , 'z' , 'w' , 
			 ), 330, (330, (), [ (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 2688 , (3, 0, None, None) , 0 , )),
	(( 'glVertex4iv' , 'v' , ), 331, (331, (), [ (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 2696 , (3, 0, None, None) , 0 , )),
	(( 'glVertex4s' , 'x' , 'y' , 'z' , 'w' , 
			 ), 332, (332, (), [ (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , (2, 0, None, None) , ], 1 , 1 , 4 , 0 , 2704 , (3, 0, None, None) , 0 , )),
	(( 'glVertex4sv' , 'v' , ), 333, (333, (), [ (16386, 0, None, None) , ], 1 , 1 , 4 , 0 , 2712 , (3, 0, None, None) , 0 , )),
	(( 'glVertexPointer' , 'size' , 'type' , 'stride' , 'pointer' , 
			 ), 334, (334, (), [ (3, 0, None, None) , (19, 0, None, None) , (3, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 2720 , (3, 0, None, None) , 0 , )),
	(( 'glViewport' , 'x' , 'y' , 'Width' , 'height' , 
			 ), 335, (335, (), [ (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 2728 , (3, 0, None, None) , 0 , )),
	(( 'glMap2d' , 'target' , 'u1' , 'u2' , 'ustride' , 
			 'uorder' , 'v1' , 'v2' , 'vstride' , 'vorder' , 
			 'points' , ), 336, (336, (), [ (19, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , 
			 (3, 0, None, None) , (3, 0, None, None) , (5, 0, None, None) , (5, 0, None, None) , (3, 0, None, None) , 
			 (3, 0, None, None) , (16389, 0, None, None) , ], 1 , 1 , 4 , 0 , 2736 , (3, 0, None, None) , 0 , )),
]

IOpenGLViewEvents_vtables_dispatch_ = 1
IOpenGLViewEvents_vtables_ = [
	(( 'OnEvent' , 'EventType' , 'Param1' , 'Param2' , 'Param3' , 
			 'Param4' , 'Param5' , 'Param6' , 'Param7' , ), 1, (1, (), [ 
			 (3, 0, None, None) , (12, 0, None, None) , (12, 0, None, None) , (12, 0, None, None) , (12, 0, None, None) , 
			 (12, 0, None, None) , (12, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
]

IPropertyWindow_vtables_dispatch_ = 1
IPropertyWindow_vtables_ = [
	(( 'RemoveAll' , ), 1, (1, (), [ ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'EnableHeaderCtrl' , 'bEnable' , ), 2, (2, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'EnableDescriptionArea' , 'bEnable' , ), 3, (3, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'SetVSDotNetLook' , 'bEnable' , ), 4, (4, (), [ (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'MarkModifiedProperties' , 'bMark' , 'bRedraw' , ), 5, (5, (), [ (3, 0, None, None) , 
			 (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'EnableHeaderCtrlEx' , 'bEnable' , 'LeftColumnName' , 'RightColumnName' , ), 6, (6, (), [ 
			 (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'AddPropertyGroup' , 'PropertyGroupName' , ), 7, (7, (), [ (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
	(( 'AddPropertyItem' , 'GroupName' , 'ItemPropertyName' , 'pValue' , 'Description' , 
			 'EditMode' , ), 8, (8, (), [ (8, 0, None, None) , (8, 0, None, None) , (12, 0, None, None) , 
			 (8, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 112 , (3, 0, None, None) , 0 , )),
	(( 'SetPropListFont' , ), 9, (9, (), [ ], 1 , 1 , 4 , 0 , 120 , (3, 0, None, None) , 0 , )),
	(( 'AdjustLayout' , ), 10, (10, (), [ ], 1 , 1 , 4 , 0 , 128 , (3, 0, None, None) , 0 , )),
	(( 'GetValue' , 'GroupName' , 'PropertyName' , 'Value' , ), 11, (11, (), [ 
			 (8, 0, None, None) , (8, 0, None, None) , (16396, 0, None, None) , ], 1 , 1 , 4 , 0 , 136 , (3, 0, None, None) , 0 , )),
	(( 'AddPropertyItemAsString' , 'GroupName' , 'PropertyName' , 'Value' , 'Description' , 
			 ), 12, (12, (), [ (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 144 , (3, 0, None, None) , 0 , )),
	(( 'GetValueAsString' , 'GroupName' , 'PropertyName' , 'pValue' , ), 13, (13, (), [ 
			 (8, 0, None, None) , (8, 0, None, None) , (16392, 0, None, None) , ], 1 , 1 , 4 , 0 , 152 , (3, 0, None, None) , 0 , )),
	(( 'AddPropertyItemsAsString' , 'GroupName' , 'PropertyName' , 'Values' , 'DefValue' , 
			 'Desc' , 'EditMode' , ), 14, (14, (), [ (8, 0, None, None) , (8, 0, None, None) , 
			 (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 160 , (3, 0, None, None) , 0 , )),
	(( 'AddFilePathItem' , 'GroupName' , 'PropertyName' , 'DefValue' , 'bIsFilePath' , 
			 'ExtensionFilter' , 'DefExt' , 'Description' , ), 15, (15, (), [ (8, 0, None, None) , 
			 (8, 0, None, None) , (8, 0, None, None) , (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , 
			 (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 168 , (3, 0, None, None) , 0 , )),
	(( 'AddColorPropertyItem' , 'GroupName' , 'PropertyName' , 'DefaultValue' , 'Description' , 
			 ), 16, (16, (), [ (8, 0, None, None) , (8, 0, None, None) , (21, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 176 , (3, 0, None, None) , 0 , )),
	(( 'AddHierarchyItem' , 'GroupName' , 'SubGroupItemNames' , 'PropertyName' , 'Description' , 
			 'ItemType' , 'DefValue' , 'AddParam1' , 'AddParam2' , 'AddParam3' , 
			 'AddParam4' , ), 17, (17, (), [ (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , 
			 (8, 0, None, None) , (3, 0, None, None) , (12, 0, None, None) , (12, 0, None, None) , (12, 0, None, None) , 
			 (12, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 184 , (3, 0, None, None) , 0 , )),
	(( 'AddCustomPropertyItem' , 'GroupName' , 'CustMFCPropWnd' , ), 18, (18, (), [ (8, 0, None, None) , 
			 (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 192 , (3, 0, None, None) , 0 , )),
	(( 'Window' , 'pVal' , ), 19, (19, (), [ (16396, 10, None, None) , ], 1 , 2 , 4 , 0 , 200 , (3, 0, None, None) , 0 , )),
	(( 'PropGrdWindow' , 'pVal' , ), 20, (20, (), [ (16396, 10, None, None) , ], 1 , 2 , 4 , 0 , 208 , (3, 0, None, None) , 0 , )),
]

IPropertyWindowEvents_vtables_dispatch_ = 1
IPropertyWindowEvents_vtables_ = [
	(( 'OnPropertyChanged' , 'GroupName' , 'PropertyName' , 'PropertyValue' , ), 1, (1, (), [ 
			 (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
]

IRibbonToolbar_vtables_dispatch_ = 1
IRibbonToolbar_vtables_ = [
	(( 'SetAddinRibbonToolbarInfo' , 'lSessionID' , 'TabName' , 'PanelGroupName' , 'ControlType' , 
			 'ControlName' , 'CtrlID' , 'ControlFunction' , 'Desc' , 'ShortcutKey' , 
			 'RibbonToolbarIndex' , ), 1, (1, (), [ (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , 
			 (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , 
			 (8, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'SetAddinRibbonMenuInfo' , 'lSessionID' , 'InsertBeforeMainMenu' , 'MenuName' , 'CtrlID' , 
			 'FunctionName' , 'Desc' , 'ShortcutKey' , 'MenuToolbarIndex' , ), 2, (2, (), [ 
			 (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , 
			 (8, 0, None, None) , (8, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'SetAddinRibbonToolbarButtonInfo' , 'lSessionID' , 'TabName' , 'PanelGroupName' , 'MenuName' , 
			 'ButtonName' , 'CtrlID' , 'FunctionName' , 'Desc' , 'ShortcutKey' , 
			 'RibbonToolbarIndex' , ), 3, (3, (), [ (3, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , 
			 (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , (8, 0, None, None) , 
			 (8, 0, None, None) , (3, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'RecalculateLayout' , ), 4, (4, (), [ ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
	(( 'Window' , 'pVal' , ), 5, (5, (), [ (16396, 10, None, None) , ], 1 , 2 , 4 , 0 , 88 , (3, 0, None, None) , 0 , )),
	(( 'SetControlText' , 'CrlID' , 'CtrlText' , ), 6, (6, (), [ (8, 0, None, None) , 
			 (8, 0, None, None) , ], 1 , 1 , 4 , 0 , 96 , (3, 0, None, None) , 0 , )),
	(( 'ReLoadMainAppExperimentList' , ), 7, (7, (), [ ], 1 , 1 , 4 , 0 , 104 , (3, 0, None, None) , 0 , )),
	(( 'GetControlText' , 'CtrlID' , 'pCtrlText' , ), 8, (8, (), [ (8, 0, None, None) , 
			 (16392, 0, None, None) , ], 1 , 1 , 4 , 0 , 112 , (3, 0, None, None) , 0 , )),
]

IRibbonToolbarEvents_vtables_dispatch_ = 1
IRibbonToolbarEvents_vtables_ = [
	(( 'OnBeforeAddinControlsLoad' , ), 1, (1, (), [ ], 1 , 1 , 4 , 0 , 56 , (3, 0, None, None) , 0 , )),
	(( 'OnAfterAddinControlsLoad' , ), 2, (2, (), [ ], 1 , 1 , 4 , 0 , 64 , (3, 0, None, None) , 0 , )),
	(( 'GetControlStatus' , 'CtrlID' , 'pStatus' , ), 3, (3, (), [ (8, 0, None, None) , 
			 (16387, 0, None, None) , ], 1 , 1 , 4 , 0 , 72 , (3, 0, None, None) , 0 , )),
	(( 'RibbonWndProc' , 'MsgID' , 'wParam' , 'lParam' , ), 4, (4, (), [ 
			 (3, 0, None, None) , (12, 0, None, None) , (12, 0, None, None) , ], 1 , 1 , 4 , 0 , 80 , (3, 0, None, None) , 0 , )),
]

RecordMap = {
}

CLSIDToClassMap = {
	'{1186BC6B-64A7-4415-BDF6-4B192BB27733}' : IAddin,
	'{70AFC313-A879-4012-812D-AA714AEF8C6F}' : Addin,
	'{9E6C4518-AF98-4654-B294-709D0440698B}' : IMainApplication,
	'{36F4F7BF-6A25-47A4-A5DB-5C84B82811A1}' : MainApplication,
	'{42C2B21E-0555-439C-8905-222DD02234FB}' : IMainApplicationEvents,
	'{D729E31D-CC69-48BF-AC5A-A5860DFD667B}' : MainApplicationEvents,
	'{76AF030A-35A8-405B-B649-ACE818A007BC}' : IMainWindow,
	'{005FD9DA-A89C-4D58-9F17-F1CAA02484F9}' : MainWindow,
	'{E03BD2AF-D443-4E17-AD85-C0D2004321C7}' : IApplicationDocument,
	'{9025BDCC-4135-4453-99F8-6FF16C1822DC}' : ApplicationDocument,
	'{497CADA6-3A37-4F5E-A41D-010221D7DEB2}' : IApplicationView,
	'{7F4547B3-FC11-4E6E-AFDA-AD77BE3A65A8}' : ApplicationView,
	'{AB315F3B-305B-44D1-871B-65FAD00B2038}' : IApplicationSettings,
	'{8BCCE11F-D19F-4483-8737-EE54E5696D7F}' : ApplicationSettings,
	'{0ADD5A11-7B77-4094-AF8A-540B28C73D71}' : IRibbonToolbar,
	'{9A19345B-2F40-4A04-B171-6EB1385637BB}' : RibbonToolbar,
	'{CC1583FE-2AB3-4D68-B050-502C588F3FE8}' : IPropertyWindow,
	'{C73BF8A4-E4DC-48E9-8A8A-68695D71C777}' : PropertyWindow,
	'{5E1D1E5E-FA59-41EF-B2F0-45007F703110}' : IApplicationViewEvents,
	'{75CCE2A4-4996-4C5E-ACF0-5779519C7A18}' : ApplicationViewEvents,
	'{86A64F82-5E60-4D4D-A3D8-3B512805F847}' : IRibbonToolbarEvents,
	'{27609E1D-AB3E-4058-8574-E9CEE5577708}' : RibbonToolbarEvents,
	'{59950C9D-08FD-4C0D-B44C-E4C0F5C2E3AB}' : IPropertyWindowEvents,
	'{5ADFA2F2-A0F4-4EED-92C3-6E91013A1F64}' : PropertyWindowEvents,
	'{902AEB11-0B73-4BE9-9000-3FE482B340A6}' : IApplicationDocumentEvents,
	'{41E563DF-22CA-4D51-B40A-EE3DCD62DB6B}' : ApplicationDocumentEvents,
	'{C8EEE054-055F-42D5-9C32-AAE2F5DB8CA3}' : IExperiment,
	'{0F56F152-82CD-4B00-9A56-EAE622E22554}' : Experiment,
	'{92F075D5-45DE-40BF-954F-9BC097FC043B}' : IExperimentEvents,
	'{E9240855-9646-4564-971D-9958CB48CB5C}' : ExperimentEvents,
	'{CDFE7248-527E-4890-B1B3-24E372679CBE}' : IApplicationChart,
	'{9D8447A5-48EA-45D9-9946-F1153C1C4888}' : ApplicationChart,
	'{1BA3E124-9276-487A-9CAE-D9A2276E80F8}' : IGraphCtl,
	'{7D7E6BCD-0089-4DB9-B4E4-8FE903732413}' : GraphCtl,
	'{99F323B8-BD88-4DCE-AA10-4BA0061BAF24}' : IMainWindowEvents,
	'{FF8F1BC0-4715-4EB8-ADC7-F40154565E33}' : MainWindowEvents,
	'{30038C10-4BA8-4AE5-820F-D22D9C947ECF}' : IExperimentTreeView,
	'{894BE66A-5355-47A5-852C-822C1829FF34}' : ExperimentTreeView,
	'{845DFB21-303B-4751-951E-6F0DF8A013C1}' : IExperimentTreeViewEvents,
	'{A219A39D-2A0F-4658-A810-8AA440B19852}' : ExperimentTreeViewEvents,
	'{EE1F0283-ED2F-4D38-B2E4-FCCEB1D460F0}' : IFileSettingsTreeViewEvents,
	'{25A65E7E-E249-425F-90BD-4BD61C727883}' : FileSettingsTreeViewEvents,
	'{54ABD01E-3A09-4AB9-A594-A652A697F2B1}' : IFileSettingsTreeView,
	'{1D8EDC8A-F049-4BA0-8BC5-955A82BFC601}' : FileSettingsTreeView,
	'{0FFFBABB-9F1A-43E8-A2FF-6CF97D6D272A}' : IGraphCtrl2d,
	'{40C1D140-3B42-46EF-848D-B1BA009692EF}' : GraphCtrl2d,
	'{ADB4E5E0-CA47-44BF-9744-3178ADE86F6D}' : IOpenGLView,
	'{0234EA06-BACD-4930-8F94-11034FE5FCC1}' : OpenGLView,
	'{800A76A8-A08C-47CF-A985-EA4DEC364E2F}' : IOpenGLViewEvents,
	'{C6C4739C-4E00-488F-8024-76F793261B17}' : OpenGLViewEvents,
	'{C648D646-603D-4F95-BB6D-A8B6FBD0471A}' : IOpenGLUtilView,
	'{D37FFE74-89CB-4272-A00B-78AAE721793C}' : OpenGLUtilView,
	'{0A6B74CB-F443-4416-A121-A16031745EBF}' : IOpenGLUtilViewEvents,
	'{E6DC451D-1949-4E5E-A9DD-B1B1BBD4E298}' : OpenGLUtilViewEvents,
}
CLSIDToPackageMap = {}
win32com.client.CLSIDToClass.RegisterCLSIDsFromDict( CLSIDToClassMap )
VTablesToPackageMap = {}
VTablesToClassMap = {
	'{1186BC6B-64A7-4415-BDF6-4B192BB27733}' : 'IAddin',
	'{9E6C4518-AF98-4654-B294-709D0440698B}' : 'IMainApplication',
	'{42C2B21E-0555-439C-8905-222DD02234FB}' : 'IMainApplicationEvents',
	'{76AF030A-35A8-405B-B649-ACE818A007BC}' : 'IMainWindow',
	'{E03BD2AF-D443-4E17-AD85-C0D2004321C7}' : 'IApplicationDocument',
	'{497CADA6-3A37-4F5E-A41D-010221D7DEB2}' : 'IApplicationView',
	'{AB315F3B-305B-44D1-871B-65FAD00B2038}' : 'IApplicationSettings',
	'{0ADD5A11-7B77-4094-AF8A-540B28C73D71}' : 'IRibbonToolbar',
	'{CC1583FE-2AB3-4D68-B050-502C588F3FE8}' : 'IPropertyWindow',
	'{5E1D1E5E-FA59-41EF-B2F0-45007F703110}' : 'IApplicationViewEvents',
	'{86A64F82-5E60-4D4D-A3D8-3B512805F847}' : 'IRibbonToolbarEvents',
	'{59950C9D-08FD-4C0D-B44C-E4C0F5C2E3AB}' : 'IPropertyWindowEvents',
	'{902AEB11-0B73-4BE9-9000-3FE482B340A6}' : 'IApplicationDocumentEvents',
	'{C8EEE054-055F-42D5-9C32-AAE2F5DB8CA3}' : 'IExperiment',
	'{92F075D5-45DE-40BF-954F-9BC097FC043B}' : 'IExperimentEvents',
	'{CDFE7248-527E-4890-B1B3-24E372679CBE}' : 'IApplicationChart',
	'{1BA3E124-9276-487A-9CAE-D9A2276E80F8}' : 'IGraphCtl',
	'{99F323B8-BD88-4DCE-AA10-4BA0061BAF24}' : 'IMainWindowEvents',
	'{30038C10-4BA8-4AE5-820F-D22D9C947ECF}' : 'IExperimentTreeView',
	'{845DFB21-303B-4751-951E-6F0DF8A013C1}' : 'IExperimentTreeViewEvents',
	'{EE1F0283-ED2F-4D38-B2E4-FCCEB1D460F0}' : 'IFileSettingsTreeViewEvents',
	'{54ABD01E-3A09-4AB9-A594-A652A697F2B1}' : 'IFileSettingsTreeView',
	'{0FFFBABB-9F1A-43E8-A2FF-6CF97D6D272A}' : 'IGraphCtrl2d',
	'{ADB4E5E0-CA47-44BF-9744-3178ADE86F6D}' : 'IOpenGLView',
	'{800A76A8-A08C-47CF-A985-EA4DEC364E2F}' : 'IOpenGLViewEvents',
	'{C648D646-603D-4F95-BB6D-A8B6FBD0471A}' : 'IOpenGLUtilView',
	'{0A6B74CB-F443-4416-A121-A16031745EBF}' : 'IOpenGLUtilViewEvents',
}


NamesToIIDMap = {
	'IAddin' : '{1186BC6B-64A7-4415-BDF6-4B192BB27733}',
	'IMainApplication' : '{9E6C4518-AF98-4654-B294-709D0440698B}',
	'IMainApplicationEvents' : '{42C2B21E-0555-439C-8905-222DD02234FB}',
	'IMainWindow' : '{76AF030A-35A8-405B-B649-ACE818A007BC}',
	'IApplicationDocument' : '{E03BD2AF-D443-4E17-AD85-C0D2004321C7}',
	'IApplicationView' : '{497CADA6-3A37-4F5E-A41D-010221D7DEB2}',
	'IApplicationSettings' : '{AB315F3B-305B-44D1-871B-65FAD00B2038}',
	'IRibbonToolbar' : '{0ADD5A11-7B77-4094-AF8A-540B28C73D71}',
	'IPropertyWindow' : '{CC1583FE-2AB3-4D68-B050-502C588F3FE8}',
	'IApplicationViewEvents' : '{5E1D1E5E-FA59-41EF-B2F0-45007F703110}',
	'IRibbonToolbarEvents' : '{86A64F82-5E60-4D4D-A3D8-3B512805F847}',
	'IPropertyWindowEvents' : '{59950C9D-08FD-4C0D-B44C-E4C0F5C2E3AB}',
	'IApplicationDocumentEvents' : '{902AEB11-0B73-4BE9-9000-3FE482B340A6}',
	'IExperiment' : '{C8EEE054-055F-42D5-9C32-AAE2F5DB8CA3}',
	'IExperimentEvents' : '{92F075D5-45DE-40BF-954F-9BC097FC043B}',
	'IApplicationChart' : '{CDFE7248-527E-4890-B1B3-24E372679CBE}',
	'IGraphCtl' : '{1BA3E124-9276-487A-9CAE-D9A2276E80F8}',
	'IMainWindowEvents' : '{99F323B8-BD88-4DCE-AA10-4BA0061BAF24}',
	'IExperimentTreeView' : '{30038C10-4BA8-4AE5-820F-D22D9C947ECF}',
	'IExperimentTreeViewEvents' : '{845DFB21-303B-4751-951E-6F0DF8A013C1}',
	'IFileSettingsTreeViewEvents' : '{EE1F0283-ED2F-4D38-B2E4-FCCEB1D460F0}',
	'IFileSettingsTreeView' : '{54ABD01E-3A09-4AB9-A594-A652A697F2B1}',
	'IGraphCtrl2d' : '{0FFFBABB-9F1A-43E8-A2FF-6CF97D6D272A}',
	'IOpenGLView' : '{ADB4E5E0-CA47-44BF-9744-3178ADE86F6D}',
	'IOpenGLViewEvents' : '{800A76A8-A08C-47CF-A985-EA4DEC364E2F}',
	'IOpenGLUtilView' : '{C648D646-603D-4F95-BB6D-A8B6FBD0471A}',
	'IOpenGLUtilViewEvents' : '{0A6B74CB-F443-4416-A121-A16031745EBF}',
}


