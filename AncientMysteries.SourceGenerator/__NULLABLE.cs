using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
public sealed class AllowNullAttribute : Attribute
{
}

// System.Diagnostics.CodeAnalysis.DisallowNullAttribute

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
public sealed class DisallowNullAttribute : Attribute
{
}

// System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class DoesNotReturnAttribute : Attribute
{
}

// System.Diagnostics.CodeAnalysis.DoesNotReturnIfAttribute

[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
public sealed class DoesNotReturnIfAttribute : Attribute
{
    public bool ParameterValue
    {
        get;
    }

    public DoesNotReturnIfAttribute(bool parameterValue)
    {
        ParameterValue = parameterValue;
    }
}

// System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembersAttribute

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, Inherited = false)]
public sealed class DynamicallyAccessedMembersAttribute : Attribute
{
    public DynamicallyAccessedMemberTypes MemberTypes
    {
        get;
    }

    public DynamicallyAccessedMembersAttribute(DynamicallyAccessedMemberTypes memberTypes)
    {
        MemberTypes = memberTypes;
    }
}

// System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes

[Flags]
public enum DynamicallyAccessedMemberTypes
{
    None = 0x0,
    PublicParameterlessConstructor = 0x1,
    PublicConstructors = 0x3,
    NonPublicConstructors = 0x4,
    PublicMethods = 0x8,
    NonPublicMethods = 0x10,
    PublicFields = 0x20,
    NonPublicFields = 0x40,
    PublicNestedTypes = 0x80,
    NonPublicNestedTypes = 0x100,
    PublicProperties = 0x200,
    NonPublicProperties = 0x400,
    PublicEvents = 0x800,
    NonPublicEvents = 0x1000,
    All = -1
}

// System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute

[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event, Inherited = false, AllowMultiple = false)]
public sealed class ExcludeFromCodeCoverageAttribute : Attribute
{
    public string? Justification
    {
        get;
        set;
    }
}

// System.Diagnostics.CodeAnalysis.MaybeNullAttribute

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
public sealed class MaybeNullAttribute : Attribute
{
}

// System.Diagnostics.CodeAnalysis.MaybeNullWhenAttribute

[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
public sealed class MaybeNullWhenAttribute : Attribute
{
    public bool ReturnValue
    {
        get;
    }

    public MaybeNullWhenAttribute(bool returnValue)
    {
        ReturnValue = returnValue;
    }
}

// System.Diagnostics.CodeAnalysis.MemberNotNullAttribute

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
public sealed class MemberNotNullAttribute : Attribute
{
    public string[] Members
    {
        get;
    }

    public MemberNotNullAttribute(string member)
    {
        Members = new string[1]
        {
            member
        };
    }

    public MemberNotNullAttribute(params string[] members)
    {
        Members = members;
    }
}

// System.Diagnostics.CodeAnalysis.MemberNotNullWhenAttribute

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
public sealed class MemberNotNullWhenAttribute : Attribute
{
    public bool ReturnValue
    {
        get;
    }

    public string[] Members
    {
        get;
    }

    public MemberNotNullWhenAttribute(bool returnValue, string member)
    {
        ReturnValue = returnValue;
        Members = new string[1]
        {
            member
        };
    }

    public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
    {
        ReturnValue = returnValue;
        Members = members;
    }
}

// System.Diagnostics.CodeAnalysis.NotNullAttribute

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
public sealed class NotNullAttribute : Attribute
{
}

// System.Diagnostics.CodeAnalysis.NotNullIfNotNullAttribute

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
public sealed class NotNullIfNotNullAttribute : Attribute
{
    public string ParameterName
    {
        get;
    }

    public NotNullIfNotNullAttribute(string parameterName)
    {
        ParameterName = parameterName;
    }
}

// System.Diagnostics.CodeAnalysis.NotNullWhenAttribute

[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
public sealed class NotNullWhenAttribute : Attribute
{
    public bool ReturnValue
    {
        get;
    }

    public NotNullWhenAttribute(bool returnValue)
    {
        ReturnValue = returnValue;
    }
}