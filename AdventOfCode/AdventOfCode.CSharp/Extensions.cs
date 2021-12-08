namespace AdventOfCode.CSharp
{
    public static class Extensions
    {
        /// <summary>
        /// Find the index of the first element in an IEnumerable&lt;<typeparamref name="T"/>&gt; that satisfies a
        /// condition, or -1 if none found
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The IEnumerable&lt;T&gt; to enumerate</param>
        /// <param name="condition"></param>
        /// <returns>The 0-based index of the first element that satisfies the condition, or -1 if no element does</returns>
        public static int IndexWhere<T>(this IEnumerable<T> items, Func<T, bool> condition)
        {
            int index = 0;

            foreach (T item in items)
            {
                if (condition(item))
                    return index;

                index++;
            }

            return -1;
        }

        /// <summary>
        /// Return true if every needle is found somewhere in this haystack
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needles"></param>
        /// <returns></returns>
        public static bool HasAll(this string haystack, char[] needles)
        {
            char[] chars = haystack.ToCharArray();
            return needles.All(n => chars.Contains(n));
        }

        /// <summary>
        /// Expect only one element in this IEnumerable&lt;<typeparamref name="T"/>&gt; and pass
        /// the element back if so. If there are no elements or more than one, throw an exception
        /// </summary>
        /// <returns>The element in this IEnumerable&lt;<typeparamref name="T"/>&gt;</returns>
        /// <exception cref="Exception">Thrown if element count is other than 1</exception>
        public static T Only<T>(this IEnumerable<T> ts)
        {
            if (ts.Count() != 1)
                throw new Exception($"Only() expects a single value in the IEnumerable but found {ts.Count()}");

            return ts.First();
        }
    }
}
