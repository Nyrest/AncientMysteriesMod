using DuckGame;
using System;
using System.Linq;

namespace AncientMysteries.Utilities
{
    public static class Helper
    {
        public static Duck SwitchTarget(Duck current, Duck ignore)
        {
            Duck[] ducks = Level.current.things[typeof(Duck)]
            .Cast<Duck>()
            .OrderBy(x => x.persona is null ? 0 : Persona.Number(x.persona))
            .ToArray();
            int startIndex = Array.IndexOf(ducks, current) + 1;
            if (startIndex >= ducks.Length) startIndex = 0;
            for (; startIndex < ducks.Length; startIndex++)
            {
                var target = ducks[startIndex];
                if (!target.dead && target != ignore)
                {
                    return target;
                }
            }
            return null;
        }

        public static void SwitchTarget(ref Duck current, Duck ignore, bool playSound = true)
        {
            Duck[] ducks = Level.current.things[typeof(Duck)]
            .Cast<Duck>()
            .OrderBy(x => x.persona is null ? 0 : Persona.Number(x.persona))
            .ToArray();
            int startIndex = Array.IndexOf(ducks, current) + 1;
            if (startIndex >= ducks.Length) startIndex = 0;
            for (; startIndex < ducks.Length; startIndex++)
            {
                var target = ducks[startIndex];
                if (!target.dead && target != ignore)
                {
                    current = target;
                    if (playSound)
                        SFX.Play("swipe", 1f, 0.8f);
                    return;
                }
            }
            current = null;
        }
    }
}
