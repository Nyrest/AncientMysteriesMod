using AncientMysteries.AmmoTypes;
using AncientMysteries.Localization.Enums;
using AncientMysteries.Bullets;
using AncientMysteries.Utilities;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AncientMysteries.groupNames;
using AncientMysteries.Items.Miscellaneous;

namespace AncientMysteries.Items.True
{
    [EditorGroup(g_staffs)]
    public class PrimordialLibram : AMStaff
    {
        public StateBinding _animationFrameBinding = new StateBinding("animationFrame");

        public SpriteMap _spriteMap;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "原初圣典",
            _ => "Primordial Libram",
        };

        public PrimordialLibram(float xval, float yval) : base(xval, yval)
        {
            this.ammo = 500;
            this._ammoType = new AT_RainbowEyedrops()
            {

            };
            this._type = "gun";
            _spriteMap = this.ReadyToRunMap("priLibram..png", 21, 14);
            this.SetBox(21, 14);
            this._barrelOffsetTL = new Vec2(6f, 5f);
            this._castSpeed = 0.006f;
            BarrelSmokeFuckOff();
            _flare.color = Color.Transparent;
            this._fireWait = 0.5f;
            this._fireSoundPitch = 0.9f;
            this._kickForce = 0.25f;
            this._fullAuto = true;
            _spriteMap.AddAnimation("out", 200f, false, 0, 1);
            _spriteMap.AddAnimation("back", 200f, false, 1,0);
            _doPose = false;
            progressBgColor = Color.Azure;
            progressFillColor = Color.Gold;
            this._holdOffset = new Vec2(2,3);
        }

        public override void OnSpelling()
        {
            base.OnSpelling();
        }

        public override void Update()
        {
            base.Update();
            if (owner != null)
            {
                _spriteMap.SetAnimation("out");
            }
            else
            {
                _spriteMap.SetAnimation("back");
            }
        }
        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            var firePos = barrelPosition;
            if (_castTime >= 1f)
            {
                TempFire t = new TempFire(this.owner.x, owner.y + 6f, true, owner);
                t.alpha = 0f;
                t.xscale *= 2f;
                t.yscale *= 2f;
                Level.Add(t);
            }
            if (Network.isActive)
            {
                NMFireGun gunEvent = new NMFireGun(this, firedBullets, bulletFireIndex, rel: false, 4);
                Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                firedBullets.Clear();
            }
        }
    }
}
