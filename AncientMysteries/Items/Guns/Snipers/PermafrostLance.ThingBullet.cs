using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    public class PermafrostLance_ThingBullet : AMThingBulletLinar
    {
        public PermafrostLance_ThingBullet(Vec2 pos, Vec2 initSpeed, Duck safeDuck, int powerLevel) : base(pos, 1750, 2, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_PermafrostBullet);
            hasGravity = true;
            maxGravity = 0.15f;
            gravityIncrement = 0.07f;
        }

        public override ColorTrajectory GetTrajectory()
        {
            return null;
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
