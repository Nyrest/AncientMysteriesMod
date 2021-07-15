namespace AncientMysteries.Items.Artifacts
{
    [EditorGroup(g_artifacts)]
    public sealed class SpearOfLeonidas : AMMelee
    {
        public static readonly Tex2D targetCircle = TexHelper.ModTex2D(t_Effect_TargetCircle);
        public static readonly int tcWidth = targetCircle.w, tcHeight = targetCircle.h;

        //public StateBinding _targetPlayerBinding = new StateBinding("_targetPlayer");
        public Duck _targetPlayer;

        public StateBinding _flyProgressBinding = new(nameof(_flying));
        public bool _flying = false;

        public bool IsTargetVaild => _targetPlayer?.dead == false && _targetPlayer?.ragdoll == null;

        public bool _quacked;

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Spear Of Leonidas",
        };

        public SpearOfLeonidas(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRunWithFrames(t_Melee_SpearOfLeonidas);
            _barrelOffsetTL = new Vec2(20f, 4f);
            _fireSound = "smg";
            physicsMaterial = PhysicsMaterial.Metal;
            _bouncy = 0.5f;
            _impactThreshold = 0.3f;
            weight = 0.9f;
        }

        public override void PressAction()
        {
            if (_targetPlayer != null)
            {
                duck.ThrowItem(true);
                _flying = true;
                canPickUp = false;
            }
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
                    (_quacked != duck.IsQuacking() && (_quacked = duck.IsQuacking()))
                    || _targetPlayer == null
                    || (_targetPlayer.position - duck.position).length > 250
                    )
                {
                    //SwitchTarget();
                    Helper.SwitchTargetCircle(ref _targetPlayer, duck, position, 250);
                }
            }
            // TODO: do this network onwer only, if null then just fucking stop flying and fall
            else if (_flying && _targetPlayer is not null)
            {
                // what a stupid implementation. I should let it just move like a normal object
                // but it can be teleported by teleporter. so just draw it no need modify it's real position.
                // however this guy is too lazy
                Vec2 anglevec = new(_targetPlayer.x - x, y - _targetPlayer.y);
                float angle = (float)Math.Atan(anglevec.y / anglevec.x);
                offDir = (sbyte)(anglevec.x < 0 ? -1 : 1);
                _angle = angle + 1.56f * offDir;
                //this.position += anglevec * 0.1f;
                hSpeed = Math.Min(anglevec.x, 5);
                vSpeed = Math.Min(anglevec.y, 5) * -1;
                canPickUp = false;
                if (_targetPlayer.dead)
                {
                    _flying = false;
                }
            }
            else
            {
                canPickUp = true;
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
                var start = duck.position;
                var end = _targetPlayer.position - new Vec2(0, 13);
                //Graphics.DrawLine(start, end, Color.White, 1f, 1);
                float fontWidth = BiosFont.GetWidth("@SHOOT@", false, duck.inputProfile);
                BiosFont.Draw("@SHOOT@", _targetPlayer.position + new Vec2(-fontWidth / 2, -20), Color.White, 1, duck.inputProfile);
                Graphics.DrawLine(start, _targetPlayer.position, Color.White, 1f, 1);
            }
        }
    }
}
