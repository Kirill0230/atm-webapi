using ATM.Application.Abstractions;
using ATM.Application.Contracts.Interfaces;
using ATM.Application.Contracts.Models;
using ATM.Application.Contracts.Operations;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ATM.Tests")]

namespace ATM.Application.Services.Session;

internal class SessionService : ISessionService
{
    private readonly string _systemPassword;

    private readonly IPersistenceContext _persistenceContext;

    public SessionService(IPersistenceContext persistenceContext, string systemPassword)
    {
        _persistenceContext = persistenceContext;
        _systemPassword = systemPassword;
    }

    public CreateAdminSession.Response CreateAdminSession(CreateAdminSession.Request request)
    {
        if (request.SystemPassword != _systemPassword)
        {
            return new CreateAdminSession.Response.Failure("Invalid system password");
        }

        var id = Guid.NewGuid();
        _persistenceContext.AdminSessionRepository.Add(id);
        return new CreateAdminSession.Response.Success(new SessionDto(id));
    }

    public CreateUserSession.Response CreateUserSession(CreateUserSession.Request request)
    {
        Domain.Accounts.Account? account = _persistenceContext.AccountRepository.Find(request.AccountNumber);

        if (account == null)
        {
            return new CreateUserSession.Response.Failure("Account not found");
        }

        if (account.IsCorrectPin(request.Pincode) is false)
        {
            return new CreateUserSession.Response.Failure("InvalidPinCode");
        }

        var id = Guid.NewGuid();
        _persistenceContext.UserSessionRepository.Add(id, request.AccountNumber);

        return new CreateUserSession.Response.Success(new SessionDto(id));
    }
}