using AncientMysteries.Helpers;
using System.Collections.Generic;

namespace AncientMysteries
{
    public static class GlobalPool<T>
    {
        private static readonly Stack<T> _stack = new();

        private static readonly object _lockObj = new();

        public static T Rent()
        {
            if (_stack.Count == 0) goto returnNew;
            lock (_lockObj)
            {
                if (_stack.Count != 0)
                    return _stack.Pop();
            }
returnNew:
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
