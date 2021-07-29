namespace AncientMysteries.Utilities
{
    public sealed class Waiter
    {
        public uint FramesToWait { get; init; }
        public uint CurrentFrame { get; private set; }
        public bool Paused { get; private set; }

        public Waiter(uint framesToWait)
        {
            FramesToWait = framesToWait;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Tick()
        {
            if (Paused) return false;

            if (CurrentFrame++ == FramesToWait)
            {
                CurrentFrame = 0;
                return true;
            }
            return false;
        }

        public void Pause() => Paused = true;

        public void Resume() => Paused = false;

        public void Reset() => CurrentFrame = 0;

        public Waiter TickToEnd()
        {
            CurrentFrame = FramesToWait;
            return this;
        }
    }
}