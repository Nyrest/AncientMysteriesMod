﻿using AncientMysteries.AmmoTypes;
using AncientMysteries.Localization.Enums;
using AncientMysteries.Bullets;
using AncientMysteries.Utilities;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.True
{
    [EditorGroup(g_staffs)]
    public class ArcaneNova : AMStaff
    {
        public StateBinding _animationFrameBinding = new StateBinding(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }


        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "奥术新星",
            _ => "Arcane Nova",
        };

        public ArcaneNova(float xval, float yval) : base(xval, yval)
        {
            this._type = "gun";
            _spriteMap = this.ReadyToRunMap("arcaneNova.png", 14, 37);
            this.SetBox(14, 37);
            this._barrelOffsetTL = new Vec2(6f, 5f);
            this._castSpeed = 0.007f;
            BarrelSmokeFuckOff();
            _flare.color = Color.Transparent;
            this._fireWait = 0.5f;
            this._fireSoundPitch = 0.9f;
            this._kickForce = 0.25f;
            this._fullAuto = true;
        }

        public override void OnSpelling()
        {
            base.OnSpelling();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            var firePos = barrelPosition;
            if (_castTime >= 1f)
                this.nmFireGun(new Bullet_AN(firePos.x, firePos.y, new AT_AN(), owner.offDir == 1 ? 0 : 180, owner, false, 275));
        }
    }
}
