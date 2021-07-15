namespace AncientMysteries.Items.True
{
    public sealed class PrimordialLibram_AmmoType_Flower : AMAmmoType
    {
        public PrimordialLibram_AmmoType_Flower()
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
            bulletType = typeof(PrimordialLibram_Bullet_Flower);
            bulletColor = Color.OrangeRed;
            rangeVariation = 60f;
        }
    }
}
