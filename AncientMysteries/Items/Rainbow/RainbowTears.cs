using AncientMysteries.AmmoTypes;
using AncientMysteries.Localization.Enums;
using AncientMysteries.Utilities;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Rainbow
{
    [EditorGroup(g_staffs)]
    public class RainbowTears : AMStaff
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
            _ => "Chromatic Tears",
        };

        public RainbowTears(float xval, float yval) : base(xval, yval)
        {         
            this._ammoType = new AT_RainbowEyedrops()
            {

            };
            this._type = "gun";
            _spriteMap = this.ReadyToRunMap("judgement.png", 13, 39);
            this.SetBox(13, 39);
            this._barrelOffsetTL = new Vec2(6f, 5f);
            this._castSpeed = 0.01f;
            BarrelSmokeFuckOff();
            _flare.color = Color.Transparent;
            this._fireSound = "flameExplode";
            this._fireWait = 0.5f;
            this._fireSoundPitch = 0.9f;
            this._kickForce = 0.25f;
            this._fullAuto = true;
            _spriteMap.AddAnimation("loop", 0.04f, true, 0, 1, 2, 1);
        }

        public override void OnSpelling()
        {
            base.OnSpelling();
        }

        public override void Update()
        {
            base.Update();
            Graphics.material = null;
            if (_castTime > 0.3f)
            {
                this._castSpeed = 0.005f;
            }
            else this._castSpeed = 0.01f;
            if (IsSpelling)
            {
                if (_castTime > 0.3f)
                {
                    Vec2 barrelPos = barrelPosition;
                    int count = Rando.Int(1, 2);
                    for (int i = 0; i < count; i++)
                    {
                        Bullet bullet = new Bullet(
                            barrelPos.x + Rando.Float(-3, 3),
                            barrelPos.y + Rando.Float(-3, 3), ammoType, 90 + Rando.Float(-10, 10), duck)
                        {
                            color = HSL.FromHslFloat(Rando.Float(0f, 1f), Rando.Float(0.7f, 1f), Rando.Float(0.45f, 0.65f)),
                            firedFrom = this,
                            range = 2000
                        };
                        firedBullets.Add(bullet);
                        Level.Add(bullet);
                    }
                    bulletFireIndex += (byte)count;
                    if (Network.isActive)
                    {
                        NMFireGun gunEvent = new NMFireGun(this, firedBullets, bulletFireIndex, false, 4);
                        Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                        firedBullets.Clear();
                    }
                }
            }
            if (duck != null)
            {
                _spriteMap.SetAnimation("loop");
            }
            else
            {
                _spriteMap.currentAnimation = null;
            }
        }
    }
}
