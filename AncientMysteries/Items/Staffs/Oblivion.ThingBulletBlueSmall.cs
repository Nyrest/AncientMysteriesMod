using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    partial class Oblivion_ThingBulletBlueSmall : AMThingBulletLinar
    {
        public int aliveTime = 0;
        public Oblivion_ThingBulletBlueSmall(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 500, 2, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_OblivionBlueSmall);
        }
        public override void Update()
        {
            base.Update();
            aliveTime++;
            if (aliveTime > 600)
            {
                Removed();
            }
            foreach (Duck d in Level.CheckCircleAll<Duck>(position, 7.5f))
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
        }
    }
}
