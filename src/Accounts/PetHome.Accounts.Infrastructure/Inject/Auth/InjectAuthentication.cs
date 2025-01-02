﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PetHome.Accounts.Application;
using PetHome.Accounts.Domain;
using System.Text;

namespace PetHome.Accounts.Infrastructure.Inject.Auth;
public static class InjectAuthentication
{
    public static IServiceCollection ApplyAuthenticationConfiguration(this IServiceCollection services)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateIssuerSigningKey = true,
                     ValidateLifetime = false,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("key"))
                 };
             });
        services.AddAuthorization();

        services.AddTransient<ITokenProvider, JwtTokenProvider>();

        services.AddIdentity<User, Role>(options =>
        {
            options.GetAuthenticationOptions();
        })
            .AddEntityFrameworkStores<AuthorizationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
