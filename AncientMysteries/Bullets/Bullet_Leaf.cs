namespace AncientMysteries.Bullets
{
    public sealed class Bullet_Leaf : Bullet
    {
        public Bullet_Leaf(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {

        }
    }
}
