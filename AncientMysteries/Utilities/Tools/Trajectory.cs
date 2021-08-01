using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Utilities
{
    // Usage:
    // Create the instance
    // Bind the thing if you created this without new Trajectory(this);
    // Call Trajectory.Update in Thing.Update
    // Call Trajectory.Draw in Thing.Draw
#warning TODO
    public sealed class Trajectory
    {
        public Queue<Vec2> _tailQueue;
        public Thing thing;

        public float SegmentMinLength { get; init; } = 1;
        public float MaxSegments { get; init; } = 10;
        public float DistanceTraveled { get; private set; }
        public float CurrentSegmentsCount => DistanceTraveled / SegmentMinLength;
        public Color Color { get; init; }

        public Trajectory(Thing thing)
        {
            this.thing = thing;
        }

        public void Update()
        {
            if (_tailQueue.Count > MaxSegments)
            {
                _tailQueue.Dequeue();
            }
            else if (_tailQueue.Count < CurrentSegmentsCount)
            {
                if ((thing.position - _tailQueue.LastOrDefault()).lengthSq >= SegmentMinLength)
                    _tailQueue.Enqueue(thing.position);
            }
        }

        public void Draw()
        {
            if (_tailQueue.Count == 0) return;
            int count = _tailQueue.Count;
            Vec2 lastPos = thing.position;
            int cur = count;
            foreach (var pos in _tailQueue.Reverse())
            {
                float alpha = (cur--) / (float)count;
                //Graphics.DrawRect(new Rectangle(pos.x, pos.y, 2, 2), Color.Red);
                Graphics.DrawLine(lastPos, pos, Color * alpha);
                lastPos = pos;
            }
        }
    }

    public static class TrajectoryPool
    {

    }
}
