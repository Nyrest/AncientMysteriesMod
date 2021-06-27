﻿using System.Collections.Generic;
using AncientMysteries.Items.Miscellaneous;

namespace AncientMysteries.Bullets
{
    public class Bullet_AN : Bullet
    {
        public Bullet_AN(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            this.collisionSize = new Vec2(32, 32);
            _collisionSize = new Vec2(32, 32);
        }

        public override void Update()
        {
            base.Update();
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
            var firedBullets = new List<Bullet>(5);
            for (int i = 0; i < 5; i++)
            {
                var bullet = new Bullet_AN2(travelEnd.x, travelEnd.y, new AT_AN2(), Rando.Float(0, 360), owner, false, 80, false, true);
                firedBullets.Add(bullet);
                Level.Add(bullet);
            }
            if (Network.isActive && this.isLocal)
            {
                NMFireGun gunEvent = new(null, firedBullets, 0, rel: false, 4);
                Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                firedBullets.Clear();
            }
            base.Removed();
        }
    }
}
