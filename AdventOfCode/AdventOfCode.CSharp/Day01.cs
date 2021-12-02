namespace AdventOfCode.CSharp
{
    public class Day01 : IAdventDay
    {
        // the day's input is parsed in the constructor and kept around
        readonly IEnumerable<int> _parsed;

        /* Day-specific constants */
        const int WindowSize = 3;


        public Day01(IEnumerable<string> input)
        {
            _parsed = Parsing.AllNumbers(input);            
        }
        
        public int Part1()
        {
            int result = CountIncreasing(_parsed, windowSize: 1);

            return result;
        }

        public int Part2()
        {
            int result = CountIncreasing(_parsed, WindowSize);

            return result;
        }


        /* Day-specific logic */

        static int CountIncreasing(IEnumerable<int> numbers, int windowSize)
        {
            // keep a separate reference that we can mutate at the end of the for loop by calling Skip(1)
            var remaining = numbers;

            int rounds = numbers.Count() - windowSize;
            int count  = 0;

            for (int i = 0; i < rounds; i++)
            {
                // at this starting position in the list of numbers, keep a copy on the right that
                // is advanced by one element
                var left  = remaining;
                var right = remaining.Skip(1);

                // take the chosen number of elements (1 for part one, 3 for part two) and sum them
                var leftSum  =  left.Take(windowSize).Sum();
                var rightSum = right.Take(windowSize).Sum();

                if (leftSum < rightSum)
                    ++count;

                // advance to the next starting position (this is done rounds times)
                remaining = remaining.Skip(1);
            }

            return count;
        }     
    }
}
