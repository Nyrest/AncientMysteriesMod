using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AncientMysteries
{
    public static class LevelPostDraw
    {
        private static readonly List<Action> drawQueue = new();

        static LevelPostDraw()
        {
            Hooks.LevelPostDraw += Flush;
        }

        public static void Draw(Action action)
        {
            drawQueue.Add(action);
        }

        public static void Flush(Level level)
        {
            if (Level.current is null) return;
            Graphics.screen.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, level.camera.getMatrix());
            for (int i = 0; i < drawQueue.Count; i++)
            {
                drawQueue[i]();
            }
            Graphics.screen.End();
            drawQueue.Clear();
        }
    }

    public static class Hooks
    {
        public static List<Thing> removedThings = new(256);

        public static bool _initialized;

        public static readonly Harmony harmony = new("FuckingAncientMysteriesMod");

        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;
            //
            (typeof(Game).GetField("updateableComponents", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(MonoMain.instance) as List<IUpdateable>).Add(new HookUpdate());
            (typeof(Game).GetField("drawableComponents", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(MonoMain.instance) as List<IDrawable>).Add(new HookDraw());

            harmony.Patch(AccessTools.Method(typeof(Level), nameof(Level.DoDraw)), null, new HarmonyMethod(AccessTools.Method(typeof(Hooks), nameof(DoDraw))));
        }

        public static event Action OnUpdate;

        public static event Action OnDraw;

        public static event Action<Level> LevelPostDraw;

        public static void DoDraw(Level __instance)
        {
            LevelPostDraw?.Invoke(__instance);
        }

        private sealed class HookDraw : IDrawable
        {
            public bool Visible => true;
            public int DrawOrder => int.MaxValue;

#pragma warning disable CS0067 // Unreachable code detected

            public event EventHandler<EventArgs> VisibleChanged;

            public event EventHandler<EventArgs> DrawOrderChanged;

#pragma warning restore CS0067 // Unreachable code detected

            public void Draw(GameTime gameTime)
            {
                OnDraw?.Invoke();
            }
        }

        private sealed class HookUpdate : IUpdateable
        {
            public bool Enabled => true;
            public int UpdateOrder => 0;

#pragma warning disable CS0067 // Unreachable code detected

            public event EventHandler<EventArgs> EnabledChanged;

            public event EventHandler<EventArgs> UpdateOrderChanged;

#pragma warning restore CS0067 // Unreachable code detected

            public void Update(GameTime gameTime)
            {
                OnUpdate?.Invoke();
                removedThings.Clear();
                if (Level.current?.things is QuadTreeObjectList things)
                {
                    foreach (var item in things[typeof(Thing)])
                    {
                        if (item.removeFromLevel)
                            removedThings.Add(item);
                    }
                }
            }
        }
    }
}