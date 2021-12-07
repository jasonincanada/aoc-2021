namespace AdventOfCode.CSharp
{
    public class Day07 : IAdventDay
    {
        readonly List<int> _crabs;

        public Day07(IEnumerable<string> input)
        {
            _crabs = Parsing.NumbersWithCommas(input.First()).ToList();
        }

        public long Part1() => MinCostBy(CostOfMovingTo1);
        public long Part2() => MinCostBy(CostOfMovingTo2);
                
        public long MinCostBy(Func<int,int> costfunction) 
        {
            // inefficient but simple, try every location between the first and last crabs
            int min = _crabs.Min();
            int max = _crabs.Max();

            List<int> costs = Enumerable
                .Range(min, max - min + 1)
                .Select(costfunction)
                .ToList();

            return costs.Min();
        }

        /// <summary>
        /// Total cost of moving the whole crab fleet to <paramref name="position"/>
        /// </summary>
        int CostOfMovingTo1(int position)
        {
            var costs = from c in _crabs
                        let distance = Math.Abs(c - position)
                        select distance;

            return costs.Sum();
        }

        int CostOfMovingTo2(int position)
        {
            var costs = from c in _crabs
                        let distance = Math.Abs(c - position)
                        select distance * (distance + 1) / 2;

            return costs.Sum();
        }
    }
}
