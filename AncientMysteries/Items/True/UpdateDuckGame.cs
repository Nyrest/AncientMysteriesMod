using Microsoft.Xna.Framework.Graphics;
using static AncientMysteries.AMFonts;

namespace AncientMysteries.Items.True
{
    [EditorGroup(g_misc)]
    public sealed class UpdateDuckGame : AMHoldable
    {
        public StateBinding _targetPlayerBinding = new(nameof(_targetPlayer));
        public Duck _targetPlayer;

        public StateBinding _blindTimeBinding = new(nameof(_blindTime));
        public int _blindTime;

        public StateBinding _cdBinding = new(nameof(_cd));
        public int _cd;

        public const int totalBlinkTime = 60 * 5; // 5sec

        public const int totalCD = totalBlinkTime * 2;

        public bool IsTargetVaild => _targetPlayer?.dead == false && _targetPlayer?.ragdoll == null;

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Force Update",
        };

        public bool _quacked;

        public UpdateDuckGame(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRunMap("forceUpdate.png");
        }

        public override void Update()
        {
            base.Update();
            if (_blindTime > 0)
            {
                _blindTime--;
            }
            else _blindTime = 0;
            if (_cd > 0)
            {
                _cd--;
            }
            else _cd = 0;
            if (duck != null)
            {
                if (
                    ((_quacked != duck.IsQuacking() && (_quacked = duck.IsQuacking()))
                    || _targetPlayer == null)
                    && _blindTime == 0
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

        public override void PressAction()
        {
            base.PressAction();
            if (IsTargetVaild)
            {
                _blindTime = totalBlinkTime;
                _cd = totalCD;
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (_cd != 0 && duck != null)
            {
                GTool.DrawTopProgressCenterTop(duck.position, _cd / totalCD, Color.White, Color.OrangeRed, Color.Black, 1, -13, 20, 7, this.depth);
            }
            if (IsTargetVaild && duck?.profile.localPlayer == true)
            {
                var start = this.topLeft + graphic.center * graphic.scale;
                float fontWidth = _biosFont.GetWidth("@SHOOT@", false, duck.inputProfile);
                _biosFont.Draw("@SHOOT@", _targetPlayer.position + new Vec2(-fontWidth / 2, -20), Color.White, 1, duck.inputProfile);
                Graphics.DrawLine(start, _targetPlayer.position, Color.White, duck is null ? 0.6f : 1f, 1);
            }
            if (IsTargetVaild && (_targetPlayer?.profile.localPlayer == true) && _blindTime > 0 && _blindTime > overlayDrawTime)
            {
                overlayDrawTime = _blindTime;
            }
        }

        static UpdateDuckGame()
        {
            Hooks.OnDraw += ForceUpdateDraw;
        }
        public static int overlayDrawTime;

        public static void ForceUpdateDraw()
        {
            if (overlayDrawTime > totalBlinkTime)
            {
                overlayDrawTime = 0;
            }
            if (overlayDrawTime > 0)
            {
                Graphics.caseSensitiveStringDrawing = false;
                Graphics.screen.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null);
                Graphics.Clear(Color.Blue);
                const string text = "Your Duck Game is updating...";
                float width = Graphics.GetStringWidth(text);
                Graphics.DrawString(text, new Vec2(Graphics.width / 2 - (width / 2) * 4, 250), Color.White, default, null, 4);
                float whiteSpaceX = 100;
                Graphics.DrawRect(new Rectangle(whiteSpaceX, 300, Graphics.width - whiteSpaceX * 2, 40),
                    Color.DarkGray);
                Graphics.DrawRect(new Rectangle(whiteSpaceX, 300, (Graphics.width - whiteSpaceX * 2) * (1f - (overlayDrawTime / (float)totalBlinkTime)), 40), Color.White);
                Graphics.screen.End();
                overlayDrawTime--;
            }
        }
    }
}
