using AdventOfCode.CSharp;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay18
    {
        [Fact]
        public void TestParser()
        {
            (var num, var rest) = SnailParser.FromString("[1,2]");

            Assert.IsType<SnailPair>(num);
            var pair = (SnailPair)num;

            SnailNumber left = pair.Left;
            SnailNumber right = pair.Right;

            Assert.Equal(1, ((RegularNumber)left).Number);
            Assert.Equal(2, ((RegularNumber)right).Number);
            Assert.Equal(string.Empty, rest);
        }

        [Fact]
        public void TestMagnitude()
        {
            var tests = new List<(string, int)>
            {
                ("[9,1]", 29),
                ("[[1,2],[[3,4],5]]", 143),
                ("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", 1384),
                ("[[[[1,1],[2,2]],[3,3]],[4,4]]", 445),
                ("[[[[3,0],[5,3]],[4,4]],[5,5]]", 791),
                ("[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137),
                ("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]",3488),
                ("[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]", 4140),
            };

            foreach ((string input, int expected) in tests)
            {
                (SnailNumber num, string rest) = SnailParser.FromString(input);

                Assert.Equal(string.Empty, rest);
                Assert.Equal(expected, ((SnailPair)num).Magnitude());
            } 
        }

        [Fact]
        public void TestToString()
        {
            string test = "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]";
            
            // test round trip from string to object to string again
            Assert.Equal(test, SnailParser.FromString(test).Item1.ToString());
        }

        [Fact]
        public void TestExplode()
        {
            var input = "[[6,[5,[4,[3,2]]]],1]";
            var expected = "[[6,[5,[7,0]]],3]";

            (var num, _) = SnailParser.FromString(input);
            //SnailOps.Explode((SnailPair)num);

            //Assert.Equal(expected, num.ToString());
        }

        [Fact]
        public void TestLeafIndexes()
        {
            var input = "[[6,[5,[4,[3,2]]]],1]";

            (var num, _) = SnailParser.FromString(input);

            num.IndexFrom(1);
        }

        [Fact]
        public void TestToLeaves()
        {
            var input = "[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]";
            var expected = "3,2,1,7,3,6,5,4,3,2";

            (SnailNumber pair, _) = SnailParser.FromString(input);
            var leaves = ((SnailPair)pair).ToLeaves();

            Assert.Equal(expected, string.Join(",", leaves));
        }

        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("18");
            var day = new Day18(input);

            Assert.Equal(4140, day.Part1());
            Assert.Equal(2, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(18);
            var day = new Day18(input);

            Assert.Equal(3, day.Part1());
            Assert.Equal(4, day.Part2());
        }
    }
}
