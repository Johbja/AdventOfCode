using AdventOfCode.Application;
using AdventOfCode.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdventOfCode.Api;
[ApiController]
public class ApiController(IApplicationOperationResolver operationResolver) : ControllerBase
{
    protected TOperation CreateOperation<TOperation>() 
        where TOperation : class, 
        IApplicationOperation
    {
        return operationResolver.ResolveOperation<TOperation>();
    }

    protected async Task<BaseResponse> CreateBaseResponse<TOutput>(ITraceableOutput<Task<TOutput>> traceableOutput)
    {
        var result = await traceableOutput.Output;
        traceableOutput.Stopwatch.Stop();
        var response = new BaseResponse(traceableOutput.TimeElapsed, result);

        return response;
    }

    public class BaseResponse(string timeElapsed, object result)
    {
        public object Result { get; } = result;
        public string TimeElapsed { get; } = timeElapsed;
    }
}
