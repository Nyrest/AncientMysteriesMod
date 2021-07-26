namespace AncientMysteries.Items
{
    public class TemperatureArt_AmmoType_Ice : TemperatureArt_AmmoType_Base
    {
        public TemperatureArt_AmmoType_Ice(Vec2 pos, float bulletRange, Vec2 initSpeed, Duck safeDuck) : base(pos, bulletRange, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_TemperatureArt_Ice);
        }
    }
}