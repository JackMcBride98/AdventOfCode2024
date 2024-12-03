namespace AdventOfCode2024;

public class SolutionBase
{
    protected string[] ExampleLines { get; set; }
    protected string[] InputLines { get; set; }

    public SolutionBase(string folderPath)
    {
        ExampleLines = GetExampleLines(folderPath).Result;
        InputLines = GetInputLines(folderPath).Result;
    }

    private async Task<string[]> GetExampleLines(string folderPath)
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", folderPath, "example.txt");

        // return [];
        return await File.ReadAllLinesAsync(filePath);
    }

    private async Task<string[]> GetInputLines(string folderPath)
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", folderPath, "input.txt");

        return await File.ReadAllLinesAsync(filePath);
    }


    public virtual string Part1(bool isExample = false) { return "123"; }

    public virtual string Part2(bool isExample = false) { return "456"; }
}
