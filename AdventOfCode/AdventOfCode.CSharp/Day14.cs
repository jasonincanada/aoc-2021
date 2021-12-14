namespace AdventOfCode.CSharp
{
    public class Day14 : IAdventDay
    {
        /// <summary>
        /// The starting polymer template from line one of the input, eg: NNCB
        /// </summary>
        readonly string _template;

        /// <summary>
        /// The pair insertion rules, eg: CH -> B
        /// </summary>
        readonly Dictionary<(char,char), char> _rules;

        /// <summary>
        /// The distinct letters found in the input (4 in the sample input, 10 in our input)
        /// </summary>
        readonly char[] _letters;


        /*
            NNCB

            CH -> B
            HH -> N
         */
        public Day14(IEnumerable<string> input)
        {
            _template = input.First();

            _rules = input
                .Skip(2)
                .Select(ToRule)
                .ToDictionary(p => p.Pair,
                              p => p.Letter);

            // gather all the letters, which will form the sides of our grid
            _letters = _rules
                .Select(x => x.Key.Item1)                
                .Distinct()
                .ToArray();
        }

        ((char,char) Pair, char Letter) ToRule(string line)
        {
            var split = line.Split(" -> ");
            return ((split[0][0], split[0][1]), split[1][0]);
        }


        public long Part1() => Evolve(10);
        public long Part2() => Evolve(40);
                
        long Evolve(int steps)
        {
            // the main 2D grid holding a tally of letter pairs. instead of expanding the
            // polymer manually as an ever-growing string, we do an initial count of the
            // occurrences of each possible pair, then evolve all occurrences of a pair at
            // once with a simple addition. after the first count of the string we no longer
            // even maintain a string, just counts of pairs of letters
            SquareGrid<char, long> pairs = new(_letters, 0);

            // count the number of each pair in the original template
            foreach (var pair in _template.SlidingPairs())
                pairs.Modify(pair, x => x + 1);            

            // if we only had the pair counts, how would we know which right-side characters
            // overlap which left side characters so we don't overcount them? i don't think we
            // can, so keep a separate count of individual characters and accrue them as we go
            Dictionary<char, long> letters = _letters.ToDictionary(l => l, x => 0L);
            
            // tally the letters in the original template
            foreach (var l in _template.ToCharArray())
                letters[l]++;

            // the main loop, 10 times for part 1, 40 times for part 2
            for (int i = 1; i <= steps; i++)
                EvolveTally(pairs, letters);
            
            long max = letters.Values.Max();
            long min = letters.Values.Min();

            return max - min;
        }

        /// <summary>
        /// Evolve the polymer strand one step by inserting a new letter between all existing
        /// pairs of letters. Since there are only so many pairs, track only the counts of
        /// each possible pair in a 2D grid, and evolve every instance of a pair at the same
        /// time by adding a single number (the number of that pair there are)
        /// </summary>
        /// <param name="tally">Counts of each possible pair of letters</param>
        /// <param name="letters">Counts of each letter</param>
        void EvolveTally(SquareGrid<char, long> tally, Dictionary<char, long> letters)
        {
            // keep a separate grid of the same size with accrued changes to our pair counts
            // for this iteration. we can't reference the tally at the same time we're
            // updating it, so separate it into two steps
            SquareGrid<char, long> queued = new(_letters, 0);

            foreach (var pair in tally.Indices())
            {
                if (tally.At(pair) == 0)
                    continue;

                char left = pair.Left;
                char right = pair.Right;
                char newletter = _rules[pair];
                long count = tally.At(pair);

                // when we split CH into CBH, we're gaining two new pairs (CB and BH) but losing
                // the original one that just got split apart (CH)
                queued.Modify((left, newletter),  x => x + count);
                queued.Modify((newletter, right), x => x + count);
                queued.Modify((left, right),      x => x - count);

                // count the new letter that's been generated. we can update this here in place
                letters[newletter] += count;
            }

            // merge our pair counts
            tally.Combine(queued, (a, b) => a + b);
        }
    }

    /// <summary>
    /// A square grid with labels from the list passed in during construction. Like a matrix
    /// but there's no operations defined except mutably updating a value at an index
    /// </summary>
    /// <typeparam name="TKey">The index type is a pair of TKeys</typeparam>
    /// <typeparam name="TValue">The stored value type</typeparam>
    class SquareGrid<TKey, TValue>
    {
        Dictionary<(TKey, TKey), TValue> _grid;
        readonly List<TKey> _keys;

        public SquareGrid(IEnumerable<TKey> keys, TValue zero)
        {
            _keys = keys.ToList();
            _grid = new Dictionary<(TKey, TKey), TValue>();

            foreach (var row in _keys)
            foreach (var col in _keys)
                _grid.Add((row, col), zero);
        }

        public void Modify((TKey, TKey) pair, Func<TValue, TValue> modifier)
        {
            _grid[pair] = modifier(_grid[pair]);
        }

        public IEnumerable<(TKey Left, TKey Right)> Indices()
        {
            foreach (var left  in _keys)
            foreach (var right in _keys)
                yield return (left, right);
        }

        public TValue At((TKey, TKey) pair)
        {
            if (!_grid.ContainsKey(pair))
                throw new IndexOutOfRangeException();

            return _grid[pair];
        }

        public void Combine(SquareGrid<TKey, TValue> deltas, Func<TValue, TValue, TValue> modifier)
        {
            foreach (var idx in Indices())
                Modify(idx, x => modifier(x, deltas.At(idx)));
        }
    }
}
