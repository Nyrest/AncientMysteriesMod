using AncientMysteries.DestroyTypes;

namespace AncientMysteries.Bases
{
    // Warning
    // Angle is not network sync
    // Do not change angle if you don't know what are you fucking doing
    public abstract class AMThingBulletBase : AMThing, ITeleport
    {
        public StateBinding positionBinding = new CompressedVec2Binding(nameof(position));
        public StateBinding speedBinding = new CompressedVec2Binding(nameof(speed));
        public Vec2 speed;
        public StateBinding safeDuckBinding = new(nameof(BulletSafeDuck));
        public Duck BulletSafeDuck;
        public readonly float BulletRange;
        public readonly bool BulletCanCollideWhenNotMoving;
        public float BulletDistanceTraveled { get; private set; }
        public readonly float BulletPenetration;
        public Vec2 lastPosition;

#if DEBUG
        [Obsolete("Use BulletSafeDuck")]
        public new object owner;

        [Obsolete("Use BulletSafeDuck")]
        public new object _owner;
#endif

        public bool IsMoving => speed != Vec2.Zero;

        public AMThingBulletBase(Vec2 pos, float bulletRange, float bulletPenetration, Vec2 initSpeed, Duck safeDuck)
        {
            BulletSafeDuck = safeDuck;
            position = pos;
            BulletRange = bulletRange;
            BulletPenetration = bulletPenetration;
            speed = initSpeed;
            angle = CalcBulletAngleRadian();
            lastPosition = pos;
        }

        public override void Update()
        {
            base.Update();

            lastPosition = position;
            if (IsMoving)
            {
                position += speed;
                BulletDistanceTraveled += speed.Length();
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
        }

        public void DoBulletCollideCheck()
        {
            foreach (var item in BulletCollideCheck())
            {
                if (BulletCanDestory(item))
                {
                    item.Destroy(new DT_ThingBullet(this));
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

        public float CalcBulletAngleDegrees() => -Maths.PointDirection(Vec2.Zero, speed);

        public float CalcBulletAngleRadian() => -Maths.PointDirectionRad(Vec2.Zero, speed);

        public float CalcBulletAngleDegrees(Vec2 speed) => -Maths.PointDirection(Vec2.Zero, speed);

        public float CalcBulletAngleRadian(Vec2 speed) => -Maths.PointDirectionRad(Vec2.Zero, speed);

        public virtual void BulletRemove()
        {
            Level.Remove(this);
        }
    }
}
