using DuckGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AncientMysteries.groupNames;

namespace AncientMysteries.Items.Mortal.props
{
    [EditorGroup(topAndSeries + "Mortal|Props")]
    public sealed class Dice : Holdable, IPlatform
    {
        public static int index = 0;
        public static Vec2 pos;
        public static bool isDrawn = false;
        public static Duck du;
        public static int boo = 0;

        public Dice()
        {
            this.ReadyToRun("rainbowGun.png");
        }
        public override void OnPressAction()
        {
            base.OnPressAction();
            var ducks = Level.current.things[typeof(Duck)].Cast<Duck>().Where(d => !d.dead);
            boo = Rando.Int(0, ducks.Count());
            int duckIndex = 0;
            foreach (Duck d in ducks)
            {
                duckIndex++;
                if (duckIndex == boo)
                {
                    pos = d.position;
                    for (int i = 0; i < 10; i++)
                    {
                        Level.Add(SmallFire.New(d.x, d.y, Rando.Float(-5, 5), Rando.Float(-5, 5)));
                    }
                    isDrawn = true;
                    du = d;
                    d.Kill(new DTCrush(this));
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (isDrawn)
            {
                Graphics.DrawString(Persona.Number(du.persona).ToString(), new Vec2(pos.x, pos.y - 20), Color.Red);
            }
            isDrawn = false;
        }
    }
}
