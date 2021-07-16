using AncientMysteries.Helpers;

namespace AncientMysteries.Utilities
{
    public static class Make
    {
        #region Bullet

        #region Default
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bullet Bullet<TAmmoType>(Vec2 position, Thing owner = null, float angleDegress = 0, Thing firedFrom = null)
    where TAmmoType : AmmoType, new()
        {
            var ammoType = GetAmmoTypeInstance<TAmmoType>();
            return ammoType.FireBullet(position, owner, angleDegress, firedFrom);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bullet Bullet<TAmmoType>(float x, float y, Thing owner = null, float angleDegress = 0, Thing firedFrom = null)
            where TAmmoType : AmmoType, new()
            => Bullet<TAmmoType>(new Vec2(x, y), owner, angleDegress, firedFrom);
        #endregion

        #region Fixed BulletSpeed and Range
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bullet Bullet<TAmmoType>(Vec2 position, float bulletSpeed, float range, Thing owner = null, float angleDegress = 0, Thing firedFrom = null)
    where TAmmoType : AmmoType, new()
        {

            var ammoType = GetAmmoTypeInstance<TAmmoType>();
            // Only these two is safe to modify
            ammoType.bulletSpeed = bulletSpeed;
            ammoType.range = range;
            return ammoType.FireBullet(position, owner, angleDegress, firedFrom);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bullet Bullet<TAmmoType>(float x, float y, float bulletSpeed, float range, Thing owner = null, float angleDegress = 0, Thing firedFrom = null)
            where TAmmoType : AmmoType, new()
            => Bullet<TAmmoType>(new Vec2(x, y), bulletSpeed, range, owner, angleDegress, firedFrom);
        #endregion

        #region Modifier
        public delegate void AmmoTypeModifier(ref float bulletSpeed, ref float range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bullet Bullet<TAmmoType>(Vec2 position, AmmoTypeModifier modifier, Thing owner = null, float angleDegress = 0, Thing firedFrom = null)
    where TAmmoType : AmmoType, new()
        {

            var ammoType = GenericNew<TAmmoType>.CreateInstance();
            modifier(ref ammoType.bulletSpeed, ref ammoType.range);
            return ammoType.FireBullet(position, owner, angleDegress, firedFrom);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bullet Bullet<TAmmoType>(float x, float y, AmmoTypeModifier modifier, Thing owner = null, float angleDegress = 0, Thing firedFrom = null)
            where TAmmoType : AmmoType, new()
            => Bullet<TAmmoType>(new Vec2(x, y), modifier, owner, angleDegress, firedFrom);
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TAmmoType GetAmmoTypeInstance<TAmmoType>()
            where TAmmoType : AmmoType, new()
        {
            return GenericNew<TAmmoType>.CreateInstance();
            //return InstanceOf<TAmmoType>.instance;
        }
        #endregion

        #region ThingBullet
        
        #endregion
    }
}
