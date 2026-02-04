using ATM.Application.Abstractions.Queries;
using ATM.Domain.Operations;

namespace ATM.Application.Abstractions.Repositories;

public interface IOperationRepository
{
    Operation AddOperation(Operation operation);

    IEnumerable<Operation> Query(OperationQuery query);
}