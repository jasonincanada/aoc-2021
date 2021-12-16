namespace AdventOfCode.CSharp
{
    public abstract class BasePacket
    {
        public int Version { get; private set; }

        public BasePacket(int version)
        {
            Version = version;
        }

        public abstract int Size();
        public abstract int VersionSum();
        public abstract long Value();
    }

    public class LiteralPacket : BasePacket
    {
        public long LiteralValue { get; private set; }

        /// <summary>
        /// Count of bits used to store the literal in the original bitstream
        /// </summary>
        public int LiteralBits { get; private set; }

        public LiteralPacket(int version, long val, int bits)
            : base(version)
        {            
            LiteralValue = val;
            LiteralBits  = bits;
        }

        /// <summary>
        /// Compute the length of bits that must have been needed to construct this packet
        /// </summary>
        public override int Size()
        {
            const int version = 3;
            const int typeId = 3;

            return version + typeId + LiteralBits;
        }

        public override int VersionSum() => Version;
        public override long Value() => LiteralValue;
    }

    /// <summary>
    /// In operator packets, the length represents either the number of packets that
    /// it immediately contains or the total number of bits in all subpackets
    /// </summary>
    public enum LengthType
    {
        PacketCount,
        BitCount
    }

    public class OperatorPacket : BasePacket
    {
        /// <summary>
        /// Nested packets, arbitrarily deep
        /// </summary>
        public List<BasePacket> SubPackets { get; private set; }
        public LengthType LengthType { get; private set; }
        public int OperatorType { get; private set; }

        public OperatorPacket(int version, int type, LengthType lengthType, IEnumerable<BasePacket> subs)
            : base(version)
        {
            OperatorType = type;
            LengthType = lengthType;
            SubPackets = subs.ToList();
        }

        /// <summary>
        /// Compute the length of bits that must have been needed to construct this packet
        /// </summary>
        public override int Size()
        {
            const int version = 3;
            const int typeId = 3;
            int size = version + typeId;

            size++; // length type bit

            if (LengthType == LengthType.PacketCount)
                size += 11;
            else
                size += 15;

            int subpackets = SubPackets
                .Select(p => p.Size())
                .Sum();

            size += subpackets;

            return size;
        }

        public override int VersionSum()
        {
            int subs = SubPackets
                .Select(p => p.VersionSum())
                .Sum();

            return subs + Version;
        }

        public override long Value()
        {
            return OperatorType switch
            {
                0 => SubPackets.Select(packet => packet.Value()).Sum(),
                1 => SubPackets.Select(packet => packet.Value()).Product(),
                2 => SubPackets.Min(packet => packet.Value()),
                3 => SubPackets.Max(packet => packet.Value()),
                5 => SubPackets[0].Value() >  SubPackets[1].Value() ? 1 : 0,
                6 => SubPackets[0].Value() <  SubPackets[1].Value() ? 1 : 0,
                7 => SubPackets[0].Value() == SubPackets[1].Value() ? 1 : 0,
                _ => throw new NotImplementedException($"OperatorPacket with OperatorType {OperatorType}"),
            };
        }
    }

    class PacketParser
    {
        IEnumerable<int> _stream;

        public PacketParser(string hex)
        {
            _stream = new BitStream(hex).Bits();
        }

        public BasePacket NextPacket()
        {
            int version = (int)GetNBitsAsLong(3);
            int type    = (int)GetNBitsAsLong(3);
            
            // operator packet
            if (type != 4)
            {
                (var lengthType, var subs) = GetNextBit() == 0                    
                    ? (LengthType.BitCount,    SubsByBitCount())
                    : (LengthType.PacketCount, SubsByPacketCount());
                
                return new OperatorPacket(version, type, lengthType, subs);
            }

            // literal value packet
            else
            {
                (long literalValue, int bitcount) = GetLiteral();
                return new LiteralPacket(version, literalValue, bitcount);
            }
        }

        /// <summary>
        /// Extract a numeric literal from the stream by taking 5 bits at a time until we
        /// have the whole number
        /// </summary>
        (long literalValue, int bitcount) GetLiteral()
        {
            bool lastGroup;
            List<int> bits = new();
            int literalbits = 0;

            do
            {
                lastGroup = GetNextBit() == 0;
                bits.AddRange(GetNextNBits(4));
                literalbits += 5;
            } while (!lastGroup);

            return (BitsToLong(bits), literalbits);
        }

        IEnumerable<BasePacket> SubsByPacketCount()
        {
            var packetCount = GetNBitsAsLong(11);

            for (long i = 1; i <= packetCount; i++)
                yield return NextPacket();
        }

        IEnumerable<BasePacket> SubsByBitCount()
        {
            var totalBits = GetNBitsAsLong(15);            

            long n = 0;
            while (n < totalBits)
            {
                var next = NextPacket();
                yield return next;
                n += next.Size();
            }
        }

        /// <summary>
        /// The next three methods get one or more bits from the stream and advance the
        /// stream by the same amount
        /// </summary>
        int GetNextBit() => GetNextNBits(1).First();
        long GetNBitsAsLong(int count) => BitsToLong(GetNextNBits(count));

        IEnumerable<int> GetNextNBits(int count)
        {
            var next = _stream.Take(count);
            _stream = _stream.Skip(count);
            return next;
        }        

        /// <summary>
        /// Convert a string of bits into a long, most-significant-bit first
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        static long BitsToLong(IEnumerable<int> bits)
        {
            long value = 0;
            long doubler = 1;

            foreach (var bit in bits.Reverse())
            {
                value += doubler * bit;
                doubler *= 2;
            }

            return value;
        }
    }

    /// <summary>
    /// Convert a hex string into a stream of 1s and 0s
    /// </summary>
    class BitStream
    {
        readonly string _hex;

        public BitStream(string hex)
        {
            _hex = hex;
        }

        public IEnumerable<int> Bits()
        {
            return _hex
                .ToCharArray()
                .SelectMany(HexToBits)
                .Select(c => c == '1' ? 1 : 0);
        }

        /// <summary>
        /// Copied right from the problem description, dear reader
        /// </summary>
        public string HexToBits(char hex) => hex switch
        {
            '0' => "0000",
            '1' => "0001",
            '2' => "0010",
            '3' => "0011",
            '4' => "0100",
            '5' => "0101",
            '6' => "0110",
            '7' => "0111",
            '8' => "1000",
            '9' => "1001",
            'A' => "1010",
            'B' => "1011",
            'C' => "1100",
            'D' => "1101",
            'E' => "1110",
            'F' => "1111",
             _  => throw new ArgumentNullException($"hex {hex}")
        };
    }

    public class Day16 : IAdventDay
    {
        readonly string _hex;

        public Day16(IEnumerable<string> input)
        {
            _hex = input.First();            
        }

        public long Part1() => Evaluate( p => p.VersionSum() );
        public long Part2() => Evaluate( p => p.Value()      );

        public long Evaluate(Func<BasePacket, long> valueGetter)
        {
            var packetParser = new PacketParser(_hex);
            var packet = packetParser.NextPacket();

            return valueGetter(packet);
        }

        /// <summary>
        /// Used only in the unit tests to inspect a parsed packet
        /// </summary>
        /// <returns></returns>
        internal BasePacket GetOutermostPacket()
        {
            var packetParser = new PacketParser(_hex);
            return packetParser.NextPacket();
        }
    }
}
