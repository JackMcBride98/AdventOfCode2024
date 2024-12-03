using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day3;

public class Day3() : SolutionBase("Day3")
{
    public override string Part1(bool isExample = false)
    {
        var lines = isExample ? ExampleLines : InputLines;

        var mulRegex = new Regex(@"mul\(\d+,\d+\)");
        var numRegex = new Regex(@"\d+");

        var mulMatches = lines.Where(line => !string.IsNullOrEmpty(line)).Select(line => mulRegex.Matches(line));

        var total = 0;

        foreach (var matches in mulMatches)
        {
            foreach (var match in matches)
            {
                var numbers = numRegex.Matches(match.ToString()!).ToList().Select(m => int.Parse( m.Value)).ToList();
                total += numbers[0] * numbers[1];
            }
        }
        return total.ToString();
    }

    public override string Part2(bool isExample = false)
    {
        return "Part 2";
    }
}
