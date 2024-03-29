﻿namespace AncientMysteries.Items
{
    public class EternalFlame_Bullet : AMBullet
    {
        public EternalFlame_Bullet(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
        }

        public override void Removed()
        {
            base.Removed();
            ExplosionPart ins = new(travelEnd.x, travelEnd.y, true);
            ins.xscale *= 1.4f;
            ins.yscale *= 1.4f;
            Level.Add(ins);
            SFX.Play("explode", 0.7f, Rando.Float(-0.7f, -0.5f), 0f, false);
            Thing bulletOwner = owner;
            IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(travelEnd, 14f);
            foreach (MaterialThing t2 in things)
            {
                if (t2 != bulletOwner)
                {
                    t2.Destroy(new DTIncinerate(this));
                }
            }
        }
    }
}