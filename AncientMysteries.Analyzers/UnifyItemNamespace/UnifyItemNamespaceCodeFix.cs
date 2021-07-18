using static AncientMysteries.Analyzers.UnifyItemNamespace.UnifyItemNamespaceAnalyzer;

namespace AncientMysteries.Analyzers.UnifyItemNamespace
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(UnifyItemNamespaceCodeFix)), Shared]
    public class UnifyItemNamespaceCodeFix : CodeFixProvider
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
            var declaration = root.FindToken(diagnosticSpan.Start).Parent?.AncestorsAndSelf().OfType<NamespaceDeclarationSyntax>().First();
            if (declaration is null) throw new NullReferenceException(nameof(declaration));

            // Register a code action that will invoke the fix.
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: Res.CodeFix_UnifyItemNamespace_Title,
                    createChangedDocument: c => GenerateCodeFixAsync(context.Document, declaration, c),
                    equivalenceKey: nameof(Res.CodeFix_UnifyItemNamespace_Title)),
                diagnostic);
        }

        private static async Task<Document> GenerateCodeFixAsync(Document document,
            NamespaceDeclarationSyntax node,
            CancellationToken cancellationToken)
        {
            var syntaxTree = await document.GetSyntaxTreeAsync(cancellationToken);
            if (syntaxTree is null) throw new NullReferenceException(nameof(syntaxTree));
            var root = await syntaxTree.GetRootAsync(cancellationToken);

            var updatedSyntaxTree = root.ReplaceNode(node, node.WithName(SF.ParseName("AncientMysteries.Items")));
            return document.WithSyntaxRoot(updatedSyntaxTree);
        }
    }
}