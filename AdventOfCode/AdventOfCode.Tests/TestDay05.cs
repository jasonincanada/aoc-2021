using AdventOfCode.CSharp;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay05
    {
        [Fact]
        public void TestLineConstructor()
        {
            Line line = Line.FromInput("12,34 -> 56,78");

            Assert.Equal(12, line.X1);
            Assert.Equal(34, line.Y1);
            Assert.Equal(56, line.X2);
            Assert.Equal(78, line.Y2);
        }

        [Fact]
        public void TestLineCoordsVert()
        {
            Line line = Line.FromInput("2,2 -> 2,1");
            List<(int, int)> coords = line.GetCoords();

            Assert.Equal((2, 2), coords[0]);
            Assert.Equal((2, 1), coords[1]);
        }

        [Fact]
        public void TestLineCoordsDiagonal()
        {
            var coords = Line.FromInput("1,1 -> 3,3").GetCoords();
            Assert.Equal((1, 1), coords[0]);
            Assert.Equal((2, 2), coords[1]);
            Assert.Equal((3, 3), coords[2]);

            coords = Line.FromInput("9,7 -> 7,9").GetCoords();
            Assert.Equal((9, 7), coords[0]);
            Assert.Equal((8, 8), coords[1]);
            Assert.Equal((7, 9), coords[2]);
        }

        [Fact]
        public void TestSampleInput()
        {
            var input = Common.GetSampleInput("5");
            var day = new Day05(input);

            Assert.Equal(5, day.Part1());
            Assert.Equal(12, day.Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(5);
            var day = new Day05(input);

            Assert.Equal(6687, day.Part1());
            Assert.Equal(19851, day.Part2());
        }
    }
}
