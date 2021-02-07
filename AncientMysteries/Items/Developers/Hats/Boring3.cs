﻿using AncientMysteries.Localization.Enums;
using DuckGame;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Armor.Developers.Hats
{
    [EditorGroup(e_developer)]
    public sealed class Boring3 : AMHelmet
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Boring3",
        };

        public Boring3(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = this.ModSpriteMap("HatBoring3.png", 32, 32, true);
            _pickupSprite = this.ReadyToRun("HatBoring3Pickup.png");
            CanCrush = false;
            Destroyable = false;
            bulletThroughNotEquipped = false;
            EquipmentMaxHitPoints = 32767;
            EquipmentHitPoints = 32767;
            _isArmor = true;
            _equippedThickness = float.MaxValue;
        }

        public override void Update()
        {
            base.Update();
            willHeat = false;
            heat = 0;
            _onFire = false;
            _equippedThickness = float.MaxValue;
        }
    }
}
