using AncientMysteries.AmmoTypes;
using DuckGame;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.Bullets
{
    public sealed class Bullet_LaserG : Bullet
    {
        public Bullet_LaserG(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {

        }
        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            base.OnCollide(pos, t, willBeStopped);
            if (willBeStopped)
            {
                Bullet b = new Bullet_LaserG2(this.start.x, this.start.y, new AT_LaserG2(), Rando.Float(0, 360), this.owner, false, 100);
                b.color = Color.LightGreen;
                Level.Add(b);
            }
        }

        public override void DoTerminate()
        {
            /*base.DoTerminate();
            ExplosionPart ins = new ExplosionPart(travelEnd.x, travelEnd.y, true);
            ins.xscale *= 0.7f;
            ins.yscale *= 0.7f;
            Level.Add(ins);
            SFX.Play("explode", 0.7f, Rando.Float(0.1f, 0.3f), 0f, false);
            Thing bulletOwner = this.owner;
            IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(travelEnd, 14f);
            foreach (MaterialThing t2 in things)
            {
                if (t2 != bulletOwner)
                {
                    t2.Destroy(new DTShot(this));
                }
            }*/
            Bullet b = new Bullet_LaserG2(this.start.x, this.start.y, new AT_LaserG2(), Rando.Float(0, 360), this.owner, false, 100);
            b.color = Color.LightGreen;
            Level.Add(b);
        }
    }
}
