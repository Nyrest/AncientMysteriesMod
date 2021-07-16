namespace AncientMysteries
{
    public static class Rand
    {
        private static readonly Random rand = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Bool() =>
        (rand.Next() & 1) == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Choose<T>(params T[] items) =>
            (items is not null && items.Length != 0)
                ? items[rand.Next(0, items.Length)]
                : default;

        /// <summary>
        /// Negative value if random generated number is odd
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomNegative(this float value) =>
        Bool()
            ? value
            : -value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float FloatRN(float min, float max) => Rando.Float(min, max).RandomNegative();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2 Vec2XY(float min, float max) => new(Rando.Float(min, max), Rando.Float(min, max));
    }
}
