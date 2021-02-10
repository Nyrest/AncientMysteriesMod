using DuckGame;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.Bullets
{
    public sealed class Bullet_LaserG2 : Bullet
    {
        public Bullet_LaserG2(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            
        }
        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            base.OnCollide(pos, t, willBeStopped);
            if (willBeStopped)
            {
                ExplosionPart ins = new(pos.x, pos.y, true);
                ins.xscale *= 0.7f;
                ins.yscale *= 0.7f;
                Level.Add(ins);
                SFX.Play("explode", 0.7f, Rando.Float(0.1f, 0.3f), 0f, false);
                Thing bulletOwner = this.owner;
                IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(pos, 14f);
                foreach (MaterialThing t2 in things)
                {
                    if (t2 != bulletOwner)
                    {
                        t2.Destroy(new DTShot(this));
                    }
                }
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
        }

        public override void Update()
        {
            base.Update();
            this._bulletSpeed -= 0.02f;
        }
    }
}
