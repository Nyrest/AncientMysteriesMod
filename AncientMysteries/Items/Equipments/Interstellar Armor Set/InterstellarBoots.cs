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
            // Not using ReadyToRun because we are going to use vanilla collide box and crop size
            graphic = _pickupSprite = this.ModSprite(tex_Equipment_InterstellarBoots_Pickup);
            _sprite = this.ModSpriteWithFrames(tex_Equipment_InterstellarBoots, 32, 32);
            CanCrush = false;
        }
    }
}
