using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AncientMysteries.Bullets
{
    public sealed class Bullet_Flowerr : Bullet
    {
        private Texture2D _beem;

        private float _thickness;

        public Bullet_Flowerr(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            _thickness = type.bulletThickness;
            _beem = this.ModTex2D("flower.png");
            alphaSub = 0f;
        }

        public override void Update()
        {
            base.Update();
            if (this.graphic != null)
            {
                this.graphic._angle++;
            }
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
            var firedBullets = new List<Bullet>(5);
            for (int i = 0; i < 3; i++)
            {
                var bullet = new Bullet_Icicle(travelEnd.x, travelEnd.y, new AT_Leaf(), Rando.Float(0, 360), owner, false, 20, false, true);
                firedBullets.Add(bullet);
                Level.Add(bullet);
            }
            if (Network.isActive && this.isLocal)
            {
                NMFireGun gunEvent = new(null, firedBullets, 0, rel: false, 4);
                Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                firedBullets.Clear();
            }
        }
    }
}
