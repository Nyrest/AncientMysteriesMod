using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    public class PermafrostLance_ThingBulletChargedSmall : AMThingBulletLinar
    {
        public PermafrostLance_ThingBulletChargedSmall(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 280, 1, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_PermafrostBulletChargedSmall);
        }
    }
}
