namespace ATM.Application.Abstractions.Repositories;

public interface IAdminSessionRepository
{
    void Add(Guid id);

    bool CheckId(Guid id);
}