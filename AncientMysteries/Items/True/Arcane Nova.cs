namespace AncientMysteries.Items.True
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
            _ => "Arcane Nova",
        };

        public ArcaneNova(float xval, float yval) : base(xval, yval)
        {
            _type = "gun";
            _spriteMap = this.ReadyToRunMap(t_ArcaneNova, 14, 37);
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

        public override void OnSpelling()
        {
            base.OnSpelling();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void OnReleaseSpell()
        {
            base.OnReleaseSpell();
            var firePos = barrelPosition;
            SFX.PlaySynchronized("laserBlast",5,-0.2f);
            if (_castTime >= 1f)
            {
                this.NmFireGun(list =>
                {
                    Bullet b = new Bullet_AN(firePos.x, firePos.y, new AT_AN(), owner.offDir == 1 ? 0 : 180, owner);
                    list.Add(b);
                });
            }
        }
    }
}
