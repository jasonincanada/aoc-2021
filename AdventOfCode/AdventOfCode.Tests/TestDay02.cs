using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay02
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("2");
            var day = new Day02(input);

            Assert.Equal(150, day.Part1());
            Assert.Equal(900, day.Part2());
        }
        
        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(2);
            var day = new Day02(input);

            Assert.Equal(1459206, day.Part1());
            Assert.Equal(1320534480, day.Part2());
        }
    }
}
