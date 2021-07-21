using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_AbyssalGazer)]
    [MetaInfo(Lang.english, "Abyssal Gazer", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Magic)]
    public partial class AbyssalGazer : AMStaff
    {
        public AbyssalGazer(float xval, float yval) : base(xval, yval)
        {
            
        }
    }
}
