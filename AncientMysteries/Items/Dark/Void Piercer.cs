namespace AncientMysteries.Items.MachineGuns
{
    [EditorGroup(g_machineGuns)]
    public sealed class VoidPiercer : AMGun
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Void Piercer",
        };

        public VoidPiercer(float xval, float yval) : base(xval, yval)
        {
            ammo = sbyte.MaxValue;
            _ammoType = new AT_Shadow()
            {
                range = 360f,
                accuracy = 0.7f,
                penetration = 1f
            };
            this.ReadyToRunMap(t_Gun_VoidPiercer);
            _barrelOffsetTL = new Vec2(20f, 4f);
            _flare.color = Color.Black;
            BarrelSmoke.color = Color.Black;
            _fireSound = "laserRifle";
            _fireWait = 0.6f;
            _fireSoundPitch = -0.5f;
            _kickForce = 0.25f;
            _fullAuto = true;
            loseAccuracy = 0.01f;
            maxAccuracyLost = 0.02f;
            _holdOffset = new Vec2(-2.5f, 0.2f);
        }

        public override void Update()
        {
            ammo = sbyte.MaxValue;
            base.Update();
        }
    }
}
