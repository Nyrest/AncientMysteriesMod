using AncientMysteries.Armor;

namespace AncientMysteries.Items.Developers.Hats
{
    [EditorGroup(e_developer)]
    public sealed class GUDZH : AMHelmet
    {
        

        public GUDZH(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = this.ModSpriteMap(t_Hat_GUDZH, 32, 32, true);
            _pickupSprite = this.ReadyToRun(t_Hat_GUDZHPickup);
            _sprite.CenterOrigin();
            EquipmentMaxHitPoints = 32767;
            EquipmentHitPoints = 32767;
            _isArmor = true;
            _equippedThickness = int.MaxValue;
        }

        public override void Update()
        {
            base.Update();
            this.scale = Vec2.One;
            if (owner is Duck duck)
            {
                if (duck.quack != 0)
                {
                    this.scale = new Vec2(2);
                }
            }
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (_equippedDuck == null || bullet.owner == duck || !bullet.isLocal)
                return false;

            if (bullet.ammo is null) return false;
            var result = base.Hit(bullet, hitPos);
            if (result)
            {
                bullet.ReverseTravel();
            }
            return result;
        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "GUDZH",
        };
    }
}
