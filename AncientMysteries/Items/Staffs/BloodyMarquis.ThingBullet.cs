using AncientMysteries.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    public class BloodyMarquis_ThingBullet : AMThingBulletLinar
    {
        public override float CalcBulletAngleRadian(Vec2 speed) => base.CalcBulletAngleRadian(speed) + 1.56f;

        public int aliveTime = 0;

        public StateBinding aliveTimeBinding = new(nameof(aliveTime));
        public BloodyMarquis_ThingBullet(Vec2 pos, float bulletRange, float bulletPenetration, Vec2 initSpeed, Duck safeDuck) : base(pos, bulletRange, bulletPenetration, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_BloodyDagger);
            alpha = 0f;
        }

        public override ColorTrajectory GetTrajectory() => base.GetTrajectory() with
        {
            Color = Color.DarkRed
        };

        public override void Update()
        {
            base.Update();
            /*if (aliveTime++ >= 60)
            {
                alpha -= 0.04f;
                if (alpha <= 0)
                {
                    Level.Remove(this);
                }
            }
            else
            {*/
            MathHelper.Min(alpha += 0.04f, 1);
            //}
            foreach (Duck duck in Level.current.things[typeof(Duck)])
            {
                if (duck.dead) continue;
                if (BulletSafeDuck != null && duck.team == BulletSafeDuck.team) continue;


            }
            bulletVelocity *= 1.04f;
        }
    }
}
