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

        public AT_Overgrowth ammoTypeSmall = new(false)
        {
            penetration = 1f
        };

        public AT_Overgrowth ammoTypeBig = new(true)
        {
            penetration = 1f
        };

        public AT_Overgrowth ammoTypeSmall2 = new(false)
        {
            bulletSpeed = 1f,
            accuracy = 1f,
            speedVariation = 0f,
            rangeVariation = 0f,
            penetration = 2147483647f
        };

        public Overgrowth(float xval, float yval) : base(xval, yval)
        {
            this._type = "gun";
            _ammoType = ammoTypeSmall;
            _spriteMap = this.ReadyToRunMap(Texs.Overgrowth, 21, 34);
            _spriteMap.AddAnimation("loop", 0.1f, true, 0, 1, 2, 3);
            _spriteMap.SetAnimation("loop");
            this.SetBox(21, 34);
            this._barrelOffsetTL = new Vec2(6f, 5f);
            this._castSpeed = 0.0035f;
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
                this.NmFireGun(list =>
                {
                    for (int i = -1; i < Math.Ceiling(Convert.ToDecimal(times / 2)); i++)
                    {

                        list.Add(new Bullet_OGB(firePos.x, firePos.y, ammoTypeBig, owner.offDir == 1 ? 0 : 180, owner, false, 170 + Rando.Float(-50, 50)));
                    }
                    for (int i = -1; i < times * 2; i++)
                    {
                        list.Add(new Bullet_OGS(firePos.x, firePos.y, ammoTypeSmall, owner.offDir == 1 ? 0 : 180, owner, false, 245 + Rando.Float(-80, 80)));
                    }
                });
            }
            if (times == 10 && _castTime >= 1f)
            {
                SFX.PlaySynchronized("scoreDing", 0.7f, 0.1f, 0, false);
                foreach (Duck d in Level.CheckCircleAll<Duck>(owner.position, 999))
                {
                    if (d != owner)
                    {
                        this.NmFireGun(list =>
                        {
                            list.Add(new Bullet_OGS(d.x - 40, d.y - 40, ammoTypeSmall2, Maths.PointDirection(d.x - 40, d.y - 40, d.x, d.y), owner, false, 60));
                            list.Add(new Bullet_OGS(d.x + 40, d.y - 40, ammoTypeSmall2, Maths.PointDirection(d.x + 40, d.y - 40, d.x, d.y), owner, false, 60));
                            list.Add(new Bullet_OGS(d.x - 40, d.y + 40, ammoTypeSmall2, Maths.PointDirection(d.x - 40, d.y + 40, d.x, d.y), owner, false, 60));
                            list.Add(new Bullet_OGS(d.x + 40, d.y + 40, ammoTypeSmall2, Maths.PointDirection(d.x + 40, d.y + 40, d.x, d.y), owner, false, 60));
                        });
                    }
                }
            }
            if (times < 10 && _castTime >= 1f)
            {
                times += 1;
                SFX.PlaySynchronized("scoreDing", 0.5f, Convert.ToSingle(-0.3 + times * 0.03f), 0, false);
            }
            _castSpeed = Convert.ToSingle(0.0035f + 0.008 * times);
        }
    }
}
