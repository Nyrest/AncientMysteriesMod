using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Utilities
{
    public static class DuckHelper
    {
        public static bool TryGetNearest<TThing>(
            this Vec2 position,
            [NotNullWhen(true)] out TThing nearestThing,
            Func<TThing, bool> condition = null,
            float maxRange = float.PositiveInfinity)
            where TThing : Thing
        {
            nearestThing = null;
            foreach (TThing thing in Level.current.things[typeof(TThing)])
            {
                float range = (position - thing.position).length;
                if (range < maxRange && condition(thing))
                {
                    maxRange = range;
                    nearestThing = thing;
                }
            }
            return nearestThing is not null;
        }
    }
}
