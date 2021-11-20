namespace AdventOfCode
{
    public static class Common
    {
        public static List<string> GetInput(int day)
        {
            string filename = $"Input{day}.txt";

            // Try these folders for the input file
            List<string> folders = new()
            {
                @"",
                @"..\..\..\",
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
