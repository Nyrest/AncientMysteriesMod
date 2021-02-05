using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
