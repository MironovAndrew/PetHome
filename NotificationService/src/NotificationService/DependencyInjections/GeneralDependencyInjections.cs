﻿using NotificationService.DependencyInjections.ApplicationDependencyInjections;
using NotificationService.DependencyInjections.InfrastructureDependencyInjections;

namespace NotificationService.DependencyInjections;

public static class GeneralDependencyInjections
{
    public static IServiceCollection AddDependencyInjections(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddServices();
        services.AddOptions(configuration);
        services.AddMassTransitConfig(configuration);

        return services;
    }
}
