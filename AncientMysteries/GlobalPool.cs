using AncientMysteries.Helpers;
using System.Collections.Generic;

namespace AncientMysteries
{
    public static class GlobalPool<T>
    {
        private static readonly Stack<T> _stack = new Stack<T>();

        private static readonly object _lockObj = new object();

        public static T Rent()
        {
            lock (_lockObj)
            {
                if (_stack.Count != 0)
                    return _stack.Pop();
            }
            return FastNew<T>.CreateInstance();
        }

        public static void Return(T value)
        {
            lock (_lockObj)
            {
                _stack.Push(value);
            }
        }
    }
}
