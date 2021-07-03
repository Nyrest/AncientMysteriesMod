namespace AncientMysteries.Utilities
{
    public static class Make
    {
        #region Bullet
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bullet Bullet<TAmmoType>(Vec2 position, Thing owner = null, float angleDegress = 0, Thing firedFrom = null) 
            where TAmmoType : AmmoType, new()
        {
            //var ammoType = FastNew<TAmmoType>.CreateInstance();
            var ammoType = InstanceOf<TAmmoType>.instance;
            return ammoType.FireBullet(position, owner, angleDegress, firedFrom);
        }


        #endregion
    }
}
