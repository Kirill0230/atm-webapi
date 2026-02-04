using ATM.Application.Contracts.Models;
using ATM.Domain.Operations;

namespace ATM.Application.Mapping;

public static class OperationMappingExtensions
{
    public static OperationDto MapToDto(this Operation operation)
        => new OperationDto(operation.NumberAccount, operation.Type.ToString(), operation.Balance, operation.Time);
}