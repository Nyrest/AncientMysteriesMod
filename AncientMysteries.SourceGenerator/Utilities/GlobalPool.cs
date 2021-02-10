using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AncientMysteries.SourceGenerator
{
    public static class SBPool
    {
        private static readonly Stack<StringBuilder> _stack = new Stack<StringBuilder>();

        private static readonly object _lockObj = new object();

        private static readonly Stack<StringBuilder> _stackMini = new Stack<StringBuilder>();

        private static readonly object _lockObjMini = new object();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Rent()
        {
            lock (_lockObj)
            {
                if (_stack.Count != 0)
                    return _stack.Pop();
            }
            return new StringBuilder(20480);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(this StringBuilder value)
        {
            value.Clear();
            lock (_lockObj)
            {
                _stack.Push(value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder RentMini()
        {
            lock (_lockObjMini)
            {
                if (_stackMini.Count != 0)
                    return _stackMini.Pop();
            }
            return new StringBuilder(1024);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ReturnMini(this StringBuilder value)
        {
            if(value.Capacity > 4096)
            {
                Return(value);
                return;
            }
            value.Clear();
            lock (_lockObjMini)
            {
                _stack.Push(value);
            }
        }
    }
}
