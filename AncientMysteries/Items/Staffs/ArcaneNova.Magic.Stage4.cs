using AncientMysteries.DestroyTypes;
using AncientMysteries.Items.Miscellaneous;

namespace AncientMysteries.Items.Staffs
{
    public class ArcaneNova_Magic_Stage4 : AMThingBulletLinar
    {
        public ArcaneNova_Magic_Stage4(Vec2 pos, Vec2 initSpeed, Duck safeDuck) : base(pos, 80, float.PositiveInfinity, initSpeed, safeDuck)
        {
            var _spriteMap = t_Bullet_Nova4.ModSpriteWithFrames(14, 6, true);
            graphic = _spriteMap;
        }
    }
}
