namespace AdventOfCode.CSharp
{
    public abstract class SnailNumber 
    {
        public abstract int Magnitude();
        public abstract List<LeafSummary> ToLeaves();

        public static SnailPair Add(SnailNumber left, SnailNumber right)
        {
            return new SnailPair(left, right);
        }

        public abstract int IndexFrom(int start);
    }

    public class SnailPair : SnailNumber
    {
        public SnailNumber Left { get; private set; }
        public SnailNumber Right { get; private set; }

        public SnailPair(SnailNumber left, SnailNumber right)
        {
            Left = left; 
            Right = right;
        }

        public override int Magnitude()
        {
            return 3 * Left.Magnitude() 
                 + 2 * Right.Magnitude();
        }

        public override string ToString() => $"[{Left},{Right}]";

        public override List<LeafSummary> ToLeaves()
        {
            List<LeafSummary> left = Left.ToLeaves();
            List<LeafSummary> right = Right.ToLeaves();

            return left.Concat(right).ToList();
        }

        public override int IndexFrom(int start)
        {
            int lastUsed = Left.IndexFrom(start);

            return Right.IndexFrom(lastUsed);
        }
    }

    public class RegularNumber : SnailNumber
    {
        public int Number { get; private set; }
        public int LeafIndex { get; private set; }
        

        public RegularNumber(int number)
        {
            Number = number;
            LeafIndex = -1;
        }

        public override int Magnitude() => Number;
        public override string ToString() => Number.ToString();        

        public override List<LeafSummary> ToLeaves()
        {
            return new();
        }

        public override int IndexFrom(int start)
        {
            LeafIndex = start;

            return start + 1;
        }
    }

    public record LeafSummary(RegularNumber Leaf, SnailPair Parent, int Depth);

    public static class SnailOps
    {
        public static List<LeafSummary> SummarizeLeaves(SnailNumber num)
        {
            return new List<LeafSummary>();

        }

        public static void Evolve(SnailPair pair)
        {
            // re-index the whole SnailNumber to order the leaf nodes (regular numbers)
            pair.IndexFrom(1);

            SnailPair? first4DeepFromLeft = First4DeepFromLeft(pair);
            //RegularNumber? firstSplittable = FirstSplittable(pair);



        }

        /// <summary>
        /// Return the first SnailPair (or null if none) that is deep enough to required exploding
        /// </summary>
        /// <param name="pair"></param>
        /// <returns>The first SnailPair from the left that is 5 levels deep</returns>
        static SnailPair? First4DeepFromLeft(SnailPair pair)
        {
            throw new NotImplementedException();            
        }
    }

    public class SnailParser
    {
        /// <summary>
        /// Process a string like [[[[1,2],[3,4]],[[5,6],[7,8]]],9] to its SnailNumber object
        /// </summary>
        public static (SnailNumber, string) FromString(string input)
        {
            // a digit always starts a regular number and will be a leaf node in the tree
            if (char.IsDigit(input.First()))
            {
                int digit = input.First() - '0';
                return (new RegularNumber(digit), input[1..]);
            }

            (SnailNumber left, string rest) = FromString(input[1..]);
            (SnailNumber right, string leftover) = FromString(rest[1..]);

            return (new SnailPair(left, right), leftover[1..]);
        }

        public static void NumberLeaves(SnailNumber number)
        {
            number.IndexFrom(1);
        }
    }

    public class Day18 : IAdventDay
    {
        readonly int _top;

        // target area: x=20..30, y=-10..-5
        public Day18(IEnumerable<string> input)
        {
        }

        public long Part1() => -1;
        public long Part2() => -2;

    }
}
