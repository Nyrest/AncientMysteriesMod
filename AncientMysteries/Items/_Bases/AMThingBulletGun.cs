using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Items
{
    public abstract class AMThingBulletGun : AMGun
    {
        protected AMThingBulletGun(float xval, float yval) : base(xval, yval)
        {
        }

        public override sealed void Fire()
        {
            if (!loaded) return;
            if (_wait != 0) return;
            if (ammo <= 0 && _wait == 0f)
            {
                DoAmmoClick();
                _wait = _fireWait;
                return;
            }
            if (duck != null)
            {
                RumbleManager.AddRumbleEvent(base.duck.profile, new RumbleEvent(_fireRumble, RumbleDuration.Pulse, RumbleFalloff.None));
            }
            if (isServerForObject)
            {
                DoFireThingBullets();
            }
            _smokeWait = 3f;
            loaded = false;
            _flareAlpha = 1.5f;
            if (!_manualLoad)
            {
                Reload(true);
            }
            firing = true;
            _wait = _fireWait;
            if (owner == null)
            {
                Vec2 fly = barrelVector * Rando.Float(1f, 3f);
                fly.y += Rando.Float(2f);
                hSpeed -= fly.x;
                vSpeed -= fly.y;
            }
            _accuracyLost += loseAccuracy;
            if (_accuracyLost > maxAccuracyLost)
            {
                _accuracyLost = maxAccuracyLost;
            }

            if (!isServerForObject) return;

            PlayFireSound();
            ApplyKick();
        }

        public virtual void DoFireThingBullets() 
        {
            float shootAngleDeg = angleDegrees;
            if (offDir < 0)
            {
                shootAngleDeg += 180f;
            }
            foreach (var item in FireThingBullets(shootAngleDeg))
            {
                Level.Add(item);
                if (Network.isActive && isServerForObject && duck != null && duck.profile.connection != null)
                {
                    item.connection = duck.profile.connection;
                }
            }
        }

        public abstract IEnumerable<AMThingBulletBase> FireThingBullets(float shootAngleDeg);

        public virtual void FireRumbleEvent()
        {
            RumbleManager.AddRumbleEvent(duck.profile, new RumbleEvent(_fireRumble, RumbleDuration.Pulse, RumbleFalloff.None));
        }
    }
}
