using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay11
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("11");
            var day = new Day11(input);

            Assert.Equal(1656, day.Part1());
            Assert.Equal(195, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(11);
            var day = new Day11(input);

            Assert.Equal(1688, day.Part1());
            Assert.Equal(403, day.Part2());
        }
    }
}
