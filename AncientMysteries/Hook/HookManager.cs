namespace AncientMysteries.Hook;

public static class HookManager
{
    private static bool _initialized;

    public static Harmony harmony = new("AncientMysteries");

    public static void Initialize()
    {
        if (_initialized) return;
        var allTypes = Assembly.GetCallingAssembly().GetTypes();
        List<MethodInfo> allMethods = new(allTypes.Length * 4);
        int allTypesCount = allTypes.Length;
        for (int i = 0; i < allTypesCount; i++)
        {
            var type = allTypes[i];
            allMethods.AddRange(type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static));
        }

        for (int methodIndex = allMethods.Count - 1; methodIndex >= 0; methodIndex--)
        {
            var method = allMethods[methodIndex];

            var attributes = (HookAttribute[])method.GetCustomAttributes(typeof(HookAttribute), true);
            int attributesLength = attributes.Length;
            for (int attrIndex = 0; attrIndex < attributesLength; attrIndex++)
            {
                var attr = attributes[attrIndex];
                if (attr is HookAttribute hookAttribute)
                {
                    hookAttribute.DoHook(method);
                }
            }
        }
        harmony.PatchAll();
        _initialized = true;
    }
}
