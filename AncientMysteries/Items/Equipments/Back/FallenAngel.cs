using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items.Equipments.Back
{
    [EditorGroup(e_developer)]
#warning Texture Todo
    [MetaImage(t_Bullet_NovaFrame)]
    [MetaInfo(Lang.english, "Fallen Angel", "感謝と祈りを忘れぬ限り、神は我々をお救い下さいます　私とともに、祈りを捧げましょう.")]
    [MetaInfo(Lang.schinese, "堕落天使", null)]
    public partial class FallenAngel : AMEquipmentWing
    {
        public FallenAngel(float xpos, float ypos) : base(xpos, ypos)
        {
        }
    }
}
