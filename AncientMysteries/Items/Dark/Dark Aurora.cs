using AncientMysteries.AmmoTypes;
using AncientMysteries.Localization.Enums;
using DuckGame;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Shotguns
{
    [EditorGroup(topAndSeries + "Dark")]
    public sealed class DarkAurora : AMGun
    {
        private float _loadProgress = 1f;

        public float _loadWait;

        public bool _first = true;

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "夜影极光",
            _ => "Dark Aurora",
        };

        public DarkAurora(float xval, float yval) : base(xval, yval)
        {
            ammo = 999;
            this._ammoType = new AT_Shadow();
            this._type = "gun";
            this.ReadyToRunMap("darkAurora.png");
            _flare.color = Color.Black;
            BarrelSmoke.color = Color.Black;
            this._barrelOffsetTL = new Vec2(28f, 4f);
            this._fireSound = "laserBlast";
            this._fireSoundPitch = -0.9f;
            this._kickForce = 2f;
            this._numBulletsPerFire = 8;
            this._manualLoad = true;
            _holdOffset = new Vec2(2.5f, 0f);
        }

        public override void Update()
        {
            ammo = 999;
            base.Update();
            if (_first)
            {
                _loadProgress = 1f;
                _loadWait = 0f;
            }
            if (!(_loadWait > 0f))
            {
                if (_loadProgress == 0f)
                {
                    SFX.Play("shotgunLoad", 0.7f, -0.8f);
                }
                if (_loadProgress == 0.5f)
                {
                    Reload();
                }
                _loadWait = 0f;
                if (_loadProgress < 1f)
                {
                    _loadProgress += 0.1f;
                    return;
                }
                _loadProgress = 1f;
                _first = false;
            }
        }

        public override void OnPressAction()
        {
            if (_loadProgress >= 1f)
            {
                base.OnPressAction();
                _loadProgress = 0f;
                _loadWait = 1f;
            }
            else if (_loadWait == 1f)
            {
                _loadWait = 0f;
            }
        }
    }
}
