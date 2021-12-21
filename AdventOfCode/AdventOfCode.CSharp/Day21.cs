namespace AdventOfCode.CSharp
{
    interface IDice
    {
        int Roll();
        int RollCount();
    }

    /// <summary>
    /// The die described in part 1 rolls 1,2,3...100 and wraps around after 100 to 1
    /// </summary>
    class DeterministicDice : IDice
    {
        int _nextRoll = 1;
        int _rolls = 0;

        public int Roll()
        {
            int roll = _nextRoll;

            if (++_nextRoll > 100)
                _nextRoll = 1;

            _rolls++;

            return roll;
        }

        public int RollCount() => _rolls;
    }

    public class Day21 : IAdventDay
    {
        readonly List<int> _positions;
                
        // Player 1 starting position: 8
        // Player 2 starting position: 4        
        public Day21(IEnumerable<string> input)
        {
            _positions = input
                .Select(ToPosition)
                .ToList();
        }

        int ToPosition(string line)
        {
            return int.Parse(line.Split(' ').Last());     
        }
                
        // Part 1 can be simulated but part 2 will require some computation
        public long Part1() => Play(new DeterministicDice(), winningScore: 1000);
        public long Part2() => Compute(_positions[0], _positions[1], winningScore: 21);

        long Play(IDice dice, int winningScore)
        {
            var positions = _positions.ToList();
            var scores = positions.Select(_ => 0).ToList();
            int player = 0;

            while (true)
            {
                // roll three times
                int total = dice.Roll() + dice.Roll() + dice.Roll();

                Move(player, total);

                scores[player] += positions[player];
                if (scores[player] >= winningScore)
                {
                    long losingScore = scores[OtherPlayer()];
                    long rolls = dice.RollCount();
                    return losingScore * rolls;
                }

                player = OtherPlayer();
            }

            int OtherPlayer() => player == 1 ? 0 : 1;

            // move the player around the clock, wrapping after 10
            void Move(int p, int steps)
            {
                positions[p] += steps % 10;

                if (positions[p] > 10)
                    positions[p] -= 10;
            }
        }

        /// <summary>
        /// This is necessarily some computation since simulating it is not feasible. Setting
        /// as static to emphasize that it can be computed with only these three starting
        /// parameters (the two starting positions and the winning score)
        /// </summary>
        static long Compute(int position1, int position2, int winningScore)
        {
            long accum = 0;
            
            // assuming for now we can do this in winningScore iterations
            for (int i = 1; i <= winningScore; i++)
            {

                accum += 0;
            }

            return accum;
        }
    }
}
