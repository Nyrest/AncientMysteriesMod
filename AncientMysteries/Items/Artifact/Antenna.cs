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

        public AntennaBullet fuck1;//左

        public AntennaBullet fuck2;//左下

        public AntennaBullet fuck3;//左上

        public AntennaBullet fuck4;//右

        public AntennaBullet fuck5;//右下

        public AntennaBullet fuck6;//右上

        public StateBinding cBinding = new(nameof(charger));
        public Antenna(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(t_Holdable_Antenna);
        }

        public override string GetLocalizedName(AMLang lang) => lang switch
        {
            _ => "Antenna",
        };

        public override void OnPressAction()
        {
            base.OnPressAction();
            float speedMultiplier = 3;
            fuck1 = new(new Vec2(-2, 0) * speedMultiplier, owner, x, y, TexHelper.ModSprite(t_Bullet_Antenna, true));
            fuck2 = new(new Vec2(-2, 1) * speedMultiplier, owner, x, y, TexHelper.ModSprite(t_Bullet_Antenna, true));
            fuck3 = new(new Vec2(-2, -1) * speedMultiplier, owner, x, y, TexHelper.ModSprite(t_Bullet_Antenna, true));
            fuck4 = new(new Vec2(2, 0) * speedMultiplier, owner, x, y, TexHelper.ModSprite(t_Bullet_Antenna, true));
            fuck5 = new(new Vec2(2, 1) * speedMultiplier, owner, x, y, TexHelper.ModSprite(t_Bullet_Antenna, true));
            fuck6 = new(new Vec2(2, -1) * speedMultiplier, owner, x, y, TexHelper.ModSprite(t_Bullet_Antenna, true));
            Level.Add(fuck1);
            Level.Add(fuck2);
            Level.Add(fuck3);
            Level.Add(fuck4);
            Level.Add(fuck5);
            Level.Add(fuck6);
        }

        public override void OnHoldAction()
        {
            base.OnHoldAction();
            charger++;
            fuck1.position = new Vec2(x - 20f, y + 3f);
            fuck2.position = new Vec2(x - 15f, y + 8f);
            fuck3.position = new Vec2(x - 15f, y - 2f);
            fuck4.position = new Vec2(x + 20f, y);
            fuck5.position = new Vec2(x + 15f, y + 5f);
            fuck6.position = new Vec2(x + 15f, y - 5f);
        }

        public override void OnReleaseAction()
        {
            base.OnReleaseAction();
            if (charger >= 60)
            {
                fuck1.isMoving = true;
                fuck2.isMoving = true;
                fuck3.isMoving = true;
                fuck4.isMoving = true;
                fuck5.isMoving = true;
                fuck6.isMoving = true;
            }
            else
            {
                Level.Remove(fuck1);
                Level.Remove(fuck2);
                Level.Remove(fuck3);
                Level.Remove(fuck4);
                Level.Remove(fuck5);
                Level.Remove(fuck6);
            }
            charger = 0;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
