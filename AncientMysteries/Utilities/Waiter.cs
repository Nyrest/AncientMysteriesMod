using AncientMysteries.Utilities.Collections;
using System.Runtime.CompilerServices;

namespace AncientMysteries.Utilities
{
    public static class Waiter
    {
        public static DictSlim<string, int> dict = new DictSlim<string, int>();

        [Obsolete]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WaitUnique<T>(
            this T obj,
            int totalFramesToWait,
            [CallerFilePath] string path = null,
            [CallerMemberName] string member = null,
            [CallerLineNumber] int line = -1)
        {
            ref int currentFrame = ref dict.GetOrAddValueRef(string.Concat(obj.GetHashCode().ToString(), path, member, line.ToString()));
            return currentFrame++ >= totalFramesToWait;
        }
    }
}
