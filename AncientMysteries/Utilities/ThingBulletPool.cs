namespace AncientMysteries.Utilities
{
    public static class ThingBulletPool
    {
        private static ThingBulletCache[] _array = new ThingBulletCache[64];

        private static int _size;

        public static void InitBullet(AMThingBulletBase bullet)
        {
            int index = _size - 1;
            if ((uint)index < (uint)_array.Length)
            {
                ThingBulletCache item = _array[_size = index];
                _array[index] = default;

                bullet._lastImpacting = item.Impacting;
                bullet._currentImpacting = item.ImpactingToKeep;
            }
            Create(bullet);
            [MethodImpl(MethodImplOptions.NoInlining)]
            static void Create(AMThingBulletBase bullet)
            {
                bullet._lastImpacting = new();
                bullet._currentImpacting = new();
            }
        }

        public static void Recycle(AMThingBulletBase bullet)
        {
            if (bullet._lastImpacting.Count > 20) return;
            lock (_array)
            {
                if ((uint)_size < (uint)_array.Length)
                {
                    _array[_size++] = new ThingBulletCache(bullet._lastImpacting, bullet._currentImpacting);
                }
                else
                {
                    ResizeThenRecycle(bullet);
                }
                bullet._lastImpacting = null;
                [MethodImpl(MethodImplOptions.NoInlining)]
                static void ResizeThenRecycle(AMThingBulletBase bullet)
                {
                    Array.Resize(ref _array, 2 * _array.Length);
                    _array[_size++] = new ThingBulletCache(bullet._lastImpacting, bullet._currentImpacting);
                }
            }
        }

        private readonly record struct ThingBulletCache(HashSet<MaterialThing> Impacting, List<MaterialThing> ImpactingToKeep);
    }
}