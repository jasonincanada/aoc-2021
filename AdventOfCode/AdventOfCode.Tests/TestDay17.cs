using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay17
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("17");
            var day = new Day17(input);

            Assert.Equal(45, day.Part1());
            Assert.Equal(112, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(17);
            var day = new Day17(input);

            Assert.Equal(12090, day.Part1());
            Assert.Equal(5059, day.Part2());
        }
    }
}
