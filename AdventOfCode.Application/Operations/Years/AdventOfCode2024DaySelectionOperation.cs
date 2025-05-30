namespace AdventOfCode.Application.Operations.Years;

public class AdventOfCode2024DaySelectionOperation 
    : ApplicationOperation<
        AdventOfCode2024DaySelectionOperation.Input, 
        AdventOfCode2024DaySelectionOperation.Output>
{
    public class Input(int day, string input)
    {
        public int Day { get; } = day;
        public string TextInput { get; } = input;
    }

    public class Output(string resultPartOne, string resultPartTwo)
    {
        public string ResultPartOne { get; } = resultPartOne;
        public string ResultPartTwo { get; } = resultPartTwo;
    }

    protected override async Task<Output> ExecuteApplicationLogic(Input input)
    {
        return new Output($"{input.Day}_{input.TextInput}_1",$"{input.Day}_{input.TextInput}_2");
    }
}
