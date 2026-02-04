namespace ATM.Domain.Accounts;

public class Account
{
    public long NumberAccount { get; }

    private readonly long _pincode;

    private readonly BankAccount _bankAccount;

    public Account(long numberAccount, long pincode, Balance startBalance)
    {
        NumberAccount = numberAccount;
        _pincode = pincode;
        _bankAccount = new BankAccount(startBalance);
    }

    public bool IsCorrectPin(long pincode) => pincode == _pincode;

    public DepositResultType Deposit(decimal amount) =>
        _bankAccount.Deposit(amount);

    public WithdrawResultType Withdraw(decimal amount) =>
        _bankAccount.Withdraw(amount);

    public Balance ViewingBalance() => _bankAccount.Balance;
}