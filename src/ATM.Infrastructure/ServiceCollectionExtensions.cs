using ATM.Application.Abstractions;
using ATM.Application.Abstractions.Repositories;
using ATM.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ATM.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection collections)
    {
        collections.AddScoped<IPersistenceContext, PersistenceContext>();

        collections.AddSingleton<IAccountRepository, AccountInMemoryRepository>();
        collections.AddSingleton<IAdminSessionRepository, AdminSessionInMemoryRepository>();
        collections.AddSingleton<IOperationRepository, OperationInMemoryRepository>();
        collections.AddSingleton<IUserSessionRepository, UserSessionInMemoryRepository>();

        return collections;
    }
}