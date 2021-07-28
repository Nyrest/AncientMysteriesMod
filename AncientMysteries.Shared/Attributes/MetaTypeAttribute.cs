namespace AncientMysteries
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MetaTypeAttribute : Attribute
    {
        public MetaTypeAttribute(MetaType type)
        {
        }
    }
}