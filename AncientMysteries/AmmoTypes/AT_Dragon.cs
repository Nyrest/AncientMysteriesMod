namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Dragon : AmmoType
    {
        public AT_Dragon()
        {
            accuracy = 0.6f;
            range = 400f;
            penetration = 2f;
            rangeVariation = 10f;
            combustable = true;
            sprite = TexHelper.ModSprite("fireball2.png");
            bulletColor = Color.OrangeRed;
            //sprite.CenterOrigin();
            bulletType = typeof(Bullet_Dragon);
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            this.bulletColor = Color.OrangeRed;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
