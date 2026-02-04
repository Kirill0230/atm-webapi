namespace ATM.Domain.Operations;

public class Operation
{
    public long NumberAccount { get; }

    public TypeOperation Type { get; }

    public decimal Balance { get; }

    public DateTime Time { get; }

    public Operation(long numberAccount, decimal balance, TypeOperation type, DateTime time)
    {
        NumberAccount = numberAccount;
        Type = type;
        Balance = balance;
        Time = time;
    }
}