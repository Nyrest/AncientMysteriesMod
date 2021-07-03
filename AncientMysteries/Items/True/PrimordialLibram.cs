namespace AncientMysteries.Items.True
{
    [EditorGroup(g_staffs)]
    public class PrimordialLibram : AMStaff
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
            //this._castSpeed = 0.006f;//0.006
            this._castSpeed = 1f;
            BarrelSmokeFuckOff();
            _flare.color = Color.Transparent;
            this._fireWait = 0.5f;
            this._fireSoundPitch = 0.9f;
            this._kickForce = 0.25f;
            this._fullAuto = true;
            _spriteMap.AddAnimation("out", 200f, false, 0, 1);
            _spriteMap.AddAnimation("back", 200f, false, 1, 0);
            _doPose = false;
            progressBgColor = Color.DeepSkyBlue;
            progressFillColor = Color.LightYellow;
            this._holdOffset = new Vec2(2, 3);
        }

        public override void OnSpelling()
        {
            base.OnSpelling();
        }

        #region Fire
        public const int totalFireBallCount = 6;
        public bool cast_FireBall = false;
        public int currentFireBallCount = 0;
        public Waiter fireBallWaiter = new Waiter(25);
        public AT_BigFB at_fb = new();

        public void FireBallUpdate()
        {
            if (cast_FireBall == false) return;
            if (fireBallWaiter.Tick())
            {
                if (currentFireBallCount++ < totalFireBallCount)
                {
                    this.NmFireGun(list =>
                    {
                        list.Add(Make.Bullet<AT_BigFB>(ownerPos, owner, (owner._offDir == 1 ? 0 : 180) + Rando.Float(-5, 5), this));
                    });
                }
                else
                {
                    cast_FireBall = false;
                    currentFireBallCount = 0;
                }
            }
        }
        #endregion

        #region Icicle
        public const int totalIcicleCount = 25;
        public bool cast_Icicle = false;
        public int currentIcicleCount = 0;
        public Vec2 icicle_pos;
        public Waiter icicleWaiter = new Waiter(6);

        public void IcicleUpdate()
        {
            if (cast_Icicle == false) return;
            if (icicleWaiter.Tick())
            {
                if (currentIcicleCount++ < totalIcicleCount)
                {
                    this.NmFireGun(list =>
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            list.Add(Make.Bullet<AT_Icicle>(icicle_pos, base.owner, Rando.Float(0, 360), this));
                        }
                        SFX.PlaySynchronized("goody", 0.4f, Rando.Float(0.2f, 0.4f));
                    });
                }
                else
                {
                    cast_Icicle = false;
                    currentIcicleCount = 0;
                }
            }
        }
        #endregion

        #region Green
        public void GreenFire(Vec2 pos)
        {
            this.NmFireGun(list =>
            {
                for (int i = 0; i < 5; i++)
                {
                    var b = Make.Bullet<AT_Flower>(pos, owner, owner._offDir == 1 ? 0 : 180 + Rando.Float(-10,10), this);
                    list.Add(b);
                }
            });
        }
        #endregion

        #region Lightning
        public const int totalLCount = 25;
        public bool cast_L = false;
        public int currentLCount = 0;
        public Vec2 l_pos;
        public Waiter lWaiter = new Waiter(6);

        public void LightningUpdate()
        {
            if (cast_L == false) return;
            if (lWaiter.Tick())
            {
                if (currentLCount++ < totalLCount)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        this.NmFireGun(list =>
                        {
                            var b = Make.Bullet<AT_LaserY>(new Vec2(l_pos.x + Rando.Float(-r, r), l_pos.y - 200f + Rando.Float(-r / 2, r / 2)),owner,
                            Rando.Float(Convert.ToSingle(80f - r / 3.5f), Convert.ToSingle(100 + r / 3.5f)),this);
                            ExplosionPart ins = new(b.travelStart.x, b.travelStart.y, true);
                            ins.xscale *= 0.2f;
                            ins.yscale *= 0.2f;
                            Level.Add(ins);
                            list.Add(b);
                        });
                    }
                    SFX.PlaySynchronized("explode", 0.4f, Rando.Float(0.2f, 0.4f));
                }
                else
                {
                    cast_L = false;
                    currentLCount = 0;
                }
            }
        }
        #endregion

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
            /*
            if (owner != null)
            {
                _spriteMap.SetAnimation("out");
                castPos = owner.position;
            }
            else
            {
                _spriteMap.SetAnimation("back");
                waiter.Reset();
                waiter.Pause();
            }
                if (owner != null)
                {
                    if (rando == 0 && waiter.Tick())
                    {

                    }
                    if (rando == 1 && waiter.Tick())
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
                            SFX.PlaySynchronized("goody", 0.4f, Rando.Float(0.2f, 0.4f));
                        });
                    }
                    if (rando == 2)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            this.NmFireGun(list =>
                        {
                            b = new Bullet_Laser(pos.x + Rando.Float(-r, r), pos.y - 200f + Rando.Float(-r / 2, r / 2), 
            new AT_LaserY(), Rando.Float(-100f - Convert.ToSingle(r / 3.5f), Convert.ToSingle(-80 + r / 3.5f)), owner, false, 400);
            ExplosionPart ins = new(b.travelStart.x, b.travelStart.y, true);
            ins.xscale *= 0.2f;
            ins.yscale *= 0.2f;
            Level.Add(ins);
            list.Add(b);
        });
                        }
}
                }
            if (owner != null && owner._offDir == 1)
{
    fireAngle = 0;
}
else
{
    fireAngle = 180;
}
*/

        }
        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
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
                    l_pos = position;
                    r = 0f;
                    cast_L = true; break;
                default:
                    // Debug so always fire ball
                    goto case 3;
            }
        }
    }
}
