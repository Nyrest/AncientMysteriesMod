using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace AncientMysteries
{
    public sealed class AncientMysteriesMod : Mod
    {
        protected override unsafe void OnPreInitialize()
        {
            base.OnPreInitialize();
            Hooks.Initialize();
            if (Debugger.IsAttached)
            {
                MonoMain.modDebugging = true;
            }
            var oldMethod = typeof(Program).GetMethod("ModResolve").MethodHandle;
            var newMethod = typeof(LightweightDependencyResolver).GetMethod("ModResolve").MethodHandle;
            RuntimeHelpers.PrepareMethod(oldMethod);
            RuntimeHelpers.PrepareMethod(newMethod);
            *((int*)oldMethod.Value.ToPointer() + 2) = *((int*)newMethod.Value.ToPointer() + 2);
        }

        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();
            (typeof(Game).GetField("updateableComponents", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(MonoMain.instance) as List<IUpdateable>).Add(new updateObject(x =>
            {
                foreach (var modTopGroup in Editor.Placeables.SubGroups)
                {
                    if (modTopGroup.Name.Equals("Ancient"))
                    {
                        modTopGroup.Name = "@HOSTCROWN@|DGORANGE|Ancient";
                        ReplaceAllSub(modTopGroup);
                        break;
                    }
                }
                static void ReplaceAllSub(EditorGroup group)
                {
                    var subGroups = group.SubGroups;
                    for (int i = subGroups.Count - 1; i >= 0; i--)
                    {
                        EditorGroup g = subGroups[i];
                        g.Name = g.Name switch
                        {
                            "Artifact" => "|ORANGE|Artifact",
                            "Dragon" => "Dragon",
                            "Developers" => "|LIME|Developers",
                            "Electronic" => "|DGYELLOW|Electronic",
                            "Isekai" => "Isekai @PLANET@",
                            "Hats" => "|PINK|Hats",
                            "Debug" => "|DGRED|Debug",
                            "WTF" => "|DGRED|WTF?!",
                            _ => g.Name.Replace('%', '|'),
                        };
                        ReplaceAllSub(g);
                    }
                }
            }));
        }

        public class updateObject : IUpdateable
        {
            public bool Enabled => true;

            public int UpdateOrder => 1;

            public Action<updateObject> action;

#pragma warning disable CS0067 // Unreachable code detected

            public event EventHandler<EventArgs> EnabledChanged;

            public event EventHandler<EventArgs> UpdateOrderChanged;

#pragma warning restore CS0067 // Unreachable code detected

            public updateObject()
            {
            }

            public updateObject(Action<updateObject> action)
            {
                this.action = action;
            }

            public void Update(GameTime gameTime)
            {
                action.Invoke(this);
                RemoveThis();
            }

            public void RemoveThis()
            {
                (typeof(Game).GetField("updateableComponents", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic).GetValue(MonoMain.instance) as List<IUpdateable>).Remove(this);
            }
        }
    }
}