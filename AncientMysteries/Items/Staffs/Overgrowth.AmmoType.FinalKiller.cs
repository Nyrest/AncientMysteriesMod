namespace AncientMysteries.Items.True
{
    public class Overgrowth_AmmoType_FinalKiller : AMAmmoType
    {
        public Overgrowth_AmmoType_FinalKiller()
        {
            sprite = t_Bullet_OvergrowthSmall.ModSprite();
            bulletLength = 0f;
            bulletSpeed = 1f;
            accuracy = 1f;
            speedVariation = 0f;
            rangeVariation = 0f;
            penetration = 2147483647f;
            range = 180;
        }
    }
}
