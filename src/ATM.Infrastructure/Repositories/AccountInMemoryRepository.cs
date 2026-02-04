using ATM.Application.Abstractions.Repositories;
using ATM.Domain.Accounts;

namespace ATM.Infrastructure.Repositories;

public class AccountInMemoryRepository : IAccountRepository
{
    private readonly List<Account> _values = [];

    public bool Update(Account account)
    {
        Account? last = Find(account.NumberAccount);

        if (last == null)
        {
            return false;
        }

        _values[_values.IndexOf(last)] = account;
        return true;
    }

    public Account Add(Account account)
    {
        if (Find(account.NumberAccount) != null)
        {
            _values[_values.IndexOf(account)] = account;
        }

        _values.Add(account);

        return account;
    }

    public Account? Find(long numberAccount)
    {
        return _values.Find(x => x.NumberAccount == numberAccount);
    }
}