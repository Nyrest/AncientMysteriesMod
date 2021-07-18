using Microsoft.Xna.Framework.Graphics;

namespace AncientMysteries.Bullets
{
    public sealed class Bullet_Shadow : Bullet
    {
        private readonly Texture2D _beem;

        private readonly float _thickness;

        public Bullet_Shadow(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            _thickness = type.bulletThickness;
            _beem = this.ModTex2D(tex_Bullet_ShadowBullet);
        }

        public override void Draw()
        {
            if (_tracer || !(_bulletDistance > 0.1f))
            {
                return;
            }
            float length = (drawStart - drawEnd).length;
            float dist = 0f;
            float incs = 1f / (length / 8f);
            float alph = 0f;
            float drawLength = 8f;
            while (true)
            {
                bool doBreak = false;
                if (dist + drawLength > length)
                {
                    drawLength = length - Maths.Clamp(dist, 0f, 99f);
                    doBreak = true;
                }
                alph += incs;
                Graphics.DrawTexturedLine(_beem, drawStart + travelDirNormalized * dist, drawStart + travelDirNormalized * (dist + drawLength), Color.White * alph, _thickness, 0.6f);
                if (doBreak)
                {
                    break;
                }
                dist += 8f;
            }
        }

        protected override void Rebound(Vec2 pos, float dir, float rng)
        {
            isRebound = true;
            Bullet_Shadow bullet = new(pos.x, pos.y, ammo, dir, null, rebound, rng);
            isRebound = false;
            bullet._teleporter = _teleporter;
            bullet.firedFrom = firedFrom;
            Level.current.AddThing(bullet);
        }
    }
}