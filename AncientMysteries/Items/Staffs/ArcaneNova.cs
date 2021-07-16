namespace AncientMysteries.Items.Staffs
{
    [EditorGroup(g_staffs)]
    public class ArcaneNova : AMStaff
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }


        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "奥术新星",
            _ => "Arcane Nova",
        };
        public override string GetLocalizedDescription(AMLang lang) => lang switch
        {
            AMLang.schinese => "一把充满了宇宙奥秘的法杖",
            _ => "A staff fulfilled with mysteries from the universe",
        };

        public ArcaneNova(float xval, float yval) : base(xval, yval)
        {
            _type = "gun";
            _spriteMap = this.ReadyToRunWithFrames(t_Staff_ArcaneNova, 14, 37);
            SetBox(14, 37);
            _barrelOffsetTL = new Vec2(6f, 5f);
            _castSpeed = 0.007f;
            BarrelSmokeFuckOff();
            _flare.color = Color.Transparent;
            _fireWait = 0.5f;
            _fireSoundPitch = 0.9f;
            _kickForce = 0.25f;
            _fullAuto = true;
        }

        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            var firePos = barrelPosition;
            if (_castTime >= 1f)
            {
                var bullet = new ArcaneNova_Magic_Stage1(firePos, GetVectorFromDegress(owner.offDir == 1 ? 0 : 180, 2.5f), duck);
                Level.Add(bullet);
                SFX.PlaySynchronized("laserBlast", 5, -0.2f);
            }
        }
    }
}
