namespace AncientMysteries.Utilities
{
    public sealed class WaiterShitty
    {
        public uint FramesToWait { get; init; }
        public uint CurrentFrame { get; private set; }
        public bool Paused { get; private set; }
        public uint Count { get; init; }
        public uint CurrentCount { get; private set; }
        public WaiterShitty(uint framesToWait,uint count)
        {
            FramesToWait = framesToWait;
            Count = count;
            Paused = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Tick()
        {
            if (CurrentCount++ == Count)
            {
                Pause();
                Reset();
            }

            if (Paused) return false;

            if (CurrentFrame++ == FramesToWait)
            {
                CurrentFrame = 0;
                return true;
            }
            return false;
        }

        public void Pause()
        {
            Paused = true;
        }

        public void Resume()
        {
            Paused = false;
        }

        public void Reset()
        {
            CurrentFrame = 0;
            CurrentCount = 0;
        }
    }
}
