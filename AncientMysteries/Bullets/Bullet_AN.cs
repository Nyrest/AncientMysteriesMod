using AncientMysteries.AmmoTypes;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AncientMysteries.Items.Miscellaneous;

namespace AncientMysteries.Bullets
{
    public class Bullet_AN : Bullet
    {
        public Bullet_AN(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {

        }

        public override void Update()
        {
            base.Update();
        }

        protected override void OnHit(bool destroyed)
        {
            base.OnHit(destroyed);
            if (!destroyed) return;
            NovaExp n = new NovaExp(travelEnd.x, travelEnd.y, true);
            n.xscale *= 3f;
            n.yscale *= 3f;
            Level.Add(n);
            SFX.Play("explode", 0.8f, Rando.Float(-0.1f, 1f), 0f, false);
            IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(travelEnd, 32f);
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
                var bullet = new Bullet_AN2(travelEnd.x, travelEnd.y, new AT_AN2(), Rando.Float(0, 360), owner, false, 80, false, true);
                firedBullets.Add(bullet);
                Level.Add(bullet);
            }
            if (Network.isActive && this.isLocal)
            {
                NMFireGun gunEvent = new NMFireGun(null, firedBullets, 0, rel: false, 4);
                Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                firedBullets.Clear();
            }
        }
    }
}
