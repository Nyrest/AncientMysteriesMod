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
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public int rando = 0;

        public Vec2 castPos;

        public int interval = 0;

        //public int greenInterval = 3;

        public bool start = false;

        //public bool greenStart = false;

        public int timer = 0;

        //public int greenTimer = 0;

        public int limiter = 0;

        public Bullet b;

        public int fireAngle = 90;

        public Vec2 pos = new Vec2();

        public float r = 0;

        //public int greenCount = 5;

        //public float nearest = 0f;
        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
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
            this._castSpeed = 1f;//0.006
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
            if (owner != null)
            {
                castPos = owner.position;
            }
            if (start)
            {
                timer++;
                limiter++;
                if (owner != null)
                {
                    if (rando == 0 && timer == interval)
                    {
                        this.NmFireGun(list =>
                        {
                            b = new Bullet_BigFB(castPos.x, castPos.y, new AT_BigFB(), fireAngle + Rando.Float(-5, 5), owner, false, 400)
                            {
                                color = Color.Orange
                            };
                            list.Add(b);
                            timer = 0;
                        });
                    }
                    if (rando == 1 && timer == interval)
                    {
                        this.NmFireGun(list =>
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                b = new Bullet_Icicle(pos.x, pos.y, new AT_Icicle(), Rando.Float(0, 360), owner, false, 250)
                                {
                                    color = Color.White
                                };
                                list.Add(b);
                            }
                            timer = 0;
                            SFX.Play("goody", 0.4f, Rando.Float(0.2f, 0.4f));
                        });
                    }
                    if (rando == 2 && timer == interval)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            this.NmFireGun(list =>
                        {
                            b = new Bullet_Laser(pos.x + Rando.Float(-r, r), pos.y - 200f + Rando.Float(-r / 2, r / 2), /*new AT9mm
                            {
                            bulletSpeed = 2f,
                            accuracy = 1f,
                            penetration = 1f,
                            bulletLength = 3,
                            }*/new AT_LaserY(), Rando.Float(-100f - Convert.ToSingle(r / 3.5f), Convert.ToSingle(-80 + r / 3.5f)), owner, false, 400);
                            ExplosionPart ins = new(b.travelStart.x, b.travelStart.y, true);
                            ins.xscale *= 0.2f;
                            ins.yscale *= 0.2f;
                            Level.Add(ins);
                            list.Add(b);
                            timer = 0;
                        });
                        }
                    }
                }
            }
            if (limiter == 160)
            {
                start = false;
                limiter = 0;
                timer = 0;
            }
            if (owner != null && owner._offDir == 1)
            {
                fireAngle = 0;
            }
            else
            {
                fireAngle = 180;
            }
            r += 0.8f;
        }
        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            var firePos = barrelPosition;
            rando = new Random().Next(0,4);
            if (_castTime >= 1f && rando == 0)
            {
                /*TempFire t = new(this.owner.x, owner.y, true, owner)
                {
                    alpha = 0f
                };
                t.xscale *= 2f;
                t.yscale *= 2f;
                Level.Add(t);*/
                /*if (timer >= 22 && removing == false)
                {*/
                    this.NmFireGun(list =>
                    {
                        b = new Bullet_BigFB(castPos.x, castPos.y, new AT_BigFB(), fireAngle + Rando.Float(-5,5), owner, false, 400)
                        {
                            color = Color.Orange
                        };
                        list.Add(b);
                        start = true;
                        interval = 25;
                        limiter = 0;
                    });
                    /*timer = 0;
                    timer2++;
                }
                if (timer2 == 8 && removing == false)
                {
                    removing = true;
                    progress = 1;
                }
                timer++;*/
            }
            if (_castTime >= 1f && rando == 1)
            {
                /*TempIce i = new(this.owner.x, owner.y, true, owner)
                {
                    alpha = 0f
                };
                i.xscale *= 2f;
                i.yscale *= 2f;
                Level.Add(i);*/
                pos = owner.position;
                start = true;
                interval = 7;
                limiter = 0;
            }
            if (_castTime >= 1f && rando == 2)
            {
                /*TempCrystal c = new(this.owner.x, owner.y - 32f, true, owner)
                {
                    alpha = 0f
                };
                c.xscale *= 2f;
                c.yscale *= 2f;
                Level.Add(c);*/
                pos = owner.position;
                r = 0f;
                for (int i = 0; i < 2; i++)
                {
                    this.NmFireGun(list =>
                    {

                        b = new Bullet_Laser(pos.x + Rando.Float(-r, r), pos.y - 200f + Rando.Float(-r / 2, r / 2), /*new AT9mm
                {
                    bulletSpeed = 2f,
                    accuracy = 1f,
                    penetration = 1f,
                    bulletLength = 3,
                }*/new AT_LaserY(), Rando.Float(-100f - Convert.ToSingle(r / 3.5f), Convert.ToSingle(-80 + r / 3.5f)), owner, false, 400);
                    ExplosionPart ins = new(b.travelStart.x, b.travelStart.y, true);
                    ins.xscale *= 0.2f;
                    ins.yscale *= 0.2f;
                    Level.Add(ins);

                    });
                }
                start = true;
                interval = 5;
                limiter = 0;
            }
            if (_castTime >= 1f && rando == 3)
            {
                /*TempNature n = new(this.owner.x, owner.y - 32f, true, owner)
                {
                    alpha = 0f
                };
                n.xscale *= 2f;
                n.yscale *= 2f;
                Level.Add(n);*/
                for (int i = 0; i < 5; i++)
                {
                    this.NmFireGun(list =>
                    {
                        b = new Bullet_Flowerr(castPos.x, castPos.y, new AT_Flower(), fireAngle + Rando.Float(-15f,15f), owner, false, 250)
                        {
                            color = Color.White
                        };
                        list.Add(b);
                    });
                }
            }
            if (Network.isActive)
            {
                NMFireGun gunEvent = new(this, firedBullets, bulletFireIndex, rel: false, 4);
                Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                firedBullets.Clear();
            }
        }
    }
}
