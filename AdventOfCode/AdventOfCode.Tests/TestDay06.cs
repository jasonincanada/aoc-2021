using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay06
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("6");
            var day = new Day06(input);

            Assert.Equal(5934, day.Part1());
            Assert.Equal(26984457539, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(6);
            var day = new Day06(input);

            Assert.Equal(389726, day.Part1());
            Assert.Equal(1743335992042, day.Part2());
        }
    }
}
