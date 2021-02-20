using AncientMysteries.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Utilities
{
    public static class Waiter
    {
        public static DictSlim<string, int> dict = new DictSlim<string, int>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitUnique<T>(
            this T obj,
            int totalFramesToWait,
            [CallerFilePath] string path = null,
            [CallerMemberName] string member = null,
            [CallerLineNumber] int line = -1)
        {
            ref int currentFrame = ref dict.GetOrAddValueRef(string.Concat(obj.GetHashCode(), path, member, line.ToString()));
            return currentFrame++ >= totalFramesToWait;
        }
    }
}
