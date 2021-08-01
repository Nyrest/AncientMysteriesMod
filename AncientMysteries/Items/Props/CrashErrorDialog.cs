using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    [EditorGroup(group_Props)]
    [MetaImage(tex_Props_CrashErrorDialog)]
    [MetaInfo(Lang.Default, "Crash Error Dialog", "desc")]
    [MetaInfo(Lang.schinese, "错误弹窗", "「你不要过来啊！！」")]
    [MetaType(MetaType.Props)]
    public partial class CrashErrorDialog : AMHoldable
    {
        private readonly HashSet<ushort> breakBlockIds = new();
        public CrashErrorDialog(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_Props_CrashErrorDialog);
            _impactThreshold = 0.3f;
            weight = 8f;
        }

        public override void Update()
        {
            base.Update();
            _holdOffset = owner switch
            {
                not null when owner.offDir == -1 => new Vec2(-(width / 2) + 6, -(height / 2) + 8),
                _ => new Vec2(width / 2 - 6, -(height / 2) + 8),
            };
            if (breakBlockIds.Count != 0)
            {
            }
        }

        public override sbyte offDir { get => 1; set { } }

        public override void Impact(MaterialThing with, ImpactedFrom from, bool solidImpact)
        {
            base.Impact(with, from, solidImpact);
            if (this.velocity.length <= 0.1f) return;
            if (with is IAmADuck or Window)
            {
                with.Destroy(new DTImpale(this));
            }
            if (from != ImpactedFrom.Bottom)
            {
                if (with is Block bl)
                {
                    bl.shouldWreck = true;
                    if (bl is AutoBlock && !(bl as AutoBlock).indestructable)
                    {
                        breakBlockIds.Add((bl as AutoBlock).blockIndex);
                    }
                    bl.group?.Wreck();
                }
            }
        }
    }
}
