namespace AncientMysteries
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MetaOrderAttribute : Attribute
    {
        public MetaOrderAttribute(int order = 0) { }
    }
}
