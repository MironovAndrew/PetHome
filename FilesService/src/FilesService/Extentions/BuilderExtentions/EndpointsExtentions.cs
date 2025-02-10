﻿using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using FilesService.Application.Endpoints;

namespace FilesService.Extentions.BuilderExtentions;

public static class EndpointsExtentions
{
    public static IServiceCollection AddEndpoints(
        this IServiceCollection services)
    {
        services.AddEndpoints(Assembly.GetExecutingAssembly());
        return services;
    }


    public static IApplicationBuilder MapEndpoints(
        this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
        IEndpointRouteBuilder builder = routeGroupBuilder is null
            ? app
            : routeGroupBuilder;

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }


    private static IServiceCollection AddEndpoints(
        this IServiceCollection services, Assembly assembly)
    {
        var serviceDescriptor = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false }
                  & type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptor);

        return services;
    }
}
