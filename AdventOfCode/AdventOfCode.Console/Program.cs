using AdventOfCode;
using AdventOfCode.FSharp;
using Microsoft.FSharp.Collections;

// Parsing
List<int> numbers = Common.GetInput(1)
    .Select(int.Parse)
    .ToList();

// Convert to an F# list
FSharpList<int> input = ListModule.OfSeq(numbers);

// Process day 1
FSharpList<int> result = Day01.doCase(input);

Console.WriteLine("Part 1: {0}", result.First());
