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
