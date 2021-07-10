namespace AncientMysteries.Bullets
{
    public class Bullet_OGS : Bullet
    {
        public Bullet_OGS(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            collisionSize = new Vec2(5, 3);
            _bulletLength = 0f;
            color = Color.Transparent;
        }
    }
}
