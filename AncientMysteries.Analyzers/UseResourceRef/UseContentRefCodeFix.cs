using AncientMysteries.SourceGenerator.Generators;

namespace AncientMysteries.Analyzers.UseResourceRef
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(UseContentRefCodeFix)), Shared]
    public class UseContentRefCodeFix : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(UseContentRefAnalyzer.DiagnosticId);

        public sealed override FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            // TODO: Replace the following code with your own analysis, generating a CodeAction for each fix to suggest
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            // Find the type declaration identified by the diagnostic.
            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<LiteralExpressionSyntax>().First();

            // Register a code action that will invoke the fix.
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: Res.CodeFix_UseContentRef_Title,
                    createChangedDocument: c => MakeConstAsync(context.Document, declaration, c),
                    equivalenceKey: nameof(Res.CodeFix_UseContentRef_Title)),
                diagnostic);
        }

        private static async Task<Document> MakeConstAsync(Document document,
    LiteralExpressionSyntax literalExpressionSyntax,
    CancellationToken cancellationToken)
        {
            var token = literalExpressionSyntax.Token;
            string name = token.ValueText;
            string extension = Path.GetExtension(name).ToLower();
            SyntaxToken newToken;
            switch (extension)
            {
                case ".png":
                    {
                        newToken = SyntaxFactory.Identifier(ContentReferencesGenerator.GetFieldName(
                            ContentReferencesGenerator.prefix_Texture,
                            token.ValueText));
                        break;
                    }
                case ".wav":
                    {
                        newToken = SyntaxFactory.Identifier(ContentReferencesGenerator.GetFieldName(
                            ContentReferencesGenerator.prefix_Sound,
                            token.ValueText));
                        break;
                    }
                default:
                    throw new Exception("Unexpected File Extension");
            }

            var sourceText = await literalExpressionSyntax.SyntaxTree.GetTextAsync(cancellationToken);
            // Return document with transformed tree.
            return document.WithText(sourceText.WithChanges(new TextChange(literalExpressionSyntax.FullSpan, newToken.ToFullString())));
        }
    }
}