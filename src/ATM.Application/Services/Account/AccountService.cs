using ATM.Application.Abstractions;
using ATM.Application.Abstractions.Queries;
using ATM.Application.Contracts.Interfaces;
using ATM.Application.Contracts.Operations;
using ATM.Application.Mapping;
using ATM.Domain.Accounts;
using ATM.Domain.Operations;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ATM.Tests")]

namespace ATM.Application.Services.Account;

internal class AccountService : IAccountService
{
    private readonly IPersistenceContext _persistenceContext;

    public AccountService(IPersistenceContext persistenceContext)
    {
        _persistenceContext = persistenceContext;
    }

    public CreateAccount.Response CreateUserAccount(CreateAccount.Request request)
    {
        if (_persistenceContext.AdminSessionRepository.CheckId(request.SessionId) is false)
        {
            return new CreateAccount.Response.Unauthorized("Session not admin");
        }

        if (_persistenceContext.AccountRepository.Find(request.AccountNumber) != null)
        {
            return new CreateAccount.Response.BadRequest("Account already exists");
        }

        var account = new Domain.Accounts.Account(request.AccountNumber, request.Pincode, new Balance(0));
        _persistenceContext.AccountRepository.Add(account);

        var operation = new Operation(request.AccountNumber, account.ViewingBalance().Value, TypeOperation.CreateAccount, DateTime.Now);
        _persistenceContext.OperationRepository.AddOperation(operation);

        Balance startBalance = account.ViewingBalance();
        return new CreateAccount.Response.Success(account.MapToDto(startBalance.MapToDto()));
    }

    public ViewingBalance.Response ViewingBalance(ViewingBalance.Request request)
    {
        long? findAccountNumber = _persistenceContext.UserSessionRepository.Find(request.SessionId);

        if (findAccountNumber == null)
        {
            return new ViewingBalance.Response.Unauthorized("Session not user");
        }

        long accountNumber = findAccountNumber.Value;

        Domain.Accounts.Account? account = _persistenceContext.AccountRepository.Find(accountNumber);

        if (account == null)
        {
            return new ViewingBalance.Response.BadRequest("Account not found");
        }

        Balance balance = account.ViewingBalance();

        var operation = new Operation(accountNumber, balance.Value, TypeOperation.ViewingBalance, DateTime.Now);
        _persistenceContext.OperationRepository.AddOperation(operation);

        return new ViewingBalance.Response.Success(balance.MapToDto());
    }

    public Deposit.Response Deposit(Deposit.Request request)
    {
        long? findAccountNumber = _persistenceContext.UserSessionRepository.Find(request.SessionId);

        if (findAccountNumber == null)
        {
            return new Deposit.Response.Unauthorized("Session not user");
        }

        long accountNumber = findAccountNumber.Value;

        Domain.Accounts.Account? account = _persistenceContext.AccountRepository.Find(accountNumber);

        if (account == null)
        {
            return new Deposit.Response.BadRequest("Account not found");
        }

        DepositResultType result = account.Deposit(request.Amount);

        if (result is DepositResultType.Failure(var textError))
        {
            return new Deposit.Response.BadRequest(textError);
        }

        _persistenceContext.AccountRepository.Update(account);

        var operation = new Operation(accountNumber, account.ViewingBalance().Value, TypeOperation.Deposit, DateTime.Now);
        _persistenceContext.OperationRepository.AddOperation(operation);

        return new Deposit.Response.Success();
    }

    public Withdraw.Response Withdraw(Withdraw.Request request)
    {
        long? findAccountNumber = _persistenceContext.UserSessionRepository.Find(request.SessionId);

        if (findAccountNumber == null)
        {
            return new Withdraw.Response.Unauthorized("Session not user");
        }

        long accountNumber = findAccountNumber.Value;

        Domain.Accounts.Account? account = _persistenceContext.AccountRepository.Find(accountNumber);

        if (account == null)
        {
            return new Withdraw.Response.BadRequest("Account not found");
        }

        WithdrawResultType result = account.Withdraw(request.Amount);

        if (result is WithdrawResultType.Failure(var textError))
        {
            return new Withdraw.Response.BadRequest(textError);
        }

        _persistenceContext.AccountRepository.Update(account);

        var operation = new Operation(accountNumber, account.ViewingBalance().Value, TypeOperation.Withdraw, DateTime.Now);
        _persistenceContext.OperationRepository.AddOperation(operation);

        return new Withdraw.Response.Success();
    }

    public GetHistoryOperation.Response GetHistoryOperation(GetHistoryOperation.Request request)
    {
        long? findAccountNumber = _persistenceContext.UserSessionRepository.Find(request.SessionId);

        if (findAccountNumber == null)
        {
            return new GetHistoryOperation.Response.Unauthorized("Session not user");
        }

        long accountNumber = findAccountNumber.Value;

        IEnumerable<Operation> operations =
            _persistenceContext.OperationRepository.Query(new OperationQuery(accountNumber));

        return new GetHistoryOperation.Response.Success(operations.Select(x => x.MapToDto()));
    }
}