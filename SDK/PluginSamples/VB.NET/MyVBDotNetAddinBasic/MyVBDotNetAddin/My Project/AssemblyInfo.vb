Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.
<Assembly: AssemblyTitle("MyVbDotNetAddin")>
<Assembly: AssemblyDescription("")>
<Assembly: AssemblyConfiguration("")>
<Assembly: AssemblyCompany("HP Inc.")>
<Assembly: AssemblyProduct("MyVbDotNetAddin")>
<Assembly: AssemblyCopyright("Copyright © HP Inc. 2021")>
<Assembly: AssemblyTrademark("")>
<Assembly: AssemblyCulture("")>

' Setting ComVisible to false makes the types in this assembly not visible 
' to COM components.  If you need to access a type in this assembly from 
' COM, set the ComVisible attribute to true on that type.
<Assembly: ComVisible(True)>

' The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("9c2ea237-2863-4a16-9d86-046c7ecececb")>

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' [assembly: AssemblyVersion("1.0.*")]
<Assembly: AssemblyDelaySign(False)>
<Assembly: AssemblyVersion("1.0.0.0")>
<Assembly: AssemblyFileVersion("1.0.0.0")>

'To Register this COM dll run the command below => Eg for project Directory "C:\MyVbDotNetAddin\"
' for x64 - "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe" /register /codebase "C:\MyVbDotNetAddin\MyVbDotNetAddin\Release\MyVbDotNetAddin.dll"
' for x86 - "C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe" /register /codebase "C:\MyVbDotNetAddin\MyVbDotNetAddin\Release\MyVbDotNetAddin.dll"
