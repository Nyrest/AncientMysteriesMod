namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Icicle : AmmoType
    {
        public AT_Icicle()
        {
            accuracy = 1f;
            range = 250f;
            penetration = 1f;
            rangeVariation = 10f;
            bulletSpeed = 1f;
            speedVariation = 0f;
            combustable = true;
            bulletLength = 0;
            sprite = TexHelper.ModSprite(t_Icicle);
            //sprite.CenterOrigin();
            bulletType = typeof(Bullet_Icicle);
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            this.bulletColor = Color.OrangeRed;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
