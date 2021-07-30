namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_Overgrowth, 21, 34)]
    [MetaInfo(Lang.Default, "Overgrowth", "Something bad might happen if it releases its full power")]
    [MetaInfo(Lang.schinese, "过度生长", "如果让它过度释放自己的力量，也许不会有好事发生")]
    [MetaType(MetaType.Magic)]
    public partial class Overgrowth : AMStaff
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public StateBinding _timesBinding = new(nameof(times));

        public SpriteMap _spriteMap;

        public int times = 1;

        public const int timesMax = 11;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public Overgrowth(float xval, float yval) : base(xval, yval)
        {
            _spriteMap = this.ReadyToRunWithFrames(tex_Staff_Overgrowth, 21, 34);
            _spriteMap.AddAnimation("loop", 0.1f, true, 0, 1, 2, 3);
            _spriteMap.SetAnimation("loop");
            SetBox(21, 34);
            _barrelOffsetTL = new Vec2(6f, 5f);
#if DEBUG
            _castSpeed = 1f;
#else
            _castSpeed = 0.0045f;
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
            if (times == timesMax)
            {
                progressFillColor = Color.DarkGreen;
                progressBgColor = Color.Lime;
            }
            _castSpeed = 0.0045f + (0.0009f * times);
#if DEBUG
            _castSpeed = 1f;
#endif
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
            if (times == timesMax && _castTime >= 1f)
            {
                SFX.PlaySynchronized("scoreDing", 0.7f, 0.1f, 0, false);
                /*foreach (Duck d in Level.CheckCircleAll<Duck>(owner.position, 999))
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
                }*/
            }
            if (times < timesMax && _castTime >= 1f)
            {
                times++;
                SFX.PlaySynchronized("scoreDing", 0.5f, Convert.ToSingle(-0.3 + (times * 0.03f)), 0, false);
            }
        }

        public void ModifyParameter(ref float bulletSpeed, ref float range)
        {
            if (times < timesMax)
            {
                bulletSpeed += times * 0.5f;
                range += times * 20;
            }
            else
            {
                bulletSpeed += times * 0.6f;
                range += times * 25;
            }
        }
    }
}