using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_CubicBlast : AmmoType
    {
        public SpriteMap _spriteMap = TexHelper.ModSpriteMap("CubicBlast.png", 8, 8, true);

        public AT_CubicBlast()
        {
            accuracy = 0.6f;
            range = 600f;
            penetration = 1f;
            rangeVariation = 10f;
            bulletSpeed = 2;
            this.speedVariation = 0.5f;
            bulletLength = 50;
            this.bulletColor = Color.LightYellow;
            bulletThickness = 2;
            this.sprite = _spriteMap;
            _spriteMap.AddAnimation("loop", 0.3f, true, 0, 1, 2, 3, 4);
            _spriteMap.SetAnimation("loop");
            _spriteMap.CenterOrigin();
        }

    }
}
