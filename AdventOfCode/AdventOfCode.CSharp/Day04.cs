namespace AdventOfCode.CSharp
{
    class BingoCard
    {
        /// <summary>
        /// The 25 squares of the bingo card with their dabbed status. Instead of a two-dimensional
        /// array we keep one line of squares and do index math to check if a column is fully dabbed
        /// </summary>
        List<Square> _squares;

        /// <summary>
        /// Set to true in CheckForWin() if the latest dab completes a row or column
        /// </summary>
        public bool IsWinner { get; private set; } = false;

        public BingoCard(IEnumerable<string> rows)
        {
            /*   22 13 17 11  0
             *    8  2 23  4 24
             *   21  9 14 16  7
             *    6 10  3 18  5
             *    1 12 20 15 19
             */

            _squares = rows
                .Take(5)
                .SelectMany(Parsing.NumbersWithSpaces)
                .Select(number => new Square(number))
                .ToList();
        }

        /// <summary>
        /// If the card has this number, dab it and check to see if we've won
        /// </summary>
        public void Dab(int number)
        {            
            int i = _squares.IndexWhere(s => s.Number == number);

            if (i < 0)
                return;

            _squares[i].IsDabbed = true;

            // with a new square dabbed, check its column/row to see if the card is a winner
            CheckForWin(i);
        }

        /// <summary>
        /// Check the row and/or column of a given square to see if all squares are dabbed
        /// </summary>
        /// <param name="index">Zero-based index into the _squares list</param>
        void CheckForWin(int index)
        {
            IEnumerable<int> rows = Enumerable
                .Range(RoundDown(index, 5), 5);
                     
            IEnumerable<int> cols = Enumerable
                .Range(0, 5)
                .Select(i => i * 5 + index)    // eg: 14, 19, 24, 29, 34
                .Select(i => i % 25);          //     14, 19, 24, 4,  9

            if (    rows.All(i => _squares[i].IsDabbed)
                 || cols.All(i => _squares[i].IsDabbed))
            {
                IsWinner = true;
            }
        }

        /// <summary>
        /// This formula is from https://stackoverflow.com/a/9303816/229717
        /// </summary>
        public static int RoundDown(int num, int v)
        {
            return (int)(Math.Floor((double)num / v) * v);
        }

        /// <summary>
        /// The result is computed using the sum of the undabbed squares
        /// </summary>
        public int SumUndabbed()
        {
            return _squares
                .Where(s => !s.IsDabbed)
                .Select(s => s.Number)
                .Sum();
        }
    }

    class Square
    {
        public Square(int number)
        {
            Number = number;
            IsDabbed = false;
        }

        public int Number { get; }
        public bool IsDabbed { get; set; }
    }


    public class Day04 : IAdventDay
    {
        readonly List<int> _numbersCalled;
        readonly List<BingoCard> _bingoCards = new List<BingoCard>();

        public Day04(IEnumerable<string> input)
        {
            _numbersCalled = input
                .Select(Parsing.NumbersWithCommas)
                .First()
                .ToList();

            var rows = input.Skip(2);

            while (rows.Count() > 0)
            {
                _bingoCards.Add(new BingoCard(rows));

                // skip the 5 card rows and a blank line after them if there
                rows = rows.Skip(5 + 1);
            }
        }


        public int Part1() => PlayBingo(lastCardToWin: false);
        public int Part2() => PlayBingo(lastCardToWin: true);

        int PlayBingo(bool lastCardToWin)
        {
            foreach (var number in _numbersCalled)
            foreach (var card in _bingoCards)
            {
                card.Dab(number);

                if (card.IsWinner)
                {
                    // if lastCardToWin is true (part 2) we have to make sure all the cards
                    // are winners before returning the answer
                    if (lastCardToWin)
                    {
                        if (_bingoCards.All(c => c.IsWinner))
                            return card.SumUndabbed() * number;
                    }
                    else
                    {
                        return card.SumUndabbed() * number;
                    }
                }
            }

            throw new Exception("No card was a winner");
        }
    }
}
