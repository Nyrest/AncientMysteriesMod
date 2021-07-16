using AncientMysteries.DestroyTypes;
using AncientMysteries.Items.Miscellaneous;

namespace AncientMysteries.Items.Staffs
{
    public class ArcaneNova_Magic_Stage1 : AMThingBulletLinar
    {
        public ArcaneNova_Magic_Stage1(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 275f, float.PositiveInfinity, initSpeed, safeDuck)
        {
            var _spriteMap = t_Bullet_NovaFrame.ModSpriteWithFrames(32, 32, true);
            _spriteMap.AddAnimation("loop", 0.15f, true, 0, 1, 2, 3);
            _spriteMap.SetAnimation("loop");
            _spriteMap.CenterOrigin();
            graphic = _spriteMap;
        }

        public override void BulletRemove()
        {
            base.BulletRemove();
            NovaExp n = new(x, y, true);
            n.xscale *= 3f;
            n.yscale *= 3f;
            Level.Add(n);
            SFX.PlaySynchronized("explode", 0.8f, Rando.Float(-0.1f, 1f), 0f, false);

            if (isServerForObject)
            {
                IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(position, 32f);
                foreach (MaterialThing t2 in things)
                {
                    if (t2 != BulletSafeDuck)
                    {
                        t2.Destroy(new DT_ThingBullet(this));
                    }
                }

                for (int i = 0; i < 5; i++)
                {
                    var bullet = new ArcaneNova_Magic_Stage2(position, GetVectorFromDegress(Rando.Float(0, 360), 6), BulletSafeDuck);
                    Level.Add(bullet);
                }
            }
        }
    }
}
