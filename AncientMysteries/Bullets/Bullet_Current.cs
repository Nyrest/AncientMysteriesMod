using Microsoft.Xna.Framework.Graphics;

namespace AncientMysteries.Bullets
{
    public sealed class Bullet_Current : LaserBullet
    {
        private Texture2D _beem;

        private float _thickness;

        public Bullet_Current(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            _thickness = type.bulletThickness;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            base.OnCollide(pos, t, willBeStopped);

        }
    }
}
