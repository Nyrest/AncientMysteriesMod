using AncientMysteries.Items;
using AncientMysteries.Localization;
using AncientMysteries.Localization.Enums;
using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AncientMysteries.Armor
{
    public abstract class AMHelmet : Helmet, IAMEquipment, IAMLocalizable
    {
        private static FieldInfo _fieldEquipmentHealth = typeof(Equipment).GetField("_equipmentHealth", BindingFlags.Instance | BindingFlags.NonPublic);

        protected AMHelmet(float xpos, float ypos) : base(xpos, ypos)
        {
            _isArmor = true;
            _editorName = GetLocalizedName(AMLocalization.Current);
        }

        public override void Update()
        {
            _fieldEquipmentHealth.SetValue(this, float.PositiveInfinity);
            base.Update();
        }

        public override void Draw()
        {
            if (CanCrush)
            {
                int frm = _sprite.frame;
                _sprite.frame = (crushed ? 1 : 0);
                base.Draw();
                _sprite.frame = frm;
            }
            else
            {
                _sprite.frame = 0;
                base.Draw();
            }
        }

        public override void Crush()
        {
            if (CanCrush)
                base.Crush();
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
            if (_equippedDuck == null || bullet.owner == base.duck || !bullet.isLocal)
            {
                return false;
            }
            if (_isArmor && base.duck != null)
            {
                EquipmentHitPoints--;
                if (bullet.isLocal && EquipmentHitPoints <= 0)
                {
                    base.duck.KnockOffEquipment(this, ting: true, bullet);
                    Thing.Fondle(this, DuckNetwork.localConnection);
                }
                if (MakeDefaultHitEffects)
                {
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
                }
                return BaseBaseHit();
            }
            return BaseBaseHit();
            bool BaseBaseHit()
            {
                if (MakeDefaultHitEffects)
                {
                    if (physicsMaterial == PhysicsMaterial.Metal)
                    {
                        Level.Add(MetalRebound.New(hitPos.x, hitPos.y, (bullet.travelDirNormalized.x > 0f) ? 1 : (-1)));
                        hitPos -= bullet.travelDirNormalized;
                        for (int j = 0; j < 3; j++)
                        {
                            Level.Add(Spark.New(hitPos.x, hitPos.y, bullet.travelDirNormalized));
                        }
                    }
                    else if (physicsMaterial == PhysicsMaterial.Wood)
                    {
                        hitPos -= bullet.travelDirNormalized;
                        for (int i = 0; i < 3; i++)
                        {
                            WoodDebris woodDebris = WoodDebris.New(hitPos.x, hitPos.y);
                            woodDebris.hSpeed = 0f - bullet.travelDirNormalized.x + Rando.Float(-1f, 1f);
                            woodDebris.vSpeed = 0f - bullet.travelDirNormalized.y + Rando.Float(-1f, 1f);
                            Level.Add(woodDebris);
                        }
                    }
                }
                return thickness > bullet.ammo.penetration;
            }
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
        protected bool hasHitEffect = true;

        public bool CanCrush { get => _canCrush; set => _canCrush = value; }
        public bool Destroyable { get => _destroyable; set => _destroyable = value; }
        public float EquipmentMaxHitPoints { get => _equipmentMaxHitPoints; set => _equipmentMaxHitPoints = value; }
        public float EquipmentHitPoints { get => _equipmentHitPoints; set => _equipmentHitPoints = value; }
        public bool KnockOffOnHit { get => _knockOffOnHit; set => _knockOffOnHit = value; }
        public bool BulletThroughNotEquipped { get => bulletThroughNotEquipped; set => bulletThroughNotEquipped = value; }
        public bool MakeDefaultHitEffects { get => hasHitEffect; set => hasHitEffect = value; }
    }
}
