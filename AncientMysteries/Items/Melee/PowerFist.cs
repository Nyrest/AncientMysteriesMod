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
        public const float maxDashTime = 3 * 60; // in ticks
        public float dashTime = -1; // in ticks, -1 = not started

        public bool Dashing
        {
            get => dashTime >= 0;
            set
            {
                if (!value)
                {
                    dashTime = -1;
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
            if (CheckCollide(out var collideWith))
            {
                switch (collideWith)
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
                    case Block fuckingBlock:
                        {
                            WorldHelper.DestroyBlocksRadius(
                                fuckingBlock.position,
                                20,
                                culprit: duck,
                                false,
                                true,
                                // culprit will not be destoryed
                                true);
                            // stop dashing
                            goto default;
                        }
                    default:
                        Dashing = false;
                        break;
                }
            }
            base.Update();
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
