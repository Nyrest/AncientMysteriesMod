using AncientMysteries.Localization.Enums;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Armor.Developers.Hats
{
    [EditorGroup(e_developer)]
    public sealed class Thingy : AMHelmet
    {
        private static readonly FieldInfo fieldAmmoType = typeof(Gun).GetField("_ammoType", BindingFlags.Instance | BindingFlags.NonPublic);

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
            var d = equippedDuck;
            if (d is not null)
            {
                d.gravMultiplier = d.crouch ? 2f : 0.3f;
                if (d.gun != null)
                {
                    d.gun.infiniteAmmoVal = true;
                    if (d.gun._fireWait > 0.05f)
                        d.gun._fireWait = 0.05f;
                    // TODO: make full auto later
                    if (d.gun is TampingWeapon tampingWeapon)
                    {
                        tampingWeapon._tamped = true;
                    }
                    if (d.gun.ammoType is not ATMissile)
                    {
                        var oriAt = d.gun.ammoType;
                        fieldAmmoType.SetValue(d.gun, new ATMissile
                        {
                            range = oriAt.range,
                            rangeVariation = oriAt.rangeVariation,
                            rebound = oriAt.rebound,
                            accuracy = oriAt.accuracy,
                            airFrictionMultiplier = oriAt.airFrictionMultiplier,
                            canTeleport = oriAt.canTeleport,
                            canBeReflected = oriAt.canBeReflected,
                            speedVariation = oriAt.speedVariation,
                            barrelAngleDegrees = oriAt.barrelAngleDegrees,
                            bulletSpeed = oriAt.bulletSpeed,
                            bulletThickness = oriAt.bulletThickness,
                            bulletLength = oriAt.bulletLength,
                            bulletColor = oriAt.bulletColor,
                        });
                    }
                }

                float amount = 0.5f;
                if (!d.sliding && !d.immobilized && !d.moveLock)
                {
                    if (!base.equippedDuck.grounded)
                    {
                        amount = 0.25f;
                    }
                    if (d.inputProfile.Down("RIGHT") && d.hSpeed < 9f)
                    {
                        d.hSpeed = MathHelper.Lerp(d.hSpeed, 9f, amount);
                    }
                    if (d.inputProfile.Down("LEFT") && base.equippedDuck.hSpeed > -9f)
                    {
                        d.hSpeed = MathHelper.Lerp(d.hSpeed, -9f, amount);
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
