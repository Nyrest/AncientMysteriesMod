namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Dragon : AMAmmoType
    {
        public AT_Dragon()
        {
            accuracy = 0.6f;
            range = 400f;
            penetration = 2f;
            rangeVariation = 10f;
            bulletLength = 0;
            combustable = true;
            bulletColor = Color.OrangeRed;
            sprite = TexHelper.ModSprite(t_Fireball2);
            sprite.CenterOrigin();
            bulletType = typeof(Bullet_Dragon);
        }

        public override void OnHit(bool destroyed, Bullet b)
        {
            base.OnHit(destroyed, b);
            Level.Add(SmallFire.New(b.x, b.y, 0, 0));
        }
    }
}
