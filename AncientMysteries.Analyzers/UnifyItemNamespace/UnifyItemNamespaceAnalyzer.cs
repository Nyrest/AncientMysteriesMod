using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AncientMysteries.Analyzers.UnifyItemNamespace
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class UnifyItemNamespaceAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "AM0002";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Res.Analyzer_UnifyItemNamespace_Title), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Res.Analyzer_UnifyItemNamespace_MessageFormat), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Res.Analyzer_UnifyItemNamespace_Description), Res.ResourceManager, typeof(Res));
        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.NamespaceDeclaration);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var node = (NamespaceDeclarationSyntax)context.Node;
            var name = node.Name.ToString();
            if (name.StartsWith("AncientMysteries.Items") && name.Length != "AncientMysteries.Items".Length)
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, node.GetLocation()));
            }
        }
    }
}
