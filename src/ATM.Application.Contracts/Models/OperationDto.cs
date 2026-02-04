namespace ATM.Application.Contracts.Models;

public sealed record OperationDto(long NumberAccount, string Type, decimal Balance, DateTime Time);