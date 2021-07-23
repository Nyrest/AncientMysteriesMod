namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Staffs)]
    [MetaImage(tex_Staff_AbyssalGazer)]
    [MetaInfo(Lang.english, "Abyssal Gazer", "The Hellfire is swaying on the staff that shrouded in the darkness of sins.")]
    [MetaInfo(Lang.schinese, "深渊凝视者", "地狱之业火摇曳于被无尽暗黑笼罩的罪恶之杖")]
    [MetaType(MetaType.Magic)]
    public partial class AbyssalGazer : AMStaff
    {
        public AbyssalGazer(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRun(tex_Staff_AbyssalGazer);
        }
    }
}