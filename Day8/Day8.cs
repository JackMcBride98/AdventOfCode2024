namespace AdventOfCode2024.Day8;

public class Day8() : SolutionBase("Day8")
{
    public override string Part1(bool isExample = false)
    {
        var lines = (isExample ? ExampleLines : InputLines).Where(line => !string.IsNullOrEmpty(line)).ToList();

        var antinodes = new List<Antinode>();

        foreach (var (line, y) in lines.Select((line, i)  => (line, i)))
        {
            foreach (var (character, x) in line.Select((character, j)  => (character, j))){
                if (character != '.')
                {
                    antinodes.Add(new (character, new Location(x, y)));
                }
            }
        }

        var gridWidth = lines[0].Length;
        var gridHeight = lines.Count;
        
        var antinodesByFrequency = antinodes.GroupBy(antinode => antinode.Frequency);

        var antinodeLocations = new List<Location>();
        
        foreach (var antinodeGroup in antinodesByFrequency)
        {
            antinodeLocations.AddRange( CalculateValidAntinodeLocations(antinodeGroup.ToList(), antinodeGroup.Key, gridWidth, gridHeight));
        }


        return antinodeLocations.Distinct().Count().ToString();
    }
    
    private record Location(int X, int Y);

    private record Antinode(char Frequency, Location Location);

    private static List<Location> CalculateValidAntinodeLocations(List<Antinode> antinodes, char frequency, int gridWidth, int gridHeight)
    {
        var outputLocations = new List<Location>();
        
        outputLocations.AddRange(antinodes.Select(antinode => antinode.Location));

        for (var i = 0; i < antinodes.Count; i++)
        {
            var startAntinode = antinodes[i];
            for (var j = 0; j < antinodes.Count; j++)
            {
                if (i == j) continue;
                var endAntinode = antinodes[j];
                var xDifference = endAntinode.Location.X - startAntinode.Location.X;
                var yDifference = endAntinode.Location.Y - startAntinode.Location.Y;
                var k = 1;
                while (true)
                {
                    var possibleNewLocation = new Location(endAntinode.Location.X + k * xDifference, endAntinode.Location.Y + k * yDifference);
                    if (
                        possibleNewLocation.X < gridWidth && possibleNewLocation.X >= 0 && 
                        possibleNewLocation.Y < gridHeight && possibleNewLocation.Y >= 0
                    )
                    {
                        outputLocations.Add(possibleNewLocation);
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        return outputLocations;
    }
}