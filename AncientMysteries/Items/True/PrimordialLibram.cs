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
        public StateBinding _animationFrameBinding = new StateBinding(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public int rando = 0;

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
            this._ammoType = new AT_RainbowEyedrops()
            {

            };
            this._type = "gun";
            _spriteMap = this.ReadyToRunMap("priLibram.png", 21, 14);
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
            progressBgColor = Color.DeepSkyBlue;
            progressFillColor = Color.LightYellow;
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
            rando = new Random().Next(0,0);
            if (_castTime >= 1f && rando == 0)
            {
                TempFire t = new TempFire(this.owner.x, owner.y, true, owner);
                t.alpha = 0f;
                t.xscale *= 2f;
                t.yscale *= 2f;
                Level.Add(t);
            }
            if (_castTime >= 1f && rando == 1)
            {
                TempIce i = new TempIce(this.owner.x, owner.y, true, owner);
                i.alpha = 0f;
                i.xscale *= 2f;
                i.yscale *= 2f;
                Level.Add(i);
            }
            if (_castTime >= 1f && rando == 2)
            {
                TempCrystal c = new TempCrystal(this.owner.x, owner.y - 32f, true, owner);
                c.alpha = 0f;
                c.xscale *= 2f;
                c.yscale *= 2f;
                Level.Add(c);
            }
            if (_castTime >= 1f && rando == 3)
            {
                TempNature n = new TempNature(this.owner.x, owner.y - 32f, true, owner);
                n.alpha = 0f;
                n.xscale *= 2f;
                n.yscale *= 2f;
                Level.Add(n);
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
