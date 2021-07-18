namespace AncientMysteries.Items
{
    public class CosmicDisruption_AmmoType : AMAmmoType
    {
        public CosmicDisruption_AmmoType()
        {
            accuracy = 1f;
            range = 2000f;
            penetration = float.PositiveInfinity;
            bulletSpeed = 100f;
            combustable = true;
            bulletLength = 20002f;
            rangeVariation = 0;
            speedVariation = 0;
            bulletColor = Color.Red;
        }
    }
}