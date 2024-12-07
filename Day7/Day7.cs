namespace AdventOfCode2024.Day7;

public class Day7() : SolutionBase("Day7")
{
  public override string Part1(bool isExample = false)
  {
    var lines = (isExample ? ExampleLines : InputLines).Where(line => !string.IsNullOrEmpty(line)).ToList();

    var calibrations = lines.Select(line =>
    {
      var parts = line.Split(":");
      var testValue = long.Parse(parts[0]);
      var values = parts[1].Split(" ").Where(v => !string.IsNullOrEmpty(v)).Select(long.Parse).ToList();
      return new Calibration(testValue, values);
    }).ToList();


    return calibrations.Where(CanBeCombined).Sum(c => c.TestValue).ToString();
  }
  
  public override string Part2(bool isExample = false)
  {
    var lines = (isExample ? ExampleLines : InputLines).Where(line => !string.IsNullOrEmpty(line)).ToList();

    var calibrations = lines.Select(line =>
    {
      var parts = line.Split(":");
      var testValue = long.Parse(parts[0]);
      var values = parts[1].Split(" ").Where(v => !string.IsNullOrEmpty(v)).Select(long.Parse).ToList();
      return new Calibration(testValue, values);
    }).ToList();


    return calibrations.Where(CanBeCombinedPart2).Sum(c => c.TestValue).ToString();
  }

  private record Calibration(long TestValue, List<long> Values);

  private static bool CanBeCombined(Calibration calibration)
  {
    var possibleValues = new List<long>{calibration.Values[0]};

    for (var i = 1; i < calibration.Values.Count; i++)
    {
      possibleValues = possibleValues.SelectMany(v => new List<long>{v + calibration.Values[i], v * calibration.Values[i]}).ToList();
      if (possibleValues.All(v => v > calibration.TestValue))
      {
        return false;
      }
      possibleValues = possibleValues.Distinct().Where(v => v <= calibration.TestValue).ToList();
    }

    return possibleValues.Contains(calibration.TestValue);
  }
  
  private static bool CanBeCombinedPart2(Calibration calibration)
  {
    var possibleValues = new List<long>{calibration.Values[0]};

    for (var i = 1; i < calibration.Values.Count; i++)
    {
      possibleValues = possibleValues.SelectMany(v => new List<long>
      {
        v + calibration.Values[i],
        v * calibration.Values[i],
        long.Parse($"{v}{calibration.Values[i]}"),
      }).ToList();
      if (possibleValues.All(v => v > calibration.TestValue))
      {
        return false;
      }
      possibleValues = possibleValues.Distinct().Where(v => v <= calibration.TestValue).ToList();
    }

    return possibleValues.Contains(calibration.TestValue);
  }

}