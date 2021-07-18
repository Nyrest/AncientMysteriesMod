namespace AncientMysteries.Items
{
    public class PrimordialLibram_Bullet_Fireball : AMBullet
    {
        public PrimordialLibram_Bullet_Fireball(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
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
                SFX.Play("explode", 0.7f, Rando.Float(-0.7f, -0.5f), 0f, false);
                Thing bulletOwner = owner;
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

        public override void Removed()
        {
            base.Removed();
            ExplosionPart ins = new(travelEnd.x, travelEnd.y, true);
            ins.xscale *= 0.7f;
            ins.yscale *= 0.7f;
            Level.Add(ins);
            SFX.Play("explode", 0.7f, Rando.Float(-0.7f, -0.5f), 0f, false);
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
    }
}