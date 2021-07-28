#nullable enable

namespace AncientMysteries.Items
{
    public class TemperatureArt_AmmoType_WaterVapor : TemperatureArt_AmmoType_Base
    {
        public TemperatureArt_AmmoType_WaterVapor(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 250, initSpeed, safeDuck)
        {
            this.ReadyToRun(tex_Bullet_TemperatureArt_WaterVapor);
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

            duck.onFire = true;
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