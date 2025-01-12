using System.Reflection;
using FluentValidation;

namespace Api.Extensions;

public static class FluentValidationServiceExtension
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}