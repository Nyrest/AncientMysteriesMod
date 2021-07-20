namespace AncientMysteries.Items
{
    [EditorGroup(group_Equipments)]
    [MetaImage(tex_Equipment_DemonWings, 28, 14, 1)]
    [MetaInfo(Lang.english, "Angel Wings", "「また 60 億分の 1 の確率で出会えたら・・・」")]
    [MetaInfo(Lang.schinese, "天使之翼", null)]
    [MetaType(MetaType.Equipment)]
    public partial class AngelWings : AMEquipmentWings
    {
        public AngelWings(float xpos, float ypos) : base(xpos, ypos)
        {
            _wingsSpriteMap = this.ReadyToRunWithFrames(tex_Equipment_DemonWings, 28, 14);
            _wingsSpriteMap.AddAnimation("loop", 0.15f, true, 0, 1, 2, 1);
            _wingsSpriteMap.AddAnimation("idle", 1f, true, 0);
            wearOffset = new(-0.5f, -1);
            _equippedDepth = -2;

        }

        public override string GetLocalizedName(Lang lang) => lang switch
        {
            Lang.schinese => "天使之翼",
            _ => "Angel Wings",
        };

        public override string GetLocalizedDescription(Lang lang) => lang switch
        {
            _ => "「また 60 億分の 1 の確率で出会えたら・・・」",
        };

        public override void Update()
        {
            base.Update();
            if (this._equippedDuck is not Duck _equippedDuck) return;


        }

        public override void Equip(Duck d)
        {
            base.Equip(d);
        }

        public override void UnEquip()
        {
            if (_equippedDuck is not null)
                _equippedDuck.gravMultiplier = 1f;
            base.UnEquip();
        }
    }
}