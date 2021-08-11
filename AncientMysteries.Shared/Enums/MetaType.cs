namespace AncientMysteries
{
    public enum MetaType
    {
        [Obsolete("Invalid MetaType Value", true)]
        Undefined = 0,

        Gun,
        Magic,
        Melee,
        Equipment,
        Throwable,
        Props,
        Decoration,
        MapTools,
        Developer,
    }
}