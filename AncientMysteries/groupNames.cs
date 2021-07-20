namespace AncientMysteries
{
    public static class groupNames
    {
        public const string grouptopGroupName = "Ancient";

        public const string group_DEBUG = grouptopGroupName + "|Debug";

        #region relative

        private const string group_relative_Isekai = "|Isekai";

        #endregion relative

        #region Guns

        public const string group_Guns = grouptopGroupName + "|Weapons";

        public const string group_Guns_Commons = group_Guns + "|Commons";

        public const string group_Guns_Rifles = group_Guns + "|Rifles";

        public const string group_Guns_Shotguns = group_Guns + "|Shotguns";

        public const string group_Guns_Pistols = group_Guns + "|Pistols";

        public const string group_Guns_Artifacts = group_Guns + "|Artifacts";

        public const string group_Guns_Misc = group_Guns + "|Misc";

        public const string group_Guns_Staffs = group_Guns + "|Staffs";

        public const string group_Guns_MachineGuns = group_Guns + "|Machine Guns";

        public const string group_Guns_Isekai = group_Guns + group_relative_Isekai;

        public const string group_Guns_Melees = group_Guns + "|Melee";

        public const string group_Guns_Snipers = group_Guns + "|Snipers";

        public const string group_Guns_Explosives = group_Guns + "|Explosives";

        public const string group_Guns_WTF = group_Guns + "|WTF";

        #endregion Guns

        #region Melee

        #endregion

        #region Equipments

        public const string group_Equipments = grouptopGroupName + "|Equipments";

        public const string group_Equipment_Developer = group_Equipments + "|Developers";

        public const string group_Equipment_Isekai_ROR = group_Equipments + group_relative_Isekai + "|Risk of Rain";

        #endregion Equipments

        #region Stuffs

        public const string group_Stuffs = grouptopGroupName + "|Stuffs";

        public const string group_Stuffs_Props = group_Stuffs + "|Props";

        #endregion Stuffs

        #region Props

        public const string group_Props = grouptopGroupName + "|Props";

        public const string group_Props_functional = group_Props + "|Functional";

        #endregion Props

        #region Map Decorations 

        public const string group_Decorations = grouptopGroupName + "|Decorations";

        #endregion Props

        #region Map Decorations 

        public const string group_ModTools = grouptopGroupName + "|MapTools";

        public const string group_MapTools_Lights = grouptopGroupName + "|Lights";

        #endregion Props

        [Obsolete("Assign a valid group.", true)]
        public const string group_Unknown = grouptopGroupName + "|Error";
    }
}