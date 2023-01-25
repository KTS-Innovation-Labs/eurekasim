Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading.Tasks
Imports EurekaSim.Net
Imports System.Windows.Forms
Imports System.Reflection
Imports System.IO
Imports System.Xml
Imports System.Drawing

Namespace MyVbDotNetAddin

	'Change this GUID for your own plugin...


	<ClassInterface(ClassInterfaceType.AutoDispatch)>
	<Guid("CA20D230-8AB6-4df7-B77C-06C3C4F9EC17")>
	Public Class MyVbDotNetAddinImp
		Implements EurekaSim.Net.Addin

		#Region "Component Category Registration"



		<ComRegisterFunction>
		<ComVisible(False)>
		Public Shared Sub RegisterFunction(ByVal t As Type)


			Dim sKey As String = "\CLSID\{" & t.GUID.ToString() & "}\Implemented Categories"

			Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(sKey, True)

			If regKey IsNot Nothing Then


				regKey.CreateSubKey("{66EBC8D5-A4D0-48C4-978B-55D1B34E157B}")

			End If

		End Sub





		<ComUnregisterFunction>
		<ComVisible(False)>
		Public Shared Sub UnregisterFunction(ByVal t As Type)


			Dim sKey As String = "\CLSID\{" & t.GUID.ToString() & "}\Implemented Categories"

			Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(sKey, True)

			If regKey IsNot Nothing Then


				regKey.DeleteSubKey("{66EBC8D5-A4D0-48C4-978B-55D1B34E157B}")


			End If

		End Sub

		Public Sub About() Implements EurekaSim.Net.IAddin.About
			MessageBox.Show("The C# Addin About Box")
		End Sub

		Public Sub Initialize(ByVal lSessionID As Integer, ByVal pApp As MainApplication, ByVal bFirstTime As Object) Implements EurekaSim.Net.IAddin.Initialize
			' TODO:  Add CAddinDotNet.SalesMatePlusLib.ISmpAddin.Initialize implementation
			'MessageBox.Show("C#.Initialize");
			Dim thisAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
			'Read the embedded XML menu resource
			Dim rgbxml As Stream = thisAssembly.GetManifestResourceStream("AddinRibbon.xml")

			Dim rgbBmp As Stream = thisAssembly.GetManifestResourceStream("toolbar1.bmp")

			Dim bmp As New Bitmap(rgbBmp)

			Dim doc As New XmlDocument()
			doc.Load(rgbxml)
			Dim strMenuXML As String = doc.InnerXml
			'MessageBox.Show(strMenuXML);
			Dim bitMapHandle As IntPtr = bmp.GetHbitmap()

			pApp.SetAddInInfo(lSessionID, 0, strMenuXML, CLng(bitMapHandle), "")
		End Sub

		Public Sub Uninitialize(ByVal lSessionID As Integer) Implements EurekaSim.Net.IAddin.Uninitialize
			GC.Collect()
		End Sub

		Public Sub InvokePreferenceSettings()
			MessageBox.Show("C#.InvokePreferenceSettings")
		End Sub

		Public Sub InvokeDefaultSettings()
			MessageBox.Show("C#.InvokeDefaultSettings")
		End Sub

		#End Region
	End Class
End Namespace
