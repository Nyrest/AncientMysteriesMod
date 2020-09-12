using DuckGame;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.Bullets
{
    public sealed class Bullet_Skull : Bullet
    {
        public MaterialGlitch glitch;

        public SpriteMap _spriteMap;

        public bool flyRight;

        public Bullet_Skull(float xval, float yval, AmmoType type, float ang = -1, bool dir = true, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            if (dir)
            {
                _spriteMap = TexHelper.ModSpriteMap("SkullR.png", 25, 14, true);
            }
            else
            {
                _spriteMap = TexHelper.ModSpriteMap("Skull.png", 25, 14, true);
            }
            _spriteMap.AddAnimation("loop", 0.3f, true, 0, 1, 2, 3);
            _spriteMap.SetAnimation("loop");
            _spriteMap.CenterOrigin();
            glitch = new MaterialGlitch(this);
        }

        public override void Update()
        {
            base.Update();

        }

        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            base.OnCollide(pos, t, willBeStopped);
        }

        public override void Draw()
        {
            base.Draw();
            material = glitch;
            _spriteMap.depth = 1f;
            _spriteMap.angleDegrees = 0f - Maths.PointDirection(Vec2.Zero, travelDirNormalized);
            Graphics.Draw(_spriteMap, start.x, start.y);
            material = null;
        }
    }
}
