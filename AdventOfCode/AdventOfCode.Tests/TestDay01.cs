using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay01
    {
        IAdventDay GetSampleInput(string fileId)
        {
            var input = Common.GetSampleInput(fileId);
            return new Day01(input);
        }

        IAdventDay GetMyInput(int fileId)
        {
            var input = Common.GetInput(fileId);
            return new Day01(input);
        }


        [Fact]
        public void TestSamplePart1()
        {
            Assert.Equal(7, GetSampleInput("1").Part1());
        }

        [Fact]
        public void TestSamplePart2()
        {
            Assert.Equal(5, GetSampleInput("1").Part2());
        }

        [Fact]
        public void TestPart1()
        {
            Assert.Equal(1696, GetMyInput(1).Part1());
        }

        [Fact]
        public void TestPart2()
        {
            Assert.Equal(1737, GetMyInput(1).Part2());
        }
    }
}
