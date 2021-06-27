namespace AncientMysteries.AmmoTypes
{
    public class AT_FerociousPredator : AmmoType
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
                hSpeed = (float)dir * (1.5f + Rando.Float(1f))
            };
            Level.Add(shell);
		}
        /*
        public override void OnHit(bool destroyed, Bullet b)
        {
            base.OnHit(destroyed, b);
            if (!destroyed || !isLocal)
            {
                return;
            }
            for (int repeat = 0; repeat < 6; repeat++)
            {
                ExplosionPart ins = new ExplosionPart(pos.x - 8f + Rando.Float(32f), pos.y - 8f + Rando.Float(32f));
                ins.xscale = 3f;
                ins.yscale = 3f;
                Level.Add(ins);
            }
            SFX.Play("explode");
            List<Bullet> firedBullets = new List<Bullet>(24);
            Vec2 bPos = pos;
            bPos -= travelDirNormalized;
            for (int i = 0; i < 24; i++)
            {
                float dir = (float)i * 30f - 10f + Rando.Float(20f);
                ATGrenadeLauncherShrapnel shrap = new ATGrenadeLauncherShrapnel();
                shrap.range = 100f + Rando.Float(20f);
                Bullet bullet = new Bullet(bPos.x, bPos.y, shrap, dir);
                bullet.firedFrom = this;
                firedBullets.Add(bullet);
                Level.Add(bullet);
            }
            if (Network.isActive && isLocal)
            {
                NMFireGun gunEvent = new NMFireGun(null, firedBullets, 0, rel: false, 4);
                Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                firedBullets.Clear();
            }
            IEnumerable<Window> windows = Level.CheckCircleAll<Window>(position, 20f);
            foreach (Window w in windows)
            {
                if (Level.CheckLine<Block>(position, w.position, w) == null)
                {
                    w.Destroy(new DTImpact(this));
                }
            }
        }
        */
    }
}
