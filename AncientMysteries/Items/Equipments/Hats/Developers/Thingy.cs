namespace AncientMysteries.Items
{
    [EditorGroup(group_Equipment_Developer)]
    [MetaImage(tex_Hat_DevastatorPickup)]
    [MetaInfo(Lang.Default, "Devastator", "Tastes great.\nMain Artist / Programmer / Lead Translater ")]
    [MetaInfo(Lang.schinese, null, "好吃。\n主画师 / 程序员 / 主翻译 ")]
    [MetaOrder(int.MaxValue - 2)]
    [MetaType(MetaType.Developer)]
    public sealed partial class Thingy : AMHelmet
    {
        private static readonly FieldInfo fieldAmmoType = typeof(Gun).GetField("_ammoType", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly FieldInfo fieldFullAuto = typeof(Gun).GetField("_fullAuto", BindingFlags.Instance | BindingFlags.NonPublic);
        public static readonly List<Gun> bindedSpawnedGuns = new();

        public Thingy(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = this.ModSpriteWithFrames(tex_Hat_Devastator, 32, 32, true);
            _pickupSprite = this.ReadyToRun(tex_Hat_DevastatorPickup);
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
                if (d.quack != 0 && d.holdObject is null)
                {
                    var cosmicGun = new CosmicDisruption(duck.x, duck.y);
                    bindedSpawnedGuns.Add(cosmicGun);
                    Level.Add(cosmicGun);
                    duck.GiveHoldable(cosmicGun);
                }
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

                if (d.holdObject is AMStaff staff)
                {
                    staff._castSpeed = 1f;
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
            if (isServerForObject)
            {
                List<Gun> toRemove = null;
                foreach (var item in bindedSpawnedGuns)
                {
                    if (d is null || d.holdObject != item)
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
        }

        public override void UnEquip()
        {
            if (equippedDuck != null)
                equippedDuck.gravMultiplier = 1f;
            base.UnEquip();
        }
    }
}