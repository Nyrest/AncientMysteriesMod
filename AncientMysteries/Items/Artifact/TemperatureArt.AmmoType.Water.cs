#nullable enable

namespace AncientMysteries.Items
{
    public class TemperatureArt_AmmoType_Water : TemperatureArt_AmmoType_Base
    {
        public TemperatureArt_AmmoType_Water(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 120, initSpeed, safeDuck)
        {
            var sprite = this.ReadyToRun(tex_Bullet_TemperatureArt_Water);
            sprite.angleDegrees = 90;
        }

        public override bool BulletCanDestory(MaterialThing thing)
        {
            Duck? duck = thing switch
            {
                Duck d when d != BulletSafeDuck => d,
                RagdollPart ragdollPart => ragdollPart.duck,
                _ => null,
            };
            if (duck is null) goto notDuck;

            duck.velocity += speed;
            duck.GoRagdoll();
            return false;

        notDuck:
            return base.BulletCanDestory(thing);
        }

        public override void LegacyImpact(MaterialThing thing)
        {
            Duck? duck = thing switch
            {
                Duck d when d != BulletSafeDuck => d,
                RagdollPart ragdollPart => ragdollPart.duck,
                _ => null,
            };
            if (duck is not null) return;

            base.LegacyImpact(thing);
        }
    }
}