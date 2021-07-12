namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Leaf : AMAmmoType
    {
        public AT_Leaf()
        {
            accuracy = 1f;
            range = 60f;
            penetration = 999f;
            bulletSpeed = 1f;
            speedVariation = 0.1f;
            combustable = true;
            bulletLength = 0;
            sprite = TexHelper.ModSprite(t_Leaf);
            sprite.CenterOrigin();
            bulletType = typeof(Bullet_Star2);
            bulletColor = Color.OrangeRed;
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
