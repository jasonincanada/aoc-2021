using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;

namespace AdventOfCode.CSharp
{
    /// <summary>
    /// We don't need a fully general grid for this challenge, so create one that wraps a
    /// square grid of integers and keeps an expansion factor that virtually tiles the
    /// grid that many times out to the right and downwards. For those extended cells,
    /// there's no point actually computing the new values since they are calculated using
    /// simple arithmetic on the original grid; we can do the calcs on-demand so the cells
    /// never have to actually be allocated. Grid values wrap after 9, starting again at 1
    /// </summary>
    public class CavernMap
    {
        readonly int[][] _riskLevels;

        /// <summary>
        /// The size of one side of the initial array of risk levels
        /// </summary>
        readonly int _size;

        /// <summary>
        /// A factor by which the grid is virtually extended out to the right and down.
        /// If this is two then the grid will be double the original width and height
        /// </summary>
        readonly int _expansion;

        int VirtualSize => _size * _expansion;

        /// <summary>
        /// The graph library we're using uses 1-based node indices, so the ending node
        /// index is just the size of the virtual grid
        /// </summary>
        uint LastCell => (uint)(VirtualSize * VirtualSize);


        public CavernMap(int[][] risks, int expansion)
        {
            _riskLevels = risks;
            _expansion = expansion;
            _size = risks.GetLength(0); 
        }

        public int LastRow => _size * _expansion;
        public int LastCol => _size * _expansion;

        /// <summary>
        /// The coordinate here can exceed the size of the initial risk grid as long
        /// as it doesn't extend past the "virtual edge"
        /// </summary>
        /// <returns>The computed risk level at this coordinate</returns>
        public int At(int row, int col)
        {
            if (row >= LastRow) throw new ArgumentOutOfRangeException("row");
            if (col >= LastCol) throw new ArgumentOutOfRangeException("col");

            int originalRow = row % _size;
            int originalCol = col % _size;
            int tilesDown  = row / _size;
            int tilesRight = col / _size;

            int derived = _riskLevels[originalRow][originalCol]
                            + tilesDown
                            + tilesRight;

            if (derived > 9)
                derived -= 9;

            return derived;
        }

        /// <summary>
        /// Build a graph from the expanded virtual risk map
        /// </summary>
        /// <returns>A Dijkstra.NET.Graph graph</returns>
        Graph<Coord, int> GenerateGraph()
        {
            var graph = new Graph<Coord, int>();

            // add all the graph vertices, labeling them with the grid coordinates they're
            // found at.  note: each call to AddNode() increments and returns a node index.
            // we would normally have to collect the node indices to remember where to find
            // them, but because we're iterating in a very predictable pattern we can just do
            // index math later (in NodeNumber) to find the graph node within the virtual map
            for (int row = 0; row < VirtualSize; row++)
            for (int col = 0; col < VirtualSize; col++)
                graph.AddNode(new Coord(row, col));

            // connect each node with an edge to all of its neighbours, resulting in two
            // directed edges between each pair of neighbouring nodes
            for (int row = 0; row < VirtualSize; row++)
            for (int col = 0; col < VirtualSize; col++)
            {
                var here = NodeNumber(row, col);

                foreach (var n in Neighbours(row, col))
                {
                    var there = NodeNumber(n.Row, n.Col);

                    // the cost of the edge to a neighbouring cell is the risk level
                    // of the neighbouring cell
                    int cost = At(n.Row, n.Col);

                    graph.Connect(here, there, cost, 0);
                }
            }

            return graph;
        }

        /// <summary>
        /// Convert a virtual row/col to the internal graph node number (1-indexed)
        /// </summary>
        uint NodeNumber(int row, int col)
        {
            return (uint)(row * VirtualSize + col + 1);            
        }

        /// <summary>
        /// Calculate the 4 neighbours of a cell (fewer if it's an edge or corner cell)
        /// </summary>
        List<Coord> Neighbours(int row, int col)
        {
            List<Coord> neighbours = new()
            {
                new Coord(row - 1, col),
                new Coord(row, col + 1),
                new Coord(row + 1, col),
                new Coord(row, col - 1)
            };

            // exclude coordinates that are off the map
            return neighbours
                .Where(n => n.Row >= 0 && n.Row < VirtualSize
                         && n.Col >= 0 && n.Col < VirtualSize)
                .ToList();
        }                

        /// <summary>
        /// Find the lowest-risk path between the top-left and bottom-right cells and return
        /// the total risk for that path
        /// </summary>
        /// <returns>
        /// The total risk (sum of edge weights between start/end cells) of the found path
        /// </returns>
        public int ShortestDiagonal()
        {
            var graph = GenerateGraph();

            ShortestPathResult path = graph.Dijkstra(1, LastCell);

            return path.Distance;
        }
    }


    public class Day15 : IAdventDay
    {
        int[][] _riskLevels;

        public Day15(IEnumerable<string> input)
        {
            _riskLevels = input
                .Select(ToDigitArray)
                .ToArray();
        }

        int[] ToDigitArray(string digits)
        {
            return digits
                .ToCharArray()
                .Select(c => c - '0')
                .ToArray();
        }

        public long Part1() => Search(expansion: 1);
        public long Part2() => Search(expansion: 5);

        long Search(int expansion)
        {
            var cavern = new CavernMap(_riskLevels, expansion);
            int risk = cavern.ShortestDiagonal();

            return risk;
        }
    }
}
