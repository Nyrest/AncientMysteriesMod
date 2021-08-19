namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_MachineGuns)]
    [MetaImage(tex_Gun_NeonStriker)]
    [MetaInfo(Lang.Default, "Neon Striker", "Those bright things gonna shine through the world.")]
    [MetaInfo(Lang.schinese, "霓虹打击", "这些明亮的小东西将要照亮整个世界。")]
    [MetaType(MetaType.Gun)]
    public partial class NeonStriker : AMThingBulletGun
    {
        public NeonStriker(float xval, float yval) : base(xval, yval)
        {
            this.ReadyToRun(tex_Gun_NeonStriker);
            ammo = 30;
            _fireWait = 1.1f;
            _fullAuto = true;
            _flare.color = Color.Blue;
            _barrelOffsetTL = new Vec2(22, 5.5f);
            _holdOffset = new(-3.7f, -1);
        }

        public override IEnumerable<AMThingBulletBase> FireThingBullets(float shootAngleDeg)
        {
            yield return new NeonStriker_ThingBullet_Blue(
                GetBarrelPosition(new Vec2(22, 5.5f)),
                GetBulletVecDeg(shootAngleDeg, 6, 1, 0.85f),
                duck,
                true);
            yield return new NeonStriker_ThingBullet_Purple(
                GetBarrelPosition(new Vec2(22, 5.5f)),
                GetBulletVecDeg(shootAngleDeg, 6, 1, 0.85f),
                duck,
                true);
        }
    }
}