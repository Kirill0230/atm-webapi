using ATM.Application.Abstractions.Repositories;

namespace ATM.Infrastructure.Repositories;

public class UserSessionInMemoryRepository : IUserSessionRepository
{
    private readonly Dictionary<Guid, long> _values = [];

    public void Add(Guid id, long accountNumber)
    {
        _values[id] = accountNumber;
    }

    public long? Find(Guid id)
    {
        if (_values.ContainsKey(id))
        {
            return _values[id];
        }

        return null;
    }
}