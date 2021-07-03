namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Flower : AmmoType
    {
        public AT_Flower()
        {
            accuracy = 1f;
            range = 250f;
            penetration = 1f;
            bulletSpeed = 4f;
            speedVariation = 3f;
            combustable = true;
            bulletLength = 0;
            sprite = TexHelper.ModSprite(t_Flower);
            sprite.CenterOrigin();
            bulletType = typeof(Bullet_Flowerr);
            this.bulletColor = Color.OrangeRed;
            rangeVariation = 60f;
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
