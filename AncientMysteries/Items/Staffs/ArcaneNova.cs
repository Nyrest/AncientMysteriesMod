namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_ArcaneNova)]
    [MetaInfo(Lang.Default, "Arcane Nova", "A staff fulfilled with mysteries from the universe")]
    [MetaInfo(Lang.schinese, "奥术新星", "一把充满了宇宙奥秘的法杖")]
    [MetaType(MetaType.Magic)]
    public partial class ArcaneNova : AMStaff
    {
        public StateBinding _animationFrameBinding = new(nameof(AnimationFrame));

        public SpriteMap _spriteMap;

        public byte AnimationFrame
        {
            get => (byte)_spriteMap._frame;
            set => _spriteMap._frame = value;
        }

        public ArcaneNova(float xval, float yval) : base(xval, yval)
        {
            _type = "gun";
            _spriteMap = this.ReadyToRunWithFrames(tex_Staff_ArcaneNova, 14, 37);
            SetBox(14, 37);
            _barrelOffsetTL = new Vec2(6f, 5f);
            _castSpeed = 0.005f;
            _fireWait = 0.5f;
        }

        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            var firePos = barrelPosition;
            if (_castTime >= 1f)
            {
                var bullet = new ArcaneNova_Magic_Stage1(firePos, GetBulletVecDeg(owner.offDir == 1 ? 0 : 180, 7.5f), duck);
                Level.Add(bullet);
                SFX.PlaySynchronized("laserBlast", 5, -0.2f);
            }
        }
    }
}