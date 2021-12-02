using AdventOfCode;
using AdventOfCode.CSharp;

List<IAdventDay> days = new() {
    new Day01(Common.GetInput(1)),
    new Day02(Common.GetInput(2))
};

for (int day = 0; day < days.Count; day++)
{
    Console.WriteLine("Day {0}", day + 1);
    Console.WriteLine("  Part 1: {0}", days[day].Part1());
    Console.WriteLine("  Part 2: {0}", days[day].Part2());
    Console.WriteLine("");
}
