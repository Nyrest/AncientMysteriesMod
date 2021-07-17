namespace AncientMysteries.Items.Staffs
{
    [EditorGroup(g_staffs)]
    public partial class Overgrowth : AMStaff
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public StateBinding _timesBinding = new(nameof(times));

        public SpriteMap _spriteMap;

        public int times = 1;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public override string GetLocalizedName(Lang lang) => lang switch
        {
            Lang.schinese => "过度生长",
            _ => "Overgrowth",
        };
        public override string GetLocalizedDescription(Lang lang) => lang switch
        {
            Lang.schinese => "如果让它过度释放自己的力量，也许不会有好事发生",
            _ => "Things won't go easier if it releases its full power",
        };
        public Overgrowth(float xval, float yval) : base(xval, yval)
        {
            _spriteMap = this.ReadyToRunWithFrames(t_Staff_Overgrowth, 21, 34);
            _spriteMap.AddAnimation("loop", 0.1f, true, 0, 1, 2, 3);
            _spriteMap.SetAnimation("loop");
            SetBox(21, 34);
            _barrelOffsetTL = new Vec2(6f, 5f);
#if DEBUG
            _castSpeed = 1;
#else
            _castSpeed = 0.0035f;
#endif
            BarrelSmokeFuckOff();
            _flare.color = Color.Transparent;
            _fireWait = 0.5f;
            _fireSoundPitch = 0.9f;
            _kickForce = 0.25f;
            _fullAuto = true;
        }

        public override void Update()
        {
            base.Update();
            _castSpeed = 0.0045f + 0.0008f * times;
        }

        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            var firePos = barrelPosition;
            if (_castTime >= 1f)
            {
                this.NmFireGun(list =>
                {
                    for (int i = 0; i < (float)(times / 2); i++)
                    {
                        list.Add(Make.Bullet<Overgrowth_AmmoType_Big>(firePos, ModifyParameter, owner, this.FaceAngleDegressLeftOrRight(), this));
                    }
                });

                this.NmFireGun(list =>
                {
                    for (int i = 0; i < times * 2; i++)
                    {
                        list.Add(Make.Bullet<Overgrowth_AmmoType_Small>(firePos, ModifyParameter, owner, this.FaceAngleDegressLeftOrRight(), this));
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
                            list.Add(Make.Bullet<Overgrowth_AmmoType_FinalKiller>(d.x - 40, d.y - 40, ModifyParameter, owner, -Maths.PointDirection(d.x - 40, d.y - 40, d.x, d.y), this));                                                         
                            list.Add(Make.Bullet<Overgrowth_AmmoType_FinalKiller>(d.x + 40, d.y - 40, ModifyParameter, owner, -Maths.PointDirection(d.x + 40, d.y - 40, d.x, d.y), this));                                                         
                            list.Add(Make.Bullet<Overgrowth_AmmoType_FinalKiller>(d.x - 40, d.y + 40, ModifyParameter, owner, -Maths.PointDirection(d.x - 40, d.y + 40, d.x, d.y), this));
                            list.Add(Make.Bullet<Overgrowth_AmmoType_FinalKiller>(d.x + 40, d.y + 40, ModifyParameter, owner, -Maths.PointDirection(d.x + 40, d.y + 40, d.x, d.y), this));
                        });
                    }
                }
            }
            if (times < 10 && _castTime >= 1f)
            {
                times++;
                SFX.PlaySynchronized("scoreDing", 0.5f, Convert.ToSingle(-0.3 + times * 0.03f), 0, false);
            }
        }

        public void ModifyParameter(ref float bulletSpeed, ref float range)
        {
            bulletSpeed += times * 0.5f;
            range += times * 20;
        }
    }
}
