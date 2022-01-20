using System.Collections.Generic;
using System.Linq;

namespace QmkRgbMatrixGenerator.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<IGrouping<int, T>> GroupByIndex<T>(this IEnumerable<T> source, int index)
        {
            return source.Select((x, i) => new { Index = i, Value = x }).GroupBy(x => x.Index / index, x => x.Value);
        }
    }
}
