using System.Collections.Generic;

namespace AncientMysteries.Items.True
{
    public class PrimordialLibram_Bullet_Flower : AMBullet
    {
        public PrimordialLibram_Bullet_Flower(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {

        }

        public override void Removed()
        {
            var firedBullets = new List<Bullet>(5);
            for (int i = 0; i < 5; i++)
            {
                var bullet = Make.Bullet<AT_Leaf>(travelEnd, _owner, Rando.Float(0f, 360f), this);
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
