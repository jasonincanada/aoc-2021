using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay12
    {
        [Fact]
        public void Test_Part1_SampleInput1()
        {
            var input = Common.GetSampleInput("12");
            var day = new Day12(input);

            Assert.Equal(10, day.Part1());
        }

        [Fact]
        public void Test_Part2_SampleInput1()
        {
            var input = Common.GetSampleInput("12");
            var day = new Day12(input);
            
            Assert.Equal(36, day.Part2());
        }

        [Fact]
        public void Test_Part1_SampleInput2()
        {
            var input = Common.GetSampleInput("12-2");
            var day = new Day12(input);

            Assert.Equal(19, day.Part1());
        }

        [Fact]
        public void Test_Part2_SampleInput2()
        {
            var input = Common.GetSampleInput("12-2");
            var day = new Day12(input);

            Assert.Equal(103, day.Part2());
        }
        [Fact]
        public void Test_Part1_SampleInput3()
        {
            var input = Common.GetSampleInput("12-3");
            var day = new Day12(input);

            Assert.Equal(226, day.Part1());
        }

        [Fact]
        public void Test_Part2_SampleInput3()
        {
            var input = Common.GetSampleInput("12-3");
            var day = new Day12(input);

            Assert.Equal(3509, day.Part2());
        }

        [Fact]
        public void Test_Part1_MyInput()
        {
            var input = Common.GetInput(12);
            var day = new Day12(input);

            Assert.Equal(4338, day.Part1());
        }

        [Fact]
        public void Test_Part2_MyInput()
        {
            var input = Common.GetInput(12);
            var day = new Day12(input);

            Assert.Equal(114189, day.Part2());
        }
    }
}
