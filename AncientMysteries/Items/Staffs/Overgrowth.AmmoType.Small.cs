namespace AncientMysteries.Items.Staffs
{
    public class Overgrowth_AmmoType_Small : AMAmmoType
    {
        public Overgrowth_AmmoType_Small()
        {
            sprite = t_Bullet_OvergrowthSmall.ModSprite();
            bulletSpeed = 5f;
            accuracy = 0.3f;
            speedVariation = 2.5f;
            bulletLength = 0f;
            rangeVariation = 50f;
            penetration = 1f;
            range = 180;
        }
    }
}
