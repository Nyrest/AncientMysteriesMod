namespace AncientMysteries.Items
{
    public class PrimordialLibram_Bullet_Flower : AMBullet
    {
        public PrimordialLibram_Bullet_Flower(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
        }

        public override void Removed()
        {
            NetHelper.NmFireGun(null, list =>
             {
                 var bullet = Make.Bullet<AT_Leaf>(travelEnd, _owner, Rando.Float(0f, 360f), this);
                 list.Add(bullet);
             });
            base.Removed();
        }
    }
}