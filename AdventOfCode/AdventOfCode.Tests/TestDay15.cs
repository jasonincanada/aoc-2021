using AdventOfCode.CSharp;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay15
    {
        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("15");
            var day = new Day15(input);

            Assert.Equal(40, day.Part1());
            Assert.Equal(315, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(15);
            var day = new Day15(input);

            Assert.Equal(415, day.Part1());
            Assert.Equal(2864, day.Part2());
        }

        [Fact]
        public void TestCavernMap()
        {
            int[][] grid = new int[][] {
                new int[] { 8, 9 },
                new int[] { 1, 2 }
            };

            var cavernMap = new CavernMap(grid, expansion: 2);

            // easy cells that are physically represented
            Assert.Equal(8, cavernMap.At(0, 0));
            Assert.Equal(9, cavernMap.At(0, 1));
            Assert.Equal(1, cavernMap.At(1, 0));
            Assert.Equal(2, cavernMap.At(1, 1));

            // virtual cells below
            Assert.Equal(9, cavernMap.At(0 + 2, 0));
            Assert.Equal(1, cavernMap.At(0 + 2, 1));
            Assert.Equal(2, cavernMap.At(1 + 2, 0));
            Assert.Equal(3, cavernMap.At(1 + 2, 1));

            // virtual cells to the right
            Assert.Equal(9, cavernMap.At(0, 0 + 2));
            Assert.Equal(1, cavernMap.At(0, 1 + 2));
            Assert.Equal(2, cavernMap.At(1, 0 + 2));
            Assert.Equal(3, cavernMap.At(1, 1 + 2));

            // virtual cells below and to the right
            Assert.Equal(1, cavernMap.At(0 + 2, 0 + 2));
            Assert.Equal(2, cavernMap.At(0 + 2, 1 + 2));
            Assert.Equal(3, cavernMap.At(1 + 2, 0 + 2));
            Assert.Equal(4, cavernMap.At(1 + 2, 1 + 2));
        }
    }
}
