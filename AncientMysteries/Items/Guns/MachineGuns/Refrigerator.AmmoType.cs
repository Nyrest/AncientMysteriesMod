namespace AncientMysteries.Items
{
    public class Refrigerator_AmmoType : AMAmmoType
    {
        public Refrigerator_AmmoType()
        {
            accuracy = 0.5f;
            range = 500;
            rangeVariation = 30;
            bulletSpeed = 23;
            speedVariation = 2;
            sprite = tex_Bullet_Refrigerator.ModSprite();
            HideTail();
        }
    }
}