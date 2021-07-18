

namespace AncientMysteries.Bullets
{
    public class Bullet_AN2 : Bullet
    {
        public Bullet_AN2(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            collisionSize = new Vec2(25, 25);
            _collisionSize = new Vec2(25, 25);
        }

        public override void Update()
        {
            base.Update();
        }
        public override void Removed()
        {
            NovaExp n = new(travelEnd.x, travelEnd.y, true);
            n.xscale *= 2.25f;
            n.yscale *= 2.25f;
            Level.Add(n);
            SFX.PlaySynchronized("explode", 0.8f, Rando.Float(-0.1f, 1f), 0f, false);
            IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(travelEnd, 25f);
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
                var bullet = Make.Bullet<AT_AN3>(travelEnd, _owner, Rando.Float(0, 360), this);
                firedBullets.Add(bullet);
                Level.Add(bullet);
            }
            if (Network.isActive)
            {
                NMFireGun gunEvent = new(null, firedBullets, (byte)firedBullets.Count, rel: false, 4);
                Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                firedBullets.Clear();
            }
            base.Removed();
        }
    }
}
