namespace AncientMysteries.Items.Guns.MachineGuns
{
    public class Iridescence_Bullet : Bullet
    {
        public Iridescence_Bullet(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network) { }

        public override void Update()
        {
            base.Update();
            if (color == Color.White)
            {
                color = HSL.FromHslFloat(Rando.Float(0f, 1f), Rando.Float(0.7f, 1f), Rando.Float(0.45f, 0.65f));
            }
        }
    }
}
