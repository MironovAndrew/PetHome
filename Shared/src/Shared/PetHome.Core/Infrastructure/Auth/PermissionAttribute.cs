﻿using Microsoft.AspNetCore.Authorization;

namespace PetHome.Core.Infrastructure.Auth;
public class PermissionAttribute : AuthorizeAttribute, IAuthorizationRequirement
{
    public string Code { get; private set; }

    public PermissionAttribute(string code) : base(policy: code)
    {
        Code = code;
    }
}
