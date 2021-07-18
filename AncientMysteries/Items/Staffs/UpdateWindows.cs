using Microsoft.Xna.Framework.Graphics;

namespace AncientMysteries.Items.Staffs
{
    [EditorGroup(g_misc)]
    [MetaImage(t_Staff_ForceUpdate)]
    [MetaInfo(Lang.english, "Update Windows", "Windows 10 is updating.\nThis will take a while(?)")]
    [MetaInfo(Lang.schinese, "更新", "窗 10 正在更新，坐和放宽，你正在成功！\n如果新版本出现问题，请滚回到以前的版本。")]
    public sealed partial class UpdateWindows : AMHoldable
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

        public override string GetLocalizedName(Lang lang) => lang switch
        {
            Lang.schinese => "更新",
            _ => "Update",
        };

        public override string GetLocalizedDescription(Lang lang) => lang switch
        {
            Lang.schinese => "窗 10 正在处理一些事情，坐和放宽，你正在成功！\n如果新版本出现问题，请尝试滚回到以前的版本。",
            _ => "Windows 10 is updating.\nThis will take a while(?)",
        };

        public bool _quacked;

        public UpdateWindows(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRunWithFrames(t_Staff_ForceUpdate);
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
                GTool.DrawTopProgressCenterTop(duck.position, _cd / totalCD, Color.White, Color.OrangeRed, Color.Black, 1, -13, 20, 7, depth);
            }
            if (IsTargetVaild && duck?.profile.localPlayer == true)
            {
                var start = topLeft + graphic.center * graphic.scale;
                float fontWidth = BiosFont.GetWidth("@SHOOT@", false, duck.inputProfile);
                BiosFont.Draw("@SHOOT@", _targetPlayer.position + new Vec2(-fontWidth / 2, -20), Color.White, 1, duck.inputProfile);
                Graphics.DrawLine(start, _targetPlayer.position, Color.White, duck is null ? 0.6f : 1f, 1);
            }
            if (IsTargetVaild && (_targetPlayer?.profile.localPlayer == true) && _blindTime > 0 && _blindTime > overlayDrawTime)
            {
                overlayDrawTime = _blindTime;
            }
        }

        static UpdateWindows()
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
                Graphics.DrawString(text, new Vec2(Graphics.width / 2 - width / 2 * 4, 250), Color.White, default, null, 4);
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
