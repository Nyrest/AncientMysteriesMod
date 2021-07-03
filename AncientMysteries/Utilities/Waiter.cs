namespace AncientMysteries.Utilities
{
    public sealed class Waiter
    {
        public readonly uint totalUpdateCount;
        public uint currentUpdateCount;

        public Waiter(uint totalUpdateCount)
        {
            this.totalUpdateCount = totalUpdateCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Tick()
        {
            if (currentUpdateCount++ == totalUpdateCount)
            {
                currentUpdateCount = 0;
                return true;
            }
            return false;
        }
    }
}
