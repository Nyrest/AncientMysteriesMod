namespace AncientMysteries.AmmoTypes
{
    public sealed class AT_Electronic : AMAmmoType
    {
        public AT_Electronic()
        {
            accuracy = 1f;
            range = 200f;
            penetration = 10f;
            rangeVariation = 20f;
            bulletThickness = 2f;
            bulletColor = Color.Lime;
            bulletSpeed = 40f;
            //this.sprite = TexHelper.ModSprite("ElectronicStar.png");
            //this.sprite.CenterOrigin();
            //bulletLength = 10;
        }
    }
}
