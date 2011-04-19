using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Evil.TestHelpers
{
    public static class ListExtensions
    {
        public static T FirstOf<T>(this IList<object[]> source)
        {
            return AllOf<T>(source).FirstOrDefault();
        }

        public static IEnumerable<T> AllOf<T>(this IList<object[]> source)
        {
            if (source == null || source.Count == 0) return new Collection<T>();

            return source.SelectMany(o => o.OfType<T>());
        }
    }
}