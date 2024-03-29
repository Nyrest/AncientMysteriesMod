﻿namespace AncientMysteries.Items
{
    [EditorGroup(group_Equipment_Developer)]
    [MetaImage(tex_Hat_ASUS, 32, 32)]
    [MetaInfo(Lang.Default, "ASUS", "Hard as rock.\nExcellent Artist / Cover Painter ")]
    [MetaInfo(Lang.schinese, null, "坚若磐石。\n神仙画师 / 封面画师 ")]
    [MetaOrder(int.MaxValue)]
    [MetaType(MetaType.Developer)]
    [BaggedProperty("isSuperWeapon", true)]
    public partial class ASUS : AMHelmet
    {
        public ASUS(float xpos, float ypos) : base(xpos, ypos)
        {
            //this.ReadyToRunWithFrames(tex_Hat_ASUS,32,32);
            _sprite = this.ModSpriteWithFrames(tex_Hat_ASUS, 32, 32);
            pickupSprite = this.ReadyToRunWithFrames(tex_Hat_ASUS, 32, 32);
        }

        public override void Update()
        {
            base.Update();
            if (duck != null && duck.quackStart)
            {
                foreach (Duck d in Level.current.things[typeof(Duck)])
                {
                    if (d != owner)
                    {
                        Paralyzed p = new(0, 0);
                        Level.Add(p);
                        d.Equip(p, false);
                    }
                }
                Level.Remove(this);
            }
        }
    }
}