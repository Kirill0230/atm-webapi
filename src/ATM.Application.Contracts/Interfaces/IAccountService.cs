using ATM.Application.Contracts.Operations;

namespace ATM.Application.Contracts.Interfaces;

public interface IAccountService
{
    CreateAccount.Response CreateUserAccount(CreateAccount.Request request);

    ViewingBalance.Response ViewingBalance(ViewingBalance.Request request);

    Deposit.Response Deposit(Deposit.Request request);

    Withdraw.Response Withdraw(Withdraw.Request request);

    GetHistoryOperation.Response GetHistoryOperation(GetHistoryOperation.Request request);
}