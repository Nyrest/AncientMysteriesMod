using System;
using System.Collections.Generic;
using System.Text;

namespace AncientMysteries.Analyzers.MarkPartial
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MakePartialAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "AM0004";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Res.Analyzer_MakePartial_Title), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Res.Analyzer_MakePartial_MessageFormat), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Res.Analyzer_MakePartial_Description), Res.ResourceManager, typeof(Res));
        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ClassDeclaration);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var node = (ClassDeclarationSyntax)context.Node;
            var symbol = context.SemanticModel.GetDeclaredSymbol(node);
            if (symbol.IsAbstract || symbol.IsStatic) return;
            bool isLocalizable = false;
            foreach (var item in symbol.AllInterfaces)
            {
                if (item.Name.Contains("IAMLocalizable"))
                {
                    isLocalizable = true;
                    break;
                }
            }
            if (!isLocalizable) return;
            if (node.Modifiers.ToString().IndexOf("partial") != -1) return;
            context.ReportDiagnostic(Diagnostic.Create(Rule, node.GetLocation()));
        }
    }

}
