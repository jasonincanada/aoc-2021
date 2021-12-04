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
        public static int IndexOfCond<T>(this IEnumerable<T> items, Func<T, bool> condition)
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
    }
}
