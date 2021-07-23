using Microsoft.Xna.Framework.Graphics;

namespace AncientMysteries
{
    public static class GfxEffects
    {
        #region Passive Reverser Bindings

        // Avoid bugs cause permanent graphics effects
        private static readonly Dictionary<Func<bool>, Action> passiveReverserBindings = new();

        private static readonly List<Func<bool>> passiveReverserBindingsToRemove = new();

        #endregion Passive Reverser Bindings

        #region Defaults

        private static readonly BlendState DefaultBlendState = Graphics.device.BlendState;
        private static readonly Color DefaultBlendFactor = Graphics.device.BlendFactor;

        #endregion Defaults

        #region Effect Invert Color

        public static void EnableInvertColor(Thing binder) => EnableInvertColor(() => binder.removeFromLevel);

        public static void EnableInvertColor(Func<bool> binder)
        {
            passiveReverserBindings.Add(binder, DisableInvertColor);
            Graphics.device.BlendState.ColorDestinationBlend = Blend.InverseSourceColor;
        }

        public static void DisableInvertColor()
        {
            Graphics.device.BlendState.ColorDestinationBlend = DefaultBlendState.ColorDestinationBlend;
        }

        #endregion Effect Invert Color

        #region Effect Blend Factor

        public static void EnableBlendFactor(Thing binder, Color factor) => EnableBlendFactor(() => binder.removeFromLevel, factor);

        public static void EnableBlendFactor(Func<bool> binder, Color factor)
        {
            passiveReverserBindings.Add(binder, DisableBlendFactor);
            Graphics.device.BlendFactor = factor;
        }

        public static void DisableBlendFactor()
        {
            Graphics.device.BlendFactor = DefaultBlendFactor;
        }

        #endregion Effect Blend Factor

        public static void Update()
        {
            foreach (var passive in passiveReverserBindings)
            {
                if (passive.Key())
                {
                    passiveReverserBindingsToRemove.Add(passive.Key);
                }
            }

            #region MyRegion

            if (passiveReverserBindingsToRemove.Count != 0)
            {
                foreach (var item in passiveReverserBindingsToRemove)
                {
                    passiveReverserBindings.Remove(item);
                }
                passiveReverserBindingsToRemove.Clear();
            }

            #endregion MyRegion
        }
    }
}