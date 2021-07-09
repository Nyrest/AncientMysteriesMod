namespace AncientMysteries.Items.Sucks
{
    public class CosmicDisruption_AmmoType : AmmoType
    {
        public CosmicDisruption_AmmoType()
        {
            accuracy = 1f;
            range = 2000f;
            penetration = float.PositiveInfinity;
            bulletSpeed = 100f;
            combustable = true;
            bulletLength = 2000;
            rangeVariation = 0;
            speedVariation = 0;
        }
    }
}
