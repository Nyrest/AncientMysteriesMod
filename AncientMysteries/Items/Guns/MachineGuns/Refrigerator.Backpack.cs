namespace AncientMysteries.Items
{
    public class Refrigerator_Backpack : Equipment
    {
        public StateBinding refrigeratorBinding = new StateBinding(nameof(refrigerator));
        public Refrigerator refrigerator;

        public Refrigerator_Backpack(float xpos, float ypos, Refrigerator refrigerator) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_Equipment_Refrigerator);
            _isArmor = false;
            enablePhysics = false;
            this.refrigerator = refrigerator;
            _equippedDepth = -12;
            // todo
            // adujust wearOffset
        }

        public override bool Destroy(DestroyType type = null) => false;
    }
}
