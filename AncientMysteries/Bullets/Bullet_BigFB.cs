using AncientMysteries.AmmoTypes;
using DuckGame;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AncientMysteries.Bullets
{
    public class Bullet_BigFB : Bullet
    {
        private Texture2D _beem;

        private float _thickness;

        public int n = 0;


        public StateBinding _bulletSpeedBinding = new(nameof(_bulletSpeed));

        public Bullet_BigFB(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            _thickness = type.bulletThickness;
            _beem = this.ModTex2D("firebally.png");
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
                var bullet = new Bullet_Lava(start.x, start.y, new AT_Lava(), Rando.Float(135, 45), owner, false, 200, false, true)
                {
                    color = Color.DarkOrange
                };
                firedBullets.Add(bullet);
                Level.Add(bullet);
            }
            this._bulletSpeed += 0.15f;
            /*foreach (Thing t in Level.CheckCircleAll<Thing>(this.position,10))
            {
                if (t != Level.CheckCircleAll<Thing>(DuckNetwork.localConnection.profile.duck.position,20))
                {
                    Level.Add(SmallFire.New(t.x, t.y, 0, 0));
                }
            }*/
        }

        public override void DoTerminate()
        {
            base.DoTerminate();
            ExplosionPart ins = new(start.x, start.y, true);
            Level.Add(ins);
            SFX.Play("explode", 0.7f, Rando.Float(-0.7f, -0.5f), 0f, false);
            Thing bulletOwner = this.owner;
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
                var bullet = new Bullet_Lava(start.x, start.y, new AT_Lava(), Rando.Float(0, 360), owner, false, 200, false, true)
                {
                    color = Color.DarkOrange
                };
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
