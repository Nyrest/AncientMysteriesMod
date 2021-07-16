using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items.Staffs
{
    public class PrimordialLibram_ThingBullet_Flower : AMThingBulletLinar
    {
        public PrimordialLibram_ThingBullet_Flower(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 300, 1, initSpeed, safeDuck)
        {
            this.ReadyToRun(t_Bullet_Flower);
        }

        public 
    }
}
