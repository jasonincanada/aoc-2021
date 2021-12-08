using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay08
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("8");
            var day = new Day08(input);

            Assert.Equal(26, day.Part1());
            Assert.Equal(61229, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(8);
            var day = new Day08(input);

            Assert.Equal(409, day.Part1());
            Assert.Equal(1024649, day.Part2());
        }
    }
}
