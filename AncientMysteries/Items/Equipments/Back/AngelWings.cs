namespace AncientMysteries.Items
{
    [EditorGroup(group_Equipments)]
    [MetaImage(tex_Equipment_DemonWings, 28, 14, 1)]
    [MetaInfo(Lang.english, "Angel Wings", "「あぁ〜麻婆豆腐〜♪〜♪」")]
    [MetaInfo(Lang.schinese, "天使之翼", null)]
    public partial class AngelWings : AMEquipmentWings
    {
        public AngelWings(float xpos, float ypos) : base(xpos, ypos)
        {
            _wingsSpriteMap = this.ReadyToRunWithFrames(tex_Equipment_DemonWings, 28, 14);
            _wingsSpriteMap.AddAnimation("loop", 0.18f, true, 0, 1, 2, 1);
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
            _ => "「あぁ〜麻婆豆腐〜♪〜♪」",
        };

        public override void Update()
        {
            base.Update();
            if (this._equippedDuck is not Duck _equippedDuck) return;

            if (_equippedDuck.grounded && _equippedDuck.inputProfile.Down("JUMP"))
            {
                
            }

            PhysicsObject propel = _equippedDuck;
            if (_equippedDuck._trapped != null)
            {
                propel = _equippedDuck._trapped;
            }
            else if (_equippedDuck.ragdoll?.part1 != null)
            {
                propel = _equippedDuck.ragdoll.part1;
            }
            propel.velocity += GetFlyDir() * 1.5f;
        }

        public override void Equip(Duck d)
        {
            base.Equip(d);
            d.gravMultiplier = 0.2f;
        }

        public override void UnEquip()
        {
            base.UnEquip();
            d.gravMultiplier = 1f;
        }
    }
}