using AncientMysteries.Helpers;

namespace AncientMysteries.Utilities
{
    public static class Make
    {
        #region Bullet
        public static Bullet Bullet<TAmmoType>(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null) where TAmmoType : AmmoType, new()
        {
            //var ammoType = FastNew<TAmmoType>.CreateInstance();
            var ammoType = InstanceOf<TAmmoType>.instance;
            return ammoType.FireBullet(position, owner, angle, firedFrom);
        }
        #endregion
    }
}
