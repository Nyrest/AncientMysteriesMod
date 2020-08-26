using AncientMysteries.Bullets;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Dragon : AmmoType
    {
        public AT_Dragon()
        {
            accuracy = 0.6f;
            range = 400f;
            penetration = 1f;
            rangeVariation = 10f;
            combustable = true;
            //sprite = TexHelper.ModSprite("fireball.png");
            //sprite.CenterOrigin();
            //bulletType = typeof(Bullet_Shadow);
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            this.bulletColor = Color.DarkRed;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
