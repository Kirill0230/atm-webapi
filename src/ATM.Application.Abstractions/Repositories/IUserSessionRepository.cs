namespace ATM.Application.Abstractions.Repositories;

public interface IUserSessionRepository
{
    void Add(Guid id, long accountNumber);

    long? Find(Guid id);
}