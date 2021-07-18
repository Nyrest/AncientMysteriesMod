using System.Collections.Generic;
using System.Text;
using static AncientMysteries.Analyzers.MissingMetadata.MissingMetadataAnalyzer;

namespace AncientMysteries.Analyzers.MissingMetadata
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(MissingMetadataCodeFix)), Shared]
    public class MissingMetadataCodeFix : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(DiagnosticId);

        public sealed override FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            if (root is null) throw new NullReferenceException(nameof(root));

            // TODO: Replace the following code with your own analysis, generating a CodeAction for each fix to suggest
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            // Find the type declaration identified by the diagnostic.
            var declaration = root.FindToken(diagnosticSpan.Start).Parent?.AncestorsAndSelf().OfType<ClassDeclarationSyntax>().First();
            if (declaration is null) throw new NullReferenceException(nameof(declaration));

            // Register a code action that will invoke the fix.
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: Res.CodeFix_MissingMetadata_Title,
                    createChangedDocument: c => GenerateCodeFixAsync(context.Document, declaration, c),
                    equivalenceKey: nameof(Res.CodeFix_MissingMetadata_Title)),
                diagnostic);
        }

        private static async Task<Document> GenerateCodeFixAsync(Document document,
            ClassDeclarationSyntax node,
            CancellationToken cancellationToken)
        {
            var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
            var symbol = semanticModel.GetDeclaredSymbol(node);
            if (symbol is null) throw new NullReferenceException(nameof(symbol));

            var syntaxTree = await document.GetSyntaxTreeAsync(cancellationToken);
            if (syntaxTree is null) throw new NullReferenceException(nameof(syntaxTree));

            var flags = GetMetadataFlags(symbol);
            List<AttributeSyntax> list = new(3);
            if ((flags & MetadataFlags.HasEditorGroup) == 0)
            {
                var args = SF.ParseAttributeArgumentList("(group_Unknown)");
                list.Add(SF.Attribute(SF.IdentifierName("EditorGroup"), args));
            }
            if ((flags & MetadataFlags.HasMetaImage) == 0)
            {
                var args = SF.ParseAttributeArgumentList("(t_)");
                list.Add(SF.Attribute(SF.IdentifierName("MetaImage"), args));
            }
            if ((flags & MetadataFlags.HasMetaInfo) == 0)
            {
                string className = symbol.Name;
                var args = SF.ParseAttributeArgumentList($"(Lang.english, \"{ParsePascalName(className)}\", \"desc\")");
                list.Add(SF.Attribute(SF.IdentifierName("MetaInfo"), args));
                var args2 = SF.ParseAttributeArgumentList($"(Lang.schinese, \"\", \"\")");
                list.Add(SF.Attribute(SF.IdentifierName("MetaInfo"), args2));
            }
            if (list.Count == 0) return document;
            var updatedNode = node;

            foreach (var item in list)
            {
                updatedNode = updatedNode.AddAttributeLists(SF.AttributeList(SF.SingletonSeparatedList(item)));
            }

            var root = await syntaxTree.GetRootAsync(cancellationToken);
            var updatedSyntaxTree = root.ReplaceNode(node, updatedNode);
            return document.WithSyntaxRoot(updatedSyntaxTree);
        }

        public static string ParsePascalName(string className)
        {
            StringBuilder result = new(className, className.Length + 5);
            int length = className.Length;
            for (int i = length - 1; i >= 0; i--)
            {
                if (char.IsUpper(className[i]) && i != 0 && className[i - 1] != ' ')
                {
                    result.Insert(i, ' ');
                }
            }
            return result.ToString();
        }
    }
}