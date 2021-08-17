namespace AncientMysteries.Items
{
    public class HolyLight_ThingBullet2 : AMThingBulletLinar
    {
        public HolyLight_ThingBullet2(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 0.2f, int.MaxValue, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_HolyStar2);
            alpha = 0;
        }

        public override void Update()
        {
            base.Update();
            MathHelper.Clamp(alpha += 0.1f, 0, 1);
        }
    }
}