using Api.Services;

namespace Api.Extensions;

public static class AccountServiceExtension
{
    public static IServiceCollection AddAccountService(this IServiceCollection services)
    {
        return services.AddScoped<IAccountService, AccountService>();
    }
}