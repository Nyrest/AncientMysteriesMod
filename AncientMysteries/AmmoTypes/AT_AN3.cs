﻿namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_AN3 : AmmoType
    {
        public SpriteMap _spriteMap = TexHelper.ModSpriteMap("novaFrm3.png", 18, 18, true);

        public AT_AN3()
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
            _spriteMap.AddAnimation("loop", 0.15f, true, 0, 1, 2, 3);
            _spriteMap.SetAnimation("loop");
            _spriteMap.CenterOrigin();
        }

    }
}
