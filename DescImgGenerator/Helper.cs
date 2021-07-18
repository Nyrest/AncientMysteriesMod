using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DescImgGenerator
{
    public static class Helper
    {
        public static SKRect crect(float left, float top, float width, float height) => new(left, top, left + width, top + height);
    }
}
