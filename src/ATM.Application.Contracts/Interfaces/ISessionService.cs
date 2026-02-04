using ATM.Application.Contracts.Operations;

namespace ATM.Application.Contracts.Interfaces;

public interface ISessionService
{
    CreateAdminSession.Response CreateAdminSession(CreateAdminSession.Request request);

    CreateUserSession.Response CreateUserSession(CreateUserSession.Request request);
}