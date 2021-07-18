namespace AncientMysteries.Bullets
{
    public sealed class Bullet_Lava : Bullet
    {
        public Bullet_Lava(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
        }

        public override void Update()
        {
            base.Update();
            /*foreach (Thing t in Level.CheckCircleAll<Thing>(this.position,10))
            {
                if (t != Level.CheckCircleAll<Thing>(DuckNetwork.localConnection.profile.duck.position,20))
                {
                    Level.Add(SmallFire.New(t.x, t.y, 0, 0));
                }
            }*/
        }

        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            base.OnCollide(pos, t, willBeStopped);
            if (willBeStopped)
            {
                ExplosionPart ins = new(pos.x, pos.y, true);
                ins.xscale *= 0.5f;
                ins.yscale *= 0.5f;
                Level.Add(ins);
                SFX.Play("explode", 0.7f, Rando.Float(-0.4f, -0.1f), 0f, false);
                Thing bulletOwner = owner;
                IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(pos, 8f);
                foreach (MaterialThing t2 in things)
                {
                    t2.Destroy(new DTShot(this));
                }
            }
        }
    }
}