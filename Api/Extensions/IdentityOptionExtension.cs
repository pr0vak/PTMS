using Microsoft.AspNetCore.Identity;

namespace Api.Extensions;

public static class IdentityOptionExtension
{
    public static IServiceCollection AddConfigureIdentityOptions(this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 3;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;

        });

        return services;
    }
}