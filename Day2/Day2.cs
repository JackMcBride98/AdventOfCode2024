namespace AdventOfCode2024.Day2;

public class Day2() : SolutionBase("Day2")
{
    public override string Part1(bool isExample = false)
    {
        var lines = isExample ? ExampleLines : InputLines;
        var numbers = lines.Where(line => !string.IsNullOrEmpty(line)).Select(line => line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)).Select(v => v.Select(int.Parse).ToList()).ToList();

        var reportSafeList = numbers.Select(IsReportSafePart1).ToList();

        return reportSafeList.Count(v => v).ToString();
    }

    public override string Part2(bool isExample = false)
    {
        var lines = isExample ? ExampleLines : InputLines;
        var numbers = lines.Where(line => !string.IsNullOrEmpty(line)).Select(line => line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)).Select(v => v.Select(int.Parse).ToList()).ToList();

        var reportSafeList = numbers.Select(IsReportSafePart2).ToList();

        return reportSafeList.Count(v => v).ToString();
    }

    private static bool IsReportSafePart1(List<int> numbers)
    {
        return (IsAllIncreasing(numbers) || IsAllDecreasing(numbers)) && AreAllDifferencesBetweenOneAndThree(numbers);
    }

    private static bool IsReportSafePart2(List<int> numbers)
    {
        if (IsReportSafePart1(numbers))
        {
            return true;
        }

        for (var i = 0; i < numbers.Count; i++)
        {
            var numbersWithoutIndex = numbers.Where((_, index) => index != i).ToList();

            if (IsReportSafePart1(numbersWithoutIndex))
            {
                return true;
            }
        }

        return false;
    }

    private static bool AreAllDifferencesBetweenOneAndThree(List<int> numbers)
    {
        return numbers.Take(numbers.Count - 1).Select((n, i) => Math.Abs(numbers[i + 1] - n)).All(v => v >= 1 && v <= 3);
    }

    private static bool IsAllIncreasing(List<int> numbers)
    {
        return numbers.Take(numbers.Count - 1).Select((n, i) => numbers[i + 1] > n).All(v => v);
    }

    private static bool IsAllDecreasing(List<int> numbers)
    {
        return numbers.Take(numbers.Count - 1).Select((n, i) => numbers[i + 1] < n).All(v => v);
    }
}
