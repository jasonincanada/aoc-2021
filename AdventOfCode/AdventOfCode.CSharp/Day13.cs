namespace AdventOfCode.CSharp
{
    record Dot(int Col, int Row);
    record Fold(bool x, int Where);

    public class Day13 : IAdventDay
    {
        List<Dot>  _dots;
        List<Fold> _folds;

        /*
            2,14
            8,10
            9,0

            fold along y=7
            fold along x=5
         */
        public Day13(IEnumerable<string> input) 
        {
            _dots = input
                .Where(line => line.Contains(','))
                .Select(ToDot)
                .ToList();

            _folds = input
                .Where(line => line.StartsWith("fold"))
                .Select(ToFold)
                .ToList();
        }

        Dot ToDot(string line)
        {
            var xy = line.Split(',');

            return new Dot(int.Parse(xy[0]), 
                           int.Parse(xy[1]));
        }

        Fold ToFold(string line)
        {
            bool x = line[11] == 'x';            
            int amount = int.Parse(line.Substring(13));

            return new Fold(x, amount);
        }

        void DoFold(Fold fold)
        {    
            _dots = _dots
                .Select(dot => FoldDot(fold, dot))
                .Distinct()
                .ToList();         
        }

        static Dot FoldDot(Fold fold, Dot dot)
        {
            if (fold.x)
            {
                if (dot.Col < fold.Where)
                    return dot;

                return new Dot(fold.Where - (dot.Col - fold.Where), dot.Row);
            }
            else
            {
                if (dot.Row < fold.Where)
                    return dot;

                return new Dot(dot.Col, fold.Where - (dot.Row - fold.Where));
            }
        }

        public long Part1() 
        {
            DoFold(_folds.First());
            return _dots.Count;           
        }

        public long Part2()
        {
            foreach (var fold in _folds)
                DoFold(fold);

            /*
             *   ###   ##  #### ###   ##  ####  ##  ###
             *   #  # #  #    # #  # #  # #    #  # #  #
             *   ###  #      #  #  # #    ###  #  # ###
             *   #  # #     #   ###  #    #    #### #  #
             *   #  # #  # #    # #  #  # #    #  # #  #
             *   ###   ##  #### #  #  ##  #### #  # ###
             */
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 5 * 8; x++)
                {
                    char dot = _dots.Contains(new Dot(x, y))
                        ? '#'
                        : ' ';

                    Console.Write(dot);
                }

                Console.WriteLine();
            }

            return _dots.Count;
        }
    }
}
