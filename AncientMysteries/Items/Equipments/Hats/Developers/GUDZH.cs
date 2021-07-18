namespace AncientMysteries.Equipments.Hats.Developers
{
    [EditorGroup(group_Equipment_Developer)]
    [MetaImage(tex_Hat_GUDZHPickup)]
    [MetaInfo(Lang.english, "GUDZH", "Eating now... Really. There's nothing to say.")]
    [MetaInfo(Lang.schinese, null, "我在吃饭。真的。没什么好说的。")]
    [MetaOrder(int.MaxValue)]
    public sealed partial class GUDZH : AMHelmet
    {
        public static Vec2 textureSize;

        public GUDZH(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = this.ModSpriteWithFrames(tex_Hat_GUDZH, 32, 32, true);
            _pickupSprite = this.ReadyToRun(tex_Hat_GUDZHPickup);
            //_sprite.CenterOrigin();
            EquipmentMaxHitPoints = 32767;
            EquipmentHitPoints = 32767;
            _isArmor = true;
            _equippedThickness = int.MaxValue;
        }

        public override void Update()
        {
            base.Update();
            if (owner is Duck duck)
            {
                this.scale = Vec2.One;
                float scale = 1;
                if (duck.quack != 0)
                {
                    scale = 2;
                }
                this.scale = new Vec2(scale);
                float w = 18 * scale, h = 18 * scale;
                this.collisionOffset = -(new Vec2(w / 2, h / 2)) + new Vec2(2.5f, 5) * scale;
                this.collisionSize = new Vec2(w, h);
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

        public override string GetLocalizedName(Lang lang) => lang switch
        {
            _ => "GUDZH",
        };

        public override string GetLocalizedDescription(Lang lang) => lang switch
        {
            Lang.schinese => "我在吃饭。真的。没什么好说的。",
            _ => "Eating now... Really. There's nothing to say.",
        };
    }
}
