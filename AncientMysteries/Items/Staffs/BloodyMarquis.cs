using DuckGame;

namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_BloodyMarquis)]
    [MetaInfo(Lang.english, "Bloody Marquis", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Magic)]
    public partial class BloodyMarquis : AMStaff
    {
        public const int BulletCount = 10;

        public int currentBulletIndex = 0;

        const int radius = 5;

        public Waiter waiter = new Waiter(2);

        public BloodyMarquis(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRun(tex_Staff_BloodyMarquis);
            _castSpeed = 0.05f;
        }

        public override void OnSpelling()
        {
            base.OnSpelling();
            if (_castTime == 1 && waiter.Tick())
            {
                GenerateBullet(currentBulletIndex++);
                if (currentBulletIndex == 10) currentBulletIndex = 0;
            }
        }

        public override void OnReleaseAction()
        {
            base.OnReleaseAction();
            waiter.Reset();
            currentBulletIndex = 0;
        }

        public void GenerateBullet(int i)
        {
            float SingleBulletAngle = 360f / BulletCount;
            float angleDeg = SingleBulletAngle * i;
            Vec2 offset = new Vec2(0, 30);
            Vec2 rotated = offset.Rotate(Maths.DegToRad(angleDeg), Vec2.Zero);
            Vec2 finalPos = owner.position + rotated;
            BloodyMarquis_Crystal crystal = new BloodyMarquis_Crystal(finalPos.x, finalPos.y);
            Level.Add(crystal);
        }
    }
}
