namespace AncientMysteries.Items
{
    public class ElectronicImpacter_AmmoType : AMAmmoType
    {
        public ElectronicImpacter_AmmoType()
        {
            accuracy = 1f;
            range = 250f;
            penetration = 10f;
            rangeVariation = 20f;
            bulletThickness = 2f;
            bulletColor = Color.Lime;
            bulletSpeed = 40f;
        }
    }
}