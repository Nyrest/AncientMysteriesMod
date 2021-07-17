namespace AncientMysteries.Items.Explosives.Grenades
{
    [EditorGroup(guns)]
    public sealed partial class TrackingGrenade : AMGun
    {
        public override string GetLocalizedName(Lang lang) => lang switch
        {
            Lang.schinese => "纳米手雷",
            _ => "Nano Grenade",
        };

        public override string GetLocalizedDescription(Lang lang) => lang switch
        {
            Lang.schinese => "一颗单身许久的饥渴手雷！",
            _ => "This grenade wants a girl friend!",
        };

        public StateBinding _timerBinding = new("_timer");

        public StateBinding _pinBinding = new("_pin");

        private readonly SpriteMap _sprite;

        public bool _pin = true;

        public float _timer = 1.2f;

        private Duck _cookThrower;

        private float _cookTimeOnThrow;

        public bool pullOnImpact;

        private bool _explosionCreated;

        private bool _localDidExplode;

        private bool _didBonus;

        private static int grenade;

        public int gr;

        public int _explodeFrames = -1;

        public Duck cookThrower => _cookThrower;

        public float cookTimeOnThrow => _cookTimeOnThrow;

        public Duck _targetPlayer;

        public bool IsTargetVaild => _targetPlayer?.dead == false && _targetPlayer?.ragdoll == null;

        public bool _quacked;

        public TrackingGrenade(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 1;
            _ammoType = new ATShrapnel
            {
                penetration = 0.4f
            };
            _type = "gun";
            _sprite = this.ReadyToRunWithFrames(t_Throwable_TrackingGrenade, 8, 9);
            graphic = _sprite;
            bouncy = 0.4f;
            friction = 0.05f;
            scale = new Vec2(1.15f);
        }

        public override void Initialize()
        {
            gr = grenade;
            grenade++;
        }

        public override void OnNetworkBulletsFired(Vec2 pos)
        {
            _pin = false;
            _localDidExplode = true;
            if (!_explosionCreated)
            {
                Graphics.FlashScreen();
            }
            CreateExplosion(pos);
        }

        public void CreateExplosion(Vec2 pos)
        {
            if (!_explosionCreated)
            {
                float cx = pos.x;
                float cy = pos.y - 2f;
                Level.Add(new ExplosionPart(cx, cy));
                int num = 6;
                if (Graphics.effectsLevel < 2)
                {
                    num = 3;
                }
                for (int i = 0; i < num; i++)
                {
                    float dir = i * 60f + Rando.Float(-10f, 10f);
                    float dist = Rando.Float(12f, 20f);
                    ExplosionPart ins = new(cx + (float)(Math.Cos(Maths.DegToRad(dir)) * dist), cy - (float)(Math.Sin(Maths.DegToRad(dir)) * dist));
                    Level.Add(ins);
                }
                _explosionCreated = true;
                SFX.Play("explode");
            }
        }

        public override void Update()
        {
            base.Update();
            if (duck != null)
            {
                //handOffset = new Vec2(0, -2);
                //_holdOffset = new Vec2(-8, -6);
                //handAngle = 1.3f * offDir;
                if (
                    (_quacked != duck.IsQuacking() && (_quacked = duck.IsQuacking())) ||
                    _targetPlayer == null
                    )
                {
                    //SwitchTarget();
                    Helper.SwitchTarget(ref _targetPlayer, duck);
                }
            }
            else if (_pin)
            {
                _targetPlayer = null;
                _quacked = false;
            }
            if (!_pin)
            {
                _timer -= 0.007f;
                if (IsTargetVaild)
                {
                    if (Level.CheckLine<Block>(position, _targetPlayer.position) != null)
                    {
                        StupidMoving.ThingMoveTo(this, _targetPlayer.position, 3f);
                    }
                    else
                    {
                        StupidMoving.ThingMoveToVertically(this, _targetPlayer.position, 3f);
                    }
                }
            }
            if (_timer < 0.5f && owner == null && !_didBonus)
            {
                _didBonus = true;
                if (Recorder.currentRecording != null)
                {
                    Recorder.currentRecording.LogBonus();
                }
            }
            if (!_localDidExplode && _timer < 0f)
            {
                if (_explodeFrames < 0)
                {
                    CreateExplosion(position);
                    _explodeFrames = 4;
                }
                else
                {
                    _explodeFrames--;
                    if (_explodeFrames == 0)
                    {
                        const int bulletCount = 25;
                        float cx = x;
                        float cy = y - 2f;
                        Graphics.FlashScreen();
                        if (isServerForObject)
                        {
                            var firedBullets = new List<Bullet>(bulletCount);
                            for (int i = 0; i < bulletCount; i++)
                            {
                                float dir = i * 18f - 5f + Rando.Float(10f);
                                ATShrapnel shrap = new()
                                {
                                    range = 70f + Rando.Float(20f)
                                };
                                Bullet bullet = new(cx + (float)(Math.Cos(Maths.DegToRad(dir)) * 6.0), cy - (float)(Math.Sin(Maths.DegToRad(dir)) * 6.0), shrap, dir)
                                {
                                    firedFrom = this
                                };
                                firedBullets.Add(bullet);
                                Level.Add(bullet);
                            }
                            IEnumerable<Window> windows = Level.CheckCircleAll<Window>(position, 50f);
                            foreach (Window w in windows)
                            {
                                if (Level.CheckLine<Block>(position, w.position, w) == null)
                                {
                                    w.Destroy(new DTImpact(this));
                                }
                            }
                            bulletFireIndex += bulletCount;
                            if (Network.isActive)
                            {
                                NMFireGun gunEvent = new(this, firedBullets, bulletFireIndex, rel: false, 4);
                                Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                                firedBullets.Clear();
                            }
                        }
                        Level.Remove(this);
                        _destroyed = true;
                        _explodeFrames = -1;
                    }
                }
            }
            if (prevOwner != null && _cookThrower == null)
            {
                _cookThrower = prevOwner as Duck;
                _cookTimeOnThrow = _timer;
            }
            _sprite.frame = (!_pin) ? 1 : 0;
        }

        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {
            if (pullOnImpact)
            {
                OnPressAction();
            }
            base.OnSolidImpact(with, from);
        }

        public override void OnPressAction()
        {
            if (_pin)
            {
                _pin = false;
                GrenadePin shell = new(x, y)
                {
                    hSpeed = -offDir * (1.5f + Rando.Float(0.5f)),
                    vSpeed = -2f
                };
                Level.Add(shell);
                SFX.Play("pullPin");
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (IsTargetVaild && duck?.profile.localPlayer == true)
            {
                var start = topLeft + graphic.center * graphic.scale;
                float fontWidth;
                fontWidth = BiosFont.GetWidth("@QUACK@", false, duck.inputProfile);
                BiosFont.Draw("@QUACK@", duck.position + new Vec2(-fontWidth / 2, -20), Color.White, 1, duck.inputProfile);
                if (_pin)
                {
                    fontWidth = BiosFont.GetWidth("@SHOOT@", false, duck.inputProfile);
                    BiosFont.Draw("@SHOOT@", _targetPlayer.position + new Vec2(-fontWidth / 2, -20), Color.White, 1, duck.inputProfile);
                }
                else
                {
                    fontWidth = BiosFont.GetWidth("@GRAB@", false, duck.inputProfile);
                    BiosFont.Draw("@GRAB@", _targetPlayer.position + new Vec2(-fontWidth / 2, -20), Color.White, 1, duck.inputProfile);
                }
                Graphics.DrawLine(start, _targetPlayer.position, Color.White, duck is null ? 0.6f : 1f, 1);
            }
        }
    }
}
