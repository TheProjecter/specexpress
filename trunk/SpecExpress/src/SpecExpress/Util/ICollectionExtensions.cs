using System.Collections;

namespace SpecExpress.Util
{
    public static class ICollectionExtensions
    {
        public static bool IsEmpty(this ICollection collection)
        {
            return collection.Count == 0;
        }
    }
}