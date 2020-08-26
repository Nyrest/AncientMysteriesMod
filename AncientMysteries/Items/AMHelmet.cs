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
    public abstract class AMHelmet : Helmet, IAMLocalizable
    {
        private static FieldInfo _fieldEquipmentHealth = typeof(Equipment).GetField("_equipmentHealth", BindingFlags.Instance | BindingFlags.NonPublic);

        public bool canCrush = true;

        public bool breakable = true;

        public float equipmentHealth = 1;

        protected AMHelmet(float xpos, float ypos) : base(xpos, ypos)
        {
            _editorName = GetLocalizedName(AMLocalization.Current);
        }

        public override void Update()
        {
            _fieldEquipmentHealth.SetValue(this, float.PositiveInfinity);
            base.Update();
        }

        public override void Draw()
        {
            if (canCrush)
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

        public override bool Destroy(DestroyType type = null)
        {
            if (equipmentHealth > 0)
                return false;
            return base.Destroy(type);
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (_equippedDuck == null || bullet.owner == base.duck || !bullet.isLocal)
            {
                return false;
            }
            if (_isArmor && base.duck != null)
            {
                if (bullet.isLocal)
                {
                    if (!breakable && --equipmentHealth <= 0)
                    {
                        base.duck.KnockOffEquipment(this, ting: true, bullet);
                        Thing.Fondle(this, DuckNetwork.localConnection);
                    }
                }
                if (bullet.isLocal && Network.isActive)
                {
                    _netTing.Play();
                }
                Level.Add(MetalRebound.New(hitPos.x, hitPos.y, (bullet.travelDirNormalized.x > 0f) ? 1 : (-1)));
                for (int i = 0; i < 6; i++)
                {
                    Level.Add(Spark.New(base.x, base.y, bullet.travelDirNormalized));
                }
                if (physicsMaterial == PhysicsMaterial.Metal)
                {
                    Level.Add(MetalRebound.New(hitPos.x, hitPos.y, (bullet.travelDirNormalized.x > 0f) ? 1 : (-1)));
                    hitPos -= bullet.travelDirNormalized;
                    for (int i = 0; i < 3; i++)
                    {
                        Level.Add(Spark.New(hitPos.x, hitPos.y, bullet.travelDirNormalized));
                    }
                }
                return thickness > bullet.ammo.penetration;
            }
            return base.Hit(bullet, hitPos);
        }

        public abstract string GetLocalizedName(AMLang lang);
    }
}
