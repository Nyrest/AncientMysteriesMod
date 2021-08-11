namespace AncientMysteries.AmmoTypes
{
    /// <summary>
    /// Not designed to network sync
    /// </summary>
    public class ThingBulletSimulation_AmmoType : AMAmmoType
    {
        public ThingBulletSimulation_AmmoType()
        {
            penetration = 0;
            range = 2;
            bulletSpeed = range;
            this.canBeReflected = true;
            bulletType = typeof(ThingBulletSimulation_Bullet);
        }

        #region Bullet

        public class ThingBulletSimulation_Bullet : AMBullet
        {
            public AMThingBulletBase callback;

            public ThingBulletSimulation_Bullet(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
            {
            }

            protected override void Rebound(Vec2 pos, float dir, float rng)
            {
                callback?.LegacyRebound(pos, dir, rng);
            }
        }

        #endregion Bullet
    }
}