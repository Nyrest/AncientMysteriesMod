namespace AncientMysteries.Items
{
    public class TemperatureArt_AmmoType_Base : AMThingBulletLinar
    {
        /*public TemperatureArt_AmmoType_Base()
        {
            accuracy = 1;
            sprite = tex_Bullet_TemperatureArt_Ice.ModSprite(true);
            range = 800;
            HideTail();
        }*/
        public TemperatureArt_AmmoType_Base(Vec2 pos, float bulletRange, Vec2 initSpeed, Duck safeDuck) : base(pos, bulletRange, 1, initSpeed, safeDuck)
        {
            BulletTail = false;
        }
    }
}