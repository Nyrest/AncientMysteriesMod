using AncientMysteries.Utilities;

namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_RainbowEyedrops : AmmoType
    {
        public AT_RainbowEyedrops()
        {
            accuracy = 0.93f;

            bulletSpeed = 9f;
            rangeVariation = 0f;
            speedVariation = 0.5f;
            range = 1500f;

            penetration = 2f;
            weight = 5f;

            bulletLength = 3000;
            affectedByGravity = true;

            bulletType = typeof(AT_RainbowEyedrops_Bullet);
        }

        public class AT_RainbowEyedrops_Bullet : Bullet
        {
            public AT_RainbowEyedrops_Bullet(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network) { }

            public override void Update()
            {
                base.Update();
                if (color == Color.White)
                {
                    color = HSL.FromHslFloat(Rando.Float(0f, 1f), Rando.Float(0.7f, 1f), Rando.Float(0.45f, 0.65f));
                }
            }
        }
    }
}
