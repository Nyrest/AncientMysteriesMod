﻿namespace AncientMysteries.Items
{
    [EditorGroup(group_Equipments)]
    [MetaImage(tex_Equipment_InterstellarVisage_Pickup)]
    [MetaInfo(Lang.Default, "Interstellar Visage", "A piece of armor left by a dead spaceship captain.")]
    [MetaInfo(Lang.schinese, "星际头盔", "一名死去的太空船船长留下的装备。")]
    [MetaType(MetaType.Equipment)]
    public partial class InterstellarVisage : AMHelmet
    {
        public InterstellarVisage(float xpos, float ypos) : base(xpos, ypos)
        {
            // Not using ReadyToRun because we are going to use vanilla collide box and crop size
            graphic = _pickupSprite = this.ModSprite(tex_Equipment_InterstellarVisage_Pickup);
            _sprite = this.ModSpriteWithFrames(tex_Equipment_InterstellarVisage, 32, 32);
            CanCrush = false;
            _isArmor = true;
            _equipmentMaxHitPoints = 999;
            _equipmentHitPoints = 999;
            _equippedThickness = float.MaxValue;
        }
    }
}