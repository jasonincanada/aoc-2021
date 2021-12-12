namespace AdventOfCode.CSharp
{
    /*
            start-A            
            start-b               start     
            A-c                   /   \     
            A-b        ->     c--A-----b--d     
            b-d                   \   /     
            A-end                  end
            b-end
     */
    class CaveSystem
    {
        /// <summary>
        /// Map from each cave to its immediate neighbours
        /// </summary>
        Dictionary<string, List<string>> _from = new();

        /// <summary>
        /// The number of steps we've taken to reach each cave. This evolves throughout the
        /// path-finding process, and importantly the lists are deleted from regularly when
        /// backtracking to a prior branching point
        /// </summary>
        Dictionary<string, List<int>> _pastVisits = new();

        /// <summary>
        /// We only have to count the paths and not collect them
        /// </summary>
        int _pathCount;

        readonly int _part;


        public CaveSystem(IEnumerable<string> input, int part)
        {
            // start-A
            // A-c
            // c-end
            foreach (var edge in input)
            {
                var names = edge.Split('-');
                AddEdge(names[0], names[1]);
            }

            // for every small cave, initialize its visit history to blank
            var caves = _from.Keys.Where(IsSmallCave);

            foreach (var cave in caves)
                _pastVisits.Add(cave, new());

            _part = part;
        }

        bool IsSmallCave(string cave) => char.IsLower(cave[0]);

        void AddEdge(string cave1, string cave2)
        {
            FromTo(cave1, cave2);
            FromTo(cave2, cave1);

            void FromTo(string from, string to)
            {
                if (to == "start") return;
                if (from == "end") return;

                _from.AddToListUnique(from, to);                
            }
        }

        public int CountPaths()
        {
            SearchFrom("start", steps: 1);
            return _pathCount;
        }

        /// <summary>
        /// Recursively try each cave next to this one, keeping track of the step counts we
        /// are at when we visit each cave. This allows us to backtrack by forgetting any caves
        /// we "visited in the future" after returning back from a recursive call
        /// </summary>
        /// <param name="cave"></param>
        /// <param name="steps">Number of steps taken to get here</param>
        void SearchFrom(string cave, int steps)
        {
            // if we've arrived at a small cave, record when we got here
            if (IsSmallCave(cave))
                _pastVisits[cave].Add(steps);

            // explore all valid paths outwards from here one at a time
            foreach (var next in _from[cave])
            {                
                if (next == "end")
                {
                    // we can get to the end node from here, but don't bother obviously,
                    // just count it and move to the next neighbouring cave
                    _pathCount++;
                    continue;
                }
               
                // big caves have no restrictions, but small caves can be visited at most
                // once in part 1, and only one of them can be visited more than once in part 2
                if (IsSmallCave(next))
                {
                    // if we've been here before
                    if (_pastVisits[next].Any())
                    {
                        // in part 1, never visit the same cave again
                        if (_part == 1)
                            continue;

                        // in part 2, skip this one if any cave has been visited twice already
                        if (_pastVisits.Any(cave => cave.Value.Count > 1))
                            continue;
                    }
                }

                // we have permission to explore this cave, so recursively call this again
                SearchFrom(next, steps + 1);

                // having returned from an arbitrarily long path, we have to undo the effects
                // of the path-taking by forgetting the visited status of any cave that we
                // visited after this older call of SearchFrom that we're now back in
                foreach (var k in _pastVisits.Keys)
                {
                    // keep only this and prior step counts
                    _pastVisits[k] = _pastVisits[k]
                        .Where(s => s <= steps)
                        .ToList();
                }
            }
        }
    }

    public class Day12 : IAdventDay
    {
        readonly IEnumerable<string> _input;

        public Day12(IEnumerable<string> input) { _input = input; }

        public long Part1() => new CaveSystem(_input, 1).CountPaths();
        public long Part2() => new CaveSystem(_input, 2).CountPaths();
    }
}
