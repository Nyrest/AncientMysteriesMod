using AncientMysteries.Localization.Enums;
using AncientMysteries.Utilities;
using DuckGame;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Artifacts
{
    [EditorGroup(topAndSeries + "Isekai|Assassins Creed")]
    public sealed class SpearOfLeonidas : AMGun
    {
        public static readonly Tex2D targetCircle = TexHelper.ModTex2D("targetCircle.png");
        public static readonly int tcWidth = targetCircle.w, tcHeight = targetCircle.h;
        public static readonly BitmapFont _biosFont = new BitmapFont("biosFont", 8);

        //public StateBinding _targetPlayerBinding = new StateBinding("_targetPlayer");
        public Duck _targetPlayer;

        public bool IsTargetVaild => _targetPlayer?.dead == false;

        public bool _quacked;

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "列奥尼达斯之矛",
            _ => "Spear Of Leonidas",
        };

        public SpearOfLeonidas(float xval, float yval) : base(xval, yval)
        {
            this.ammo = 10;
            this._ammoType = new AT9mm();
            this._type = "gun";
            this.ReadyToRunMap("SpearOfLeonidas.png");
            this._barrelOffsetTL = new Vec2(20f, 4f);
            this._fireSound = "smg";
            physicsMaterial = PhysicsMaterial.Metal;
            _bouncy = 0.5f;
            _impactThreshold = 0.3f;
        }

        public override void PressAction()
        {
            base.PressAction();
            if (_targetPlayer != null)
                _targetPlayer.Kill(new DTCrush(_targetPlayer));
        }

        // TODO:
        public override void Update()
        {
            base.Update();
            if (duck != null)
            {
                handOffset = new Vec2(0, -2);
                _holdOffset = new Vec2(-8, -6);
                handAngle = 1.3f * offDir;
                if (
                    (_quacked != duck.IsQuacking() && (_quacked = duck.IsQuacking())) ||
                    _targetPlayer == null
                    )
                {
                    //SwitchTarget();
                    Helper.SwitchTarget(ref _targetPlayer, duck);
                }
            }
            else
            {
                _targetPlayer = null;
                _quacked = false;
            }
        }

        public void SwitchTarget()
        {
            Duck[] ducks = Level.current.things[typeof(Duck)]
            .Cast<Duck>()
            .OrderBy(x => x.persona is null ? 0 : Persona.Number(x.persona))
            .ToArray();
            int startIndex = Array.IndexOf(ducks, _targetPlayer) + 1;
            if (startIndex >= ducks.Length) startIndex = 0;
            for (; startIndex < ducks.Length; startIndex++)
            {
                var target = ducks[startIndex];
                if (!target.dead && target != duck)
                {
                    _targetPlayer = target;
                    break;
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (IsTargetVaild && duck?.profile.localPlayer == true)
            {
                var start = this.topLeft + graphic.center * graphic.scale;
                var end = _targetPlayer.position - new Vec2(0, 13);
                //Graphics.DrawLine(start, end, Color.White, 1f, 1);
                float fontWidth = _biosFont.GetWidth("@SHOOT@", false, duck.inputProfile);
                _biosFont.Draw("@SHOOT@", _targetPlayer.position + new Vec2(-fontWidth / 2, -20), Color.White, 1, duck.inputProfile);
                Graphics.Draw(targetCircle, _targetPlayer.position, null, Color.Orange, 0, new Vec2(tcWidth / 2, tcHeight / 2), new Vec2(0.5f), SpriteEffects.None, 1);
                Graphics.DrawLine(start, _targetPlayer.position, Color.White, 1f, 1);
            }
        }

        public void Shing()
        {
            SFX.Play("swordClash", Rando.Float(0.6f, 0.7f), Rando.Float(-0.1f, 0.1f), Rando.Float(-0.1f, 0.1f));
            Vec2 vec = (position - base.barrelPosition).normalized;
            Vec2 start = base.barrelPosition;
            for (int i = 0; i < 6; i++)
            {
                Level.Add(Spark.New(start.x, start.y, new Vec2(Rando.Float(-1f, 1f), Rando.Float(-1f, 1f))));
                start += vec * 4f;
            }
        }
    }
}
