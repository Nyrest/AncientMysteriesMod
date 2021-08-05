#nullable enable

namespace AncientMysteries.Items
{
    [EditorGroup(group_Guns_Melees)]
    [MetaImage(tex_Melee_PowerFist)]
    [MetaInfo(Lang.Default, "Power Fist", "Deja Vu ♫.")]
    [MetaInfo(Lang.schinese, "超载铁拳", "逮虾户！！")]
    [MetaType(MetaType.Melee)]
    public partial class PowerFist : AMHoldable
    {
        public const float maxDashTime = 15; // in ticks
        public float dashTime = -1; // in ticks, -1 = not started
        public Waiter chargeWaiter = new(90);
        public bool charged = false;
        public const float dashSpeed = 20;

        public bool Dashing
        {
            get => duck is not null && dashTime >= 0;
            set
            {
                if (!value)
                {
                    dashTime = -1;
                    if (duck != null)
                    {
                        duck.immobilized = false;
                    }
                    return;
                }
                if (dashTime < 0)
                    dashTime = 0;
            }
        }

        // Clone the template
        public ColorTrajectory trajectory1;
        public ColorTrajectory trajectory2;
        public ColorTrajectory trajectory3;

        public PowerFist(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_Melee_PowerFist);
            trajectory1 = new(this, new(1, 6)) { Color = Color.Red };
            trajectory2 = new(this, new(1, 8)) { Color = Color.Red };
            trajectory3 = new(this, new(1, 10)) { Color = Color.Red };
        }

        public override void Update()
        {
            base.Update();
            if (Dashing)
            {
                if (dashTime++ >= maxDashTime)
                {
                    Dashing = false;
                    return;
                }
                var vel = Maths.AngleToVec(angle) * dashSpeed;
                vel.x *= offDir;
                vel.y *= -offDir;
                duck.velocity = vel;
                foreach (MaterialThing item in Level.CheckCircleAll<MaterialThing>(duck.position, 30))
                {
                    if (item is not PowerFist && item as Duck != duck)
                    {
                        switch (item)
                        {
                            case Window window:
                                {
                                    window.Destroy(new DTImpact(duck));
                                    break;
                                }
                            case Door door when door.locked is false:
                                {
                                    // TODO: use DTShot to make velocity
                                    door.Destroy(new DTImpact(duck));
                                    break;
                                }
                            case MaterialThing mat:
                                {

                                    mat.Destroy(new DTImpact(duck));
                                    break;
                                }
                            default: break;
                        }
                    }
                }
                if (CheckCollide(out var collideWith))
                {
                    switch (collideWith)
                    {
                        case Block fuckingBlock:
                            {
                                WorldHelper.DestroyBlocksRadius(
                                    fuckingBlock.position,
                                    35,
                                    culprit: duck,
                                    true,
                                    true,
                                    // culprit will not be destroyed
                                    true,
                                    false);
                                // stop dashing
                                Dashing = false;
                                ExplosionPart exp = new(fuckingBlock.position.x, fuckingBlock.position.y);
                                exp.scale = new(1.5f, 1.5f);
                                SFX.PlaySynchronized("explode", 1, 0.1f);
                                Level.Add(exp);
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
            trajectory1.Update();
            trajectory2.Update();
            trajectory3.Update();
        }

        public override void Draw()
        {
            base.Draw();
            trajectory1.Draw();
            trajectory2.Draw();
            trajectory3.Draw();
            if (duck is not null && chargeWaiter.CurrentFrame != 0)
            {
                LevelPostDraw.Draw(() =>
                {
                    duck.DrawProgressBarBottom(
        charged ? 1 : chargeWaiter.ToProgress(),
        Color.White,
        charged ? Color.Orange : new Color(254, 67, 69),
        new Color(61, 57, 58));
                });
            }
        }

        public override void PressAction()
        {
            base.PressAction();
        }

        public override void OnReleaseAction()
        {
            base.OnReleaseAction();
            chargeWaiter.Reset();
            if (this.duck is Duck duck && charged)
            {
                duck.immobilized = true;
                charged = false;
                Dash();
            }
        }

        public override void OnHoldAction()
        {
            base.OnHoldAction();
            if (chargeWaiter.Tick() && charged == false)
            {
                charged = true;
                SFX.PlaySynchronized("targetDing", 1, 0.3f);
            }
        }

        public bool CheckCollide([NotNullWhen(true)] out MaterialThing? collideWith) =>
            (collideWith = duck?.collideLeft) is not null ||
            (collideWith = duck?.collideRight) is not null;

        public void Dash()
        {
            Dashing = true;
        }
    }
}
