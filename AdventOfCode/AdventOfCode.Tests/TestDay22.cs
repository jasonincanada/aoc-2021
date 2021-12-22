using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay22
    {
        [Fact]
        public void TestSampleInput1()
        {
            var input = Common.GetSampleInput("22-1");
            var day = new Day22(input);

            Assert.Equal(39, day.Part1());
            Assert.Equal(1, day.Part2());
        }

        [Fact]
        public void TestSampleInput2()
        {
            var input = Common.GetSampleInput("22-2");
            var day = new Day22(input);

            Assert.Equal(590784, day.Part1());
            Assert.Equal(2, day.Part2());
        }

        [Fact]
        public void TestSampleInput3()
        {
            var input = Common.GetSampleInput("22-3");
            var day = new Day22(input);

            Assert.Equal(474140, day.Part1());
            Assert.Equal(2758514936282235, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(22);
            var day = new Day22(input);

            Assert.Equal(577205, day.Part1());
            Assert.Equal(4, day.Part2());
        }
    }
}
