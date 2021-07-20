using System;

namespace DuckGame
{
    [EditorGroup(group_)]
    public class CameraFixed : CustomCamera
    {
        public EditorProperty<int> Size = new EditorProperty<int>(320, null, 60f, 1920f, 1f);

        public CameraFixed()
        {
            _contextMenuFilter.Add("wide");
            _editorName = "Quadlaser Killer";
            editorTooltip = "Perfect for those who wants to kill laggy things.";
            Size._tooltip = "The size of killing quad lasers (in pixel)";
            graphic = new Sprite("cameraIcon");
            collisionSize = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-4f, -4f);
            _visibleInGame = false;
        }

        public override void Initialize()
        {
            wide.value = Size.value;
            base.Initialize();
        }

        public override void Update()
        {
            if (!(Level.current is GameLevel) || GameMode.started)
            {
                float num = wide.value;
                float num2 = num * 0.5625f;
                foreach (QuadLaserBullet q in Level.CheckRectAll<QuadLaserBullet>(position + new Vec2((0f - num) / 2f, (0f - num2) / 2f), position + new Vec2(num / 2f, num2 / 2f)))
                {
                    Level.Remove(q);
                }
            }
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}