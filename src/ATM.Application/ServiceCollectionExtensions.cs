using ATM.Application.Contracts.Interfaces;
using ATM.Application.Services.Account;
using ATM.Application.Services.Session;
using Microsoft.Extensions.DependencyInjection;

namespace ATM.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collections, string systemPassword)
    {
        collections.AddSingleton(systemPassword);

        collections.AddScoped<IAccountService, AccountService>();
        collections.AddScoped<ISessionService, SessionService>();

        return collections;
    }
}