namespace AncientMysteries.Items
{
    [EditorGroup(group_Equipments)]
    [MetaImage(tex_Equipment_InterstellarBoots_Pickup)]
    [MetaInfo(Lang.Default, "Interstellar Boots", "A piece of armor left by a dead spaceship captain.")]
    [MetaInfo(Lang.schinese, "星际之靴", "一名死去的太空船船长留下的装备。")]
    [MetaType(MetaType.Equipment)]
    public partial class InterstellarBoots : AMBoots
    {
        public InterstellarBoots(float xpos, float ypos) : base(xpos, ypos)
        {
            // Not using ReadyToRun because we are going to use vanilla collide box and crop size
            graphic = _pickupSprite = this.ModSprite(tex_Equipment_InterstellarBoots_Pickup);
            _sprite = this.ModSpriteWithFrames(tex_Equipment_InterstellarBoots, 32, 32);
            CanCrush = false;
            _isArmor = true;
            _equippedThickness = float.MaxValue;
            _equipmentMaxHitPoints = 999;
            _equipmentHitPoints = 999;
        }
    }
}