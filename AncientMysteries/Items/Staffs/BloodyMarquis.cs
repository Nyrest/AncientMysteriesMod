using DuckGame;

namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_BloodyMarquis)]
    [MetaInfo(Lang.english, "Bloody Marquis", "Just looking at it makes you feel a chill down your spine.")]
    [MetaInfo(Lang.schinese, "血腥公爵", "只是看着它就让你脊背发凉。")]
    [MetaType(MetaType.Magic)]
    public partial class BloodyMarquis : AMStaff
    {
        public const int BulletCount = 10;

        public int currentBulletIndex = 0;

        public Waiter waiter = new(2);

        public BloodyMarquis(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRun(tex_Staff_BloodyMarquis);
            _castSpeed = 0.01f;
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
            if (owner == null) return;

            float SingleBulletAngle = 360f / BulletCount;
            float angleDeg = SingleBulletAngle * i;
            Vec2 offset = new(0, 30);
            Vec2 rotated = offset.Rotate(Maths.DegToRad(angleDeg), Vec2.Zero);
            Vec2 rotated2 = offset.Rotate(Maths.DegToRad(angleDeg) + 3.14f, Vec2.Zero);
            Vec2 finalPos = owner.position + rotated;
            Vec2 finalPos2 = owner.position + rotated2;
            BloodyMarquis_Crystal crystal = new(finalPos.x, finalPos.y, angleDeg, duck);
            BloodyMarquis_Crystal crystal2 = new(finalPos2.x, finalPos2.y, angleDeg + 180, duck);
            Level.Add(crystal);
            Level.Add(crystal2);
        }
    }
}
