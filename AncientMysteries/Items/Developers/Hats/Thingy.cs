using AncientMysteries.Localization.Enums;
using System.Reflection;

namespace AncientMysteries.Armor.Developers.Hats
{
    [EditorGroup(e_developer)]
    public sealed class Thingy : AMHelmet
    {
        private static readonly FieldInfo fieldAmmoType = typeof(Gun).GetField("_ammoType", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo fieldFullAuto = typeof(Gun).GetField("_fullAuto", BindingFlags.Instance | BindingFlags.NonPublic);

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
            _isArmor = true;
            _equippedThickness = int.MaxValue;
        }

        public override void Update()
        {
            base.Update();
            var d = equippedDuck;
            if (d is not null)
            {
                d.gravMultiplier = d.crouch ? 2f : 0.3f;
                if (d.gun is Gun gun)
                {
                    gun.infiniteAmmoVal = true;
                    gun.loaded = true;

                    if (gun._wait > 0.05f)
                        gun._wait = 0.05f;
                    if (gun is TampingWeapon tampingWeapon)
                    {
                        tampingWeapon._tamped = true;
                    }
                    if (!gun.fullAuto)
                    {
                        fieldFullAuto.SetValue(gun, true);
                    }
                    if (gun is OldPistol oldPistol)
                    {
                        oldPistol._loadState = -1;
                    }
                    if (gun.ammoType is not ATMissile)
                    {
                        var oriAt = gun.ammoType;
                        fieldAmmoType.SetValue(gun, new ATMissile
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
                    if (d.inputProfile.Down("RIGHT") && d.hSpeed is > 1f and < 9f)
                    {
                        d.hSpeed = MathHelper.Lerp(d.hSpeed, 9f, amount);
                    }
                    if (d.inputProfile.Down("LEFT") && d.hSpeed is < -1f and > -9f)
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
