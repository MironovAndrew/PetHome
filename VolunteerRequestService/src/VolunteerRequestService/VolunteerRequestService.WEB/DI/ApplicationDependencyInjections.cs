﻿using FluentValidation.AspNetCore;
using VolunteerRequestService.WEB.DI.ApplicationDI;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using PetHome.VolunteerRequests.Application.Inject;

namespace VolunteerRequestService.WEB.DI;

public static class ApplicationDependencyInjections
{
    public static IServiceCollection AddApplicationDependencyInjection(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddVolunteerRequestsServices();
        services.AddRedis(configuration);
        services.AddMassTransitConfig(configuration);
        services.AddOpenTelemetryMetrics();
        services.AddControllers();
        services.AddCors();
        services.ApplyAuthenticationAuthorizeConfiguration(configuration);
        services.AddSwaggerGetWithAuthentication();
        //services.AddFluentValidationAutoValidation(configuration =>
        //{
        //    configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
        //});


        return services;
    }
}
