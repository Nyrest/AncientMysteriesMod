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
    [MetaInfo(Lang.english, "Crash Error Dialog", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Props)]
    public partial class CrashErrorDialog : AMHoldable
    {
        private HashSet<ushort> breakBlockIds = new();
        public CrashErrorDialog(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_Props_CrashErrorDialog);
            _impactThreshold = 0.3f;
            weight = 8f;
        }

        public override void Update()
        {
            base.Update();
            if (owner is Thing && owner.offDir == -1)
            {
                _holdOffset = new Vec2(-(width / 2) + 6, -(height / 2) + 8);
            }
            else
            {
                _holdOffset = new Vec2((width / 2) - 6, -(height / 2) + 8);
            }
            if (breakBlockIds.Count != 0)
            {
            }
        }

        public override sbyte offDir { get => 1; set { } }

        public override void Impact(MaterialThing with, ImpactedFrom from, bool solidImpact)
        {
            base.Impact(with, from, solidImpact);
            if (this.velocity.length <= 0.1f) return;
            if (with is IAmADuck || with is Window)
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
