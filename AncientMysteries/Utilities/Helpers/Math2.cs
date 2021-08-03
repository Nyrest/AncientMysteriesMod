namespace AncientMysteries.Utilities
{
    public static class Math2
    {
        public const float PI = 3.14159274f;

        public const float TwoPI = PI * 2;

        public static Vec2 GetBulletVecDeg(float degress, float speed = 1f)
        {
            return Maths.AngleToVec(Maths.DegToRad(degress)) * speed;
        }

        public static Vec2 GetBulletVecRad(float degress, float speed = 1f)
        {
            return Maths.AngleToVec(Maths.DegToRad(degress)) * speed;
        }

        public static Vec2 GetBulletVecDeg(float degress, float speed = 1f, float speedVariable = 0, float accuracy = 1f)
        {
            var accuracyLossDeg = Rando.Float(360 * accuracy);
            return Maths.AngleToVec(Maths.DegToRad(degress + accuracyLossDeg)) * (speed + Rando.Float(speedVariable).RandomNegative());
        }
    }
}