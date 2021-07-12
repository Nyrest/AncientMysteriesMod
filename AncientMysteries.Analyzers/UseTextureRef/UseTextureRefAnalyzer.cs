using AncientMysteries.SourceGenerator.Generators;

namespace AncientMysteries.Analyzers.UseTextureRef
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class UseTextureRefAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "AM0001";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Res.Analyzer_UseTextureRef_Title), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Res.Analyzer_UseTextureRef_MessageFormat), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Res.Analyzer_UseTextureRef_Description), Res.ResourceManager, typeof(Res));
        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.StringLiteralExpression);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var sourceText = ((LiteralExpressionSyntax)context.Node).GetText();
            if (sourceText.Length < 6) return;
            string text = sourceText.ToString(new TextSpan(sourceText.Length - 5, 4));
            if (text.Equals(".png", StringComparison.OrdinalIgnoreCase))
            {
                if (Path.GetFileName(context.Node.SyntaxTree.FilePath) == "TextureReferences.cs") return;
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation(), TexturesReference.GetFieldName(sourceText.ToString(new TextSpan(1, sourceText.Length - 2)))));
            }
        }
    }
}
