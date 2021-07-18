using System.Runtime.InteropServices;

namespace AncientMysteries
{
    public static class NativeMethods
    {
        #region MyRegion
        [DllImport("steam_api.dll", EntryPoint = "SteamAPI_ISteamUtils_GetSteamUILanguage")]
        private static extern IntPtr SteamAPI_ISteamUtils_GetSteamUILanguage(IntPtr c_instancePtr);
        #endregion
    }
}
