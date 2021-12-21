namespace AdventOfCode.CSharp
{
    public class Day20 : IAdventDay
    {
        readonly char[] _enhancer;        
        readonly Dictionary<Coord, char> _image;

        /// <summary>
        /// Width/height of the original image
        /// </summary>
        readonly int _size;

        /// <summary>
        /// If a group of 9 empty cells is converted to a # and a group of 9 # back
        /// to empty, each step will toggle the ambient space between empty and filled
        /// </summary>
        readonly bool _togglingEmptiness;
        

        public Day20(IEnumerable<string> input)
        {
            _enhancer = input.First().ToCharArray();
            _size     = input.Skip(2).First().Length;
            _image    = ReadImage(input.Skip(2));

            _togglingEmptiness = _enhancer.First() == '#'
                              && _enhancer.Last()  == '.';
        }

        Dictionary<Coord, char> ReadImage(IEnumerable<string> lines)
        {
            var image = new Dictionary<Coord, char>();
            int row = 0;

            foreach (var line in lines)
            {
                int col = 0;
                foreach (char c in line)
                    image.Add(new Coord(row, col++), c);
                
                row++;
            }

            return image;
        }

        public long Part1() => Process(steps: 2);
        public long Part2() => Process(steps: 50);

        long Process(int steps)
        {
            var image = new Dictionary<Coord, char>(_image);
            var final = Evolve(image, steps);

            return final
                .Where(c => c.Value == '#')
                .Count();
        }

        Dictionary<Coord, char> Evolve(Dictionary<Coord, char> image, int steps)
        {
            int step = 0;

            while (++step <= steps)
            {
                // the grid we have to check needs to be expanded by 3 all around
                int left   = 0     - 3 * step;
                int top    = 0     - 3 * step;
                int right  = _size + 3 * step;
                int bottom = _size + 3 * step;

                // build the next image one cell at a time
                var next = new Dictionary<Coord, char>();

                for (int row=top; row<=bottom; row++)
                for (int col=left; col<=right; col++)
                {
                    var neighbours = Neighbours(row, col);

                    List<int> scan = neighbours
                        .Select(n => GetAt(n))
                        .Select(n => n == '#' ? 1 : 0)  // get ready for FromBits()
                        .ToList();

                    int index = FromBits(scan);
                    char enhanced = _enhancer[index];

                    next.Add(new Coord(row, col), enhanced);
                }

                // this is now the new image for the next step
                image = next;

                // embed GetAt within this function so it can see the locals image and step
                char GetAt(Coord coord)
                {
                    if (image.ContainsKey(coord))
                        return image[coord];

                    if (_togglingEmptiness)
                        return (step-1) % 2 == 0 ? '.' : '#';
                    else
                        return '.';
                }
            }

            return image;
        }

        /// <summary>
        /// Get the 9 coordinates in the neighbourhood of a cell including the cell
        /// </summary>
        static List<Coord> Neighbours(int row, int col)
        {
            return new()
            {
                new Coord(row - 1, col - 1),
                new Coord(row - 1, col),
                new Coord(row - 1, col + 1),
                new Coord(row, col - 1),
                new Coord(row, col),
                new Coord(row, col + 1),
                new Coord(row + 1, col - 1),
                new Coord(row + 1, col),
                new Coord(row + 1, col + 1),
            };
        }

        /// <summary>
        /// Convert a list of bits to an integer (most-significant bit first).  This uses a
        /// rudimentary doubler to handle increasing powers of 2 instead of bringing in Math.Pow()
        /// </summary>
        static int FromBits(List<int> bits)
        {
            int result = 0;
            int doubling = 1;

            // go right-to-left
            for (int i = bits.Count - 1; i >= 0; i--)
            {
                if (bits[i] == 1)
                    result += doubling;

                doubling *= 2;
            }

            return result;
        }
    }
}
