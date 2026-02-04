namespace ATM.Application.Contracts.Models;

public sealed record AccountDto(long AccountNumber, BalanceDto Balance);