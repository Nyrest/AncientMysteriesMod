namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_MachineGuns)]
    [MetaImage(tex_Gun_TheSpiritOfDarkness)]
    [MetaInfo(Lang.english, "The Spirit Of Darkness", "desc")]
    [MetaInfo(Lang.schinese, "", "")]
    [MetaType(MetaType.Gun)]
    public partial class TheSpiritOfDarkness : AMGun
    {
        public TheSpiritOfDarkness(float xval, float yval) : base(xval, yval)
        {
        }
    }
}