using AdventOfCode;

// Parsing
List<int> numbers = Common.GetInput(1)
    .Select(int.Parse)
    .ToList();

// Solve day 1, part 1 from last year (2020)
foreach (var m in numbers)
{
    foreach (var n in numbers)
    {
        if (m + n == 2020)
        {
            Console.WriteLine("Part 1: {0}", m * n);
        }
    }
}

