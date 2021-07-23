namespace AncientMysteries
{
    public static class NetHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NmFireGun(this Gun gun, Action<List<Bullet>> value, bool alsoAddThemToWorld = true)
        {
            var firedBullets = GlobalPool<List<Bullet>>.Rent();
            firedBullets.Clear(); // ensure clean
            value(firedBullets);

            if (firedBullets.Count == 0) goto collect;

            if (alsoAddThemToWorld)
            {
                var currentLevel = Level.current;
                foreach (var item in firedBullets)
                {
                    currentLevel.AddThing(item);
                }
            }

            if (gun != null)
            {
                gun.bulletFireIndex++;

            }
            if (Network.isActive)
            {
                NMFireGun gunEvent = new(gun, firedBullets, gun?.bulletFireIndex ?? 0, rel: false, 4);
                Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
            }
        collect:
            firedBullets.Clear();
            GlobalPool<List<Bullet>>.Return(firedBullets);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NmFireGun(Action<List<Bullet>> value, bool alsoAddThemToWorld = true) => NmFireGun(null, value, alsoAddThemToWorld);

        private static readonly List<Bullet> Size1List = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NmFireGun(this Bullet value, bool alsoAddThemToWorld = true)
        {
            if (alsoAddThemToWorld)
            {
                Level.current.AddThing(value);
            }

            if (Network.isActive)
                {
                    Size1List[0] = value;
                    NMFireGun gunEvent = new(null, Size1List, 0, rel: false, 4);
                    Size1List[0] = null;
                    Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void NmFireGun(this Gun gun, delegate*<List<Bullet>, void> value, bool alsoAddThemToWorld = true)
        {
            var firedBullets = GlobalPool<List<Bullet>>.Rent();
            firedBullets.Clear(); // ensure clean
            value(firedBullets);

            if (firedBullets.Count == 0) goto collect;

            if (alsoAddThemToWorld)
            {
                foreach (var item in firedBullets)
                {
                    Level.Add(item);
                }
            }

            if (gun is not null)
            {
                gun.bulletFireIndex++;
                if (Network.isActive)
                {
                    NMFireGun gunEvent = new(gun, firedBullets, gun.bulletFireIndex, false, 4);
                    Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                }
            }
        collect:
            firedBullets.Clear();
            GlobalPool<List<Bullet>>.Return(firedBullets);
        }
    }
}