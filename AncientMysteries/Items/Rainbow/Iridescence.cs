using AncientMysteries.Utilities;

namespace AncientMysteries.Items.MachineGuns
{
    [EditorGroup(g_rifles)]
    public sealed class Iridescence : AMGun
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Iridescence",
        };

        public Iridescence(float xval, float yval) : base(xval, yval)
        {
            this.ammo = 127;
            this._ammoType = new AT_Iridescence()
            {

            };
            this._type = "gun";
            this.ReadyToRunMap("rainbowGun.png");
            this._barrelOffsetTL = new Vec2(33f, 6f);
            BarrelSmoke.color = Color.White;
            _fireSound = "laserRifle";
            _fireWait = 0.6f;
            _fireSoundPitch = 0.9f;
            _kickForce = 0.25f;
            _fullAuto = true;
            loseAccuracy = 0.01f;
            maxAccuracyLost = 0.02f;
            _holdOffset = new Vec2(-2.5f, 0.2f);
        }

        public override void Update()
        {
            var color = HSL.FromHslFloat(Rando.Float(0f, 1f), Rando.Float(0.7f, 1f), Rando.Float(0.45f, 0.65f));
            ammoType.bulletColor = color;
            _flare.color = color;
            base.Update();
        }
    }
}
