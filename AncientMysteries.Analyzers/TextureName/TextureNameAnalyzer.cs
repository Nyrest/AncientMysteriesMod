using System;
using System.Collections.Generic;
using System.Text;

namespace AncientMysteries.Analyzers.UnclassifiedTexture
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class TextureNameAnalyzer : DiagnosticAnalyzer
    {
        #region Invalid Texture
        public const string InvalidTexture_DiagnosticId = "AM0002";

        private static readonly LocalizableString InvalidTexture_Title
            = new LocalizableResourceString(nameof(Res.Analyzer_InvalidTexture_Title), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString InvalidTexture_MessageFormat
            = new LocalizableResourceString(nameof(Res.Analyzer_InvalidTexture_MessageFormat), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString InvalidTexture_Description
            = new LocalizableResourceString(nameof(Res.Analyzer_InvalidTexture_Description), Res.ResourceManager, typeof(Res));
        private const string InvalidTexture_Category = "Usage";

        private static readonly DiagnosticDescriptor InvalidTexture_Rule = new(
            InvalidTexture_DiagnosticId,
            InvalidTexture_Title,
            InvalidTexture_MessageFormat,
            InvalidTexture_Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: InvalidTexture_Description);
        #endregion

        #region Unclassified Texture
        public const string Unclassified_DiagnosticId = "AM0003";

        private static readonly LocalizableString Unclassified_Title
            = new LocalizableResourceString(nameof(Res.Analyzer_UnclassifiedTexture_Title), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString Unclassified_MessageFormat
            = new LocalizableResourceString(nameof(Res.Analyzer_UnclassifiedTexture_MessageFormat), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString Unclassified_Description
            = new LocalizableResourceString(nameof(Res.Analyzer_UnclassifiedTexture_Description), Res.ResourceManager, typeof(Res));
        private const string Unclassified_Category = "Usage";

        private static readonly DiagnosticDescriptor Unclassified_Rule = new(
            Unclassified_DiagnosticId,
            Unclassified_Title,
            Unclassified_MessageFormat,
            Unclassified_Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            description: Unclassified_Description);
        #endregion

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(InvalidTexture_Rule, Unclassified_Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterCompilationAction(Analyze);
        }

        private void Analyze(CompilationAnalysisContext context)
        {
            context.Options.AnalyzerConfigOptionsProvider.GlobalOptions.TryGetValue("build_property.projectdir", out string projectDir);
            foreach (var fullname in Directory.GetFiles(projectDir + "content", "*.*"))
            {
                if (IsException(fullname)) continue;
                #region Invalid Texture
                if (!Path.GetExtension(fullname).Equals(".png", StringComparison.OrdinalIgnoreCase))
                {
                    context.ReportDiagnostic(Diagnostic.Create(InvalidTexture_Rule, Location.None, Path.GetFileName(fullname)));
                }
                #endregion

                #region Invalid Texture
                if (Path.GetFileName(fullname).IndexOf('_') == -1)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Unclassified_Rule, Location.None, Path.GetFileName(fullname)));
                }
                #endregion
            }
        }
        private static string previewPng = Path.DirectorySeparatorChar + "preview.png";
        private static string screenshotPng = Path.DirectorySeparatorChar + "screenshot.png";
        private static bool IsException(string fullname)
        {
            if (fullname.EndsWith(previewPng, StringComparison.Ordinal)) return true;
            if (fullname.EndsWith(screenshotPng, StringComparison.Ordinal)) return true;
            return false;
        }
    }
}
