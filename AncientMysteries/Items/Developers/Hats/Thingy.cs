using System.Collections.Generic;
using AncientMysteries.Items.Sucks;

namespace AncientMysteries.Armor.Developers.Hats
{
    [EditorGroup(e_developer)]
    public sealed class Thingy : AMHelmet
    {
        private static readonly FieldInfo fieldAmmoType = typeof(Gun).GetField("_ammoType", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo fieldFullAuto = typeof(Gun).GetField("_fullAuto", BindingFlags.Instance | BindingFlags.NonPublic);
        public static readonly List<Gun> bindedSpawnedGuns = new();

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Devastator",
        };

        public Thingy(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = this.ModSpriteMap(t_Hat_Devastator, 32, 32, true);
            _pickupSprite = this.ReadyToRun(t_Hat_DevastatorPickup);
            EquipmentMaxHitPoints = 32767;
            EquipmentHitPoints = 32767;
            _isArmor = true;
            Destroyable = false;
            _equippedThickness = int.MaxValue;
        }

        public override void Update()
        {
            base.Update();
            var d = equippedDuck;
            if (d is not null)
            {
                d.lives = 1;
                d.gravMultiplier = d.crouch ? 2f : 0.2f;
                if (d.gun is Gun gun)
                {
                    gun.infiniteAmmoVal = true;
                    gun.loaded = true;
                    if (gun._wait > 0.03f)
                        gun._wait = 0.03f;
                    if (!gun.fullAuto)
                    {
                        fieldFullAuto.SetValue(gun, true);
                    }
                    if (gun is TampingWeapon tampingWeapon)
                    {
                        tampingWeapon._tamped = true;
                    }
                    else if (gun is OldPistol oldPistol)
                    {
                        oldPistol._loadState = -1;
                    }
                    if (gun is not CosmicDisruption && gun.ammoType is not ATMissile)
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
                    if (!equippedDuck.grounded)
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
            List<Gun> toRemove = null;
            foreach (var item in bindedSpawnedGuns)
            {
                if (owner is null || item.owner != owner)
                {
                    Level.Remove(item);
                    if (toRemove is null)
                    {
                        toRemove = new List<Gun>(bindedSpawnedGuns.Count);
                    }
                    toRemove.Add(item);
                }
            }
            if (toRemove != null)
            {
                foreach (var item in toRemove)
                {
                    bindedSpawnedGuns.Remove(item);
                }
            }
        }

        public override void Quack(float volume, float pitch)
        {
            base.Quack(volume, pitch);
            if (owner is Duck duck && duck.holdObject is null)
            {
                var gun = new CosmicDisruption(duck.x, duck.y);
                bindedSpawnedGuns.Add(gun);
                duck.GiveHoldable(gun);
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
