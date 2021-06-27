namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Star : AmmoType
    {
        public AT_Star()
        {
            accuracy = 0.2f;
            range = 1000f;
            penetration = 1f;
            rangeVariation = 10f;
            combustable = true;
            bulletSpeed = 5f;
            sprite = TexHelper.ModSprite("holyStar.png");
            bulletLength = 0;
            //sprite.CenterOrigin();
            bulletType = typeof(Bullet_Star);
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            this.bulletColor = Color.Yellow;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
