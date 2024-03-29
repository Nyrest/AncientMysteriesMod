﻿using System.Collections.Generic;
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
                var args = SF.ParseAttributeArgumentList("(tex_)");
                list.Add(SF.Attribute(SF.IdentifierName("MetaImage"), args));
            }
            if ((flags & MetadataFlags.HasMetaInfo) == 0)
            {
                string className = symbol.Name;
                var args = SF.ParseAttributeArgumentList($"({nameof(Lang)}.Default, \"{ParsePascalName(className)}\", \"todo\")");
                list.Add(SF.Attribute(SF.IdentifierName("MetaInfo"), args));
                var args2 = SF.ParseAttributeArgumentList($"({nameof(Lang)}.{nameof(Lang.schinese)}, \"\", \"\")");
                list.Add(SF.Attribute(SF.IdentifierName("MetaInfo"), args2));
            }
            if ((flags & MetadataFlags.HasMetaType) == 0)
            {
                MetaType metaType = 0; // 0 = MetaType.Undefined
                if (symbol.HasBaseType("AncientMysteries.Items.AMStaff"))
                {
                    metaType = MetaType.Magic;
                    goto mustBeIt;
                }
                if (symbol.HasBaseType("AncientMysteries.Items.AMMelee"))
                {
                    metaType = MetaType.Melee;
                    goto mustBeIt;
                }
                if (symbol.HasBaseType("AncientMysteries.Items.AMChestPlate",
                   "AncientMysteries.Items.AMBoots",
                   "AncientMysteries.Items.AMEquipment",
                   "AncientMysteries.Items.AMHelmet"))
                {
                    metaType = MetaType.Equipment;
                    goto mustBeIt;
                }
                if (symbol.HasBaseType("AncientMysteries.Items.AMThrowable"))
                {
                    metaType = MetaType.Throwable;
                    goto mustBeIt;
                }
                if (symbol.HasBaseType("AncientMysteries.Items.AMDecoration"))
                {
                    metaType = MetaType.Decoration;
                    goto mustBeIt;
                }
                if (symbol.HasBaseType("AncientMysteries.Items.AMGun"))
                {
                    metaType = MetaType.Gun;
                    goto mustBeIt;
                }
            mustBeIt:
                var args = SF.ParseAttributeArgumentList($"(MetaType.{metaType})");
                list.Add(SF.Attribute(SF.IdentifierName("MetaType"), args));
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