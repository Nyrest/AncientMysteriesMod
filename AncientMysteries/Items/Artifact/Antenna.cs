using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items.Artifact
{
    [EditorGroup(g_artifacts)]
    class Antenna : AMHoldable
    {
        public int charger = 0;

        public AntennaBullet[] bulletsBuffer;

        public StateBinding cBinding = new(nameof(charger));

        public bool ShouldShoot => charger >= 60;

        public Antenna(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(t_Holdable_Antenna);
        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            AMLang.schinese => "怖人触须",
            _ => "Antenna",
        };

        public override string GetLocalizedDescription(AMLang lang) => lang switch
        {
            AMLang.schinese => "沾染了罪恶之血的触须，正等待着下一个目标……",
            _ => "It, which is stained by the blood of sins, awaits...",
        };

        public override void OnHoldAction()
        {
            base.OnHoldAction();
            if (!ShouldShoot)
            {
                charger++;
            }
        }

        public override void OnPressAction()
        {
            base.OnPressAction();
            float speedMultiplier = 3;
            bulletsBuffer = new AntennaBullet[6]
            {
                new(new Vec2(-2, 0) * speedMultiplier, duck, x, y),
                new(new Vec2(-2, 1) * speedMultiplier, duck, x, y),
                new(new Vec2(-2, -1) * speedMultiplier, duck, x, y),
                new(new Vec2(2, 0) * speedMultiplier, duck, x, y),
                new(new Vec2(2, 1) * speedMultiplier, duck, x, y),
                new(new Vec2(2, -1) * speedMultiplier, duck, x, y),
            };
            for (int i = 0; i < bulletsBuffer.Length; i++)
            {
                Level.Add(bulletsBuffer[i]);
            }
        }

        public override void OnReleaseAction()
        {
            base.OnReleaseAction();
            const float speedMultiplier = 3;
            if (ShouldShoot)
            {
                bulletsBuffer[0].move = speedMultiplier * new Vec2(-2, +0);
                bulletsBuffer[1].move = speedMultiplier * new Vec2(-2, +1);
                bulletsBuffer[2].move = speedMultiplier * new Vec2(-2, -1);
                bulletsBuffer[3].move = speedMultiplier * new Vec2(+2, +0);
                bulletsBuffer[4].move = speedMultiplier * new Vec2(+2, +1);
                bulletsBuffer[5].move = speedMultiplier * new Vec2(+2, -1);
            }
            else if(bulletsBuffer != null)
            {
                for (int i = 0; i < bulletsBuffer.Length; i++)
                {
                    Level.Remove(bulletsBuffer[i]);
                }
            }
            bulletsBuffer = null;
        }

        public override void Update()
        {
            base.Update();
            if (held && bulletsBuffer != null)
            {
                bulletsBuffer[0].position = new Vec2(x - 20f, y + 3f);
                bulletsBuffer[1].position = new Vec2(x - 15f, y + 8f);
                bulletsBuffer[2].position = new Vec2(x - 15f, y - 2f);
                bulletsBuffer[3].position = new Vec2(x + 20f, y);
                bulletsBuffer[4].position = new Vec2(x + 15f, y + 5f);
                bulletsBuffer[5].position = new Vec2(x + 15f, y - 5f);
            }
            else charger = 0;
        }
    }
}
