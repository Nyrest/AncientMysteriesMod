namespace AncientMysteries.Items
{
    public abstract class AMThingBulletCircular : AMThingBulletBase
    {
        public float BulletRadius { get; init; }

        public AMThingBulletCircular(Vec2 pos, float bulletRange, float bulletPenetration, Vec2 initSpeed, Duck safeDuck) : base(pos, bulletRange, bulletPenetration, initSpeed, safeDuck)
        {
        }

        public override IEnumerable<MaterialThing> BulletCollideCheck()
        {
            return Level.CheckCircleAll<MaterialThing>(position, BulletRadius);
        }
    }
}