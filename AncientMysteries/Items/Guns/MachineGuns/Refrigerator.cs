namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_MachineGuns)]
    [MetaImage(tex_Gun_Refrigerator)]
    [MetaInfo(Lang.Default, "Refrigerator", "The ice freezed within is hard enough to be a bullet.")]
    [MetaInfo(Lang.schinese, "冰箱", "其冷冻的冰块用来当子弹也不为过。")]
    [MetaType(MetaType.Gun)]
    public partial class Refrigerator : AMGun
    {
        public StateBinding backpackBinding = new StateBinding(nameof(backpack));
        public Refrigerator_Backpack backpack;

        public Refrigerator(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRunWithFrames(tex_Gun_Refrigerator);
            this._barrelOffsetTL = new Vec2(21, 3);
            _holdOffset = new Vec2(-3, 0);
            _ammoType = new Refrigerator_AmmoType();
            ammo = 100;
            _fireWait = 0.8f;
            _fullAuto = true;
            _kickForce = 0.9f;
            weight = 10;
        }

        public override void Update()
        {
            if (backpack == null && !(Level.current is Editor) && isServerForObject)
            {
                backpack = new Refrigerator_Backpack(x, y, this)
                {
                    visible = false,
                };
                Level.Add(backpack);
            }
            base.Update();
            if (backpack != null && isServerForObject)
            {
                if (duck is Duck d)
                {
                    if (!d.HasEquipment(backpack))
                    {
                        d.Equip(backpack);
                    }
                    backpack.visible = true;
                }
                else
                {
                    if (backpack?.equippedDuck is Duck equippedDuck)
                    {
                        equippedDuck.Unequip(backpack);
                    }
                    backpack.UnEquip();
                    backpack.visible = false;
                }
            }
        }
    }
}