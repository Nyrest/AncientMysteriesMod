using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries
{
    public static class AMath2
    {
        private static readonly Random rand = new Random();

        /// <summary>
        /// Negative value if random generated number is odd
        /// </summary>
        public static float RandomNegative(this float value) =>
            (rand.Next() & 1) == 0
                ? value
                : -value;
    }
}
