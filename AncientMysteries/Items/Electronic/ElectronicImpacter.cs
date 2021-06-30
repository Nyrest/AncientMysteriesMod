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
			this.ammo = 80;
			this._ammoType = new AT_Electronic()
			{
				
			};
			this._type = "gun";
			this.ReadyToRunMap("ElectronicImpacter.png");
			_flare.color = Color.Transparent;
			BarrelSmokeFuckOff();
			this._barrelOffsetTL = new Vec2(24f, 5f);
			this._fireSound = "laserRifle";
			this._fireSoundPitch = 0.9f;
			this._fireWait = 0.9f;
			this._kickForce = 0f;
			this._fullAuto = true;
		}
    }
}
