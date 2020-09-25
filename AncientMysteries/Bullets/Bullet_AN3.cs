using AncientMysteries.AmmoTypes;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AncientMysteries.Items.Miscellaneous;

namespace AncientMysteries.Bullets
{
    public class Bullet_AN3 : Bullet
    {
        public Bullet_AN3(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {

        }

        public override void Update()
        {
            base.Update();
        }
        public override void DoTerminate()
        {
            base.DoTerminate();
            NovaExp n = new NovaExp(travelEnd.x, travelEnd.y, true);
            n.xscale *= 1.25f;
            n.yscale *= 1.25f;
            Level.Add(n);
            SFX.Play("explode", 0.8f, Rando.Float(-0.1f, 1f), 0f, false);
            for (int i = 0; i < 5; i++)
            {
                Level.Add(new Bullet_AN4(travelEnd.x, travelEnd.y, new AT_AN4(), Rando.Float(0, 360), owner, false, 80, false, true));
            }
        }
    }
}
