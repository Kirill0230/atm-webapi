namespace ATM.Domain.Accounts;

public class BankAccount
{
    public Balance Balance { get; private set; }

    public BankAccount(Balance balance)
    {
        Balance = balance;
    }

    public DepositResultType Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            return new DepositResultType.Failure("Amount must be positive");
        }

        Balance = Balance.Increased(amount);
        return new DepositResultType.Success();
    }

    public WithdrawResultType Withdraw(decimal amount)
    {
        if (amount < 0)
        {
            return new WithdrawResultType.Failure("Amount must be positive");
        }

        if (!Balance.CanWithdraw(amount))
        {
            return new WithdrawResultType.Failure("Balance below withdraw amount");
        }

        Balance = Balance.Decreased(amount);

        return new WithdrawResultType.Success();
    }
}