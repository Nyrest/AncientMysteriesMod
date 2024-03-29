﻿namespace AncientMysteries.Items
{
    public class AMMeleeFlagBinding : StateFlagBase
    {
        public override ushort ushortValue
        {
            get
            {
                _value = 0;
                AMMelee obj = _thing as AMMelee;
                if (obj._jabStance)
                {
                    _value |= 16;
                }
                if (obj._crouchStance)
                {
                    _value |= 8;
                }
                if (obj._slamStance)
                {
                    _value |= 4;
                }
                if (obj._swinging)
                {
                    _value |= 2;
                }
                if (obj._volatile)
                {
                    _value |= 1;
                }
                return _value;
            }
            set
            {
                _value = value;
                AMMelee obj = _thing as AMMelee;
                obj._jabStance = (_value & 0x10) != 0;
                obj._crouchStance = (_value & 8) != 0;
                obj._slamStance = (_value & 4) != 0;
                obj._swinging = (_value & 2) != 0;
                obj._volatile = (_value & 1) != 0;
            }
        }

        public AMMeleeFlagBinding(GhostPriority p = GhostPriority.Normal)
            : base(p, 5)
        {
        }
    }

    public abstract class AMMelee : AMGun
    {
        public StateBinding _swingBinding = new(doLerp: true, "_swing");

        public StateBinding _holdBinding = new(doLerp: true, "_hold");

        public StateBinding _stanceBinding = new AMMeleeFlagBinding();

        public StateBinding _pullBackBinding = new(doLerp: true, "_pullBack");

        public StateBinding _throwSpinBinding = new(doLerp: true, "_throwSpin");

        public StateBinding _addOffsetXBinding = new("_addOffsetX");

        public StateBinding _addOffsetYBinding = new("_addOffsetY");

        public float _swing;

        public float _hold;

        public bool _drawing;

        public bool _clashWithWalls = true;

        public bool _pullBack;

        public bool _jabStance;

        public bool _crouchStance;

        public bool _slamStance;

        public bool _swinging;

        public float _addOffsetX;

        public float _addOffsetY;

        public bool _swingPress;

        public bool _shing;

        public static bool _playedShing;

        public bool _atRest = true;

        public bool _swung;

        public bool _wasLifted;

        public float _throwSpin;

        public int _framesExisting;

        public int _hitWait;

        public bool _enforceJabSwing = true;

        public bool _allowJabMotion = true;

        public SpriteMap _swordSwing;

        public float _stickWait;

        public Vec2 additionalHoldOffset = Vec2.Zero;

        public int _unslam;

        public float _afterSwingWait;

        public float _afterSwingCounter;

        public Vec2 _tapeOffset = Vec2.Zero;

        public byte blocked;

        public bool _volatile;

        public Vec2 centerHeld = new(4f, 21f);

        public Vec2 centerUnheld = new(4f, 11f);

        public bool _stayVolatile;

        public bool bayonetLethal;

        public float _prevAngle;

        public Vec2 _prevPos;

        public int _prevOffdir = -1;

        public float[] _lastAngles = new float[8];

        public Vec2[] _lastPositions = new Vec2[8];

        public int _lastIndex;

        public int _lastSize;

        public Thing _prevHistoryOwner;

        public Vec2 _lastHistoryPos = Vec2.Zero;

        public float _lastHistoryAngle;

        public string _swingSound = "swipe";

        public float _timeSinceSwing;

        public override float angle
        {
            get
            {
                return _drawing ? _angle : base.angle + ((_swing + _hold) * offDir);
            }
            set
            {
                _angle = value;
            }
        }

        public bool jabStance => _jabStance;

        public bool crouchStance => _crouchStance;

        public virtual Vec2 barrelStartPos
        {
            get
            {
                return owner switch
                {
                    null => position - (Offset(barrelOffset) - position).normalized * 6f,
                    _ => _slamStance
                        ? position + (Offset(barrelOffset) - position).normalized * 12f
                        : position + (Offset(barrelOffset) - position).normalized * 2f,
                };
            }
        }

        public override Vec2 tapedOffset
        {
            get
            {
                return tapedCompatriot is Gun ? (tapedCompatriot as Gun).barrelOffset + new Vec2(-14f, 2f) : new Vec2(-6f, -3f);
            }
        }

        public new bool held
        {
            get
            {
                return duck != null && duck.holdObject == this;
            }
        }

        public virtual DestroyType destroyType => new DTImpale(this);

        public AMMelee(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 4;
            _type = "gun";
            graphic = new Sprite("sword");
            center = new Vec2(4f, 21f);
            collisionOffset = new Vec2(-2f, -16f);
            collisionSize = new Vec2(4f, 18f);
            _barrelOffsetTL = new Vec2(4f, 1f);
            _fireSound = "smg";
            _fullAuto = true;
            _fireWait = 1f;
            _kickForce = 3f;
            _holdOffset = new Vec2(-4f, 4f);
            weight = 0.9f;
            physicsMaterial = PhysicsMaterial.Metal;
            _swordSwing = new SpriteMap("swordSwipe", 32, 32);
            _swordSwing.AddAnimation("swing", 0.6f, false, 0, 1, 1, 2);
            _swordSwing.currentAnimation = "swing";
            _swordSwing.speed = 0f;
            _swordSwing.center = new Vec2(9f, 25f);
            holsterAngle = 180f;
            tapedIndexPreference = 0;
            _bouncy = 0.5f;
            _impactThreshold = 0.3f;
            editorTooltip = "Basically a giant letter opener.";
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void UpdateTapedPositioning(TapedGun pTaped)
        {
            if (pTaped.gun1 != null && pTaped.gun2 != null)
            {
                angleDegrees = pTaped.angleDegrees - (90 * offDir);
            }
            if (tapedCompatriot is Gun gun)
            {
                gun.addVerticalTapeOffset = false;
                tape._holdOffset = gun._holdOffset;
                tape.handOffset = gun.handOffset;
            }
            collisionOffset = new Vec2(-4f, 0f);
            collisionSize = new Vec2(4f, 4f);
            center = centerHeld;
            thickness = 0f;
        }

        public override void CheckIfHoldObstructed()
        {
            if (owner is Duck duckOwner)
            {
                duckOwner.holdObstructed = false;
            }
        }

        public override bool HolsterActivate(Holster pHolster)
        {
            pHolster.EjectItem();
            return true;
        }

        public override void Thrown()
        {
        }

        public virtual void Shing()
        {
            if (_shing)
            {
                return;
            }
            _pullBack = false;
            _swinging = false;
            _shing = true;
            _swingPress = false;
            if (!_playedShing)
            {
                _playedShing = true;
                SFX.Play("swordClash", Rando.Float(0.6f, 0.7f), Rando.Float(-0.1f, 0.1f), Rando.Float(-0.1f, 0.1f));
            }
            Vec2 vec = (position - barrelPosition).normalized;
            Vec2 start = barrelPosition;
            for (int i = 0; i < 6; i++)
            {
                Spark s = Spark.New(start.x, start.y, new Vec2(Rando.Float(-1f, 1f), Rando.Float(-1f, 1f)));
                Level.Add(s);
                start += vec * 4f;
            }
            if (duck != null)
            {
                RumbleManager.AddRumbleEvent(duck.profile, new RumbleEvent(RumbleIntensity.Light, RumbleDuration.Pulse, RumbleFalloff.None));
            }
            _swung = false;
            _swordSwing.speed = 0f;
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (duck != null)
            {
                if (blocked == 0)
                {
                    duck.AddCoolness(1);
                }
                else
                {
                    blocked++;
                    if (blocked > 4)
                    {
                        blocked = 1;
                        duck.AddCoolness(1);
                    }
                }
                RumbleManager.AddRumbleEvent(duck.profile, new RumbleEvent(RumbleIntensity.Kick, RumbleDuration.Pulse, RumbleFalloff.None));
                SFX.Play("ting");
                return base.Hit(bullet, hitPos);
            }
            return false;
        }

        public override void OnSoftImpact(MaterialThing with, ImpactedFrom from)
        {
            if ((tape == null || tape.owner == null) && _wasLifted && owner == null && (with is Block || (with is IPlatform && from == ImpactedFrom.Bottom && vSpeed > 0f)))
            {
                Shing();
                _framesSinceThrown = 25;
            }
        }

        public override void ReturnToWorld()
        {
            _throwSpin = 90f;
            RestoreCollisionSize();
        }

        public virtual void RestoreCollisionSize(bool pHeld = false)
        {
            if (pHeld)
            {
                collisionOffset = new Vec2(-4f, 0f);
                collisionSize = new Vec2(4f, 4f);
                if (_crouchStance && !_jabStance)
                {
                    collisionOffset = new Vec2(-2f, -19f);
                    collisionSize = new Vec2(4f, 16f);
                    thickness = 3f;
                }
            }
            else
            {
                collisionOffset = new Vec2(-2f, -16f);
                collisionSize = new Vec2(4f, 18f);
                if (_wasLifted)
                {
                    collisionOffset = new Vec2(-4f, -2f);
                    collisionSize = new Vec2(8f, 4f);
                }
            }
        }

        public virtual void PerformAirSpin()
        {
            if (hSpeed > 0f)
            {
                _throwSpin += ((Math.Abs(hSpeed) + Math.Abs(vSpeed)) * 2f) + 4f;
            }
            else
            {
                _throwSpin -= ((Math.Abs(hSpeed) + Math.Abs(vSpeed)) * 2f) + 4f;
            }
        }

        public virtual void QuadLaserHit(QuadLaserBullet pBullet)
        {
        }

        public virtual void UpdateCrouchStance()
        {
            if (!_crouchStance)
            {
                _hold = -0.4f;
                handOffset = new Vec2(_addOffsetX, _addOffsetY);
                _holdOffset = new Vec2(-4f + _addOffsetX, 4f + _addOffsetY) + additionalHoldOffset;
            }
            else
            {
                _hold = 0f;
                _holdOffset = new Vec2(0f + _addOffsetX, 4f + _addOffsetY) + additionalHoldOffset;
                handOffset = new Vec2(3f + _addOffsetX, _addOffsetY);
            }
        }

        public virtual void UpdateJabPullback()
        {
            _swing = MathHelper.Lerp(_swing, 1.75f, 0.4f);
            if (_swing > 1.55f)
            {
                _swing = 1.55f;
                _shing = false;
                _swung = false;
            }
            _addOffsetX = MathHelper.Lerp(_addOffsetX, -12f, 0.45f);
            if (_addOffsetX < -12f)
            {
                _addOffsetX = -12f;
            }
            _addOffsetY = MathHelper.Lerp(_addOffsetY, -4f, 0.35f);
            if (_addOffsetX < -3f)
            {
                _addOffsetY = -3f;
            }
        }

        public virtual void UpdateSlamPullback()
        {
            _swing = MathHelper.Lerp(_swing, 3.14f, 0.8f);
            if (_swing > 3.1f && _unslam == 0)
            {
                _swing = 3.14f;
                _shing = false;
                _swung = true;
            }
            _addOffsetX = MathHelper.Lerp(_addOffsetX, -5f, 0.45f);
            if (_addOffsetX < -4.6f)
            {
                _addOffsetX = -5f;
            }
            _addOffsetY = MathHelper.Lerp(_addOffsetY, -6f, 0.35f);
            if (_addOffsetX < -5.5f)
            {
                _addOffsetY = -6f;
            }
        }

        public override void Update()
        {
            bayonetLethal = false;
            if (tape != null && tapedCompatriot != null)
            {
                if (tapedCompatriot != null && (Math.Abs(_prevAngle - angleDegrees) > 1f || (_prevPos - position).length > 2f))
                {
                    bayonetLethal = true;
                }
                if (isServerForObject && bayonetLethal)
                {
                    foreach (IAmADuck d2 in Level.CheckLineAll<IAmADuck>(Offset(new Vec2(4f, 10f) - center + _extraOffset), barrelPosition))
                    {
                        if (d2 == duck)
                        {
                            continue;
                        }
                        if (d2 is not MaterialThing realThing3)
                        {
                            continue;
                        }
                        bool isntPrevOwner = realThing3 != prevOwner;
                        if (isntPrevOwner && realThing3 is RagdollPart && (realThing3 as RagdollPart).doll != null && (realThing3 as RagdollPart).doll.captureDuck == prevOwner)
                        {
                            isntPrevOwner = false;
                        }
                        int waitFrames = 16;
                        if (isntPrevOwner || tape._framesSinceThrown > waitFrames)
                        {
                            realThing3.Destroy(destroyType);
                            if (Recorder.currentRecording != null)
                            {
                                Recorder.currentRecording.LogBonus();
                            }
                        }
                    }
                }
                if (_prevOffdir != offDir)
                {
                    ResetTrailHistory();
                }
                _prevOffdir = offDir;
                _prevPos = position;
                _prevAngle = angleDegrees;
                return;
            }
            _tapeOffset = Vec2.Zero;
            base.Update();
            _timeSinceSwing += Maths.IncFrameTimer();
            if (_swordSwing.finished)
            {
                _swordSwing.speed = 0f;
            }
            if (_hitWait > 0)
            {
                _hitWait--;
            }
            _framesExisting++;
            if (_framesExisting > 100)
            {
                _framesExisting = 100;
            }
            if (Math.Abs(hSpeed) + Math.Abs(vSpeed) > 4f && _framesExisting > 10)
            {
                _wasLifted = true;
            }
            if (owner != null)
            {
                _wasLifted = true;
            }
            if (held)
            {
                _hold = -0.4f;
                _wasLifted = true;
                center = centerHeld;
                _framesSinceThrown = 0;
                _volatile = false;
            }
            else
            {
                if (_framesSinceThrown == 1)
                {
                    _throwSpin = Maths.RadToDeg(angle) - 90f;
                    _hold = 0f;
                    _swing = 0f;
                }
                if (_wasLifted)
                {
                    if (enablePhysics)
                    {
                        angleDegrees = 90f + _throwSpin;
                    }
                    center = centerUnheld;
                    if (duck != null || owner is Holster)
                    {
                        _hold = 0f;
                        _swing = 0f;
                        angleDegrees = 0f;
                        _throwSpin = 0f;
                        return;
                    }
                }
                _volatile = _stayVolatile;
                bool spinning = false;
                bool againstWall = false;
                if (Math.Abs(hSpeed) + Math.Abs(vSpeed) > 2f || !grounded)
                {
                    if (!grounded && Level.CheckRect<Block>(position + new Vec2(-6f, -6f), position + new Vec2(6f, -2f)) != null)
                    {
                        againstWall = true;
                        if (vSpeed > 4f)
                        {
                            _volatile = true;
                        }
                    }
                    if (!againstWall && !_grounded && (Level.CheckPoint<IPlatform>(position + new Vec2(0f, 8f)) == null || vSpeed < 0f))
                    {
                        PerformAirSpin();
                        spinning = true;
                    }
                }
                if (_framesExisting > 15 && Math.Abs(hSpeed) + Math.Abs(vSpeed) > 3f)
                {
                    _volatile = true;
                }
                if (!spinning || againstWall)
                {
                    _throwSpin %= 360f;
                    if (againstWall)
                    {
                        if (Math.Abs(_throwSpin - 90f) < Math.Abs(_throwSpin + 90f))
                        {
                            _throwSpin = Lerp.Float(_throwSpin, 90f, 16f);
                        }
                        else
                        {
                            _throwSpin = Lerp.Float(-90f, 0f, 16f);
                        }
                    }
                    else if (_throwSpin is > 90f and < 270f)
                    {
                        _throwSpin = Lerp.Float(_throwSpin, 180f, 14f);
                    }
                    else
                    {
                        if (_throwSpin > 180f)
                        {
                            _throwSpin -= 360f;
                        }
                        else if (_throwSpin < -180f)
                        {
                            _throwSpin += 360f;
                        }
                        _throwSpin = Lerp.Float(_throwSpin, 0f, 14f);
                    }
                }
                if (_volatile && _hitWait == 0)
                {
                    (Offset(barrelOffset) - position).Normalize();
                    Offset(barrelOffset);
                    bool rebound = false;
                    foreach (AMMelee s3 in Level.current.things[typeof(AMMelee)])
                    {
                        if (s3 != this && s3.owner != null && s3._crouchStance && !s3._jabStance && !s3._jabStance && ((hSpeed > 0f && s3.x > x - 4f) || (hSpeed < 0f && s3.x < x + 4f)) && Collision.LineIntersect(barrelStartPos, barrelPosition, s3.barrelStartPos, s3.barrelPosition))
                        {
                            Shing();
                            s3.Shing();
                            rebound = true;
                            _hitWait = 4;
                            s3.owner.hSpeed += offDir * 1f;
                            s3.owner.vSpeed -= 1f;
                            hSpeed = (0f - hSpeed) * 0.6f;
                        }
                    }
                    int waitFrames3 = 12;
                    if (_stayVolatile)
                    {
                        waitFrames3 = 22;
                    }
                    if (!rebound)
                    {
                        foreach (Chainsaw s2 in Level.current.things[typeof(Chainsaw)])
                        {
                            if (s2.owner != null && s2.throttle && Collision.LineIntersect(barrelStartPos, barrelPosition, s2.barrelStartPos, s2.barrelPosition))
                            {
                                Shing();
                                s2.Shing(this);
                                s2.owner.hSpeed += offDir * 1f;
                                s2.owner.vSpeed -= 1f;
                                rebound = true;
                                hSpeed = (0f - hSpeed) * 0.6f;
                                _hitWait = 4;
                                if (Recorder.currentRecording != null)
                                {
                                    Recorder.currentRecording.LogBonus();
                                }
                            }
                        }
                        if (!rebound)
                        {
                            Helmet helmetHit2 = Level.CheckLine<Helmet>(barrelStartPos, barrelPosition, null);
                            if (helmetHit2 != null && helmetHit2.equippedDuck != null && (helmetHit2.owner != prevOwner || _framesSinceThrown > waitFrames3))
                            {
                                hSpeed = (0f - hSpeed) * 0.6f;
                                Shing();
                                rebound = true;
                                _hitWait = 4;
                            }
                            else
                            {
                                ChestPlate chestHit2 = Level.CheckLine<ChestPlate>(barrelStartPos, barrelPosition, null);
                                if (chestHit2 != null && chestHit2.equippedDuck != null && (chestHit2.owner != prevOwner || _framesSinceThrown > waitFrames3))
                                {
                                    hSpeed = (0f - hSpeed) * 0.6f;
                                    Shing();
                                    rebound = true;
                                    _hitWait = 4;
                                }
                            }
                        }
                    }
                    if (!rebound && isServerForObject)
                    {
                        foreach (IAmADuck d5 in Level.CheckLineAll<IAmADuck>(barrelStartPos, barrelPosition))
                        {
                            if (d5 == duck)
                            {
                                continue;
                            }
                            if (d5 is not MaterialThing realThing5)
                            {
                                continue;
                            }
                            bool isntPrevOwner3 = realThing5 != prevOwner;
                            if (isntPrevOwner3 && realThing5 is RagdollPart && (realThing5 as RagdollPart).doll != null && (realThing5 as RagdollPart).doll.captureDuck == prevOwner)
                            {
                                isntPrevOwner3 = false;
                            }
                            if (isntPrevOwner3 || _framesSinceThrown > waitFrames3)
                            {
                                realThing5.Destroy(destroyType);
                                if (Recorder.currentRecording != null)
                                {
                                    Recorder.currentRecording.LogBonus();
                                }
                            }
                        }
                    }
                }
            }
            if (_stayVolatile && isServerForObject)
            {
                int waitFrames2 = 28;
                foreach (IAmADuck d4 in Level.CheckLineAll<IAmADuck>(barrelStartPos, barrelPosition))
                {
                    if (d4 == duck)
                    {
                        continue;
                    }
                    if (d4 is not MaterialThing realThing4)
                    {
                        continue;
                    }
                    bool isntPrevOwner2 = realThing4 != prevOwner;
                    if (isntPrevOwner2 && realThing4 is RagdollPart && (realThing4 as RagdollPart).doll != null && (realThing4 as RagdollPart).doll.captureDuck == prevOwner)
                    {
                        isntPrevOwner2 = false;
                    }
                    if (isntPrevOwner2 || _framesSinceThrown > waitFrames2)
                    {
                        realThing4.Destroy(destroyType);
                        if (Recorder.currentRecording != null)
                        {
                            Recorder.currentRecording.LogBonus();
                        }
                    }
                }
            }
            if (owner == null)
            {
                _swinging = false;
                _jabStance = false;
                _crouchStance = false;
                _pullBack = false;
                _swung = false;
                _shing = false;
                _swing = 0f;
                _swingPress = false;
                _slamStance = false;
                _unslam = 0;
            }
            _afterSwingCounter += Maths.IncFrameTimer();
            if (isServerForObject)
            {
                if (_unslam > 1)
                {
                    _unslam--;
                    _slamStance = true;
                }
                else if (_unslam == 1)
                {
                    _unslam = 0;
                    _slamStance = false;
                }
                if (_pullBack)
                {
                    if (duck != null)
                    {
                        if (_jabStance)
                        {
                            _pullBack = false;
                            _swinging = true;
                        }
                        else
                        {
                            _swinging = true;
                            _pullBack = false;
                        }
                    }
                }
                else if (_swinging)
                {
                    if (_jabStance)
                    {
                        _addOffsetX = MathHelper.Lerp(_addOffsetX, 3f, 0.4f);
                        if (_addOffsetX > 2f && !action)
                        {
                            _swinging = false;
                        }
                    }
                    else if (raised)
                    {
                        _swing = MathHelper.Lerp(_swing, -2.8f, 0.2f);
                        if (_swing < -2.4f && !action)
                        {
                            _swinging = false;
                            _swing = 1.8f;
                        }
                    }
                    else
                    {
                        _swing = MathHelper.Lerp(_swing, 2.1f, 0.4f);
                        if (_swing > 1.8f && !action)
                        {
                            _swinging = false;
                            _swing = 1.8f;
                        }
                    }
                }
                else
                {
                    if (_wasLifted && !_swinging && (!_swingPress || _shing || (_jabStance && _addOffsetX < 1f) || (!_jabStance && _swing < 1.6f)))
                    {
                        if (_jabStance)
                        {
                            UpdateJabPullback();
                        }
                        else if (_slamStance)
                        {
                            UpdateSlamPullback();
                        }
                        else if (_afterSwingWait < _afterSwingCounter)
                        {
                            float lerpSpeed = 0.36f;
                            _swing = MathHelper.Lerp(_swing, -0.22f, lerpSpeed);
                            _addOffsetX = MathHelper.Lerp(_addOffsetX, 1f, 0.2f);
                            if (_addOffsetX > 0f)
                            {
                                _addOffsetX = 0f;
                            }
                            _addOffsetY = MathHelper.Lerp(_addOffsetY, 1f, 0.2f);
                            if (_addOffsetY > 0f)
                            {
                                _addOffsetY = 0f;
                            }
                        }
                    }
                    if ((_swing < 0f || _jabStance) && _swing < 0f && _enforceJabSwing)
                    {
                        _swing = 0f;
                        _shing = false;
                        _swung = false;
                    }
                }
            }
            if (duck != null)
            {
                RestoreCollisionSize(pHeld: true);
                _swingPress = false;
                if (!_pullBack && !_swinging)
                {
                    _crouchStance = false;
                    _jabStance = false;
                    if (duck.crouch)
                    {
                        if (!_pullBack && !_swinging && duck.inputProfile.Down((offDir > 0) ? "LEFT" : "RIGHT"))
                        {
                            _jabStance = true;
                        }
                        _crouchStance = true;
                    }
                    if (!_crouchStance || _jabStance)
                    {
                        _slamStance = false;
                    }
                }
                UpdateCrouchStance();
            }
            else
            {
                RestoreCollisionSize();
                thickness = 0f;
            }
            if ((_swung || _swinging) && !_shing)
            {
                (Offset(barrelOffset) - position).Normalize();
                Offset(barrelOffset);
                IEnumerable<IAmADuck> hit = Level.CheckLineAll<IAmADuck>(barrelStartPos, barrelPosition);
                Block wallHit = Level.CheckLine<Block>(barrelStartPos, barrelPosition);
                Level.CheckRect<Icicles>(barrelStartPos, barrelPosition)?.Hurt(100f);
                if (wallHit != null && !_slamStance)
                {
                    if (offDir < 0 && wallHit.x > x)
                    {
                        wallHit = null;
                    }
                    else if (offDir > 0 && wallHit.x < x)
                    {
                        wallHit = null;
                    }
                }
                bool clashed = false;
                if (wallHit != null && _clashWithWalls)
                {
                    if (_slamStance)
                    {
                        owner.vSpeed = -5f;
                    }
                    Shing();
                    if (_slamStance)
                    {
                        _swung = false;
                        _unslam = 20;
                    }
                    if (wallHit is Window)
                    {
                        wallHit.Destroy(new DTImpact(this));
                    }
                }
                else if (!_jabStance && !_slamStance && isServerForObject)
                {
                    Thing ignore = null;
                    if (duck != null)
                    {
                        ignore = duck.GetEquipment(typeof(Helmet));
                    }
                    Vec2 barrel = barrelPosition + (barrelVector * 3f);
                    Vec2 p3 = new((position.x < barrel.x) ? position.x : barrel.x, (position.y < barrel.y) ? position.y : barrel.y);
                    Vec2 p2 = new((position.x > barrel.x) ? position.x : barrel.x, (position.y > barrel.y) ? position.y : barrel.y);
                    QuadLaserBullet laserHit = Level.CheckRect<QuadLaserBullet>(p3, p2);
                    if (laserHit != null)
                    {
                        Shing();
                        Fondle(laserHit);
                        laserHit.safeFrames = 8;
                        laserHit.safeDuck = duck;
                        float mag = laserHit.travel.length;
                        float mul = 1.5f;
                        laserHit.travel = (offDir > 0) ? new Vec2(mag * mul, 0f) : new Vec2((0f - mag) * mul, 0f);
                        QuadLaserHit(laserHit);
                    }
                    else
                    {
                        Helmet helmetHit = Level.CheckLine<Helmet>(barrelStartPos, barrelPosition, ignore);
                        if (helmetHit != null && helmetHit.equippedDuck != null && helmetHit.owner != null)
                        {
                            Shing();
                            helmetHit.owner.hSpeed += offDir * 3f;
                            helmetHit.owner.vSpeed -= 2f;
                            if (helmetHit.duck != null)
                            {
                                helmetHit.duck.crippleTimer = 1f;
                            }
                            helmetHit.Hurt(0.53f);
                            clashed = true;
                        }
                        else
                        {
                            if (duck != null)
                            {
                                ignore = duck.GetEquipment(typeof(ChestPlate));
                            }
                            ChestPlate chestHit = Level.CheckLine<ChestPlate>(barrelStartPos, barrelPosition, ignore);
                            if (chestHit != null && chestHit.equippedDuck != null && chestHit.owner != null)
                            {
                                Shing();
                                chestHit.owner.hSpeed += offDir * 3f;
                                chestHit.owner.vSpeed -= 2f;
                                if (chestHit.duck != null)
                                {
                                    chestHit.duck.crippleTimer = 1f;
                                }
                                chestHit.Hurt(0.53f);
                                clashed = true;
                            }
                        }
                    }
                }
                if (!clashed && isServerForObject)
                {
                    foreach (AMMelee s in Level.current.things[typeof(AMMelee)])
                    {
                        if (s == this || !s.held || _jabStance || s._jabStance || duck == null || !Collision.LineIntersect(barrelStartPos, barrelPosition, s.barrelStartPos, s.barrelPosition))
                        {
                            continue;
                        }
                        Shing();
                        s.Shing();
                        s.owner.hSpeed += offDir * 3f;
                        s.owner.vSpeed -= 2f;
                        duck.hSpeed += -offDir * 3f;
                        duck.vSpeed -= 2f;
                        if (s.duck != null && duck != null)
                        {
                            s.duck.crippleTimer = 1f;
                            duck.crippleTimer = 1f;
                            clashed = true;
                        }
                    }
                }
                if (clashed || !isServerForObject)
                {
                    return;
                }
                foreach (IAmADuck d3 in hit)
                {
                    if (d3 == duck || d3 == prevOwner)
                    {
                        continue;
                    }
                    if (d3 is MaterialThing realThing2)
                    {
                        if (realThing2 is Duck realDuck3 && !realDuck3.destroyed && duck != null)
                        {
                            RumbleManager.AddRumbleEvent(duck.profile, new RumbleEvent(RumbleIntensity.Light, RumbleDuration.Pulse, RumbleFalloff.Short));
                            Global.data.swordKills.valueInt++;
                        }
                        realThing2.Destroy(destroyType);
                    }
                }
            }
            else
            {
                if (!_crouchStance || duck == null)
                {
                    return;
                }
                foreach (IAmADuck d in Level.CheckLineAll<IAmADuck>(barrelStartPos, barrelPosition))
                {
                    if (d == duck)
                    {
                        continue;
                    }
                    if (d is not MaterialThing realThing)
                    {
                        continue;
                    }
                    if (realThing.vSpeed > 0.5f && realThing.bottom < position.y - 8f && realThing.left < barrelPosition.x && realThing.right > barrelPosition.x)
                    {
                        if (realThing is Duck realDuck2 && !realDuck2.destroyed)
                        {
                            RumbleManager.AddRumbleEvent(duck.profile, new RumbleEvent(RumbleIntensity.Light, RumbleDuration.Pulse, RumbleFalloff.Short));
                            Global.data.swordKills.valueInt++;
                        }
                        realThing.Destroy(destroyType);
                    }
                    else
                    {
                        if (_jabStance || realThing.destroyed || ((offDir <= 0 || !(realThing.x > duck.x)) && (offDir >= 0 || !(realThing.x < duck.x))))
                        {
                            continue;
                        }
                        if (realThing is Duck)
                        {
                            (realThing as Duck).crippleTimer = 1f;
                        }
                        else if ((duck.x > realThing.x && realThing.hSpeed > 1.5f) || (duck.x < realThing.x && realThing.hSpeed < -1.5f))
                        {
                            if (realThing is Duck realDuck && !realDuck.destroyed)
                            {
                                RumbleManager.AddRumbleEvent(duck.profile, new RumbleEvent(RumbleIntensity.Light, RumbleDuration.Pulse, RumbleFalloff.Short));
                                Global.data.swordKills.valueInt++;
                            }
                            realThing.Destroy(destroyType);
                        }
                        Fondle(realThing);
                        realThing.hSpeed = offDir * 3f;
                        realThing.vSpeed = -2f;
                    }
                }
            }
        }

        public virtual void ResetTrailHistory()
        {
            _lastAngles = new float[8];
            _lastPositions = new Vec2[8];
            _lastIndex = 0;
            _lastSize = 0;
            _lastHistoryPos = Vec2.Zero;
        }

        public int historyIndex(int idx)
        {
            int ret = _lastIndex - idx - 1;
            if (ret < 0)
            {
                ret += 8;
            }
            return ret;
        }

        public void addHistory(float angle, Vec2 position)
        {
            if (_lastHistoryPos != Vec2.Zero)
            {
                _lastAngles[_lastIndex] = (angle + _lastHistoryAngle) / 2f;
                _lastPositions[_lastIndex] = (position + _lastHistoryPos) / 2f;
                _lastIndex = (_lastIndex + 1) % 8;
                _lastSize++;
            }
            _lastAngles[_lastIndex] = angle;
            _lastPositions[_lastIndex] = position;
            _lastIndex = (_lastIndex + 1) % 8;
            _lastSize++;
            _lastHistoryPos = position;
            _lastHistoryAngle = angle;
        }

        public override void Draw()
        {
            _playedShing = false;
            if (owner != _prevHistoryOwner)
            {
                _prevHistoryOwner = owner;
                ResetTrailHistory();
            }
            if (DevConsole.showCollision)
            {
                Graphics.DrawLine(barrelStartPos, barrelPosition, Color.Red, 1f, 1f);
            }
            if (_swordSwing.speed > 0f)
            {
                if (duck != null)
                {
                    _swordSwing.flipH = duck.offDir <= 0;
                }
                _swordSwing.alpha = 0.4f;
                _swordSwing.position = position;
                _swordSwing.depth = depth + 1;
                _swordSwing.Draw();
            }
            Vec2 pos = position;
            Depth d = depth;
            graphic.color = Color.White;
            if ((owner == null && velocity.length > 1f) || _swing != 0f || (tape != null && bayonetLethal))
            {
                float al = alpha;
                alpha = 1f;
                float rlAngle = angle;
                _drawing = true;
                float a = _angle;
                angle = rlAngle;
                for (int i = 0; i < 7; i++)
                {
                    base.Draw();
                    if (_lastSize <= i)
                    {
                        break;
                    }
                    int idx = historyIndex(i);
                    _angle = _lastAngles[idx];
                    position = _lastPositions[idx];
                    depth -= 2;
                    alpha -= 0.15f;
                    graphic.color = Color.Red;
                }
                position = pos;
                depth = d;
                alpha = al;
                _angle = a;
                xscale = 1f;
                _drawing = false;
            }
            else
            {
                base.Draw();
            }
            addHistory(angle, position);
        }

        public virtual void OnSwing()
        {
        }

        public override void OnPressAction()
        {
            if ((_crouchStance && _jabStance && !_swinging) || (!_crouchStance && !_swinging && _swing < 0.1f))
            {
                if (!_jabStance || _allowJabMotion)
                {
                    _afterSwingCounter = 0f;
                    _pullBack = true;
                    _swung = true;
                    _shing = false;
                    _timeSinceSwing = 0f;
                    OnSwing();
                    if (_swingSound != null)
                    {
                        SFX.Play(_swingSound, Rando.Float(0.8f, 1f), Rando.Float(-0.1f, 0.1f));
                    }
                    if (!_jabStance)
                    {
                        _swordSwing.speed = 1f;
                        _swordSwing.frame = 0;
                    }
                }
            }
            else if (_crouchStance && !_jabStance && duck != null && !duck.grounded)
            {
                _slamStance = true;
            }
        }

        public override void Fire()
        {
        }
    }
}