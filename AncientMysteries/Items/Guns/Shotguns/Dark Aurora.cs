﻿namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Shotguns)]
    [MetaImage(tex_Gun_DarkAurora)]
    [MetaInfo(Lang.Default, "Dark Aurora", "Pour down shadowy light to crush your foes.")]
    [MetaInfo(Lang.schinese, "暗影极光", "召唤阴影之光来击碎你的敌人。")]
    [MetaType(MetaType.Gun)]
    public sealed partial class DarkAurora : AMGun
    {
        private float _loadProgress = 1f;

        public float _loadWait;

        public bool _first = true;

        public DarkAurora(float xval, float yval) : base(xval, yval)
        {
            ammo = 10;
            _ammoType = new AT_Shadow()
            {
                range = 400f,
            };
            this.ReadyToRunWithFrames(tex_Gun_DarkAurora);
            _flare.color = Color.Black;
            BarrelSmoke.color = Color.Black;
            _barrelOffsetTL = new Vec2(28f, 4f);
            _fireRumble = RumbleIntensity.Medium;
            _fireSound = "laserBlast";
            _fireSoundPitch = -0.9f;
            _kickForce = 2f;
            _numBulletsPerFire = 8;
            _manualLoad = true;
            _holdOffset = new Vec2(2.5f, 0f);
        }

        public override void Update()
        {
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