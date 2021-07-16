using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items.Staffs
{
    public class HolyLight_ThingBullet2 : AMThingBulletLinar
    {
        public HolyLight_ThingBullet2(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 0.2f, int.MaxValue, initSpeed, safeDuck)
        {
            this.ReadyToRun(t_Bullet_HolyStar2);
            alpha = Rando.Float(0.7f, 1);
        }
    }
}
