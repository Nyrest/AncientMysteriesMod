using DuckGame;
using System;
using System.Collections.Generic;

namespace AncientMysteries.Items
{
    public abstract class AMMelee : AMGun
    {
        public StateBinding _swingBinding = new(doLerp: true, "_swing");

        public StateBinding _holdBinding = new(doLerp: true, "_hold");

        public StateBinding _jabStanceBinding = new("_jabStance");

        public StateBinding _crouchStanceBinding = new("_crouchStance");

        public StateBinding _slamStanceBinding = new("_slamStance");

        public StateBinding _pullBackBinding = new(doLerp: true, "_pullBack");

        public StateBinding _swingingBinding = new("_swinging");

        public StateBinding _throwSpinBinding = new(doLerp: true, "_throwSpin");

        public StateBinding _volatileBinding = new("_volatile");

        public StateBinding _addOffsetXBinding = new("_addOffsetX");

        public StateBinding _addOffsetYBinding = new("_addOffsetY");

        public float _swing;

        public float _hold;

        public bool _drawing;

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

        public SpriteMap _swordSwing;

        public int _unslam;

        public byte blocked;

        public bool _volatile;

        public List<float> _lastAngles = new();

        public List<Vec2> _lastPositions = new();

        public float _pitchOffset = 0;

        public override float angle
        {
            get
            {
                if (_drawing)
                {
                    return _angle;
                }
                return base.angle + (_swing + _hold) * (float)offDir;
            }
            set
            {
                _angle = value;
            }
        }

        public bool jabStance => _jabStance;

        public bool crouchStance => _crouchStance;

        public Vec2 barrelStartPos
        {
            get
            {
                if (owner == null)
                {
                    return position - (Offset(base.barrelOffset) - position).normalized * 6f;
                }
                if (_slamStance)
                {
                    return position + (Offset(base.barrelOffset) - position).normalized * 12f;
                }
                return position + (Offset(base.barrelOffset) - position).normalized * 2f;
            }
        }

        public AMMelee(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 4;
            _ammoType = new ATLaser
            {
                range = 170f,
                accuracy = 0.8f
            };
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
            _bouncy = 0.5f;
            _impactThreshold = 0.3f;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void CheckIfHoldObstructed()
        {
            Duck duckOwner = owner as Duck;
            if (duckOwner != null)
            {
                duckOwner.holdObstructed = false;
            }
        }

        public override void Thrown()
        {
        }

        public void Shing()
        {
            if (!_shing)
            {
                _pullBack = false;
                _swinging = false;
                _shing = true;
                _swingPress = false;
                if (!_playedShing)
                {
                    _playedShing = true;
                    SFX.Play("swordClash", Rando.Float(0.6f, 0.7f), Rando.Float(-0.1f, 0.1f) + _pitchOffset, Rando.Float(-0.1f, 0.1f));
                }
                Vec2 vec = (position - base.barrelPosition).normalized;
                Vec2 start = base.barrelPosition;
                for (int i = 0; i < 6; i++)
                {
                    Level.Add(Spark.New(start.x, start.y, new Vec2(Rando.Float(-1f, 1f), Rando.Float(-1f, 1f))));
                    start += vec * 4f;
                }
                _swung = false;
                _swordSwing.speed = 0f;
            }
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (base.duck != null)
            {
                if (blocked == 0)
                {
                    base.duck.AddCoolness(1);
                }
                else
                {
                    blocked++;
                    if (blocked > 4)
                    {
                        blocked = 1;
                        base.duck.AddCoolness(1);
                    }
                }
                SFX.Play("ting", 1, _pitchOffset);
                return base.Hit(bullet, hitPos);
            }
            return false;
        }

        public override void OnSoftImpact(MaterialThing with, ImpactedFrom from)
        {
            if (_wasLifted && owner == null && with is Block)
            {
                Shing();
                _framesSinceThrown = 15;
            }
        }

        public override void ReturnToWorld()
        {
            _throwSpin = 90f;
            collisionOffset = new Vec2(-2f, -16f);
            collisionSize = new Vec2(4f, 18f);
            if (_wasLifted)
            {
                collisionOffset = new Vec2(-4f, -2f);
                collisionSize = new Vec2(8f, 4f);
            }
        }

        public override void Update()
        {
            base.Update();
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
                _hold = -0.4f;
                _wasLifted = true;
                center = new Vec2(4f, 21f);
                _framesSinceThrown = 0;
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
                    base.angleDegrees = 90f + _throwSpin;
                    center = new Vec2(4f, 11f);
                }
                _volatile = false;
                bool spinning = false;
                bool againstWall = false;
                if (Math.Abs(hSpeed) + Math.Abs(vSpeed) > 2f || !base.grounded)
                {
                    if (!base.grounded)
                    {
                        Block b = Level.CheckRect<Block>(position + new Vec2(-6f, -6f), position + new Vec2(6f, -2f));
                        if (b != null)
                        {
                            againstWall = true;
                            if (vSpeed > 4f)
                            {
                                _volatile = true;
                            }
                        }
                    }
                    if (!againstWall && !_grounded)
                    {
                        IPlatform floor = Level.CheckPoint<IPlatform>(position + new Vec2(0f, 8f));
                        if (floor == null)
                        {
                            if (hSpeed > 0f)
                            {
                                _throwSpin += (Math.Abs(hSpeed) + Math.Abs(vSpeed)) * 2f + 4f;
                            }
                            else
                            {
                                _throwSpin -= (Math.Abs(hSpeed) + Math.Abs(vSpeed)) * 2f + 4f;
                            }
                            spinning = true;
                        }
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
                    else if (_throwSpin > 90f && _throwSpin < 270f)
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
                    (Offset(base.barrelOffset) - position).Normalize();
                    Offset(base.barrelOffset);
                    bool rebound = false;
                    foreach (AMMelee s3 in Level.current.things[typeof(AMMelee)])
                    {
                        if (s3 != this && s3.owner != null && s3._crouchStance && !s3._jabStance && !s3._jabStance && ((hSpeed > 0f && s3.x > base.x - 4f) || (hSpeed < 0f && s3.x < base.x + 4f)) && Collision.LineIntersect(barrelStartPos, base.barrelPosition, s3.barrelStartPos, s3.barrelPosition))
                        {
                            Shing();
                            s3.Shing();
                            s3.owner.hSpeed += (float)offDir * 1f;
                            s3.owner.vSpeed -= 1f;
                            rebound = true;
                            _hitWait = 4;
                            hSpeed = (0f - hSpeed) * 0.6f;
                        }
                    }
                    int waitFrames = 12;
                    if (!rebound)
                    {
                        foreach (Chainsaw s2 in Level.current.things[typeof(Chainsaw)])
                        {
                            if (s2.owner != null && s2.throttle && Collision.LineIntersect(barrelStartPos, base.barrelPosition, s2.barrelStartPos, s2.barrelPosition))
                            {
                                Shing();
                                s2.Shing(this);
                                s2.owner.hSpeed += (float)offDir * 1f;
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
                            Helmet helmetHit2 = Level.CheckLine<Helmet>(barrelStartPos, base.barrelPosition);
                            if (helmetHit2 != null && helmetHit2.equippedDuck != null && (helmetHit2.owner != base.prevOwner || _framesSinceThrown > waitFrames))
                            {
                                hSpeed = (0f - hSpeed) * 0.6f;
                                Shing();
                                rebound = true;
                                _hitWait = 4;
                            }
                            else
                            {
                                ChestPlate chestHit2 = Level.CheckLine<ChestPlate>(barrelStartPos, base.barrelPosition);
                                if (chestHit2 != null && chestHit2.equippedDuck != null && (chestHit2.owner != base.prevOwner || _framesSinceThrown > waitFrames))
                                {
                                    hSpeed = (0f - hSpeed) * 0.6f;
                                    Shing();
                                    rebound = true;
                                    _hitWait = 4;
                                }
                            }
                        }
                    }
                    if (!rebound && base.isServerForObject)
                    {
                        IEnumerable<IAmADuck> hit3 = Level.CheckLineAll<IAmADuck>(barrelStartPos, base.barrelPosition);
                        foreach (IAmADuck d3 in hit3)
                        {
                            if (d3 == base.duck)
                            {
                                continue;
                            }
                            MaterialThing realThing2 = d3 as MaterialThing;
                            if (realThing2 != null && (realThing2 != base.prevOwner || _framesSinceThrown > waitFrames))
                            {
                                realThing2.Destroy(new DTImpale(this));
                                if (Recorder.currentRecording != null)
                                {
                                    Recorder.currentRecording.LogBonus();
                                }
                            }
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
            if (base.isServerForObject)
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
                    if (base.duck != null)
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
                    else if (base.raised)
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
                    if (!_swinging && (!_swingPress || _shing || (_jabStance && _addOffsetX < 1f) || (!_jabStance && _swing < 1.6f)))
                    {
                        if (_jabStance)
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
                        else if (_slamStance)
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
                        else
                        {
                            _swing = MathHelper.Lerp(_swing, -0.22f, 0.36f);
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
                    if ((_swing < 0f || _jabStance) && _swing < 0f)
                    {
                        _swing = 0f;
                        _shing = false;
                        _swung = false;
                    }
                }
            }
            if (base.duck != null)
            {
                collisionOffset = new Vec2(-4f, 0f);
                collisionSize = new Vec2(4f, 4f);
                if (_crouchStance && !_jabStance)
                {
                    collisionOffset = new Vec2(-2f, -19f);
                    collisionSize = new Vec2(4f, 16f);
                    thickness = 3f;
                }
                _swingPress = false;
                if (!_pullBack && !_swinging)
                {
                    _crouchStance = false;
                    _jabStance = false;
                    if (base.duck.crouch)
                    {
                        if (!_pullBack && !_swinging && base.duck.inputProfile.Down((offDir > 0) ? "LEFT" : "RIGHT"))
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
                if (!_crouchStance)
                {
                    _hold = -0.4f;
                    handOffset = new Vec2(_addOffsetX, _addOffsetY);
                    _holdOffset = new Vec2(-4f + _addOffsetX, 4f + _addOffsetY);
                }
                else
                {
                    _hold = 0f;
                    _holdOffset = new Vec2(0f + _addOffsetX, 4f + _addOffsetY);
                    handOffset = new Vec2(3f + _addOffsetX, _addOffsetY);
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
                thickness = 0f;
            }
            if ((_swung || _swinging) && !_shing)
            {
                (Offset(base.barrelOffset) - position).Normalize();
                Offset(base.barrelOffset);
                IEnumerable<IAmADuck> hit2 = Level.CheckLineAll<IAmADuck>(barrelStartPos, base.barrelPosition);
                Block wallHit = Level.CheckLine<Block>(barrelStartPos, base.barrelPosition);
                if (wallHit != null && !_slamStance)
                {
                    if (offDir < 0 && wallHit.x > base.x)
                    {
                        wallHit = null;
                    }
                    else if (offDir > 0 && wallHit.x < base.x)
                    {
                        wallHit = null;
                    }
                }
                bool clashed = false;
                if (wallHit != null)
                {
                    Shing();
                    if (_slamStance)
                    {
                        _swung = false;
                        _unslam = 20;
                        owner.vSpeed = -5f;
                    }
                    if (wallHit is Window)
                    {
                        wallHit.Destroy(new DTImpact(this));
                    }
                }
                else if (!_jabStance && !_slamStance)
                {
                    Thing ignore = null;
                    if (base.duck != null)
                    {
                        ignore = base.duck.GetEquipment(typeof(Helmet));
                    }
                    Vec2 barrel = base.barrelPosition + base.barrelVector * 3f;
                    Vec2 p1 = new((position.x < barrel.x) ? position.x : barrel.x, (position.y < barrel.y) ? position.y : barrel.y);
                    Vec2 p2 = new((position.x > barrel.x) ? position.x : barrel.x, (position.y > barrel.y) ? position.y : barrel.y);
                    QuadLaserBullet laserHit = Level.CheckRect<QuadLaserBullet>(p1, p2);
                    if (laserHit != null)
                    {
                        Shing();
                        Fondle(laserHit);
                        laserHit.safeFrames = 8;
                        laserHit.safeDuck = base.duck;
                        Vec2 travel = laserHit.travel;
                        float mag = travel.length;
                        float mul = 1f;
                        if (offDir > 0 && travel.x < 0f)
                        {
                            mul = 1.5f;
                        }
                        else if (offDir < 0 && travel.x > 0f)
                        {
                            mul = 1.5f;
                        }
                        travel = (laserHit.travel = ((offDir > 0) ? new Vec2(mag * mul, 0f) : new Vec2((0f - mag) * mul, 0f)));
                    }
                    else
                    {
                        Helmet helmetHit = Level.CheckLine<Helmet>(barrelStartPos, base.barrelPosition, ignore);
                        if (helmetHit != null && helmetHit.equippedDuck != null)
                        {
                            Shing();
                            helmetHit.owner.hSpeed += (float)offDir * 3f;
                            helmetHit.owner.vSpeed -= 2f;
                            helmetHit.duck.crippleTimer = 1f;
                            helmetHit.Hurt(0.53f);
                            clashed = true;
                        }
                        else
                        {
                            if (base.duck != null)
                            {
                                ignore = base.duck.GetEquipment(typeof(ChestPlate));
                            }
                            ChestPlate chestHit = Level.CheckLine<ChestPlate>(barrelStartPos, base.barrelPosition, ignore);
                            if (chestHit != null && chestHit.equippedDuck != null)
                            {
                                Shing();
                                chestHit.owner.hSpeed += (float)offDir * 3f;
                                chestHit.owner.vSpeed -= 2f;
                                chestHit.duck.crippleTimer = 1f;
                                chestHit.Hurt(0.53f);
                                clashed = true;
                            }
                        }
                    }
                }
                if (!clashed)
                {
                    foreach (AMMelee s in Level.current.things[typeof(AMMelee)])
                    {
                        if (s != this && s.duck != null && !_jabStance && !s._jabStance && base.duck != null && Collision.LineIntersect(barrelStartPos, base.barrelPosition, s.barrelStartPos, s.barrelPosition))
                        {
                            Shing();
                            s.Shing();
                            s.owner.hSpeed += (float)offDir * 3f;
                            s.owner.vSpeed -= 2f;
                            base.duck.hSpeed += (float)(-offDir) * 3f;
                            base.duck.vSpeed -= 2f;
                            s.duck.crippleTimer = 1f;
                            base.duck.crippleTimer = 1f;
                            clashed = true;
                        }
                    }
                }
                if (clashed)
                {
                    return;
                }
                foreach (IAmADuck d2 in hit2)
                {
                    if (d2 != base.duck)
                    {
                        (d2 as MaterialThing)?.Destroy(new DTImpale(this));
                    }
                }
            }
            else
            {
                if (!_crouchStance || base.duck == null)
                {
                    return;
                }
                IEnumerable<IAmADuck> hit = Level.CheckLineAll<IAmADuck>(barrelStartPos, base.barrelPosition);
                foreach (IAmADuck d in hit)
                {
                    if (d == base.duck)
                    {
                        continue;
                    }
                    MaterialThing realThing = d as MaterialThing;
                    if (realThing == null)
                    {
                        continue;
                    }
                    if (realThing.vSpeed > 0.5f && realThing.bottom < position.y - 8f && realThing.left < base.barrelPosition.x && realThing.right > base.barrelPosition.x)
                    {
                        realThing.Destroy(new DTImpale(this));
                    }
                    else if (!_jabStance && !realThing.destroyed && ((offDir > 0 && realThing.x > base.duck.x) || (offDir < 0 && realThing.x < base.duck.x)))
                    {
                        if (realThing is Duck)
                        {
                            (realThing as Duck).crippleTimer = 1f;
                        }
                        else if ((base.duck.x > realThing.x && realThing.hSpeed > 1.5f) || (base.duck.x < realThing.x && realThing.hSpeed < -1.5f))
                        {
                            realThing.Destroy(new DTImpale(this));
                        }
                        Fondle(realThing);
                        realThing.hSpeed = (float)offDir * 3f;
                        realThing.vSpeed = -2f;
                    }
                }
            }
        }

        public override void Draw()
        {
            _playedShing = false;
            if (_swordSwing.speed > 0f)
            {
                if (base.duck != null)
                {
                    _swordSwing.flipH = ((base.duck.offDir <= 0) ? true : false);
                }
                _swordSwing.alpha = 0.4f;
                _swordSwing.position = position;
                _swordSwing.depth = base.depth + 1;
                _swordSwing.Draw();
            }
            base.alpha = 1f;
            Vec2 pos = position;
            Depth d = base.depth;
            graphic.color = Color.White;
            if ((owner == null && base.velocity.length > 1f) || _swing != 0f)
            {
                float rlAngle = angle;
                _drawing = true;
                float a = _angle;
                angle = rlAngle;
                for (int i = 0; i < 7; i++)
                {
                    base.Draw();
                    if (_lastAngles.Count > i)
                    {
                        _angle = _lastAngles[i];
                    }
                    if (_lastPositions.Count <= i)
                    {
                        break;
                    }
                    position = _lastPositions[i];
                    if (owner != null)
                    {
                        position += owner.velocity;
                    }
                    base.depth -= 2;
                    base.alpha -= 0.15f;
                    graphic.color = Color.Red;
                }
                position = pos;
                base.depth = d;
                base.alpha = 1f;
                _angle = a;
                base.xscale = 1f;
                _drawing = false;
            }
            else
            {
                base.Draw();
            }
            _lastAngles.Insert(0, angle);
            _lastPositions.Insert(0, position);
            if (_lastAngles.Count > 2)
            {
                _lastAngles.Insert(0, (_lastAngles[0] + _lastAngles[2]) / 2f);
                _lastPositions.Insert(0, (_lastPositions[0] + _lastPositions[2]) / 2f);
            }
            if (_lastAngles.Count > 8)
            {
                _lastAngles.RemoveAt(_lastAngles.Count - 1);
            }
            if (_lastPositions.Count > 8)
            {
                _lastPositions.RemoveAt(_lastPositions.Count - 1);
            }
        }

        public override void OnPressAction()
        {
            if ((_crouchStance && _jabStance && !_swinging) || (!_crouchStance && !_swinging && _swing < 0.1f))
            {
                _pullBack = true;
                _swung = true;
                _shing = false;
                SFX.Play("swipe", Rando.Float(0.8f, 1f), Rando.Float(-0.1f, 0.1f) + _pitchOffset);
                if (!_jabStance)
                {
                    _swordSwing.speed = 1f;
                    _swordSwing.frame = 0;
                }
            }
            else if (_crouchStance && !_jabStance && base.duck != null && !base.duck.grounded)
            {
                _slamStance = true;
            }
        }

        public override void Fire()
        {
        }
    }
}
