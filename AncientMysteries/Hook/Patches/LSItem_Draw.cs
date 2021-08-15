using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace AncientMysteries.Hook.Patches
{
    [HarmonyPatch(typeof(LSItem), nameof(LSItem.Draw))]
    public class LSItem_Draw
    {
        const string modItemName = "AncientMysteries";

        static void Prefix(LSItem __instance, ref bool __state)
        {
            __state = false;
            if (__instance._name == modItemName)
            {
                __instance._name = string.Empty;
                __state = true;
            }
        }

        static void Postfix(LSItem __instance, ref bool __state, BitmapFont ____font, bool ____selected)
        {
            if (__state)
            {
                float xDraw = __instance.x + 10 + 10;

                ____font.Draw(GetName(____selected ? 0.5f : 0.7f), xDraw, __instance.y, Color.White, 0.8f);
                __instance._name = modItemName;
            }

            static string GetName(float highlight)
            {
                AMStringHandler stringHandler = new(stackalloc char[13 + 14]);
                stringHandler.AppendDGColorString(HSL.Hue(AncientMysteriesMod.displayNameHue, highlight));
                stringHandler.AppendLiteralNoGrow("Ancient Levels".AsSpan());
                return stringHandler.ToStringAndClear();
            }
        }
    }
}
