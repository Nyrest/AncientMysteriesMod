﻿#nullable enable
namespace AncientMysteries.Hook.Attributes;

public sealed class HookAfterAttribute : HookAttribute
{
    public HookAfterAttribute(string fullName) : base(fullName) { }

    public HookAfterAttribute(Type type, string methodName) : base(type, methodName) { }

    public HookAfterAttribute(string fullName, Type[]? parameters = null) : base(fullName, parameters) { }

    public HookAfterAttribute(Type type, string methodName, Type[]? parameters = null) : base(type, methodName, parameters) { }

    public HookAfterAttribute(string fullName, Type[]? parameters = null, Type[]? generics = null) : base(fullName, parameters, generics) { }

    public HookAfterAttribute(Type type, string methodName, Type[]? parameters = null, Type[]? generics = null) : base(type, methodName, parameters, generics) { }

    public override void DoHook(MethodInfo newMethod)
    {
        HookManager.harmony.Patch(originalMethod,
            null,
            new HarmonyMethod(newMethod),
            null,
            null);
    }
}
