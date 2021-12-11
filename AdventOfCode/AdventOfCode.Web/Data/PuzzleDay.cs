using System.ComponentModel.DataAnnotations;

namespace AdventOfCode.Web.Data
{
    public class PuzzleDay
    {
        [Required(ErrorMessage = "Input is required")]
        public string Input { get; set; }

        public string Part1 { get; set; }
        public string Part2 { get; set; }

        public PuzzleDay()
        {
            Input = "paste input here";
            Part1 = string.Empty;
            Part2 = string.Empty;
        }
    }
}
