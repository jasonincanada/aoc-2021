namespace AdventOfCode.CSharp
{
    /// <summary>
    /// We use a record here (new to C# 9.0!) so the call to basin.Contains() later on can
    /// check for membership based on the Coord's values, not on its address as a reference type.
    /// If we do it with a class (a reference type--address equality), the code will occasionally
    /// miss that it's looking at two different objects that represent the same coordinate. The
    /// point of C# record types is to do this more intuitive value comparison automatically
    /// </summary>
    public record Coord(int Row, int Col);

    public class Day09 : IAdventDay
    {
        readonly int[][] _heights;
        readonly int _floorRows;
        readonly int _floorCols;

        public Day09(IEnumerable<string> input)
        {
            _heights = input
                .Select(ToDigitArray)
                .ToArray();
            
            // track these separately because they're different in the sample input
            _floorRows = _heights.Length;
            _floorCols = _heights[0].Length;
        }

        int[] ToDigitArray(string digits)
        {
            return digits
                .ToCharArray()
                .Select(c => c - 48)
                .ToArray();
        }

        int HeightAt(Coord point)
        {
            return _heights[point.Row][point.Col];
        }


        public long Part1()
        {
            int risk = 0;

            foreach (var point in FindLowPoints())
                risk += 1 + HeightAt(point);

            return risk;
        }

        public long Part2()
        {
            List<List<Coord>> basins = new();

            // each low point represents the bottom of a basin. flood upwards from it and track
            // the coordinates taken along the way
            foreach (var point in FindLowPoints())
                basins.Add(FloodBasin(point));

            var biggestThree = basins
                .OrderByDescending(b => b.Count)
                .Take(3);

            long size = biggestThree
                .Select(b => b.Count)
                .Product();

            return size;
        }

        /// <summary>
        /// Find all the low points on the floor map
        /// </summary>
        List<Coord> FindLowPoints()
        {
            List<Coord> points = new();

            for (int row = 0; row < _floorRows; row++)
            for (int col = 0; col < _floorCols; col++)
            {
                int height = _heights[row][col];
                var neighbours = Neighbours(row, col);

                // it's a low point if all of its neighbours are higher than it
                bool islowest = neighbours
                    .All(n => HeightAt(n) > height);

                if (islowest)
                    points.Add(new Coord(row, col));
            }

            return points;
        }

        /// <summary>
        /// Calculate the 4 neighbours of a cell (fewer if it's an edge or corner cell)
        /// </summary>
        List<Coord> Neighbours(int row, int col)
        {
            List<Coord> neighbours = new()
            {
                new Coord(row-1, col),
                new Coord(row, col+1),
                new Coord(row+1, col),
                new Coord(row, col-1)
            };
            
            // exclude coordinates that are off the map
            return neighbours
                .Where(n => n.Row >= 0 && n.Row < _floorRows
                         && n.Col >= 0 && n.Col < _floorCols)
                .ToList();
        }

        /// <summary>
        /// Flood outwards/upwards from this low point, returning a list of all coordinates found
        /// </summary>
        /// <param name="point">The starting point of the basin fill (it's up to the caller to ensure it's a low point)</param>
        /// <returns>List of coordinates reachable upwards from the passed point</returns>
        List<Coord> FloodBasin(Coord point)
        {
            // the frontier is our working list of coordinates to check neighbours for
            Stack<Coord> frontier = new();
            List<Coord> basin = new();

            // start with the basin low point
            frontier.Push(point);
            basin.Add(point);
            
            while (frontier.Any())
            {
                var next = frontier.Pop();
                var neighbours = Neighbours(next.Row, next.Col);

                // follow only paths heading upwards, up to but not including heights of 9.
                // note this > could also be >= and it still passes the tests, but since this
                // is modeling the downwards flow of water it feels more sensical to insist
                // on a strictly upwards path as we expand outwards
                var upwards = neighbours
                    .Where(n => HeightAt(n) > HeightAt(next))
                    .Where(n => HeightAt(n) < 9);

                foreach (var coord in upwards)
                {
                    if (basin.Contains(coord))
                        continue;

                    frontier.Push(coord);
                    basin.Add(coord);
                }
            }

            return basin;
        }
    }
}
