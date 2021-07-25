namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_Judgement, 17, 49)]
    [MetaInfo(Lang.english, "Rainbow Tears", "「天気なんて、狂ったままでいいんだ！」")]
    [MetaInfo(Lang.schinese, "棱彩之泪", "「天気なんて、狂ったままでいいんだ！」")]
    [MetaType(MetaType.Magic)]
    public partial class RainbowTears : AMStaff
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public Waiter waiter = new(2);
        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public RainbowTears(float xval, float yval) : base(xval, yval)
        {
            _ammoType = new AT_RainbowEyedrops();
            _type = "gun";
            _spriteMap = this.ReadyToRunWithFrames(tex_Staff_Judgement, 17, 49);
            SetBox(17, 47);
            _barrelOffsetTL = new Vec2(6f, 5f);
            _castSpeed = 0.01f;
            BarrelSmokeFuckOff();
            _flare.color = Color.Transparent;
            _fireSound = "flameExplode";
            _fireWait = 0.5f;
            _fireSoundPitch = 0.9f;
            _holdOffset = new Vec2(-5, -10);
            _kickForce = 0.25f;
            _fullAuto = true;
            _spriteMap.AddAnimation("loop", 0.04f, true, 0, 1, 2, 1);
            _castSpeed = 0.01f;
        }

        public override void OnSpelling()
        {
            base.OnSpelling();
        }

        public override void Update()
        {
            base.Update();
            Graphics.material = null;
            if (IsSpelling)
            {
                if (_castTime > 0.5f && waiter.Tick())
                {
                    Vec2 barrelPos = barrelPosition;
                    int count = Rando.Int(1, 2);
                    for (int i = 0; i < count; i++)
                    {
                        Bullet bullet = new(
                            barrelPos.x + Rando.Float(-3, 3),
                            barrelPos.y + Rando.Float(-3, 3), ammoType, 90 + Rando.Float(-10, 10), duck)
                        {
                            color = HSL.FromHslFloat(Rando.Float(0f, 1f), Rando.Float(0.7f, 1f), Rando.Float(0.45f, 0.65f)),
                            firedFrom = this,
                            range = 2000
                        };
                        firedBullets.Add(bullet);
                        Level.Add(bullet);
                    }
                    bulletFireIndex += (byte)count;
                    if (Network.isActive)
                    {
                        NMFireGun gunEvent = new(this, firedBullets, bulletFireIndex, false, 4);
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