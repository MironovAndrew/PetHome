﻿using Microsoft.AspNetCore.Identity;

namespace PetHome.Accounts.Infrastructure.Inject.Auth;
public static class OptionsAuth
{
    public static IdentityOptions GetAuthenticationOptions(this IdentityOptions options)
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;

        return options;
    }
}