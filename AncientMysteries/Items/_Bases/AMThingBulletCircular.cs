namespace AncientMysteries.Items
{
    public abstract class AMThingBulletCircular : AMThingBulletBase
    {
        public float BulletRadius { get; init; }

        public AMThingBulletCircular(Vec2 pos, float bulletRange, float bulletPenetration, Vec2 initSpeed, Duck safeDuck) : base(pos, bulletRange, bulletPenetration, initSpeed, safeDuck)
        {

        }

        #if DEBUG
        public override void Initialize()
        {
            base.Initialize();
            if (BulletRadius == 0)
            {
                throw new Exception("Set a fucking value for BulletRadius");
            }
        }
        #endif

        public override IEnumerable<MaterialThing> BulletCollideCheck()
        {
            return Level.CheckCircleAll<MaterialThing>(position, BulletRadius);
        }
    }
}