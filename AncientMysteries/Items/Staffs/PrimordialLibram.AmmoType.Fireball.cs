namespace AncientMysteries.Items
{
    public class PrimordialLibram_AmmoType_Fireball : AMAmmoType
    {
        public PrimordialLibram_AmmoType_Fireball()
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
            bulletColor = Color.OrangeRed;
            bulletType = typeof(PrimordialLibram_Bullet_Fireball);
        }
    }
}