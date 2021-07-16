using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items.Staffs
{
    public class HolyLight_ThingBullet : AMThingBulletLinar
    {
        public Waiter w = new(5);
        public HolyLight_ThingBullet(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 400, 1, initSpeed, safeDuck)
        {
            this.ReadyToRun(t_Bullet_HolyStar);
        }

        public override void Update()
        {
            base.Update();
            if (isServerForObject && w.Tick())
            {
                NetHelper.NmFireGun(list =>
                {
                    HolyLight_ThingBullet2 bullet = new(position, GetVectorFromDegress(angle,0.01f), BulletSafeDuck);
                    Level.Add(bullet);
                });
            }
        }
    }
}
