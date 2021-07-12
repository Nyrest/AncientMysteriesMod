namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Shadow : AMAmmoType
    {
        public AT_Shadow()
        {
            accuracy = 0.9f;
            range = 600f;
            penetration = 2f;
            rangeVariation = 10f;
            combustable = true;
            bulletColor = Color.Purple;
            bulletType = typeof(AT_Shadow_Bullet);
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            return base.FireBullet(position, owner, angle, firedFrom);
        }

        public class AT_Shadow_Bullet : Bullet
        {
            public AT_Shadow_Bullet(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
            {
                color = Color.Purple;
            }

            public override void Update()
            {
                base.Update();
                color = Color.Purple;
            }
        }
    }
}
