namespace AncientMysteries.Items{
    [EditorGroup(group_Guns_MachineGuns)]
    public sealed class MoltenSpitter_AmmoType : AMAmmoType
    {
        public MoltenSpitter_AmmoType()
        {
            accuracy = 0.6f;
            range = 400f;
            penetration = 2f;
            rangeVariation = 10f;
            bulletLength = 0;
            combustable = true;
            bulletColor = Color.OrangeRed;
            sprite = TexHelper.ModSprite(tex_Bullet_Fireball2);
            sprite.CenterOrigin();
        }

        public override void OnHit(bool destroyed, Bullet b)
        {
            Level.Add(SmallFire.New(b.x, b.y, 0, 0));
            base.OnHit(destroyed, b);
        }
    }
}
