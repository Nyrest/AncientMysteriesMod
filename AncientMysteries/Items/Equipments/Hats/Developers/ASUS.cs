namespace AncientMysteries.Items
{
    [EditorGroup(group_Equipment_Developer)]
    [MetaImage(tex_Hat_ASUS)]
    [MetaInfo(Lang.english, "ASUS", "desc")]
    [MetaInfo(Lang.schinese, null, "")]
    public partial class ASUS : AMHelmet
    {
        public ASUS(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_Hat_ASUS);
        }

        public override string GetLocalizedDescription(Lang lang) => lang switch
        {
            Lang.schinese => "",
            _ => "",
        };

        public override string GetLocalizedName(Lang lang) => lang switch
        {
            _ => "ASUS",
        };
    }
}
