namespace AncientMysteries.Items
{
    [EditorGroup(group_MapTools)]
    [MetaImage(tex_MapTools_Swirl)]
    [MetaInfo(Lang.Default, "Adjustment - Gravity", "Allow to adjust the level gravity multiplier")]
    [MetaInfo(Lang.schinese, "重力调整", "使地图制作者可以调整关卡重力系数")]
    [MetaType(MetaType.MapTools)]
    public partial class GravityAdjustment : AMMapToolGameplay
    {
        public EditorProperty<float> GravityMultiplier = new(1, null, -20, 20, 0.1f)
        {
            name = "Gravity Multiplier",
            _tooltip = "Gravity Multiplier of everything in this level"
        };

        public EditorProperty<bool> ForceApply = new(false)
        {
            name = "Force Apply",
            _tooltip = "Try to override all gravity multiplier change"
        };

        public GravityAdjustment(float xpos, float ypos) : base(xpos, ypos)
        {

        }

        public override void Update()
        {
            base.Update();
            foreach (PhysicsObject item in Level.current.things[typeof(PhysicsObject)])
            {
                if (item.gravMultiplier == 1 || ForceApply)
                    item.gravMultiplier = GravityMultiplier;
            }
        }
    }
}
