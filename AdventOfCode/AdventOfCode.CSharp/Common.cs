namespace AdventOfCode
{
    public static class Common
    {
        public static List<string> GetInput(int day)
        {
            string filename = $"Day{day}.txt";

            // Try these folders for the input file
            List<string> folders = new()
            {
                @"",                     // if running in Docker
                @"..\..\..\..\Inputs\",  // if running in Visual Studio
            };

            foreach (var folder in folders)
            {
                var file = $"{folder}{filename}";

                if (File.Exists(file))
                    return File.ReadAllLines(file).ToList();
            }

            throw new Exception($"Couldn't locate input file {filename} for day {day}");
        }

        public static List<string> GetSampleInput(string filename)
        {
            string file = $@"..\..\..\..\SampleInputs\Day{filename}.txt";

            return File.ReadAllLines(file).ToList();
        }

        /// <summary>
        /// Sorts a string in character order. Found at https://www.dotnetperls.com/alphabetize-string
        /// </summary>
        public static string Alphabetize(string s)
        {
            // Convert to char array.
            char[] a = s.ToCharArray();

            // Sort letters.
            Array.Sort(a);

            // Return modified string.
            return new string(a);
        }
    }
}
