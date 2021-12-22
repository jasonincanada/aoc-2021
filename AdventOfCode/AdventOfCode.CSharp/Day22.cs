using System.Text.RegularExpressions;

namespace AdventOfCode.CSharp
{
    record Cuboid(bool On, int X1, int X2, int Y1, int Y2, int Z1, int Z2);

    public class Day22 : IAdventDay
    {
        readonly List<Cuboid> _cuboids;

        // on x=11..13,y=11..13,z=11..13
        // off x=9..11,y=9..11,z=9..11  
        public Day22(IEnumerable<string> input)
        {
            _cuboids = input
                .Select(ToCuboid)
                .ToList();
        }

        Cuboid ToCuboid(string line)
        {
            var numbers = Regex
                .Matches(line, @"-?\d+")
                .Select(m => m.Value)
                .Select(int.Parse)
                .ToArray();

            bool on = line.StartsWith("on");
            int x1 = numbers[0];
            int x2 = numbers[1];
            int y1 = numbers[2];
            int y2 = numbers[3];
            int z1 = numbers[4];
            int z2 = numbers[5];

            return new Cuboid(on, x1, x2, y1, y2, z1, z2);            
        }

        public long Part1()
        {
            NaiveReactor r = new NaiveReactor(-50, 50);

            var part1 = _cuboids.Where(InPart1Range);

            foreach (var cube in part1)
                r.Set(cube);
                        
            return r.CountOn();          
        }

        static bool InPart1Range(Cuboid c)
        {
            return c.X1 >= -50 && c.X1 <= 50 &&
                   c.Y1 >= -50 && c.Y1 <= 50 &&
                   c.Z1 >= -50 && c.Z1 <= 50;
        }

        // Need a smarter Reactor for part 2!
        public long Part2() => -2;      
    }

    interface IReactor
    {
        void Set(Cuboid cube);
        long CountOn();
    }

    class NaiveReactor : IReactor
    {
        int[,,] _cubes;

        readonly int _min;
        readonly int _max;
        readonly int _offset;

        /// <summary>
        /// Specify the min/max coordinate value extents and map them to a 0-based 3D array
        /// </summary>
        public NaiveReactor(int min, int max)
        {
            int range = max - min + 1;
            _cubes = new int[range, range, range];

            _max = max;
            _min = min;
            _offset = 0 - _min; // what happens if _min > 0 ?
        }

        public void Set(Cuboid cube)
        {
            int val = cube.On ? 1 : 0;

            for (int x = cube.X1; x <= cube.X2; x++)
            for (int y = cube.Y1; y <= cube.Y2; y++)
            for (int z = cube.Z1; z <= cube.Z2; z++)
                _cubes[x + _offset,
                       y + _offset,
                       z + _offset] = val;
        }

        public long CountOn()
        {
            long count = 0;

            for (int x = _min; x <= _max; x++)
            for (int y = _min; y <= _max; y++)
            for (int z = _min; z <= _max; z++)
                count += _cubes[x + _offset,
                                y + _offset,
                                z + _offset];

            return count;
        }
    }
}
