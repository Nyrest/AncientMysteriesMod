namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public sealed class CallerArgumentExpressionAttribute : Attribute
	{
		public string ParameterName { get; }

		public CallerArgumentExpressionAttribute(string parameterName)
		{
			ParameterName = parameterName;
		}
	}
}
