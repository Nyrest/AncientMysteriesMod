using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (x < Level.current.camera.left)
            {
                for (int i = 0; i < 10; i++)
                {
                    PermafrostLance_ThingBulletChargedSmall b = new(position, GetBulletVecDeg(0, 3, 1, 0.5f),BulletSafeDuck);
                    Level.Add(b);
                }
            }
            else if (x > Level.current.camera.right)
            {
                for (int i = 0; i < 10; i++)
                {
                    PermafrostLance_ThingBulletChargedSmall b = new(position, GetBulletVecDeg(180, 3, 1, 0.5f), BulletSafeDuck);
                    Level.Add(b);
                }
            }
            else if (y > Level.current.camera.bottom)
            {
                for (int i = 0; i < 10; i++)
                {
                    PermafrostLance_ThingBulletChargedSmall b = new(position, GetBulletVecDeg(90, 3, 1, 0.5f), BulletSafeDuck);
                    Level.Add(b);
                }
            }
            else if (y < Level.current.camera.top)
            {
                for (int i = 0; i < 10; i++)
                {
                    PermafrostLance_ThingBulletChargedSmall b = new(position, GetBulletVecDeg(270, 3, 1, 0.5f), BulletSafeDuck);
                    Level.Add(b);
                }
            }
        }
        public override void Removed()
        {
            PermafrostExplosion exp = new(x, y,false);
            Level.Add(exp);
            SFX.PlaySynchronized("glassBreak",1,1);
            base.Removed();
        }
    }
}
