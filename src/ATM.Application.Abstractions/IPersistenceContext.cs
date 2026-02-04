using ATM.Application.Abstractions.Repositories;

namespace ATM.Application.Abstractions;

public interface IPersistenceContext
{
    IAccountRepository AccountRepository { get; }

    IAdminSessionRepository AdminSessionRepository { get; }

    IOperationRepository OperationRepository { get; }

    IUserSessionRepository UserSessionRepository { get; }
}