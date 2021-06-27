﻿using AncientMysteries.Localization.Enums;

namespace AncientMysteries.Items.Dragon
{
    [EditorGroup(g_machineGuns)]
    public sealed class MoltenSpitter : AMGun
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Molten Spitter",
        };

        public MoltenSpitter(float xval, float yval) : base(xval, yval)
        {
            this.ammo = 127;
            this._ammoType = new AT_Dragon()
            {
                range = 400,
                accuracy = 0.6f,
            };
            this._type = "gun";
            _spriteMap = this.ReadyToRunMap("MoltenSpitter.png", 39, 15);
            this.SetBox(39, 13);
            BarrelSmokeFuckOff();
            this._fireSound = "smg";
            this._fireSoundPitch = -0.9f;
            this._fireWait = 0.6f;
            this._kickForce = 2.3f;
            this._fullAuto = true;
            this._numBulletsPerFire = 2;
            weight = 10f;
            _holdOffset = new Vec2(9f, 2f);
            _spriteMap.AddAnimation("out", 0.2f, false, 0, 1, 2);
            _spriteMap.AddAnimation("back", 0.2f, false, 2, 1, 0);
        }

        public override void Update()
        {
            this._barrelOffsetTL = new Vec2(_spriteMap._frame switch
            {
                2 => 38,
                _ => 35,
            }, 9f);
            _spriteMap.SetAnimation(duck != null ? "out" : "back");
            base.Update();
        }
    }
}
