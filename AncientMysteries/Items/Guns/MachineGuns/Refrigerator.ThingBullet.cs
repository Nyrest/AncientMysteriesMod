using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    public sealed class Refrigerator_ThingBullet : AMThingBulletLinar
    {
        public Refrigerator_ThingBullet(Vec2 pos, float bulletRange, float bulletPenetration, Vec2 initSpeed, Duck safeDuck) : base(pos, bulletRange, bulletPenetration, initSpeed, safeDuck)
        {

        }
    }
}
