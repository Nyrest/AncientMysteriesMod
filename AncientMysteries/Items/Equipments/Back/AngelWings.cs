namespace AncientMysteries.Items.Equipments.Back
{
    [EditorGroup(e_developer)]
#warning Texture Todo
    [MetaImage(tex_Bullet_NovaFrame)]
    [MetaInfo(Lang.english, "Angel Wings", "「あぁ〜麻婆豆腐〜♪〜♪」")]
    [MetaInfo(Lang.schinese, "天使之翼", null)]
    public partial class AngelWings : AMEquipmentWing
    {
        public AngelWings(float xpos, float ypos) : base(xpos, ypos)
        {
        }

        public override string GetLocalizedName(Lang lang) => lang switch
        {
            Lang.schinese => "天使之翼",
            _ => "Angel Wings",
        };

        public override string GetLocalizedDescription(Lang lang) => lang switch
        {
            _ => "「あぁ〜麻婆豆腐〜♪〜♪」",
        };
    }
}