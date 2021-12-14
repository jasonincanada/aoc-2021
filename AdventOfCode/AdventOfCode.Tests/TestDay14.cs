using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay14
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("14");
            var day = new Day14(input);

            Assert.Equal(1588, day.Part1());
            Assert.Equal(2188189693529, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(14);
            var day = new Day14(input);

            Assert.Equal(2027, day.Part1());
            Assert.Equal(2265039461737, day.Part2());
        }
    }
}
