namespace AncientMysteries.Items.Artifact
{
    public class TemperatureArt_AmmoType_Base : AMAmmoType
    {
        public TemperatureArt_AmmoType_Base()
        {
            accuracy = 1;
            sprite = t_Bullet_TemperatureArt_Ice.ModSprite(true);
            range = 800;
            HideTail();
        }
    }
}
