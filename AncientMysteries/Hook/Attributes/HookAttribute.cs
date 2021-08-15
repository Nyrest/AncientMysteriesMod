#nullable enable
namespace AncientMysteries.Hook.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor,
    AllowMultiple = true,
    Inherited = true)]
public abstract class HookAttribute : Attribute
{
    public MethodInfo originalMethod;

    public HookAttribute(Type type, string methodName) =>
        originalMethod = AccessTools.Method(type, methodName);

    public HookAttribute(Type type, string methodName, Type[]? parameters = null) =>
        originalMethod = AccessTools.Method(type, methodName, parameters, null);

    public HookAttribute(Type type, string methodName, Type[]? parameters = null, Type[]? generics = null) =>
        originalMethod = AccessTools.Method(type, methodName, parameters, generics);

    public HookAttribute(string fullName) =>
        originalMethod = AccessTools.Method(fullName);

    public HookAttribute(string fullName, Type[]? parameters = null) =>
        originalMethod = AccessTools.Method(fullName, parameters, null);

    public HookAttribute(string fullName, Type[]? parameters = null, Type[]? generics = null) =>
        originalMethod = AccessTools.Method(fullName, parameters, generics);

    public abstract void DoHook(MethodInfo newMethod);
}
