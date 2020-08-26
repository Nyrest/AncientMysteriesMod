using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_None : AmmoType
    {
        public AT_None()
        {
            accuracy = 1f;
            range = 0f;
            penetration = 0f;
            rangeVariation = 0f;
            bulletThickness = 0f;
            bulletSpeed = 0f;
        }
    }
}
