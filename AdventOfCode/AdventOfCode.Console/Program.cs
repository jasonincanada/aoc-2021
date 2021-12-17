using AdventOfCode;
using AdventOfCode.CSharp;

List<IAdventDay> days = new() {
    new Day01(Common.GetInput(1)),
    new Day02(Common.GetInput(2)),
    new Day03(Common.GetInput(3)),
    new Day04(Common.GetInput(4)),
    new Day05(Common.GetInput(5)),
    new Day06(Common.GetInput(6)),
    new Day07(Common.GetInput(7)),
    new Day08(Common.GetInput(8)),
    new Day09(Common.GetInput(9)),
    new Day10(Common.GetInput(10)),
    new Day11(Common.GetInput(11)),
    new Day12(Common.GetInput(12)),
    new Day13(Common.GetInput(13)),
    new Day14(Common.GetInput(14)),
    new Day15(Common.GetInput(15)),
    new Day16(Common.GetInput(16)),
    new Day17(Common.GetInput(17)),
};

for (int day = 0; day < days.Count; day++)
{
    Console.WriteLine("Day {0}", day + 1);
    Console.WriteLine("  Part 1: {0}", days[day].Part1());
    Console.WriteLine("  Part 2: {0}", days[day].Part2());
    Console.WriteLine("");
}
