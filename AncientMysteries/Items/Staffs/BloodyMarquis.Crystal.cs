namespace AncientMysteries.Items
{
    public class BloodyMarquis_Crystal : AMThing
    {
        public StateBinding positionBinding = new StateBinding(GhostPriority.High, nameof(position));
        public StateBinding alphaBinding = new StateBinding(GhostPriority.High, nameof(alpha));
        public StateBinding fireAngleDegreeBinding = new StateBinding(GhostPriority.High, nameof(fireAngleDegree));
        public StateBinding safeDuckBinding = new StateBinding(GhostPriority.High, nameof(safeDuck));
        private Duck safeDuck;
        private bool fired = false;
        private float fireAngleDegree;

        public BloodyMarquis_Crystal(float xpos, float ypos, float angleDeg, Duck safeDuck) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_Props_BloodyCrystal);
            fireAngleDegree = angleDeg;
            this.safeDuck = safeDuck;
        }

        public override void Update()
        {
            const int bulletSpeed = 3;
            base.Update();
            alpha -= 0.06f;
            if (!fired)
            {
                fired = true;

                #region Do Fire

                var bullet = new BloodyMarquis_ThingBullet(position, 800, 1, GetBulletVecDeg(-fireAngleDegree, bulletSpeed) * 0.01f, safeDuck);
                Level.Add(bullet);

                #endregion Do Fire
            }
            if (alpha <= 0)
            {
                Level.Remove(this);
            }
        }
    }
}