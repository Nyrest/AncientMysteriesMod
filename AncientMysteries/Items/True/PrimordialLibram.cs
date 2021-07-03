namespace AncientMysteries.Items.True
{
    [EditorGroup(g_staffs)]
    public class PrimordialLibram : AMStaff
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public Vec2 ownerPos;

        public int fireAngle = 90;

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
            this._castSpeed = 0.01f;
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

        public void FireBallUpdate()
        {
            if (cast_FireBall == false) return;
            if (fireBallWaiter.Tick())
            {
                if (currentFireBallCount++ < totalFireBallCount)
                {
                    this.NmFireGun(list =>
                    {
                        var fireball = new Bullet_BigFB(ownerPos.x, ownerPos.y, new AT_BigFB(), (owner._offDir == 1 ? 0 : 180) + Rando.Float(-5, 5), owner, false, 400);
                        list.Add(fireball);
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

        #region Fire
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
                            var b = new Bullet_Icicle(icicle_pos.x, icicle_pos.y, new AT_Icicle(), Rando.Float(0, 360), owner, false, 250);
                            list.Add(b);
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
r += 0.8f;
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
                default:
                    // Debug so always fire ball
                    icicle_pos = position;
                    cast_Icicle = true; break;
            }
        }
    }
}
