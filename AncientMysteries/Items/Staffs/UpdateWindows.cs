using Microsoft.Xna.Framework.Graphics;

namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Misc)]
    [MetaImage(tex_Staff_ForceUpdate)]
    [MetaInfo(Lang.english, "Update Windows", "Windows 10 is updating.\nThis will take a while(?)")]
    [MetaInfo(Lang.schinese, "Windows 更新", "窗 10 正在更新，坐和放宽，你正在成功！\n如果新版本出现问题，请滚回到以前的版本。")]
    [MetaType(MetaType.Magic)]
    public sealed partial class UpdateWindows : AMHoldable
    {
        public StateBinding _targetPlayerBinding = new(nameof(_targetPlayer));
        public Duck _targetPlayer;

        public StateBinding _blindTimeBinding = new(nameof(_blindTime));
        public int _blindTime;

        public const int maxBlinkTime = 60 * 3; // 5sec

        public bool IsTargetVaild => _targetPlayer?.dead == false;

        public bool _quacked;

        public UpdateWindows(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRunWithFrames(tex_Staff_ForceUpdate);
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
                //_targetPlayer = null;
                _quacked = false;
            }
            if (IsTargetVaild && (_targetPlayer?.profile.localPlayer == true) && _blindTime > 0 && _blindTime > overlayDrawTime)
            {
                overlayDrawTime = _blindTime;
            }
        }

        public override void PressAction()
        {
            base.PressAction();
            if (IsTargetVaild && _blindTime == 0)
            {
                _blindTime = maxBlinkTime;
                SFX.PlayMod(snd_Sound_WinXPShutdown);
                this.visible = false;
                this.canPickUp = false;
                duck?.ThrowItem(false);
                //position = new Vec2(float.PositiveInfinity, float.PositiveInfinity);
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (IsTargetVaild && duck?.profile.localPlayer == true)
            {
                var start = topLeft + (graphic.center * graphic.scale);
                float fontWidth = BiosFont.GetWidth("@SHOOT@", false, duck.inputProfile);
                BiosFont.Draw("@SHOOT@", _targetPlayer.position + new Vec2(-fontWidth / 2, -20), Color.White, 1, duck.inputProfile);
                Graphics.DrawLine(start, _targetPlayer.position, Color.White, duck is null ? 0.6f : 1f, 1);
            }
        }

        static UpdateWindows()
        {
            Hooks.OnDraw += ForceUpdateDraw;
        }

        public static int overlayDrawTime;

        public static void ForceUpdateDraw()
        {
            if (overlayDrawTime > maxBlinkTime)
            {
                overlayDrawTime = 0;
            }
            if (overlayDrawTime > 0)
            {
                Graphics.caseSensitiveStringDrawing = false;
                Graphics.screen.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null);
                Graphics.Clear(Color.Blue);
                const string oops = ":(";
                float oopsWidth = Graphics.GetStringWidth(oops);
                Graphics.DrawString(oops, new Vec2(100, 100), Color.White, default, null, 8);
                const string text = "Oh Shit.\n\nYour Duck Game is updating...";
                float width = Graphics.GetStringWidth(text);
                Graphics.DrawString(text, new Vec2((Graphics.width / 2) - (width / 2 * 4), 250), Color.White, default, null, 4);
                float whiteSpaceX = 100;
                Graphics.DrawRect(new Rectangle(whiteSpaceX, 400, Graphics.width - (whiteSpaceX * 2), 40),
                    Color.DarkGray);
                Graphics.DrawRect(new Rectangle(whiteSpaceX, 400, (Graphics.width - (whiteSpaceX * 2)) * (1f - (overlayDrawTime / (float)maxBlinkTime)), 40), Color.White);
                Graphics.screen.End();
                overlayDrawTime--;
            }
        }
    }
}