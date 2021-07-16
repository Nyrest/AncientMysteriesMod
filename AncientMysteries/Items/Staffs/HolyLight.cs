namespace AncientMysteries.Items.Staffs
{
    [EditorGroup(g_staffs)]
    public class HolyLight : AMStaff
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public int r;

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "神圣之光",
            _ => "Holy Light",
        };

        public override string GetLocalizedDescription(AMLang lang) => lang switch
        {
            AMLang.schinese => "奇迹",
            _ => "The miracle.",
        };

        public HolyLight(float xval, float yval) : base(xval, yval)
        {
            _type = "gun";
            _spriteMap = this.ReadyToRunWithFrames(t_Staff_HolyLight, 15, 37);
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
            r = Rando.Int(3, 7);
            int count = _castTime >= 0.5 ? r : 1;
            for (int i = 0; i < count; i++)
            {
                var b = new HolyLight_ThingBullet(barrelPosition, GetVectorFromDegress(duck.FaceAngleDegressLeftOrRight() + Rando.Float(-20, 20), Rando.Float(5, 6)), duck);
                Level.Add(b);
            }
        }
    }
}
