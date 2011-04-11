using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Evil.Web.Tests.TestHelpers
{
    public static class ListExtensions
    {
        public static T First<T>(this IList<object[]> source)
        {
            return All<T>(source).FirstOrDefault();
        }

        public static IEnumerable<T> All<T>(this IList<object[]> source)
        {
            if (source == null || source.Count == 0) return new Collection<T>();

            return source.SelectMany(o => o.OfType<T>());
        }
    }
}