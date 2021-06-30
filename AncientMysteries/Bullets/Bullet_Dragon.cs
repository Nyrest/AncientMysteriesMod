using Microsoft.Xna.Framework.Graphics;

namespace AncientMysteries.Bullets
{
    public sealed class Bullet_Dragon : Bullet
    {
        private Texture2D _beem;

        private float _thickness;

        public Bullet_Dragon(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            _thickness = type.bulletThickness;
            _beem = this.ModTex2D("fireball2.png");
            collisionSize = new Vec2(15, 6);
            _collisionSize = new Vec2(15, 6);
            _center = new Vec2(7.5f, 3f);
            center = new Vec2(7.5f, 3f);
            collisionCenter = new Vec2(7.5f, 3f);
        }

        public override void Update()
        {
            base.Update();
            /*foreach (Thing t in Level.CheckCircleAll<Thing>(this.position,10))
            {
                if (t != Level.CheckCircleAll<Thing>(DuckNetwork.localConnection.profile.duck.position,20))
                {
                    Level.Add(SmallFire.New(t.x, t.y, 0, 0));
                }
            }*/
        }

        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            base.OnCollide(pos, t, willBeStopped);
            Level.Add(SmallFire.New(pos.x, pos.y, 0, 0));
        }
    }
}
