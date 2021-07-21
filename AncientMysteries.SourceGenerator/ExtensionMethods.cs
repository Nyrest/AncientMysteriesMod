namespace AncientMysteries.SourceGenerator
{
    public static class ExtensionMethods
    {
        public static bool HasBaseType(this ITypeSymbol typeSymbol, string displayName)
            => HasBaseType(typeSymbol, x => x.ToDisplayString() == displayName);

        public static bool HasBaseType(this ITypeSymbol typeSymbol, params string[] displayNames)
            => HasBaseType(typeSymbol, x =>
            {
                int length = displayNames.Length;
                for (int i = 0; i < length; i++)
                {
                    if (x.ToDisplayString() == displayNames[i])
                        return true;
                }
                return false;
            });

        public static bool HasBaseType(this ITypeSymbol typeSymbol, Func<INamedTypeSymbol, bool> predicate)
        {
            var baseType = typeSymbol.BaseType;
            return baseType is not null &&
                (predicate(baseType) || HasBaseType(baseType, predicate));
        }
    }
}
