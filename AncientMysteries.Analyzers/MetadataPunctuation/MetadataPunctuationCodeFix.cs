using AncientMysteries.SourceGenerator;
using static AncientMysteries.Analyzers.MetadataPunctuation.MetadataPunctuationAnalyzer;

namespace AncientMysteries.Analyzers.MetadataPunctuation
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(MetadataPunctuationCodeFix)), Shared]
    public class MetadataPunctuationCodeFix : CodeFixProvider
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
            var declaration = root.FindToken(diagnosticSpan.Start).Parent?.AncestorsAndSelf().OfType<AttributeSyntax>().First();
            if (declaration is null) throw new NullReferenceException(nameof(declaration));

            // Register a code action that will invoke the fix.
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: Res.CodeFix_FixMetadataPunctuation_Title,
                    createChangedDocument: c => GenerateCodeFixAsync(context.Document, declaration, c),
                    equivalenceKey: nameof(Res.CodeFix_FixMetadataPunctuation_Title)),
                diagnostic);
        }

        private static async Task<Document> GenerateCodeFixAsync(Document document,
            AttributeSyntax node,
            CancellationToken cancellationToken)
        {
            var args = node.ArgumentList!.Arguments;

            if (
    args[0].Expression is not MemberAccessExpressionSyntax langExp ||
    //args[1].Expression is not LiteralExpressionSyntax nameExp ||
    args[2].Expression is not LiteralExpressionSyntax descriptionExp)
                return document;

            if (!Enum.TryParse(langExp.OperatorToken.GetNextToken().ToString(), out Lang lang))
                return document;

            //string? name = nameExp.Token.Value as string;
            string? description = descriptionExp.Token.Value as string;

            var sb = SBPool.Rent();
            if (TryProcessText(description, lang, sb))
            {
                var sourceText = await descriptionExp.SyntaxTree.GetTextAsync(cancellationToken);
                string newText = "\"" + sb.ToString().Replace("\n", "\\n") + "\"";
                return document.WithText(sourceText.WithChanges(new TextChange(descriptionExp.FullSpan, newText)));
            }
            sb.Return();

            return document;
        }
    }
}