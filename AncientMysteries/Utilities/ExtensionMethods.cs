using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Utilities
{
    public static class ExtensionMethods
    {
        public static Color Add(this in Color color, in Color with, byte alpha = 255)
        {
            return new Color(color.r + with.r, color.g + with.g, color.b + with.b);
        }
    }
}
