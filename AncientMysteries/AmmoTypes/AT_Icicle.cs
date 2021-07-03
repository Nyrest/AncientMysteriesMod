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
            bulletSpeed = 2f;
            speedVariation = 0f;
            combustable = true;
            bulletLength = 0;
            sprite = TexHelper.ModSprite(t_Icicle);
            sprite.CenterOrigin();
            bulletType = typeof(Bullet_Icicle);
        }
    }
}
