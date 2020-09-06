using AncientMysteries.AmmoTypes;
using AncientMysteries.Localization.Enums;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Rainbow
{
    [EditorGroup(topAndSeries + "Rainbow")]
    public class BloodyEyedrops : AMStaff
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
            AMLang.schinese => "棱彩流光",
            _ => "Chromatic Streamer",
        };

        public BloodyEyedrops(float xval, float yval) : base(xval, yval)
        {
            this.ammo = 500;
            this._ammoType = new AT_BloodyEyedrop()
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
            this._fireWait = 0.6f;
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
            if(_castTime > 0.3f)
            {
                this._castSpeed = 0.005f;
            }
            else this._castSpeed = 0.01f;
            if (IsSpelling)
            {
                if(_castTime > 0.3f)
                {
                    Vec2 barrelPos = barrelPosition;
                    for (int i = 0; i < 2; i++)
                    {
                        Bullet bullet = new Bullet(
                            barrelPos.x,
                            barrelPos.y, ammoType, 90 + Rando.Float(-10, 10));
                        bullet.firedFrom = this;
                        firedBullets.Add(bullet);
                        Level.Add(bullet);
                    }
                    bulletFireIndex += 20;
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
