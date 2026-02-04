namespace ATM.Domain.Accounts;

public class Balance
{
    public decimal Value { get; }

    public Balance(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Balance cannot be negative.");
        }

        Value = value;
    }

    public Balance Increased(decimal amount)
    {
        return new Balance(Value + amount);
    }

    public Balance Decreased(decimal amount)
    {
        return new Balance(Value - amount);
    }

    public bool CanWithdraw(decimal amount)
    {
        return Value >= amount;
    }
}