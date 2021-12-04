using AdventOfCode.CSharp;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestParsing
    {
        [Fact]
        public void TestStringWithNumber()
        {
            Assert.Equal(("test", 123), Parsing.StringWithNumber("test 123"));
        }

        [Fact]
        public void TestAllNumbers()
        {
            List<string> input = new() { "123", "-456" };
            List<int> parsed = Parsing.AllNumbers(input).ToList();

            Assert.Equal(123, parsed[0]);
            Assert.Equal(-456, parsed[1]);
        }

        [Fact]
        public void TestNumbersWithSpaces()
        {
            var input = "1 2 3";
            List<int> parsed = Parsing.NumbersWithSpaces(input).ToList();

            Assert.Equal(1, parsed[0]);
            Assert.Equal(3, parsed[2]);
        }

        [Fact]
        public void TestNumbersWithSpaces_SelectMany()
        {
            List<string> rows = new()
            {
                "22 13 17 11  0",
                " 8  2 23  4 24",
                "21  9 14 16  7",
                " 6 10  3 18  5",
                " 1 12 20 15 19"                
            };

            // from the constructor of Day04
            var five = rows.Take(5);
            var many = five.SelectMany(Parsing.NumbersWithSpaces);
            
            List<int> list = many.ToList();

            Assert.Equal(22, list.First());
            Assert.Equal(19, list.Last());
        }


    }
}
