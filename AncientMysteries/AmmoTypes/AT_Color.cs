using AncientMysteries.Bullets;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Color : AmmoType
    {
        public AT_Color(Color color)
        {
            bulletColor = color;
            accuracy = 0.9f;
            range = 500f;
            penetration = 2f;
            rangeVariation = 10f;
            combustable = true;
        }
    }
}
