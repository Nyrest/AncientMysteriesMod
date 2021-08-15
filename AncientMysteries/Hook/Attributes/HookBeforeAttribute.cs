#nullable enable
namespace AncientMysteries.Hook.Attributes;

public sealed class HookBeforeAttribute : HookAttribute
{
    public HookBeforeAttribute(string fullName) : base(fullName) { }

    public HookBeforeAttribute(Type type, string methodName) : base(type, methodName) { }

    public HookBeforeAttribute(string fullName, Type[]? parameters = null) : base(fullName, parameters) { }

    public HookBeforeAttribute(Type type, string methodName, Type[]? parameters = null) : base(type, methodName, parameters) { }

    public HookBeforeAttribute(string fullName, Type[]? parameters = null, Type[]? generics = null) : base(fullName, parameters, generics) { }

    public HookBeforeAttribute(Type type, string methodName, Type[]? parameters = null, Type[]? generics = null) : base(type, methodName, parameters, generics) { }

    public override void DoHook(MethodInfo newMethod)
    {
        HookManager.harmony.Patch(originalMethod,
            new HarmonyMethod(newMethod),
            null,
            null,
            null);
    }
}
