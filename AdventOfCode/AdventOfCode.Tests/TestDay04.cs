using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay04
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("4");
            var day = new Day04(input);

            Assert.Equal(4512, day.Part1());
            Assert.Equal(1924, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(4);
            var day = new Day04(input);

            Assert.Equal(51034, day.Part1());
            Assert.Equal(5434, day.Part2());
        }

        [Fact]
        public void TestRound()
        {
            Assert.Equal(0, BingoCard.RoundDown(0, 5));
            Assert.Equal(0, BingoCard.RoundDown(1, 5));
            Assert.Equal(0, BingoCard.RoundDown(2, 5));
            Assert.Equal(0, BingoCard.RoundDown(3, 5));
            Assert.Equal(0, BingoCard.RoundDown(4, 5));

            // This should flick over to 5
            Assert.Equal(5, BingoCard.RoundDown(5, 5));
        }
    }
}
