#region Assembly DuckGame, Version=1.1.7833.19655, Culture=neutral, PublicKeyToken=null
// D:\Steam\steamapps\common\Duck Game\DuckGame.exe
// Decompiled with ICSharpCode.Decompiler 6.1.0.5902
#endregion

using AncientMysteries;

namespace DuckGame
{
    public class AMQuadLaserBullet : Thing, ITeleport
    {
        public StateBinding _positionBinding = new CompressedVec2Binding("position", int.MaxValue, isvelocity: false, doLerp: true);

        public StateBinding _travelBinding = new CompressedVec2Binding("travel", 20);

        private Vec2 _travel;

        private SinWaveManualUpdate _wave = 0.5f;

        private SinWaveManualUpdate _wave2 = 1f;

        public int safeFrames;

        public Duck safeDuck;

        public float timeAlive;

        public static readonly Tex2D t = TexHelper.ModTex2D(t_NovaFrm);

        public Vec2 travel
        {
            get
            {
                return _travel;
            }
            set
            {
                _travel = value;
            }
        }

        public AMQuadLaserBullet(float xpos, float ypos, Vec2 travel)
            : base(xpos, ypos)
        {
            _travel = travel;
            collisionOffset = new Vec2(-1f, -1f);
            _collisionSize = new Vec2(2f, 2f);
        }

        public override void Update()
        {
            _wave.Update();
            _wave2.Update();
            timeAlive += 0.016f;
            position += _travel * 0.5f;
            if (base.isServerForObject && (base.x > Level.current.bottomRight.x + 200f || base.x < Level.current.topLeft.x - 200f))
            {
                Level.Remove(this);
            }

            foreach (MaterialThing item in Level.CheckRectAll<MaterialThing>(base.topLeft, base.bottomRight))
            {
                if ((safeFrames > 0 && item == safeDuck) || !item.isServerForObject)
                {
                    continue;
                }

                bool destroyed = item.destroyed;
                item.Destroy(new DTIncinerate(this));
                if (item.destroyed && !destroyed)
                {
                    if (Recorder.currentRecording != null)
                    {
                        Recorder.currentRecording.LogAction(2);
                    }

                    if (item is Duck && !(item as Duck).dead)
                    {
                        Recorder.currentRecording.LogBonus();
                    }
                }
            }

            if (safeFrames > 0)
            {
                safeFrames--;
            }

            base.Update();
        }

        public override void Draw()
        {
            Graphics.Draw(t, x, y);
            base.Draw();
        }
    }
}
#if false // Decompilation log
'14' items in cache
------------------
Resolve: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.1\mscorlib.dll'
------------------
Resolve: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.1\System.dll'
------------------
Resolve: 'System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Could not find by name: 'System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
------------------
Resolve: 'Microsoft.Xna.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553'
Found single assembly: 'Microsoft.Xna.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553'
Load from: 'F:\ancient\References\Microsoft.Xna.Framework.dll'
------------------
Resolve: 'Microsoft.Xna.Framework.Game, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553'
Found single assembly: 'Microsoft.Xna.Framework.Game, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553'
Load from: 'F:\ancient\References\Microsoft.Xna.Framework.Game.dll'
------------------
Resolve: 'System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.1\System.Core.dll'
------------------
Resolve: 'Microsoft.Xna.Framework.Graphics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553'
Found single assembly: 'Microsoft.Xna.Framework.Graphics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553'
Load from: 'F:\ancient\References\Microsoft.Xna.Framework.Graphics.dll'
------------------
Resolve: 'DGSteam, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'DGSteam, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
------------------
Resolve: 'System.IO.Compression, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Could not find by name: 'System.IO.Compression, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
------------------
Resolve: 'System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.1\System.Drawing.dll'
------------------
Resolve: 'System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.1\System.Xml.Linq.dll'
------------------
Resolve: 'System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.1\System.Xml.dll'
------------------
Resolve: 'DGInput, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'DGInput, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'
------------------
Resolve: 'NAudio, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'NAudio, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null'
------------------
Resolve: 'NVorbis, Version=0.8.4.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'NVorbis, Version=0.8.4.0, Culture=neutral, PublicKeyToken=null'
------------------
Resolve: 'System.Speech, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
Could not find by name: 'System.Speech, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
------------------
Resolve: 'CrashWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'CrashWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
------------------
Resolve: 'NVorbis.NAudioSupport, Version=0.5.6.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'NVorbis.NAudioSupport, Version=0.5.6.0, Culture=neutral, PublicKeyToken=null'
#endif
