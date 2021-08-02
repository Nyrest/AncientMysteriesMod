using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Utilities
{
    public record class ColorTrajectory : TrajectoryBase
    {
        private Queue<Vec2> _segmentsQueue = new Queue<Vec2>();

        public float SegmentMinLength { get; init; } = 1;
        public float MaxSegments { get; init; } = 10;
        public float DistanceTraveled { get; private set; }
        public float CurrentSegmentsCount => DistanceTraveled / SegmentMinLength;
        public Color Color { get; set; } = Color.White;
        private Vec2 lastUpdatePosition;

        public ColorTrajectory(Thing thing)
        {
            Bind(thing);
        }

        public void Bind(Thing thing)
        {
            PositionProvider = () => thing.position;
            lastUpdatePosition = GetGetCurrentPosition();
        }

        public void Bind(Thing thing, Vec2 offsetLT)
        {
            PositionProvider = thing switch
            {
                Holdable => () => thing.Offset(offsetLT - thing.center + ((Holdable)thing)._extraOffset),
                _ => () => thing.Offset(offsetLT - thing.center),
            };
            lastUpdatePosition = GetGetCurrentPosition();
        }

        public override void Update()
        {
            Vec2 pos = GetGetCurrentPosition();
            DistanceTraveled += (pos - lastUpdatePosition).length;
            lastUpdatePosition = pos;
            if (_segmentsQueue.Count > MaxSegments)
            {
                _segmentsQueue.Dequeue();
            }
            else if (_segmentsQueue.Count < CurrentSegmentsCount)
            {
                if ((pos - _segmentsQueue.LastOrDefault()).lengthSq >= SegmentMinLength)
                    _segmentsQueue.Enqueue(pos);
            }
        }

        public override void Draw()
        {
            if (_segmentsQueue.Count == 0) return;
            int count = _segmentsQueue.Count;
            Vec2 lastPos = GetGetCurrentPosition();
            int cur = count;
            foreach (var pos in _segmentsQueue.Reverse())
            {
                float alpha = (cur--) / (float)count;
                //Graphics.DrawRect(new Rectangle(pos.x, pos.y, 2, 2), Color.Red);
                Graphics.DrawLine(lastPos, pos, Color * alpha);
                lastPos = pos;
            }
        }

        public Vec2 GetGetCurrentPosition() => PositionProvider();
    }
}
