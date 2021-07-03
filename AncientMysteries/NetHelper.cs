using System.Collections.Generic;

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
                foreach (var item in firedBullets)
                {
                    Level.Add(item);
                }
            }

            if (gun != null)
            {
                gun.bulletFireIndex++;
                if (Network.isActive)
                {
                    NMFireGun gunEvent = new(gun, firedBullets, gun.bulletFireIndex, rel: false, 4);
                    Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                }
            }
collect:
            firedBullets.Clear();
            GlobalPool<List<Bullet>>.Return(firedBullets);
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
