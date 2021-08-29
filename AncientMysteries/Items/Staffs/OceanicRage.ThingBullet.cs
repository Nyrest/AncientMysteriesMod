using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    partial class OceanicRage_ThingBullet : AMThingBulletLinar
    {
        public Waiter waiter = new(3);

        float Deviation = 0;
        public override float CalcBulletAutoAngleRadian(Vec2 speed) => base.CalcBulletAutoAngleRadian(speed) + 1.56f;
        public OceanicRage_ThingBullet(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 500, 2, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_OceanicRage);
        }

        public override void Update()
        {
            base.Update();
            if (waiter.Tick())
            {
                float r = Rando.Float(1, 1.7f); ;
                Deviation = Rando.Float(KineticEnergy * -2.5f, KineticEnergy * 2.5f);
                OceanicRage_ThingBulletSmall b1 = new(position, 500, 1, GetBulletVecDeg(_angle - 90f - Deviation, 2 * r), BulletSafeDuck);
                OceanicRage_ThingBulletSmall b2 = new(position, 500, 1, GetBulletVecDeg(_angle + 90f + Deviation, 2 * r), BulletSafeDuck);
                b1.xscale = b1.yscale = r;
                b2.xscale = b2.yscale = r;
                Level.Add(b1);
                Level.Add(b2);
                SFX.PlaySynchronized("flameThrowing", 5, Rando.Float(-0.2f, 0.1f));
            }
            bulletVelocity *= 1.04f;
            foreach (Duck d in Level.CheckCircleAll<Duck>(position, 2f))
            {
                if (d != BulletSafeDuck)
                {
                    d.hSpeed += bulletVelocity.x * 1.5f;
                    d.vSpeed += bulletVelocity.y * 1.5f;
                }
            }
        }

        public override ColorTrajectory GetTrajectory()
        {
            return null;
        }

        public override void Removed()
        {
            base.Removed();
            foreach (PhysicsObject p in Level.CheckCircleAll<PhysicsObject>(position, 9f))
            {
                p.velocity = Maths.AngleToVec(Maths.PointDirection(position, p.position)) * 5;
                if (p is Duck && p != BulletSafeDuck)
                {
                    p.Destroy(new DTImpact(this));
                }
            }
            SFX.PlaySynchronized("largeSplash", 5, Rando.Float(0.3f, 0.5f));
        }
    }
}
