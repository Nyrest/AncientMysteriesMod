namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_HolyLight,15,38,0)]
    [MetaInfo(Lang.Default, "Holy Light", "The miracle.")]
    [MetaInfo(Lang.schinese, "神圣之光", "奇迹。")]
    [MetaType(MetaType.Magic)]
    public partial class HolyLight : AMStaff
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public HolyLight(float xval, float yval) : base(xval, yval)
        {
            _type = "gun";
            _spriteMap = this.ReadyToRunWithFrames(tex_Staff_HolyLight, 15, 38);
            _spriteMap.AddAnimation("anim", 0.15f, true,
                0,
                1,
                2,
                3,
                4,
                5);
            _spriteMap.SetAnimation("anim");
            SetBox(15, 38);
            _barrelOffsetTL = new Vec2(6f, 5f);
            _castSpeed = 0.01f;
            BarrelSmokeFuckOff();
            _flare.color = Color.Transparent;
            castingParticlesEnabled = true;
            castingParticlesColor = Color.Yellow;
        }

        public override void OnSpelling()
        {
            base.OnSpelling();
        }

        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            var r = Rando.Int(2, 3);
            int count = _castTime >= 0.5 ? r : 1;
            float angleVariation;
            float bulletSpeed;
            for (int i = 0; i < count; i++)
            {
                angleVariation = _castTime >= 0.5 ? Rando.Float(-20, 20) : Rando.Float(-25, 25);
                bulletSpeed = _castTime >= 0.5 ? Rando.Float(4, 5) : Rando.Float(5, 6);
                var b = new HolyLight_ThingBullet(barrelPosition, GetBulletVecDeg(duck.FaceAngleDegressLeftOrRight() + angleVariation, bulletSpeed), duck);
                Level.Add(b);
            }
            SFX.PlaySynchronized("scoreDing", _castTime >= 0.5f ? 1f : 0.5f, _castTime >= 0.5f ? -0.1f : -0.5f);
        }
    }
}