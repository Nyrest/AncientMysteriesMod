namespace AncientMysteries
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MetaTypeAttribute : Attribute
    {
        public MetaTypeAttribute(MetaType type) { }
    }

    public enum MetaType
    {
        [Obsolete()]
        Error,
        Gun,
        Magic,
        Melee,
        Equipment,
        Throwable,
        Props,
        Decoration,
        Developer,
    }
}
