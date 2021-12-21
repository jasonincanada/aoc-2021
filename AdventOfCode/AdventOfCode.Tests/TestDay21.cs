using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay21
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("21");
            var day = new Day21(input);

            Assert.Equal(739785, day.Part1());
            Assert.Equal(444356092776315, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(21);
            var day = new Day21(input);

            Assert.Equal(504972, day.Part1());
            Assert.Equal(4, day.Part2());
        }
    }
}
