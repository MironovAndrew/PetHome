﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Minio.AspNetCore;
using PetHome.Application.Database;
using PetHome.Application.Interfaces;
using PetHome.Application.Interfaces.RepositoryInterfaces;
using PetHome.Application.Messaging;
using PetHome.Infrastructure.Background;
using PetHome.Infrastructure.DataBase;
using PetHome.Infrastructure.DataBase.DBContexts;
using PetHome.Infrastructure.DataBase.Repositories.Write;
using PetHome.Infrastructure.MessageQueues;
using PetHome.Infrastructure.Providers.Minio;
using MinioOptions = PetHome.Infrastructure.Options.MinioOptions;

namespace PetHome.Infrastructure;
public static class Inject
{
    public static IServiceCollection AddInfrastructure(
       this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<WriteDBContext>();
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
        services.AddMinio(configuration);
        //services.AddScoped<IFilesProvider, MinioProvider>();
        services.AddSingleton<IFilesProvider, MinioProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IMessageQueue, FilesCleanerMessageQueue>();
        services.AddHostedService<FilesCleanerHostedService>();
        return services;
    }

    private static IServiceCollection AddMinio(
        this IServiceCollection services, ConfigurationManager configuration)
    {
        var minioOptions = configuration.GetSection(MinioOptions.MINIO_NAME).Get<MinioOptions>()
            ?? throw new Exception("Ошибка со строкой подключения minio. Проверьте конфигурацию.");

        services.AddMinio(options =>
        {
            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.Accesskey, minioOptions.Secretkey);
            options.WithSSL(minioOptions.IsWithSSL);
        });
        return services;
    }

}
