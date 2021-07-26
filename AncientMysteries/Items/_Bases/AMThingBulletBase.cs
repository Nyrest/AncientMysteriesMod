using AncientMysteries.DestroyTypes;

namespace AncientMysteries.Items
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
        public float BulletRange { get; init; }
        public bool BulletCanCollideWhenNotMoving { get; init; }
        public Color BulletTailColor { get; init; } = Color.White;
        public bool BulletTail { get; init; } = true;
        public float BulletTailSegmentMinLength { get; init; } = 1;
        public float BulletTailMaxSegments { get; init; } = 10;
        public float BulletDistanceTraveled { get; private set; }
        public float CurrentTailSegments => BulletDistanceTraveled / BulletTailSegmentMinLength;
        public readonly float BulletPenetration;
        public Vec2 lastPosition;

        public Queue<Vec2> _tailQueue;
        public HashSet<MaterialThing> _lastImpacting;
        public List<MaterialThing> _currentImpacting;

#if DEBUG

        [Obsolete("Use BulletSafeDuck", true)]
        public new object owner;

        [Obsolete("Use BulletSafeDuck", true)]
        public new object _owner;

#endif

        public virtual bool IsMoving => speed != Vec2.Zero;

        public AMThingBulletBase(Vec2 pos, float bulletRange, float bulletPenetration, Vec2 initSpeed, Duck safeDuck) : base(pos.x, pos.y)
        {
            ThingBulletPool.InitBullet(this);
            BulletSafeDuck = safeDuck;
            BulletRange = bulletRange;
            BulletPenetration = bulletPenetration;
            speed = initSpeed;
            angle = CalcBulletAngleRadian();
            lastPosition = pos;
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

            if (BulletTail)
            {
                if (_tailQueue.Count > BulletTailMaxSegments)
                {
                    _tailQueue.Dequeue();
                }
                else if (_tailQueue.Count < CurrentTailSegments)
                {
                    if ((position - _tailQueue.LastOrDefault()).lengthSq >= BulletTailSegmentMinLength)
                        _tailQueue.Enqueue(position);
                }
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
                Make.Bullet<AT_ThingBulletSimulation>(thing.position, BulletSafeDuck, angleDegrees, this);
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

        public float CalcBulletAngleDegrees() => CalcBulletAngleDegrees(speed);

        public float CalcBulletAngleRadian() => CalcBulletAngleRadian(speed);

        public float CalcBulletAngleDegrees(Vec2 speed) => Maths.RadToDeg(CalcBulletAngleRadian(speed));

        public virtual float CalcBulletAngleRadian(Vec2 speed) => -Maths.PointDirectionRad(Vec2.Zero, speed);

        public virtual void BulletRemove()
        {
            Level.Remove(this);
        }

        public override void Draw()
        {
            base.Draw();
            if (BulletTail && _tailQueue.Count != 0)
                DrawTail();
        }

        public virtual void DrawTail()
        {
            int count = _tailQueue.Count;
            Vec2 lastPos = position;
            int cur = count;
            foreach (var pos in _tailQueue.Reverse())
            {
                float alpha = (cur--) / (float)count;
                //Graphics.DrawRect(new Rectangle(pos.x, pos.y, 2, 2), Color.Red);
                Graphics.DrawLine(lastPos, pos, BulletTailColor * alpha);
                lastPos = pos;
            }
        }

        public virtual void GoTo(Thing thing) => GoTo(thing.position);
        public virtual void GoTo(Vec2 pos)
        {

        }
    }
}