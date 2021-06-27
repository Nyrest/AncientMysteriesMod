namespace AncientMysteries.AmmoTypes
{
    public class AT_Overgrowth : AmmoType
    {
        public AT_Overgrowth()
        {
            sprite = Texs.OvergrowthSmall.ModSprite();
            bulletSpeed = 5f;
            accuracy = 0.3f;
            speedVariation = 2.5f;
        }

        public AT_Overgrowth(bool isBig)
        {
            if (isBig)
            {
                sprite = Texs.OvergrowthBig.ModSprite();
                bulletSpeed = 4f;
                accuracy = 0.4f;
                speedVariation = 3f;
            }
            else
            {
                sprite = Texs.OvergrowthSmall.ModSprite();
                bulletSpeed = 5f;
                accuracy = 0.3f;
                speedVariation = 2.5f;
            }
            rangeVariation = 50f;
        }
    }
}
