#if NETFRAMEWORK || NETSTANDARD2_0

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
public sealed class InterpolatedStringHandlerArgumentAttribute : Attribute
{
	public string[] Arguments { get; }

	public InterpolatedStringHandlerArgumentAttribute(string argument)
	{
		Arguments = new string[1] { argument };
	}

	public InterpolatedStringHandlerArgumentAttribute(params string[] arguments)
	{
		Arguments = arguments;
	}
}

#endif