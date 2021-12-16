using AdventOfCode.CSharp;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode.Tests
{
    public class TestDay16
    {
        [Fact]
        public void TestLiteralPacket()
        {
            var day = new Day16(new List<string>() { "D2FE28" });
            var packet = (LiteralPacket)day.GetOutermostPacket();

            Assert.Equal(2021, packet.LiteralValue);
        }

        [Fact]
        public void TestOperatorPacket()
        {
            var day = new Day16(new List<string>() { "38006F45291200" });
            var packet = (OperatorPacket)day.GetOutermostPacket();

            Assert.Equal(2, packet.SubPackets.Count);
            Assert.Equal(10, ((LiteralPacket)packet.SubPackets[0]).LiteralValue);
            Assert.Equal(20, ((LiteralPacket)packet.SubPackets[1]).LiteralValue);
        }

        [Fact]
        public void TestOperatorPacketThreeSubs()
        {
            var day = new Day16(new List<string>() { "EE00D40C823060" });
            var packet = (OperatorPacket)day.GetOutermostPacket();

            Assert.Equal(3, packet.SubPackets.Count);
            Assert.Equal(1, ((LiteralPacket)packet.SubPackets[0]).LiteralValue);
            Assert.Equal(2, ((LiteralPacket)packet.SubPackets[1]).LiteralValue);
            Assert.Equal(3, ((LiteralPacket)packet.SubPackets[2]).LiteralValue);
        }

        /// <summary>
        /// "8A004A801A8002F478 represents an operator packet (version 4) which contains an
        /// operator packet (version 1) which contains an operator packet (version 5) which
        /// contains a literal value (version 6); this packet has a version sum of 16."
        /// </summary>
        [Fact]
        public void TestOperatorPacketNested()
        {
            var day = new Day16(new List<string>() { "8A004A801A8002F478" });
            var packet = (OperatorPacket)day.GetOutermostPacket();

            Assert.Equal(4, packet.Version);
            Assert.Single(packet.SubPackets);
            Assert.Equal(1, packet.SubPackets[0].Version);

            Assert.Single(((OperatorPacket)packet.SubPackets[0]).SubPackets);
            Assert.Equal(5, ((OperatorPacket)packet.SubPackets[0]).SubPackets[0].Version);
            
            Assert.Equal(16, packet.VersionSum());
        }

        [Fact]
        public void TestSampleInput()
        {
            Assert.Equal(16, new Day16(new List<string>() { "8A004A801A8002F478" }).Part1());
            Assert.Equal(12, new Day16(new List<string>() { "620080001611562C8802118E34" }).Part1());
            Assert.Equal(23, new Day16(new List<string>() { "C0015000016115A2E0802F182340" }).Part1());
            Assert.Equal(31, new Day16(new List<string>() { "A0016C880162017C3686B18A3D4780" }).Part1());
        }

        [Fact]
        public void TestSampleInputPart2()
        {
            Assert.Equal(3, new Day16(new List<string>() { "C200B40A82" }).Part2());
            Assert.Equal(54,new Day16(new List<string>() { "04005AC33890" }).Part2());
            Assert.Equal(7, new Day16(new List<string>() { "880086C3E88112" }).Part2());
            Assert.Equal(9, new Day16(new List<string>() { "CE00C43D881120" }).Part2());
            Assert.Equal(1, new Day16(new List<string>() { "D8005AC2A8F0" }).Part2());
            Assert.Equal(0, new Day16(new List<string>() { "F600BC2D8F" }).Part2());
            Assert.Equal(0, new Day16(new List<string>() { "9C005AC2F8F0" }).Part2());
            Assert.Equal(1, new Day16(new List<string>() { "9C0141080250320F1802104A08" }).Part2());
        }

        [Fact]
        public void TestMyInput()
        {
            var input = Common.GetInput(16);
            var day = new Day16(input);

            Assert.Equal(883, day.Part1());
            Assert.Equal(1675198555015, day.Part2());
        }
    }
}
