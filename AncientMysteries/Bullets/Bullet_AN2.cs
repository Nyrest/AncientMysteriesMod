using AncientMysteries.AmmoTypes;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AncientMysteries.Items.Miscellaneous;

namespace AncientMysteries.Bullets
{
    public class Bullet_AN2 : Bullet
    {
        public Bullet_AN2(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {

        }

        public override void Update()
        {
            base.Update();
        }
        public override void DoTerminate()
        {
            base.DoTerminate();
        }
    }
}
