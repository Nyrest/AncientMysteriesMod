﻿namespace AncientMysteries.Items
{
    [EditorGroup(group_MapTools_Gameplay)]
    [MetaImage(tex_MapTools_Swirl)]
    [MetaInfo(Lang.Default, "Adjustment - Duck Size", "Allow to adjust the duck size multiplier.")]
    [MetaInfo(Lang.schinese, "鸭子大小调整", "使地图制作者可以调整鸭子大小系数。")]
    [MetaType(MetaType.MapTools)]
    public partial class DuckSizeAdjustment : AMMapToolGameplay
    {
        public EditorProperty<float> Size = new(1, null, 0.5f, 5, 0.1f)
        {
            name = "Duck Size Multiplier",
            _tooltip = "Size Multiplier of duck"
        };

        public EditorProperty<bool> ForceApply = new(false)
        {
            name = "Force Apply",
            _tooltip = "Try to override all DuckSize multiplier change"
        };

        public DuckSizeAdjustment(float xpos, float ypos) : base(xpos, ypos)
        {
        }

        public override void Update()
        {
            base.Update();
            foreach (Duck item in Level.current.things[typeof(Duck)])
            {
                if (item.duckSize == 1 || ForceApply)
                    item.duckSize = Size;
            }
        }
    }
}