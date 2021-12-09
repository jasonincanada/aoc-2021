using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay09
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("9");
            var day = new Day09(input);

            Assert.Equal(15, day.Part1());
            Assert.Equal(1134, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(9);
            var day = new Day09(input);

            Assert.Equal(500, day.Part1());
            Assert.Equal(970200, day.Part2());
        }
    }
}
