using System.Text.RegularExpressions;

namespace AdventOfCode.CSharp
{
    class Line
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public static Line FromInput(string input)
        {
            // extract the numbers from the input by splitting on non-digit strings
            int[] numbers = Regex.Split(input, @"\D+")
                .Select(int.Parse)
                .ToArray();

            return new Line
            {
                X1 = numbers[0],
                Y1 = numbers[1],
                X2 = numbers[2],
                Y2 = numbers[3]
            };
        }

        public List<(int, int)> GetCoords()
        {
            var x1 = Math.Min(X1, X2);
            var x2 = Math.Max(X1, X2);
            var y1 = Math.Min(Y1, Y2);
            var y2 = Math.Max(Y1, Y2);

            var xs = Enumerable.Range(x1, x2 - x1 + 1);
            var ys = Enumerable.Range(y1, y2 - y1 + 1);

            if (X1 > X2) xs = xs.Reverse();
            if (Y1 > Y2) ys = ys.Reverse();

            if (IsHoriz())      return xs.Select(x => (x, Y1)).ToList();
            else if (IsVert())  return ys.Select(y => (X1, y)).ToList();
            else                return xs.Zip(ys).ToList();
        }

        public bool IsHoriz()    => Y1 == Y2;
        public bool IsVert()     => X1 == X2;
        public bool IsDiagonal() => !IsHoriz() && !IsVert();
    }

    public class Day05 : IAdventDay
    {
        readonly List<Line> _lines;

        public Day05(IEnumerable<string> input)
        {
            _lines = input
                .Select(Line.FromInput)
                .ToList();
        }

        public long Part1() => CountOverlapsOver(2, diagonals: false);
        public long Part2() => CountOverlapsOver(2, diagonals: true);

        int CountOverlapsOver(int threshold, bool diagonals)
        {
            return _lines
                .Where(line => diagonals || !line.IsDiagonal())
                .SelectMany(line => line.GetCoords())
                .GroupBy(c => c)
                .Where(grp => grp.Count() >= threshold)
                .Count();
        }
    }
}
