using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using DuckGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AncientMysteries
{
    public static class Hooks
    {
        private static FieldInfo QuadTreeObjectList_removeThings = typeof(QuadTreeObjectList).GetField("_removeThings", BindingFlags.NonPublic | BindingFlags.Instance);

        public static List<Thing> removedThings = new(256);

        public static bool _initialized;

        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;
            // 
            (typeof(Game).GetField("updateableComponents", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(MonoMain.instance) as List<IUpdateable>).Add(new HookUpdate());
            (typeof(Game).GetField("drawableComponents", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(MonoMain.instance) as List<IDrawable>).Add(new HookDraw());
        }

        public static event Action OnUpdate;

        public static event Action OnDraw;

        private sealed class HookDraw : IDrawable
        {
            public bool Visible => true;
            public int DrawOrder => 0;

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
                        if(item.removeFromLevel)
                        removedThings.Add(item);
                    }
                }
            }
        }
    }
}