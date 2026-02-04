using ATM.Domain.Accounts;

namespace ATM.Application.Abstractions.Repositories;

public interface IAccountRepository
{
    bool Update(Account account);

    Account Add(Account account);

    Account? Find(long numberAccount);
}