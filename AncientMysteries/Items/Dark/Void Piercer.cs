using AncientMysteries.AmmoTypes;
using AncientMysteries.Localization.Enums;
using DuckGame;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.MachineGuns
{
    [EditorGroup(g_machineGuns)]
    public sealed class VoidPiercer : AMGun
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "虚无渗透",
            _ => "Void Piercer",
        };

        public VoidPiercer(float xval, float yval) : base(xval, yval)
        {
            ammo = 999;
            this._ammoType = new AT_Shadow()
            {
                range = 360f,
                accuracy = 0.7f,
                penetration = 1f
            };
            this._type = "gun";
            this.ReadyToRunMap("VoidPiercer.png");
            this._barrelOffsetTL = new Vec2(20f, 4f);
            _flare.color = Color.Black;
            BarrelSmoke.color = Color.Black;
            this._fireSound = "laserRifle";
            this._fireWait = 0.6f;
            this._fireSoundPitch = -0.5f;
            this._kickForce = 0.25f;
            this._fullAuto = true;
            loseAccuracy = 0.01f;
            maxAccuracyLost = 0.02f;
            _holdOffset = new Vec2(-2.5f, 0.2f);
        }

        public override void Update()
        {
            ammo = 999;
            base.Update();
        }
    }
}
