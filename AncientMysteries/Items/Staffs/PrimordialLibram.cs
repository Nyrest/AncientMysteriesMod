﻿namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_PrimordialSpellbook, 21, 14, 0, 1)]
    [MetaInfo(Lang.Default, "Primordial Libram", "Everything is born and withers away, for they are all affected by this.")]
    [MetaInfo(Lang.schinese, "源生法典", "万物生而凋零，一切皆因于此。")]
    [MetaType(MetaType.Magic)]
    [BaggedProperty("isSuperWeapon", true)]
    public partial class PrimordialLibram : AMStaff
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public Vec2 ownerPos;

        public int fireAngle = 90;

        public float r = 0;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public PrimordialLibram(float xval, float yval) : base(xval, yval)
        {
            _type = "gun";
            _spriteMap = this.ReadyToRunWithFrames(tex_Staff_PrimordialSpellbook, 21, 14);
            SetBox(21, 14);
            _barrelOffsetTL = new Vec2(6f, 5f);
            _castSpeed = 0.006f;
            BarrelSmokeFuckOff();
            _flare.color = Color.Transparent;
            _fireWait = 0.5f;
            _fireSoundPitch = 0.9f;
            _kickForce = 0.25f;
            _fullAuto = true;
            _spriteMap.AddAnimation("out", 200f, false, 0, 1);
            _spriteMap.AddAnimation("back", 200f, false, 1, 0);
            _doPose = false;
            progressBgColor = Color.DeepSkyBlue;
            progressFillColor = Color.LightYellow;
            _holdOffset = new Vec2(2, 3);
        }

        public override void OnSpelling()
        {
            base.OnSpelling();
        }

        #region Fire

        public const int totalFireBallCount = 6;
        public bool cast_FireBall = false;
        public int currentFireBallCount = 0;
        public Waiter fireBallWaiter = new Waiter(30).TickToEnd();

        public void FireBallUpdate()
        {
            if (cast_FireBall == false) return;
            if (fireBallWaiter.Tick())
            {
                if (owner != null)
                {
                    if (currentFireBallCount++ < totalFireBallCount)
                    {
                        this.NmFireGun(list =>
                        {
                            list.Add(Make.Bullet<AT_BigFB>(ownerPos, duck, (owner._offDir == 1 ? 0 : 180) + Rando.Float(-5, 5), this));
                        });
                    }
                    else
                    {
                        fireBallWaiter.TickToEnd();
                        cast_FireBall = false;
                        currentFireBallCount = 0;
                    }
                }
                else
                {
                    fireBallWaiter.TickToEnd();
                    cast_FireBall = false;
                    currentFireBallCount = 0;
                }
            }
        }

        #endregion Fire

        #region Icicle

        public const int totalIcicleCount = 25;
        public bool cast_Icicle = false;
        public int currentIcicleCount = 0;
        public Vec2 icicle_pos;
        public Waiter icicleWaiter = new Waiter(7).TickToEnd();

        public void IcicleUpdate()
        {
            if (cast_Icicle == false) return;
            if (icicleWaiter.Tick())
            {
                if (owner != null)
                {
                    if (currentIcicleCount++ < totalIcicleCount)
                    {
                        this.NmFireGun(list =>
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                list.Add(Make.Bullet<AT_Icicle>(icicle_pos, duck, Rando.Float(0, 360), this));
                            }
                            SFX.PlaySynchronized("goody", 0.4f, Rando.Float(0.2f, 0.4f));
                        });
                    }
                    else
                    {
                        icicleWaiter.TickToEnd();
                        cast_Icicle = false;
                        currentIcicleCount = 0;
                    }
                }
                else
                {
                    icicleWaiter.TickToEnd();
                    cast_Icicle = false;
                    currentIcicleCount = 0;
                }
            }
        }

        #endregion Icicle

        #region Green

        public void GreenFire(Vec2 pos)
        {
            this.NmFireGun(list =>
            {
                for (int i = 0; i < 4; i++)
                {
                    PrimordialLibram_ThingBullet_Flower b = new(barrelPosition, GetBulletVecDeg((duck.offDir == 1 ? 0 : 180) + Rando.Float(-15, 15), Rando.Float(3, 5)), duck);
                    Level.Add(b);
                }
            });
        }

        #endregion Green

        #region Lightning

        public const int totalLightningCount = 20;
        public bool cast_Lightning = false;
        public int currentLightningCount = 0;
        public Vec2 lightning_pos;
        public Waiter lightningWaiter = new Waiter(6).TickToEnd();

        public void LightningUpdate()
        {
            if (cast_Lightning == false) return;
            if (lightningWaiter.Tick())
            {
                if (owner != null)
                {
                    if (currentLightningCount++ < totalLightningCount)
                    {
                        this.NmFireGun(list =>
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                var b = Make.Bullet<AT_LaserY>(
                                        new Vec2(
                                            lightning_pos.x + Rando.Float(-r, r),
                                            lightning_pos.y - 200f + Rando.Float(-r / 2, r / 2)),
                                        duck,
                                        Rando.Float(
                                            Convert.ToSingle(80f - (r / 3.5f)),
                                            Convert.ToSingle(100 + (r / 3.5f))), this);
                                ExplosionPart ins = new(b.travelStart.x, b.travelStart.y, true);
                                ins.xscale *= 0.2f;
                                ins.yscale *= 0.2f;
                                Level.Add(ins);
                                list.Add(b);
                            }
                        });
                        SFX.PlaySynchronized("explode", 0.4f, Rando.Float(0.2f, 0.4f));
                    }
                    else
                    {
                        lightningWaiter.TickToEnd();
                        cast_Lightning = false;
                        currentLightningCount = 0;
                    }
                }
                else
                {
                    lightningWaiter.TickToEnd();
                    cast_Lightning = false;
                    currentLightningCount = 0;
                }
            }
        }

        #endregion Lightning

        public override void Update()
        {
            base.Update();
            if (owner != null)
            {
                _spriteMap.SetAnimation("out");
                ownerPos = owner.position;
            }
            else
            {
                _spriteMap.SetAnimation("back");
            }
            FireBallUpdate();
            IcicleUpdate();
            LightningUpdate();
            r += 0.8f;
        }

        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            if (_castTime >= 1)
            {
                switch (Rando.Int(0, 3))
                {
                    case 0:
                        cast_FireBall = true; break;
                    case 1:
                        icicle_pos = position;
                        cast_Icicle = true; break;
                    case 2:
                        GreenFire(position); break;
                    case 3:
                        lightning_pos = position;
                        r = 0f;
                        cast_Lightning = true; break;
                    default:
                        // Debug so always fire ball
                        goto case 3;
                }
            }
        }
    }
}