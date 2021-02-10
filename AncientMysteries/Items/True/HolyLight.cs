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

namespace AncientMysteries.Items.True
{
    [EditorGroup(g_staffs)]
    public class HolyLight : AMStaff
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public int r;

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Holy Light",
        };

        public HolyLight(float xval, float yval) : base(xval, yval)
        {
            this._ammoType = new AT_RainbowEyedrops()
            {

            };
            this._type = "gun";
            _spriteMap = this.ReadyToRunMap("holyLight.png", 15, 37);
            this.SetBox(15, 37);
            this._barrelOffsetTL = new Vec2(6f, 5f);
            this._castSpeed = 0.012f;
            BarrelSmokeFuckOff();
            _flare.color = Color.Transparent;
            this.castingParticlesEnabled = true;
            this.castingParticlesColor = Color.Yellow;

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
            r = Rando.Int(3, 7);
            int count = _castTime >= 0.5 ? r : 1;
            var firedBullets = new List<Bullet>(count);
            for (int i = 0; i < count; i++)
            {
                Bullet b = new Bullet_Star(firePos.x, firePos.y, new AT_Star(), owner.offDir == 1 ? 0 : 180, owner, false, 275);
                firedBullets.Add(b);
                Level.Add(b);
            }
            bulletFireIndex += (byte)count;
            if (Network.isActive)
            {
                NMFireGun gunEvent = new(this, firedBullets, bulletFireIndex, rel: false, 4);
                Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                firedBullets.Clear();
            }
        }
    }
}
