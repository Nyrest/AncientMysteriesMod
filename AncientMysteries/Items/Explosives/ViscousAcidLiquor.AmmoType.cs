namespace AncientMysteries.Items.Explosives
{
    public class ViscousAcidLiquor_AmmoType : AMAmmoType
    {
		public ViscousAcidLiquor_AmmoType()
		{
			accuracy = 1f;
			penetration = 0.35f;
			bulletSpeed = 9f;
			rangeVariation = 0f;
			speedVariation = 0f;
			range = 2000f;
			affectedByGravity = true;
			deadly = false;
			weight = 5f;
			bulletThickness = 2f;
			bulletColor = Color.White;
			bulletType = typeof(ViscousAcidLiquor_Bullet);
			immediatelyDeadly = true;
			sprite = TexHelper.ModSprite(t_ViscousAcidLiquor_Bullet);
			sprite.CenterOrigin();
		}
	}
}
