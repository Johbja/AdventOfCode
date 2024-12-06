using AdventOfCode.Infrastructure.Attributes;
using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using AdventOfCode.Infrastructure.Models._2024.Day06;
using System;
using System.Reflection;

namespace AdventOfCode.Years._2024.Days;

[DayInfo("Guard Gallivant", "Day 6")]
public class Day06 : ISolution
{
    private readonly string[] _input;

    public Day06(LoadInputService inputService)
    {
        _input = inputService.GetInputAsLines(nameof(Day06), 2024);
    }

    private int EvaluateCharacter(char value)
        => value == '^' ? 2 : (value == '#' ? 1 : 0);

    public void SolvePartOne()
    {
        var grid = _input.Select(row => row.Select(col => EvaluateCharacter(col)).ToArray()).ToArray();
        var guardPos = grid.SelectMany((row, y) => row.Select((value, x) => (x, y, value)))
                          .Where(x => x.value == 2)
                          .Select(pos => (pos.x, pos.y))
                          .Single();

        grid[guardPos.y][guardPos.x] = 0;

        int[] yMoveset = [-1, 0, 1, 0];
        int[] xMoveset = [0, 1, 0, -1];

        Direction currentDirection = Direction.Up;
        Dictionary<(int x, int y), int> visitedPositions = new()
        {
            { guardPos , 1}
        };

        while (guardPos.x > 0 && guardPos.x < grid[0].Length && guardPos.y > 0 && guardPos.y < grid.Length)
        {
            var (x, y) = (guardPos.x + xMoveset[(int)currentDirection], guardPos.y + yMoveset[(int)currentDirection]);

            if (x >= 0 && x < grid[0].Length && y >= 0 && y < grid.Length && grid[y][x] == 1)
            {
                currentDirection = (Direction)(((int)currentDirection + 1) % 4);
            }
            else
            {
                guardPos = (x, y);
            }

            if(x >= 0 && x < grid[0].Length && y >= 0 && y < grid.Length)
            {
                visitedPositions.TryAdd(guardPos, 0);
                visitedPositions[guardPos]++;
            }
        }

        Console.WriteLine(visitedPositions.Count);
    }

    public void SolvePartTwo()
    {

    }
}
