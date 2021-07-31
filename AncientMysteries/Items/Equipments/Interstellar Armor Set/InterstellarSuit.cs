using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    [EditorGroup(group_Equipments)]
    [MetaImage(tex_Equipment_InterstellarSuit_Pickup)]
    [MetaInfo(Lang.Default, "Interstellar Suit", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Equipment)]
    public partial class InterstellarSuit : AMChestPlate
    {
        public InterstellarSuit(float xpos, float ypos) : base(xpos, ypos)
        {
            // Not using ReadyToRun because we are going to use vanilla collide box and crop size
            graphic = _pickupSprite = this.ModSprite(tex_Equipment_InterstellarSuit_Pickup);
            _sprite = this.ModSpriteWithFrames(tex_Equipment_InterstellarSuit_Anim, 32, 32);
            _spriteOver = this.ModSpriteWithFrames(tex_Equipment_InterstellarSuit_AnimOver,32,32);
            CanCrush = false;
            _isArmor = true;
            _equipmentMaxHitPoints = 999;
            _equipmentHitPoints = 999;
            _equippedThickness = float.MaxValue;
        }
    }
}
