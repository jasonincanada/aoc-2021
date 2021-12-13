using System.Diagnostics;

namespace AdventOfCode.CSharp
{
    /// <summary>
    /// Represents a single line of the input, with 10 patterns and 4 output values
    /// </summary>
    class Note
    {
        /// <summary>
        /// The 10 patterns to the left of the | sign in the input
        /// </summary>
        public List<string> Patterns { get; }

        /// <summary>
        /// The 4 output values to the right of the | sign in the input
        /// </summary>
        public List<string> Outputs { get; }

        /// <summary>
        /// Where each digit is found in the patterns list.  The goal is to gradually update
        /// this array as we deduce where each digit is located in the list.  i.e. Digits[3] will
        /// hold the digit represented by the third Pattern
        /// </summary>
        public int[] Digits { get; } = new int[10];
          

        // be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
        public Note(string input)
        {
            var split = input.Split(" | ");

            // sorting the input right away if we can is usually smart, and we can!  the order
            // the segments are specified in is irrelevant in determining the final digit. having
            // sorted patterns and outputs makes the deduction operations simpler
            Patterns = split[0].Split(' ').Select(Common.Alphabetize).ToList();
            Outputs  = split[1].Split(' ').Select(Common.Alphabetize).ToList();

            Debug.Assert(Patterns.Count == 10);
            Debug.Assert(Outputs.Count  == 4 );
        }
        
        /// <summary>
        /// Determine which patterns represent which digits using deduction
        /// </summary>
        public void DeduceDigits()
        {
            // these ones are "freebies" since their segment count gives away the digit they represent
            string one   = GetThe2SegmentPattern();
            string seven = GetThe3SegmentPattern();
            string four  = GetThe4SegmentPattern();
            string eight = GetThe7SegmentPattern();

            FoundDigit(1, one);
            FoundDigit(7, seven);
            FoundDigit(4, four);
            FoundDigit(8, eight);

            // now we begin the real deduction process
            //
            // going from digit 1 to 4 adds two more segments, which we'll call the "elbow".
            // as of now we don't know which letter goes with which segment, we'll deduce it later
            char[] elbow = AddedFrom(one, four);   Debug.Assert(elbow.Length == 2);

            // overlap the 4 and 7 to get an almost 9 that is missing its bottom segment
            char[] fourseven = Overlap(four, seven);

            // find the only 6-segment digit that has our 4|7 plus one more segment, it has to be
            // the 9.  the other possibility (with the bottom-left segment) isn't a valid digit
            string nine = Patterns
                .Where(p => p.Length == 6)
                .Where(p => p.HasAll(fourseven))
                .Only();

            FoundDigit(9, nine);
                        
            // next, find the 5.  this is the only five-segment digit that has both segments
            // introduced from the transition from 1 -> 4 (the elbow)
            string five = Patterns
                .Where(p => p.Length == 5)
                .Where(p => p.HasAll(elbow))
                .Only();

            FoundDigit(5, five);

            // the 5 is not the 3, but differs from it by only one segment
            string three = Patterns
                .Where(p => OneSegmentDifferent(five, p))
                .Only();

            FoundDigit(3, three);

            // at this point we've identified where digits 1 3 4 5 7 8 9 are in the pattern list

            // 2 is the remaining five-segment digit that isn't 3 and 5
            string two = Patterns
                .Where(p => p.Length == 5)
                .Where(p => p != three && p != five)
                .Only();

            FoundDigit(2, two);

            // we can deduce two more segments that we'll need in a bit.  the new letter in
            // the 3 is the top-right segment.  the letter left behind in the 5 is the top-left
            char topright = AddedFrom(five, three)[0];
            char topleft  = AddedFrom(three, five)[0];

            // it remains to distinguish between the middle and bottom-right segments.  consider
            // the middle segment. it's just the elbow without the top-left segment
            char middle = elbow
                .Where(s => s != topleft)
                .Only();

            // 0 is the only six-segment digit without the middle segment
            string zero = Patterns
                .Where(p => p.Length == 6)
                .Where(p => !p.Contains(middle))
                .Only();

            FoundDigit(0, zero);

            // 6 is the only six-segment digit without the top-right segment
            string six = Patterns
                .Where(p => p.Length == 6)
                .Where(p => !p.Contains(topright))
                .Only();

            FoundDigit(6, six);
        }
        
        /// <summary>
        /// Which letters are new to <paramref name="to"/> starting from <paramref name="from"/>
        /// </summary>
        /// <param name="from">Original string</param>
        /// <param name="to">New string possibly with letters added</param>
        /// <returns>Letters found in <paramref name="to"/> that aren't in <paramref name="from"/></returns>
        static char[] AddedFrom(string from, string to)
        {
            char[] froms = from.ToCharArray();
            char[] tos = to.ToCharArray();

            char[] added = tos
                .Where(c => !froms.Contains(c))
                .ToArray();

            return added;
        }

        static bool OneSegmentDifferent(string first, string second)
        {
            return AddedFrom(first, second).Length == 1
                && AddedFrom(second, first).Length == 1;
        }

        static char[] Overlap(string first, string second)
        {
            return $"{first}{second}"
                .ToCharArray()
                .Distinct()       // assumes Distinct() retains the sort order, which it seems to
                .ToArray();
        }

        // we get four digits for free because their segment counts are unique
        string GetThe2SegmentPattern() => PatternsOfLength(2).Only();
        string GetThe3SegmentPattern() => PatternsOfLength(3).Only();
        string GetThe4SegmentPattern() => PatternsOfLength(4).Only();
        string GetThe7SegmentPattern() => PatternsOfLength(7).Only();

        List<string> PatternsOfLength(int length)
        {
            return Patterns
                .Where(p => p.Length == length)
                .ToList();
        }

        /// <summary>
        /// Record which <paramref name="digit"/> is represented by the given <paramref name="pattern"/>
        /// </summary>
        /// <param name="digit"></param>
        /// <param name="pattern"></param>
        void FoundDigit(int digit, string pattern)
        {
            int i = Patterns.IndexOf(pattern);
            Digits[i] = digit;
        }
    }

    public class Day08 : IAdventDay
    {
        readonly List<Note> _notes;

        public Day08(IEnumerable<string> input)
        {
            // one note per line
            _notes = input.Select(n => new Note(n)).ToList();
        }

        public long Part1()        
        {
            List<int> targetLengths = new() { 2, 3, 4, 7 };
            int count = 0;

            foreach (var note in _notes)
            {
                var outputLengths = note.Outputs
                    .Select(o => o.Length)
                    .ToList();

                // how many of this note's output values match the target lengths listed above
                count += outputLengths
                    .Where(l => targetLengths.Contains(l))
                    .Count();
            }

            return count;
        }

        public long Part2()
        {
            int result = 0;

            // solve each note individually and track the sum of the output values
            foreach (var note in _notes)
            {
                note.DeduceDigits();

                int count = 0;
                int multiplier = 1000;  // all output values are 4 digits so start at 10^3

                foreach (var output in note.Outputs)
                {
                    int idx = note
                        .Digits
                        .Where(i => note.Patterns[i] == output)
                        .Only();

                    count += note.Digits[idx] * multiplier;
                    multiplier /= 10;
                }

                result += count;
            }

            return result;
        }
    }
}
