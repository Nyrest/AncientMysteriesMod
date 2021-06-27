namespace AncientMysteries.Bullets
{
    public sealed class Bullet_Electronic : Bullet
    {
        public Bullet_Electronic(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            this._bulletSpeed = 4f;
        }

        public override void Draw()
        {
            ammo.sprite.depth = 1f;
            ammo.sprite.angleDegrees += 10;
            Graphics.Draw(ammo.sprite, position.x, position.y);
        }
    }
}
