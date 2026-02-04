using ATM.Application.Abstractions;
using ATM.Application.Abstractions.Repositories;

namespace ATM.Infrastructure;

internal sealed class PersistenceContext : IPersistenceContext
{
    public PersistenceContext(
        IAccountRepository accountRepository,
        IAdminSessionRepository adminSessionRepository,
        IOperationRepository operationRepository,
        IUserSessionRepository userSessionRepository)
    {
        AccountRepository = accountRepository;
        AdminSessionRepository = adminSessionRepository;
        OperationRepository = operationRepository;
        UserSessionRepository = userSessionRepository;
    }

    public IAccountRepository AccountRepository { get; }

    public IAdminSessionRepository AdminSessionRepository { get; }

    public IOperationRepository OperationRepository { get; }

    public IUserSessionRepository UserSessionRepository { get; }
}