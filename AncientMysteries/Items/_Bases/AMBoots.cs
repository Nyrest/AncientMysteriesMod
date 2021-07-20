namespace AncientMysteries.Items
{
    public abstract class AMBoots : Boots, IAMEquipment, IAMLocalizable
    {
        private static FieldInfo _fieldEquipmentHealth = typeof(Equipment).GetField("_equipmentHealth", BindingFlags.Instance | BindingFlags.NonPublic);

        public new ref SpriteMap _sprite => ref base._sprite;

        public new ref Sprite _pickupSprite => ref base._pickupSprite;

        protected AMBoots(float xpos, float ypos) : base(xpos, ypos)
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
            return EquipmentHitPoints <= 0 && Destroyable && base.Destroy(type);
        }

        protected override bool OnDestroy(DestroyType type = null)
        {
            return EquipmentHitPoints <= 0 && Destroyable && base.OnDestroy(type);
        }

        public abstract string GetLocalizedName(Lang lang);

        public abstract string GetLocalizedDescription(Lang lang);

        public StateBinding _equipmentMaxHitPointsBinding = new(nameof(_equipmentMaxHitPoints));
        public StateBinding _equipmentHitPointsBinding = new(nameof(_equipmentHitPoints));

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
        public bool MakeDefaultHitEffects { get; set; }
    }
}