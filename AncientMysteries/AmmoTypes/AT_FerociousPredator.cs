namespace AncientMysteries.AmmoTypes
{
    public class AT_FerociousPredator : AMAmmoType
    {
        public AT_FerociousPredator()
        {
			accuracy = 1f;
			penetration = 0.35f;
			bulletSpeed = 9f;
			rangeVariation = 0f;
			speedVariation = 0f;
			range = 2000f;
			rebound = true;
			affectedByGravity = true;
			deadly = false;
			weight = 5f;
			bulletThickness = 2f;
			bulletColor = Color.White;
			bulletType = typeof(Bullet_FerociousPredato);
			immediatelyDeadly = true;
			sprite = new Sprite("launcherGrenade");
			sprite.CenterOrigin();
		}

		public override void PopShell(float x, float y, int dir)
		{
            PistolShell shell = new(x, y)
            {
                hSpeed = dir * (1.5f + Rando.Float(1f))
            };
            Level.Add(shell);
		}
    }
}
