using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using static System.String;

namespace AdventOfCode2024.Day4;

public class Day4() : SolutionBase("Day4")
{
    public override string Part1(bool isExample = false)
    {
        var lines = (isExample ? ExampleLines : InputLines).Where(line => !IsNullOrEmpty(line)).ToList();
        var totalOccurrencesOfXmas = 0;
        //forwards check - example has 3
        var joinedLines = Join("\n", lines);
        totalOccurrencesOfXmas += GetCountOfOccurrencesOfXmasInString(joinedLines);
        //backwards check - example has 2
        var joinedReversedLines = Join("\n", lines.Select(Strings.StrReverse));
        totalOccurrencesOfXmas += GetCountOfOccurrencesOfXmasInString(joinedReversedLines);

        // right and down diagonal check - example has 1
        var rightAndDownDiagonalLines = GetAllDiagonalLines(lines, UpDownDirection.Down, LeftRightDirection.Right);
        totalOccurrencesOfXmas += GetCountOfOccurrencesOfXmasInString(rightAndDownDiagonalLines);


        //right and up diagonal check - example has 4
        var rightAndUpDiagonalLines = GetAllDiagonalLines(lines, UpDownDirection.Up, LeftRightDirection.Right);
        totalOccurrencesOfXmas += GetCountOfOccurrencesOfXmasInString(rightAndUpDiagonalLines);

        //left and down diagonal check - example has 1
        var leftAndDownDiagonalLines = GetAllDiagonalLines(lines, UpDownDirection.Down, LeftRightDirection.Left);
        totalOccurrencesOfXmas += GetCountOfOccurrencesOfXmasInString(leftAndDownDiagonalLines);

        //left and up diagonal check - example has 4
        var leftAndUpDiagonalLines = GetAllDiagonalLines(lines, UpDownDirection.Up, LeftRightDirection.Left);
        totalOccurrencesOfXmas += GetCountOfOccurrencesOfXmasInString(leftAndUpDiagonalLines);

        //vertical down check - example has 1
        for (var i = 0; i < lines[0].Length; i++)
        {
            var verticalLine = "";
            for (var j = 0; j < lines.Count; j++)
            {
                verticalLine += lines[j][i];
            }
            totalOccurrencesOfXmas += GetCountOfOccurrencesOfXmasInString(verticalLine);
        }

        //vertical up check - example has 2
        for (var i = 0; i < lines[0].Length; i++)
        {
            var verticalLine = "";
            for (var j = lines.Count - 1; j >= 0; j--)
            {
                verticalLine += lines[j][i];
            }
            totalOccurrencesOfXmas += GetCountOfOccurrencesOfXmasInString(verticalLine);
        }

        return totalOccurrencesOfXmas.ToString();
    }

    public override string Part2(bool isExample = false)
    {
        var lines = (isExample ? ExampleLines : InputLines).Where(line => !IsNullOrEmpty(line)).ToList();

        var aPoints = new List<Point>();

        for (var i = 1; i < lines.Count - 1; i++)
        {
            for (var j = 1; j < lines[i].Length - 1; j++)
            {
                if (lines[i][j] == 'A')
                {
                    aPoints.Add(new Point(j, i));
                }
            }
        }

        return aPoints.Count(aPoint => IsXmas(aPoint, lines)).ToString();
    }

    private static bool IsXmas(Point aPoint, List<string> lines)
    {
        return ((lines[aPoint.Y - 1][aPoint.X - 1] == 'S' && lines[aPoint.Y + 1][aPoint.X + 1] == 'M') || (lines[aPoint.Y - 1][aPoint.X - 1] == 'M' && lines[aPoint.Y + 1][aPoint.X + 1] == 'S'))
               && ((lines[aPoint.Y - 1][aPoint.X + 1] == 'S' && lines[aPoint.Y + 1][aPoint.X - 1] == 'M') || (lines[aPoint.Y - 1][aPoint.X + 1] == 'M' && lines[aPoint.Y + 1][aPoint.X - 1] == 'S'));
    }

    private static int GetCountOfOccurrencesOfXmasInString(string input)
    {
        var regex = new Regex(@"XMAS");

       return regex.Matches(input).Count;
    }

    private record Point(int X, int Y);

    private enum UpDownDirection
    {
        Up,
        Down,
    }

    private enum LeftRightDirection
    {
        Left,
        Right,
    }

    private static string GetAllDiagonalLines(List<string> input, UpDownDirection upDownDirection, LeftRightDirection leftRightDirection)
    {
        var startingPoints = Enumerable
            .Range(0, input.Count).Select(i => new Point(leftRightDirection == LeftRightDirection.Right ? 0 : input[0].Length - 1, i)).ToList()
            .Concat(Enumerable.Range(0, input[0].Length).Select(i => new Point(i, upDownDirection == UpDownDirection.Down ? 0 : input.Count - 1)).ToList()).Distinct();

        var diagonalLines = "";

        foreach (var point in startingPoints)
        {
            var i = leftRightDirection == LeftRightDirection.Left ? -1 : 1;
            var j = upDownDirection == UpDownDirection.Up ? -1 : 1;
            var diagonalLine = "";
            diagonalLine += input[point.Y][point.X];
            while (point.Y + j < input.Count && point.Y + j >= 0 && point.X + i < input[point.Y + j].Length && point.X + i >= 0)
            {
                diagonalLine += input[point.Y + j][point.X + i];
                if (upDownDirection == UpDownDirection.Up)
                {
                    j--;
                }
                else
                {
                    j++;
                }
                if (leftRightDirection == LeftRightDirection.Left)
                {
                    i--;
                }
                else
                {
                    i++;
                }
            }
            diagonalLines += diagonalLine + "\n";
        }

        return diagonalLines;
    }
}
