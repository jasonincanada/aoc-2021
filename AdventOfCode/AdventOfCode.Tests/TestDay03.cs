using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay03
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("3");
            var day = new Day03(input);

            Assert.Equal(198, day.Part1());
            Assert.Equal(230, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(3);
            var day = new Day03(input);

            Assert.Equal(3277364, day.Part1());
            Assert.Equal(5736383, day.Part2());
        }
    }
}
