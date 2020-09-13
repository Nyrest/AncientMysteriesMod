using AncientMysteries.Localization.Enums;
using AncientMysteries.Utilities;
using DuckGame;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.True
{
    [EditorGroup(topAndSeries + "True")]
    public sealed class ForceUpdateGame : AMHoldable
    {
        public StateBinding _targetPlayerBinding = new StateBinding(nameof(_targetPlayer));
        public Duck _targetPlayer;

        public StateBinding _blindTimeBinding = new StateBinding(nameof(_blindTime));
        public int _blindTime;

        public bool IsTargetVaild => _targetPlayer?.dead == false;

        public bool _quacked;

        public ForceUpdateGame(float xpos, float ypos) : base(xpos, ypos)
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
                //Graphics.DrawRect(Level.current.camera.rectangle, Color.White, 0.999f, true);
                doOverlayDraw = true;
            }
        }

        static ForceUpdateGame()
        {
            Hooks.OnDraw += ForceUpdateDraw;
        }
        public static bool doOverlayDraw;


        public static void ForceUpdateDraw()
        {
            if (doOverlayDraw)
            {
                Graphics.caseSensitiveStringDrawing = false;
                Graphics.screen.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null);
                Graphics.Clear(Color.Blue);
                const string text = "Your Duck Game is updating...";
                float width = Graphics.GetStringWidth(text);
                Graphics.DrawString(text, new Vec2(Graphics.width / 2 - (width / 2)*4, 250), Color.White, default, null, 4);
                float whiteSpaceX = 100;
                Graphics.DrawRect(new Rectangle(whiteSpaceX, 300, Graphics.width - whiteSpaceX * 2, 40),
                    Color.White);
                Graphics.screen.End();
                doOverlayDraw = false;
            }
        }
    }
}
