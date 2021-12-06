namespace AdventOfCode.CSharp
{
    public class Day02 : IAdventDay
    {
        // the day's input is parsed in the constructor and kept around
        readonly IEnumerable<(string command, int amount)> _parsed;

        public Day02(IEnumerable<string> input)
        {
            _parsed = Parsing.AllStringWithNumber(input);
        }

        public long Part1()
        {
            return PilotSub(1);
        }

        public long Part2()
        {
            return PilotSub(2);
        }


        /* Day-specific logic */

        int PilotSub(int part)
        {
            int horiz = 0;
            int depth = 0;
            int aim = 0;

            foreach ((string command, int amount) in _parsed)
            {
                if (part == 1)
                {
                    switch (command)
                    {
                        case "forward": horiz += amount; break;
                        case "down":    depth += amount; break;
                        case "up":      depth -= amount; break;
                    }
                }
                
                if (part == 2)
                {
                    switch (command)
                    {
                        case "forward":
                            horiz += amount;
                            depth += aim * amount;
                            break;

                        case "down": aim += amount; break;
                        case "up":   aim -= amount; break;
                    }
                }
            }

            return horiz * depth;
        }
    }
}
