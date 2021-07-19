using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientMysteries.Utilities
{
    public static class InputHelper
    {
        #region Gameplay
        public const string trigger_Up = "Up";
        public const string trigger_DOWN = "DOWN";
        public const string trigger_Left = "LEFT";
        public const string trigger_Right = "RIGHT";

        public const string trigger_Shoot = "SHOOT";
        public const string trigger_Jump = "JUMP";
        public const string trigger_Quack = "QUACK";
        public const string trigger_Ragdoll = "RAGDOLL";
        public const string trigger_Grab = "GRAB";
        public const string trigger_Strafe = "STRAFE";
        #endregion

        #region Control
        public const string trigger_Select = "SELECT";
        public const string trigger_Chat = "CHAT";
        public const string trigger_Cancel = "CANCEL";
        public const string trigger_Menu1 = "MENU1";
        public const string trigger_Menu2 = "MENU2";
        public const string trigger_MenuLeft = "MENULEFT";
        public const string trigger_MenuRight = "MENURIGHT";
        public const string trigger_MenuUp = "MENUUP";
        public const string trigger_MeneDown = "MENUDOWN";
        #endregion

        #region Keyboard Only
        public const string trigger_Keyboard_VoiceReg = "VOICEREG";
        public const string trigger_Keyboard_KBDF = "KBDF";
        #endregion

        #region Gamepad Only
        public const string trigger_Gamepad_LTriggle = "LTRIGGER";
        public const string trigger_Gamepad_RTriggle = "RTRIGGER";

        public const string trigger_Gamepad_LBumber = "LBUMPER";
        public const string trigger_Gamepad_RBumber = "RBUMPER";

        public const string trigger_Gamepad_LStrick = "LSTICK";
        public const string trigger_Gamepad_RStick = "RSTICK";
        #endregion
    }
}
