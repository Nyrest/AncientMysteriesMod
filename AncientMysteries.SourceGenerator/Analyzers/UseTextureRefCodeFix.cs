using System.Collections.Immutable;
using System.Composition;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AncientMysteries.SourceGenerator.Generators;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AncientMysteries.SourceGenerator.Analyzers
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(UseTextureRefCodeFix)), Shared]
    public class UseTextureRefCodeFix : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(UseTextureRefAnalyzer.DiagnosticId);

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
                    title: Res.CodeFix_UseTextureRef_Title,
                    createChangedDocument: c => MakeConstAsync(context.Document, declaration, c),
                    equivalenceKey: nameof(Res.CodeFix_UseTextureRef_Title)),
                diagnostic);
        }

        private static async Task<Document> MakeConstAsync(Document document,
    LiteralExpressionSyntax literalExpressionSyntax,
    CancellationToken cancellationToken)
        {
            var token = literalExpressionSyntax.Token;
            var newToken = SyntaxFactory.Identifier(TexturesReference.GetFieldName(token.ValueText));

            var sourceText = await literalExpressionSyntax.SyntaxTree.GetTextAsync(cancellationToken);
            // Return document with transformed tree.
            return document.WithText(sourceText.WithChanges(new TextChange(literalExpressionSyntax.FullSpan, newToken.ToFullString())));
        }
    }
}
