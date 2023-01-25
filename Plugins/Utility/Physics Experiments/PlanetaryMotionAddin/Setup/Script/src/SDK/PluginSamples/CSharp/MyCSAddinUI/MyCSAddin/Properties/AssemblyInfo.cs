using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("MyCSAddin")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("HP Inc.")]
[assembly: AssemblyProduct("MyCSAddin")]
[assembly: AssemblyCopyright("Copyright © HP Inc. 2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(true)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("9c2ea237-2863-4a16-9d86-046c7ecececb")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

//To Register this COM dll run the command below => Eg for project Directory "C:\MyCSAddin\"
// for x64 - "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe" /register /codebase "C:\MyCSAddin\MyCSAddin\Release\MyCSAddin.dll"
// for x86 - "C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe" /register /codebase "C:\MyCSAddin\MyCSAddin\Release\MyCSAddin.dll"