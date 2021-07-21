namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Artifacts)]
    [MetaImage(tex_Gun_TemperatureArt_Water)]
    [MetaInfo(Lang.english, "Temperature Art", "Isn't Water Vapor invisible?🤔")]
    [MetaInfo(Lang.schinese, "温度的艺术", "水蒸气难道不是看不见的吗？🤔")]
    [MetaType(MetaType.Gun)]
    public partial class TemperatureArt : AMGun
    {
        private bool _quacked;

        public static readonly Mode[] modes = new[] {
            Mode.Water,
            Mode.Ice,
            Mode.WaterVapor
        };

        public StateBinding _currentModeBinding = new(nameof(_currentMode));
        public byte _currentMode = 0;

        public Mode currentMode
        {
            get => (Mode)_currentMode;
            set => _currentMode = (byte)value;
        }

        public Mode lastMode;

        public AmmoType AmmoWater = new TemperatureArt_AmmoType_Water();
        public AmmoType AmmoIce = new TemperatureArt_AmmoType_Ice();
        public AmmoType AmmoWaterVapor = new TemperatureArt_AmmoType_WaterVapor();

        public TemperatureArt(float xval, float yval) : base(xval, yval)
        {
            ammo = byte.MaxValue;
            _holdOffset = new Vec2(-5, -3);
            _barrelOffsetTL = new Vec2(38, 21);
            this.loseAccuracy = 0;
            this.maxAccuracyLost = 0;
            UpdateMode();
        }

        public override void Update()
        {
            ammo = byte.MaxValue;
            if (duck != null)
            {
                if (_quacked != duck.IsQuacking() && (_quacked = duck.IsQuacking()))
                {
                    Helper.Switch(modes, ref _currentMode);
                    SFX.Play("swipe", 1f, 0.8f);
                }
            }

            if (lastMode != currentMode)
            {
                lastMode = currentMode;
                UpdateMode();
            }
            base.Update();
        }

        public void UpdateMode()
        {
            switch (currentMode)
            {
                case Mode.Water:
                    this.ReadyToRun(tex_Gun_TemperatureArt_Water);
                    _ammoType = AmmoWater;
                    break;

                case Mode.Ice:
                    this.ReadyToRun(tex_Gun_TemperatureArt_Ice);
                    _ammoType = AmmoIce;
                    break;

                case Mode.WaterVapor:
                    this.ReadyToRun(tex_Gun_TemperatureArt_WaterVapor);
                    _ammoType = AmmoWaterVapor;
                    break;

                default:
                    break;
            }
        }
        public enum Mode : byte
        {
            Water,
            Ice,
            WaterVapor
        }
    }
}