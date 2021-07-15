using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items.Artifact
{
    [EditorGroup(g_artifacts)]
    public class TemperatureArt : AMGun
    {
        private bool _quacked;

        public static readonly Mode[] modes = new[] {
            Mode.Water,
            Mode.Ice,
            Mode.WaterVapor
        };

        public StateBinding currentModeBinding = new StateBinding(nameof(currentMode));
        public Mode currentMode = Mode.Water;
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
                    Helper.Switch(modes, ref Unsafe.As<Mode, byte>(ref currentMode));
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
                    this.ReadyToRun(t_Gun_TemperatureArt_Water);
                    _ammoType = AmmoWater;
                    break;
                case Mode.Ice:
                    this.ReadyToRun(t_Gun_TemperatureArt_Ice);
                    _ammoType = AmmoIce;
                    break;
                case Mode.WaterVapor:
                    this.ReadyToRun(t_Gun_TemperatureArt_WaterVapor);
                    _ammoType = AmmoWaterVapor;
                    break;
                default:
                    break;
            }
        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Art of Temperature",
        };

        public enum Mode : byte
        {
            Water,
            Ice,
            WaterVapor
        }
    }
}
