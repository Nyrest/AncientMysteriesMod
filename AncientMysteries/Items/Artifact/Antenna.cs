﻿namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Artifacts)]
    [MetaImage(tex_Holdable_Antenna)]
    [MetaInfo(Lang.Default, "Antenna", "It, which is stained by the blood of sins, awaits...")]
    [MetaInfo(Lang.schinese, "怖人触须", "沾染了罪恶之血的触须，正等待着下一个目标……")]
    [MetaType(MetaType.Magic)]
    partial class Antenna : AMHoldable
    {
        public int charger = 0;

        public AntennaBullet[] bulletsBuffer;

        public StateBinding changerBinding = new(nameof(charger));

        public const int bulletCount = 6;

        public const int changerMax = 50;

        // do not modify at runtime
        public static readonly Vec2[] bulletPosition = new Vec2[bulletCount]
        {
            new Vec2(- 25f, 0),
            new Vec2(- 20f, 0 + 6f),
            new Vec2(- 20f, 0 - 6f),
            new Vec2(+ 25f, 0),
            new Vec2(+ 20f, 0 + 6f),
            new Vec2(+ 20f, 0 - 6f)
        };

        // do not modify at runtime
        public static readonly Vec2[] bulletAngle = new Vec2[bulletCount]
        {
            new Vec2(-2, +0),
            new Vec2(-2, +1),
            new Vec2(-2, -1),
            new Vec2(+2, +0),
            new Vec2(+2, +1),
            new Vec2(+2, -1)
        };

        public bool ShouldShoot => charger >= changerMax;

        public Antenna(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_Holdable_Antenna).CenterOrigin();
        }

        public Waiter RumbleWaiter = new(5);

        public override void OnHoldAction()
        {
            base.OnHoldAction();
            if (bulletsBuffer is null)
            {
                if (isServerForObject)
                {
                    bulletsBuffer = new AntennaBullet[bulletCount]
                    {
                        new(position, duck, bulletAngle[0]),
                        new(position, duck, bulletAngle[1]),
                        new(position, duck, bulletAngle[2]),
                        new(position, duck, bulletAngle[3]),
                        new(position, duck, bulletAngle[4]),
                        new(position, duck, bulletAngle[5]),
                    };
                    for (int i = 0; i < bulletsBuffer.Length; i++)
                    {
                        Level.Add(bulletsBuffer[i]);
                    }
                }
            }
            else
            {
                if (!ShouldShoot)
                {
                    charger++;
                    if (duck != null && RumbleWaiter.Tick())
                        RumbleManager.AddRumbleEvent(duck.profile, new RumbleEvent(RumbleIntensity.Kick, RumbleDuration.Pulse, RumbleFalloff.None));
                }
            }
        }

        public override void OnReleaseAction()
        {
            const float speedMultiplier = 3;
            if (ShouldShoot && bulletsBuffer != null)
            {
                for (int i = 0; i < bulletCount; i++)
                {
                    Fondle(bulletsBuffer[i]);
                    bulletsBuffer[i].bulletVelocity = speedMultiplier * bulletAngle[i];
                }
                bulletsBuffer = null;
                charger = 0;
            }
        }

        public override void Update()
        {
            base.Update();
            if (held)
            {
                if (bulletsBuffer is null) return;
                for (int i = 0; i < bulletCount; i++)
                {
                    bulletsBuffer[i].position = position + bulletPosition[i];
                    bulletsBuffer[i].alpha = charger / (float)changerMax;

                    float shakeOffset = 3 - (3 * (charger / (float)changerMax));
                    Vec2 offset = new(
                        i < bulletCount / 2 ? Rando.Float(-shakeOffset, 0) : Rando.Float(0, shakeOffset),
                        Rando.Float(0, shakeOffset).RandomNegative());
                    bulletsBuffer[i].position += offset;
                }

                for (int i = 0; i < bulletCount; i++)
                {
                    bulletsBuffer[i].angle = bulletsBuffer[i].CalcBulletAngleRadian(bulletAngle[i]);
                }
            }
            else
            {
                charger = 0;
                if (bulletsBuffer != null)
                {
                    for (int i = 0; i < bulletCount; i++)
                    {
                        Level.Remove(bulletsBuffer[i]);
                    }
                    bulletsBuffer = null;
                }
            }
        }
    }
}