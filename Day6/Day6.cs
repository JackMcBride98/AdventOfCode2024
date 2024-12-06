using System.Diagnostics;
using System.Text.Json;

namespace AdventOfCode2024.Day6;

public class Day6() : SolutionBase("Day6")
{
    public override string Part1(bool isExample = false)
    {
        var lines = (isExample ? ExampleLines : InputLines).Where(line => !string.IsNullOrEmpty(line)).ToList();

        var grid = lines.Select(line => line.ToCharArray().ToList()).ToList();

        var y = grid.FindIndex(line => line.Contains('^'));
        var x = grid[y].IndexOf('^');

        var direction = Direction.Up;

        grid[y][x] = 'X';
        while (true)
        {
            (x, y, direction) = MoveGuard(grid, x, y, direction);
            if (direction == Direction.OffMap)
            {
                break;
            }
            grid[y][x] = 'X';
        }

        return grid.Sum(line => line.Count(c => c == 'X')).ToString();
    }

    public override string Part2(bool isExample = false)
    {
        var lines = (isExample ? ExampleLines : InputLines).Where(line => !string.IsNullOrEmpty(line)).ToList();

        var grid = lines.Select(line => line.ToCharArray().ToList()).ToList();

        var startingY = grid.FindIndex(line => line.Contains('^'));
        var startingX = grid[startingY].IndexOf('^');

        var startingDirection = Direction.Up;

        grid[startingY][startingX] = '.';


        var firstX = startingX;
        var firstY = startingY;
        var firstDirection = startingDirection;

        var firstGridCopy = JsonSerializer.Deserialize<List<List<char>>>(JsonSerializer.Serialize(grid));
        var firstRoutePositions = new List<Position>{ new (firstX, firstY, firstDirection)};

        while (true)
        {
            (firstX, firstY, firstDirection) = MoveGuard(firstGridCopy!, firstX, firstY, firstDirection);
            if (firstDirection == Direction.OffMap)
            {
                break;
            }

            firstRoutePositions.Add(new Position(firstX, firstY, firstDirection));
        }



        var infiniteLoopCount = 0;

        for (var i = 0; i < grid.Count; i++)
        {
            for (var j = 0; j < grid[i].Count; j++)
            {
                Console.WriteLine($"Checking y = {i}, x = {j} out of {grid.Count}x{grid[i].Count}");
                if (!firstRoutePositions.Any(pos => pos.X == j && pos.Y == i))
                {
                    Console.WriteLine("Skipping as isn't on the first route");
                    continue;
                }
                var gridCopy = JsonSerializer.Deserialize<List<List<char>>>(JsonSerializer.Serialize(grid));
                var x = startingX;
                var y = startingY;
                var direction = startingDirection;

                var positions = new List<Position>{ new (x, y, direction)};

                gridCopy![i][j] = '#';

                while (true)
                {
                    (x, y, direction) = MoveGuard(gridCopy, x, y, direction);
                    if (direction == Direction.OffMap)
                    {
                        break;
                    }
                    if (positions.Any(pos => pos.X == x && pos.Y == y && pos.Direction == direction))
                    {
                        infiniteLoopCount++;
                        break;
                    }

                    positions.Add(new Position(x, y, direction));
                }


            }
        }

        return infiniteLoopCount.ToString();
    }

    private record Position(int X, int Y, Direction Direction);

    private enum Direction
    {
        Up,
        Right,
        Down,
        Left,
        OffMap,
    }

    private static (int x, int y, Direction direction) MoveGuard(List<List<char>> grid, int x, int y, Direction direction)
    {
        var possibleX = x;
        var possibleY = y;

        switch (direction)
        {
            case Direction.Up:
                possibleY--;
                break;
            case Direction.Right:
                possibleX++;
                break;
            case Direction.Down:
                possibleY++;
                break;
            case Direction.Left:
                possibleX--;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        if (possibleX < 0 || possibleX >= grid[0].Count || possibleY < 0 || possibleY >= grid.Count)
        {
            return (possibleX, possibleY, Direction.OffMap);
        }

        if (grid[possibleY][possibleX] is '.' or '^' or 'X')
        {
            return (possibleX, possibleY, direction);
        }

        if (grid[possibleY][possibleX] == '#')
        {
            var newDirection =
                direction switch
                {
                    Direction.Up => Direction.Right,
                    Direction.Right => Direction.Down,
                    Direction.Down => Direction.Left,
                    Direction.Left => Direction.Up,
                    _ => throw new ArgumentOutOfRangeException()
                };

            return MoveGuard(grid, x, y, newDirection);
        }

        throw new UnreachableException("This should never happen");
    }
}
