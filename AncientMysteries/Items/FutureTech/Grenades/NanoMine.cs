using System.Collections.Generic;
using static AncientMysteries.AMFonts;

namespace AncientMysteries.Items.FutureTech.Grenades
{
    [EditorGroup(guns)]
    public sealed class NanoMine : AMGun
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Nano Mine",
        };

        public StateBinding _pinBinding = new("_pin");

        public StateBinding _armedBinding = new("_armed");

        public StateBinding _clickedBinding = new("_clicked");

        public StateBinding _thrownBinding = new("_thrown");

        public StateBinding _netDoubleBeepBinding = new NetSoundBinding("_netDoubleBeep");

        public NetSoundEffect _netDoubleBeep = new("doubleBeep");

        public StateBinding _netPinPlayHBinding = new NetSoundBinding("_netPin");

        public StateBinding _mineOwnerBinding = new(nameof(_mineOwner));

        public NetSoundEffect _netPin = new("pullPin");

        private SpriteMap _sprite;

        public bool _pin = true;

        public bool blownUp;

        public float _timer = 1.2f;

        public bool _armed;

        public bool _clicked;

        public float addWeight;

        public int _framesSinceArm;

        public float _holdingWeight;

        public bool _thrown;

        private Sprite _mineFlash;

        private Dictionary<Duck, float> _ducksOnMine = new();

        public List<PhysicsObject> previousThings = new();

        private float prevAngle;

        public bool pin => _pin;

        public Dictionary<Duck, float> ducksOnMine => _ducksOnMine;

        public Duck _targetPlayer;

        public Duck _mineOwner;

        public bool IsTargetVaild => _targetPlayer?.dead == false && _targetPlayer?.ragdoll == null;

        public NanoMine(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 1;
            _ammoType = new ATShrapnel();
            _type = "gun";
            _sprite = this.ReadyToRunMap("nanoMine.png", 18, 16);
            SpriteMap sprite = _sprite;
            int[] frames = new int[1];
            sprite.AddAnimation("pickup", 1f, looping: true, frames);
            _sprite.AddAnimation("idle", 0.05f, true, 1, 2);
            _sprite.SetAnimation("pickup");
            graphic = _sprite;
            center = new Vec2(9f, 8f);
            collisionOffset = new Vec2(-5f, -5f);
            collisionSize = new Vec2(10f, 9f);
            _mineFlash = new Sprite("mineFlash");
            _mineFlash.CenterOrigin();
            _mineFlash.alpha = 0f;
            bouncy = 0f;
            friction = 0.2f;
        }

        public void Arm()
        {
            if (_armed)
            {
                return;
            }
            _holdingWeight = 0f;
            _armed = true;
            if (isServerForObject)
            {
                if (Network.isActive)
                {
                    _netPin.Play();
                }
                else
                {
                    SFX.Play("pullPin");
                }
            }
        }

        protected override bool OnDestroy(DestroyType type = null)
        {
            if (!_pin)
            {
                BlowUp();
                return true;
            }
            return false;
        }

        public void UpdatePinState()
        {
            if (!_pin)
            {
                canPickUp = false;
                _sprite.SetAnimation("idle");
                collisionOffset = new Vec2(-6f, -2f);
                collisionSize = new Vec2(12f, 3f);
                depth = 0.8f;
                _hasOldDepth = false;
                thickness = 1f;
                center = new Vec2(9f, 14f);
            }
            else
            {
                canPickUp = true;
                _sprite.SetAnimation("pickup");
                collisionOffset = new Vec2(-5f, -4f);
                collisionSize = new Vec2(10f, 8f);
                thickness = -1f;
            }
        }

        public void FindTarget()
        {
            _targetPlayer = null;
            float shortest = float.MaxValue;
            foreach (Duck target in Level.CheckCircleAll<Duck>(position, 250))
            {
                if (target != _mineOwner && !target.dead && Level.CheckLine<Block>(position, target.position) == null)
                {
                    if ((position - target.position).length < shortest)
                    {
                        shortest = (position - target.position).length;
                        _targetPlayer = target;
                    }
                }
            }
        }

        public override void Update()
        {
            if (!_pin)
            {
                FindTarget();
                if (IsTargetVaild && grounded && _ducksOnMine.Count == 0)
                {
                    bool onGround = grounded;
                    StupidMoving.ThingMoveToVertically(this, _targetPlayer.position, 5f);
                    if (onGround && vSpeed <= 0 && (position - _targetPlayer.position).length >= 10)
                    {
                        vSpeed = -3;
                    }
                    else vSpeed = 0;
                }
            }

            if (!pin)
            {
                collisionOffset = new Vec2(-6f, -2f);
                collisionSize = new Vec2(12f, 3f);
            }
            base.Update();
            if (!pin && Math.Abs(prevAngle - angle) > 0.1f)
            {
                Vec2 colSizeWide = new(14f, 3f);
                Vec2 colOffsetWide = new(-7f, -2f);
                Vec2 colSizeTall = new(4f, 14f);
                Vec2 colOffsetTall = new(-2f, -7f);
                float norm = (float)Math.Abs(Math.Sin(angle));
                collisionSize = colSizeWide * (1f - norm) + colSizeTall * norm;
                collisionOffset = colOffsetWide * (1f - norm) + colOffsetTall * norm;
                prevAngle = angle;
            }
            UpdatePinState();
            if (_sprite.imageIndex == 2)
            {
                _mineFlash.alpha = Lerp.Float(_mineFlash.alpha, 0.4f, 0.08f);
            }
            else
            {
                _mineFlash.alpha = Lerp.Float(_mineFlash.alpha, 0f, 0.08f);
            }
            if (_armed)
            {
                _sprite.speed = 2f;
            }
            if (_thrown && owner == null)
            {
                _thrown = false;
                if (Math.Abs(hSpeed) + Math.Abs(vSpeed) > 0.4f)
                {
                    angleDegrees = 180f;
                }
            }
            if (_armed)
            {
                _framesSinceArm++;
            }
            if (!_pin && _grounded && (!_armed || _framesSinceArm > 4))
            {
                canPickUp = false;
                float holdWeight = addWeight;
                IEnumerable<PhysicsObject> col = Level.CheckLineAll<PhysicsObject>(new Vec2(x - 7f, y - 4f), new Vec2(x + 7f, y - 4f));
                List<Duck> ducks = new();
                Duck stepDuck = null;
                bool hadServerThing = false;
                foreach (PhysicsObject t in previousThings)
                {
                    if (t.isServerForObject)
                    {
                        hadServerThing = true;
                    }
                }
                previousThings.Clear();
                foreach (PhysicsObject o in col)
                {
                    if (o == this || o.owner != null || (o is Holdable && (!(o as Holdable).canPickUp || (o as Holdable).hoverSpawner != null)) || Math.Abs(o.bottom - bottom) > 6f)
                    {
                        continue;
                    }
                    previousThings.Add(o);
                    if (o is Duck || o is TrappedDuck || o is RagdollPart)
                    {
                        holdWeight += 5f;
                        Duck d2 = o as Duck;
                        if (o is TrappedDuck)
                        {
                            d2 = (o as TrappedDuck).captureDuck;
                        }
                        else if (o is RagdollPart && (o as RagdollPart).doll != null)
                        {
                            d2 = (o as RagdollPart).doll.captureDuck;
                        }
                        if (d2 != null)
                        {
                            stepDuck = d2;
                            if (!_ducksOnMine.ContainsKey(d2))
                            {
                                _ducksOnMine[d2] = 0f;
                            }
                            _ducksOnMine[d2] += Maths.IncFrameTimer();
                            ducks.Add(d2);
                        }
                    }
                    else
                    {
                        holdWeight += o.weight;
                    }
                }
                List<Duck> remove = new();
                foreach (KeyValuePair<Duck, float> pair in _ducksOnMine)
                {
                    if (!ducks.Contains(pair.Key))
                    {
                        remove.Add(pair.Key);
                    }
                    else
                    {
                        pair.Key.profile.stats.timeSpentOnMines += Maths.IncFrameTimer();
                    }
                }
                foreach (Duck d in remove)
                {
                    _ducksOnMine.Remove(d);
                }
                if (holdWeight < _holdingWeight && hadServerThing)
                {
                    Fondle(this, DuckNetwork.localConnection);
                    if (!_armed)
                    {
                        Arm();
                    }
                    else
                    {
                        _timer = -1f;
                    }
                }
                if (_armed && holdWeight > _holdingWeight)
                {
                    if (!_clicked && stepDuck != null)
                    {
                        stepDuck.profile.stats.minesSteppedOn++;
                    }
                    _clicked = true;
                    if (Network.isActive)
                    {
                        _netDoubleBeep.Play();
                    }
                    else
                    {
                        SFX.Play("doubleBeep");
                    }
                }
                _holdingWeight = holdWeight;
            }
            if (_timer < 0f && isServerForObject)
            {
                _timer = 1f;
                BlowUp();
            }
            addWeight = 0f;
        }

        public void BlowUp()
        {
            if (blownUp)
            {
                return;
            }
            MakeBlowUpHappen(position);
            blownUp = true;
            IEnumerable<PhysicsObject> area = Level.CheckCircleAll<PhysicsObject>(position, 22f);
            foreach (PhysicsObject p in area)
            {
                if (p != this)
                {
                    Vec2 dir2 = p.position - position;
                    float mul = 1f - Math.Min(dir2.length, 22f) / 22f;
                    float len = mul * 4f;
                    dir2.Normalize();
                    p.hSpeed += len * dir2.x;
                    p.vSpeed += -5f * mul;
                    p.sleeping = false;
                    Fondle(p);
                }
            }
            float cx = position.x;
            float cy = position.y;
            var firedBullets = new List<Bullet>(20);
            for (int i = 0; i < 20; i++)
            {
                float dir = (float)i * 18f - 5f + Rando.Float(10f);
                ATShrapnel shrap = new()
                {
                    range = 60f + Rando.Float(18f)
                };
                Bullet bullet = new(cx, cy, shrap, dir)
                {
                    firedFrom = this
                };
                firedBullets.Add(bullet);
                Level.Add(bullet);
            }
            bulletFireIndex += 20;
            if (Network.isActive && isServerForObject)
            {
                NMFireGun gunEvent = new(this, firedBullets, bulletFireIndex, rel: false, 4);
                Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                firedBullets.Clear();
            }
            if (Recorder.currentRecording != null)
            {
                Recorder.currentRecording.LogBonus();
            }
            Level.Remove(this);
        }

        public void MakeBlowUpHappen(Vec2 pos)
        {
            if (!blownUp)
            {
                blownUp = true;
                SFX.Play("explode");
                Graphics.FlashScreen();
                float cx = pos.x;
                float cy = pos.y;
                Level.Add(new ExplosionPart(cx, cy));
                int num = 6;
                if (Graphics.effectsLevel < 2)
                {
                    num = 3;
                }
                for (int i = 0; i < num; i++)
                {
                    float dir = (float)i * 60f + Rando.Float(-10f, 10f);
                    float dist = Rando.Float(12f, 20f);
                    ExplosionPart ins = new(cx + (float)(Math.Cos(Maths.DegToRad(dir)) * (double)dist), cy - (float)(Math.Sin(Maths.DegToRad(dir)) * (double)dist));
                    Level.Add(ins);
                }
            }
        }

        public override void OnNetworkBulletsFired(Vec2 pos)
        {
            MakeBlowUpHappen(pos);
            base.OnNetworkBulletsFired(pos);
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (bullet.isLocal && owner == null && !canPickUp && _timer > 0f)
            {
                Fondle(this, DuckNetwork.localConnection);
                BlowUp();
            }
            return false;
        }

        public override void Draw()
        {
            if (_mineFlash.alpha > 0.01f)
            {
                Graphics.Draw(_mineFlash, x, y - 3f);
            }
            base.Draw();
            if (IsTargetVaild && duck?.profile.localPlayer == true)
            {
                var start = this.topLeft + graphic.center * graphic.scale;
                float fontWidth = _biosFont.GetWidth("@SHOOT@", false, duck.inputProfile);
                _biosFont.Draw("@SHOOT@", _targetPlayer.position + new Vec2(-fontWidth / 2, -20), Color.White, 1, duck.inputProfile);
                Graphics.DrawLine(start, _targetPlayer.position, Color.White, duck is null ? 0.6f : 1f, 1);
            }
        }

        public override void OnPressAction()
        {
            if (owner == null)
            {
                _pin = false;
                if (heat > 0.5f)
                {
                    BlowUp();
                }
            }
            if (_pin)
            {
                _pin = false;
                UpdatePinState();
                Duck duckOwner = owner as Duck;
                if (duckOwner != null)
                {
                    _holdingWeight = 5f;
                    _mineOwner = duckOwner;
                    duckOwner.doThrow = true;
                    _responsibleProfile = duckOwner.profile;
                }
                else
                {
                    Arm();
                }
                _thrown = true;
            }
        }
    }
}
