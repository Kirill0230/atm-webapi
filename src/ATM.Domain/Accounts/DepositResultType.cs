namespace ATM.Domain.Accounts;

public abstract record DepositResultType
{
    public sealed record Success() : DepositResultType;

    public sealed record Failure(string TextError) : DepositResultType;
}