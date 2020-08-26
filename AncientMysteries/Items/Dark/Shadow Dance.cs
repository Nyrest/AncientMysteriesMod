using AncientMysteries.AmmoTypes;
using AncientMysteries.Localization.Enums;
using DuckGame;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Snipers
{
    [EditorGroup(topAndSeries + "Dark")]
    public sealed class ShadowDance : AMGun
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "亡命舞步",
            _ => "Shadow Dance",
        };

        public ShadowDance(float xval, float yval) : base(xval, yval)
        {
            ammo = 999;
            this._ammoType = new AT_Shadow();
            this._ammoType.range = 2500;
            this._type = "gun";
            this.ReadyToRunMap("shadowDance.png");
            _flare.color = Color.Black;
            BarrelSmoke.color = Color.Black;
            this._barrelOffsetTL = new Vec2(34f, 6f);
            this._fireSound = "laserBlast";
            this._fireSoundPitch = -0.8f;
            this._kickForce = 0f;
            _holdOffset = new Vec2(3f, 0f);
        }

        public override void Update()
        {
            ammo = 999;
            base.Update();
        }
    }
}
