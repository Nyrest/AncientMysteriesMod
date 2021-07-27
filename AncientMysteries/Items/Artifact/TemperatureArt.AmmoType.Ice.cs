namespace AncientMysteries.Items
{
    public class TemperatureArt_AmmoType_Ice : TemperatureArt_AmmoType_Base
    {
        public TemperatureArt_AmmoType_Ice(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 500, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_TemperatureArt_Ice);
        }
    }
}