using System;

namespace AncientMysteries.Items
{
    [EditorGroup(group_MapTools_Gameplay)]
    [MetaImage(tex_MapTools_Swirl)]
    [MetaInfo(Lang.english, "Outbound Killer", "Works well with Fixed Camera")]
    [MetaInfo(Lang.schinese, null, "与固定相机完美配合")]
    [MetaType(MetaType.MapTools)]
    public partial class OutboundKiller : AMMapToolGameplay
    {
        public OutboundKiller(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_MapTools_Swirl);
            _editorName = "Outbound Killer";
            editorTooltip = "Works well with Fixed Camera";
            _visibleInGame = false;
        }

        public override void Update()
        {
            if (!(Level.current is Editor))
            {
                foreach (Duck d in Level.current.things[typeof(Duck)])
                if (d.y > Level.current.camera.bottom || d.y < Level.current.camera.top || d.x > Level.current.camera.right || d.x < Level.current.camera.left)
                {
                        d.Destroy(new DTCrush(d));
                }
            }
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}