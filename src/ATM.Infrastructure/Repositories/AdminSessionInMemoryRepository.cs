using ATM.Application.Abstractions.Repositories;

namespace ATM.Infrastructure.Repositories;

public class AdminSessionInMemoryRepository : IAdminSessionRepository
{
    private readonly List<Guid> _values = [];

    public void Add(Guid id)
    {
        _values.Add(id);
    }

    public bool CheckId(Guid id)
    {
        return _values.Contains(id);
    }
}