using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay20
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("20");
            var day = new Day20(input);

            Assert.Equal(35, day.Part1());
            Assert.Equal(3351, day.Part2());
        }

        [Fact]
        public void TestSampleInput2()
        {
            var input = Common.GetSampleInput("20-2");
            var day = new Day20(input);

            Assert.Equal(5326, day.Part1());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(20);
            var day = new Day20(input);

            Assert.Equal(5503, day.Part1());
            Assert.Equal(19156, day.Part2());
        }
    }
}
