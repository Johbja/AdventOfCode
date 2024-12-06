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
        _input  = inputService.GetInputAsLines(nameof(Day06), 2024);
    }

    private int EvaluateCharacter(char value)
        => value == '^' ? 2 : (value == '#' ? 1 : 0);

    public void SolvePartOne()
    {
        var map = _input.Select(row => row.Select(col => EvaluateCharacter(col)).ToArray()).ToArray();
        var guardPos = map.SelectMany((row, y) => row.Select((value, x) => (x, y, value)))
                          .Where(x => x.value == 2)
                          .Select(pos => (pos.x, pos.y))
                          .Single();

        map[guardPos.y][guardPos.x] = 0;

        Direction currentDirection = Direction.Up;
        Dictionary<(int x, int y), int> visitedPositions = new() 
        { 
            { guardPos , 1} 
        };

        while(guardPos.x > 0 && guardPos.x < map[0].Length && guardPos.y > 0 && guardPos.y < map.Length)
        {
            if(currentDirection == Direction.Up)
            {
                if (guardPos.y - 1 < 0)
                {
                    guardPos = (guardPos.x ,guardPos.y - 1);
                    break;
                }

                if (map[guardPos.y - 1][guardPos.x] == 0)
                    guardPos = (guardPos.x, guardPos.y - 1);
                else
                    currentDirection = (Direction)(((int)currentDirection + 1) % 4);
            }

            if (currentDirection == Direction.Down)
            {
                if (guardPos.y + 1 >= map.Length)
                {
                    guardPos = (guardPos.x, guardPos.y + 1);
                    break;
                }

                if (map[guardPos.y + 1][guardPos.x] == 0)
                    guardPos = (guardPos.x, guardPos.y + 1);
                else
                    currentDirection = (Direction)(((int)currentDirection + 1) % 4);
            }


            if (currentDirection == Direction.Right)
            {
                if (guardPos.x + 1 >= map[0].Length)
                {
                    guardPos = (guardPos.x + 1, guardPos.y);
                    break;
                }

                if (map[guardPos.y][guardPos.x + 1] == 0)
                    guardPos = (guardPos.x + 1, guardPos.y);
                else
                    currentDirection = (Direction)(((int)currentDirection + 1) % 4);
            }


            if (currentDirection == Direction.Left)
            {
                if (guardPos.x - 1 >= map[0].Length)
                {
                    guardPos = (guardPos.x - 1, guardPos.y);
                    break;
                }

                if (map[guardPos.y][guardPos.x - 1] == 0)
                    guardPos = (guardPos.x - 1, guardPos.y);
                else
                    currentDirection = (Direction)(((int)currentDirection + 1) % 4);
            }

            if (!visitedPositions.ContainsKey(guardPos))
                visitedPositions.Add(guardPos, 0);

            visitedPositions[guardPos]++;
        }

        int result = visitedPositions.Count;
        Console.WriteLine(result);
    }

    public void SolvePartTwo()
    {

    }
}
