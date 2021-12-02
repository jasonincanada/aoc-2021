using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay01
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("1");
            var day = new Day01(input);

            Assert.Equal(7, day.Part1());
            Assert.Equal(5, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(1);
            var day = new Day01(input);

            Assert.Equal(1696, day.Part1());
            Assert.Equal(1737, day.Part2());
        }
    }
}
