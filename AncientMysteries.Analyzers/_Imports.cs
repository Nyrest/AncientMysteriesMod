global using System;
global using System.Collections.Immutable;
global using System.Composition;
global using System.IO;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Diagnostics;

global using Microsoft.CodeAnalysis;
global using Microsoft.CodeAnalysis.CodeActions;
global using Microsoft.CodeAnalysis.CodeFixes;
global using Microsoft.CodeAnalysis.CSharp;
global using Microsoft.CodeAnalysis.CSharp.Syntax;
global using Microsoft.CodeAnalysis.Diagnostics;
global using Microsoft.CodeAnalysis.Text;

global using Res = AncientMysteries.Analyzers.Properties.Resources;
global using SF = Microsoft.CodeAnalysis.CSharp.SyntaxFactory;