using System.Diagnostics;

namespace AdventOfCode.CSharp
{
    public class Day06 : IAdventDay
    {
        readonly List<int> _fish;

        public Day06(IEnumerable<string> input)
        {
            _fish = Parsing.NumbersWithCommas(input.First()).ToList();
        }

        public long Part1() => Evolve(80);        
        public long Part2() => Evolve(256);
                
        long Evolve(int steps)
        {
            // evolving the list of fish individually is far too slow for part 2, so we first
            // initialize a new list that sums up at each index i the number of fish with
            // i days left until spawning
            List<long> fishCounts = Enumerable.Range(0, 8+1)
                .Select(days => (long)_fish.Count(f => f == days))
                .ToList();

            for (int i = 0; i < steps; i++)
            {
                // the number of fish with 0 days left is now the first element
                long spawning = fishCounts.First();

                // shift all fish down a day (rolling the 0-day fish off the front of the list)
                fishCounts = fishCounts.Skip(1).ToList();

                // should now only have data for lifespans 0..7
                Debug.Assert(fishCounts.Count() == 7+1);

                // all the fish that rolled off the front get added again with 6 days left
                fishCounts[6] += spawning;

                // and contribute that many new fish with 8 days left
                fishCounts.Add(spawning);                
            }

            return fishCounts.Sum();
        }
    }
}
