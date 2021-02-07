using AncientMysteries.Localization.Enums;
using DuckGame;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.FutureTech.Grenades
{
    [EditorGroup(guns)]
    public class ReboundShield : AMGun, IPlatform
    {
        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Rebound Shield",
        };

        public ReboundShield(float xval, float yval) : base(xval, yval)
        {
            ammo = 1;
            var _sprite = new SpriteMap("rock01", 16, 16);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -5f);
            collisionSize = new Vec2(16f, 12f);
            thickness = 100f;
            weight = 10f;
            flammable = 0f;
            physicsMaterial = PhysicsMaterial.Metal;
        }

        public override void ApplyKick() { }

        public override void PressAction() { }

        public override void Fire() { }
    }
}
