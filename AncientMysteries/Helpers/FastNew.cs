using System.Linq.Expressions;

namespace AncientMysteries.Helpers
{
    public static class FastNew<T>
    {
        public static readonly Expression<Func<T>> SourceExpression =
            !typeof(T).IsValueType
            ? Expression.Lambda<Func<T>>(Expression.New(typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null)))
            : Expression.Lambda<Func<T>>(Expression.New(typeof(T)));

        public static readonly Func<T> CreateInstance = SourceExpression.Compile();
    }
}
