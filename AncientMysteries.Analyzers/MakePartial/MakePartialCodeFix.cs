namespace AncientMysteries.Analyzers.MarkPartial
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(MakePartialCodeFix)), Shared]
    public class MakePartialCodeFix : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(MakePartialAnalyzer.DiagnosticId);

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
            var parent = root.FindToken(diagnosticSpan.Start).Parent;
            if (parent is null) throw new NullReferenceException(nameof(parent));
            var declaration = parent.AncestorsAndSelf().OfType<ClassDeclarationSyntax>().First();

            // Register a code action that will invoke the fix.
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: Res.CodeFix_MakePartial_Title,
                    createChangedDocument: c => GenerateCodeFixAsync(context.Document, declaration, c),
                    equivalenceKey: nameof(Res.CodeFix_MakePartial_Title)),
                diagnostic);
        }

        private static async Task<Document> GenerateCodeFixAsync(Document document,
            ClassDeclarationSyntax node,
            CancellationToken cancellationToken)
        {
            var modifierPartical = SyntaxFactory.ParseToken("partial ");
            var updatedNode = node.AddModifiers(modifierPartical);

            var syntaxTree = await document.GetSyntaxTreeAsync(cancellationToken);
            if (syntaxTree is null) throw new NullReferenceException(nameof(syntaxTree));
            var updatedSyntaxTree =
                syntaxTree.GetRoot().ReplaceNode(node, updatedNode);
            return document.WithSyntaxRoot(updatedSyntaxTree);
        }
    }
}