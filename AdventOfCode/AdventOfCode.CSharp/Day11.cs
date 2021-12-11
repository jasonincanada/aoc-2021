namespace AdventOfCode.CSharp
{
    class Octo
    {
        public int Energy { get; set; }
        public bool Flashed { get; set; }

        public Octo(int energy)
        {
            Energy = energy;
            Flashed = false;
        }
    }

    class Cavern
    {
        /// <summary>
        /// Keep one line of octos instead of a 2D-array and use index math to emulate a grid
        /// </summary>
        readonly List<Octo> _octos;

        /// <summary>
        /// Width of the cavern (one row of octos)
        /// </summary>
        readonly int _width;

        /// <summary>
        /// Count every time an octo is flashed
        /// </summary>
        public int FlashCount { get; private set; }

        public Cavern(IEnumerable<string> input)
        {
            _octos = input
                .SelectMany(ListOfDigits)
                .Select(energy => new Octo(energy))
                .ToList();

            _width = input.First().Length;

            FlashCount = 0;
        }

        List<int> ListOfDigits(string line)
        {
            return line
                .ToCharArray()
                .Select(ToDigit)
                .ToList();

            static int ToDigit(char c) => c - '0';
        }

        public void Step()
        {
            ResetFlashed();
            IncreaseAll();

            bool flashed;

            do
            {
                flashed = false;

                for (int i = 0; i < _octos.Count; i++)
                {
                    var octo = _octos[i];

                    // can't flash an octopus twice, do you want to risk it, i don't
                    if (octo.Flashed)
                        continue;

                    // if this octo isn't in energy crisis we've got no truck with it
                    if (octo.Energy <= 9)
                        continue;

                    flashed = true;             // stay in the main loop another time
                    octo.Flashed = true;        // don't let a neighbour flash us in this step
                    FlashCount++;               // track total flashes over all steps (part 1)

                    foreach (var o in Neighbours(i))
                    {
                        if (o.Flashed)
                            continue;

                        o.Energy++;
                    }
                }

            } while (flashed);
                        
            // any octo that was flashed in this step has its energy set to 0
            foreach (var octo in _octos)
                if (octo.Flashed)
                    octo.Energy = 0;
        }

        void IncreaseAll()
        {
            foreach (var octo in _octos)
                octo.Energy++;
        }

        void ResetFlashed()
        {
            foreach (var octo in _octos)
                octo.Flashed = false;
        }

        IEnumerable<Octo> Neighbours(int i)
        {
            int row = i / _width;
            int col = i % _width;

            List<(int r, int c)> neighbours = new()
            {
                (row - 1, col - 1),
                (row - 1, col),
                (row - 1, col + 1),

                (row, col - 1),
                (row, col + 1),

                (row + 1, col - 1),
                (row + 1, col),
                (row + 1, col + 1)
            };
                        
            return neighbours
                .Where(coord => coord.r >= 0 && coord.r < _width
                             && coord.c >= 0 && coord.c < _width)
                .Select(coord => _octos[coord.r * _width + coord.c]);
        }

        int CountFlashed() => _octos.Count(o => o.Flashed);
        int CountAll()     => _octos.Count;

        public bool AllFlashed() => CountFlashed() == CountAll();
    }

    public class Day11 : IAdventDay
    {
        readonly IEnumerable<string> _input;               

        public Day11(IEnumerable<string> input)
        {
            _input = input;     
        }
        
        public long Part1()
        {
            var cavern = new Cavern(_input);

            for (int i = 1; i <= 100; i++)
                cavern.Step();

            return cavern.FlashCount;
        }

        public long Part2()
        {
            var cavern = new Cavern(_input);
            int steps = 0;            
                        
            do {
                cavern.Step();
                steps++;
            } while (!cavern.AllFlashed());

            return steps;
        }
    }
}
