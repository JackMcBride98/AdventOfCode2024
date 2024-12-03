namespace AdventOfCode2024.Day1;

public class Day1() : SolutionBase("Day1")
{
    public override string Part1(bool isExample = false)
    {
        var lines = isExample ? ExampleLines : InputLines;
        var numbers = lines.Where(line => !string.IsNullOrEmpty(line)).Select(line => line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)).Select(v => v.Select(int.Parse).ToList()).ToList();

        var leftNumbers = numbers.Select(row => row.First()).ToList();
        var rightNumbers = numbers.Select(row => row.Last()).ToList();

        leftNumbers.Sort();
        rightNumbers.Sort();

        var totalDifference = leftNumbers.Select((t, i) => Math.Abs(t - rightNumbers[i])).Sum();

        return totalDifference.ToString();
    }

    public override string Part2(bool isExample = false)
    {
        var lines = isExample ? ExampleLines : InputLines;
        var numbers = lines.Where(line => !string.IsNullOrEmpty(line)).Select(line => line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)).Select(v => v.Select(int.Parse).ToList()).ToList();

        var leftNumbers = numbers.Select(row => row.First()).ToList();
        var rightNumbers = numbers.Select(row => row.Last()).ToList();

        var totalSimilarityScore = 0;

        for (var i  = 0; i < leftNumbers.Count; i++)
        {
            var leftNumber = leftNumbers[i];

            var countInRightNumbers = rightNumbers.FindAll(rightNumber => rightNumber == leftNumber).Count;

            totalSimilarityScore += leftNumber * countInRightNumbers;
        }

        return totalSimilarityScore.ToString();
    }
}
