namespace AncientMysteries.Items{
    public class EternalFlame_AmmoType : AMAmmoType
    {
        public EternalFlame_AmmoType()
        {
            accuracy = 0.2f;
            range = 200f;
            penetration = 1f;
            rangeVariation = 10f;
            bulletSpeed = 15f;
            speedVariation = -10f;
            rangeVariation = -50f;
            combustable = true;
            bulletLength = 0;
            sprite = TexHelper.ModSprite(tex_Bullet_FireBolt);
            sprite.CenterOrigin();
            bulletType = typeof(EternalFlame_Bullet);
        }
    }
}
