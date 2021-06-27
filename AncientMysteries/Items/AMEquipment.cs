using AncientMysteries.Localization;
using AncientMysteries.Localization.Enums;
using System.Reflection;

namespace AncientMysteries.Items
{
    public abstract class AMEquipment : Equipment, IAMEquipment, IAMLocalizable
    {
        private static FieldInfo _fieldEquipmentHealth = typeof(Equipment).GetField("_equipmentHealth", BindingFlags.Instance | BindingFlags.NonPublic);

        protected AMEquipment(float xpos, float ypos) : base(xpos, ypos)
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

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (BulletThroughNotEquipped && (_equippedDuck == null || bullet.owner == base.duck || !bullet.isLocal))
            {
                return false;
            }
            if (_isArmor)
            {
                if (bullet.isLocal && duck != null)
                {
                    if (--EquipmentHitPoints <= 0 && KnockOffOnHit)
                    {
                        base.duck.KnockOffEquipment(this, ting: true, bullet);
                        Fondle(this, DuckNetwork.localConnection);
                    }
                }
                if (bullet.isLocal && Network.isActive)
                {
                    NetSoundEffect.Play("equipmentTing");
                }
                bullet.hitArmor = true;
                Level.Add(MetalRebound.New(hitPos.x, hitPos.y, (bullet.travelDirNormalized.x > 0f) ? 1 : (-1)));
                for (int i = 0; i < 6; i++)
                {
                    Level.Add(Spark.New(base.x, base.y, bullet.travelDirNormalized));
                }
                return thickness > bullet.ammo.penetration;
            }
            return base.Hit(bullet, hitPos);
        }

        public abstract string GetLocalizedName(AMLang lang);

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
