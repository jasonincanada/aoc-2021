namespace AdventOfCode.CSharp
{
    public class Day03 : IAdventDay
    {
        /// <summary>
        /// Our input arrives as a list of bitstrings consisting of '0's and '1's, which we
        /// convert in the constructor to integers 1 and -1 to make columns of them more summable
        /// </summary>
        readonly List<List<int>> _bitstrings;

        /// <summary>
        /// The length of each string of bits (same for each line of the input)
        /// </summary>
        readonly int _length;


        public Day03(IEnumerable<string> input)
        {
            _bitstrings = input
                .Select(line => line.Select(CharToInt).ToList())
                .ToList();

            // all bitstrings are the same length so measure the first one
            _length = _bitstrings.First().Count;
        }

        static int CharToInt(char bit) => bit switch
        {
            '0' => -1,
            '1' => 1,
             _  => throw new ArgumentOutOfRangeException(bit.ToString())
        };


        public long Part1()
        {
            int gamma = 0;
            int epsilon = 0;
            int doubling = 1;

            for (int i = _length - 1; i >= 0; i--)
            {
                int sum = SumAtIndex(_bitstrings, i);

                if (sum > 0)
                    gamma += doubling;
                else
                    epsilon += doubling;

                doubling *= 2;
            }

            return gamma * epsilon;
        }

               
        public long Part2()
        {
            // the oxygen and carbon computations differ only in the filtering bit being 1 or -1
            int oxygen = FromBits(FilterPile(1, -1));
            int carbon = FromBits(FilterPile(-1, 1));

            return oxygen * carbon;
        }

        /// <summary>
        /// This is the filtering procedure from part 2. It's called twice, once for oxygen and once for carbon dioxide
        /// </summary>
        /// <param name="positive">Should really be named positiveOrZero</param>
        /// <param name="negative"></param>
        /// <returns></returns>
        List<int> FilterPile(int positive, int negative)
        {
            // start with our original parsed input
            var pile = _bitstrings;

            for (int i = 0; i < _length; i++)
            {
                int sum = SumAtIndex(pile, i);

                // whittle down the pile
                if (sum >= 0)
                    pile = pile.Where(bits => bits[i] == positive).ToList();
                else
                    pile = pile.Where(bits => bits[i] == negative).ToList();

                // per the problem description, we can bail as soon as we have one bitstring left
                if (pile.Count == 1)
                    break;
            }

            return pile.First();
        }

        /// <summary>
        /// Convert a list of bits to an integer (most-significant bit first).  This uses a
        /// rudimentary doubler to handle increasing powers of 2 instead of bringing in Math.Pow()
        /// </summary>
        static int FromBits(List<int> bits)
        {
            int result = 0;
            int doubling = 1;

            // go right-to-left
            for (int i = bits.Count - 1; i >= 0; i--)
            {
                if (bits[i] == 1)
                    result += doubling;

                doubling *= 2;
            }

            return result;
        }

        /// <summary>
        /// Find the sum of the bits at position <paramref name="i"/> in every bitstring
        /// </summary>
        /// <param name="bitstrings">
        /// Pass bitstrings every time instead of using the parsed input (this method is static
        /// and can't see it anyway)
        /// </param>
        static int SumAtIndex(List<List<int>> bitstrings, int i)
        {
            return bitstrings
                .Select(bits => bits[i])
                .Sum();
        }             
    }
}
