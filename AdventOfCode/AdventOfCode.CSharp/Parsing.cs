namespace AdventOfCode.CSharp
{
    public static class Parsing
    {
        // Convert each line in the input to a number
        public static IEnumerable<int> AllNumbers(IEnumerable<string> input)
        {
            var integers = input.Select(int.Parse);

            return integers;
        }
                
        public static IEnumerable<(string, int)> AllStringWithNumber(IEnumerable<string> input)
        {
            return input.Select(StringWithNumber);
        }

        // "forward 5" -> ("forward", 5)
        static (string, int) StringWithNumber(string input)
        {
            var split = input.Split(' ');

            return (split[0], int.Parse(split[1]));
        }
    }
}
