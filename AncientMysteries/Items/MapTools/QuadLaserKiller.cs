namespace AncientMysteries.Items
{
    [EditorGroup(group_MapTools)]
    [MetaImage(tex_MapTools_Swirl)]
    [MetaInfo(Lang.english, "Quad Laser Remover", "Perfect for those who wants to remove laggy things.")]
    [MetaInfo(Lang.schinese, "Quad Laser 移除器", "给那些想要手动移除 QuadLaserBullet 的人的礼物")]
    [MetaType(MetaType.MapTools)]
    public partial class QuadLaserKiller : AMMapTool
    {
        public EditorProperty<float> Width = new(100, null, 20f, 1000f, 1f)
        {
            name = "Width",
            _tooltip = "Width of the QuadLazerBullet Removing Zone"
        };

        public EditorProperty<float> Height = new(100, null, 20f, 1000f, 1f)
        {
            name = "Height",
            _tooltip = "Height of the QuadLazerBullet Removing Zone"
        };

        public QuadLaserKiller(float xpos, float ypos) : base(xpos, ypos) { }

        public override void Update()
        {
            if (Level.current is not Editor)
            {
                foreach (QuadLaserBullet q in Level.CheckRectAll<QuadLaserBullet>(
                    position - new Vec2(Width / 2f, Height / 2f),
                    position + new Vec2(Width / 2f, Height / 2f)))
                {
                    Level.Remove(q);
                }
            }
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            if (!Editor.editorDraw && Level.current is Editor)
            {
                Graphics.DrawRect(position - new Vec2(Width / 2f, Height / 2f),
                    position + new Vec2(Width / 2f, Height / 2f), Color.OrangeRed * 0.5f);
            }
        }
    }
}