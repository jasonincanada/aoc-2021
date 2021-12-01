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
    }
}
