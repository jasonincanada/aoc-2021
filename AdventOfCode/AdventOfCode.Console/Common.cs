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
    }
}
