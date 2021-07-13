namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Star : AMAmmoType
    {
        public AT_Star()
        {
            accuracy = 0.2f;
            range = 400f;
            penetration = 1f;
            rangeVariation = 10f;//10
            combustable = true;
            bulletSpeed = 4f;//4
            sprite = TexHelper.ModSprite(t_HolyStar);
            bulletLength = 1000;//0
            sprite.CenterOrigin();
            bulletType = typeof(Bullet_Star);
            bulletColor = Color.Gold;
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
