using ATM.Application.Abstractions;
using ATM.Application.Abstractions.Repositories;
using ATM.Application.Contracts.Operations;
using ATM.Application.Services.Account;
using ATM.Domain.Accounts;
using NSubstitute;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.ATM.Tests;

public sealed class TestBankSystem
{
    [Fact]
    public void Withdraw_WhenSufficientBalance_ShouldUpdateAccountBalance()
    {
        IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
        IUserSessionRepository userSessionRepository = Substitute.For<IUserSessionRepository>();
        IAdminSessionRepository adminSessionRepository = Substitute.For<IAdminSessionRepository>();
        IOperationRepository operationRepository = Substitute.For<IOperationRepository>();

        IPersistenceContext persistenceContext = Substitute.For<IPersistenceContext>();
        persistenceContext.AccountRepository.Returns(accountRepository);
        persistenceContext.UserSessionRepository.Returns(userSessionRepository);
        persistenceContext.AdminSessionRepository.Returns(adminSessionRepository);
        persistenceContext.OperationRepository.Returns(operationRepository);

        var accountService = new AccountService(persistenceContext);

        var id = Guid.NewGuid();
        long accountNumber = 10;
        long pincode = 1111;

        var account = new Account(accountNumber, pincode, new Balance(12000));

        userSessionRepository.Find(id).Returns(accountNumber);
        accountRepository.Find(accountNumber).Returns(account);

        var request = new Withdraw.Request(id, 1000);

        Withdraw.Response response = accountService.Withdraw(request);

        Assert.True(response is Withdraw.Response.Success);
        Assert.Equal(11000, account.ViewingBalance().Value);
    }

    [Fact]
    public void Withdraw_WhenNotSufficientBalance_ShouldReturnBadRequest()
    {
        IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
        IUserSessionRepository userSessionRepository = Substitute.For<IUserSessionRepository>();
        IAdminSessionRepository adminSessionRepository = Substitute.For<IAdminSessionRepository>();
        IOperationRepository operationRepository = Substitute.For<IOperationRepository>();

        IPersistenceContext persistenceContext = Substitute.For<IPersistenceContext>();
        persistenceContext.AccountRepository.Returns(accountRepository);
        persistenceContext.UserSessionRepository.Returns(userSessionRepository);
        persistenceContext.AdminSessionRepository.Returns(adminSessionRepository);
        persistenceContext.OperationRepository.Returns(operationRepository);

        var accountService = new AccountService(persistenceContext);

        var id = Guid.NewGuid();
        long accountNumber = 10;
        long pincode = 1111;

        var account = new Account(accountNumber, pincode, new Balance(12000));

        userSessionRepository.Find(id).Returns(accountNumber);
        accountRepository.Find(accountNumber).Returns(account);

        var request = new Withdraw.Request(id, 100000);

        Withdraw.Response response = accountService.Withdraw(request);

        Assert.True(response is Withdraw.Response.BadRequest);
        Assert.Equal(12000, account.ViewingBalance().Value);
    }

    [Fact]
    public void Deposit_WhenDepositOperation_ShouldSuccessUpdateBalance()
    {
        IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
        IUserSessionRepository userSessionRepository = Substitute.For<IUserSessionRepository>();
        IAdminSessionRepository adminSessionRepository = Substitute.For<IAdminSessionRepository>();
        IOperationRepository operationRepository = Substitute.For<IOperationRepository>();

        IPersistenceContext persistenceContext = Substitute.For<IPersistenceContext>();
        persistenceContext.AccountRepository.Returns(accountRepository);
        persistenceContext.UserSessionRepository.Returns(userSessionRepository);
        persistenceContext.AdminSessionRepository.Returns(adminSessionRepository);
        persistenceContext.OperationRepository.Returns(operationRepository);

        var accountService = new AccountService(persistenceContext);

        var id = Guid.NewGuid();
        long accountNumber = 10;
        long pincode = 1111;

        var account = new Account(accountNumber, pincode, new Balance(12000));

        userSessionRepository.Find(id).Returns(accountNumber);
        accountRepository.Find(accountNumber).Returns(account);

        var request = new Deposit.Request(id, 100000);

        Deposit.Response response = accountService.Deposit(request);

        Assert.True(response is Deposit.Response.Success);
        Assert.Equal(112000, account.ViewingBalance().Value);
    }
}