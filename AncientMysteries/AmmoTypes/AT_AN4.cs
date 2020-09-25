using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_AN4 : AmmoType
    {
        public SpriteMap _spriteMap = TexHelper.ModSpriteMap("nova4.png", 14, 6, true);

        public AT_AN4()
        {
            accuracy = 1f;
            range = 275f;
            penetration = 200000f;
            bulletSpeed = 5;
            this.speedVariation = 0.5f;
            bulletLength = 50;
            this.bulletColor = Color.MediumPurple;
            bulletThickness = 2.5f;
            this.sprite = _spriteMap;
        }
        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            this.bulletColor = Color.MediumPurple;
            return base.FireBullet(position, owner, angle, firedFrom);
        }
    }
}
