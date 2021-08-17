using HarmonyLib;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace AncientMysteries;

public sealed unsafe class AncientMysteriesMod : Mod
{
    public bool Initialized { get; private set; }
    #region previewTextures
    private readonly Waiter _frameWaiter = new (5);
    private byte _currentPreviewFrame = 0;
    private Tex2D[] _previewTextures;
    public override Tex2D previewTexture
    {
        get
        {
            if (_previewTextures is null)
            {
                return base.previewTexture;
            }
            return _frameWaiter.Tick() 
                ? Helper.Switch(_previewTextures, ref _currentPreviewFrame) 
                : _previewTextures[_currentPreviewFrame];
        }
        protected set => base.previewTexture = value;
    }
    #endregion

    #region Configuration
    public Action<string> setDisplayName;
    public static float displayNameHue;
    public static bool displayNameHueReversed;
    #endregion

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        HookManager.Initialize();
        Hooks.Initialize();
        if (Debugger.IsAttached)
        {
            MonoMain.modDebugging = true;
        }
        Initialized = true;
        setDisplayName = (Action<string>)AccessTools.PropertySetter(typeof(ModConfiguration), nameof(configuration.displayName))
            .CreateDelegate(typeof(Action<string>), configuration);
    }

    protected override void OnPostInitialize()
    {
        base.OnPostInitialize();
        _previewTextures = new Tex2D[]
        {
            TexHelper.ModTex2D(tex_Preview_Frames_1),
            TexHelper.ModTex2D(tex_Preview_Frames_2),
            TexHelper.ModTex2D(tex_Preview_Frames_3),
            TexHelper.ModTex2D(tex_Preview_Frames_4),
            TexHelper.ModTex2D(tex_Preview_Frames_5),
        };
        (typeof(Game).GetField("updateableComponents", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(MonoMain.instance) as List<IUpdateable>).Add(new UpdateObject(x =>
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
        Hooks.OnUpdate += Hooks_OnUpdate;
    }

    private readonly FieldInfo _fieldLevelSelectItems = typeof(LevelSelect).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
    private readonly FieldInfo _fieldLevelSelectCompanionMenuLevelSelector = typeof(LevelSelectCompanionMenu).GetField("_levelSelector", BindingFlags.NonPublic | BindingFlags.Instance);
    private void Hooks_OnUpdate()
    {
        const float step = 0.009f;
        if (displayNameHueReversed)
        {
            if ((displayNameHue -= step) <= 0)
            {
                displayNameHue = 0;
                displayNameHueReversed = false;
            }
        }
        else
        {
            if ((displayNameHue += step) >= 1)
            {
                displayNameHue = 1;
                displayNameHueReversed = true;
            }
        }
        UpdateModDisplayName();
        void UpdateModDisplayName()
        {
            //SetDisplayName(AMStr($"{HSL.Hue(displayNameHue)}Ancient Mysteries"));
            AMStringHandler stringHandler = new(stackalloc char[30]);
            stringHandler.AppendDGColorString(HSL.Hue(displayNameHue));
            stringHandler.AppendLiteralNoGrow("Ancient Mysteries".AsSpan());
            setDisplayName(stringHandler.ToStringAndClear());
        }
    }

    public class UpdateObject : IUpdateable
    {
        public bool Enabled => true;

        public int UpdateOrder => 1;

        public Action<UpdateObject> action;

#pragma warning disable CS0067 // Unreachable code detected

        public event EventHandler<EventArgs> EnabledChanged;

        public event EventHandler<EventArgs> UpdateOrderChanged;

#pragma warning restore CS0067 // Unreachable code detected

        public UpdateObject()
        {
        }

        public UpdateObject(Action<UpdateObject> action)
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
