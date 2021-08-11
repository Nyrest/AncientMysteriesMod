namespace AncientMysteries.Items
{
    public class PermafrostLance_ThingBulletChargedSmall : AMThingBulletLinar
    {
        public int aliveTime = 0;

        public PermafrostLance_ThingBulletChargedSmall(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 15000, 1, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_PermafrostBulletChargedSmall);
            xscale = yscale = 1.3f;
        }

        public override ColorTrajectory GetTrajectory()
        {
            return null;
        }

        public override void Update()
        {
            aliveTime++;
            if (aliveTime >= 35)
            {
                alpha -= 0.2f;
                if (alpha <= 0)
                {
                    Level.Remove(this);
                }
            }
            base.Update();
        }
    }
}