using AncientMysteries.Utilities.Collections;

namespace AncientMysteries.Utilities
{
    public static class Waiter
    {
        public static DictSlim<string, int> dict = new();

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
