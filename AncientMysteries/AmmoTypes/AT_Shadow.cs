using AncientMysteries.Bullets;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Shadow : AmmoType
    {
        public AT_Shadow()
        {
            accuracy = 0.9f;
            range = 600f;
            penetration = 2f;
            rangeVariation = 10f;
            combustable = true;
            this.bulletColor = Color.Purple;
            //bulletType = typeof(Bullet_Shadow);
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            this.bulletColor = Color.Purple;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
