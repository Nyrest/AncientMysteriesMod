using AncientMysteries.Armor;

namespace AncientMysteries.Items.Developers.Hats
{
    [EditorGroup(e_developer)]
    public sealed class GUDZH : AMHelmet
    {
        public GUDZH(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = this.ModSpriteMap(t_HatGUDZH, 32, 32, true);
            _pickupSprite = this.ReadyToRun(t_HatGUDZHPickup);
            EquipmentMaxHitPoints = 32767;
            EquipmentHitPoints = 32767;
            _isArmor = true;
            _equippedThickness = int.MaxValue;
        }

        public override string GetLocalizedName(AMLang lang)
        {
            throw new NotImplementedException();
        }
    }
}
