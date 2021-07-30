namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Snipers)]
    [MetaImage(tex_Gun_ShadowDance)]
    [MetaInfo(Lang.Default, "Shadow Dance", "Aim, fire!")]
    [MetaInfo(Lang.schinese, "影舞者", "瞄准，开火！")]
    [MetaType(MetaType.Gun)]
    public sealed partial class ShadowDance : AMGun
    {
        public ShadowDance(float xval, float yval) : base(xval, yval)
        {
            ammo = sbyte.MaxValue;
            _ammoType = new AT_Shadow()
            {
                range = 1600,
                penetration = 3.5f,
            };
            this.ReadyToRunWithFrames(tex_Gun_ShadowDance);
            _flare.color = Color.Black;
            BarrelSmoke.color = Color.Black;
            _fireRumble = RumbleIntensity.Medium;
            _barrelOffsetTL = new Vec2(34f, 6f);
            _fireSound = "laserBlast";
            _fireSoundPitch = -0.8f;
            _kickForce = 0f;
            _holdOffset = new Vec2(3f, 0f);
        }

        public override void Update()
        {
            ammo = sbyte.MaxValue;
            base.Update();
        }
    }
}