using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    public abstract class AMMapToolBlock : Block, IAMLocalizable
    {
        public AMMapToolBlock(float x, float y) : base(x, y)
        {
        }

        public abstract string GetLocalizedDescription(Lang lang);
        public abstract string GetLocalizedName(Lang lang);
    }
}
