namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_BloodyMarquis)]
    [MetaInfo(Lang.english, "Bloody Marquis", "Just looking at it makes you feel a chill down your spine.")]
    [MetaInfo(Lang.schinese, "血腥公爵", "只是看着它就让你脊背发凉。")]
    [MetaType(MetaType.Magic)]
    public partial class BloodyMarquis : AMStaff
    {
        public BloodyMarquis(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRun(tex_Staff_BloodyMarquis);
        }

        public override void OnSpelling()
        {
            base.OnSpelling();
            
        }
    }
}
