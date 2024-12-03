namespace AdventOfCode2024.Day2;

public class Day2() : SolutionBase("Day2")
{
    public override string Part1(bool isExample = false)
    {
        var lines = isExample ? ExampleLines : InputLines;
        var numbers = lines.Where(line => !string.IsNullOrEmpty(line)).Select(line => line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)).Select(v => v.Select(int.Parse).ToList()).ToList();

        var reportSafeList = numbers.Select(IsReportSafe).ToList();

        return reportSafeList.Count(v => v).ToString();
    }

    private static bool IsReportSafe(List<int> numbers)
    {
        var isAllIncreasing = numbers.Take(numbers.Count - 1).Select((n, i) => numbers[i + 1] > n).All(v => v);
        var isAllDecreasing = numbers.Take(numbers.Count - 1).Select((n, i) => numbers[i + 1] < n).All(v => v);

        var areAllDifferencesBetween1And3 = numbers.Take(numbers.Count - 1).Select((n, i) => Math.Abs(numbers[i + 1] - n)).All(v => v >= 1 && v <= 3);

        return (isAllIncreasing || isAllDecreasing) && areAllDifferencesBetween1And3;
    }
}
