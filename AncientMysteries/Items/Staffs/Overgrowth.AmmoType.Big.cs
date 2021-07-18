namespace AncientMysteries.Items
{
    public class Overgrowth_AmmoType_Big : AMAmmoType
    {
        public Overgrowth_AmmoType_Big()
        {
            sprite = tex_Bullet_OvergrowthBig.ModSprite();
            bulletSpeed = 3f;
            accuracy = 0.4f;
            speedVariation = 3f;
            bulletLength = 0f;
            rangeVariation = 50f;
            penetration = 1f;
            range = 180f;
            bulletType = typeof(Overgrowth_Bullet_Big);
        }
    }
}