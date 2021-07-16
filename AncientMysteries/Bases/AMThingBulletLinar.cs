namespace AncientMysteries.Bases
{
    public abstract class AMThingBulletLinar : AMThingBulletBase
    {
        public AMThingBulletLinar(Vec2 pos, float bulletRange, float bulletPenetration, Vec2 initSpeed, Duck safeDuck) : base(pos, bulletRange, bulletPenetration, initSpeed, safeDuck)
        {
        }

        public override IEnumerable<MaterialThing> BulletCollideCheck()
        {
            return Level.CheckLineAll<MaterialThing>(lastPosition, position);
        }
    }
}
