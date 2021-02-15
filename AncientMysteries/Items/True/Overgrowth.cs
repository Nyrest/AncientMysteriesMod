using AncientMysteries.AmmoTypes;
using AncientMysteries.Bullets;
using AncientMysteries.Localization.Enums;
using DuckGame;
using System;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.True
{
    [EditorGroup(g_staffs)]
    public class Overgrowth : AMStaff
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public int times = 0;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }


        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Overgrowth",
        };

        public Overgrowth(float xval, float yval) : base(xval, yval)
        {
            this._type = "gun";
            _spriteMap = this.ReadyToRunMap("overgrowth.png", 19, 34);
            this.SetBox(19, 34);
            this._barrelOffsetTL = new Vec2(6f, 5f);
            this._castSpeed = 0.003f;
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
            {
                for (int i = -1; i < times; i++)
                {
                    this.NmFireGun(new Bullet_AN2(firePos.x, firePos.y, new AT_AN(), owner.offDir == 1 ? 0 : 180, owner, false, 275));
                }
            }
            if (times < 10)
            {
                times += 1;
            }
            _castSpeed = Convert.ToSingle(0.003f + 0.0008 * times);
        }
    }
}
