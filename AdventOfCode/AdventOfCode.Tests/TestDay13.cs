using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay13
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("13");
            var day = new Day13(input);

            Assert.Equal(17, day.Part1());
            Assert.Equal(16, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(13);
            var day = new Day13(input);

            Assert.Equal(847, day.Part1());
            Assert.Equal(104, day.Part2());
        }
    }
}
