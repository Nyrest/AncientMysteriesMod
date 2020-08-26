using AncientMysteries.Bullets;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Color : AmmoType
    {
        public Color _color;

        public AT_Color(Color color)
        {
            _color = color;
            accuracy = 0.9f;
            range = 500f;
            penetration = 2f;
            rangeVariation = 10f;
            combustable = true;
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            this.bulletColor = _color;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
