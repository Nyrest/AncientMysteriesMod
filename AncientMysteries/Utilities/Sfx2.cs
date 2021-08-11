namespace AncientMysteries.Utilities
{
    public static partial class SFX2
    {
        public static Sound PlayMod(string sound, float vol = 1, float pitch = 0, float pan = 0, bool looped = false)
        {
            string fullName = Mod.GetPath<AncientMysteriesMod>(sound);
            return Play(fullName, vol, pitch, pan, looped);
        }

        public static Sound PlayModSynchronized(string sound, float vol = 1, float pitch = 0, float pan = 0, bool looped = false)
        {
            string fullName = Mod.GetPath<AncientMysteriesMod>(sound);
            return PlaySynchronized(fullName, vol, pitch, pan, looped);
        }

        public static Sound Play(int sound, float vol = 1, float pitch = 0, float pan = 0, bool looped = false) =>
            DuckGame.SFX.Play(sound, vol, pitch, pan, looped);

        public static Sound Play(string sound, float vol = 1, float pitch = 0, float pan = 0, bool looped = false) =>
            DuckGame.SFX.Play(sound, vol, pitch, pan, looped);

        public static Sound PlaySynchronized(string sound, float vol = 1, float pitch = 0, float pan = 0, bool looped = false) =>
            DuckGame.SFX.PlaySynchronized(sound, vol, pitch, pan, looped);

        public static Sound PlaySynchronized(string sound, float vol, float pitch, float pan, bool looped, bool louderForMe) =>
            DuckGame.SFX.PlaySynchronized(sound, vol, pitch, pan, looped, louderForMe);
    }
}