using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Melees)]
    [MetaImage(tex_Melee_PowerFist)]
    [MetaInfo(Lang.english, "Power Fist", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Melee)]
    public partial class PowerFist : AMHoldable
    {
        public const float maxDashTime = 30; // in ticks
        public float dashTime = -1; // in ticks, -1 = not started

        public bool Dashing
        {
            get => duck is not null && dashTime >= 0;
            set
            {
                if (!value)
                {
                    dashTime = -1;
                    if (duck != null)
                    {
                        duck.immobilized = false;
                    }
                    return;
                }
                if (dashTime < 0)
                    dashTime = 0;
            }
        }

        public PowerFist(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_Melee_PowerFist);
        }

        public override void Update()
        {
            base.Update();
            if (Dashing)
            {
                if (dashTime++ >= maxDashTime)
                {
                    Dashing = false;
                    return;
                }
                var vel = Maths.AngleToVec(angle) * 14;
                vel.x *= offDir;
                vel.y *= -1;
                duck.velocity = vel;
                foreach (MaterialThing item in Level.CheckCircleAll<MaterialThing>(duck.position, 30))
                {
                    switch (item)
                    {
                        case Window window:
                            {
                                window.Destroy(new DTImpact(duck));
                                break;
                            }
                        case Door door when door.locked is false:
                            {
                                // TODO: use DTShot to make velocity
                                door.Destroy(new DTImpact(duck));
                                break;
                            }
                        default: break;
                    }
                }
                if (CheckCollide(out var collideWith))
                {
                    switch (collideWith)
                    {
                        case Block fuckingBlock:
                            {
                                WorldHelper.DestroyBlocksRadius(
                                    fuckingBlock.position,
                                    35,
                                    culprit: duck,
                                    true,
                                    true,
                                    // culprit will not be destoryed
                                    true,
                                    false);
                                // stop dashing
                                Dashing = false;
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
        }

        public override void PressAction()
        {
            base.PressAction();
            if (this.duck is Duck duck)
            {
                duck.immobilized = true;
                Dash();
            }
        }

        public bool CheckCollide([NotNullWhen(true)] out MaterialThing collideWith) =>
            (collideWith = duck.collideLeft) is not null ||
            (collideWith = duck.collideRight) is not null;

        public void Dash()
        {
            Dashing = true;
        }
    }
}
