using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    partial class Oblivion_ThingBulletBlue : AMThingBulletLinar
    {
        public int aliveTime = 0;
        public Oblivion_ThingBulletBlue(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 500, 2, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_OblivionBlue);
        }
        public override void Update()
        {
            base.Update();
            bulletVelocity *= 0.98f;
            aliveTime++;
            if (aliveTime > 420)
            {
                Level.Remove(this);
            }
            foreach (Oblivion_ThingBulletBlue b in Level.CheckCircleAll<Oblivion_ThingBulletBlue>(position, 12f))
            {
                if (b != null && b != this)
                {
                    Level.Remove(b);
                    Level.Remove(this);
                }
            }
            foreach (Oblivion_ThingBulletRed b in Level.CheckCircleAll<Oblivion_ThingBulletRed>(position, 12f))
            {
                if (b != null)
                {
                    Level.Remove(b);
                    Level.Remove(this);
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
            for (int i = 0; i < 4; i++)
            {
                var b = new Oblivion_ThingBulletBlueSmall(position, GetBulletVecDeg(Rando.Float(Maths.PointDirection(Vec2.Zero, bulletVelocity) - 15, Maths.PointDirection(Vec2.Zero, bulletVelocity) + 15), 3), BulletSafeDuck);
                Level.Add(b);
                SFX.PlaySynchronized("laserBlast", 5, 1f);
            }
        }
    }
}
