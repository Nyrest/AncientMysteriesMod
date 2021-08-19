using AncientMysteries.DestroyTypes;
using static AncientMysteries.AmmoTypes.ThingBulletSimulation_AmmoType;

namespace AncientMysteries.Items
{
    // Warning
    // Angle is not network sync
    // Do not change angle if you don't know what are you fucking doing
    public abstract class AMThingBulletBase : AMThing, ITeleport
    {
        public StateBinding positionBinding = new(GhostPriority.High, nameof(position));
        public StateBinding bulletVelocityBinding = new(GhostPriority.High, nameof(bulletVelocity));
        public StateBinding safeDuckBinding = new(nameof(BulletSafeDuck));

        public Vec2 bulletVelocity;
        public Vec2 initVelocity;
        public Duck BulletSafeDuck;
        public float BulletRange { get; init; }
        public bool BulletCanCollideWhenNotMoving { get; init; }
        public float BulletDistanceTraveled { get; private set; }
        public float BulletPenetration { get; init; }
        public bool BulletAutoAngle { get; init; } = true;
        public Vec2 LastPosition { get; protected set; }


        public bool GravityEnabled { get; init; } = false;
        public float GravityIncrement { get; init; } = 0.05f;
        public float GravityMax { get; init; } = 1;
        public float GravityCurrent { get; set; } = 0;
        public bool GravityReversed { get; init; } = false;

        public ColorTrajectory Trajectory { get; private set; }
        public HashSet<MaterialThing> _lastImpacting;
        public List<MaterialThing> _currentImpacting;

#if DEBUG

        [Obsolete($"Use {nameof(BulletSafeDuck)}", true)]
        public new object owner;

        [Obsolete($"Use {nameof(BulletSafeDuck)}", true)]
        public new object _owner;

#endif

        public virtual bool IsMoving => bulletVelocity != Vec2.Zero;

        public AMThingBulletBase(Vec2 pos, float bulletRange, float bulletPenetration, Vec2 initSpeed, Duck safeDuck) : base(pos.x, pos.y)
        {
            ThingBulletPool.InitBullet(this);
            BulletSafeDuck = safeDuck;
            BulletRange = bulletRange;
            BulletPenetration = bulletPenetration;
            bulletVelocity = initSpeed;
            initVelocity = initSpeed;
            if (BulletAutoAngle)
            {
                angle = CalcBulletAutoAngleRadian();
            }
            LastPosition = pos;
            Trajectory = GetTrajectory();
        }

        public virtual ColorTrajectory GetTrajectory() => new(this);

        ~AMThingBulletBase()
        {
            ThingBulletPool.Recycle(this);
        }

        public override void Update()
        {
            base.Update();

            LastPosition = position;
            if (IsMoving)
            {
                position += bulletVelocity;
                BulletDistanceTraveled += bulletVelocity.Length();
                DoBulletCollideCheck();
            }
            else if (BulletCanCollideWhenNotMoving)
            {
                DoBulletCollideCheck();
            }

            if (BulletAutoAngle)
                AutoUpdateAngle();

            if (BulletDistanceTraveled > BulletRange)
            {
                BulletRemove();
            }
            Trajectory?.Update();

            if (GravityEnabled)
            {
                if (!GravityReversed)
                {
                    MathHelper.Clamp(GravityCurrent += GravityIncrement, 0, GravityMax);
                    bulletVelocity = new(initVelocity.x, initVelocity.y + GravityCurrent);
                }
                else
                {
                    MathHelper.Clamp(GravityCurrent -= GravityIncrement, -GravityMax, 0);
                    bulletVelocity = new(initVelocity.x, initVelocity.y - GravityCurrent);
                }
            }
        }

        public void DoBulletCollideCheck()
        {
            foreach (var item in BulletCollideCheck())
            {
                if (!BulletCanHit(item)) continue;
                if (_lastImpacting.Add(item))
                {
                    LegacyImpact(item);
                    bool willStop = false;
                    BulletOnHit(item, ref willStop);
                    if (willStop)
                    {
                        BulletRemove();
                        break;
                    }
                    _currentImpacting.Add(item);
                }
                if (item.thickness > BulletPenetration && item is not Teleporter)
                {
                    bool willStop = true;
                    BulletOnHit(item, ref willStop);
                    if (willStop)
                    {
                        BulletRemove();
                        break;
                    }
                    return;
                }
            }
            if (_currentImpacting.Count != 0)
            {
                _lastImpacting.RemoveWhere(x => !_currentImpacting.Contains(x));
                _currentImpacting.RemoveAll(x => !_lastImpacting.Contains(x));
            }
        }

        public virtual void LegacyImpact(MaterialThing thing)
        {
            // local only
            // do not sync
            if (isServerForObject)
            {
                var bullet = (ThingBulletSimulation_Bullet)Make.Bullet<ThingBulletSimulation_AmmoType>(thing.position, BulletSafeDuck, angleDegrees, this);
                bullet.callback = this;
            }
        }

        /// <summary>
        /// Absolutely not Network sync
        /// </summary>
        public virtual void LegacyRebound(Vec2 pos, float dirDegress, float rangeLeft)
        {
        }

        public abstract IEnumerable<MaterialThing> BulletCollideCheck();

        public virtual bool BulletCanHit(MaterialThing thing)
        {
            /*
            if (BulletSafeDuck is not null &&
                    (thing == BulletSafeDuck ||
                    BulletSafeDuck.ExtendsTo(thing) ||
                    thing == BulletSafeDuck.holdObject)
                )
            {
                return false;
            }
            if (thing is IAmADuck) return true;
            return false;
            */
            return (BulletSafeDuck is null ||
                    thing != BulletSafeDuck &&
                    !BulletSafeDuck.ExtendsTo(thing) &&
                    thing != BulletSafeDuck.holdObject);
        }

        public virtual void BulletOnHit(MaterialThing thing, ref bool willStop)
        {

        }

        public virtual void AutoUpdateAngle()
        {
            if (IsMoving)
                angleDegrees = CalcBulletAutoAngleDegrees();
        }

        public float CalcBulletAutoAngleDegrees() => CalcBulletAutoAngleDegrees(bulletVelocity);

        public float CalcBulletAutoAngleRadian() => CalcBulletAutoAngleRadian(bulletVelocity);

        public float CalcBulletAutoAngleDegrees(Vec2 speed) => Maths.RadToDeg(CalcBulletAutoAngleRadian(speed));

        public virtual float CalcBulletAutoAngleRadian(Vec2 speed) => -Maths.PointDirectionRad(Vec2.Zero, new Vec2(speed.x, speed.y + GravityCurrent));

        public virtual void BulletRemove()
        {
            Level.Remove(this);
        }

        public override void Draw()
        {
            base.Draw();
            Trajectory?.Draw();
        }

        public void GoToByVelocity(Transform transform, float speed, float lerpAmount) => GoToByVelocity(transform.position, speed, lerpAmount);

        public void GoToByVelocity(Vec2 target, float speed, float lerpAmount)
        {
            float angleToTargetRad = Maths.PointDirectionRad(position, target);
            var vecToTarget = Maths.AngleToVec(angleToTargetRad) * speed;
            //vecToTarget.y *= -1;
            bulletVelocity = Vec2.Lerp(bulletVelocity, vecToTarget, lerpAmount);
        }

        [Obsolete("Need to fix")]
        /// <summary>
        /// Call this after base.Update();
        /// </summary>
        public void GoToByAngle(Transform transform, float speed, float lerpAmount) => GoToByAngle(transform.position, speed, lerpAmount);

        [Obsolete("Need to fix")]
        /// <summary>
        /// Call this after base.Update();
        /// </summary>
        public void GoToByAngle(Vec2 target, float speed, float lerpAmount)
        {
            float angleToTargetRad = Maths.PointDirectionRad(position, target);

            var lerpedAngleRad = MathHelper.Lerp(angle, angleToTargetRad, lerpAmount);
            bulletVelocity = Maths.AngleToVec(lerpedAngleRad) * speed;
            //var vecToTarget = Maths.AngleToVec(angleToTargetRad) * speed;

            //bulletVelocity = Vec2.Lerp(bulletVelocity, vecToTarget, speed);
        }
    }
}
