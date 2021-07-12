using System.Collections.Generic;

namespace AncientMysteries.Bullets
{
    public sealed class Bullet_Laser : Bullet
    {
        public Bullet_Laser(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            _bulletSpeed = 30f;
            color = Color.Yellow;

        }

        public override void Removed()
        {
            base.Removed();
            ExplosionPart ins = new(travelEnd.x, travelEnd.y, true);
            ins.xscale *= 0.7f;
            ins.yscale *= 0.7f;
            Level.Add(ins);
            SFX.PlaySynchronized("explode", 0.7f, Rando.Float(0.1f, 0.3f), 0f, false);
            Thing bulletOwner = owner;
            IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(travelEnd, 14f);
            foreach (MaterialThing t2 in things)
            {
                if (t2 != bulletOwner)
                {
                    t2.Destroy(new DTShot(this));
                }
            }
        }

        public override void Update()
        {
            base.Update();
            color = Color.Yellow;
        }
    }
}
