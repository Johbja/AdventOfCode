namespace AdventOfCode.Application.Interfaces;

public class ApplicationOperationManager : IApplicationOperationManager
{
    public ITraceableOutput<Task<TOutput>> HandleExecute<TOutput, TInput>(
        Func<TInput, ITraceableOutput<Task<TOutput>>> operationLogic, 
        TInput input)
        where TOutput : class
        where TInput : class
    => operationLogic.Invoke(input);
}
