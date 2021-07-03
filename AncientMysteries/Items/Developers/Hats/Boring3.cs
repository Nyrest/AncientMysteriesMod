namespace AncientMysteries.Armor.Developers.Hats
{
    [EditorGroup(e_developer)]
    public sealed class Boring3 : AMHelmet, IPlatform
    {
        public static readonly FieldInfo feather_Sprite = typeof(Feather).GetField("_sprite", BindingFlags.NonPublic | BindingFlags.Instance);

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Boring3",
        };

        public Vec2 baseCollisionOffset, baseCollisionSize;

        public Boring3(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = this.ModSpriteMap("HatBoring3.png", 32, 32, true);
            _pickupSprite = this.ReadyToRun("HatBoring3Pickup.png");
            baseCollisionOffset = _collisionOffset;
            baseCollisionSize = _collisionSize;
            _equippedCollisionOffset = baseCollisionOffset - new Vec2(1, 1);
            _equippedCollisionSize = baseCollisionSize + new Vec2(2, 2); ;
            _hasEquippedCollision = true;
            MakeDefaultHitEffects = false;
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

            if (_equippedDuck != null && !destroyed)
            {
                _equippedCollisionOffset = duck.topLeft - this.position + new Vec2(-1.5f, -1.5f);
                if (duck.offDir == -1)
                    _equippedCollisionOffset.x -= duck.collisionOffset.x;
                _equippedCollisionSize = _equippedDuck.collisionSize + new Vec2(3, 3);
            }
            else
            {
                _equippedCollisionOffset = baseCollisionOffset - new Vec2(1, 1);
                _equippedCollisionSize = baseCollisionSize + new Vec2(2, 2); ;
            }

            if (_equippedDuck != null)
            {
                var rect = _equippedDuck.rectangle;
                bool foundFire = false;
                foreach (SmallFire item in Level.current.things[typeof(SmallFire)])
                {
                    if (item.stick == _equippedDuck)
                    {
                        item.stick = null;
                    }
                    if (Collision.Rect(rect, item.rectangle))
                    {
                        foundFire = true;
                    }
                }
                if (!foundFire)
                {
                    _equippedDuck.burnt = 0;
                    _equippedDuck.onFire = false;
                }
                var equippedDuckFeather = _equippedDuck.persona.featherSprite.texture;
                var equppedDuckColor = _equippedDuck.persona.colorUsable;
                foreach (var feather in Level.CheckCircleAll<Feather>(_equippedDuck.position, 50))
                {
                    var spriteMap = (SpriteMap)feather_Sprite.GetValue(feather);
                    if (spriteMap.texture == equippedDuckFeather)
                    {
                        var particleEnd = feather.position + -feather.velocity * 15;

                        for (int i2 = 0; i2 < 3; i2++)
                        {
                            Level.Add(DotParticle.New(
                                feather.x,
                                feather.y,
                                () => particleEnd,
                                HSL.RandomRGB(),
                                0.2f));
                        }
                        Level.Remove(feather);
                    }
                }
            }

            willHeat = false;
            heat = 0;
            _onFire = false;
            _equippedThickness = float.MaxValue;
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (_equippedDuck == null || bullet.owner == duck || !bullet.isLocal)
                return false;

            var particleEnd =
                hitPos
                + (bullet.travelDirNormalized * bullet.bulletSpeed)
                * 3
                + new Vec2(Rando.Float(-5f, 5f), Rando.Float(-5f, 5f));

            for (int i2 = 0; i2 < 3; i2++)
            {
                Level.Add(DotParticle.New(
                    hitPos.x + Rando.Float(-2f, 2f),
                    hitPos.y + Rando.Float(-2f, 2f),
                    () => particleEnd,
                    bullet.color.Add(new Color(
                        (byte)Rando.Int(-15, 15),
                        (byte)Rando.Int(-15, 15),
                        (byte)Rando.Int(-15, 15)
                        )),
                    0.2f));
            }
            SFX.Play("scimiSurge", 0.8f, Rando.Float(-0.2f, 0.2f));
            return true;
        }

        public override void Equip(Duck d)
        {
            base.Equip(d);
            d.invincible = true;
        }

        public override void UnEquip()
        {
            _equippedDuck.invincible = false;
            base.UnEquip();
        }
    }
}
