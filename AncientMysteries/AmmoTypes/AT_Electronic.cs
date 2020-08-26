using AncientMysteries.Bullets;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Electronic : AmmoType
    {
        public AT_Electronic()
        {
            accuracy = 0.9f;
            range = 200f;
            penetration = 10f;
            rangeVariation = 20f;
            this.bulletThickness = 2f;
            //this.bulletType = typeof(Bullet_Electronic);
            //this.sprite = TexHelper.ModSprite("ElectronicStar.png");
            //this.sprite.CenterOrigin();
            //bulletLength = 10;
        }

        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            this.bulletColor = Color.Lime;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
