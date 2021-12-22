using AdventOfCode.CSharp;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay19
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("19");
            var day = new Day19(input);

            Assert.Equal(79, day.Part1());
            Assert.Equal(2, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(19);
            var day = new Day19(input);

            Assert.Equal(3, day.Part1());
            Assert.Equal(4, day.Part2());
        }
    }
}
