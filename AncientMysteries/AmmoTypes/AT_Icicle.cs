namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Icicle : AMAmmoType
    {
        public AT_Icicle()
        {
            accuracy = 1f;
            range = 250f;
            penetration = 1f;
            rangeVariation = 10f;
            bulletSpeed = 2f;
            speedVariation = 0f;
            bulletLength = 0;
            sprite = TexHelper.ModSprite(tex_Bullet_Icicle);
            sprite.CenterOrigin();
        }
    }
}
