namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Artifacts)]
    [MetaImage(tex_Gun_TemperatureArt_Water)]
    [MetaInfo(Lang.Default, "Temperature Art", "Isn't Water Vapor invisible?🤔")]
    [MetaInfo(Lang.schinese, "温度的艺术", "水蒸气难道不是看不见的吗？🤔")]
    [MetaType(MetaType.Gun)]
    public partial class TemperatureArt : AMGun
    {
        private bool _quacked;

        public TemperatureArt_AmmoType_Base b;

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

        /*public AmmoType AmmoWater = new TemperatureArt_AmmoType_Water();
        public AmmoType AmmoIce = new TemperatureArt_AmmoType_Ice();
        public AmmoType AmmoWaterVapor = new TemperatureArt_AmmoType_WaterVapor();*/

        public TemperatureArt(float xval, float yval) : base(xval, yval)
        {
            ammo = byte.MaxValue;
            _holdOffset = new Vec2(-5, -3);
            _barrelOffsetTL = new Vec2(38, 21);
            loseAccuracy = 0;
            maxAccuracyLost = 0;
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
                    _fireWait = 2f;
                    break;

                case Mode.Ice:
                    this.ReadyToRun(tex_Gun_TemperatureArt_Ice);
                    _fireWait = 1.2f;
                    //b = new TemperatureArt_AmmoType_Ice();
                    break;

                case Mode.WaterVapor:
                    this.ReadyToRun(tex_Gun_TemperatureArt_WaterVapor);
                    _fireWait = 20.7f;
                    //b = new TemperatureArt_AmmoType_WaterVapor();
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

        public override void Fire()
        {
            //base.Fire();
            switch (currentMode)
            {
                case Mode.Water:
                    for (int i = 0; i < 12; i++)
                    {
                        b = new TemperatureArt_AmmoType_Water(barrelPosition, barrelVector.Rotate(Rando.Float((float)(-1.56 / 6), (float)(1.56 / 6)), Vec2.Zero) * 10, duck);
                        Level.Add(b);
                    }
                    break;

                case Mode.Ice:
                    //b = new TemperatureArt_AmmoType_Ice();
                    b = new TemperatureArt_AmmoType_Ice(barrelPosition, barrelVector.Rotate(Rando.Float((float)(-1.56 / 75), (float)(1.56 / 75)), Vec2.Zero) * 20, duck);
                    Level.Add(b);
                    break;

                case Mode.WaterVapor:
                    //b = new TemperatureArt_AmmoType_WaterVapor();
                    for (int i = 0; i < 5; i++)
                    {
                        b = new TemperatureArt_AmmoType_WaterVapor(barrelPosition, barrelVector.Rotate(Rando.Float((float)(-1.56 / 8), (float)(1.56 / 8)), Vec2.Zero) * 5, duck);
                        Level.Add(b);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}