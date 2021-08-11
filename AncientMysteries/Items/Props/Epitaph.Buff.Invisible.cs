namespace AncientMysteries.Items
{
    public class Epitaph_Buff_Invisible : Equipment
    {
        public Waiter waiter = new(480);

        public Epitaph_Buff_Invisible(float xpos, float ypos) : base(xpos, ypos)
        {
            canPickUp = false;
            visible = false;
        }

        public override void Update()
        {
            base.Update();
            if (_equippedDuck is Duck equippedDuck)
            {
                if (!waiter.Tick())
                {
                    ApplyBuff(equippedDuck, true);
                }
                else
                {
                    ApplyBuff(equippedDuck, false);
                    Level.Remove(this);
                }
            }
            else
            {
                Level.Remove(this);
            }
        }

        public override void UnEquip()
        {
            base.UnEquip();
            if (_equippedDuck != null)
            {
                ApplyBuff(_equippedDuck, false);
            }
        }

        public void ApplyBuff(Duck duck, bool enable)
        {
            if (enable)
            {
                if (duck?.inputProfile?.virtualDevice is null)
                {
                    duck.alpha = 0.5f;
                    duck.material = new MaterialSelection();
                }
                else
                {
                    duck.alpha = 0;
                    duck.visible = false;
                }
            }
            else
            {
                if (duck?.inputProfile?.virtualDevice is null)
                {
                    duck.material = null;
                    duck.alpha = 1f;
                }
                else
                {
                    duck.alpha = 1;
                    duck.visible = true;
                }
            }
        }
    }
}