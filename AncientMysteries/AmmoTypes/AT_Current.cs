namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Current : AMAmmoType
    {
        public AT_Current()
        {
            range = 80f;
            rangeVariation = 10f;
            //sprite.CenterOrigin();
            accuracy = 1f;
            penetration = 1f;
            bulletSpeed = 60f;
            bulletThickness = 0.3f;
            rebound = true;
            bulletType = typeof(Bullet_Current);
            bulletColor = Color.Yellow;
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}