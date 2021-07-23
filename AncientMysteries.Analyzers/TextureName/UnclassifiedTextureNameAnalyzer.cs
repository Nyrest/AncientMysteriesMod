namespace AncientMysteries.Analyzers.UnclassifiedTexture
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class UnclassifiedTextureNameAnalyzer : DiagnosticAnalyzer
    {
        #region Unclassified Texture

        public const string DiagnosticId = "AM0003";

        private static readonly LocalizableString Title
            = new LocalizableResourceString(nameof(Res.Analyzer_UnclassifiedTexture_Title), Res.ResourceManager, typeof(Res));

        private static readonly LocalizableString MessageFormat
            = new LocalizableResourceString(nameof(Res.Analyzer_UnclassifiedTexture_MessageFormat), Res.ResourceManager, typeof(Res));

        private static readonly LocalizableString Description
            = new LocalizableResourceString(nameof(Res.Analyzer_UnclassifiedTexture_Description), Res.ResourceManager, typeof(Res));

        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = new(
            DiagnosticId,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: Description);

        #endregion Unclassified Texture

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterCompilationAction(Analyze);
        }

        private void Analyze(CompilationAnalysisContext context)
        {
            context.Options.AnalyzerConfigOptionsProvider.GlobalOptions.TryGetValue("build_property.projectdir", out var projectDir);
            if (projectDir is null) throw new NullReferenceException(nameof(projectDir));
            foreach (var fullname in Directory.GetFiles(projectDir + "content", "*.*"))
            {
                if (IsException(fullname)) continue;

                #region Invalid Texture

                if (Path.GetExtension(fullname).Equals(".png", StringComparison.OrdinalIgnoreCase) && Path.GetFileName(fullname).IndexOf('_') == -1)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, Location.None, Path.GetFileName(fullname)));
                }

                #endregion Invalid Texture
            }
        }

        private static readonly string previewPng = Path.DirectorySeparatorChar + "preview.png";
        private static readonly string screenshotPng = Path.DirectorySeparatorChar + "screenshot.png";

        private static bool IsException(string fullname)
        {
            return fullname.EndsWith(previewPng, StringComparison.Ordinal) || fullname.EndsWith(screenshotPng, StringComparison.Ordinal);
        }
    }
}