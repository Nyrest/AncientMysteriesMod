#nullable enable

namespace AncientMysteries.Items
{
    [EditorGroup(group_Decorations_Lights)]
    [MetaImage(tex_Decoration_PointLight)]
    [MetaInfo(Lang.english, "Point Light", "Highly Customizable Point Light")]
    [MetaInfo(Lang.schinese, "点光源", "高度可定制的点光源")]
    [MetaType(MetaType.Decoration)]
    public partial class PointLight2 : AMDecoration
    {
        private readonly List<LightOccluder> _occluders = new List<LightOccluder>();

        public float lightRange = 80;

        public byte R = 255;
        public byte G = 255;
        public byte B = 180;
        public byte A = 255;

        public EditorProperty<int> property_ColorR;
        public EditorProperty<int> property_ColorG;
        public EditorProperty<int> property_ColorB;
        public EditorProperty<int> property_ColorA;

        public EditorProperty<float> property_LightRange;

        public Color LightColor
        {
            get => new(R, G, B, A);
            set
            {
                R = value.r;
                G = value.g;
                B = value.b;
                A = value.a;
            }
        }

        public PointLight? light;

        public PointLight2(float xpos, float ypos) : base(xpos, ypos)
        {
            this.ReadyToRun(tex_Decoration_PointLight);
            graphic.CenterOrigin();
            property_ColorR = new EditorProperty<int>(R, this, 0, 255, 1)
            {
                name = "Red",
                _tooltip = "Red channel of the light color",
            };
            property_ColorG = new EditorProperty<int>(G, this, 0, 255, 1)
            {
                name = "Green",
                _tooltip = "Green channel of the light color",
            };
            property_ColorB = new EditorProperty<int>(B, this, 0, 255, 1)
            {
                name = "Blue",
                _tooltip = "Blue channel of the light color",
            };
            property_ColorA = new EditorProperty<int>(A, this, 0, 255, 1)
            {
                name = "Alpha",
                _tooltip = "Alpha channel of the light color",
            };
            property_LightRange = new EditorProperty<float>(lightRange, this, 1, 512, 1)
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
            R = (byte)property_ColorR;
            G = (byte)property_ColorG;
            B = (byte)property_ColorB;
            A = (byte)property_ColorA;
            lightRange = property_LightRange;
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
            light = new PointLight(x, y, LightColor, lightRange, _occluders);
            Level.Add(light);
        }

        public override void Draw()
        {
            Graphics.DrawCircle(position, lightRange, LightColor);
            base.Draw();
        }
    }
}
