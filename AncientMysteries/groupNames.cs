namespace AncientMysteries
{
    public static class groupNames
    {
        public const string topGroupName = "Ancient";

        public const string _debugGroup = topGroupName + "|Debug";

        #region relative
        public const string relative_Isekai = "|Isekai";
        #endregion

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

        public const string g_wtf = guns + "|WTF";
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

        #region Props
        public const string props = topGroupName + "|Props";

        public const string p_functional = props + "|Functional";
        #endregion

        [Obsolete("Assign a valid group.", true)]
        public const string g_unknown = topGroupName + "|Error";
    }
}
