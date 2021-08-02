namespace AncientMysteries.Items
{
    public class HolyLight_ThingBullet : AMThingBulletLinar
    {
        public Waiter waiter = new(5);

        public HolyLight_ThingBullet(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 400, 1, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_HolyStar);
        }

        public override ColorTrajectory GetTrajectory() => base.GetTrajectory() with
        {
            Color = Color.Yellow,
        };

        public override void Update()
        {
            base.Update();
            if (isServerForObject && waiter.Tick())
            {
                NetHelper.NmFireGun(list =>
                {
                    HolyLight_ThingBullet2 bullet = new(position, GetBulletVecDeg(Rando.Float(0, 360), 0.01f), BulletSafeDuck);
                    Level.Add(bullet);
                });
            }
        }
    }
}