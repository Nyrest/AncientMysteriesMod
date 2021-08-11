namespace AncientMysteries.Items
{
    [EditorGroup(group_Props)]
    [MetaImage(tex_Holdable_Epitaph)]
    [MetaInfo(Lang.Default, "Epitaph", "Someday. You will be there where we are.")]
    [MetaInfo(Lang.schinese, "墓志铭", "也许某天，你会出现在和我们一样的地方。")]
    [MetaType(MetaType.Props)]
    public partial class Epitaph : AMHoldable
    {
        public Epitaph(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_Holdable_Epitaph);
        }

        public override void OnPressAction()
        {
            base.OnPressAction();
            if (duck is Duck d)
            {
                Epitaph_Buff_Invisible buff = new(0, 0);
                Level.Add(buff);
                d.Equip(buff, false);
                Level.Remove(this);
            }
        }
    }
}