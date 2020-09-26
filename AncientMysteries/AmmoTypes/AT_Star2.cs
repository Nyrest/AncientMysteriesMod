using AncientMysteries.Bullets;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Star2 : AmmoType
    {
        public AT_Star2()
        {
            accuracy = 1f;
            range = 100f;
            penetration = 1f;
            rangeVariation = 0;
            bulletSpeed = 2;
            combustable = true;
            sprite = TexHelper.ModSprite("holyLight2.png");
            //sprite.CenterOrigin();
            bulletType = typeof(Bullet_Star);
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            this.bulletColor = Color.Yellow;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
