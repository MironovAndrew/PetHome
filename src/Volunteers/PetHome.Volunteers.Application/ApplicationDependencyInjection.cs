﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHome.Core.Interfaces.FeatureManagment;

namespace PetHome.Volunteers.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddVolunteerServices(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(ApplicationDependencyInjection).Assembly)
        .AddClasses(classes => classes
            .AssignableToAny(
                typeof(ICommandHandler<>), typeof(ICommandHandler<,>),
                typeof(IQueryHandler<>), typeof(IQueryHandler<,>)))
        .AsSelfWithInterfaces()
        .WithScopedLifetime());

        services.AddValidatorsFromAssembly(typeof(ApplicationDependencyInjection).Assembly);

        return services;
    }
}
