namespace AncientMysteries.Items{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_Judgement, 13, 39)]
    [MetaInfo(Lang.english, "Rainbow Tears", "「天気なんて、狂ったままでいいんだ！」")]
    [MetaInfo(Lang.schinese, "棱彩之泪", "「天気なんて、狂ったままでいいんだ！」")]
    public partial class RainbowTears : AMStaff
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }


        public override string GetLocalizedName(Lang lang) => lang switch
        {
            Lang.schinese => "棱彩之泪",
            _ => "Chromatic Tears",
        };

        public override string GetLocalizedDescription(Lang lang) => lang switch
        {
            _ => "「天気なんて、狂ったままでいいんだ！」",
        };

        public RainbowTears(float xval, float yval) : base(xval, yval)
        {
            _ammoType = new AT_RainbowEyedrops();
            _type = "gun";
            _spriteMap = this.ReadyToRunWithFrames(tex_Staff_Judgement, 13, 39);
            SetBox(13, 39);
            _barrelOffsetTL = new Vec2(6f, 5f);
            _castSpeed = 0.01f;
            BarrelSmokeFuckOff();
            _flare.color = Color.Transparent;
            _fireSound = "flameExplode";
            _fireWait = 0.5f;
            _fireSoundPitch = 0.9f;
            _kickForce = 0.25f;
            _fullAuto = true;
            _spriteMap.AddAnimation("loop", 0.04f, true, 0, 1, 2, 1);
        }

        public override void OnSpelling()
        {
            base.OnSpelling();
        }

        public override void Update()
        {
            base.Update();
            Graphics.material = null;
            if (_castTime > 0.3f)
            {
                _castSpeed = 0.005f;
            }
            else _castSpeed = 0.01f;
            if (IsSpelling)
            {
                if (_castTime > 0.3f)
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
