using System.Collections.Generic;
using AncientMysteries.Items.Miscellaneous;

namespace AncientMysteries.Bullets
{
    public class Bullet_AN : Bullet
    {
        public Bullet_AN(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            this.collisionSize = new Vec2(32, 32);
        }

        public override void Removed()
        {
            NovaExp n = new(travelEnd.x, travelEnd.y, true);
            n.xscale *= 3f;
            n.yscale *= 3f;
            Level.Add(n);
            SFX.PlaySynchronized("explode", 0.8f, Rando.Float(-0.1f, 1f), 0f, false);
            IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(travelEnd, 32f);
            foreach (MaterialThing t2 in things)
            {
                if (t2 != owner)
                {
                    t2.Destroy(new DTShot(this));
                }
            }
            NetHelper.NmFireGun(null, list =>
            {
                for (int i = 0; i < 5; i++)
                {
                    var bullet = new Bullet_AN2(travelEnd.x, travelEnd.y, new AT_AN2(), Rando.Float(0, 360), owner, false, 80, false, true);
                    list.Add(bullet);
                }
            });
            base.Removed();
        }
    }
}
