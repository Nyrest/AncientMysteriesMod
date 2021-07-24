namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_BloodyMarquis)]
    [MetaInfo(Lang.english, "Bloody Marquis", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
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
