namespace AncientMysteries.Items
{
    public class TemperatureArt_AmmoType_Base : AMAmmoType
    {
        public TemperatureArt_AmmoType_Base()
        {
            accuracy = 1;
            sprite = tex_Bullet_TemperatureArt_Ice.ModSprite(true);
            range = 800;
            HideTail();
        }
    }
}