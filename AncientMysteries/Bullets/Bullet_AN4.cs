namespace AncientMysteries.Bullets
{
    public class Bullet_AN4 : Bullet
    {
        public Bullet_AN4(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            collisionSize = new Vec2(14, 6);
            _collisionSize = new Vec2(14, 6);
        }
    }
}
