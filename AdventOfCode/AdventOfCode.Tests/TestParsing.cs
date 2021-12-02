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
    }
}
