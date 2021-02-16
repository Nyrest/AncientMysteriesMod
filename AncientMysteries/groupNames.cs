using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AncientMysteries
{
    public static class groupNames
    {
        public const string topGroupName = "Ancient";

        public const string relative_Isekai = "|Isekai";

        public const string _debugGroup = topGroupName + "||DGRED|Debug";

        #region Guns
        public const string guns = topGroupName + "|Weapons";

        public const string g_commons = guns + "|Commons";

        public const string g_rifles = guns + "|Rifles";

        public const string g_shotguns = guns + "|Shotguns";

        public const string g_pistols = guns + "|Pistols";

        public const string g_artifacts = guns + "|Artifacts";

        public const string g_misc = guns + "|Misc";

        public const string g_staffs = guns + "|Staffs";

        public const string g_machineGuns = guns + "|Machine Guns";

        public const string g_isekai = guns + relative_Isekai;

        public const string g_melees = guns + "|Melee";

        public const string g_snipers = guns + "|Snipers";

        public const string g_explosives = guns + "|Explosives";
        #endregion

        #region Equipments
        public const string equipments = topGroupName + "|Equipments";

        public const string e_developer = equipments + "|Developers";

        public const string e_isekai_ror = equipments + relative_Isekai + "|Risk of Rain";
        #endregion

        #region Stuffs
        public const string stuffs = topGroupName + "|Stuffs";

        public const string s_props = stuffs + "|Props";
        #endregion

        public static class Series
        {
            public const string darkName = "Dark";
        }
    }
}
