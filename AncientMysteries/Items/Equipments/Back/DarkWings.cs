﻿namespace AncientMysteries.Items
{
    [EditorGroup(group_Equipments)]
    [MetaImage(tex_Equipment_DarkWings, 43, 31, 1)]
    [MetaInfo(Lang.Default, "Dark Wings", "The wings, which pierces through the light, will eventually stretch, spreading fainted blessings.")]
    [MetaInfo(Lang.schinese, "漆黑之翼", "刺破光芒的羽翼终将伸展，撒下晦暗的恩泽。")]
    [MetaType(MetaType.Equipment)]
    [BaggedProperty("isSuperWeapon", true)]
    public partial class DarkWings : AMEquipmentWings
    {
        public DarkWings(float xpos, float ypos) : base(xpos, ypos)
        {
            _wingsSpriteMap = this.ReadyToRunWithFrames(tex_Equipment_DarkWings, 43, 31);
            _wingsSpriteMap.AddAnimation("loop", 0.2f, true, 1, 2, 3, 2);
            _wingsSpriteMap.AddAnimation("idle", 1f, true, 0);
            wearOffset = new(-0.5f, 1);
            _equippedDepth = -12;
        }
    }
}