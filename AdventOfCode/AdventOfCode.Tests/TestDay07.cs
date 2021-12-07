using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay07
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("7");
            var day = new Day07(input);

            Assert.Equal(37, day.Part1());
            Assert.Equal(168, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(7);
            var day = new Day07(input);

            Assert.Equal(344138, day.Part1());
            Assert.Equal(94862124, day.Part2());
        }
    }
}
