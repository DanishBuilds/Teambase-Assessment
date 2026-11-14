using Microsoft.Extensions.DependencyInjection;
using Teambase.Assessment.Application.Abstractions;
using Teambase.Assessment.Application.Services;

namespace Teambase.Assessment.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<PremiumCalculator>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IPremiumService, PremiumService>();
        return services;
    }
}