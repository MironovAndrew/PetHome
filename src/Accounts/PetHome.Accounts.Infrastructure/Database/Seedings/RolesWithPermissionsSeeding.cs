﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Logs;
using PetHome.Accounts.Domain.Aggregates.RolePermission;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PetHome.Accounts.Infrastructure.Database.Seedings;
public static class RolesWithPermissionsSeeding
{
    public static string JSON_FOLDER_PATH = "..\\Accounts\\PetHome.Accounts.Infrastructure\\Auth\\Permissions\\JsonConfigurations\\";
    public static string ROLES_JSON_FILE_NAME = "Roles.json";
    public static string PERMISSIONS_JSON_FILE_NAME = "Permissions.json";

    private static List<Role> _roles = new List<Role>();
    private static List<Permission> _permissions = new List<Permission>();

    public static IServiceCollection SeedRolesWithPermissions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AuthorizationDbContext dbContext = new AuthorizationDbContext();

        if (dbContext.Roles.Count() == 0)
        {
            SeedRoles(dbContext);
            SeedPermissions(dbContext);

            dbContext.Roles.AddRange(_roles);
            dbContext.Permissions.AddRange(_permissions);

            var rolesPermissions = RolePermission.Create(_roles);

            dbContext.RolesPermissions.AddRange(rolesPermissions);
            dbContext.SaveChanges();
        }

        return services;
    }

    private static void SeedRoles(AuthorizationDbContext dbContext)
    {
        string roleJsonPath = JSON_FOLDER_PATH + ROLES_JSON_FILE_NAME;
        string jsonRolesString = File.ReadAllText(roleJsonPath);
        var jsonRolesObject = JsonNode.Parse(jsonRolesString);

        if (jsonRolesObject is not null)
        {
            var rolesStrings = jsonRolesObject?["Roles"]?["Items"]?.AsArray()?.Deserialize<string[]>();
            _roles = rolesStrings?.Select(r => Role.Create(r).Value).ToList();
        }
    }

    private static async void SeedPermissions(AuthorizationDbContext dbContext)
    {
        string permissionJsonPath = JSON_FOLDER_PATH + PERMISSIONS_JSON_FILE_NAME;
        string jsonPermissionString = File.ReadAllText(permissionJsonPath);
        var jsonPermissionObject = JsonNode.Parse(jsonPermissionString);

        if (jsonPermissionObject is not null)
        {
            foreach (var role in _roles)
            {
                List<Permission>? _rolePermissions = new List<Permission>();

                var permissionStrings = jsonPermissionObject?["Permissions"]?[role.Name]?
                    .AsArray()?.Deserialize<string[]>().ToList();

                _rolePermissions = permissionStrings? 
                        .Select(r => Permission.Create(r).Value).ToList();

                if (_rolePermissions is not null && _rolePermissions.Count>0)
                {
                    role.SetPermissions(_rolePermissions);
                    _permissions.AddRange(_rolePermissions);
                }
            }
        }
    }
}
