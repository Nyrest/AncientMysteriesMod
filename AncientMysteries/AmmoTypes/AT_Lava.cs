using AncientMysteries.Bullets;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Lava : AmmoType
    {
        public AT_Lava()
        {
            accuracy = 1f;
            range = 100f;
            penetration = 1f;
            rangeVariation = 0;
            bulletSpeed = 2;
            combustable = true;
            affectedByGravity = true;;
            //sprite.CenterOrigin();
            bulletType = typeof(Bullet_Lava);
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            this.bulletColor = Color.DarkOrange;
            var result = base.FireBullet(position, owner, angle, firedFrom);
            result.color = Color.DarkOrange;
            return result;
        }
    }
}
