using ATM.Application.Contracts.Models;
using ATM.Domain.Accounts;

namespace ATM.Application.Mapping;

public static class AccountMappingExtensions
{
    public static AccountDto MapToDto(this Account account, BalanceDto balanceDto)
        => new AccountDto(account.NumberAccount, balanceDto);
}