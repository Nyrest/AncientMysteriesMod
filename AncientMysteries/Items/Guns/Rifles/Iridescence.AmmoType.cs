namespace AncientMysteries.Items.Rainbow
{
    public sealed class Iridescence_AmmoType : AMAmmoType
    {
        public Iridescence_AmmoType()
        {
            accuracy = 1f;
            range = 800f;
            penetration = 2f;
            rangeVariation = 10f;
            bulletLength = 3000;
            combustable = true;
            bulletType = typeof(Iridescence_Bullet);
            bulletColor = Color.White;
        }
    }
}
