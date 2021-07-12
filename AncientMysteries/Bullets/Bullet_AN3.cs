using System.Collections.Generic;
using AncientMysteries.Items.Miscellaneous;

namespace AncientMysteries.Bullets
{
    public class Bullet_AN3 : Bullet
    {
        public NovaExp n;

        public StateBinding novaBinding = new(nameof(n));
        public Bullet_AN3(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            collisionSize = new Vec2(18, 18);
            _collisionSize = new Vec2(18, 18);
        }

        public override void Update()
        {
            base.Update();
        }
        public override void Removed()
        {
            n = new(travelEnd.x,travelEnd.y, true);
            n.xscale *= 1.5f;
            n.yscale *= 1.5f;
            Level.Add(n);
            SFX.PlaySynchronized("explode", 0.8f, Rando.Float(-0.1f, 1f), 0f, false);
            IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(travelEnd, 18f);
            foreach (MaterialThing t2 in things)
            {
                if (t2 != owner)
                {
                    t2.Destroy(new DTShot(this));
                }
            }
            var firedBullets = new List<Bullet>(5);
            for (int i = 0; i < 5; i++)
            {
                var bullet = new Bullet_AN4(travelEnd.x, travelEnd.y, new AT_AN4(), Rando.Float(0, 360), owner, false, 80, false, true);
                firedBullets.Add(bullet);
                Level.Add(bullet);
            }
            if (Network.isActive)
            {
                NMFireGun gunEvent = new(null, firedBullets, (byte)firedBullets.Count, rel: false, 4);
                Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                firedBullets.Clear();
            }
            base.Removed();
        }
    }
}
