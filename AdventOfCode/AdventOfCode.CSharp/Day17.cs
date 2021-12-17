using System.Text.RegularExpressions;

namespace AdventOfCode.CSharp
{
    public class Day17 : IAdventDay
    {
        /// <summary>
        /// The target area including its boundary
        /// </summary>
        readonly int _left;
        readonly int _right;
        readonly int _bottom;
        readonly int _top;

        // target area: x=20..30, y=-10..-5
        public Day17(IEnumerable<string> input)
        {
            var numbers = Regex
                .Matches(input.First(), @"-?\d+")
                .Select(m => m.Value)
                .Select(int.Parse)
                .ToArray();
            
            _left   = numbers[0];
            _right  = numbers[1];
            _bottom = numbers[2];
            _top    = numbers[3];
        }

        public long Part1() => RunSimulation().Highest;
        public long Part2() => RunSimulation().Hits;

        public (int Highest, int Hits) RunSimulation()
        {
            // fifty thousand iterations under the sea
            const int iterations = 50_000;
                        
            int dy = _bottom;
            int dx = 1;
            int highest = 0;
            int iter = 0;
            int hits = 0;

            while (++iter <= iterations)
            {
                (bool hit, int h) = Fire(dx, dy);

                if (hit)
                {
                    hits++;
                    if (highest < h)
                        highest = h;
                }

                // never fire so fast that the first location is beyond the target area
                if (++dx > _right)
                {
                    dx = 1;

                    // aim a bit higher
                    dy++;
                }
            }            

            return (highest, hits);
        }

        /// <summary>
        /// Simulate one round of firing a probe at a certain velocity/direction
        /// </summary>
        /// <param name="dx">Horizontal velocity at start</param>
        /// <param name="dy">Vertical velocity at start</param>
        /// <returns>
        /// True if the probe enters the target area, and the highest y value attained during
        /// the simulation (aposea?)
        /// </returns>
        (bool hit, int highest) Fire(int dx, int dy)
        {
            int x = 0;
            int y = 0;
            int highest = y;

            while (true)
            {
                x += dx;
                y += dy;

                if (highest < y)
                    highest = y;

                if (InTargetArea(x, y)) return (true , highest);
                if (GoneForever(x, y))  return (false, highest);

                // slow the horizontal speed of the probe until it's accelerating directly down
                if (dx > 0) dx--;

                // gradually fall faster
                dy--;
            }

            bool InTargetArea(int x, int y) => x >= _left 
                                            && x <= _right
                                            && y >= _bottom
                                            && y <= _top;

            bool GoneForever(int x, int y)  => x > _right
                                            || y < _bottom;
        }              
    }
}
