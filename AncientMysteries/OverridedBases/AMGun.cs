namespace AncientMysteries
{
    public abstract class AMGun : Gun, IAMLocalizable
    {
        public static readonly FieldInfo _refBarrelSmoke = typeof(Gun).GetField("_barrelSmoke", BindingFlags.NonPublic | BindingFlags.Instance);

        public static AmmoType DefaultAmmoType => _ImplicitDefaultAmmoType.Instance;

        private bool _fisrtUpdate;

        public SpriteMap BarrelSmoke
        {
            get => (SpriteMap)_refBarrelSmoke.GetValue(this);
            set => _refBarrelSmoke.SetValue(this, value);
        }

        protected AMGun(float xval, float yval) : base(xval, yval)
        {
            _type = "gun";
            _ammoType = DefaultAmmoType;
            _editorName = GetLocalizedName(AMLocalization.Current);
        }

        public override void DoUpdate()
        {
            if (!_fisrtUpdate)
            {
                FirstUpdate();
                _fisrtUpdate = true;
            }
            base.DoUpdate();
        }

        public virtual void FirstUpdate() { }

        public void BarrelSmokeFuckOff()
        {
            var smoke = (SpriteMap)_refBarrelSmoke.GetValue(this);
            smoke.color = Color.Transparent;
        }

        /// <summary>
        /// Use this when collisionSize different with frame size
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public void SetBox(float w, float h)
        {
            collisionOffset = -(center = new Vec2(w / 2, h / 2));
            collisionSize = new Vec2(w, h);
        }

        public abstract string GetLocalizedName(AMLang lang);
    }
}
