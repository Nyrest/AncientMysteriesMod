namespace AncientMysteries.Items.Explosives.Grenades
{
    [EditorGroup(guns)]
    public partial class FuriousNewYear : AMGun
    {
        private static FieldInfo _firecrackerExplodeTimer = typeof(Firecracker).GetField("_explodeTimer", BindingFlags.Instance | BindingFlags.NonPublic);

        public override string GetLocalizedName(Lang lang) => lang switch
        {
            Lang.schinese => "新年狂欢",
            _ => "Furious New Year",
        };

        public override string GetLocalizedDescription(Lang lang) => lang switch
        {
            Lang.schinese => "不准放鞭炮！",
            _ => "No firecrackers!",
        };

        public StateBinding _timerBinding = new("_timer");

        public StateBinding _pinBinding = new("_pin");

        private SpriteMap _sprite;

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

        public bool _quacked;

        public FuriousNewYear(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 1;
            _type = "gun";
            _sprite = this.ReadyToRunWithFrames(t_Throwable_FuriousNewYear, 7, 15);
            graphic = _sprite;
            bouncy = 0.4f;
            friction = 0.05f;
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
            CreateExplosion(pos);
        }

        public void CreateExplosion(Vec2 pos)
        {
            if (!_explosionCreated)
            {
                /*
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
                    ExplosionPart ins = new ExplosionPart(cx + (float)(Math.Cos(Maths.DegToRad(dir)) * dist), cy - (float)(Math.Sin(Maths.DegToRad(dir)) * dist));
                    Level.Add(ins);
                }
                */
                _explosionCreated = true;
                SFX.Play("explode", 0.8f, -0.5f);
            }
        }

        public override void Update()
        {
            base.Update();
            if (!_pin)
            {
                _timer -= 0.014f;
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
                        const int bulletCount = 24;
                        float cx = x;
                        float cy = y - 2f;
                        if (isServerForObject)
                        {
                            for (int i = 0; i < bulletCount; i++)
                            {
                                float addSpeedX = hSpeed * 0.7f;
                                float addSpeedY = vSpeed * 0.7f;
                                Firecracker f = new(cx + Rando.Float(-1f, 1f), cy + Rando.Float(-1f, 1f));
                                _firecrackerExplodeTimer.SetValue(f, new ActionTimer(Rando.Float(0.018f, 0.024f)));
                                f.spinAngle = 90f;
                                f.hSpeed = Rando.Float(1.5f, 3f).RandomNegative() + addSpeedX;
                                f.vSpeed = addSpeedY > 0 ? 0 : addSpeedY + Rando.Float(1.5f, 3f).RandomNegative();
                                Level.Add(f);
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
                //SFX.Play("pullPin");
                SFX.PlaySynchronized("lightMatch", 0.8f, -0.6f + Rando.Float(0.2f));
            }
        }
    }
}
