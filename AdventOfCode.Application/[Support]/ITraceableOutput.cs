using System.Diagnostics;

namespace AdventOfCode.Application;

public interface ITraceableOutput<TOutput>
{
    public Stopwatch Stopwatch { get; }
    TOutput Output { get; }
    public string TimeElapsed { get; }
}

public class TraceableOutput<TOutput>(
    Stopwatch stopwatch, 
    TOutput output)
    : ITraceableOutput<TOutput>
{
    public Stopwatch Stopwatch { get; } = stopwatch;
    public TOutput Output { get; } = output;
    public string TimeElapsed => $"Time elapsed: {Stopwatch.Elapsed.TotalMilliseconds}ms";
}
