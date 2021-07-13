namespace AncientMysteries.Items.Electronic
{
    [EditorGroup(g_rifles)]
    public sealed class ElectronicImpacter : AMGun
	{
		public override string GetLocalizedName(AMLang lang) => lang switch
		{
			_ => "Electronic Impacter",
		};

		public ElectronicImpacter(float xval, float yval) : base(xval, yval)
		{
			ammo = 80;
			_ammoType = new AT_Electronic();
			_type = "gun";
			this.ReadyToRunMap(t_ElectronicImpacter);
			_flare.color = Color.Transparent;
			BarrelSmokeFuckOff();
			_barrelOffsetTL = new Vec2(24f, 5f);
			_fireSound = "laserRifle";
			_fireSoundPitch = 0.9f;
			_fireWait = 0.9f;
			_kickForce = 0f;
			_fullAuto = true;
		}
    }
}
