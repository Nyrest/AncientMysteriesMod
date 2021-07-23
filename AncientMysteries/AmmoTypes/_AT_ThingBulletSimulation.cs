namespace AncientMysteries.AmmoTypes
{
    /// <summary>
    /// Not designed to network sync
    /// </summary>
    public class AT_ThingBulletSimulation : AMAmmoType
    {
        public AT_ThingBulletSimulation()
        {
            penetration = 0;
            range = 2;
            bulletSpeed = range;
            this.canBeReflected = false;
        }
    }
}