namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_HolyLight)]
    [MetaInfo(Lang.english, "Holy Light", "The miracle.")]
    [MetaInfo(Lang.schinese, "神圣之光", "奇迹")]
    public partial class HolyLight : AMStaff
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
            Lang.schinese => "神圣之光",
            _ => "Holy Light",
        };

        public override string GetLocalizedDescription(Lang lang) => lang switch
        {
            Lang.schinese => "奇迹",
            _ => "The miracle.",
        };

        public HolyLight(float xval, float yval) : base(xval, yval)
        {
            _type = "gun";
            _spriteMap = this.ReadyToRunWithFrames(tex_Staff_HolyLight, 15, 37);
            SetBox(15, 37);
            _barrelOffsetTL = new Vec2(6f, 5f);
            _castSpeed = 0.012f;
            BarrelSmokeFuckOff();
            _flare.color = Color.Transparent;
            castingParticlesEnabled = true;
            castingParticlesColor = Color.Yellow;

            _fireWait = 0.5f;
            _fireSoundPitch = 0.9f;
            _kickForce = 0.25f;
            _fullAuto = true;
        }

        public override void OnSpelling()
        {
            base.OnSpelling();
        }

        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            var r = Rando.Int(5, 7);
            int count = _castTime >= 0.5 ? r : 1;
            for (int i = 0; i < count; i++)
            {
                var b = new HolyLight_ThingBullet(barrelPosition, GetBulletVecDeg(duck.FaceAngleDegressLeftOrRight() + Rando.Float(-20, 20), Rando.Float(5, 6)), duck);
                Level.Add(b);
            }
            SFX.PlaySynchronized("scoreDing", _castTime >= 0.5f ? 1f : 0.5f, _castTime >= 0.5f ? -0.1f : -0.5f);
        }
    }
}