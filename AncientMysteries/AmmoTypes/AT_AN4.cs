﻿using DuckGame;
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
            penetration = 20000000f;
            rangeVariation = 10f;
            bulletSpeed = 6;
            this.speedVariation = 0.5f;
            bulletLength = 0;
            this.bulletColor = Color.LightYellow;
            bulletThickness = 2;
            this.sprite = _spriteMap;
            _spriteMap.CenterOrigin();
        }

    }
}