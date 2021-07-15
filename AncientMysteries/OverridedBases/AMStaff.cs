namespace AncientMysteries
{
    public abstract class AMStaff : AMGun, IAMLocalizable
    {
        public StateBinding _castTimeBinding = new("_castTime");

        public StateBinding _castWaitValBinding = new("_castWaitVal");

        public float _holdAngle = 0.8f;

        public float _fireAngle = 1.1f;

        /// <summary>
        /// modify this
        /// </summary>
        public float _castSpeed = 0.04f;

        /// <summary>
        /// 0-1
        /// </summary>
        public float _castTime = 0f;

        public float _castWaitVal = 0f;

        public float _castWait = 1f;

        public bool _doPose = true;

        public bool IsSpelling
        {
            get
            {
                return _castTime != 0f;
            }
        }

        public AMStaff(float xval, float yval) : base(xval, yval)
        {
            _ammoType = new AT_None();
            _type = "gun";
            ammo = 4;
            _flare.color = Color.Transparent;
            _holdOffset = new Vec2(-5, -3);
            BarrelSmokeFuckOff();
            _fireSound = "laserRifle";
            _fireSoundPitch = 0.9f;
            _fullAuto = true;
            _editorName = GetLocalizedName(AMLocalization.Current);
        }

        public virtual void OnSpelling()
        {
        }

        public virtual void EndSpelling()
        {
        }

        public virtual void OnReleaseSpell()
        {
        }

        public override sealed void Fire()
        {
        }

        public override void OnReleaseAction()
        {
            base.OnReleaseAction();
            if (_castWaitVal == 0)
            {
                bool flag = duck != null;
                if (flag)
                {
                    OnReleaseSpell();
                    EndSpelling();
                    _castWaitVal = _castWait;
                }
                _castTime = 0f;
            }
        }

        public override void Update()
        {
            base.Update();
            if (action && _castWaitVal == 0)
            {
                if (_castTime <= 1)
                    _castTime += _castSpeed;
                else _castTime = 1;
            }
            if (_castWaitVal > 0)
            {
                _castWaitVal -= 0.04f;
            }
            else
            {
                _castWaitVal = 0;
            }
            if (duck != null)
            {
                if (_doPose)
                    handAngle = offDir * MathHelper.Lerp(_holdAngle, _fireAngle, _castTime);
                else handAngle = 0;
            }
            if(castingParticlesEnabled)
            {
                DoCastingParticles();
            }
            if (drawProgressBar)
                UpdateProgressStyle();
        }

        public virtual void DoCastingParticles()
        {

        }

        #region Progress Bar
        public bool drawProgressBar = true;

        public Color progressBgColor = Color.White;

        public Color progressFillColor = Color.Red;

        public Color progressBorderColor = Color.Black;

        public bool castingParticlesEnabled = false;

        public Color castingParticlesColor = Color.Black;

        public Vec2 castingParticlesOffset = default;

        public float progressBorderWidth = 1f;

        public float progressWidth = 15f;

        public float progressHeight = 4f;

        public override void DoDraw()
        {
            base.DoDraw();
            if (drawProgressBar && duck?.profile.localPlayer == true)
            {
                GTool.DrawTopProgressCenterTop(duck.position, _castTime, progressBgColor, progressFillColor, progressBorderColor, progressBorderWidth, -13, progressWidth, progressHeight, 1);
            }
        }

        public virtual void UpdateProgressStyle()
        {
            progressFillColor = _castTime >= 1 ? Color.Orange : new Color((byte)(_castTime * 255), (byte)0, (byte)0);
        }
        #endregion
    }
}
