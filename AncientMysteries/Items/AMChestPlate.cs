using AncientMysteries.Localization;
using AncientMysteries.Localization.Enums;
using DuckGame;
using System.Reflection;

namespace AncientMysteries.Items
{
    public abstract class AMChestPlate : ChestPlate, IAMEquipment, IAMLocalizable
    {
        private static FieldInfo _fieldEquipmentHealth = typeof(Equipment).GetField("_equipmentHealth", BindingFlags.Instance | BindingFlags.NonPublic);
        private static FieldInfo _fieldSprite = typeof(ChestPlate).GetField("_sprite", BindingFlags.Instance | BindingFlags.NonPublic);
        private static FieldInfo _fieldSpriteOver = typeof(ChestPlate).GetField("_spriteOver", BindingFlags.Instance | BindingFlags.NonPublic);
        private static FieldInfo _fieldPickupSprite = typeof(ChestPlate).GetField("_pickupSprite", BindingFlags.Instance | BindingFlags.NonPublic);

        public SpriteMap _sprite
        {
            get => (SpriteMap)_fieldSprite.GetValue(this);
            set => _fieldSprite.SetValue(this, value);
        }

        public SpriteMap _spriteOver
        {
            get => (SpriteMap)_fieldSpriteOver.GetValue(this);
            set => _fieldSpriteOver.SetValue(this, value);
        }

        public Sprite _pickupSprite
        {
            get => (Sprite)_fieldPickupSprite.GetValue(this);
            set => _fieldPickupSprite.SetValue(this, value);
        }

        protected AMChestPlate(float xpos, float ypos) : base(xpos, ypos)
        {
            _isArmor = true;
            _editorName = GetLocalizedName(AMLocalization.Current);
        }

        public override void Update()
        {
            _fieldEquipmentHealth.SetValue(this, float.PositiveInfinity);
            base.Update();
        }

        public override bool Destroy(DestroyType type = null)
        {
            if (EquipmentHitPoints > 0 || !Destroyable)
                return false;
            return base.Destroy(type);
        }

        protected override bool OnDestroy(DestroyType type = null)
        {
            if (EquipmentHitPoints > 0 || !Destroyable)
                return false;
            return base.OnDestroy(type);
        }

        public abstract string GetLocalizedName(AMLang lang);

        public StateBinding _equipmentMaxHitPointsBinding = new StateBinding(nameof(_equipmentMaxHitPoints));
        public StateBinding _equipmentHitPointsBinding = new StateBinding(nameof(_equipmentHitPoints));

        protected bool _canCrush = true;
        protected bool _destroyable = true;
        protected float _equipmentMaxHitPoints = 1;
        protected float _equipmentHitPoints = 1;
        protected bool _knockOffOnHit = true;
        protected bool bulletThroughNotEquipped = true;

        public bool CanCrush { get => _canCrush; set => _canCrush = value; }
        public bool Destroyable { get => _destroyable; set => _destroyable = value; }
        public float EquipmentMaxHitPoints { get => _equipmentMaxHitPoints; set => _equipmentMaxHitPoints = value; }
        public float EquipmentHitPoints { get => _equipmentHitPoints; set => _equipmentHitPoints = value; }
        public bool KnockOffOnHit { get => _knockOffOnHit; set => _knockOffOnHit = value; }
        public bool BulletThroughNotEquipped { get => bulletThroughNotEquipped; set => bulletThroughNotEquipped = value; }
    }
}
