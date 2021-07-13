using System.Collections.Generic;

namespace AncientMysteries.Bullets
{
    public class Bullet_BigFB : Bullet
    {
        public int n = 0;

        public Vec2 pos;

        public StateBinding _bulletSpeedBinding = new(nameof(_bulletSpeed));
        public StateBinding _bulletPosBinding = new(nameof(start));
        public StateBinding _posBinding = new(nameof(pos));

        public Bullet_BigFB(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {

        }

        public override void Update()
        {
            base.Update();
            n++;
            var firedBullets = new List<Bullet>(1);
            if (n == 10)
            {
                SFX.Play("flameExplode", 0.7f, Rando.Float(-0.8f, -0.4f), 0f, false);
                n = 0;
                var bullet = Make.Bullet<AT_Lava>(start, _owner, Rando.Float(135, 45), this);
                firedBullets.Add(bullet);
                Level.Add(bullet);
            }
            _bulletSpeed += 0.15f;
            pos = start;
            /*foreach (Thing t in Level.CheckCircleAll<Thing>(this.position,10))
            {
                if (t != Level.CheckCircleAll<Thing>(DuckNetwork.localConnection.profile.duck.position,20))
                {
                    Level.Add(SmallFire.New(t.x, t.y, 0, 0));
                }
            }*/
        }

        public override void Removed()
        {
            base.Removed();
            ExplosionPart ins = new(travelEnd.x, travelEnd.y, true);
            Level.Add(ins);
            SFX.Play("explode", 0.7f, Rando.Float(-0.7f, -0.5f), 0f, false);
            Thing bulletOwner = owner;
            IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(travelEnd, 16f);
            foreach (MaterialThing t2 in things)
            {
                if (t2 != bulletOwner && t2.owner != bulletOwner)
                {
                    t2.Destroy(new DTShot(this));
                }
            }
            var firedBullets = new List<Bullet>(7);
            for (int i = 0; i < 7; i++)
            {
                var bullet = Make.Bullet<AT_Lava>(travelEnd, _owner, Rando.Float(0, 360), this);
                firedBullets.Add(bullet);
                Level.Add(bullet);
            }
            if (Network.isActive)
            {
                NMFireGun gunEvent = new(null, firedBullets, (byte)firedBullets.Count, rel: false, 4);
                Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                firedBullets.Clear();
            }
            SFX.Play("sizzle", 0.2f, Rando.Float(1, 4), 0f, false);
        }
    }
}
