using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.Host;

namespace AncientMysteries.Analyzers.MissingMetadata
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MissingMetadataAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "AM0005";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Res.Analyzer_MissingMetadata_Title), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Res.Analyzer_MissingMetadata_MessageFormat), Res.ResourceManager, typeof(Res));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Res.Analyzer_MissingMetadata_Description), Res.ResourceManager, typeof(Res));
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
            if (symbol is null || symbol.IsAbstract || symbol.IsStatic) return;
            bool isLocalizable = false;
            foreach (var item in symbol.AllInterfaces)
            {
                if (item.Name.Equals("IAMLocalizable"))
                {
                    isLocalizable = true;
                    break;
                }
            }
            if (!isLocalizable) return;
            var flags = GetMetadataFlags(symbol);
            if (flags == MetadataFlags.All) return;
            context.ReportDiagnostic(Diagnostic.Create(Rule, node.GetLocation()));
        }

        internal static MetadataFlags GetMetadataFlags(INamedTypeSymbol symbol)
        {
            MetadataFlags result = MetadataFlags.None;
            foreach (var item in symbol.GetAttributes())
            {
                if (item.AttributeClass is null) continue;
                string fullname = item.AttributeClass.ToDisplayString(NullableFlowState.NotNull);
                if (fullname.Equals("DuckGame.EditorGroupAttribute"))
                {
                    result |= MetadataFlags.HasEditorGroup;
                    continue;
                }
                if (fullname.Equals("AncientMysteries.MetaInfoAttribute"))
                {
                    var args = item.ConstructorArguments;
                    //if (args.Length == 0 || !args[0].Value.Equals(0)) continue;

                    result |= MetadataFlags.HasMetaInfo;

                    continue;
                }
                if (fullname.Equals("AncientMysteries.MetaImageAttribute"))
                {
                    result |= MetadataFlags.HasMetaImage;
                    continue;
                }
            }
            return result;
        }

        [Flags]
        internal enum MetadataFlags
        {
            None = 0,
            HasEditorGroup = 1 << 0,
            HasMetaInfo = 1 << 1,
            HasMetaImage = 2 << 2,
            All = HasEditorGroup | HasMetaInfo | HasMetaImage,
        }
    }
}
