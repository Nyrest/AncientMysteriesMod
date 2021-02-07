using AncientMysteries.Localization.Enums;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Armor.Developers.Hats
{
    [EditorGroup(e_developer)]
    public sealed class Thingy : AMHelmet
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Devastator",
        };

        public Thingy(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = this.ModSpriteMap("hatHatty.png", 32, 32, true);
            _pickupSprite = this.ReadyToRun("hatHattyPickup.png");
            EquipmentMaxHitPoints = 32767;
            EquipmentHitPoints = 32767;
            _isArmor = false;
            _equippedThickness = 1000;
        }

        public override void Update()
        {
            base.Update();
            var equipped = equippedDuck;
            if (base.equippedDuck != null)
            {
                equippedDuck.gravMultiplier = equipped.crouch ? 2f : 0.3f;
                if (equippedDuck.gun != null)
                    equippedDuck.gun.infiniteAmmoVal = true;
                float amount = 0.5f;
                if (!base.equippedDuck.sliding && !base.equippedDuck.immobilized && !base.equippedDuck.moveLock)
                {
                    if (!base.equippedDuck.grounded)
                    {
                        amount = 0.25f;
                    }
                    if (base.equippedDuck.inputProfile.Down("RIGHT") && base.equippedDuck.hSpeed < 9f)
                    {
                        base.equippedDuck.hSpeed = MathHelper.Lerp(base.equippedDuck.hSpeed, 9f, amount);
                    }
                    if (base.equippedDuck.inputProfile.Down("LEFT") && base.equippedDuck.hSpeed > -9f)
                    {
                        base.equippedDuck.hSpeed = MathHelper.Lerp(base.equippedDuck.hSpeed, -9f, amount);
                    }
                }
            }
        }

        public override void Equip(Duck d)
        {
            base.Equip(d);
        }

        public override void UnEquip()
        {
            if (equippedDuck != null)
                equippedDuck.gravMultiplier = 1f;
            base.UnEquip();
        }
    }
}
