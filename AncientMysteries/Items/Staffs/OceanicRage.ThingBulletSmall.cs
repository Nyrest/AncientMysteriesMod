using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    partial class OceanicRage_ThingBulletSmall : AMThingBulletLinar
    {
        public int aliveTime = 0;

        public StateBinding aliveTimeBinding = new(nameof(aliveTime));
        public override float CalcBulletAutoAngleRadian(Vec2 speed) => base.CalcBulletAutoAngleRadian(speed) + 1.56f;
        public OceanicRage_ThingBulletSmall(Vec2 pos, float bulletRange, float bulletPenetration, Vec2 initSpeed, Duck safeDuck) : base(pos, bulletRange, bulletPenetration, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_OceanicShard);
        }

        public override ColorTrajectory GetTrajectory()
        {
            return null;
        }

        public override void Update()
        {
            base.Update();
            aliveTime++;
            if (aliveTime > 10)
            {
                MathHelper.Clamp(alpha -= 0.2f, 0, 1);
                MathHelper.Clamp(xscale -= 0.4f, 0, 99);
                MathHelper.Clamp(yscale -= 0.4f, 0, 99);
                if (alpha <= 0)
                {
                    Level.Remove(this);
                }
            }
            bulletVelocity *= 0.97f;
        }
    }
}
