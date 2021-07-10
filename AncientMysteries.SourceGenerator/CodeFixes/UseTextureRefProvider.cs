using System.Collections.Immutable;
using System.Composition;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Simplification;

namespace AncientMysteries.SourceGenerator.CodeFixes
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(UseTextureRefProvider)), Shared]
    public class UseTextureRefProvider : CodeFixProvider
    {
        private static string[] _fixableDiagnosticIds = new[] { "Use Texture Reference" };
        public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(_fixableDiagnosticIds);

        public override FixAllProvider GetFixAllProvider()
        {
            return base.GetFixAllProvider();
        }

        public override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            // TODO: Replace the following code with your own analysis, generating a CodeAction for each fix to suggest
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;
            var a = "holyStar.png";
            // Find the type declaration identified by the diagnostic.
            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<LiteralExpressionSyntax>().First();

            // Register a code action that will invoke the fix.
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: Res.CFTitle_UseTextureRef,
                    createChangedDocument: c => MakeConstAsync(context.Document, declaration, c),
                    equivalenceKey: nameof(Res.CFTitle_UseTextureRef)),
                diagnostic);
        }

        private static async Task<Document> MakeConstAsync(Document document, LiteralExpressionSyntax localDeclaration, CancellationToken cancellationToken)
        {

            // Return document with transformed tree.
            return document;
        }
    }
}
