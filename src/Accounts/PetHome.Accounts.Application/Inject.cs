﻿using Microsoft.Extensions.DependencyInjection;
using PetHome.Core.Interfaces.FeatureManagment;

namespace PetHome.Accounts.Application;
public static class Inject
{
    public static IServiceCollection AddAccountsServices(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
        .AddClasses(classes => classes
            .AssignableToAny(
                typeof(ICommandHandler<>), typeof(ICommandHandler<,>),
                typeof(IQueryHandler<>), typeof(IQueryHandler<,>)))
        .AsSelfWithInterfaces()
        .WithScopedLifetime());

        return services;
    }
}