using AdventOfCode.Application.Interfaces;
using AdventOfCode.Application.Operations.Years;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventOfCode.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdventOfCode2024Controller(IApplicationOperationResolver operationResolver) 
    : ApiController(operationResolver)
{
    [HttpPost("v1/solve-with-payload-for/{day}")]
    public async Task<IActionResult> SolveForDay(
        [FromRoute] int day,
        [FromBody] string payload)
    {
        var input = new AdventOfCode2024DaySelectionOperation.Input(day, payload);
        var traceableOutput = CreateOperation<AdventOfCode2024DaySelectionOperation>().Execute(input);
        var result = await CreateBaseResponse(traceableOutput);
        return Ok(result);
    }
}
