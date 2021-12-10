using AdventOfCode.CSharp;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay10
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("10");
            var day = new Day10(input);

            Assert.Equal(26397, day.Part1());
            Assert.Equal(288957, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(10);
            var day = new Day10(input);

            Assert.Equal(318099, day.Part1());
            Assert.Equal(2389738699, day.Part2());
        }

        [Fact]
        public void TestScoreMissing()
        {
            Assert.Equal(288957,  Day10.ScoreMissing("}}]])})]".ToCharArray()));
            Assert.Equal(5566,    Day10.ScoreMissing(")}>]})".ToCharArray()));
            Assert.Equal(1480781, Day10.ScoreMissing("}}>}>))))".ToCharArray()));
            Assert.Equal(995444,  Day10.ScoreMissing("]]}}]}]}>".ToCharArray()));
            Assert.Equal(294,     Day10.ScoreMissing("])}>".ToCharArray()));
        }

        [Fact]
        public void TestStackToMissing()
        {
            string opens  = "([{<";
            string closes = ">}])";

            Stack<char> stack = new(opens.ToCharArray());
            
            Assert.Equal(closes, Day10.StackToMissing(stack));
        }
    }
}
