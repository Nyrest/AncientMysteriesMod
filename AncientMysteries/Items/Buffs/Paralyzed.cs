namespace AncientMysteries.Items
{
    public partial class Paralyzed : Equipment
    {
        public Waiter waiter = new(360);

        public Paralyzed(float xpos, float ypos) : base(xpos, ypos)
        {
            canPickUp = false;
            visible = false;
        }

        public override void Update()
        {
            base.Update();
            if (_equippedDuck != null)
            {
                if (waiter.Tick())
                {
                    _equippedDuck.immobilized = false;
                    Level.Remove(this);
                }
                else
                {
                    _equippedDuck.immobilized = true;
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
                _equippedDuck.immobilized = false;
            }
        }
    }
}