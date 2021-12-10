namespace AdventOfCode.CSharp
{
    public class Day10 : IAdventDay
    {
        List<string> _lines;

        public Day10(IEnumerable<string> input)
        {
            _lines = input.ToList();            
        }

        public long Part1()
        {
            int errorscore = 0;

            // calculate the error score for the corrupted lines
            foreach (var line in _lines)
            {
                Result result = Inspect(line);
                
                if (result is Corrupted corrupted)
                {
                    char c = corrupted.IllegalChar;

                    errorscore += c switch
                    {
                        ')' => 3,       // magic numbers from the problem description
                        ']' => 57,
                        '}' => 1197,
                        '>' => 25137,
                         _  => throw new ArgumentOutOfRangeException($"c was {c}")
                    };
                }
            }

            return errorscore;
        }

        public long Part2()
        {
            List<long> scores = new();

            // score all of the incomplete lines and return the middle score
            foreach (var line in _lines)
            {
                Result result = Inspect(line);

                if (result is Incomplete incomplete)
                {
                    char[] missing = incomplete.Missing;
                    scores.Add(ScoreMissing(missing));
                }
            }

            scores.Sort();

            return scores[scores.Count / 2];
        }


        // closest thing to sum types in c#...
        abstract record Result {}

        record Normal                      : Result {}
        record Incomplete(char[] Missing)  : Result {}
        record Corrupted(char IllegalChar) : Result {}
        
        /// <summary>
        /// Look through a line character-by-character, matching brackets as far as possible
        /// </summary>
        /// <returns>A record derived from Result</returns>
        static Result Inspect(string line)
        {
            Stack<char> stack = new();

            foreach (char c in line.ToCharArray())
            {
                if ("([{<".Contains(c))
                {
                    stack.Push(c);
                }
                else
                {
                    // the closing character needs to match the top one on the stack
                    var opener = stack.Pop();

                    if (!IsLegalPair(opener, c))
                        return new Corrupted(c);
                }
            }

            // if there are any characters left on the stack, it's not a complete line
            if (stack.Any())
            {
                char[] missing = StackToMissing(stack);
                return new Incomplete(missing);
            }

            return new Normal();
        }

        /// <summary>
        /// Convert the characters on the stack to their matching closing braces
        /// </summary>
        /// <returns>Array of characters that would close the line with matching braces</returns>
        public static char[] StackToMissing(Stack<char> stack)
        {
            return stack
                .ToArray()
                .Select(ToComplement)
                .ToArray();
        }

        static char ToComplement(char c) => c switch
        {
            '(' => ')',
            '[' => ']',
            '{' => '}',
            '<' => '>',
             _  => throw new ArgumentOutOfRangeException($"c is {c}")
        };

        static bool IsLegalPair(char open, char close)
        {
            return ToComplement(open) == close;
        }

        /// <summary>
        /// The scoring procedure described in part 2
        /// </summary>
        public static long ScoreMissing(char[] missing)
        {
            long score = 0;

            foreach (var c in missing)
            {
                score *= 5;
                score += c switch
                {
                    ')' => 1,
                    ']' => 2,
                    '}' => 3,
                    '>' => 4,
                     _  => throw new ArgumentOutOfRangeException($"c was {c}")
                };
            }

            return score;
        }
    }
}
