namespace ATM.Domain.Accounts;

public record WithdrawResultType
{
    public sealed record Success() : WithdrawResultType;

    public sealed record Failure(string TextError) : WithdrawResultType;
}