namespace AncientMysteries.Items
{
    public class TemperatureArt_AmmoType_WaterVapor : TemperatureArt_AmmoType_Base
    {
        public TemperatureArt_AmmoType_WaterVapor(Vec2 pos, float bulletRange, Vec2 initSpeed, Duck safeDuck) : base(pos, bulletRange, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_TemperatureArt_WaterVapor);
        }

        public override bool BulletCanDestory(MaterialThing thing)
        {
            if (thing is Duck d)
            {
                d.onFire = true;
                return false;
            }
            return base.BulletCanDestory(thing);
        }
    }
}