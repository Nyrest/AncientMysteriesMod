namespace AncientMysteries.Utilities
{
    public sealed class Ticker
    {
        public readonly uint totalUpdateCount;
        public uint currentUpdateCount;

        public Ticker(uint totalUpdateCount)
        {
            this.totalUpdateCount = totalUpdateCount;
        }

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
