namespace AncientMysteries.Items
{
    [EditorGroup(group_Equipments)]
    [MetaImage(tex_Equipment_VampireWings, 30, 11, 1)]
    [MetaInfo(Lang.Default, "Vampire Wings", "They say when the blood moon rises, the nightmare will come")]
    [MetaInfo(Lang.schinese, "吸血鬼之翼", "他们说当血月升起时，梦魇就将来临")]
    [MetaType(MetaType.Equipment)]
    public partial class VampireWings : AMEquipmentWings
    {
        public VampireWings(float xpos, float ypos) : base(xpos, ypos)
        {
            _wingsSpriteMap = this.ReadyToRunWithFrames(tex_Equipment_VampireWings, 30, 11);
            _wingsSpriteMap.AddAnimation("loop", 0.2f, true, 0, 1, 2, 1);
            _wingsSpriteMap.AddAnimation("idle", 1f, true, 0);
            wearOffset = new(-0.5f, 1);
            _equippedDepth = -12;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Equip(Duck d)
        {
            base.Equip(d);
        }

        public override void UnEquip()
        {
            base.UnEquip();
        }
    }
}