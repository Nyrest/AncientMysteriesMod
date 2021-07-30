using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    [EditorGroup(group_Equipments)]
    [MetaImage(tex_Equipment_InterstellarBoots_Pickup)]
    [MetaInfo(Lang.english, "Interstellar Boots", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Equipment)]
    public partial class InterstellarBoots : AMBoots
    {
        public InterstellarBoots(float xpos, float ypos) : base(xpos, ypos)
        {
            _pickupSprite = this.ReadyToRun(tex_Equipment_InterstellarBoots_Pickup);
            _sprite = this.ReadyToRunWithFrames(tex_Equipment_InterstellarBoots, 32, 32);
            CanCrush = false;
        }
    }
}
