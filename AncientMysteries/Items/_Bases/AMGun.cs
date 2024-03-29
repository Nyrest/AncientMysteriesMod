﻿namespace AncientMysteries.Items
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
            editorTooltip = GetLocalizedDescription(AMLocalization.Current);
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

        public virtual void FirstUpdate()
        {
        }

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

        public void SetBarrelFlare(string flareTexReference)
        {
            _flare = this.ModSpriteWithFrames(flareTexReference, -1, -1, true);
            _flare.centerx = 0;
        }

        public Vec2 GetBarrelPosition(Vec2 barrelOffsetTL)
        {
            return Offset(barrelOffsetTL - center + _extraOffset);
        }

        public abstract string GetLocalizedName(Lang lang);

        public abstract string GetLocalizedDescription(Lang lang);
    }
}