namespace AdventOfCode.Application.Interfaces;

public interface IApplicationOperation
{
    void Initialize(
        IApplicationOperationManager operationManager,
        IApplicationOperationResolver operationResolver);
}
