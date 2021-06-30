namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Leaf : AmmoType
    {
        public AT_Leaf()
        {
            accuracy = 1f;
            range = 20f;
            penetration = 999f;
            bulletSpeed = 2f;
            speedVariation = 0.1f;
            combustable = true;
            bulletLength = 0;
            sprite = TexHelper.ModSprite(t_Leaf);
            sprite.CenterOrigin();
            bulletType = typeof(Bullet_FB);
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            this.bulletColor = Color.OrangeRed;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
