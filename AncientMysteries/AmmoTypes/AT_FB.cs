namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_FB : AMAmmoType
    {
        public AT_FB()
        {
            accuracy = 0.2f;
            range = 200f;
            penetration = 1f;
            rangeVariation = 10f;
            bulletSpeed = 15f;
            speedVariation = -10f;
            rangeVariation = -50f;
            combustable = true;
            bulletLength = 0;
            sprite = TexHelper.ModSprite(t_Bullet_FireBolt);
            //sprite.CenterOrigin();
            bulletType = typeof(Bullet_FB);
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            bulletColor = Color.OrangeRed;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}