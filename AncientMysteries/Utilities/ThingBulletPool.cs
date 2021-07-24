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

                bullet._tailQueue = item.TailQueue;
                bullet._lastImpacting = item.Impacting;
                bullet._currentImpacting = item.ImpactingToKeep;
            }
            Create(bullet);
            [MethodImpl(MethodImplOptions.NoInlining)]
            static void Create(AMThingBulletBase bullet)
            {
                bullet._tailQueue = new(10);
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
                    _array[_size++] = new ThingBulletCache(bullet._tailQueue, bullet._lastImpacting, bullet._currentImpacting);
                }
                else
                {
                    ResizeThenRecycle(bullet);
                }
                bullet._tailQueue = null;
                bullet._lastImpacting = null;
                [MethodImpl(MethodImplOptions.NoInlining)]
                static void ResizeThenRecycle(AMThingBulletBase bullet)
                {
                    Array.Resize(ref _array, 2 * _array.Length);
                    _array[_size++] = new ThingBulletCache(bullet._tailQueue, bullet._lastImpacting, bullet._currentImpacting);
                }
            }
        }

        private record struct ThingBulletCache(Queue<Vec2> TailQueue, HashSet<MaterialThing> Impacting, List<MaterialThing> ImpactingToKeep);
    }
}