using AncientMysteries.DestroyTypes;
using AncientMysteries.Utilities;
using static AncientMysteries.AmmoTypes.ThingBulletSimulation_AmmoType;

namespace AncientMysteries.Items
{
    // Warning
    // Angle is not network sync
    // Do not change angle if you don't know what are you fucking doing
    public abstract class AMThingBulletBase : AMThing, ITeleport
    {
        public StateBinding positionBinding = new CompressedVec2Binding(nameof(position));
        public StateBinding speedBinding = new CompressedVec2Binding(nameof(bulletVelocity));
        public Vec2 bulletVelocity;
        public StateBinding safeDuckBinding = new(nameof(BulletSafeDuck));
        public Duck BulletSafeDuck;
        public float BulletRange { get; init; }
        public bool BulletCanCollideWhenNotMoving { get; init; }
        public Color BulletTailColor { get; init; } = Color.White;
        public bool BulletTail { get; init; } = true;
        public float BulletTailSegmentMinLength { get; init; } = 1;
        public float BulletTailMaxSegments { get; init; } = 10;
        public float BulletDistanceTraveled { get; private set; }
        public float CurrentTailSegments => BulletDistanceTraveled / BulletTailSegmentMinLength;
        public float BulletPenetration { get; init; }
        public Vec2 lastPosition;

        public Trajectory trajectory;
        public HashSet<MaterialThing> _lastImpacting;
        public List<MaterialThing> _currentImpacting;

#if DEBUG

        [Obsolete("Use BulletSafeDuck", true)]
        public new object owner;

        [Obsolete("Use BulletSafeDuck", true)]
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
            angle = CalcBulletAngleRadian();
            lastPosition = pos;
            trajectory = new Trajectory(this)
            {
                Color = BulletTailColor,
            };
        }

        ~AMThingBulletBase()
        {
            ThingBulletPool.Recycle(this);
        }

        public override void Update()
        {
            base.Update();

            lastPosition = position;
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

            UpdateAngle();

            if (BulletDistanceTraveled > BulletRange)
            {
                BulletRemove();
            }
            if (BulletTail)
            {
                trajectory.MaxSegments = BulletTailMaxSegments;
                trajectory.SegmentMinLength = BulletTailSegmentMinLength;
                trajectory.Color = BulletTailColor;
                trajectory.Update();
            }
        }

        public void DoBulletCollideCheck()
        {
            foreach (var item in BulletCollideCheck())
            {
                if (BulletCanDestory(item))
                {
                    item.Destroy(new DT_ThingBullet(this));
                }
                if (_lastImpacting.Add(item))
                {
                    LegacyImpact(item);
                    _currentImpacting.Add(item);
                }
                if (item.thickness > BulletPenetration && item is not Teleporter)
                {
                    if (BulletCanHit(item))
                    {
                        BulletOnHit(item);
                        return;
                    }
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

        public virtual bool BulletCanDestory(MaterialThing thing)
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
                    thing != BulletSafeDuck.holdObject)
                    && thing is IAmADuck;
        }

        public virtual bool BulletCanHit(MaterialThing thing)
        {
            return true;
        }

        public virtual void BulletOnHit(MaterialThing thing)
        {
            BulletRemove();
        }

        public virtual void UpdateAngle()
        {
            if (IsMoving)
                angle = CalcBulletAngleRadian();
        }

        public float CalcBulletAngleDegrees() => CalcBulletAngleDegrees(bulletVelocity);

        public float CalcBulletAngleRadian() => CalcBulletAngleRadian(bulletVelocity);

        public float CalcBulletAngleDegrees(Vec2 speed) => Maths.RadToDeg(CalcBulletAngleRadian(speed));

        public virtual float CalcBulletAngleRadian(Vec2 speed) => -Maths.PointDirectionRad(Vec2.Zero, speed);

        public virtual void BulletRemove()
        {
            Level.Remove(this);
        }

        public override void Draw()
        {
            base.Draw();
            if (BulletTail)
                trajectory.Draw();
        }

        public void GoToByVelocity(Transform transform, float speed, float lerpAmount) => GoToByVelocity(transform.position, speed, lerpAmount);
        public void GoToByVelocity(Vec2 target, float speed, float lerpAmount)
        {
            float angleToTargetRad = Maths.PointDirectionRad(position, target);
            var vecToTarget = Maths.AngleToVec(angleToTargetRad) * speed;
            //vecToTarget.y *= -1;
            bulletVelocity = Vec2.Lerp(bulletVelocity, vecToTarget, lerpAmount);
        }

#warning GoToByAngle: Need to fix
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