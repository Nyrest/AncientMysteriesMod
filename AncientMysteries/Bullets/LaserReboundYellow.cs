namespace AncientMysteries.Bullets
{
    public class LaserReboundYellow : Thing
    {
        private Tex2D _rebound = TexHelper.ModTex2D(tex_Bullet_LaserRebound);

        public LaserReboundYellow(float xpos, float ypos)
            : base(xpos, ypos)
        {
            graphic = new Sprite(_rebound);
            base.depth = 0.9f;
            center = new Vec2(4f, 4f);
        }

        public override void Update()
        {
            base.alpha -= 0.07f;
            if (base.alpha <= 0f)
            {
                Level.Remove(this);
            }
        }
    }
}