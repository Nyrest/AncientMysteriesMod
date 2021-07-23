#nullable enable

namespace AncientMysteries.Items
{
    [EditorGroup(group_MapTools_Lights)]
    [MetaImage(tex_Decoration_PointLight)]
    [MetaInfo(Lang.english, "Point Light", "Highly Customizable Point Light")]
    [MetaInfo(Lang.schinese, "点光源", "高度可定制的点光源")]
    [MetaType(MetaType.MapTools)]
    public partial class PointLight2 : AMMapTool
    {
        private readonly List<LightOccluder> _occluders = new List<LightOccluder>();

        public float val_LightRange = 80;

        public EditorProperty<int> Red;
        public EditorProperty<int> Green;
        public EditorProperty<int> Blue;
        public EditorProperty<int> Alpha;
        public EditorProperty<float> LightRange;

        public byte val_R = 255;
        public byte val_G = 255;
        public byte val_B = 180;
        public byte val_A = 255;

        public Color LightColor
        {
            get => new(val_R, val_G, val_B, val_A);
            set
            {
                val_R = value.r;
                val_G = value.g;
                val_B = value.b;
                val_A = value.a;
            }
        }

        public PointLight? light;

        public PointLight2(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_Decoration_PointLight);
            _editorName = "Point Light";
            graphic.CenterOrigin();
            Red = new EditorProperty<int>(val_R, this, 0, 255, 1)
            {
                name = "Red",
                _tooltip = "Red channel of the light color",
            };
            Green = new EditorProperty<int>(val_G, this, 0, 255, 1)
            {
                name = "Green",
                _tooltip = "Green channel of the light color",
            };
            Blue = new EditorProperty<int>(val_B, this, 0, 255, 1)
            {
                name = "Blue",
                _tooltip = "Blue channel of the light color",
            };
            Alpha = new EditorProperty<int>(val_A, this, 0, 255, 1)
            {
                name = "Alpha",
                _tooltip = "Alpha channel of the light color",
            };
            LightRange = new EditorProperty<float>(val_LightRange, this, 1, 512, 1)
            {
                name = "Range",
                _tooltip = "Range of the light",
            };
            depth = 0.9f;
            layer = Layer.Game;
            _visibleInGame = false;
            graphic.color = LightColor;
        }

        public override void EditorPropertyChanged(object property)
        {
            val_R = (byte)Red;
            val_G = (byte)Green;
            val_B = (byte)Blue;
            val_A = (byte)Alpha;
            val_LightRange = LightRange;
            graphic.color = LightColor;
        }

        public override void Initialize()
        {
            UpdateLight();
        }

        public void UpdateLight()
        {
            _occluders.Clear();
            if (light is null)
            {
                Level.Remove(light);
            }
            light = new PointLight(x, y, LightColor, val_LightRange, _occluders);
            Level.Add(light);
        }

        public override void Draw()
        {
            Graphics.DrawCircle(position, val_LightRange, LightColor);
            base.Draw();
        }
    }
}