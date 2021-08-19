namespace AncientMysteries.Items
{
    public class NeonStriker_ThingBullet_Blue : AMThingBulletLinar
    {
        public Waiter waiter = new(2);

        public bool _canMultiply;

        public NeonStriker_ThingBullet_Blue(Vec2 pos, Vec2 initSpeed, Duck safeDuck, bool canMultiply) : base(pos, 1000, 1, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_NeonLightBlue);
            _canMultiply = canMultiply;
            GravityEnabled = true;
            GravityIncrement = 0.2f;
            GravityMax = 1f;
            BulletAutoAngle = false;
        }

        public override void Update()
        {
            if (!_canMultiply)
            {
                alpha -= 0.1f;
                if (alpha <= 0)
                {
                    Level.Remove(this);
                }
            }
            if (waiter.Tick() && _canMultiply)
            {
                NeonStriker_ThingBullet_Blue b = new(position, bulletVelocity * 0.001f, BulletSafeDuck, false);
                Level.Add(b);
            }
            base.Update();
        }

        public override ColorTrajectory GetTrajectory()
        {
            return null;
        }
    }
}