using System.Collections.Immutable;
using System.Diagnostics;
using AncientMysteries.SourceGenerator.Generators;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace AncientMysteries.SourceGenerator.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class UseTextureRefAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "AM0001";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Res.Analyzer_UseTextureRef_Title), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Res.Analyzer_UseTextureRef_MessageFormat), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Res.Analyzer_UseTextureRef_Description), Res.ResourceManager, typeof(Res));
        private const string Category = "Usage";

        private static DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.StringLiteralExpression);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var syntax = (LiteralExpressionSyntax)context.Node;
            var a = syntax.GetText();
            string text = a.ToString(new TextSpan(a.Length - 5, 5));
            if (text.Equals(".png\"", StringComparison.OrdinalIgnoreCase))
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation(), TexturesReference.GetFieldName(a.ToString(new TextSpan(1, a.Length - 2)))));
            }
        }
    }
}
