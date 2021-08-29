#! "net5.0"
#r "nuget: Mono.Cecil, 0.11.4"
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;
using Mono.Cecil;
#nullable enable
string file;
var asmDef = Mono.Cecil.AssemblyDefinition.ReadAssembly(file);
