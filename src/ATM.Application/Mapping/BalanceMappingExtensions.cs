using ATM.Application.Contracts.Models;
using ATM.Domain.Accounts;

namespace ATM.Application.Mapping;

public static class BalanceMappingExtensions
{
    public static BalanceDto MapToDto(this Balance balance)
        => new BalanceDto(balance.Value);
}