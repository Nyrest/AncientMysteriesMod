using System;

namespace AncientMysteries.Items
{
    [EditorGroup(group_MapTools)]
    [MetaImage(tex_MapTools_Swirl)]
    [MetaInfo(Lang.english, "Quad Laser Killer", "Perfect for those who wants to kill laggy things.")]
    [MetaInfo(Lang.schinese, null, "给那些想要手动移除 QuadLaserBullet 的人的礼物")]
    [MetaType(MetaType.MapTools)]
    public partial class QuadLaserKiller : AMMapTool
    {
        public EditorProperty<float> Width = new EditorProperty<float>(100, null, 1f, 1000f, 1f)
        {
            name = "Width",
            _tooltip = "Width"
        };

        public EditorProperty<float> Height = new EditorProperty<float>(100, null, 1f, 1000f, 1f)
        {
            name = "Height",
            _tooltip = "Height"
        };

        public QuadLaserKiller(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_MapTools_Swirl);
            _editorName = "Quadlaser Killer";
            editorTooltip = "Perfect for those who wants to kill laggy things.";
            _visibleInGame = false;
        }

        public override void Update()
        {
            if (!(Level.current is Editor))
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