using System.Linq.Expressions;

namespace AncientMysteries.Helpers
{
    public static class GenericNew<T>
    {
        public static readonly Expression<Func<T>> SourceExpression = Expression.Lambda<Func<T>>(Expression.New(typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null)));

        public static readonly Func<T> _compiled = SourceExpression.Compile();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CreateInstance() => typeof(T).IsValueType
            ? default
            : _compiled();
    }
}
