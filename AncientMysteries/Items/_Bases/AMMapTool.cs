using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    public abstract partial class AMMapTool : AMThing, IAMLocalizable
    {
        public AMMapTool(float xpos, float ypos) : base(xpos, ypos)
        {
        }

        public string GetLocalizedDescription(Lang lang) => string.Empty;

        public string GetLocalizedName(Lang lang) => string.Empty;
    }
}
