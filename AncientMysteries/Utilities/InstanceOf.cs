using AncientMysteries.Helpers;

namespace AncientMysteries
{
    public static class InstanceOf<T> where T : new()
    {
        public static readonly T instance = GenericNew<T>.CreateInstance();
    }
}
