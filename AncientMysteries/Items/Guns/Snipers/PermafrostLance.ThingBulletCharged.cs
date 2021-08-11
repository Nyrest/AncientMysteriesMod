namespace AncientMysteries.Items
{
    public class PermafrostLance_ThingBulletCharged : AMThingBulletLinar
    {
        public PermafrostLance_ThingBulletCharged(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 1750, 2, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_PermafrostBulletCharged);
        }

        public override ColorTrajectory GetTrajectory()
        {
            return null;
        }

        public override void Update()
        {
            base.Update();
            if (!isServerForObject) return;
            float bulletSpeed = 3f;
            float speedVariation = 1f;
            float accuracy = 0.9f;
            if (x < Level.current.camera.left)
            {
                for (int i = 0; i < 10; i++)
                {
                    PermafrostLance_ThingBulletChargedSmall b = new(position, GetBulletVecDeg(0, bulletSpeed, speedVariation, accuracy), BulletSafeDuck);
                    Level.Add(b);
                    Level.Remove(this);
                }
            }
            else if (x > Level.current.camera.right)
            {
                for (int i = 0; i < 10; i++)
                {
                    PermafrostLance_ThingBulletChargedSmall b = new(position, GetBulletVecDeg(180, bulletSpeed, speedVariation, accuracy), BulletSafeDuck);
                    Level.Add(b);
                    Level.Remove(this);
                }
            }
            else if (y > Level.current.camera.bottom)
            {
                for (int i = 0; i < 10; i++)
                {
                    PermafrostLance_ThingBulletChargedSmall b = new(position, GetBulletVecDeg(90, bulletSpeed, speedVariation, accuracy), BulletSafeDuck);
                    Level.Add(b);
                    Level.Remove(this);
                }
            }
            else if (y < Level.current.camera.top)
            {
                for (int i = 0; i < 10; i++)
                {
                    PermafrostLance_ThingBulletChargedSmall b = new(position, GetBulletVecDeg(270, bulletSpeed, speedVariation, accuracy), BulletSafeDuck);
                    Level.Add(b);
                    Level.Remove(this);
                }
            }
        }

        public override void Removed()
        {
            SFX.PlaySynchronized("laserBlast", 1, 1);
            base.Removed();
        }
    }
}