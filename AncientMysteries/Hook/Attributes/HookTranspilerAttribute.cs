#nullable enable
namespace AncientMysteries.Hook.Attributes;

public sealed class HookTranspilerAttribute : HookAttribute
{
    public HookTranspilerAttribute(string fullName) : base(fullName) { }

    public HookTranspilerAttribute(Type type, string methodName) : base(type, methodName) { }

    public HookTranspilerAttribute(string fullName, Type[]? parameters = null) : base(fullName, parameters) { }

    public HookTranspilerAttribute(Type type, string methodName, Type[]? parameters = null) : base(type, methodName, parameters) { }

    public HookTranspilerAttribute(string fullName, Type[]? parameters = null, Type[]? generics = null) : base(fullName, parameters, generics) { }

    public HookTranspilerAttribute(Type type, string methodName, Type[]? parameters = null, Type[]? generics = null) : base(type, methodName, parameters, generics) { }

    public override void DoHook(MethodInfo newMethod)
    {
        HookManager.harmony.Patch(originalMethod,
            null,
            null,
            new HarmonyMethod(newMethod),
            null);
    }
}
