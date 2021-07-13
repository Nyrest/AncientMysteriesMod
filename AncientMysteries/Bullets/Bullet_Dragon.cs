namespace AncientMysteries.Bullets
{
    public sealed class Bullet_Dragon : Bullet
    {
        public Bullet_Dragon(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            color = Color.OrangeRed;
        }

        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            base.OnCollide(pos, t, willBeStopped);
            Level.Add(SmallFire.New(pos.x, pos.y, 0, 0));
        }
    }
}
