namespace AncientMysteries.Items.Artifacts
{
    [EditorGroup(g_artifacts)]
    public sealed class FallGuy : AMHoldable
    {
        public static readonly Tex2D mark = TexHelper.ModTex2D(t_Effect_FallGuyMark);
        public static readonly int markWidth = mark.w, markHeight = mark.h;

        //public StateBinding _targetPlayerBinding = new StateBinding("_targetPlayer");

        public StateBinding _cdTimeBinding = new("_cdTime");

        public Duck _targetPlayer;

        public float _cdTime;

        private bool _quacked;

        public bool IsTargetVaild => _targetPlayer?.dead == false && _targetPlayer?.ragdoll == null;

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "时空扭曲",
            _ => "Fall Guy",
        };

        public override string GetLocalizedDescription(AMLang lang) => lang switch
        {
            AMLang.schinese => "让你的好朋友替你受罪！",
            _ => "Let someone fall instead of you!",
        };

        public FallGuy(float xpos, float ypos) : base(xpos, ypos)
        {
            _type = "gun";
            this.ReadyToRunWithFrames(t_Staff_FallGuy);
            physicsMaterial = PhysicsMaterial.Metal;
            material = new MaterialGlitch(this);
        }

        public override void PressAction()
        {
            if (_targetPlayer != null && _cdTime == 0) // Do Replace Localtion
            {
                if (Network.isActive && isServerForObject)
                {
                    SuperFondle(duck, DuckNetwork.localConnection);
                    SuperFondle(_targetPlayer, DuckNetwork.localConnection);
                }
                var targetPos = _targetPlayer.position;
                _targetPlayer.position = duck.position;
                duck.position = targetPos;
                if (_targetPlayer._vSpeed == 0)
                    _targetPlayer._vSpeed = 0.1f;
                if (duck._vSpeed == 0)
                    duck._vSpeed = 0.1f;
                _cdTime = 1;
                SFX.Play("swipe", 0.9f, -0.8f);
                for (int i = 0; i < 8; i++)
                {
                    const float flyMax = 3f;
                    MusketSmoke smoke = new(duck.x + Rando.Float(-5, 5), duck.y + Rando.Float(-10, 3))
                    {
                        alpha = 0.9f,
                        depth = 0.9f + i * 0.001f,
                        fly = new Vec2(Rando.Float(-flyMax, flyMax), Rando.Float(-flyMax, flyMax))
                    };
                    if (i % 4 != 0)
                        smoke.move.x -= Rando.Float(-0.1f, 0.1f);
                    MusketSmoke smoke2 = new(_targetPlayer.x + Rando.Float(-5, 5), _targetPlayer.y + Rando.Float(-10, 3))
                    {
                        alpha = 0.9f,
                        depth = 0.9f + i * 0.001f,
                        fly = new Vec2(Rando.Float(-flyMax, flyMax), Rando.Float(-flyMax, flyMax))
                    };
                    if (i % 4 != 0)
                        smoke2.move.x -= Rando.Float(-0.1f, 0.1f);
                    Level.Add(smoke);
                    Level.Add(smoke2);
                }
            }
            base.PressAction();
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

        public override void Update()
        {
            base.Update();
            if (duck != null)
            {
                if (
                    (_quacked != duck.IsQuacking() && (_quacked = duck.IsQuacking())) ||
                    _targetPlayer == null
                    )
                {
                    Helper.SwitchTarget(ref _targetPlayer, duck);
                }
                if (_cdTime > 0)
                {
                    _cdTime -= 0.005f;
                }
                else
                {
                    _cdTime = 0;
                }
            }
            else
            {
                _targetPlayer = null;
                _cdTime = 0;
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (IsTargetVaild && duck?.profile.localPlayer == true)
            {
                var start = topLeft + graphic.center * graphic.scale;
                var end = _targetPlayer.position - new Vec2(0, 13);
                var cache = material;
                Graphics.material = null;
                if (_cdTime == 0)
                {
                    //Graphics.DrawLine(start, end, Color.DarkOrange, 1.5f, 1);
                }
                Graphics.DrawLine(start, end, new Color((byte)(255 * (1 - _cdTime)), (byte)25, (byte)34, (byte)(255 * (1 - _cdTime))), 1f, 1);
                Graphics.material = cache;
                Graphics.Draw(mark, end.x - markWidth / 2, end.y - markHeight / 2, 0.9f);
                _targetPlayer.DrawTopText("@SHOOT@", Color.White, -9, duck.inputProfile);
            }
        }
    }
}
