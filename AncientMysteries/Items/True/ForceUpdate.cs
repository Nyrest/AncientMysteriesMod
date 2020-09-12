using AncientMysteries.Localization.Enums;
using AncientMysteries.Utilities;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.True
{
    [EditorGroup(topAndSeries + "True")]
    public sealed class ForceUpdate : AMHoldable
    {
        public StateBinding _targetPlayerBinding = new StateBinding(nameof(_targetPlayer));
        public Duck _targetPlayer;

        public StateBinding _blindTimeBinding = new StateBinding(nameof(_blindTime));
        public int _blindTime;

        public bool IsTargetVaild => _targetPlayer?.dead == false;

        public bool _quacked;

        public ForceUpdate(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRunMap("rainbowGun.png");
        }

        public override void Update()
        {
            base.Update();
            if (_blindTime > 0)
            {
                _blindTime--;
            }
            else _blindTime = 0;
            if (duck != null)
            {
                if (
                    (_quacked != duck.IsQuacking() && (_quacked = duck.IsQuacking())) ||
                    _targetPlayer == null
                    )
                {
                    Helper.SwitchTarget(ref _targetPlayer, duck);
                }
            }
            else
            {
                _targetPlayer = null;
                _quacked = false;
            }
        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "强制更新",
            _ => "Force Update",
        };

        public override void PressAction()
        {
            base.PressAction();
            if (IsTargetVaild)
                _blindTime = 60 * 8;
        }

        public override void Draw()
        {
            base.Draw();
            if (IsTargetVaild && (_targetPlayer?.profile.localPlayer == true) && _blindTime > 0)
            {
                Graphics.DrawRect(Level.current.camera.rectangle, Color.White, 0.999f, true);
            }
        }
    }
}
