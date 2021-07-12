namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_LaserY : AMAmmoType
    {
        public AT_LaserY()
        {
            accuracy = 1f;
            range = 400f;
            penetration = 1f;
            rangeVariation = 20f;
            bulletThickness = 2f;
            bulletSpeed = 30f;
            bulletColor = Color.Yellow;
            bulletType = typeof(Bullet_Laser);
            //this.bulletType = typeof(Bullet_Electronic);
            //this.sprite = TexHelper.ModSprite("ElectronicStar.png");
            //this.sprite.CenterOrigin();
            //bulletLength = 10;
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            bulletColor = Color.Yellow;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
        public class AT_LaserY_Bullet : Bullet
        {
            public AT_LaserY_Bullet(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network) { }

            public override void Update()
            {
                base.Update();
                color = Color.Yellow;
            }
        }
    }
}
