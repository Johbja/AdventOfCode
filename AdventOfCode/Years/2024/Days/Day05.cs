using AdventOfCode.Infrastructure.Attributes;
using AdventOfCode.Infrastructure.Interfaces;
using AdventOfCode.Infrastructure.Services;
using AdventOfCode.Infrastructure.Models._2024.Day05;

namespace AdventOfCode.Years._2024.Days;

[DayInfo("Print Queue", "Day 5")]
public class Day05 : ISolution
{
    private readonly string _input;

    private Dictionary<int, IEnumerable<int>> _rules;
    private List<int[]> _updates;

    private Dictionary<Validity, List<int[]>> _sortedUpdates = new()
    {
        { Validity.Valid, new() },
        { Validity.NotValid, new() },
    };

    public Day05(LoadInputService inputService)
    {
        _input = inputService.GetInput(nameof(Day05), 2024);
        var input = _input.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);

        _rules = input[0]
            .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split("|", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
            .GroupBy(x => x[0])
            .ToDictionary(group => group.Key, group => group.Select(value => value[1]));

        _updates = input[1]
            .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
            .ToList();

        SortUpdates();
    }

    private void SortUpdates()
    {

        foreach (var update in _updates)
        {
            bool valid = true;
            for (var i = update.Length - 1; i >= 0; i--)
            {
                int currentPage = update[i];
                if (_rules.TryGetValue(currentPage, out var pageRules))
                {
                    var subUpdate = update.Take(i);

                    if (subUpdate.Any(page => pageRules.Contains(page)))
                    {
                        _sortedUpdates[Validity.NotValid].Add(update);
                        valid = false;
                        break;
                    }
                }
            }

            if (valid)
                _sortedUpdates[Validity.Valid].Add(update);
        }
    }

    public void SolvePartOne()
    {
        var result = _sortedUpdates[Validity.Valid]
            .Select(update => update[update.Length / 2])
            .Sum();

        Console.WriteLine(result);
    }

    public void SolvePartTwo()
    {
        var updatesToSort = _sortedUpdates[Validity.NotValid];

        for (int update = 0; update < updatesToSort.Count; update++)
        {
            for (var i = updatesToSort[update].Length - 1; i > 0; i--)
            {
                int currentPage = updatesToSort[update][i];

                if (_rules.TryGetValue(currentPage, out var pageRules))
                {
                    int index = i - 1;
                    int nextPage = updatesToSort[update][index];

                    while (index > 0 && pageRules.Contains(nextPage))
                    {
                        updatesToSort[update][index] = currentPage;
                        updatesToSort[update][index + 1] = nextPage;

                        index--;
                        nextPage = updatesToSort[update][index];
                    }
                }
            }
        }

        var result = updatesToSort.Select(x => x[x.Length / 2]).Sum();

        Console.WriteLine(result);

        //var result = updatesToSort.Select(update => update
        //        .Select(page => (value: page, weight: _rules.TryGetValue(page, out var pageRules) ? pageRules.Count() : 0))
        //        .OrderByDescending(page => page.weight)
        //        .ToArray())
        //        .Select(update => update[update.Length/2])
        //        .Sum(x => x.value);

        //Console.WriteLine(result);
    }
}
