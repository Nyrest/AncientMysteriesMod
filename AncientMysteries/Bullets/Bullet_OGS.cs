using DuckGame;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.Bullets
{
    public class Bullet_OGS : Bullet
    {
        public Bullet_OGS(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            this.collisionSize = new Vec2(5, 3);
            _bulletLength = 0f;
            this.color = Color.Transparent;
        }
    }
}
