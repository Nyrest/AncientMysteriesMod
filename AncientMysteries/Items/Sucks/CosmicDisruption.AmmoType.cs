namespace AncientMysteries.Items.Sucks
{
    public class CosmicDisruption_AmmoType : AmmoType
    {
        public CosmicDisruption_AmmoType()
        {
            accuracy = 1f;
            range = 2000f;
            penetration = float.MaxValue;
            bulletSpeed = 80f;
            combustable = true;

            rangeVariation = 0;
            speedVariation = 0;
        }
    }
}
