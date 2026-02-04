using ATM.Application.Abstractions.Queries;
using ATM.Application.Abstractions.Repositories;
using ATM.Domain.Operations;

namespace ATM.Infrastructure.Repositories;

public class OperationInMemoryRepository : IOperationRepository
{
    private readonly List<Operation> _values = [];

    public Operation AddOperation(Operation operation)
    {
        _values.Add(operation);
        return operation;
    }

    public IEnumerable<Operation> Query(OperationQuery query)
    {
        return _values.Where(x => query.AccountNumber == x.NumberAccount);
    }
}