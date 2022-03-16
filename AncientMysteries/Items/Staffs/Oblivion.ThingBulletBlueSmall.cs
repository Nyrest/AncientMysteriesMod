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
        public Oblivion_ThingBulletBlueSmall(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 350, 1, initSpeed, safeDuck)
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
