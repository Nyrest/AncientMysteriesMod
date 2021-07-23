using System.Collections.Generic;

namespace AncientMysteries.SourceGenerator.Generators
{
    public record LocalizationInfo(Lang Lang, string? Name, string? Description);

    public record LocalizedClass(ClassDeclarationSyntax Node, ITypeSymbol Symbol, List<LocalizationInfo> Infos);

    public class LocalizationSyntaxReceiver : ISyntaxContextReceiver
    {
        public List<LocalizedClass> localizations = new(50);

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            var node = context.Node;
            // Accept ClassDeclaration only
            // And AttributeLists can't be empty
            if (node is not ClassDeclarationSyntax syntax ||
                syntax.AttributeLists.Count == 0) return;

            // Find the symbol
            if (context.SemanticModel.GetDeclaredSymbol(syntax) is not ITypeSymbol symbol) return;

            if (symbol.IsAbstract) return;

            if (!symbol.AllInterfaces.Any(x => x.ToDisplayString() == "AncientMysteries.Localization.IAMLocalizable")) return;

            List<LocalizationInfo> infos = new(4);
            foreach (var attr in symbol.GetAttributes())
            {
                if (attr.AttributeClass is not INamedTypeSymbol attrTypeSymbol) continue;
                if (attrTypeSymbol.ToDisplayString(NullableFlowState.NotNull) == "AncientMysteries.MetaInfoAttribute")
                {
                    var arguments = attr.ConstructorArguments;
                    var lang = (Lang)arguments[0].Value!;
                    string? name = arguments[1].Value as string;
                    string? description = arguments[2].Value as string;
                    infos.Add(new LocalizationInfo(lang, name, description));
                }
            }
            if (infos.Count != 0)
            {
                localizations.Add(new LocalizedClass(syntax, symbol, infos));
            }
        }
    }
}